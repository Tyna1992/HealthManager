using System.Text;
using HealthManagerServer.Data;
using HealthManagerServer.Service;
using HealthManagerServer.Service.Authentication;
using HealthManagerServer.Service.ExternalApis;
using HealthManagerServer.Service.JsonProcess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

AddServices();
AddDbContext();
AddIdentity();
ConfigureSwagger();
AddAuthentication();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var authenticationSeeder = scope.ServiceProvider.GetRequiredService<AuthSeeder>();
authenticationSeeder.AddRoles();
authenticationSeeder.AddAdmin();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void AddServices()
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddScoped<INutritionRepository, NutritionRepository>();
    builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
    builder.Services.AddScoped<ICocktailRepository, CocktailRepository>();
    builder.Services.AddSingleton<NutritionApiCall>();
    builder.Services.AddSingleton<ActivitiesApiCall>();
    builder.Services.AddSingleton<CocktailApiCall>();
    builder.Services.AddSingleton<JsonProcessor>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<AuthSeeder>();
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
}

void AddDbContext()
{
    builder.Services.AddDbContext<DataBaseContext>();
    builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer("Server=tcp:mysqlserverhun.database.windows.net,1433;Initial Catalog=Base;Persist Security Info=False;User ID=azureuser;Password=121212EmQ1994!%;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
}

void AddIdentity()
{
    builder.Services
        .AddIdentityCore<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<UserContext>();
}

void ConfigureSwagger()
{
    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });
}

void AddAuthentication()
{
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddCookie(options => {
            options.Cookie.Name = "Authorization";
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var issuerSigningKey = builder.Configuration.GetSection("IssuerSigningKey").Value;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.ValidIssuer,
                ValidAudience = jwtSettings.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey)),
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Cookies.ContainsKey("Authorization"))
                    {
                        context.Token = context.Request.Cookies["Authorization"];
                    }
                    return Task.CompletedTask;
                }
            };
        });
}