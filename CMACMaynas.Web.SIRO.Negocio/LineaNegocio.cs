using Newtonsoft.Json;
using System;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class LineaNegocio
    {
        
        [JsonProperty(PropertyName = "cCodLineaNeg", Order = 0)]
        public string cCodLineaNeg { get; set; }

        [JsonProperty(PropertyName = "cDescLineaNeg", Order = 1)]
        public string cDescLineaNeg{ get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 2)]
        public bool bEstado { get; set; }

        [JsonProperty(PropertyName = "dFechaRegistro", Order = 3)]
        public DateTime dFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "dUltimaActualizacion", Order = 4)]
        public DateTime dUltimaActualizacion { get; set; }

        [JsonProperty(PropertyName = "oSubLineaneg", Order = 5)]
        public SubLineaNegocio oSubLineaNeg { get; set; }
    }
}
