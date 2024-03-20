using API.Data;
using API.DBContext;
using API.Enity;

namespace API.Repositories
{
    public interface ILevelRepositories : IRepository<Level>
    {
        public void insertLevel(Level level);
        public void deleteLevel(Level level);
        public void updateLevel(Level level);
        public Level getbyid(object id);
    }
    public class LevelRepository : RepositoryBase<Level>, ILevelRepositories
    {
        public LevelRepository(DB dbContext) : base(dbContext)
        {

        }
        public void insertLevel(Level level)
        {
            _dbContext.levels.Add(level);
        }
        public void deleteLevel(Level level)
        {
            _dbContext.levels.Remove(level);
        }
        public void updateLevel(Level level)
        {
            _dbContext.levels.Attach(level);
            _dbContext.Entry(level).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Level getbyid(object id)
        {
            return _dbContext.levels.Find(id);
        }
    }
}
