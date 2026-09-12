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
    public class ReportesApp : IReportesApp
    {
        readonly IReportes reportes;
        public ReportesApp(IReportes reportes)
        {
            this.reportes = reportes;
        }
        public List<dynamic> MatrizRiesgo(int pnTpoRiesgo, bool pbFiltro, string psAreaCod, string psDesde, string psHasta)
        {
            return reportes.MatrizRiesgo(pnTpoRiesgo, pbFiltro, psAreaCod, psDesde, psHasta);
        }

        public List<DatosRiesgos> ObtenerDatosMapaDet(int pnProbabilidad, int pnImpacto, int pnTpoRiesgo, int pnTpoNivRiesgo)
        {
            return reportes.ObtenerDatosMapaDet(pnProbabilidad, pnImpacto, pnTpoRiesgo, pnTpoNivRiesgo);
        }

        public List<dynamic> ObtenerDetalleRiesgoNivelRiesgo(int pnTpoRiesgo, int pnTpoNivRiesgo)
        {
            return reportes.ObtenerDetalleRiesgoNivelRiesgo(pnTpoRiesgo, pnTpoNivRiesgo);
        }

        public List<dynamic> ObtenerEventoPerdidaValorFiltro(string psNombreCol, string psValorFiltro)
        {
            return reportes.ObtenerEventoPerdidaValorFiltro(psNombreCol, psValorFiltro);
        }

        public List<Constante> ObtenerMapaRiesgo(int pnTpoRiesgo, int pnTpoNivRiesgo)
        {
            return reportes.ObtenerMapaRiesgo(pnTpoRiesgo, pnTpoNivRiesgo);
        }

        public List<dynamic> ObtenerRiesgoPlanAccionReporte(int pnTpoBuscar, int pnTpoRiesgo, string psValorBuscar)
        {
            return reportes.ObtenerRiesgoPlanAccionReporte(pnTpoBuscar, pnTpoRiesgo, psValorBuscar);
        }

        public List<dynamic> ReporteGraficoRiesgoOperacional(int pnIndex, int pnTpoRiesgo)
        {
            return reportes.ReporteGraficoRiesgoOperacional(pnIndex, pnTpoRiesgo);
        }

        public List<dynamic> ReporteGraficoCantidadAnio(int pnTpoRiesgo)
        {
            return reportes.ReporteGraficoEvaluaciones(pnTpoRiesgo);
        }

        public List<Constante> ReporteGraficoCantidadRiesgoNivelResidual(int pnTpoRiesgo)
        {
            return reportes.ReporteGraficoCantidadRiesgoNivelResidual(pnTpoRiesgo);
        }

        public List<dynamic> ReportesGraficosEventoPerdida(int pnIndex)
        {
            return reportes.ReportesGraficosEventoPerdida(pnIndex);
        }

        public List<dynamic> ObtenerRiesgoRiesgosOperacionalesProceso(string psProcesos)
        {
            return reportes.ObtenerRiesgoRiesgosOperacionalesProceso(psProcesos);
        }

        public List<dynamic> ObtenerObjetoReporteIncentivo(int pnReporte, int pnTpoRiesgo, int pnMotivo = 0, decimal pnMonto = 0)
        {
            return reportes.ObtenerObjetoReporteIncentivo(pnReporte, pnTpoRiesgo, pnMotivo, pnMonto);
        }

        public List<dynamic> ObtenerRiesgoEstadoPlanAccion(int pnTpoRiesgo, int pnEstadoPlan)
        {
            return reportes.ObtenerRiesgoEstadoPlanAccion(pnTpoRiesgo, pnEstadoPlan);
        }

        public List<dynamic> RiesgosOperacinalesRechazados(string psDesde, string psHasta)
        {
            return reportes.RiesgosOperacinalesRechazados(psDesde, psHasta);
        }

        
    }
}
