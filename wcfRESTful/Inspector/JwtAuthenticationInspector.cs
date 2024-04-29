using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace wcfRESTful.Inspector
{
    public class JwtAuthenticationInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            // Aquí realizas la validación del token JWT
            HttpRequestMessageProperty httpRequest = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
            string token = httpRequest.Headers["Authorization"];

            if (string.IsNullOrEmpty(token))
            {
                return new FaultException("Token de autorización no proporcionado.");
            }

            // Aquí puedes implementar la lógica para validar el token JWT
            var jwtProvider = new JwtProvider("6oGTy1E2ixZb2t33g5hFgPmmAaQkIW5lp7Z1+n9y2mY=");
            var issuer = "issuer";
            var audience = "audience";

            try
            {
                // Validar el token JWT
                ClaimsPrincipal principal = jwtProvider.ValidateToken(token, issuer, audience);

                // Si la validación es exitosa, puedes acceder a los claims del token a través del principal
                // Por ejemplo, principal.FindFirst(ClaimTypes.NameIdentifier).Value te dará el valor del claim "sub" (subject)

                // Continuar con el procesamiento de la solicitud
                return null;
            }
            catch (SecurityTokenValidationException ex)
            {
                // Manejar la excepción si la validación del token falla
                //return new FaultException("Token de autorización inválido.");
                throw;
            }
            catch (FaultException)
            {
                // Ya se está devolviendo una excepción FaultException, no es necesario hacer nada aquí
                throw;
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones que puedan ocurrir durante la validación del token
                Console.WriteLine("Error durante la validación del token JWT: " + ex.Message);
                //return new FaultException("Error durante la validación del token JWT: " + ex.Message);
                throw;
            }
        }

        public void BeforeSendReply(ref Message reply, object correlationState) { }

    }
}