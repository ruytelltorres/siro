using Newtonsoft.Json;
using System;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class CriteriosEvaluacion
    {

        [JsonProperty(PropertyName = "nCriterioCod", Order = 0)]
        public int nCriterioCod { get; set; }

        [JsonProperty(PropertyName = "nCriterioValor", Order = 1)]
        public int nCriterioValor { get; set; }

        [JsonProperty(PropertyName = "cCriterioDesc", Order = 2)]
        public string cCriterioDesc { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 3)]
        public bool bEstado { get; set; }

        [JsonProperty(PropertyName = "dFechaRegistro", Order = 4)]
        public DateTime dFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "dFechaActualizacion", Order = 5)]
        public DateTime dFechaActualizacion { get; set; }
        
        [JsonProperty(PropertyName = "nValorRelacion", Order = 6)]
        public int nValorRelacion { get; set; }


    }
}
