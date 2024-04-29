using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using wcfRESTful.Inspector;
using static wcfRESTful.ExchangeRates;

namespace wcfRESTful
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioREST" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServicioREST.svc o ServicioREST.svc.cs en el Explorador de soluciones e inicie la depuración.
    
    [TokenValidationBehavior]
    public class ServicioREST : IServicioREST
    {
        ExchangeRates exchangeRates = new ExchangeRates();
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
        
        public List<Model.ExchangeRates> ListaExchangeRates()
        {
            return exchangeRates.ListaExchangeRates;
        }
        public Model.ExchangeRates InsertExchangeRate(ExchangeRatesB exchangeRate)
        {
            return exchangeRates.InsertExchangeRate(exchangeRate);
        }

        public Model.ExchangeRates UpdateExchangeRate(ExchangeRatesC exchangeRatesC)
        {

            return exchangeRates.UpdateExchangeRate(exchangeRatesC);
        }

        public void DeleteExchangeRate(string id)
        {
            exchangeRates.DeleteExchangeRate(int.Parse(id));
        }

        public string GenerateToken()
        {
            string username = System.Configuration.ConfigurationManager.AppSettings["username"];
            string token = TokenGenerator.GenerateToken(username);
            return token;
        }
    }
}
