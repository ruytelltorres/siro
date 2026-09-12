using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public sealed class SubLineaNegocio
    {


        [JsonProperty(PropertyName = "oLineaNeg", Order = 0)]
        public LineaNegocio oLineaNeg { get; set; }

        [JsonProperty(PropertyName = "cCodSubLineaNeg", Order = 1)]
        public string cCodSubLineaNeg { get; set; }

        [JsonProperty(PropertyName = "cDescSubLineaNeg", Order = 2)]
        public string cDescSubLineaNeg { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 3)]
        public bool bEstado { get; set; }

        [JsonProperty(PropertyName = "dFechaRegistro", Order = 4)]
        public DateTime dFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "dUltimaActualizacion", Order = 5)]
        public DateTime dUltimaActualizacion { get; set; }



    }
}
