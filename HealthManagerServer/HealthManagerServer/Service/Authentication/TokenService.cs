using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace HealthManagerServer.Service.Authentication;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private const int ExpirationMinutes = 60;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(ApplicationUser user, string role)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(
            CreateClaims(user, role),
            CreateSigningCredentials(),
            expiration
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
        DateTime expiration) =>
        new(
            _configuration.GetSection("JwtSettings").Get<JwtSettings>().ValidIssuer,
            _configuration.GetSection("JwtSettings").Get<JwtSettings>().ValidAudience,
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

    private List<Claim> CreateClaims(ApplicationUser user, string? role)
    {
        try
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),

            };
            if (!string.IsNullOrEmpty(user.Gender))
            {
                claims.Add(new Claim("Gender", user.Gender));
            }

            claims.Add(new Claim("Weight", user.Weight.ToString()));

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("IssuerSigningKey").Value)
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}