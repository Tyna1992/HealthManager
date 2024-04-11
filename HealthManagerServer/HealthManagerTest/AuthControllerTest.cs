using System.Net.Http.Json;
using HealthManagerServer.Contracts;
using HealthManagerServer.Service.Authentication;
using Xunit.Abstractions;

namespace HealthManagerTest;

[Collection("Integration")] 
public class AuthControllerTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public AuthControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task RegisterUser_Test()
    {
        var app = new HealthManagerWebApplicationFactory();
        var client = app.CreateClient();

        var request = new RegistrationRequest
        (
            Email: "test@test.com",
            Username : "test",
            Password: "test",
            Weight: 70,
            Gender: "male"
        );
        
        var response = await client.PostAsJsonAsync("api/auth/Register", request);
        _testOutputHelper.WriteLine(response.ToString());
        
        response.EnsureSuccessStatusCode();
        var registrationResponse = await response.Content.ReadFromJsonAsync<RegistrationResponse>();
        
        Assert.NotNull(registrationResponse);
        Assert.Equal("test@test.com", registrationResponse.Email);
        Assert.Equal("test", registrationResponse.UserName);
    }
    
    [Fact]
    public async Task AuthenticateUser_Test()
    {
        var app = new HealthManagerWebApplicationFactory();
        var client = app.CreateClient();

        var request = new AuthRequest
        (
            UserName: "admin",
            Password: "admin"
        );
        
        var response = await client.PostAsJsonAsync("api/auth/Login", request);
        _testOutputHelper.WriteLine(response.ToString());
        
        response.EnsureSuccessStatusCode();
        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        
        Assert.NotNull(authResponse);
        Assert.Equal("admin", authResponse.UserName);
        Assert.Equal("admin@admin.com", authResponse.Email);
    }
}