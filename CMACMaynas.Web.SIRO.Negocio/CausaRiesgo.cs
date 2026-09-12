using Newtonsoft.Json;
using System;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class CausaRiesgo
    {
        [JsonProperty(PropertyName = "cCodCausa", Order = 0)]
        public string cCodCausa { get; set; }

        [JsonProperty(PropertyName = "nItem", Order = 1)]
        public int nItem { get; set; }

        [JsonProperty(PropertyName = "cCausaDesc", Order = 2)]
        public string cCausaDesc { get; set; }

        [JsonProperty(PropertyName = "dFechaReg", Order = 3)]
        public DateTime dFechaReg { get; set; }

        [JsonProperty(PropertyName = "dFechaActualizacion", Order = 4)]
        public DateTime dFechaActualizacion { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 5)]
        public Boolean bEstado { get; set; }

        [JsonProperty(PropertyName = "oDatosRiesgo", Order = 6)]
        public DatosRiesgos oDatosRiesgo { get; set; }
    }
}
