using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface IAutoevaluacionApp
    {
        string RegistrarEvaluacion(int pnCodEvaluacion, string psRiesgoIdentificado, string psCauasRiesgo, string psUltimaActualizacion,
                                             string psAgeCod = "", string psAreaCod = "", string psCodProceso = "", string psCodSubProceso = "");

        List<Autoevaluacion> MostrarEvaluacionesGestion(int pnTpoBuscar, string psValorBuscar, string psUsuario);

        Autoevaluacion MostrarDetalleEvaluacion(long pnNroRiesgo);

        List<Autoevaluacion> MostrarEvaluacionNotificacion(string psUser);

        List<Autoevaluacion> MostrarEvaluacionVerificacionTaller(string psCodTaller);

    }
}
