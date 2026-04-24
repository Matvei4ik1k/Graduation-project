using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Graduation_project.Model;

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

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBook> UserBooks { get; set; }

    public virtual DbSet<UserCourse> UserCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Graduation_project;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CI_AS");

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC078AD4791F");

            entity.ToTable("Book");

            entity.Property(e => e.Name).HasMaxLength(80);
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Chapter__3214EC074FF02AE3");

            entity.ToTable("Chapter");

            entity.Property(e => e.ChapterTitle).HasMaxLength(80);

            entity.HasOne(d => d.Book).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Chapter__BookId__60A75C0F");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Course__3214EC079C1214A7");

            entity.ToTable("Course");

            entity.Property(e => e.DifficultyLevel).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(80);
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Lesson__3214EC076B824B4B");

            entity.ToTable("Lesson");

            entity.Property(e => e.LessonTopic).HasMaxLength(50);

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Lesson__CourseId__70DDC3D8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC074222E1A2");

            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(60);
        });

        modelBuilder.Entity<UserBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserBook__3214EC0761CBDDE3");

            entity.ToTable("UserBook");

            entity.Property(e => e.IndexChapter).HasDefaultValue(0);
            entity.Property(e => e.Percent).HasDefaultValue(0);

            entity.HasOne(d => d.Book).WithMany(p => p.UserBooks)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__UserBook__BookId__6477ECF3");

            entity.HasOne(d => d.User).WithMany(p => p.UserBooks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserBook__UserId__6383C8BA");
        });

        modelBuilder.Entity<UserCourse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserCour__3214EC0759AD7C95");

            entity.ToTable("UserCourse");

            entity.Property(e => e.IndexLesson).HasDefaultValue(0);
            entity.Property(e => e.Percent).HasDefaultValue(0);

            entity.HasOne(d => d.Course).WithMany(p => p.UserCourses)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__UserCours__Cours__6C190EBB");

            entity.HasOne(d => d.User).WithMany(p => p.UserCourses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserCours__UserI__6B24EA82");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
