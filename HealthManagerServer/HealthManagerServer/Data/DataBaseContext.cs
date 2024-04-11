using HealthManagerServer.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthManagerServer.Data;

public class DataBaseContext : DbContext
{
    public DbSet<Nutrition> Nutritions { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Cocktail> Cocktails { get; set; }
    public DbSet<MealPlan> MealPlans { get; set; }

    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>().HasIndex(a => new { a.Name, a.Duration_minutes, a.Weight }).IsUnique();
        modelBuilder.Entity<Nutrition>().HasIndex(n => new { n.Name, n.Serving_size_g }).IsUnique();
        modelBuilder.Entity<Cocktail>().HasIndex(c => c.Name).IsUnique();

        modelBuilder.Entity<MealPlan>()
        .HasOne(mp => mp.Meal)
        .WithMany() // Assuming a Meal can be in multiple MealPlans
        .HasForeignKey(mp => mp.MealId)
        .IsRequired();

        base.OnModelCreating(modelBuilder);

    }
}