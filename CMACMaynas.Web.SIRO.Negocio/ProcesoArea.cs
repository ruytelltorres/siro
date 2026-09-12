using Newtonsoft.Json;
using System;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class ProcesoArea
    {
        [JsonProperty(PropertyName = "oAreas", Order = 0)]
        public Areas oAreas { get; set; }

        [JsonProperty(PropertyName = "cCodProceso", Order = 1)]
        public string cCodProceso { get; set; }

        [JsonProperty(PropertyName = "cDescProceso", Order = 2)]
        public string cDescProceso { get; set;}

        [JsonProperty(PropertyName = "dFechaRegistro", Order = 3)]
        public DateTime dFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "dUltimaActualizacion", Order = 4)]
        public DateTime dUltimaActualizacion { get; set; }

        [JsonProperty(PropertyName = "bProcesoEstado", Order = 5)]
        public bool bProcesoEstado { get; set; }

        [JsonProperty(PropertyName = "oSubProceso", Order = 6)]
        public SubProcesos oSubProceso { get; set; }
    }
}
