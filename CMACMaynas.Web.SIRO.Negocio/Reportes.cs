using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Reportes
    {
        [JsonProperty(PropertyName = "nTpoRiesgo", Order = 0)]
        public string nTpoRiesgo { get; set;}

        [JsonProperty(PropertyName = "cTpoRiesgo", Order = 0)]
        public string cTpoRiesgo { get; set; }

        [JsonProperty(PropertyName = "nTotalRiesgo", Order = 0)]
        public string nTotalRiesgo { get; set; }

        [JsonProperty(PropertyName = "cEstadoRiesgo", Order = 0)]
        //public IEnumerable<string> cEstadoRiesgo { get; set; }
        public List<string> cEstadoRiesgo { get; set; }

        [JsonProperty(PropertyName = "nEstadoRiesgo", Order = 0)]
        //public IEnumerable<string> nEstadoRiesgo { get; set; }
        public List<string> nEstadoRiesgo { get; set; }


        //public IEnumerable<string> CamReporGraf { get; set; }

        
    }
}
