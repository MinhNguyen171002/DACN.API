using API.Data;
using API.DBContext;
using API.Enity;

namespace API.Repositories
{
    #region "user"
    public interface IUserRepositories : IRepository<User>
    {
        public void insertUser(User user);
        public void updateUser(User user);
        public void deleteUser(User user);

    }
    public class UserRepository : RepositoryBase<User>, IUserRepositories
    {
        public UserRepository(DB dbContext) : base(dbContext)
        {

        }
        public void insertUser(User user)
        {
            _dbContext.users.Add(user);
        }
        public void deleteUser(User user)
        {
            _dbContext.users.Remove(user);
        }

        public void updateUser(User user)
        {
            _dbContext.users.Attach(user);
            _dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
    #endregion
    #region "exam"
    public interface IExamRepositories : IRepository<Exam>
    {
        public void insertExam(Exam exam);
        public void deleteExam(Exam exam);
        public void updateExam(Exam exam);
        public Exam getbyid(object id);
/*        public List<Exam> Listall(object id);*/
    }
    public class ExamRepository : RepositoryBase<Exam>, IExamRepositories
    {
        public ExamRepository(DB dbContext) : base(dbContext)
        {
        }
        public void insertExam(Exam exam)
        {
            _dbContext.exams.Add(exam);
        }
        public void deleteExam(Exam exam)
        {
            _dbContext.exams.Remove(exam);
        }
        public void updateExam(Exam exam)
        {
            _dbContext.exams.Attach(exam);
            _dbContext.Entry(exam).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Exam getbyid(object id)
        {
            return _dbContext.exams.Find(id);
        }
/*        public List<Exam> Listall(object id)
        {
            return _dbContext.exams.Where(x=>x. == (string)id).ToList();
        }*/
    }
    #endregion
    #region "question"
    public interface IQuestionRepositories : IRepository<Question>
    {
        public void insertQuestion(Question question);
        public void deletetQuestion(Question question);
        public void updateQuestion(Question question);
        public Question getbyid(object id);
        public List<Question> Listall(object id);
    }
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepositories
    {
        public QuestionRepository(DB dbContext) : base(dbContext)
        {
        }
        public void insertQuestion(Question question)
        {
            _dbContext.questions.Add(question);
        }
        public void deletetQuestion(Question question)
        {
            _dbContext.questions.Remove(question);
        }
        public void updateQuestion(Question question)
        {
            _dbContext.questions.Attach(question);
            _dbContext.Entry(question).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Question getbyid(object id)
        {
            return _dbContext.questions.Find(id);
        }
        public List<Question> Listall(object id)
        {
            return _dbContext.questions.Where(x => x.ExamID == (int)id).ToList();
        }
    }
    #endregion
    #region "result"
    public interface IResultRepositories : IRepository<Result>
    {
        public void insertResult(Result result);
        public void deleteResult(Result result);
        public void updateResult(Result result);
        public Result getbyid(object id);
        public List<Result> Listall(object idExam, object idUser);
    }
    public class ResultRepository : RepositoryBase<Result>, IResultRepositories
    {
        public ResultRepository(DB dbContext) : base(dbContext)
        {
        }
        public void insertResult(Result result)
        {
            _dbContext.results.Add(result);
        }
        public void deleteResult(Result result)
        {
            _dbContext.results.Remove(result);
        }
        public void updateResult(Result result)
        {
            _dbContext.results.Attach(result);
            _dbContext.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Result getbyid(object id)
        {
            return _dbContext.results.Find(id);
        }
        public List<Result> Listall(object idExam,object idUser)
        {
            if(idExam == null && idUser == null)
            {
                
            }
            return _dbContext.results.Where(x => x.ExamID == (int)idExam && x.UserID == (string)idUser).ToList();
        }
    }
    #endregion
}
