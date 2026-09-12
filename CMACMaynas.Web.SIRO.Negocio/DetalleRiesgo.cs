using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CMACMaynas.Web.SIRO.Negocio
{
    /// <summary>
    /// Clase que representa la tabla DetalleRiesgo
    /// </summary>
    public class DetalleRiesgo : Riesgo
    {
        [JsonProperty(PropertyName = "cCodRiesgo", Order = 1)]
        public string cCodRiesgo { get; set; }

        /// <summary>
        /// Tipo de riesgo
        /// <constante:500>
        /// </summary>
        [JsonProperty(PropertyName = "oTipoRiesgo", Order = 3)]
        public Constante oTipoRiesgo { get; set; }

        [JsonProperty(PropertyName = "cRiesgoIdentiticado", Order = 2)]
        public string cRiesgoIdentiticado { get; set; }

        /// <summary>
        /// Proceso actual del riesgo
        /// <constante:1002>
        /// </summary>
        [JsonProperty(PropertyName = "oProcRiesgo", Order = 4)]
        public Constante oProcRiesgo { get; set; }

        /// <summary>
        /// Agencia afectada
        /// Obj. dependiente: Areas
        /// </summary>
        [JsonProperty(PropertyName = "oAgencia", Order = 7)]
        public Agencias oAgencia { get; set; }


        /// <summary>
        /// Estado del Riesgo
        /// <constante:1000>
        /// </summary>
        [JsonProperty(PropertyName = "oEstado", Order = 9)]
        public Constante oEstado { get; set; }

        [JsonProperty(PropertyName = "oCausas", Order = 10)]
        public CausaRiesgo oCausas { get; set; }

        /// <summary>
        /// Proceso del area
        /// Obj. dependiente: SubProcesos
        /// </summary>
        [JsonProperty(PropertyName = "oProceso", Order = 5)]
        public ProcesoArea oProceso { get; set; }


        [JsonProperty(PropertyName = "oTallerRiesgo", Order = 5)]
        public TallerRiesgo oTallerRiesgo { get; set; }
    }
}
