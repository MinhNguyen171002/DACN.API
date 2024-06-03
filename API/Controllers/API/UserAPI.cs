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
        ExamService examService;
        UserService userService;
        SentenceCompServices sentenceCompService;
        QuestionService questionService;
        SentenceService sentenceService;
        public UserAPI(UserService userService,            
           SentenceCompServices sentenceCompService, QuestionService questionService,
            SentenceService sentenceService, ExamService examService)
        {
            this.userService = userService;
            this.sentenceCompService = sentenceCompService;
            this.questionService = questionService;
            this.sentenceService = sentenceService;
            this.examService = examService;
        }
        #region "userinfo"
        [HttpGet]
        [Route("userinfo")]
        public IActionResult getUser(string id)
        {
            UserGDTO user = userService.GetUser(id);
            return Ok(user);
        }
        [HttpGet]
        [Route("getexams")]
        public IActionResult ListExam()
        {
            return Ok(examService.List());
        }
        #endregion
        #region "questioncomplete"
        [HttpGet]
        [Route("allQues")]
        public IActionResult Listall()
        {
            List<QuestionGDTO> questions = questionService.ListAll();
            return Ok(questions);
        }
        [HttpGet]
        [Route("getquestions")]
        public IActionResult ListQuestion(string id)
        {
            List<QuestionGDTO> questions = questionService.List(id);
            return Ok(questions);
        }
        [HttpGet]
        [Route("getsentences")]
        public IActionResult ListSentence(string id, string userId)
        {
            List<UserSentenceDTO> sentences = sentenceService.List(id, userId);
            return Ok(sentences);
        }
        [HttpGet]
        [Route("getpractice")]
        public IActionResult GetPratice(string id, string userId)
        {
            PracticeDTO practice = sentenceService.getPractice(id, userId);
            return Ok(practice);
        }
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
