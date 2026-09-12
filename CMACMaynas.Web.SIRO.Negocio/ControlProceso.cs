using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class ControlProceso
    {
        [JsonProperty(PropertyName = "oProceso", Order = 0)]
        public ProcesoArea oProceso { get; set; }

        [JsonProperty(PropertyName = "nCodControl", Order = 1)]
        public long nCodControl { get; set; }

        [JsonProperty(PropertyName = "cControlDescripcion", Order = 2)]
        public string cControlDescripcion { get; set; }

        [JsonProperty(PropertyName = "dFechaRegistro", Order = 3)]
        public DateTime dFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "dUltimaActualizacion", Order = 4)]
        public DateTime dUltimaActualizacion { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 5)]
        public bool bEstado { get; set; }

    }
}
