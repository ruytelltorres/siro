using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace CMACMaynas.Web.SIRO.Aplicacion
{
    public class TallerApp : ITallerApp
    {
        readonly ITaller taller;
        public TallerApp(ITaller taller)
        {
            this.taller = taller;
        }
        public string[] AsignarRiesgosTaller(string psNroRiesgos, string psCodTaller, string psUltimaActualizacion)
        {
            return taller.AsignarRiesgosTaller(psNroRiesgos, psCodTaller, psUltimaActualizacion);
        }

        public Taller ObtenerDatosTaller(string psCodTaller)
        {
            return taller.ObtenerDatosTaller(psCodTaller);
        }

        public List<Usuario> ObtenerUsuarioCodTpoEvaluacion(string psCodTpoEval)
        {
            return taller.ObtenerUsuarioCodTpoEvaluacion(psCodTpoEval);
        }

        public List<TallerRiesgo> ObtenerRiesgosTaller(string psCodTaller)
        {
            return taller.ObtenerRiesgosTaller(psCodTaller);
        }

        public string ObtenerTaller(string psAreaCod)
        {
            return taller.ObtenerTaller(psAreaCod);
        }

        //public List<Taller> ObtenerTallerUsuario(string psCodTpoEval, string psUsuario, string psEstados = "")
        public List<Taller> ObtenerTallerUsuario(string psCodTpoEval)
        {
            //return taller.ObtenerTallerUsuario(psCodTpoEval, psUsuario, psEstados);
            return taller.ObtenerTallerUsuario(psCodTpoEval);
        }

      
    }
}
