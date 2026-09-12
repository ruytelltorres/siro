using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface IGestionIncentivoApp
    {
        List<Incentivos> ObtenerRiesgoOperacionalIncentivo(int pnTrimestre, int pnAnio);

        Incentivos ObtenerDetalleIncentivo(long pnNroRiesog);

        int GrabaGestionIncentivo(long pnNroRiesog, int pnMotivo, string psComentario, decimal psMontoInc, string cNombreDoc, string cNombreDocDB, string psUltimaActualizacion);

        string GrabaConfigIncentivo(int pnProbabilidad, int pnImpacto, decimal psMontoInc, string psUltimaActualizacion);
        List<Incentivos> MostrarConfigMontoIncentos();
    }
}
