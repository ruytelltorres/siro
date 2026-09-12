using Newtonsoft.Json;
using System;

namespace CMACMaynas.Web.SIRO.Negocio
{
    /// <summary>
    /// Clase que representa la información de la tabla maestra de Riesgo
    /// </summary>
    public class Riesgo
    {
        [JsonProperty(PropertyName = "nNroRiesgo", Order = 0)]
        public long nNroRiesgo { get; set; }

        [JsonProperty(PropertyName = "cNroRiesgo", Order = 1)]
        public string cNroRiesgo { get; set; }

        [JsonProperty(PropertyName = "cDescRiesgo", Order = 2)]
        public string cDescRiesgo { get; set; }

        [JsonProperty(PropertyName = "nEstado", Order = 3)]
        public int nEstado { get; set; }

        /// <summary>
        /// Estado de bandera del riesgo
        /// </summary>
        [JsonProperty(PropertyName = "nFlag", Order = 4)]
        public int nFlag { get; set; }


        [JsonProperty(PropertyName = "oUsuario", Order = 2)]
        public Usuario oUsuario { get; set; }

        [JsonProperty(PropertyName = "dFechaRegistro", Order = 5)]
        public DateTime dFechaRegistro { get; set; }

    }
}
