using HealthManagerServer.Model;

namespace HealthManagerServer.Service.Authentication;

public interface ITokenService
{
    string CreateToken(ApplicationUser user, string role);
    
}