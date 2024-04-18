using API.DBContext;
using API.Enity;
using API.Model;
using API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace API.Services
{
    #region "user"
    public class UserService
    {
        private DB dBContext;
        private IUserRepositories userRepositories;
        public UserService(DB dBContext)
        {

            this.dBContext = dBContext;
            this.userRepositories = new UserRepository(dBContext);
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public void insert(User user)
        {

            userRepositories.insertUser(user);
            Save();
        }
        public void update(User user)
        {
            userRepositories.updateUser(user);
            Save();
        }
        public void delete(User user)
        {
            userRepositories.deleteUser(user);
            Save();
        }
    }
    #endregion  
    #region "exam"
    public class ExamService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IExamRepositories examRepositories;
        public ExamService(DB dBContext, UserManager<IdentityUser> userManager)
        {
            this.dBContext = dBContext;
            this.examRepositories = new ExamRepository(dBContext);
            this._userManager = userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(ExamDTO ex)
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
                examRepositories.insertExam(exam);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(ExamDTO ex)
        {
            var user = await _userManager.FindByNameAsync(ex.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var exam = GetbyId(ex.ExamID);
                if (exam == null)
                {
                    examRepositories.updateExam(exam);
                    Save();
                    return new Response { Status = true, Message = "Success"};
                }
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(ExamDTO ex)
        {
            var user = await _userManager.FindByNameAsync(ex.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Exam exam = GetbyId(ex.ExamID);
                if (exam == null)
                {
                    examRepositories.deleteExam(exam);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }                
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public Exam GetbyId(int id)
        {
            return examRepositories.getbyid(id);
        }
/*        public List<Exam> List(string id)
        {
            return examRepositories.Listall(id);
        }*/
    }
    #endregion
    #region "question"
    public class QuestionService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IQuestionRepositories questionRepositories;
        public QuestionService(DB dBContext, UserManager<IdentityUser> _userManager)
        {
            this.dBContext = dBContext;
            this.questionRepositories = new QuestionRepository(dBContext);
            this._userManager = _userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(QuestionDTO ques)
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
                questionRepositories.insertQuestion(question);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(QuestionDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var question = GetbyId(ques.QuestionID);
                if (question == null)
                {
                    questionRepositories.updateQuestion(question);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(QuestionDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var question = GetbyId(ques.QuestionID);
                if (question == null)
                {
                    questionRepositories.deletetQuestion(question);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public Question GetbyId(int id)
        {
            return questionRepositories.getbyid(id);
        }
        public List<Question> List(int id)
        {
            return questionRepositories.Listall(id);
        }
    }
    #endregion
    #region "result"
    public class ResultService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IResultRepositories resultRepositories;
        public ResultService(DB dBContext, UserManager<IdentityUser> userManager)
        {
            this.dBContext = dBContext;
            this.resultRepositories = new ResultRepository(dBContext);
            this._userManager = userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(ResultDTO re)
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
                resultRepositories.insertResult(result);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(ResultDTO re)
        {
            var user = await _userManager.FindByNameAsync(re.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var result = GetbyId(re.ExamID);
                if (result == null)
                {
                    resultRepositories.updateResult(result);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(ResultDTO re)
        {
            var user = await _userManager.FindByNameAsync(re.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var result = GetbyId(re.ExamID);
                if (result == null)
                {
                    resultRepositories.deleteResult(result);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public Result GetbyId(int id)
        {
            return resultRepositories.getbyid(id);
        }
        public List<Result> List(int idExam, string idUser)
        {
            return resultRepositories.Listall(idExam,idUser);
        }
    }
    #endregion
}
