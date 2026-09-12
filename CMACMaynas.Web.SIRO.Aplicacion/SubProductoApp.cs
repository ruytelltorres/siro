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
    public class SubProductoApp : ISubProductoApp
    {
        readonly ISubProducto subProducto;
        public SubProductoApp(ISubProducto subProducto)
        {
            this.subProducto = subProducto;
        }
        public List<SubProducto> ObtenerSubProducto(string psCodProducto)
        {
            return subProducto.ObtenerSubProducto(psCodProducto);
        }
    }
}
