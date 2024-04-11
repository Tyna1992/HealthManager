using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;
using HealthManagerServer.Model;

namespace HealthManagerTest;



[Collection("Integration")]
public class MealPlanControllerTests
{
    [Fact]
    public async Task CreateMealPlan_UserAuthentication()
    {
        var app = new HealthManagerWebApplicationFactory();
        var client = app.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestUserScheme");
        var mealPlanRequest = new MealPlanRequest(
        
            UserName : "TestUser",
            ServingSize : 100,
            Date : DateOnly.FromDateTime(DateTime.Now),
            MealTime : "Breakfast",
            Name : "cereal"
        );
        
        var response = await client.PostAsJsonAsync("/api/mealPlan/create", mealPlanRequest);
        response.EnsureSuccessStatusCode();
        
        var responseString = await response.Content.ReadAsStringAsync();
        var mealPlan = await response.Content.ReadFromJsonAsync<MealPlan>();
        
        Assert.NotNull(mealPlan);
        Assert.Equal(mealPlanRequest.Date.ToDateTime(TimeOnly.MinValue), mealPlan.Date);
        Assert.Equal(mealPlanRequest.MealTime, mealPlan.MealTime);
        Assert.Equal(mealPlanRequest.Name, mealPlan.Meal.Name);
        Assert.Equal(mealPlanRequest.ServingSize, mealPlan.Meal.Serving_size_g);
        
    }
    
    
}