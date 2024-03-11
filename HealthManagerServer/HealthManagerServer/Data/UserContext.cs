using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HealthManagerServer.Model;

namespace HealthManagerServer.Data
{
    public class UserContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:mysqlserverhun.database.windows.net,1433;Initial Catalog=Base;Persist Security Info=False;User ID=azureuser;Password=121212EmQ1994!%;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}