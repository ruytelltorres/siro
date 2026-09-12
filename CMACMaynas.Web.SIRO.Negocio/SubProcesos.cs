using Newtonsoft.Json;
using System;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class SubProcesos
    {
        [JsonProperty(PropertyName = "nCodSubProceso", Order = 0)]
        public int nCodSubProceso { get; set; }

        [JsonProperty(PropertyName = "cDescSubProceso", Order = 1)]
        public string cDescSubProceso { get; set; }

        [JsonProperty(PropertyName = "cAbreviatura", Order = 2)]
        public string cAbreviatura { get; set; }

        [JsonProperty(PropertyName = "dUltimaActualizacion", Order = 3)]
        public DateTime dUltimaActualizacion { get; set; }

        [JsonProperty(PropertyName = "nEstadoSubProceso", Order = 4)]
        public int nEstadoSubProceso { get; set; }
        
    }
}
