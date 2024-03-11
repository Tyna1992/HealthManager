

using Microsoft.AspNetCore.Identity;
namespace HealthManagerServer.Service.Authentication;
public interface ITokenService
{
    public string CreateToken(IdentityUser user, string role);
}