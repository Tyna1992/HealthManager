

using Microsoft.AspNetCore.Identity;
namespace HealthManagerServer.Service.Authentication;
public interface ITokenService
{
    public string CreateToken(ApplicationUser user, string role);
}