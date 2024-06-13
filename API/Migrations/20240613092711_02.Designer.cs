﻿// <auto-generated />
using System;
using API.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(DB))]
    [Migration("20240613092711_02")]
    partial class _02
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("API.Enity.Exam", b =>
                {
                    b.Property<string>("ExamID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ExamDescription")
                        .HasColumnType("longtext");

                    b.Property<TimeSpan>("ExamDuration")
                        .HasColumnType("time(6)");

                    b.Property<int?>("ExamSerial")
                        .HasColumnType("int");

                    b.Property<int?>("Part")
                        .HasColumnType("int");

                    b.Property<string>("Skill")
                        .HasColumnType("longtext");

                    b.HasKey("ExamID");

                    b.ToTable("exams");
                });

            modelBuilder.Entity("API.Enity.Question", b =>
                {
                    b.Property<string>("QuestionID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Answer1")
                        .HasColumnType("longtext");

                    b.Property<string>("Answer2")
                        .HasColumnType("longtext");

                    b.Property<string>("Answer3")
                        .HasColumnType("longtext");

                    b.Property<string>("Answer4")
                        .HasColumnType("longtext");

                    b.Property<string>("CorrectAnswer")
                        .HasColumnType("longtext");

                    b.Property<string>("CorrectDescription")
                        .HasColumnType("longtext");

                    b.Property<string>("QuestionContext")
                        .HasColumnType("longtext");

                    b.Property<int?>("QuestionSerial")
                        .HasColumnType("int");

                    b.Property<string>("SentenceId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UrlAudio")
                        .HasColumnType("longtext");

                    b.Property<string>("UrlImage")
                        .HasColumnType("longtext");

                    b.HasKey("QuestionID");

                    b.HasIndex("SentenceId");

                    b.ToTable("questions");
                });

            modelBuilder.Entity("API.Enity.QuestionComplete", b =>
                {
                    b.Property<string>("QuestionID")
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("IsCorrect")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("QuestionChoose")
                        .HasColumnType("longtext");

                    b.Property<int?>("QuestionSerial")
                        .HasColumnType("int");

                    b.Property<string>("SentenceId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.HasKey("QuestionID");

                    b.HasIndex("SentenceId");

                    b.HasIndex("UserID");

                    b.ToTable("questionCompletes");
                });

            modelBuilder.Entity("API.Enity.Sentence", b =>
                {
                    b.Property<string>("SentenceId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("ExamID")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("SentenceSerial")
                        .HasColumnType("int");

                    b.HasKey("SentenceId");

                    b.HasIndex("ExamID");

                    b.ToTable("sentences");
                });

            modelBuilder.Entity("API.Enity.SentenceComplete", b =>
                {
                    b.Property<string>("SentenceID")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("CorrectQuestion")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<TimeSpan>("Totaltime")
                        .HasColumnType("time(6)");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.HasKey("SentenceID");

                    b.HasIndex("UserID");

                    b.ToTable("sentenceCompletes");
                });

            modelBuilder.Entity("API.Enity.Topic", b =>
                {
                    b.Property<string>("TopicId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TopicName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.HasKey("TopicId");

                    b.HasIndex("UserID");

                    b.ToTable("topic");
                });

            modelBuilder.Entity("API.Enity.User", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserID");

                    b.ToTable("users");
                });

            modelBuilder.Entity("API.Enity.Vocabulary", b =>
                {
                    b.Property<string>("VocabularyId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("TopicId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("vocabulary")
                        .HasColumnType("longtext");

                    b.HasKey("VocabularyId");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserID");

                    b.ToTable("vocabularies");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "ad376a8f-9eab-4bb9-9fca-30b01540f445",
                            Name = "admin",
                            NormalizedName = "admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "669408c1-293d-48c4-8410-8d54605d6f86",
                            Email = "admin@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "admin@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAIAAYagAAAAELD9OqfD5sa8wDBUrt9uR7yiPJ5qWkIVIMpnmI9hRjr4EDxKp2YmpQJ+tNXSqXiChA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                            RoleId = "ad376a8f-9eab-4bb9-9fca-30b01540f445"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("API.Enity.Question", b =>
                {
                    b.HasOne("API.Enity.Sentence", "sentence")
                        .WithMany("questions")
                        .HasForeignKey("SentenceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("sentence");
                });

            modelBuilder.Entity("API.Enity.QuestionComplete", b =>
                {
                    b.HasOne("API.Enity.Question", "test")
                        .WithOne("quescom")
                        .HasForeignKey("API.Enity.QuestionComplete", "QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Enity.Sentence", "sen")
                        .WithMany("questionCompletes")
                        .HasForeignKey("SentenceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("API.Enity.User", "user")
                        .WithMany("quescoms")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("sen");

                    b.Navigation("test");

                    b.Navigation("user");
                });

            modelBuilder.Entity("API.Enity.Sentence", b =>
                {
                    b.HasOne("API.Enity.Exam", "exam")
                        .WithMany("sentences")
                        .HasForeignKey("ExamID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("exam");
                });

            modelBuilder.Entity("API.Enity.SentenceComplete", b =>
                {
                    b.HasOne("API.Enity.Sentence", "sentence")
                        .WithOne("sencom")
                        .HasForeignKey("API.Enity.SentenceComplete", "SentenceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Enity.User", "user")
                        .WithMany("sencoms")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("sentence");

                    b.Navigation("user");
                });

            modelBuilder.Entity("API.Enity.Topic", b =>
                {
                    b.HasOne("API.Enity.User", "user")
                        .WithMany("topic")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("user");
                });

            modelBuilder.Entity("API.Enity.User", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "user")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("API.Enity.Vocabulary", b =>
                {
                    b.HasOne("API.Enity.Topic", "topic")
                        .WithMany("vocabularies")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("API.Enity.User", "user")
                        .WithMany("vocabularies")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("topic");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Enity.Exam", b =>
                {
                    b.Navigation("sentences");
                });

            modelBuilder.Entity("API.Enity.Question", b =>
                {
                    b.Navigation("quescom")
                        .IsRequired();
                });

            modelBuilder.Entity("API.Enity.Sentence", b =>
                {
                    b.Navigation("questionCompletes");

                    b.Navigation("questions");

                    b.Navigation("sencom");
                });

            modelBuilder.Entity("API.Enity.Topic", b =>
                {
                    b.Navigation("vocabularies");
                });

            modelBuilder.Entity("API.Enity.User", b =>
                {
                    b.Navigation("quescoms");

                    b.Navigation("sencoms");

                    b.Navigation("topic");

                    b.Navigation("vocabularies");
                });
#pragma warning restore 612, 618
        }
    }
}