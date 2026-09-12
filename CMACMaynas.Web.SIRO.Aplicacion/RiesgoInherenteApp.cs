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
    public class RiesgoInherenteApp : IRiesgoInherenteApp
    {
        readonly IRiesgoInherente riesgoInherente;
        public RiesgoInherenteApp(IRiesgoInherente riesgoInherente)
        {
            this.riesgoInherente = riesgoInherente;
        }
        public RiesgoInherente ObtenerNivelRiesgoInherente(int pnProbalidad, int pnImpacto)
        {
            return riesgoInherente.ObtenerNivelRiesgoInherente(pnProbalidad, pnImpacto); ;
        }
    }
}
