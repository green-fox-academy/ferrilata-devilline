using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ferrilata_devilline.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(string UserEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("FDTOKENSECRET"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                 {
                    new Claim(ClaimTypes.Email, UserEmail)
                 }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                , SecurityAlgorithms.HmacSha256Signature)
            };

            string token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return token;
        }

        public string GenerateTestToken(string UserEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("7f57a148-46cf-4ca1-84b9-b76a127526f0");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                 {
                    new Claim(ClaimTypes.Email, UserEmail)
                 }),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                , SecurityAlgorithms.HmacSha256Signature)
            };

            string token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return token;
        }
    }
}
