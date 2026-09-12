using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface IEventoPerdidaApp
    {
        List<dynamic> ObtenerClaseEventoPerdida();

        List<dynamic> ObtenerSubClaseEventoPerdida(int pnCodSubClaseEventoP);

        List<EventoPerdida> ObtenerEventosPerdida();

        EventoPerdida ObtenerDetalleEventoPerdida(long pnNroRiesgo);

        List<SubClaseEventoPerdida> ObtenerSubEventoPerdida(int pnCodEvento);


        string[] GrabarGestionEventoPerdida(long pnNroRiesgo, string psAgeCod, string psAreaCod, string psLineNeg, string psSubLineaNeg, string psCausas,
                                           string psProcesos, string psSubProcesos, int pnSubEventoPerdida, bool pbRiesgoCrediticio, bool pbExEventoPerdida, string psUltimaActualizacion,
                                           string psMedidasCorrectivas = "", string psAccionRealizada = "");

        bool GrabarActualizacionEventoPerdida(long pnCodEvento, int pnCodDescCorta, string psEventoDesc, string psMedidas, string psAcciones, string psAgeCod, string psAreaCod, string pdFechaOcurrencia, string pdFechaDescubrimiento, string pdFechaRegContable,
                                              string psAnio, int pnClasEventoP, int pnSubClasEventoP, bool pnReportado, string psCtaCont, string psLineaNeg, string psSubLineaNeg,
                                              int pnCobertura, string psProceso, int pnSubProceso, int pnPenMontoPerdida, double pnMontoPerdida, int pnPenMontoRecupera, double pnMontoRecuperado,
                                              int pnPenMontoProvisiona, double pnMontoProvision, double pnMontoBruto, double pnPerdidaNeta, bool pbAsocRiesgo, string psUltimaActualizacion, string psCodEventoPadre = "",
                                              List<dynamic> poDetCuentas = null, List<dynamic> poDetGastos = null);

        dynamic ObtenerCuentasContables(string psFiltroCta);

        double ObtenerTipoCambio(string psFecha);

        string GrabarEventoPerdida(int pnCodDescCorta, string psEventoDesc, string psMedidas, string psAcciones, string psAgeCod, string psAreaCod, string pdFechaOcurrencia, string pdFechaDescubrimiento, string pdFechaRegContable,
                                          string psAnio, int pnClasEventoP, int pnSubClasEventoP, bool pnReportado, string psCtaCont, string psLineaNeg, string psSubLineaNeg,
                                          int pnCobertura, string psProceso, int pnSubProceso, int pnPenMontoPerdida, double pnMontoPerdida, int pnPenMontoRecupera, double pnMontoRecuperado,
                                          int pnPenMontoProvisiona, double pnMontoProvision, double pnMontoBruto, double pnPerdidaNeta, bool pbAsocRiesgo, string psUltimaActualizacion, string psCodEventoPadre = "",
                                          List<dynamic> poDetCuentas = null, List<dynamic> poDetGastos = null);


        List<dynamic> ObtenerDetGastosEventoPerdida(long pnNroRiesgo);
        List<dynamic> ObtenerDetCtaContablesEventoPerdida(long pnNroRiesgo);

        List<EventoPerdida> ObtenerMatrizEventoPerdida(string psDesde, string psHasta);

        //string[] GrabarAgrupacionEventoPerdida(long pnNroRiesgo, string psAgrupado, string psUltimaActualizacion);

        List<EventoPerdida> ObtenerEventoPerdidaAgrupados();

        List<EventoPerdida> ObtenerAgrupacionEvento(long pnNroRiesgo);

        List<EventoPerdida> ListarEventoPerdidaNoGrupo();

        bool ValidarEventoPerdida(string psCodEvento);

        int EliminarEventoGrupo(long pnEvento);
    }
}
