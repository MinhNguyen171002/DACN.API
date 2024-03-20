using API.DBContext;
using API.Enity;
using API.Repositories;

namespace API.Services
{
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
}
