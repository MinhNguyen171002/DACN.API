using API.Enity;
using Microsoft.EntityFrameworkCore;

namespace API.DBContext
{
    public class DB: DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Result> results { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Level> levels { get; set; }
        public DbSet<Exam> exams { get; set; }
        public DB(DbContextOptions<DB> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                
            }
        }
    }
}
