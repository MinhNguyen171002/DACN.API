using API.DBContext;
using API.Enity;
using API.Model;
using API.Model.DTO;
using API.Model.GetDTO;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.API
{
    [Route("API/[controller]")]
    [ApiController]
    public class AdminAPI : Controller
    {
        UserService userService;
        ExamService examService;
        QuestionService questionService;
        SentenceService sentenceService;
        public AdminAPI(ExamService examService, 
            QuestionService questionService,
            SentenceService sentenceService,
            UserService userService)
        {
            this.examService = examService;
            this.questionService = questionService;
            this.sentenceService = sentenceService;
            this.userService = userService;
        }

        [HttpGet]
        [Route("getUsers")]
        public IActionResult ListUsers()
        {
            List<UserGDTO> user = userService.GetUsers();
            return Ok(user);
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
        public async Task<IActionResult> DelExam([FromBody] BobDTO bob)
        {
            var status = await examService.delete(bob);
            if (status.Status.Equals(false))
            { 
                return Ok(status);                
            }
            return Ok(status);
        }
        [HttpGet]
        [Route("getexams")]
        public IActionResult ListExam()
        {
            return Ok(examService.List());
        }
        #endregion

        #region "question"
        [HttpPost]
        [Route("addquestion")]
        public async Task<IActionResult> CreQuestion([FromForm] QuestionFDTO fDTO)
        {
            var status = await questionService.upload(fDTO);
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
        [Route("delsquestion")]
        public async Task<IActionResult> DelQuestion([FromBody] BobDTO bob)
        {
            var status = await questionService.delete(bob);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpGet]
        [Route("allQues")]
        public IActionResult Listall()
        {
            List<QuestionGDTO> questions = questionService.ListAll();
            return Ok(questions);
        }
        #endregion

        #region "sentence"
        [HttpPost]
        [Route("addsentence")]
        public async Task<IActionResult> CreSentence([FromBody] SentenceDTO sen)
        {
            var status = await sentenceService.insert(sen);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpPut]
        [Route("updatesentence")]
        public async Task<IActionResult> UpQSentence([FromBody] SentenceDTO sen)
        {
            var status = await sentenceService.update(sen);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpDelete]
        [Route("deletesentence")]
        public async Task<IActionResult> DelSentence([FromBody] BobDTO bob)
        {
            var status = await sentenceService.delete(bob);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpGet]
        [Route("allSentences")]
        public IActionResult ListSen()
        {
            List<SentencesGDTO> sentences = sentenceService.ListAll();
            return Ok(sentences);
        }
        #endregion
    }
}
