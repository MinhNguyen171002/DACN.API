using API.DBContext;
using API.Enity;
using API.Model;
using API.Model.DTO;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.API
{
    [Route("API/[controller]")]
    [ApiController]
    public class AdminAPI : Controller
    {
        private DB dBContext;
        ExamService examService;
        QuestionService questionService;
        SentenceService sentenceService;
        FileService fileService;
        public AdminAPI(DB dBContext,ExamService examService, 
            QuestionService questionService,
            SentenceService sentenceService,
            FileService fileService)
        {
            this.dBContext = dBContext;
            this.examService = examService;
            this.questionService = questionService;
            this.sentenceService = sentenceService;
            this.fileService = fileService;
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
        public async Task<IActionResult> CreQuestion([FromBody] QuestionFDTO fDTO)
        {
            var status = await questionService.insert(fDTO);
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
        [Route("getquestions")]
        public IActionResult ListQuestion(string id)
        {
            return Ok(questionService.List(id));
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
        public async Task<IActionResult> DelSentence([FromBody] SentenceDTO sen)
        {
            var status = await sentenceService.delete(sen);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        [HttpGet]
        [Route("getsentences")]
        public IActionResult ListSentence(int id,string name)
        {
            return Ok(sentenceService.List(id,name));
        }
        [HttpGet]
        [Route("getpractice")]
        public IActionResult GetPratice(int id, string name)
        {
            return Ok(sentenceService.getPractice(id,name));
        }
        #endregion

        #region"file"
        [HttpPost]
        [Route("postfile")]
        public async Task<IActionResult> postFile([FromForm]FileDTO file)
        {
            if (file.FileData == null || file.FileData.Length == 0)
                return BadRequest("File is empty");

            using (var memoryStream = new MemoryStream())
            {
                await file.FileData.CopyToAsync(memoryStream);
                var fileData = memoryStream.ToArray();
                return Ok(await fileService.insert(fileData, file.FileData.FileName, file.Question, file.userName,file.fileType));
            }
        }
        [HttpDelete]
        [Route("delfile")]
        public async Task<IActionResult> delFile([FromBody] FileDTO file)
        {
            var status = await fileService.delete(file);
            if (status.Status.Equals(false))
            {
                return Ok(status);
            }
            return Ok(status);
        }
        #endregion
    }
}
