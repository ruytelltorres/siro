using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class MontoPerdida
    {
        [JsonProperty(PropertyName = "nMontPerdCod", Order = 0)]
        public int nMontPerdCod { get; set; }

        [JsonProperty(PropertyName = "nProbabilidad", Order = 1)]
        public int nProbabilidad { get; set; }

        [JsonProperty(PropertyName = "nImpacto", Order = 2)]
        public int nImpacto { get; set; }

        [JsonProperty(PropertyName = "nMontoPerdida", Order = 3)]
        public decimal nMontoPerdida { get; set; }

        [JsonProperty(PropertyName = "cMontoPerdida", Order = 4)]
        public string cMontoPerdida { get; set; }

        [JsonProperty(PropertyName = "cComentario", Order = 5)]
        public string cComentario { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 6)]
        public bool bEstado { get; set; }

        [JsonProperty(PropertyName = "dFechaRegistro", Order = 7)]
        public DateTime dFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "dFechaCese", Order = 8)]
        public DateTime dFechaCese { get; set; }

        [JsonProperty(PropertyName = "dUltimaActualizacion", Order = 9)]
        public DateTime dUltimaActualizacion { get; set; }

    }
}
