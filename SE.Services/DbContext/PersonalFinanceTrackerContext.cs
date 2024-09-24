using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SE.Models;

public partial class PersonalFinanceTrackerContext : DbContext
{
    public PersonalFinanceTrackerContext()
    {
    }

    public PersonalFinanceTrackerContext(DbContextOptions<PersonalFinanceTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserInformation> UserInformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-OAVP92F\\SQLEXPRESS;Initial Catalog=\"Personal Finance Tracker\";User ID=sa;Password=abc123;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.RoleName).IsUnicode(false);
        });

        modelBuilder.Entity<UserInformation>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserInformation");

            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.FailedLoginAttempts).HasDefaultValue(0);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IsEmailVerified).HasColumnName("isEmailVerified");
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture).IsUnicode(false);
            entity.Property(e => e.UserName).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
