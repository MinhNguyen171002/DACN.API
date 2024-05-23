using API.Enity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.DBContext
{
    public class DB: IdentityDbContext<IdentityUser>
    {
        public DbSet<User> users { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<QuestionFile> files { get; set; }
        public DbSet<Exam> exams { get; set; }
        public DbSet<SentenceComplete> sentenceCompletes { get; set; }
        public DbSet<QuestionComplete> questionCompletes { get; set; }
        public DbSet<Sentence> sentences { get; set; }
        public DB(DbContextOptions<DB> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ROLE_ID = "ad376a8f-9eab-4bb9-9fca-30b01540f445";

            builder.Entity<Exam>()
            .HasMany(c => c.sentences)
            .WithOne(e => e.exam)
            .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Sentence>()
                .HasMany(c => c.questions)
                .WithOne(c => c.sentence)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<SentenceComplete>()
                .HasMany(c => c.QuestionCompletes)
                .WithOne(c => c.sencom)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Question>()
                .HasOne(c => c.sentence)
                .WithMany(c => c.questions)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Question>()
                .HasMany(c => c.files)
                .WithOne(c => c.ques)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<SentenceComplete>()
                .HasOne(c => c.user)
                .WithMany(c => c.sencoms).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<QuestionComplete>()
                .HasOne(c => c.user)
                .WithMany(c => c.quescoms).OnDelete(DeleteBehavior.SetNull);


            builder.Entity<QuestionComplete>()
                .HasOne(c => c.test)
                .WithOne(c => c.quescom).HasForeignKey<QuestionComplete>(c => c.QuestionID);

            builder.Entity<SentenceComplete>()
                .HasOne(c => c.sentence)
                .WithOne(c => c.sencom).HasForeignKey<SentenceComplete>(c => c.SentenceID);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = "admin",
                NormalizedName = "admin"
            });

            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "Admin123#"),
                SecurityStamp = string.Empty
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

        }
    }
}
