using API.DBContext;
using API.Enity;
using API.Model;
using API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.DirectoryServices.Protocols;

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
                Exam exam = examRepositories.getbyid(ex.ExamID);
                if (exam == null)
                {
                    Exam exam1 = new Exam()
                    {
                        ExamID = ex.ExamID,
                        Skill = ex.Skill,
                        ExamDescription = ex.ExamDescription,
                        ExamDuration = ex.ExamDuration,
                    };
                    examRepositories.insertExam(exam1);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Exam already exist" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(ExamDTO ex)
        {
            var user = await _userManager.FindByNameAsync(ex.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Exam exam = GetbyId(ex.ExamID);
                if (exam != null)
                {
                    exam.ExamDescription = ex.ExamDescription;
                    exam.ExamDuration = ex.ExamDuration;
                    exam.Skill = ex.Skill;
                    examRepositories.updateExam(exam);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Exam not found!!!" };
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
                if (exam != null)
                {
                    examRepositories.deleteExam(exam);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Exam not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public Exam GetbyId(int id)
        {
            return examRepositories.getbyid(id);
        }
        public List<ExamDTO> List(string skill)
        {
            List<ExamDTO> examDTOs = new List<ExamDTO>();
            var exams = examRepositories.Listall(skill);
            foreach (var exam in exams)
            {
                examDTOs.Add(new ExamDTO { ExamID = exam.ExamID, ExamDescription = exam.ExamDescription, Skill = exam.Skill, PracticeCount = examRepositories.Count(exam.ExamID) });
            }
            return examDTOs;            
        }
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
                Question test = questionRepositories.getbyid(ques.QuestionID);
                if (test == null)
                {
                    Question question = new Question()
                    {
                        QuestionID = ques.QuestionID,
                        QuestionContext = ques.QuestionContext,
                        Answer1 = ques.Answer1,
                        Answer2 = ques.Answer2,
                        Answer3 = ques.Answer3,
                        Answer4 = ques.Answer4,
                        CorrectAnswer = ques.CorrectAnswer,
                        SentenceID = ques.SentenceID,
                };
                    questionRepositories.insertQuestion(question);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Question already exist" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(QuestionDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Question question = GetbyId(ques.QuestionID);
                if (question != null)
                {
                    question.Answer1 = ques.Answer1;
                    question.Answer1 = ques.Answer2;
                    question.Answer3 = ques.Answer3;
                    question.Answer4 = ques.Answer4;
                    question.QuestionContext = ques.QuestionContext;
                    question.CorrectAnswer = ques.CorrectAnswer;
                    question.SentenceID = ques.SentenceID;
                    questionRepositories.updateQuestion(question);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Question not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(QuestionDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Question question = GetbyId(ques.QuestionID);
                if (question != null)
                {
                    questionRepositories.deleteQuestion(question);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Question not found!!!" };
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
                Result result = resultRepositories.getbyid(re.ResultID);
                if (result == null)
                {
                    Result result1 = new Result()
                    {
                        ResultID = re.ResultID,
                        PracticeID = re.PracticeID,
                        Score = re.Score,
                        UserID = user.Id,
                    };
                    resultRepositories.insertResult(result1);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Result already exist" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        /*public async Task<Response> update(ResultDTO re)
        {
            var user = await _userManager.FindByNameAsync(re.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin" & user.Id == re.UserID)
            {
                var result = GetbyId(re.ExamID);
                if (result != null)
                {
                    result.Score = re.Score; 
                    result.UserID = re.UserID;
                    result.ExamID = re.ExamID;
                    resultRepositories.updateResult(result);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Result not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }*/
        public async Task<Response> delete(ResultDTO re)
        {
            var user = await _userManager.FindByNameAsync(re.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var result = GetbyId(re.ResultID);
                if (result != null)
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
    }
    #endregion

    #region "practice"
    public class PracticeService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IPracticeRepositories practiceRepositories;
        public PracticeService(DB dBContext, UserManager<IdentityUser> userManager)
        {
            this.dBContext = dBContext;
            this.practiceRepositories = new PracticeRepository(dBContext);
            this._userManager = userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(PracticeDTO pra)
        {
            var user = await _userManager.FindByNameAsync(pra.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Practice practice = practiceRepositories.getbyid(pra.PracticeID);
                if (practice == null)
                {
                    Practice practice1 = new Practice()
                    {
                        PracticeID = pra.PracticeID,
                        PracticeDescription = pra.PracticeDescription,
                        ExamID = pra.PracticeID,
                    };
                    practiceRepositories.insertPractice(practice1);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Practice already exist" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(PracticeDTO pra)
        {
            var user = await _userManager.FindByNameAsync(pra.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Practice practice = GetbyId(pra.ExamID);
                if (practice != null)
                {
                    practice.PracticeDescription = pra.PracticeDescription;
                    practice.ExamID = pra.ExamID;
                    practiceRepositories.updatePractice(practice);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Practice not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(PracticeDTO pra)
        {
            var user = await _userManager.FindByNameAsync(pra.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var practice = GetbyId(pra.PracticeID);
                if (practice != null)
                {
                    practiceRepositories.deletePractice(practice);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Practice not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public Practice GetbyId(int id)
        {
            return practiceRepositories.getbyid(id);
        }
        public List<PracticeDTO> List(int id)
        {
            List<PracticeDTO> practiceDTOs = new List<PracticeDTO>();
            var practices = practiceRepositories.Listall(id);
            foreach (var practice in practices)
            {
                practiceDTOs.Add(new PracticeDTO { PracticeID = practice.PracticeID,PracticeDescription = practice.PracticeDescription, ExamID = practice.ExamID, TestCount = practiceRepositories.Count(practice.PracticeID) });
            }
            return practiceDTOs;
        }
    }
    #endregion

    #region "practiceComplete"
    public class PracticeComplete
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IPractceCompleteRepositories pracomRepositories;

        public PracticeComplete(DB dBContext, UserManager<IdentityUser> userManager)
        {
            this.dBContext = dBContext;
            this.pracomRepositories = new PractceCompleteRepository(dBContext);
            this._userManager = userManager;
        }
    }
    #endregion
}
