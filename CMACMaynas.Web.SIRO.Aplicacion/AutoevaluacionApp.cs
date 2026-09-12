using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace CMACMaynas.Web.SIRO.Aplicacion
{
    public class AutoevaluacionApp : IAutoevaluacionApp
    {
        readonly IAutoevaluacion evaluacion;
        public AutoevaluacionApp(IAutoevaluacion evaluacion)
        {
            this.evaluacion = evaluacion;
        }
        //public DatosRiesgos MostrarDetalleEvaluacion(long pnNroRiesgo)
        //{
        //    return evaluacion.MostrarDetalleEvaluacion(pnNroRiesgo);
        //}

        //public List<DatosRiesgos> MostrarEvaluacionesGestion(int pnTpoBuscar, string psValorBuscar, string psUsuario)
        //{
        //    return evaluacion.MostrarEvaluacionesGestion(pnTpoBuscar, psValorBuscar, psUsuario);
        //}

        //public List<DatosRiesgos> MostrarEvaluacionNotificacion(string psUser)
        //{
        //    return evaluacion.MostrarEvaluacionNotificacion(psUser);
        //}

        //public List<DatosRiesgos> MostrarEvaluacionVerificacionTaller(string psCodTaller)
        //{
        //    return evaluacion.MostrarEvaluacionVerificacionTaller(psCodTaller);
        //}

        public Autoevaluacion MostrarDetalleEvaluacion(long pnNroRiesgo)
        {
            return evaluacion.MostrarDetalleEvaluacion(pnNroRiesgo);
        }

        public List<Autoevaluacion> MostrarEvaluacionesGestion(int pnTpoBuscar, string psValorBuscar, string psUsuario)
        {
            return evaluacion.MostrarEvaluacionesGestion(pnTpoBuscar, psValorBuscar, psUsuario);
        }

        public List<Autoevaluacion> MostrarEvaluacionNotificacion(string psUser)
        {
            return evaluacion.MostrarEvaluacionNotificacion(psUser);
        }

        public List<Autoevaluacion> MostrarEvaluacionVerificacionTaller(string psCodTaller)
        {
            return evaluacion.MostrarEvaluacionVerificacionTaller(psCodTaller);
        }

        public string RegistrarEvaluacion(int pnCodEvaluacion, string psRiesgoIdentificado, string psCauasRiesgo, string psUltimaActualizacion, string psAgeCod = "", string psAreaCod = "", string psCodProceso = "", string psCodSubProceso = "")
        {
            return evaluacion.RegistrarEvaluacion(pnCodEvaluacion, psRiesgoIdentificado, psCauasRiesgo, psUltimaActualizacion, psAgeCod, psAreaCod, psCodProceso, psCodSubProceso);
        }
    }
}
