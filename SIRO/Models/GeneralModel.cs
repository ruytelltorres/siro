using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace SIRO.Models
{
    public class GeneralModel
    {
      
        public List<Agencias> oLstAgencia { get; set; }
        public List<Areas> oLstAreas { get; set; }
        public List<ProcesoArea> oLstProcesoArea { get; set; }
        public List<SubProcesos> oLstSubProcesoArea { get; set; }
        public List<ControlProceso> oLstControlesAreas { get; set; }
        public List<CausaRiesgo> oLstCausaRiesgo { get; set; }
        public List<Constante> oLstConstante { get; set; }
        public List<Usuario> oLstUsuarios { get; set; }

    }
}