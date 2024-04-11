using HealthManagerServer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthManagerServer.Data
{
    public class UserContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}