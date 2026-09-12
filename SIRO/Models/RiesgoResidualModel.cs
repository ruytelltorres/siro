using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace SIRO.Models
{
    public class RiesgoResidualModel
    {
        public RiesgoResidual oRiesgoResidualEN { get; set; }
        public List<RiesgoResidual> oLstRiesgoResidual { get; set; }

        public EscalaNivelesRiesgo oEscalaNivRiesgo { get; set; }
        public int CalifEfecControl { get; set; }

    }
}