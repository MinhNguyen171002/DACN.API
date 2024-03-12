using API.Data;
using API.DBContext;
using API.Enity;
using System.Linq.Expressions;

namespace API.Repositories
{
    public interface IURepositories : IRepository<User>
    {
        public void insertUser(User user);
    }
    public class URepository : RepositoryBase<User>, IURepositories
    {
        public URepository(DB dbContext) : base(dbContext)
        {
        }
        public void insertUser(User user)
        {
            _dbContext.users.Add(user);
        }
        public void insertLevel(Level level)
        {
            _dbContext.levels.Add(level);
        }
        public void insertExam(Exam exam)
        {
            _dbContext.exams.Add(exam);
        }
        public void insertQuestion(Question question)
        {
            _dbContext.questions.Add(question);
        }
        public void insertResult(Result result)
        {
            _dbContext.results.Add(result);
        }
        public void DeleteUser(User user)
        {
            _dbContext.users.Remove(user);
        }
        public void DeleteLevel(Level level)
        {
            _dbContext.levels.Remove(level);
        }
        public void DeleteExam(Exam exam)
        {
            _dbContext.exams.Remove(exam);
        }
        public void DeletetQuestion(Question question)
        {
            _dbContext.questions.Remove(question);
        }
        public void DeleteResult(Result result)
        {
            _dbContext.results.Remove(result);
        }

    }
}
    
