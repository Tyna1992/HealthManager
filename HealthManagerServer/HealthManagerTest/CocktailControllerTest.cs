using System.Net.Http.Json;
using HealthManagerServer.Model;
using Xunit.Abstractions;

namespace HealthManagerTest;

[Collection("Integration")]
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
        var cocktailData = await response.Content.ReadFromJsonAsync<List<Cocktail>>();

        Assert.NotNull(cocktailData);
        Assert.Equal(6, cocktailData.Count);
        Assert.Equal("margarita", cocktailData[0].Name);
    }
}