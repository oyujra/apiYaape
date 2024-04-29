using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel.Web;
using static wcfRESTful.ExchangeRates;

namespace wcfRESTful
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioREST" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioREST
    {
        [OperationContract]
        [WebInvoke(Method="GET", 
            ResponseFormat=WebMessageFormat.Xml, 
            BodyStyle=WebMessageBodyStyle.Wrapped, 
            UriTemplate="ProductosXML/{id}")]
        string devuelveProductoXML(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "ProductosXML/")]
        List<Producto> devuelveProductosXML();
        
        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Wrapped, 
            UriTemplate = "ProductosJSON/{id}")]
        string devuelveProductoJSON(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "ProductosJSON/")]
        List<Producto> devuelveProductosJSON();



        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "ExchangeRates/")]
        JsonResponse<List<Model.ExchangeRates>> ListaExchangeRates();

        [OperationContract]
        [WebInvoke(Method = "POST",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "ExchangeRates/")]
        JsonResponse<Model.ExchangeRates> InsertExchangeRate(ExchangeRatesB exchangeRate);

        [OperationContract]
        [WebInvoke(Method = "PUT",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "ExchangeRates/")]
        JsonResponse<Model.ExchangeRates> UpdateExchangeRate(ExchangeRatesC exchangeRatesC);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "ExchangeRates/{id}")]
        void DeleteExchangeRate(string id);


        [OperationContract]
        [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "ExchangeRates/Login")]
        string Login(Login login);


    }
}
