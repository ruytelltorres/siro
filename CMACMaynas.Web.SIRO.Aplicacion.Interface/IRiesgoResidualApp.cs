using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface IRiesgoResidualApp
    {
        List<RiesgoResidual> ObtenerControlRiesgoResidual(DetalleRiesgo riesgo);

        int RegistrarControlesRiesgoResidual(long pnNroRiesgo, string psComentario, int pnReponControl, int pnPeriEjec, int pnEvidenControl, int pnEjecControl,
                                                           int pnCumpleObj, int pnEfecControl, string psUltimaActualizacion);

        int ActualizarControlesRiesgoResidual(long pnNroRiesgo, int pnItem, string psComentario, int pnReponControl, int pnPeriEjec, int pnEvidenControl, int pnEjecControl,
                                                           int pnCumpleObj, int pnEfecControl, string psUltimaActualizacion);

        int EliminarControlRiesgoResidual(long pnNroRiesgo, int pnItem, string psUltimaActualizacion);

        RiesgoResidual ObtenerCriteriosEvalControlRiesgoResidual(long pnNroRiesgo, int pnItem);


        EscalaNivelesRiesgo ObtenerNivelRiesgoResidualEscala(int pnEscala);

        EscalaNivelesRiesgo ObtenerEscalaNivelRiesgo(int pnProbabilidad, int pnImpacto);
    }
}
