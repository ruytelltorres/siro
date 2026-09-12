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
    public class LineaNegocioApp : ILineaNegocioApp
    {
        readonly ILineaNegocio lineaNegocio;
        public LineaNegocioApp(ILineaNegocio lineaNegocio)
        {
            this.lineaNegocio = lineaNegocio;
        }
        public List<LineaNegocio> MostrarLineaNegocio()
        {
            return lineaNegocio.MostrarLineaNegocio();
        }

        public List<LineaNegocio> ObtenerLineaNegocio()
        {
            return lineaNegocio.ObtenerLineaNegocio();
        }
    }
}
