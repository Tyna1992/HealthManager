using HealthManagerServer.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace HealthManagerTest;

internal class HealthManagerWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<DataBaseContext>));
            services.RemoveAll(typeof(DbContextOptions<UserContext>));

            var connectionString =
                "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=HealthManagerTest;Data Source=TIMI\\SQLEXPRESS;TrustServerCertificate=true;";
            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(connectionString));
            
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(connectionString));
            
            services.AddAuthentication("TestUserScheme")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestUserScheme", options => { });
            
            services.AddAuthentication("TestAdminScheme")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestAdminScheme", options => { });

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var dataBaseContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
                var userContext = scope.ServiceProvider.GetRequiredService<UserContext>();
                
                dataBaseContext.Database.EnsureDeleted();
                userContext.Database.EnsureDeleted();
                
                dataBaseContext.Database.EnsureCreated();
                userContext.Database.Migrate();
                userContext.Database.EnsureCreated();
            }
            
        });
    }
    
    private static string? GetConnectionString()
    {
        DotNetEnv.Env.Load();
        var connectionString = DotNetEnv.Env.GetString("CONNECTION_STRING_TEST");
        return connectionString;
    }
}