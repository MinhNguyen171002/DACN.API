using API.Data;
using API.DBContext;
using API.Enity;

namespace API.Repositories
{
    public interface IQuestionRepositories : IRepository<Question>
    {
        public void insertQuestion(Question question);
        public void deletetQuestion(Question question);
        public void updateQuestion(Question question);
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
    }
}
