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
using System.Net.NetworkInformation;
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
        #region "authentication"
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
        #endregion
        #region "level"
        [HttpPost]
        [Route("addlevel")]
        public async Task<IActionResult> CreLevel(string username, string levelid ,string levelname)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user);
            if(roles.FirstOrDefault() == "admin")
            {
                Level level = new Level()
                {
                    LevelID = levelid,
                    LevelName = levelname,
                };
                levelService.insert(level);
                return Ok(new Response { Status = true, Message = "Succces"});
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });            
        }
        [HttpPut]
        [Route("updatelevel")]
        public async Task<IActionResult> UpLevel(string username, string id)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var level = levelService.getbyid(id);
                if (level == null)
                {
                    levelService.update(level);
                    return Ok(new Response { Status = true, Message = "Succces" });
                }
                return Ok(new Response { Status = false, Message = "Fail" });
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });
        }
        [HttpDelete]
        [Route("deletelevel")]
        public async Task<IActionResult> DelLevel(string username, string id)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var level = levelService.getbyid(id);
                if (level == null)
                {
                    levelService.delete(level);
                    return Ok(new Response { Status = true, Message = "Succces" });
                }
                return Ok(new Response { Status = false, Message = "Fail" });
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });
        }
        #endregion
        #region "exam"
        [HttpPost]
        [Route("addexam")]
        public async Task<IActionResult> CreExam([FromBody] MExam ex)
        {
            var user = await _userManager.FindByNameAsync(ex.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Exam exam = new Exam()
                {
                    ExamID = ex.ExamID,
                    ExamName = ex.ExamName,
                    ExamDescription = ex.ExamDescription,
                    ExamDuration = ex.ExamDuration,
                };
                examService.insert(exam);
                return Ok(new Response { Status = true, Message = "Succces" });
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });
        }
        [HttpPut]
        [Route("updatexam")]
        public async Task<IActionResult> UpExam([FromBody] MExam ex)
        {
            var user = await _userManager.FindByNameAsync(ex.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var exam = examService.GetbyId(ex.ExamID);
                if (exam == null)
                {
                    examService.update(exam);
                    return Ok(new Response { Status = true, Message = "Succces" });
                }
                return Ok(new Response { Status = false, Message = "Fail" });
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });
        }
        [HttpDelete]
        [Route("deletexam")]
        public async Task<IActionResult> DelExam([FromBody] MExam ex)
        {
            var user = await _userManager.FindByNameAsync(ex.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var exam = examService.GetbyId(ex.ExamID);
                if (exam == null)
                {
                    examService.delete(exam);
                    return Ok(new Response { Status = true, Message = "Succces" });
                }
                return Ok(new Response { Status = false, Message = "Fail" });
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });
        }
        #endregion
        #region "question"
        [HttpPost]
        [Route("addquestion")]
        public async Task<IActionResult> CreQuestion([FromBody] MQuestion ques)
        {
            var user = await _userManager.FindByNameAsync(ques.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Question question = new Question()
                {
                    QuestionID = ques.QuestionID,
                    QuestionContext = ques.QuestionContext,
                    Question1 = ques.Question1,
                    Question2 = ques.Question2,
                    Question3 = ques.Question3,
                    Question4 = ques.Question4,
                    CorrectAnswer = ques.CorrectAnswer,
                    ExamID = ques.ExamID,
                };
                questionService.insert(question);
                return Ok(new Response { Status = true, Message = "Succces" });
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });
        }
        [HttpPut]
        [Route("updatequestion")]
        public async Task<IActionResult> UpQuestion([FromBody] MQuestion ques)
        {
            var user = await _userManager.FindByNameAsync(ques.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var question = questionService.GetbyId(ques.QuestionID);
                if (question == null)
                {
                    questionService.update(question);
                    return Ok(new Response { Status = true, Message = "Succces" });
                }
                return Ok(new Response { Status = false, Message = "Fail" });
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });
        }
        [HttpDelete]
        [Route("deletequestion")]
        public async Task<IActionResult> DelQuestion([FromBody] MQuestion ques)
        {
            var user = await _userManager.FindByNameAsync(ques.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var question = questionService.GetbyId(ques.QuestionID);
                if (question == null)
                {
                    questionService.delete(question);
                    return Ok(new Response { Status = true, Message = "Succces" });
                }
                return Ok(new Response { Status = false, Message = "Fail" });
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });
        }
        #endregion
        #region "result"
        [HttpPost]
        [Route("addresult")]
        public async Task<IActionResult> CreResult([FromBody] MResult re)
        {
            var user = await _userManager.FindByNameAsync(re.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Result result = new Result()
                {
                    ResultID = re.ResultID,
                    ExamID = re.ExamID,
                    Score = re.Score,
                    UserID = re.UserID,
                };
                resultService.insert(result);
                return Ok(new Response { Status = true, Message = "Succces" });
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });
        }
        [HttpPut]
        [Route("updateresult")]
        public async Task<IActionResult> UpResult([FromBody] MResult re)
        {
            var user = await _userManager.FindByNameAsync(re.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var result = resultService.GetbyId(re.ExamID);
                if (result == null)
                {
                    resultService.update(result);
                    return Ok(new Response { Status = true, Message = "Succces" });
                }
                return Ok(new Response { Status = false, Message = "Fail" });
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });
        }
        [HttpDelete]
        [Route("deleteresult")]
        public async Task<IActionResult> DelResult(MResult re)
        {
            var user = await _userManager.FindByNameAsync(re.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var result = resultService.GetbyId(re.ExamID);
                if (result == null)
                {
                    resultService.delete(result);
                    return Ok(new Response { Status = true, Message = "Succces" });
                }
                return Ok(new Response { Status = false, Message = "Fail" });
            }
            return Ok(new Response { Status = false, Message = "Need authorization" });
        }
        #endregion

    }
}
