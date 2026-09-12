using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace SIRO.Models
{
    public class ConfiguracionModel
    {
        public List<Areas> LstAreas { get; set; }

        public List<ProcesoArea> LstProcesosAreas { get; set; }
        public List<ControlProceso> LstControlesAreas { get; set; }
        public List<CausaRiesgo> LstCausasRiesgos { get; set; }
        public List<CriteriosEvaluacion> LstCriteriosEval { get; set; }
        public List<Constante> LstProbabilidad { get; set; }
        public List<Constante> LstImpacto { get; set; }
        public List<Incentivos> LstConfigIncentivos { get; set; }

    }
}