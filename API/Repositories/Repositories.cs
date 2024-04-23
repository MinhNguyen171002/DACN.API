using API.Data;
using API.DBContext;
using API.Enity;
using API.Model;
using static System.Net.Mime.MediaTypeNames;

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
        public List<Exam> Listall(object id);
        public int Count(object id);
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
        public List<Exam> Listall(object skill)
        {
            return _dbContext.exams.Where(x => x.Skill == (string)skill).ToList();
        }
        public int Count(object id)
        {
            return _dbContext.practices.Where(x=>x.ExamID == (int)id).Count();
        }
    }
    #endregion

    #region "question"
    public interface IQuestionRepositories : IRepository<Question>
    {
        public void insertQuestion(Question test);
        public void deleteQuestion(Question test);
        public void updateQuestion(Question test);
        public Question getbyid(object id);
        public List<Question> Listall(object id);
    }
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepositories
    {
        public QuestionRepository(DB dbContext) : base(dbContext)
        {
        }
        public void insertQuestion(Question test)
        {
            _dbContext.questions.Add(test);
        }
        public void deleteQuestion(Question test)
        {
            _dbContext.questions.Remove(test);
        }
        public void updateQuestion(Question test)
        {
            _dbContext.questions.Attach(test);
            _dbContext.Entry(test).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Question getbyid(object id)
        {
            return _dbContext.questions.Find(id);
        }
        public List<Question> Listall(object id)
        {
            return _dbContext.questions.Where(x => x.SentenceID == (int)id).ToList();
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
    }
    #endregion

    #region "practice"

    public interface IPracticeRepositories : IRepository<Practice>
    {
        public void insertPractice(Practice practice);
        public void deletePractice(Practice practice);
        public void updatePractice(Practice practice);
        public Practice getbyid(object id);
        public List<Practice> Listall(object id);
        public int Count(object id);
    }
    public class PracticeRepository : RepositoryBase<Practice>, IPracticeRepositories
    {
        public PracticeRepository(DB dbContext) : base(dbContext)
        {

        }
        public void insertPractice(Practice practice)
        {
            _dbContext.practices.Add(practice);
        }
        public void deletePractice(Practice practice)
        {
            _dbContext.practices.Remove(practice);
        }
        public void updatePractice(Practice practice)
        {
            _dbContext.practices.Attach(practice);
            _dbContext.Entry(practice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Practice getbyid(object id)
        {
            return _dbContext.practices.Find(id);
        }
        public List<Practice> Listall(object id)
        {
            return _dbContext.practices.Where(x => x.ExamID.Equals((int)id)).ToList();
        }
        public int Count(object id)
        {
            return _dbContext.questions.Where(x => x.SentenceID.Equals((int)id)).Count();
        }
    }
    #endregion

    #region "practicecomplete"
    public interface IPractceCompleteRepositories : IRepository<PracticeComplete>
    {
        public void insertPraComplete(PracticeComplete pracom);
        public void deletePraComplete(PracticeComplete pracom);
        public void updatePraComplete(PracticeComplete pracom);
        public PracticeComplete getbyid(object id);
        public int Count(object id);
    }
    public class PractceCompleteRepository : RepositoryBase<PracticeComplete>, IPractceCompleteRepositories
    {
        public PractceCompleteRepository(DB dbContext) : base(dbContext)
        {

        }
        public void insertPraComplete(PracticeComplete pracom)
        {
            _dbContext.practiceCompletes.Add(pracom);
        }
        public void deletePraComplete(PracticeComplete pracom)
        {
            _dbContext.practiceCompletes.Remove(pracom);
        }
        public void updatePraComplete(PracticeComplete pracom)
        {
            _dbContext.practiceCompletes.Attach(pracom);
            _dbContext.Entry(pracom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public PracticeComplete getbyid(object id)
        {
            return _dbContext.practiceCompletes.Find(id);
        }
        public List<PracticeComplete> Listall(object id)
        {
            return _dbContext.practiceCompletes.Where(x => x.PracticeID.Equals((int)id)).ToList();
        }
        public int Count(object id)
        {
            return _dbContext.questionCompletes.Where(x => x.QuestionID.Equals((int)id) & x.IsCorrect.Equals(true)).Count();
        }
    }
    #endregion
}
