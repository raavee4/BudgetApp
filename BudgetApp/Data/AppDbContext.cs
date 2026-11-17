using BudgetApp.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Statement> Statements => Set<Statement>();
    public DbSet<FinancialTransaction> FinancialTransactions => Set<FinancialTransaction>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<RecurringExpense> RecurringExpenses => Set<RecurringExpense>();
    public DbSet<SavingGoal> SavingGoals => Set<SavingGoal>();
    public DbSet<BudgetAlerts> BudgetAlerts => Set<BudgetAlerts>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserProfile>(entity =>
        {
            entity.Property(x => x.UserId)
            .IsRequired();
            entity.HasOne(up => up.User)
                .WithOne()
                .HasForeignKey<UserProfile>(up => up.UserId);

            entity.HasIndex(up => up.UserId)
                .IsUnique();
        });
        builder.Entity<Statement>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId);

        builder.Entity<FinancialTransaction>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);

        builder.Entity<FinancialTransaction>()
            .HasOne(t => t.Statement)
            .WithMany(s => s.FinancialTransactions)
            .HasForeignKey(t => t.StatementId);

        builder.Entity<FinancialTransaction>()
            .HasOne(t => t.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SavingGoal>()
            .HasOne(sg => sg.User)
            .WithMany()
            .HasForeignKey(sg => sg.UserId);

        builder.Entity<RecurringExpense>()
            .HasOne(re => re.ApplicationUser)
            .WithMany()
            .HasForeignKey(re => re.UserId);

        builder.Entity<RecurringExpense>()
            .HasOne(re => re.Category)
            .WithMany()
            .HasForeignKey(re => re.CategoryId);

        builder.Entity<BudgetAlerts>()
            .HasOne(ba => ba.ApplicationUser)
            .WithMany()
            .HasForeignKey(ba => ba.UserId);

    }
}
