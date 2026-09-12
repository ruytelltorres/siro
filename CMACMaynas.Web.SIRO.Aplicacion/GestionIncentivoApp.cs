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
    public class GestionIncentivoApp : IGestionIncentivoApp
    {
        readonly IGestionIncentivo gestionIncentivo;
        public GestionIncentivoApp(IGestionIncentivo gestionIncentivo)
        {
            this.gestionIncentivo = gestionIncentivo;
        }
        public string GrabaConfigIncentivo(int pnProbabilidad, int pnImpacto, decimal psMontoInc, string psUltimaActualizacion)
        {
            return gestionIncentivo.GrabaConfigIncentivo(pnProbabilidad, pnImpacto, psMontoInc, psUltimaActualizacion);
        }

        public int GrabaGestionIncentivo(long pnNroRiesog, int pnMotivo, string psComentario, decimal psMontoInc, string cNombreDoc, string cNombreDocDB, string psUltimaActualizacion)
        {
            return gestionIncentivo.GrabaGestionIncentivo(pnNroRiesog, pnMotivo, psComentario, psMontoInc, cNombreDoc, cNombreDocDB, psUltimaActualizacion);
        }

        public List<Incentivos> MostrarConfigMontoIncentos()
        {
            return gestionIncentivo.MostrarConfigMontoIncentos();
        }

        public Incentivos ObtenerDetalleIncentivo(long pnNroRiesog)
        {
            return gestionIncentivo.ObtenerDetalleIncentivo(pnNroRiesog);
        }

        public List<Incentivos> ObtenerRiesgoOperacionalIncentivo(int pnTrimestre, int pnAnio)
        {
            return gestionIncentivo.ObtenerRiesgoOperacionalIncentivo(pnTrimestre, pnAnio);
        }
    }
}
