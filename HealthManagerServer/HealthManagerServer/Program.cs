using HealthManagerServer.Data;
using HealthManagerServer.Service;
using HealthManagerServer.Service.ExternalApis;
using HealthManagerServer.Service.JsonProcess;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DataBaseContext>();
builder.Services.AddSingleton<IUserRepository,UserRepository>();
builder.Services.AddSingleton<INutritionRepository,NutritionRepository>();
builder.Services.AddSingleton<IActivityRepository,ActivityRepository>();
builder.Services.AddSingleton<ICocktailRepository,CocktailRepository>();
builder.Services.AddSingleton<NutritionApiCall>();
builder.Services.AddSingleton<ActivitiesApiCall>();
builder.Services.AddSingleton<CocktailApiCall>();
builder.Services.AddSingleton<JsonProcessor>();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();