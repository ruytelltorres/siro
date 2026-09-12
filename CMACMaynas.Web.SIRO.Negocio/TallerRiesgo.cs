using Newtonsoft.Json;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class TallerRiesgo
    {

        [JsonProperty(PropertyName = "oAutoevaluacion", Order = 0)]
        public Autoevaluacion oAutoevaluacion { get; set; }

        [JsonProperty(PropertyName = "oTaller", Order = 1)]
        public Taller oTaller { get; set; }


        [JsonProperty(PropertyName = "oCondicionRiesgo", Order = 2)]
        public Constante oCondicionRiesgo { get; set; }



    }
}
