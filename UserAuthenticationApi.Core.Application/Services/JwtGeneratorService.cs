using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserAuthenticationApi.Core.Application.Dtos;
using UserAuthenticationApi.Core.Application.Interfaces.IServices;
using UserAuthenticationApi.Core.Domain.Entities;
using UserAuthenticationApi.Core.Domain.Settings;

namespace UserAuthenticationApi.Core.Application.Services
{
    public class JwtGeneratorService : IJwtGeneratorService
    {
        JWTSettings _jwtSettings;
        public JwtGeneratorService(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken()
            {
                Token = "",
                Expires = DateTime.UtcNow,
                Revoked = DateTime.UtcNow,
            };
        }

        private string RandomTokenString()
        {
            using var rngCrytoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCrytoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        public Task<string> GenerateJwt(Users user)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email, user.Email)
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentials,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
            };

            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescriptor);
            return Task.FromResult(token);
        }
    }
}
