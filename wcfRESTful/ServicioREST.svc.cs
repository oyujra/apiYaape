using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using wcfRESTful.Inspector;
using static wcfRESTful.ExchangeRates;

namespace wcfRESTful
{

    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioREST" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServicioREST.svc o ServicioREST.svc.cs en el Explorador de soluciones e inicie la depuración.
    [JwtAuthenticationBehavior]
    public class ServicioREST : IServicioREST
    {
        ExchangeRates exchangeRates = new ExchangeRates();
        JwtProvider _jwtProvider = new JwtProvider("6oGTy1E2ixZb2t33g5hFgPmmAaQkIW5lp7Z1+n9y2mY=");

        public ServicioREST() {
           // _jwtProvider = new JwtProvider("6oGTy1E2ixZb2t33g5hFgPmmAaQkIW5lp7Z1+n9y2mY=");
        }        

        public string devuelveProductoXML(string id)
        {
            return "Has solicitado datos en XML sobre el producto " + id;
        }

        public List<Producto> devuelveProductosXML()
        {
            return Productos.Instance.ListaProductos;
        }


        public string devuelveProductoJSON(string id)
        {
            return "Has solicitado datos en JSON sobre el producto " + id;
        }

        public List<Producto> devuelveProductosJSON()
        {
            return Productos.Instance.ListaProductos;
        }
        
        public JsonResponse<List<Model.ExchangeRates>> ListaExchangeRates()
        {
            //try
            //{
                // Obtener la lista de tasas de cambio
                List<Model.ExchangeRates> exchangeRatesList = exchangeRates.ListaExchangeRates;

                // Crear una instancia de JsonResponse con el mensaje personalizado y los datos
                var response = new JsonResponse<List<Model.ExchangeRates>>
                {
                    Message = "¡Lista de tasas de cambio obtenida correctamente!",
                    Data = exchangeRatesList
                };
                return response;
            /*}
            catch (FaultException ex)
            {
                // Capturar la excepción FaultException y devolver una respuesta personalizada
                var response = new JsonResponse<List<Model.ExchangeRates>>
                {
                    Message = "ERROR: " + ex.Message,
                    Data = null
                };
                return response;
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones que puedan ocurrir durante la obtención de las tasas de cambio
                Console.WriteLine("Error al obtener la lista de tasas de cambio: " + ex.Message);
                // Crear una instancia de JsonResponse con el mensaje de error
                var response = new JsonResponse<List<Model.ExchangeRates>>
                {
                    Message = "ERROR: " + ex.Message,
                    Data = null
                };
                return response;
            }*/

        }
        
        public JsonResponse<Model.ExchangeRates> InsertExchangeRate(ExchangeRatesB exchangeRate)
        {
            // Insertar la tasa de cambio y obtener la tasa insertada
            Model.ExchangeRates insertedRate = exchangeRates.InsertExchangeRate(exchangeRate);

            // Crear una instancia de JsonResponse con el mensaje personalizado y los datos
            var response = new JsonResponse<Model.ExchangeRates>
            {
                Message = "¡Tasa de cambio insertada correctamente!",
                Data = insertedRate
            };

            return response;
        }
        
        public JsonResponse<Model.ExchangeRates> UpdateExchangeRate(ExchangeRatesC exchangeRatesC)
        {
            // Actualizar la tasa de cambio y obtener la tasa actualizada
            Model.ExchangeRates updatedRate = exchangeRates.UpdateExchangeRate(exchangeRatesC);

            // Crear una instancia de JsonResponse con el mensaje personalizado y los datos
            var response = new JsonResponse<Model.ExchangeRates>
            {
                Message = "¡Tasa de cambio actualizada correctamente!",
                Data = updatedRate
            };

            return response;
        }
       
        public void DeleteExchangeRate(string id)
        {
            exchangeRates.DeleteExchangeRate(int.Parse(id));
        }

        public string Login(Login login)
        {
            // Aquí validarías las credenciales del usuario
            // Si las credenciales son válidas, genera un token JWT y devuélvelo
            string token = _jwtProvider.GenerateToken("email", "role", "issuer", "audience");
            return token;
        }
    }
}
