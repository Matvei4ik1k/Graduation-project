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

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserContent> UserContents { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Graduation_project;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CI_AS");

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BooksId).HasName("PK__Books__959FD33CE25724A4");

            entity.Property(e => e.BooksId).HasColumnName("Books_Id");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__6DB281364F8F1F52");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__Content__4F5DE4DDFD65B325");

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
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__37E005DB92C47995");

            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.DifficultyLevel).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonsId).HasName("PK__Lessons__5D768A2D7CF64644");

            entity.Property(e => e.LessonsId).HasColumnName("Lessons_Id");
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.LessonTopic).HasMaxLength(50);

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Lessons__Course___6D0D32F4");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__D80BB09351A15379");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__206A9DF859AACAB8");

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
            entity.HasKey(e => e.UserContentId).HasName("PK__User_con__D698C354CAD9F4BF");

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
            entity.HasKey(e => e.UserRoleId).HasName("PK__User_rol__41F7C3A58899A1A5");

            entity.ToTable("User_role");

            entity.Property(e => e.UserRoleId).HasColumnName("User_role_id");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.UserId).HasColumnName("User_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
