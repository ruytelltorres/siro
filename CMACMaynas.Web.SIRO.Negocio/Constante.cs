using Newtonsoft.Json;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Constante
    {
        [JsonProperty(PropertyName = "nConsCod", Order = 0)]
        public int nConsCod { get; set; }

        [JsonProperty(PropertyName = "nConsValor", Order = 1)]
        public int nConsValor { get; set; }

        [JsonProperty(PropertyName = "cConsDescripcion", Order = 2)]
        public string cConsDescripcion { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 3)]
        public bool bEstado { get; set; }

        /// <summary>
        /// Este valor solo sera cargado pra enviar datos en reportes
        /// </summary>
        [JsonProperty(PropertyName = "nCantidad", Order = 4)]
        public int nCantidad { get; set; }




    }
}
