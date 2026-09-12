using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class SubProducto
    {
        [JsonProperty(PropertyName = "oProducto", Order = 0)]
        public Producto oProducto { get; set; }

        [JsonProperty(PropertyName = "cCodSubProducto", Order = 0)]
        public string cCodSubProducto { get; set; }

        [JsonProperty(PropertyName = "cDescSubProducto", Order = 0)]
        public string cDescSubProducto { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 0)]
        public bool bEstado { get; set; }

        [JsonProperty(PropertyName = "dFechaRegistro", Order = 0)]
        public DateTime dFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "dFechaActualizacion", Order = 0)]
        public DateTime dFechaActualizacion { get; set; }
    }
}
