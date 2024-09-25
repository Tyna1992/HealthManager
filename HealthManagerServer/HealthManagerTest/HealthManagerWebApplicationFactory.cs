using DotNetEnv;
using HealthManagerServer.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HealthManagerTest;

internal class HealthManagerWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();
        });
        
        builder.ConfigureServices((context,services) =>
        {
            services.RemoveAll(typeof(DbContextOptions<DataBaseContext>));
            services.RemoveAll(typeof(DbContextOptions<UserContext>));

            var configuration = context.Configuration;
            var connectionString = configuration.GetConnectionString("TestDatabase");
            Console.WriteLine($"Test Database Connection String: {connectionString}");
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
                
                if (!dataBaseContext.Database.CanConnect() || !userContext.Database.CanConnect())
                {
                    
                    dataBaseContext.Database.EnsureCreated();
                    userContext.Database.Migrate(); 
                    userContext.Database.EnsureCreated();
                }
                else
                {
                    dataBaseContext.Database.EnsureDeleted();
                    userContext.Database.EnsureDeleted();
        
                    // Recreate the database
                    dataBaseContext.Database.EnsureCreated();
                    userContext.Database.Migrate();
                    userContext.Database.EnsureCreated();
                }
            }
        });
    }
}