using HealthManagerServer.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthManagerServer.Data;

public class DataBaseContext : DbContext
{
    public DbSet<Nutrition> Nutritions { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Cocktail> Cocktails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:mysqlserverhun.database.windows.net,1433;Initial Catalog=Base;Persist Security Info=False;User ID=azureuser;Password=121212EmQ1994!%;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>().HasIndex(a => new {a.Name, a.Duration_minutes, a.Weight}).IsUnique();
        modelBuilder.Entity<Nutrition>().HasIndex(n => new{n.Name, n.Serving_size_g}).IsUnique();
        modelBuilder.Entity<Cocktail>().HasIndex(c => c.Name).IsUnique();
    }
}