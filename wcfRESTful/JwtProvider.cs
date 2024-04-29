using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace wcfRESTful
{
    public class JwtProvider
    {
        private readonly string _plainTextSecurityKey;

        // Constructor que recibe la clave secreta
        public JwtProvider(string plainTextSecurityKey)
        {
            _plainTextSecurityKey = plainTextSecurityKey;
        }

        public string GenerateToken(string email, string role, string issuer, string audience)
        {
            // Se crea la clave de firma utilizando la clave secreta
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_plainTextSecurityKey));

            // Se definen las credenciales de firma utilizando la clave de firma y el algoritmo de firma
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // Se crea una identidad de claims con el email y el rol del usuario
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, email),
                new Claim(ClaimTypes.Role, role),
            }, "Custom");

            // Se crea la descripción del token de seguridad
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                Audience = audience,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(1), // El token expira en 1 hora
                SigningCredentials = signingCredentials,
            };

            // Se instancia el manejador de tokens JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            // Se crea y codifica el token
            var token = tokenHandler.CreateToken(securityTokenDescriptor);

            // Se escribe y codifica el token
            return tokenHandler.WriteToken(token);
        }

        // Método para validar un token JWT
        /*public ClaimsPrincipal ValidateToken(string token, string issuer, string audience)
        {
            // Se crea la clave de firma utilizando la clave secreta
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_plainTextSecurityKey));

            // Se definen los parámetros de validación del token
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = signingKey
            };

            // Se instancia el manejador de tokens JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            // Se valida el token y se retorna el principal de claims si es válido
            return tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
        }*/
        public ClaimsPrincipal ValidateToken(string token, string issuer, string audience)
        {
            try
            {
                // Se crea la clave de firma utilizando la clave secreta
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_plainTextSecurityKey));

                // Se definen los parámetros de validación del token
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = signingKey
                };

                // Se instancia el manejador de tokens JWT
                var tokenHandler = new JwtSecurityTokenHandler();

                // Se valida el token y se retorna el principal de claims si es válido
                return tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            }
            catch (SecurityTokenMalformedException ex)
            {
                // Manejar la excepción si el token está malformado
                Console.WriteLine("Error: El token JWT está malformado.");
                // Crear una nueva excepción con un mensaje personalizado y lanzarla
                throw new SecurityTokenMalformedException("El token JWT está malformado.", ex);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones que puedan ocurrir durante la validación del token
                Console.WriteLine("Error durante la validación del token JWT: " + ex.Message);
                // Crear una nueva excepción con un mensaje personalizado y lanzarla
                throw new Exception("Error durante la validación del token JWT: " + ex.Message);
            }
        }

    }
}