using API.Enity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace API.DBContext
{
    public class DB: IdentityDbContext<IdentityUser>
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
            builder.Entity<IdentityUser>().ToTable("IdenityUser");
        }
    }
}
