using HealthManagerServer.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthManagerServer.Data;

public class DataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Nutrition> Nutritions { get; set; }
    public DbSet<Activity> Activities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:mysqlserverhun.database.windows.net,1433;Initial Catalog=Base;Persist Security Info=False;User ID=azureuser;Password=121212EmQ1994!%;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Activity>().HasIndex(a => a.Name).IsUnique();
        modelBuilder.Entity<Nutrition>().HasIndex(n => new{n.Name, n.Serving_size_g}).IsUnique();
    }
}