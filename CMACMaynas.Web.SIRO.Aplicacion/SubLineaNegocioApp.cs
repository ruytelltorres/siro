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
    public class SubLineaNegocioApp : ISubLineaNegocioApp
    {
        readonly ISubLineaNegocio subLineaNegocio;
        public SubLineaNegocioApp(ISubLineaNegocio subLineaNegocio)
        {
            this.subLineaNegocio = subLineaNegocio;
        }
        public List<SubLineaNegocio> ObtenerSubLineaNegocio(string psCodLineaNeg)
        {
            return subLineaNegocio.ObtenerSubLineaNegocio(psCodLineaNeg);
        }
    }
}
