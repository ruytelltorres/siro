using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Persona
    {
        [JsonProperty(PropertyName = "cPersCod", Order = 0)]
        public string cPersCod { get; set; }

        [JsonProperty(PropertyName = "cPersNombre", Order = 1)]
        public string cPersNombre { get; set; }

        [JsonProperty(PropertyName = "cPersDireccDomicilio", Order = 2)]
        public string cPersDireccDomicilio { get; set; }

        [JsonProperty(PropertyName = "cPersSexo", Order = 3)]
        public string cPersSexo { get; set; }

        [JsonProperty(PropertyName = "cNacionalidad", Order = 4)]
        public string cNacionalidad { get; set; }

        [JsonProperty(PropertyName = "nResidente", Order = 5)]
        public int nResidente { get; set; }

        [JsonProperty(PropertyName = "nPersPersoneria", Order = 6)]
        public int nPersPersoneria { get; set; }

        [JsonProperty(PropertyName = "dFecNacCreacion", Order = 7)]
        public DateTime dFecNacCreacion { get; set; }

        [JsonProperty(PropertyName = "nIngPromedio", Order = 8)]
        public decimal nIngPromedio { get; set; }

        [JsonProperty(PropertyName = "cTelefono", Order = 9)]
        public string cTelefono { get; set; }

        [JsonProperty(PropertyName = "cCorreo", Order = 10)]
        public string cCorreo { get; set; }
        

    }
}
