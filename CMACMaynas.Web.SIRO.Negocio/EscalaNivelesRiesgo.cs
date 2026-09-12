using Newtonsoft.Json;
using System;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class EscalaNivelesRiesgo
    {
        [JsonProperty(PropertyName = "nEscala", Order = 0)]
        public int nEscala { get; set; }

        [JsonProperty(PropertyName = "nProbabilidad", Order = 1)]
        public int nProbabilidad { get; set; }

        [JsonProperty(PropertyName = "cProbabilidad", Order = 2)]
        public string cProbabilidad { get; set; }

        [JsonProperty(PropertyName = "nImpacto", Order = 3)]
        public int nImpacto { get; set; }

        [JsonProperty(PropertyName = "cImpacto", Order = 4)]
        public string cImpacto { get; set; }

        [JsonProperty(PropertyName = "nValorEstala", Order = 5)]
        public int nValorEstala { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 6)]
        public bool bEstado { get; set; }

        [JsonProperty(PropertyName = "cFechaRegistro", Order = 7)]
        public DateTime cFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "cUltimactualizacion", Order = 8)]
        public DateTime cUltimactualizacion { get; set; }

        [JsonProperty(PropertyName = "nNivelRiesgo", Order = 9)]
        public int nNivelRiesgo { get; set; }

    }
}
