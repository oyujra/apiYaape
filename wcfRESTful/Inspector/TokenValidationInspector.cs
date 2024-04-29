using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;

namespace wcfRESTful.Inspector
{
    public class TokenValidationInspector : IDispatchMessageInspector
    {
        
        private const string TokenHeaderNameX = "AuthorizationX";
        private const string TokenHeaderName = "Authorization";
        private readonly HashSet<string> MethodsToSecure = new HashSet<string> {
        "ListaExchangeRates","InsertExchangeRate","UpdateExchangeRate","DeleteExchangeRate"
        };
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (WebOperationContext.Current == null || WebOperationContext.Current.IncomingRequest == null)
                throw new FaultException("Unauthorized. Token missing.");

            string methodName = GetMethodName();
            if (MethodsToSecure.Contains(methodName))
            {
                var tokenHeader = WebOperationContext.Current.IncomingRequest.Headers[TokenHeaderNameX]==null? WebOperationContext.Current.IncomingRequest.Headers[TokenHeaderName]: WebOperationContext.Current.IncomingRequest.Headers[TokenHeaderNameX];

                if (string.IsNullOrEmpty(tokenHeader))
                    throw new FaultException("Unauthorized. Token missing.");

                string token = tokenHeader.Replace("Bearer ", "");

                if (!ValidateToken(token))
                    throw new FaultException("Unauthorized. Invalid token.");
            }

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            // No action needed
        }

        private bool ValidateToken(string token)
        {
            try
            {
                string SecretKey = System.Configuration.ConfigurationManager.AppSettings["SecretKey"];
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
                    ValidateIssuer = false, // Puedes configurar estos valores según tus necesidades
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Puedes ajustar el tiempo de tolerancia aquí
                };

                SecurityToken validatedToken;
                tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción, registrarla, etc.
                return false;
            }
        }

        private string GetMethodName()
        {
            OperationContext context = OperationContext.Current;
            if (context != null && context.IncomingMessageProperties.TryGetValue("HttpOperationName", out object value))
            {
                return value.ToString();
            }
            return string.Empty;
        }
    }
}