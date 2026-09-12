using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class RiesgoOperacional : DetalleRiesgo
    {
        /// <summary>
        /// Type: List<dynamic>
        /// Lista de controles
        /// </summary>
        [JsonProperty(PropertyName = "lstControles", Order = 11)]
        public List<dynamic> lstControles { get; set; }

        [JsonProperty(PropertyName = "dFechaDeteccion", Order = 0)]
        public DateTime dFechaDeteccion { get; set; }

        [JsonProperty(PropertyName = "bControlEfectivo", Order = 0)]
        public bool bControlEfectivo { get; set; }

        [JsonProperty(PropertyName = "cEfectosRiesgo", Order = 0)]
        public string cEfectosRiesgo { get; set; }





        //[JsonProperty(PropertyName = "oAreas", Order = 1)]
        //public Areas oAreas { get; set; }

        //[JsonProperty(PropertyName = "cObservacion", Order = 1)]
        //public string cObservacion { get; set; }

        //[JsonProperty(PropertyName = "dFechaDeteccion", Order = 2)]
        //public DateTime dFechaDeteccion { get; set; }

        //[JsonProperty(PropertyName = "bControlEfectivo", Order = 3)]
        //public bool bControlEfectivo { get; set; }

        //[JsonProperty(PropertyName = "oControlProceso", Order = 4)]
        //public ControlProceso oControlProceso { get; set; }




    }
}
