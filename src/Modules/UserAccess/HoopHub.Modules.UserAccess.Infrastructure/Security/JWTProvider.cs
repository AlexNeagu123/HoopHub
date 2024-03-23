using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HoopHub.Modules.UserAccess.Application.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HoopHub.Modules.UserAccess.Infrastructure.Security
{
    public class JwtProvider(IConfiguration configuration)
    {
        private const int ExpirationLimit = 3;
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[Config.JwtSecretKey] ?? string.Empty));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration[Config.JwtIssuerKey],
                Audience = configuration[Config.JwtAudienceKey],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(ExpirationLimit),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
