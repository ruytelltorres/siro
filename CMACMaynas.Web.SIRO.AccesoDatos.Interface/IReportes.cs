using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
    public interface IReportes
    {
        List<dynamic> MatrizRiesgo(int pnTpoRiesgo, bool pbFiltro, string psAreaCod, string psDesde, string psHasta);

        List<dynamic> ReporteGraficoRiesgoOperacional(int pnIndex, int pnTpoRiesgo);

        List<Constante> ReporteGraficoCantidadRiesgoNivelResidual(int pnTpoRiesgo);

        List<Constante> ObtenerMapaRiesgo(int pnTpoRiesgo, int pnTpoNivRiesgo);

        List<DatosRiesgos> ObtenerDatosMapaDet(int pnProbabilidad, int pnImpacto, int pnTpoRiesgo, int pnTpoNivRiesgo);

        List<dynamic> ObtenerDetalleRiesgoNivelRiesgo(int pnTpoRiesgo, int pnTpoNivRiesgo);

        List<dynamic> ReporteGraficoEvaluaciones(int pnTpoRiesgo);

        List<dynamic> ReportesGraficosEventoPerdida(int pnIndex);

        List<dynamic> ObtenerEventoPerdidaValorFiltro(string psNombreCol, string psValorFiltro);

        List<dynamic> ObtenerRiesgoPlanAccionReporte(int pnTpoBuscar, int pnTpoRiesgo, string psValorBuscar);

        List<dynamic> ObtenerRiesgoRiesgosOperacionalesProceso(string psProcesos);

        List<dynamic> ObtenerObjetoReporteIncentivo(int pnReporte, int pnTpoRiesgo, int pnMotivo = 0, decimal pnMonto = 0);
        List<dynamic> ObtenerRiesgoEstadoPlanAccion(int pnTpoRiesgo, int pnEstadoPlan);

        List<dynamic> RiesgosOperacinalesRechazados(string psDesde, string psHasta);


    }
}
