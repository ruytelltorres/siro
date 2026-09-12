using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
   public interface ITaller
    {
        string ObtenerTaller(string psAreaCod);

        List<Usuario> ObtenerUsuarioCodTpoEvaluacion(string psCodTpoEval);

        //List<Taller> ObtenerTallerUsuario(string psCodTpoEval, string psUsuario, string psEstados = "");
        List<Taller> ObtenerTallerUsuario(string psCodTpoEval);
        
        List<TallerRiesgo> ObtenerRiesgosTaller(string psCodTaller);

        Taller ObtenerDatosTaller(string psCodTaller);

        string[] AsignarRiesgosTaller(string psNroRiesgos, string psCodTaller, string psUltimaActualizacion);

    }
}
