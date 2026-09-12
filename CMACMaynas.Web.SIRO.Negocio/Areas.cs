using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Areas
    {
        [JsonProperty(PropertyName = "cAreaCod", Order = 0)]
        public string cAreaCod { get; set; }

        [JsonProperty(PropertyName = "cAreaDescripcion", Order = 1)]
        public string cAreaDescripcion { get; set; }

        [JsonProperty(PropertyName = "cAreaResumen", Order = 2)]
        public string cAreaResumen { get; set; }

        [JsonProperty(PropertyName = "cUltimaActualizacion", Order = 3)]
        public DateTime cUltimaActualizacion { get; set; }
    }
}
