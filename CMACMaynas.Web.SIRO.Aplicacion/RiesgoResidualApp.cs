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
    public class RiesgoResidualApp : IRiesgoResidualApp
    {
        readonly IRiesgoResidual residual;
        public RiesgoResidualApp(IRiesgoResidual residual)
        {
            this.residual = residual;
        }
        public int ActualizarControlesRiesgoResidual(long pnNroRiesgo, int pnItem, string psComentario, int pnReponControl, int pnPeriEjec, int pnEvidenControl, int pnEjecControl, int pnCumpleObj, int pnEfecControl, string psUltimaActualizacion)
        {
            return residual.ActualizarControlesRiesgoResidual(pnNroRiesgo, pnItem, psComentario, pnReponControl, pnPeriEjec, pnEvidenControl, pnEjecControl, pnCumpleObj, pnEfecControl, psUltimaActualizacion);
        }

        public int EliminarControlRiesgoResidual(long pnNroRiesgo, int pnItem, string psUltimaActualizacion)
        {
            return residual.EliminarControlRiesgoResidual(pnNroRiesgo, pnItem, psUltimaActualizacion);
        }

        public List<RiesgoResidual> ObtenerControlRiesgoResidual(DetalleRiesgo riesgo)
        {
            return residual.ObtenerControlRiesgoResidual(riesgo);
        }

        public RiesgoResidual ObtenerCriteriosEvalControlRiesgoResidual(long pnNroRiesgo, int pnItem)
        {
            return residual.ObtenerCriteriosEvalControlRiesgoResidual(pnNroRiesgo, pnItem);
        }

        public EscalaNivelesRiesgo ObtenerEscalaNivelRiesgo(int pnProbabilidad, int pnImpacto)
        {
            return residual.ObtenerEscalaNivelRiesgo(pnProbabilidad, pnImpacto);
        }

        public EscalaNivelesRiesgo ObtenerNivelRiesgoResidualEscala(int pnEscala)
        {
            return residual.ObtenerNivelRiesgoResidualEscala(pnEscala);
        }

        public int RegistrarControlesRiesgoResidual(long pnNroRiesgo, string psComentario, int pnReponControl, int pnPeriEjec, int pnEvidenControl, int pnEjecControl, int pnCumpleObj, int pnEfecControl, string psUltimaActualizacion)
        {
            return residual.RegistrarControlesRiesgoResidual(pnNroRiesgo, psComentario, pnReponControl, pnPeriEjec, pnEvidenControl, pnEjecControl, pnCumpleObj, pnEfecControl, psUltimaActualizacion);
        }
    }
}
