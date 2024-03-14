using API.Data;
using API.DBContext;
using API.Enity;
using System.Linq.Expressions;

namespace API.Repositories
{
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
}
    
