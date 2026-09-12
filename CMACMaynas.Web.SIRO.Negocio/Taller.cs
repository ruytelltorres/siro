using Newtonsoft.Json;
using System;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Taller
    {
        [JsonProperty(PropertyName = "cCodTaller", Order = 0)]
        public string cCodTaller { get; set; }

        [JsonProperty(PropertyName = "cCodTpoEval", Order = 1)]
        public string cCodTpoEval { get; set; }

        //[JsonProperty(PropertyName = "cDescTaller", Order = 1)]
        //public string cDescTaller { get; set; }

        ///// <summary>
        ///// Valor del estado del riesgo dentro del taller
        ///// </summary>
        //[JsonProperty(PropertyName = "nEstado", Order = 2)]
        //public int nEstado { get; set; }

        ///// <summary>
        ///// Descripcion del estado del riesgo dentro del taller
        ///// </summary>
        //[JsonProperty(PropertyName = "cEstado", Order = 3)]
        //public string cEstado { get; set; }

        /// <summary>
        /// Estado del taller
        /// </summary>
        [JsonProperty(PropertyName = "oEstado", Order = 3)]
        public Constante oEstado { get; set; }

        [JsonProperty(PropertyName = "cUltimaActualizacion", Order = 4)]
        public string cUltimaActualizacion { get; set; }

        [JsonProperty(PropertyName = "dFechaCreacion", Order = 5)]
        public DateTime dFechaCreacion { get; set; }

        //[JsonProperty(PropertyName = "dFechaCreacion", Order = 4)]
        //public DateTime dFechaCreacion { get; set; }

        //[JsonProperty(PropertyName = "oDatosRiesgo", Order = 5)]
        //public DatosRiesgos oDatosRiesgo { get; set; }

        //[JsonProperty(PropertyName = "oTpoEveluacion", Order = 6)]
        //public Constante oTpoEveluacion { get; set; }

        //[JsonProperty(PropertyName = "cTpoCodEval", Order = 7)]
        //public string cTpoCodEval { get; set; }

    }
}
