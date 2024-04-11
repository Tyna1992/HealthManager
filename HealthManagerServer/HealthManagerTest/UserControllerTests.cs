using System.Net.Http.Headers;
using System.Net.Http.Json;
using HealthManagerServer.Service;

namespace HealthManagerTest;


[Collection("Integration")]
public class UserControllerTests
{
    [Fact]
    public async Task GetAllUsers_AdminAuthorized()
    {
        var app = new HealthManagerWebApplicationFactory();
        var client = app.CreateClient();
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestAdminScheme");
        var response = await client.GetAsync($"/api/user/getAll");
        response.EnsureSuccessStatusCode();
        
        var users = await response.Content.ReadFromJsonAsync<List<UserResponse>>();
        
        Assert.NotNull(users);
        Assert.Single(users);
    }
    
    [Fact]
    public async Task GetUserByEmail_AdminAuthorized()
    {
        var app = new HealthManagerWebApplicationFactory();
        var client = app.CreateClient();
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestAdminScheme");
        var email = "admin@admin.com";
        var response = await client.GetAsync($"/api/user/getByEmail/{email}");
        response.EnsureSuccessStatusCode();
        
        var user = await response.Content.ReadFromJsonAsync<UserResponse>();
        
        Assert.NotNull(user);
        Assert.Equal(email, user.Email);
    }
}