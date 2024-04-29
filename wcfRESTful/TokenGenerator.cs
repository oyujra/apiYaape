
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace wcfRESTful
{
    public static class TokenGenerator
    {

        //private const string SecretKey = System.Configuration.ConfigurationManager.AppSettings["KeyWoord"]; // Clave secreta para firmar el token (cambia esto)
        private static readonly string SecretKey = System.Configuration.ConfigurationManager.AppSettings["SecretKey"];
        private static readonly SymmetricSecurityKey _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        public static string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username) // Puedes añadir más claims según tus necesidades
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Tiempo de expiración del token
                SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}