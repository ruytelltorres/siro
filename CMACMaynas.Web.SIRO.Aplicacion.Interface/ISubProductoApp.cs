using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface ISubProductoApp
    {
        List<SubProducto> ObtenerSubProducto(string psCodProducto);
    }
}
