using API.Data;
using API.DBContext;
using API.Enity;

namespace API.Repositories
{
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
}
