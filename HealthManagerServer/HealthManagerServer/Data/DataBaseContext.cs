using HealthManagerServer.Model;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

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
        // meal plan unnique index, user id is foreign key to user, meal id is foreign key to meal
        modelBuilder.Entity<MealPlan>()
        .HasOne(mp => mp.User)
        .WithMany() // Assuming a User can have multiple MealPlans
        .HasForeignKey(mp => mp.UserId);

    modelBuilder.Entity<MealPlan>()
        .HasOne(mp => mp.Meal)
        .WithMany() // Assuming a Meal can be in multiple MealPlans
        .HasForeignKey(mp => mp.MealId);

    }
}