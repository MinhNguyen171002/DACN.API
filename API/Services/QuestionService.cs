using API.DBContext;
using API.Enity;
using API.Repositories;

namespace API.Services
{
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
    }
}
