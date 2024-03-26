using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace HealthManagerTest;

public class CocktailControllerTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public CocktailControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GetCocktailData_NoAuthentication_Test()
    {
        var app = new HealthManagerWebApplicationFactory();
        var client = app.CreateClient();
        var cocktailName = "margarita";

        var response = await client.GetAsync($"/api/cocktail/{cocktailName}");
        _testOutputHelper.WriteLine(response.ToString());

        response.EnsureSuccessStatusCode();
        var cocktailData = await response.Content.ReadFromJsonAsync<Cocktail>();

        Assert.NotNull(cocktailData);
        Assert.Equal("margarita", cocktailData.Name);
    }
}