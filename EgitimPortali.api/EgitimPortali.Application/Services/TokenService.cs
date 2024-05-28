using EgitimPortali.Core.Entities;
using EgitimPortali.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EgitimPortali.Application.Services
{
    public static class TokenService
    {
        public static string CreateToken(User user)
        {
            var settings = Configuration.GetSettings<Settings>("TokenInfo");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var expiry = DateTime.Now.AddDays(settings.ValidityPeriod);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(settings.ValidAudience,settings.ValidIssuer, claims, null, expiry, creds);

            String tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            String bearerToken = "Bearer " + tokenStr;
            return bearerToken;
        }
        public static DecodedTokenInfo DecodeToken(string authorizationHeader)
        {
            string jwtToken = authorizationHeader.Replace("Bearer ", "");
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = tokenHandler.ReadJwtToken(jwtToken);

            var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var email = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
            var role = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            return new DecodedTokenInfo
            {
                UserId = Guid.Parse(userId),
                Email = email,
                Role = role
            };
        }
    }
}
