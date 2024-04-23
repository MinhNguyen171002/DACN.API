using API.DBContext;
using API.Enity;
using API.Model;
using API.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using MySqlConnector.Logging;
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
        ExamService examService;
        QuestionService questionService;
        ResultService resultService;
        PracticeService practiceService;
        public AdminAPI(DB dBContext,ExamService examService, QuestionService questionService, ResultService resultService,
            PracticeService practiceService)
        {
            this.dBContext = dBContext;
            this.examService = examService;
            this.questionService = questionService;
            this.resultService = resultService;
            this.practiceService = practiceService;
        }
    
        #region "exam"
        [HttpPost]
        [Route("addexam")]
        public async Task<IActionResult> CreExam([FromBody] ExamDTO ex)
        {
            var status = await examService.insert(ex);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }                
            return Ok(status);
        }
        [HttpPut]
        [Route("updatexam")]
        public async Task<IActionResult> UpExam([FromBody] ExamDTO ex)
        {
            var status = await examService.update(ex);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpDelete]
        [Route("deletexam")]
        public async Task<IActionResult> DelExam([FromBody] ExamDTO ex)
        {
            var status = await examService.delete(ex);
            if (status.Status.Equals(false))
            { 
                return Ok(status);                
            }
            return Ok(status);
        }
        [HttpGet]
        [Route("getexams")]
        public IActionResult ListExam(string skill)
        {
            return Ok(examService.List(skill));
        }
        #endregion

        #region "question"
        [HttpPost]
        [Route("addquestion")]
        public async Task<IActionResult> CreQuestion([FromBody] QuestionDTO ques)
        {
            var status = await questionService.insert(ques);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpPut]
        [Route("updatequestion")]
        public async Task<IActionResult> UpQuestion([FromBody] QuestionDTO ques)
        {
            var status = await questionService.update(ques);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpDelete]
        [Route("updatequestion")]
        public async Task<IActionResult> DelQuestion([FromBody] QuestionDTO ques)
        {
            var status = await questionService.delete(ques);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpGet]
        [Route("listquestion")]
        public IActionResult ListQuestion(int id)
        {
            return Ok(questionService.List(id));
        }
        #endregion

        #region "result"
        [HttpPost]
        [Route("addresult")]
        public async Task<IActionResult> CreResult([FromBody] ResultDTO re)
        {
            var status = await resultService.insert(re);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpDelete]
        [Route("deleteresult")]
        public async Task<IActionResult> DelResult([FromBody]ResultDTO re)
        {
            var status = await resultService.delete(re);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        #endregion

        #region "practice"
        [HttpPost]
        [Route("addpractice")]
        public async Task<IActionResult> CrePractice([FromBody] PracticeDTO pra)
        {
            var status = await practiceService.insert(pra);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpPut]
        [Route("updatpractice")]
        public async Task<IActionResult> UpPractice([FromBody] PracticeDTO pra)
        {
            var status = await practiceService.update(pra);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpDelete]
        [Route("deletpractice")]
        public async Task<IActionResult> DelPractice([FromBody] PracticeDTO pra)
        {
            var status = await practiceService.delete(pra);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpGet]
        [Route("getpractices")]
        public IActionResult ListPratice(int id)
        {
            return Ok(practiceService.List(id));
        }
        #endregion
    }
}
