using HealthManagerServer.Model;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace HealthManagerServer.Data;

public class DataBaseContext : DbContext
{
    public DbSet<Nutrition> Nutritions { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Cocktail> Cocktails { get; set; }

    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
       
    }


    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     var connectionString = DotNetEnv.Env.GetString("CONNECTION_STRING");
    //     optionsBuilder.UseSqlServer(connectionString);
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>().HasIndex(a => new { a.Name, a.Duration_minutes, a.Weight }).IsUnique();
        modelBuilder.Entity<Nutrition>().HasIndex(n => new { n.Name, n.Serving_size_g }).IsUnique();
        modelBuilder.Entity<Cocktail>().HasIndex(c => c.Name).IsUnique();
    }
}