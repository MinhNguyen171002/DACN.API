using API.DBContext;
using API.Services;
using Microsoft.AspNetCore.Mvc;

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
        public AdminAPI(DB dBContext, UserService userService, LevelService levelService, 
            ExamService examService, QuestionService questionService, ResultService resultService)
        {
            this.dBContext = dBContext;
            this.userService = userService;
            this.levelService = levelService;
            this.examService = examService;
            this.questionService = questionService;
            this.resultService = resultService;
        }
        [HttpPost]
        [Route("add")]
    }
}
