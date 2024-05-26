using API.DBContext;
using API.Enity;
using API.Model;
using API.Model.DTO;
using API.Model.GetDTO;
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
        SentenceCompServices sentenceCompService;
        private RoleManager<IdentityRole> _roleManager
        {
            get;
        }
        public UserAPI(DB dBContext, UserService userService,
           RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> _userManager,
           SignInManager<IdentityUser> _signInManager, IConfiguration _configuration, 
           SentenceCompServices sentenceCompService)
        {
            this.dBContext = dBContext;
            this.userService = userService;
            this._roleManager = roleManager;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._configuration = _configuration;
            this.sentenceCompService = sentenceCompService;
        }
        #region "questioncomplete"
        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> Submit([FromBody] SubmitDTO submit ) 
        {
            var status = await sentenceCompService.submit(submit);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpDelete]
        [Route("repractice")]
        public IActionResult rePractice([FromBody] BobDTO bob)
        {
            var status = sentenceCompService.delete(bob);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }

        [HttpGet]
        [Route("getquescom")]
        public async Task<IActionResult> ListQuesCom(string id,string userid)
        {
            List<QuestionComGDTO> questions = await sentenceCompService.List(id, userid);
            return Ok(questions);
        }
        [HttpGet]
        [Route("getsencom")]
        public async Task<IActionResult> GetSenCom(string id, string userid)
        {
            return Ok(await sentenceCompService.getSenCom(id, userid));
        }
        #endregion

    }
}
