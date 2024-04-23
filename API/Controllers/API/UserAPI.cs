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
    public class UserAPI : Controller
    {
        private DB dBContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        UserService userService;
        private RoleManager<IdentityRole> _roleManager
        {
            get;
        }
        public UserAPI(DB dBContext, UserService userService,
           RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> _userManager,
           SignInManager<IdentityUser> _signInManager, IConfiguration _configuration)
        {
            this.dBContext = dBContext;
            this.userService = userService;
            this._roleManager = roleManager;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._configuration = _configuration;
        }
    }
}
