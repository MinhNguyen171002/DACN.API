﻿using API.Enity;
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
        public DbSet<Result> results { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Exam> exams { get; set; }
        public DbSet<Practice> practices { get; set; }
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
            .HasMany(c => c.practices)
            .WithOne(e => e.exam)
            .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Practice>()
            .HasMany(c => c.sentences)
            .WithOne(e => e.practice)
            .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Sentence>()
                .HasMany(c => c.questions)
                .WithOne(c => c.sentences)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<SentenceComplete>()
                .HasMany(c => c.QuestionCompletes)
                .WithOne(c => c.SenCom)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Question>()
                .HasOne(c => c.sentences)
                .WithMany(c => c.questions)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Result>()
                .HasOne(c => c.sentenceCom)
                .WithOne(c => c.result).HasForeignKey<Result>(c => c.Sentence);

            builder.Entity<Result>()
                .HasOne(c => c.user)
                .WithOne(c => c.result).HasForeignKey<Result>(c => c.User);

            builder.Entity<QuestionComplete>()
                .HasOne(c => c.test)
                .WithOne(c => c.quescom).HasForeignKey<QuestionComplete>(c => c.QuestionID);

            builder.Entity<SentenceComplete>()
                .HasOne(c => c.Sentence)
                .WithOne(c => c.sencom).HasForeignKey<SentenceComplete>(c => c.SentenceID);

            builder.Entity<SentenceComplete>()
                .HasOne(c => c.user)
                .WithOne(c => c.sencom).HasForeignKey<SentenceComplete>(c => c.User);

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
