using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Agencias
    {
        [JsonProperty(PropertyName = "cAgeCod", Order = 0)]
        public string cAgeCod { get; set; }

        [JsonProperty(PropertyName = "cAgeDescripcion", Order = 1)]
        public string cAgeDescripcion { get; set; }

        [JsonProperty(PropertyName = "nAgeEstado", Order = 2)]
        public int nAgeEstado { get; set; }

        [JsonProperty(PropertyName = "oAreas", Order = 3)]
        public Areas oArea { get; set; }
    }
}
