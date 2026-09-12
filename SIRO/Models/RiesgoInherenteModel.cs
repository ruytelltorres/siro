using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace SIRO.Models
{
    public class RiesgoInherenteModel
    {
        public RiesgoInherente oRiesgoInherente { get; set; }
        public MontoPerdida oMontoPerdida { get; set; }
        public List<MontoPerdida> oLstMontoPerdida { get; set; }
    }
}