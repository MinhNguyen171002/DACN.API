using API.DBContext;
using API.Enity;
using API.Repositories;

namespace API.Services
{
    public class UserService
    {
        private DB dBContext;
        private IUserRepositories userRepositories;
        public UserService(DB dBContext) {  
            
            this.dBContext = dBContext;
            this.userRepositories = new UserRepository(dBContext);
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public void insert(User user)
        {
            userRepositories.insertUser(user);
            Save();
        }
        public void update(User user)
        {
            userRepositories.updateUser(user);
            Save();
        }
        public void delete(User user)
        {
            userRepositories.deleteUser(user);
            Save();
        }
    }
}
