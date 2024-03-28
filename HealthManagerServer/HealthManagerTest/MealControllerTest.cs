using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace HealthManagerTest;

public class MealControllerTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public MealControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GetMealData_NoAuthentication_Test()
    {
        var app = new HealthManagerWebApplicationFactory();
        var client = app.CreateClient();
        var servingSize = 200;
        var mealName = "popcorn and butter";

        var response = await client.GetAsync($"/api/meal/{servingSize}/{mealName}");
        _testOutputHelper.WriteLine(response.ToString());
        
        response.EnsureSuccessStatusCode();
        var mealData = await response.Content.ReadFromJsonAsync<List<Nutrition>>();
        
        Assert.NotNull(mealData);
        Assert.NotEmpty(mealData);
        Assert.Equal(2, mealData.Count);
        Assert.Equal("popcorn", mealData[0].Name);
        
        
    }

    [Fact]
    public async Task GetAllMealData_AdminAuthorized_Test()
    {
        var app = new HealthManagerWebApplicationFactory();
        var client = app.CreateClient();
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestAdminScheme");
        
        var response = await client.GetAsync($"/api/meal/getAll");
        
        response.EnsureSuccessStatusCode();
        var allData = await response.Content.ReadFromJsonAsync<List<Nutrition>>();
        
        Assert.NotNull(allData);
        Assert.Empty(allData);
    }
}