using HealthManagerServer.Service.Authentication;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(string email, string username, string password, double weight, string gender, string role);
    Task<AuthResult> LoginAsync(string email, string password);
}