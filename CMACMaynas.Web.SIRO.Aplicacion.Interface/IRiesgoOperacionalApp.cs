using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface IRiesgoOperacionalApp
    {
        //string Registrar(string psRiesgoIdent, string psAgeCod, string psFechaDetec, string psAreaCod, string psCausasRiesgo, string psProceso,
        //                         int pnSubProceso, string psControlProceso, int pbEfectividadCtrl, string psUltimaActualizacion, string psObservacion = "",
        //                         List<dynamic> paControles = null);

        string Registrar(RiesgoOperacional model);

        //int Actualizar(long pnNroRiesog, string psRiesgoIdent, string psAgeCod, string psFechaDetec, string psAreaCod, string psCausasRiesgo, string psProceso,
        //                         int pnSubProceso, string psControlProceso, int pbEfectividadCtrl, string psUltimaActualizacion, string psObservacion = "",
        //                         List<dynamic> paControles = null);

        int Actualizar(RiesgoOperacional model);


        ///// <summary>
        ///// Obtiene informacion del registro inical del riesgo operacional
        ///// </summary>
        ///// <param name="pnNroRiesgo"></param>
        ///// <returns></returns>
        //DatosRiesgos ObtenerInfoGeneralRiesgoOperacional(long pnNroRiesgo);

        RiesgoOperacional ObtenerInfoGeneralRiesgoOperacional(DetalleRiesgo model);


        //List<DatosRiesgos> ObtenerDatosRiesgoOperacional(string psUser /*, string psBuscar = "", int nTpoRiesgo = 0, int pnTpoBusqueda = 0*/);
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
