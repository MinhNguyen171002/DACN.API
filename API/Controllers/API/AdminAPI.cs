using API.DBContext;
using API.Enity;
using API.Model;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers.API
{
    [Route("API/[controller]")]
    [ApiController]
    public class AdminAPI : Controller
    {
        private DB dBContext;
        UserService userService;
        LevelService levelService;
        ExamService examService;
        QuestionService questionService;
        ResultService resultService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private RoleManager<IdentityRole> _roleManager
        {
            get;
        }
        public AdminAPI(DB dBContext, UserService userService, LevelService levelService, 
            ExamService examService, QuestionService questionService, ResultService resultService, 
            RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> _userManager, 
            SignInManager<IdentityUser> _signInManager, IConfiguration _configuration)
        {
            this.dBContext = dBContext;
            this.userService = userService;
            this.levelService = levelService;
            this.examService = examService;
            this.questionService = questionService;
            this.resultService = resultService;
            this._roleManager = roleManager;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._configuration = _configuration;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new IdentityUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName                
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(Role.Customer))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Role.Customer));
                }
                await _userManager.AddToRoleAsync(user, Role.Customer);
            }
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            
            User user1 = new User()
            {
                UserID = user.Id,
                UserName = model.UserName,
                Email = model.Email,
                SDT = model.SDT
            };
            userService.insert(user1);
            return Ok(new Response { Status = true, Message = "Welcome" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> SignIn([FromBody] LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password) && (await _signInManager.PasswordSignInAsync(user, login.Password, false, false)).Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("id", user.Id)
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    expires: DateTime.UtcNow.AddSeconds(300),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                return Ok(new LoginResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Status = true
                });
            }
            return Unauthorized();
        }
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok(new Response { Message = "Logout success!", Status = true });
        }
    }
}
