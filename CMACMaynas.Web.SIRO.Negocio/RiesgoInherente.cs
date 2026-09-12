using Newtonsoft.Json;
using System;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class RiesgoInherente
    {
        [JsonProperty(PropertyName = "oDatosRiesgo", Order = 0)]
        public DatosRiesgos oDatosRiesgo { get; set; }

        [JsonProperty(PropertyName = "nProbabilidad", Order = 1)]
        public int nProbabilidad { get; set; }

        [JsonProperty(PropertyName = "nImpacto", Order = 2)]
        public int nImpacto { get; set; }

        [JsonProperty(PropertyName = "nValorEscala", Order = 3)]
        public int nValorEscala { get; set; }

        [JsonProperty(PropertyName = "dFechaRegistro", Order = 4)]
        public DateTime dFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "dFechaUltimaActualizacion", Order = 5)]
        public DateTime dFechaUltimaActualizacion { get; set; }

    }
}
