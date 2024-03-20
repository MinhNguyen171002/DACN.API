using API.DBContext;
using API.Enity;
using API.Repositories;

namespace API.Services
{
    public class LevelService
    {
        private DB dBContext;
        private ILevelRepositories levelRepositories;
        public LevelService(DB dBContext)
        {
            this.dBContext = dBContext;
            this.levelRepositories = new LevelRepository(dBContext);
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public void insert(Level level)
        {
            levelRepositories.insertLevel(level);
            Save();
        }
        public void update(Level level)
        {
            levelRepositories.updateLevel(level);
            Save();
        }
        public void delete(Level level)
        {
            levelRepositories.deleteLevel(level);
            Save();
        }
        public Level getbyid(string id)
        {
            return levelRepositories.getbyid(id);
        }
    }
}
