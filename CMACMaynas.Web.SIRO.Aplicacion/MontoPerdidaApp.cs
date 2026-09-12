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
    public class MontoPerdidaApp : IMontoPerdidaApp
    {
        readonly IMontoPerdida montoPerdida;
        public MontoPerdidaApp(IMontoPerdida montoPerdida)
        {
            this.montoPerdida = montoPerdida;
        }
        public int GrabaMontoPerdida(int pnProbabilidad, int pnImpacto, decimal pnMontoPerdida, string psComentarios, string psUltimaActualizacion)
        {
            return montoPerdida.GrabaMontoPerdida(pnProbabilidad, pnImpacto, pnMontoPerdida, psComentarios, psUltimaActualizacion);
        }

        public List<MontoPerdida> ObtenerMontoPerdida()
        {
            return montoPerdida.ObtenerMontoPerdida();
        }
    }
}
