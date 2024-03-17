using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace HealthManagerServer.Data
{
    public class UserContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            DotNetEnv.Env.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = DotNetEnv.Env.GetString("CONNECTION_STRING");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}