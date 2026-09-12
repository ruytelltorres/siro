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
    public class EventoPerdidaApp : IEventoPerdidaApp
    {
        readonly IEventoPerdida evento;
        public EventoPerdidaApp(IEventoPerdida evento)
        {
            this.evento = evento;
        }

        //public string[] GrabarAgrupacionEventoPerdida(long pnNroRiesgo, string psAgrupado, string psUltimaActualizacion)
        //{
        //    return evento.GrabarAgrupacionEventoPerdida(pnNroRiesgo, psAgrupado, psUltimaActualizacion);
        //}

        public string GrabarEventoPerdida(int pnCodDescCorta, string psEventoDesc, string psMedidas, string psAcciones, string psAgeCod, string psAreaCod, string pdFechaOcurrencia, string pdFechaDescubrimiento, string pdFechaRegContable,
                                          string psAnio, int pnClasEventoP, int pnSubClasEventoP, bool pnReportado, string psCtaCont, string psLineaNeg, string psSubLineaNeg,
                                          int pnCobertura, string psProceso, int pnSubProceso, int pnPenMontoPerdida, double pnMontoPerdida, int pnPenMontoRecupera, double pnMontoRecuperado,
                                          int pnPenMontoProvisiona, double pnMontoProvision, double pnMontoBruto, double pnPerdidaNeta, bool pbAsocRiesgo, string psUltimaActualizacion, string psCodEventoPadre = "",
                                          List<dynamic> poDetCuentas = null, List<dynamic> poDetGastos = null)
        {
            return evento.GrabarEventoPerdida(pnCodDescCorta, psEventoDesc, psMedidas, psAcciones, psAgeCod, psAreaCod, pdFechaOcurrencia, pdFechaDescubrimiento, pdFechaRegContable,
                                               psAnio, pnClasEventoP, pnSubClasEventoP, pnReportado, psCtaCont, psLineaNeg, psSubLineaNeg,
                                               pnCobertura, psProceso, pnSubProceso, pnPenMontoPerdida, pnMontoPerdida, pnPenMontoRecupera, pnMontoRecuperado,
                                               pnPenMontoProvisiona, pnMontoProvision, pnMontoBruto, pnPerdidaNeta, pbAsocRiesgo, psUltimaActualizacion, psCodEventoPadre,
                                               poDetCuentas, poDetGastos);
        }

        public bool GrabarActualizacionEventoPerdida(long pnCodEvento, int pnCodDescCorta, string psEventoDesc, string psMedidas, string psAcciones, string psAgeCod, string psAreaCod, string pdFechaOcurrencia, string pdFechaDescubrimiento, string pdFechaRegContable,
                               string psAnio, int pnClasEventoP, int pnSubClasEventoP, bool pnReportado, string psCtaCont, string psLineaNeg, string psSubLineaNeg,
                               int pnCobertura, string psProceso, int pnSubProceso, int pnPenMontoPerdida, double pnMontoPerdida, int pnPenMontoRecupera, double pnMontoRecuperado,
                               int pnPenMontoProvisiona, double pnMontoProvision, double pnMontoBruto, double pnPerdidaNeta, bool pbAsocRiesgo, string psUltimaActualizacion, string psCodEventoPadre = "",
                               List<dynamic> poDetCuentas = null, List<dynamic> poDetGastos = null)
        {
            return evento.GrabarActualizacionEventoPerdida(pnCodEvento, pnCodDescCorta, psEventoDesc, psMedidas, psAcciones, psAgeCod, psAreaCod, pdFechaOcurrencia, pdFechaDescubrimiento, pdFechaRegContable,
                                                            psAnio, pnClasEventoP, pnSubClasEventoP, pnReportado, psCtaCont, psLineaNeg, psSubLineaNeg,
                                                            pnCobertura, psProceso, pnSubProceso, pnPenMontoPerdida, pnMontoPerdida, pnPenMontoRecupera, pnMontoRecuperado,
                                                            pnPenMontoProvisiona, pnMontoProvision, pnMontoBruto, pnPerdidaNeta, pbAsocRiesgo, psUltimaActualizacion, psCodEventoPadre,
                                                            poDetCuentas, poDetGastos);
        }



        public string[] GrabarGestionEventoPerdida(long pnNroRiesgo, string psAgeCod, string psAreaCod, string psLineNeg, string psSubLineaNeg, string psCausas, string psProcesos, string psSubProcesos, int pnSubEventoPerdida, bool pbRiesgoCrediticio, bool pbExEventoPerdida, string psUltimaActualizacion, string psMedidasCorrectivas = "", string psAccionRealizada = "")
        {
            return evento.GrabarGestionEventoPerdida(pnNroRiesgo, psAgeCod, psAreaCod, psLineNeg, psSubLineaNeg, psCausas, psProcesos, psSubProcesos, pnSubEventoPerdida, pbRiesgoCrediticio, pbExEventoPerdida, psUltimaActualizacion, psMedidasCorrectivas, psAccionRealizada);
        }

        public List<EventoPerdida> ListarEventoPerdidaNoGrupo()
        {
            return evento.ListarEventoPerdidaNoGrupo();
        }

        public List<EventoPerdida> ObtenerAgrupacionEvento(long pnNroRiesgo)
        {
            return evento.ObtenerAgrupacionEvento(pnNroRiesgo);
        }

        public List<dynamic> ObtenerClaseEventoPerdida()
        {
            return evento.ObtenerClaseEventoPerdida();
        }

        public dynamic ObtenerCuentasContables(string psFiltroCta)
        {
            return evento.ObtenerCuentasContables(psFiltroCta);
        }

        public EventoPerdida ObtenerDetalleEventoPerdida(long pnNroRiesgo)
        {
            return evento.ObtenerDetalleEventoPerdida(pnNroRiesgo);
        }

        public List<dynamic> ObtenerDetGastosEventoPerdida(long pnNroRiesgo)
        {
            return evento.ObtenerDetGastosEventoPerdida(pnNroRiesgo);
        }

        public List<dynamic> ObtenerDetCtaContablesEventoPerdida(long pnNroRiesgo)
        {
            return evento.ObtenerDetCtaContablesEventoPerdida(pnNroRiesgo);
        }

        public List<EventoPerdida> ObtenerEventoPerdidaAgrupados()
        {
            return evento.ObtenerEventoPerdidaAgrupados();
        }

        public List<EventoPerdida> ObtenerEventosPerdida()
        {
            return evento.ObtenerEventosPerdida();
        }

        public List<EventoPerdida> ObtenerMatrizEventoPerdida(string psDesde, string psHasta)
        {
            return evento.ObtenerMatrizEventoPerdida(psDesde, psHasta);
        }

        public List<dynamic> ObtenerSubClaseEventoPerdida(int pnCodSubClaseEventoP)
        {
            return evento.ObtenerSubClaseEventoPerdida(pnCodSubClaseEventoP);
        }

        public List<SubClaseEventoPerdida> ObtenerSubEventoPerdida(int pnCodEvento)
        {
            return evento.ObtenerSubEventoPerdida(pnCodEvento);
        }

        public double ObtenerTipoCambio(string psFecha)
        {
            return evento.ObtenerTipoCambio(psFecha);
        }

        public bool ValidarEventoPerdida(string psCodEvento)
        {
            return evento.ValidarEventoPerdida(psCodEvento);
        }

        public int EliminarEventoGrupo(long pnEvento)
        {
            return evento.EliminarEventoGrupo(pnEvento);
        }
    }
}
