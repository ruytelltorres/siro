using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class ResponsablePlanAccion
    {
        [JsonProperty(PropertyName = "oDatosRiesgo", Order = 0)]
        public DatosRiesgos oDatosRiesgo { get; set; }

        [JsonProperty(PropertyName = "oPlanAccion", Order = 1)]
        public PlanAccion oPlanAccion { get; set; }

        [JsonProperty(PropertyName = "oPersona", Order = 2)]
        public Persona oPersona { get; set; }

        [JsonProperty(PropertyName = "nItemResponPlan", Order = 3)]
        public int nItemResponPlan{ get; set; }

        [JsonProperty(PropertyName = "cUserResponsable", Order = 4)]
        public string cUserResponsable { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 5)]
        public bool bEstado { get; set; }

        [JsonProperty(PropertyName = "cConfrimados", Order = 6)]
        public string cConfrimados { get; set; }

        [JsonProperty(PropertyName = "cUserConfrimados", Order = 7)]
        public string cUserConfrimados { get; set; }
        
        [JsonProperty(PropertyName = "dFechaRegistro", Order = 8)]
        public DateTime dFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "dFechaUltimaActualizacion", Order = 9)]
        public DateTime dFechaUltimaActualizacion { get; set; }

        
    }
}
