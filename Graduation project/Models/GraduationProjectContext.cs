using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Graduation_project.Models;

public partial class GraduationProjectContext : DbContext
{
    public GraduationProjectContext()
    {
    }

    public GraduationProjectContext(DbContextOptions<GraduationProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserContent> UserContents { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Graduation_project;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CI_AS");

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BooksId).HasName("PK__Books__959FD33CE3C7039C");

            entity.Property(e => e.BooksId).HasColumnName("Books_Id");
            entity.Property(e => e.Name).HasMaxLength(80);
            entity.Property(e => e.Progress).HasDefaultValue(0);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__6DB28136B98A6BEF");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChaptersId).HasName("PK__Chapters__9C7896904CF6F39C");

            entity.Property(e => e.ChaptersId).HasColumnName("Chapters_Id");
            entity.Property(e => e.BooksId).HasColumnName("Books_Id");
            entity.Property(e => e.ChapterTitle).HasMaxLength(80);

            entity.HasOne(d => d.Books).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.BooksId)
                .HasConstraintName("FK__Chapters__Books___71D1E811");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__Content__4F5DE4DD2C605750");

            entity.ToTable("Content");

            entity.Property(e => e.ContentId).HasColumnName("Content_id");
            entity.Property(e => e.Author).HasMaxLength(200);
            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.ContentUrl)
                .HasMaxLength(500)
                .HasColumnName("Content_URL");
            entity.Property(e => e.Difficulty).HasMaxLength(50);
            entity.Property(e => e.Tag).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__37E005DB0E7EAD1B");

            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.DifficultyLevel).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(80);
            entity.Property(e => e.Progress).HasDefaultValue(0);
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonsId).HasName("PK__Lessons__5D768A2DFF0DE3DD");

            entity.Property(e => e.LessonsId).HasColumnName("Lessons_Id");
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.LessonTopic).HasMaxLength(50);

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Lessons__Course___6EF57B66");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__D80BB0931C853AB1");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__206A9DF8AB28BB76");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(60);
            entity.Property(e => e.RegistrationDate).HasColumnName("Registration_date");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
        });

        modelBuilder.Entity<UserContent>(entity =>
        {
            entity.HasKey(e => e.UserContentId).HasName("PK__User_con__D698C354E4DA26CC");

            entity.ToTable("User_content");

            entity.Property(e => e.UserContentId).HasColumnName("User_content_id");
            entity.Property(e => e.CompletionDate).HasColumnName("Completion_date");
            entity.Property(e => e.ContentId).HasColumnName("Content_id");
            entity.Property(e => e.InteractionType)
                .HasMaxLength(50)
                .HasColumnName("Interaction_type");
            entity.Property(e => e.ProgressPercent)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Progress_percent");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");
            entity.Property(e => e.UserId).HasColumnName("User_id");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__User_rol__41F7C3A58FAA09B9");

            entity.ToTable("User_role");

            entity.Property(e => e.UserRoleId).HasColumnName("User_role_id");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.UserId).HasColumnName("User_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
