using System.Net.Http.Headers;
using System.Net.Http.Json;
using HealthManagerServer.Model;
using Xunit.Abstractions;

namespace HealthManagerTest;

public class ActivitiesControllerTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public ActivitiesControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task GetActivityData_NoAuthentication_Test()
    {
        var app = new HealthManagerWebApplicationFactory();
        var client = app.CreateClient();
        var activityName = "running";
        var weight = 160;
        var duration = 60;
        
        var response = await client.GetAsync($"/api/activities/{activityName}/{weight}/{duration}");
        _testOutputHelper.WriteLine(response.ToString());
        
        response.EnsureSuccessStatusCode();
        var activityData = await response.Content.ReadFromJsonAsync<List<Activity>>();
        
        Assert.NotNull(activityData);
        Assert.Equal("Running, 5 mph (12 minute mile)", activityData[0].Name);
    }
    
}