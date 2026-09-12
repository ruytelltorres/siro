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
    public class CausaRiesgoApp : ICausaRiesgoApp
    {
        readonly ICausaRiesgo causa;

        public CausaRiesgoApp(ICausaRiesgo causa)
        {
            this.causa = causa;
        }
        public List<CausaRiesgo> MostrarCausasRiesgo()
        {
            return causa.MostrarCausasRiesgo();
        }

        public List<CausaRiesgo> ObtenerCausaPorRiesgo(long pnNroRiesgo)
        {
            return causa.ObtenerCausaPorRiesgo(pnNroRiesgo);
        }

        public List<CausaRiesgo> ObtenerCausasRiesgo()
        {
            return causa.ObtenerCausasRiesgo();
        }

        public int RegistrarGestionCausaRiesgo(string psCodCausa, string psCausaDesc, string psUltimaActualizacion, int pnAccion = 0)
        {
            return causa.RegistrarGestionCausaRiesgo(psCodCausa, psCausaDesc, psUltimaActualizacion, pnAccion);
        }
    }
}
