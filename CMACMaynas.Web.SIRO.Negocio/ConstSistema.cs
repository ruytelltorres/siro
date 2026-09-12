using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class ConstSistema
    {
        [JsonProperty(PropertyName = "nConsSisCod", Order = 0)]
        public int nConsSisCod { get; set; }

        [JsonProperty(PropertyName = "cConsSisDesc", Order = 2)]
        public string cConsSisDesc { get; set; }

        [JsonProperty(PropertyName = "cConsSisValor", Order = 1)]
        public string cConsSisValor { get; set; }

        [JsonProperty(PropertyName = "dFechaActualizacion", Order = 3)]
        public DateTime dFechaActualizacion { get; set; }
    }
}
