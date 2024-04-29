using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace wcfRESTful
{
    [DataContract]
    public class Producto
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int precio { get; set; }
    }

    public partial class Productos
    {
        private static readonly Productos _instance = new Productos();

        private Productos() { }

        public static Productos Instance
        {
            get { return _instance; }
        }

        public List<Producto> ListaProductos
        {
            get { return productos; }
        }

        private List<Producto> productos = new List<Producto>()
        {
            new Producto() {id = 1, nombre = "Producto 1", precio = 100},
            new Producto() {id = 2, nombre = "Producto 2", precio = 200},
            new Producto() {id = 3, nombre = "Producto 3", precio = 300},
            new Producto() {id = 4, nombre = "Producto 4", precio = 400},
        };

    }
}