using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class RiesgoResidual
    {

        [JsonProperty(PropertyName = "nItem", Order = 1)]
        public int nItem { get; set; }


        [JsonProperty(PropertyName = "cComentario", Order = 3)]
        public string cComentario { get; set; }

        public List<Item<string>> oResposableDefinido { get; set; }

        public List<Item<string>> oPeriodoEfecucion { get; set; }

        public List<Item<string>> oEvidenciaControl { get; set; }
        
        public List<Item<string>> oEjecucionControl { get; set; }

        public List<Item<string>> oCumplimientoObjectivo { get; set; }

        public List<Item<string>> oEfectividadControl { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 16)]
        public bool bEstado { get; set; }

        [JsonProperty(PropertyName = "dFechaReg", Order = 17)]
        public DateTime dFechaReg { get; set; }

        [JsonProperty(PropertyName = "dFechaActualizacion", Order = 18)]
        public DateTime dFechaActualizacion { get; set; }

    }
}
