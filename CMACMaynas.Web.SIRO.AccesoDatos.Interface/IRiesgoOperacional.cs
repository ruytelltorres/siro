using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
    public interface IRiesgoOperacional
    {
        string Registrar(RiesgoOperacional model);

        int Actualizar(RiesgoOperacional model);

        /// <summary>
        /// Obtiene informacion del registro inical del riesgo operacional
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        RiesgoOperacional ObtenerInfoGeneralRiesgoOperacional(DetalleRiesgo model);


        List<RiesgoOperacional> ObtenerDatosRiesgoOperacional(DetalleRiesgo model);


        string[] GrabarProcesoPaso1(long pnNroRiesgo, int pnFactorRiesgo, int pnEventoPerdida, int pnSubEventoPerdida, string psLineaNeg, string psProducto, string psSubProducto, string psUltimaActializacion);

        string[] GrabarProcesoPaso2(long pnNroRiesgo, int pnProbabilidad, int pnImpacto, string psUltimaActualizacion);


        string[] GrabarProcesoPaso3(long pnNroRiesgo, string psComentarios, string psUltimaActualizacion);

        string[] GrabarProcesoPaso4(long pnNroRiesgo, /*string psComentarios,*/ string psUltimaActualizacion);

        int GrabarProcesoPaso5(long pnNroRiesgo, string psUltimaActualizacion);

        List<string> ObtenerInfoGestionRiesgo(long pnNroRiesgo, int pnProceso);

        int GrabarObservacionGestion(long pnNroRiesgo, List<dynamic> poObservaciones, string psUltimaActializacion);

        List<dynamic> ObtenerObservacionesGestion(long pnNroRiesgo, int pnProcedencia = 2);

        int ConfirmarModificacionesRiesgo(long pnNroRiesgo, string psUltimaActualizacion);

        int RechazarRiesgo(long pnNroRiesgo, string psMotivoRechazo, string psUltimaActualizacion);

        int EliminarRiesgo(long pnNroRiesgo, string psUltimaActualizacion);

        int ObtenerTpoRiesgo(long pnNroRiesgo);
    }
}
