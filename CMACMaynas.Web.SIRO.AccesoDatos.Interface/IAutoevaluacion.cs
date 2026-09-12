using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
    public interface IAutoevaluacion
    {
        //List<Constante> ObtenerTipoEvaluacion();

        string RegistrarEvaluacion(int pnCodEvaluacion, string psRiesgoIdentificado, string psCauasRiesgo, string psUltimaActualizacion,
                                             string psAgeCod = "", string psAreaCod = "", string psCodProceso = "", string psCodSubProceso = "");

        //List<DatosRiesgos> MostrarEvaluacionesGestion(int pnTpoBuscar, string psValorBuscar, string psUsuario);
        
        //DatosRiesgos MostrarDetalleEvaluacion(long pnNroRiesgo);

        //List<DatosRiesgos> MostrarEvaluacionNotificacion(string psUser);

        //List<DatosRiesgos> MostrarEvaluacionVerificacionTaller(string psCodTaller);

        List<Autoevaluacion> MostrarEvaluacionesGestion(int pnTpoBuscar, string psValorBuscar, string psUsuario);
        Autoevaluacion MostrarDetalleEvaluacion(long pnNroRiesgo);

        List<Autoevaluacion> MostrarEvaluacionNotificacion(string psUser);

        List<Autoevaluacion> MostrarEvaluacionVerificacionTaller(string psCodTaller);
    }
}
