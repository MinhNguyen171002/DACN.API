using API.DBContext;
using API.Enity;
using API.Repositories;

namespace API.Services
{
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
}
