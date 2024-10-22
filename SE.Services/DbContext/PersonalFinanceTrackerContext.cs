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

    public virtual DbSet<CategoriesType> CategoriesTypes { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<RepeatedTransaction> RepeatedTransactions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<UserInformation> UserInformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-OAVP92F\\SQLEXPRESS;Initial Catalog=\"Personal Finance Tracker\";User ID=sa;Password=abc123;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriesType>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B71D825C4");

            entity.ToTable("CategoriesType");

            entity.Property(e => e.CategoryName).IsUnicode(false);
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.GoalId).HasName("PK__Goals__8A4FFFD1EAE11BBA");

            entity.Property(e => e.CreatedDate).IsUnicode(false);
            entity.Property(e => e.Deadlline).IsUnicode(false);
            entity.Property(e => e.DeleteTransaction).HasDefaultValue(false);
            entity.Property(e => e.DeletedDate).IsUnicode(false);
            entity.Property(e => e.GoalName).IsUnicode(false);
            entity.Property(e => e.UpdatedDate).IsUnicode(false);
        });

        modelBuilder.Entity<RepeatedTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Repeated__3214EC0727D79F5B");

            entity.ToTable("RepeatedTransaction");

            entity.Property(e => e.DeletedDate).IsUnicode(false);
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Frequency).IsUnicode(false);
            entity.Property(e => e.TransactionDate).IsUnicode(false);
            entity.Property(e => e.UpdatedDate).IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.RoleName).IsUnicode(false);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6BC80777C6");

            entity.Property(e => e.DeletedDate).IsUnicode(false);
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.TransactionDate).IsUnicode(false);
            entity.Property(e => e.UpdatedDate).IsUnicode(false);
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("PK__Transact__20266D0BF48C56AB");

            entity.ToTable("TransactionType");

            entity.Property(e => e.TransactionName).IsUnicode(false);
        });

        modelBuilder.Entity<UserInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserInfo__3214EC079919F254");

            entity.ToTable("UserInformation");

            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.FailedLoginAttempts).HasDefaultValue(0);
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
