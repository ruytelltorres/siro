using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface ICausaRiesgoApp
    {
        List<CausaRiesgo> MostrarCausasRiesgo();
        List<CausaRiesgo> ObtenerCausasRiesgo();

        List<CausaRiesgo> ObtenerCausaPorRiesgo(long pnNroRiesgo);

        int RegistrarGestionCausaRiesgo(string psCodCausa, string psCausaDesc, string psUltimaActualizacion, int pnAccion = 0);
    }
}
