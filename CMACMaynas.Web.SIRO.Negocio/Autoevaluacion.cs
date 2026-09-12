using Newtonsoft.Json;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Autoevaluacion : DetalleRiesgo
    {
        [JsonProperty(PropertyName = "oTipoEvaluacion", Order = 1)]
        public Constante oTipoEvaluacion { get; set; }
        
        [JsonProperty(PropertyName = "oTaller", Order = 2)]
        public Taller oTaller { get; set; }
    }
}
