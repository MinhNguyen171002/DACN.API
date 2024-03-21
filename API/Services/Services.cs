using API.DBContext;
using API.Enity;
using API.Model;
using API.Repositories;
using Microsoft.AspNetCore.Identity;
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
    #region "level"
    public class LevelService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private DB dBContext;
        private ILevelRepositories levelRepositories;
        public LevelService(DB dBContext, UserManager<IdentityUser> _userManager)
        {
            this.dBContext = dBContext;
            this.levelRepositories = new LevelRepository(dBContext);
            this._userManager = _userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task insert(MLevel lev)
        {
            var user = await _userManager.FindByNameAsync(lev.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Level level = new Level()
                {
                    LevelID = lev.LevelID,
                    LevelName = lev.LevelName,
                };
                levelRepositories.insertLevel(level);
                Save();
            }            
        }
        public async Task update(MLevel lev)
        {
            var user = await _userManager.FindByNameAsync(lev.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var level = getbyid(lev.LevelID);
                if (level == null)
                {
                    levelRepositories.updateLevel(level);
                    Save();
                }
            }
        }
        public async Task delete(MLevel lev)
        {
            var user = await _userManager.FindByNameAsync(lev.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var level = getbyid(lev.LevelID);
                if (level == null)
                {
                    levelRepositories.deleteLevel(level);
                    Save();
                }
            }

        }
        public Level getbyid(string id)
        {
            return levelRepositories.getbyid(id);
        }
        public List<Level> list()
        {
            return levelRepositories.Listall();
        }
    }
    #endregion
    #region "exam"
    public class ExamService
    {
        private DB dBContext;
        private IExamRepositories examRepositories;
        public ExamService(DB dBContext)
        {
            this.dBContext = dBContext;
            this.examRepositories = new ExamRepository(dBContext);
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public void insert(Exam exam)
        {
            examRepositories.insertExam(exam);
            Save();
        }
        public void update(Exam exam)
        {
            examRepositories.updateExam(exam);
            Save();
        }
        public void delete(Exam exam)
        {
            examRepositories.deleteExam(exam);
            Save();
        }
        public Exam GetbyId(string id)
        {
            return examRepositories.getbyid(id);
        }
    }
    #endregion
    #region "question"
    public class QuestionService
    {
        private DB dBContext;
        private IQuestionRepositories questionRepositories;
        public QuestionService(DB dBContext)
        {
            this.dBContext = dBContext;
            this.questionRepositories = new QuestionRepository(dBContext);
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public void insert(Question question)
        {
            questionRepositories.insertQuestion(question);
            Save();
        }
        public void update(Question question)
        {
            questionRepositories.updateQuestion(question);
            Save();
        }
        public void delete(Question question)
        {
            questionRepositories.deletetQuestion(question);
            Save();
        }
        public Question GetbyId(string id)
        {
            return questionRepositories.getbyid(id);
        }
    }
    #endregion
    #region "result"
    public class ResultService
    {
        private DB dBContext;
        private IResultRepositories resultRepositories;
        public ResultService(DB dBContext)
        {
            this.dBContext = dBContext;
            this.resultRepositories = new ResultRepository(dBContext);
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public void insert(Result result)
        {
            resultRepositories.insertResult(result);
            Save();
        }
        public void update(Result result)
        {
            resultRepositories.updateResult(result);
            Save();
        }
        public void delete(Result result)
        {
            resultRepositories.deleteResult(result);
            Save();
        }
        public Result GetbyId(string id)
        {
            return resultRepositories.getbyid(id);
        }
    }
    #endregion
}
