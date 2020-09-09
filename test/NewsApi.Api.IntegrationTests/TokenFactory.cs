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
        public static string GetToken()
        {
            string secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secret));

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