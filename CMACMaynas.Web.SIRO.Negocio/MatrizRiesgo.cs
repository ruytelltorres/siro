using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class MatrizRiesgo
    {
        [JsonProperty(PropertyName = "oDatosRiesgo", Order = 0)]
        public DatosRiesgos oDatosRiesgo { get; set; }

        [JsonProperty(PropertyName = "oPlanAccion", Order = 1)]
        public PlanAccion oPlanAccion { get; set; }

        //[JsonProperty(PropertyName = "cFactorRiesgo", Order = 2)]
        //public string cFactorRiesgo { get; set; }

        [JsonProperty(PropertyName = "oFactorRiesgo", Order = 2)]
        public Constante oFactorRiesgo { get; set; }

        //[JsonProperty(PropertyName = "cEventoPerdida", Order = 3)]
        //public string cEventoPerdida { get; set; }

        [JsonProperty(PropertyName = "oEventoPerdida", Order = 3)]
        public Constante oEventoPerdida { get; set; }
        
        [JsonProperty(PropertyName = "oLineaNegocio", Order = 4)]
        public LineaNegocio oLineaNegocio  { get; set; }

        [JsonProperty(PropertyName = "oProducto", Order = 5)]
        public Producto oProducto  { get; set; }

        [JsonProperty(PropertyName = "oSubProducto", Order = 6)]
        public SubProducto oSubProducto { get; set; }

        //[JsonProperty(PropertyName = "cProbabilidadInherente", Order = 7)]
        //public string cProbabilidadInherente  { get; set; }
        [JsonProperty(PropertyName = "oProbabilidadInherente", Order = 7)]
        public Constante oProbabilidadInherente { get; set; }

        //[JsonProperty(PropertyName = "cImpactoInherente", Order = 8)]
        //public string cImpactoInherente { get; set; }

        [JsonProperty(PropertyName = "oImpactoInherente", Order = 8)]
        public Constante oImpactoInherente { get; set; }

        [JsonProperty(PropertyName = "cNivelRiesgoInherente", Order = 9)]
        public string cNivelRiesgoInherente { get; set; }

        [JsonProperty(PropertyName = "oMontoPerdida", Order = 10)]
        public MontoPerdida oMontoPerdida { get; set; }





    }
}
