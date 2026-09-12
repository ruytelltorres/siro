using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion
{
    public class ProductoApp : IProductoApp
    {
        readonly IProducto producto;
        public ProductoApp(IProducto producto)
        {
            this.producto = producto;
        }
        public List<Producto> ObtenerProducto(string psCodLineaNeg)
        {
            return producto.ObtenerProducto(psCodLineaNeg);
        }
    }
}
