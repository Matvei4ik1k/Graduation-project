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

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserContent> UserContents { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Graduation_project;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__6DB2813680B69392");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__Content__4F5DE4DDCF3306F6");

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

            entity.HasOne(d => d.Category).WithMany(p => p.Contents)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Content__Categor__68487DD7");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__D80BB093984A7DFB");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__206A9DF8721C6C26");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.RegistrationDate).HasColumnName("Registration_date");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__Role_id__6754599E");
        });

        modelBuilder.Entity<UserContent>(entity =>
        {
            entity.HasKey(e => e.UserContentId).HasName("PK__User_con__D698C354CFE424B7");

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

            entity.HasOne(d => d.Content).WithMany(p => p.UserContents)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_cont__Conte__6C190EBB");

            entity.HasOne(d => d.User).WithMany(p => p.UserContents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_cont__User___6B24EA82");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__User_rol__41F7C3A52F28D8D0");

            entity.ToTable("User_role");

            entity.Property(e => e.UserRoleId).HasColumnName("User_role_id");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_role__Role___6A30C649");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_role__User___693CA210");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
