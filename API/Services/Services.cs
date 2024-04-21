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
                    if(exam == null)
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
                    return new Response { Status = true, Message = "Success"};
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
/*        public List<Exam> List(string id)
        {
            return examRepositories.Listall(id);
        }*/
    }
    #endregion
    #region "test"
    public class TestService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private ITestRepositories testRepositories;
        public TestService(DB dBContext, UserManager<IdentityUser> _userManager)
        {
            this.dBContext = dBContext;
            this.testRepositories = new TestRepository(dBContext);
            this._userManager = _userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(TestDTO tes)
        {
            var user = await _userManager.FindByNameAsync(tes.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Test test = testRepositories.getbyid(tes.TestID);
                if(test == null)
                {
                    Test test1 = new Test()
                    {
                        TestID = tes.TestID,
                        Question = tes.Question,
                        Answer1 = tes.Answer1,
                        Answer2 = tes.Answer2,
                        Answer3 = tes.Answer3,
                        Answer4 = tes.Answer4,
                        CorrectAnswer = tes.CorrectAnswer,
                        PracticeID = tes.PracticeID,
                    };                    
                    testRepositories.insertTest(test1);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Test already exist" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(TestDTO tes)
        {
            var user = await _userManager.FindByNameAsync(tes.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Test test = GetbyId(tes.TestID);
                if (test != null)
                {
                    test.Answer1 = tes.Answer1;
                    test.Answer1 = tes.Answer2;
                    test.Answer3 = tes.Answer3;
                    test.Answer4 = tes.Answer4;
                    test.Question = tes.Question;
                    test.CorrectAnswer = tes.CorrectAnswer;
                    test.PracticeID = tes.PracticeID;
                    testRepositories.updateTest(test);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Test not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(TestDTO tes)
        {
            var user = await _userManager.FindByNameAsync(tes.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Test test = GetbyId(tes.TestID);
                if (test != null)
                {
                    testRepositories.deleteTest(test);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Test not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public Test GetbyId(int id)
        {
            return testRepositories.getbyid(id);
        }
        public List<Test> List(int id)
        {
            return testRepositories.Listall(id);
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
                if(result == null)
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
}
