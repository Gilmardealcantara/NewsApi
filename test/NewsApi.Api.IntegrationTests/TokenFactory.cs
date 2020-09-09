using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NewsApi.Api.IntegrationTests
{
    public static class TokenFactory
    {
        public static string GetToken(NewsApi.Api.Configurations.Authorization auth)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(auth.SecretKey));

            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha512);


            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, "Gilmar de Alcantara"),
                new Claim(ClaimTypes.Email, "gilmardealcantara@gmail.com")
            };

            var handler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(securityKey,
                  SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtSecurityToken = handler.CreateJwtSecurityToken(descriptor);

            return handler.WriteToken(jwtSecurityToken);
        }
    }
}