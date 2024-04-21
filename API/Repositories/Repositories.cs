using API.Data;
using API.DBContext;
using API.Enity;
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
    #region "test"
    public interface ITestRepositories : IRepository<Test>
    {
        public void insertTest(Test test);
        public void deleteTest(Test test);
        public void updateTest(Test test);
        public Test getbyid(object id);
        public List<Test> Listall(object id);
    }
    public class TestRepository : RepositoryBase<Test>, ITestRepositories
    {
        public TestRepository(DB dbContext) : base(dbContext)
        {
        }
        public void insertTest(Test test)
        {
            _dbContext.tests.Add(test);
        }
        public void deleteTest(Test test)
        {
            _dbContext.tests.Remove(test);
        }
        public void updateTest(Test test)
        {
            _dbContext.tests.Attach(test);
            _dbContext.Entry(test).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Test getbyid(object id)
        {
            return _dbContext.tests.Find(id);
        }
        public List<Test> Listall(object id)
        {
            return _dbContext.tests.Where(x => x.PracticeID == (int)id).ToList();
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
}
