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
        SentenceCompServices sentenceCompService;
        QuestionComServices questionComServices;
        private RoleManager<IdentityRole> _roleManager
        {
            get;
        }
        public UserAPI(DB dBContext, UserService userService,
           RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> _userManager,
           SignInManager<IdentityUser> _signInManager, IConfiguration _configuration, 
           SentenceCompServices sentenceCompService,
            QuestionComServices questionComServices)
        {
            this.dBContext = dBContext;
            this.userService = userService;
            this._roleManager = roleManager;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._configuration = _configuration;
            this.questionComServices = questionComServices;
            this.sentenceCompService = sentenceCompService;
        }
        #region "questioncomplete"
        [HttpPost]
        [Route("addquescom")]
        public async Task<IActionResult> CreQuesCom([FromBody] QuestionCompleteDTO ques)
        {
            var status = await questionComServices.insert(ques);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpPut]
        [Route("updatequescom")]
        public async Task<IActionResult> UpQuesCom([FromBody] QuestionCompleteDTO ques)
        {
            var status = await questionComServices.update(ques);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpDelete]
        [Route("deletequescom")]
        public async Task<IActionResult> DelQuesCom([FromBody] QuestionCompleteDTO ques)
        {
            var status = await questionComServices.delete(ques);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpGet]
        [Route("getquescom")]
        public IActionResult ListQuesCom(int id)
        {
            return Ok(questionComServices.List(id));
        }
        [HttpGet]
        [Route("getcorrect")]
        public IActionResult getCorrect(int id)
        {
            return Ok(questionComServices.Correct(id));
        }
        #endregion

        #region "sentencecomplete"
        [HttpPost]
        [Route("addsencom")]
        public async Task<IActionResult> CreSenCom([FromBody] SentenceComDTO sen)
        {
            var status = await sentenceCompService.insert(sen);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpPut]
        [Route("updatesencom")]
        public async Task<IActionResult> UpSenCom([FromBody] SentenceComDTO sen)
        {
            var status = await sentenceCompService.update(sen);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpDelete]
        [Route("deletesencom")]
        public async Task<IActionResult> DelSenCom([FromBody] SentenceComDTO sen)
        {
            var status = await sentenceCompService.delete(sen);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        #endregion
    }
}
