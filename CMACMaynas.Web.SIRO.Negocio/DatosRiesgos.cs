using Newtonsoft.Json;
using System;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class DatosRiesgos
    {

        [JsonProperty(PropertyName = "nNroRiesgo", Order = 0)]
        public long nNroRiesgo { get; set; }

        [JsonProperty(PropertyName = "cCodRiesgo", Order = 3)]
        public string cCodRiesgo { get; set; }
        
        [JsonProperty(PropertyName = "nTpoRiesgo", Order = 5)]
        public int nTpoRiesgo { get; set; }

        [JsonProperty(PropertyName = "cRiesgoIdentiticado", Order = 6)]
        public string cRiesgoIdentiticado { get; set; }

        [JsonProperty(PropertyName = "dFechaDeteccion", Order = 7)]
        public DateTime dFechaDeteccion { get; set; }

        [JsonProperty(PropertyName = "cEstadoRiesgo", Order = 9)]
        public string cEstadoRiesgo { get; set; }

        [JsonProperty(PropertyName = "dFechaReg", Order = 12)]
        public DateTime dFechaReg { get; set; }

        [JsonProperty(PropertyName = "oUsuarios", Order = 14)]
        public Usuario oUsuarios { get; set; }

        [JsonProperty(PropertyName = "oAgencias", Order = 15)]
        public Agencias oAgencias { get; set; }

        [JsonProperty(PropertyName = "oAreas", Order = 16)]
        public Areas oAreas { get; set; }

        [JsonProperty(PropertyName = "oProceso", Order = 17)]
        public ProcesoArea oProceso { get; set; }




    








    }
}
