using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class PlanAccion
    {
        [JsonProperty(PropertyName = "oDatosRiesgo", Order = 0)]
        public DatosRiesgos oDatosRiesgo { get; set; }

        [JsonProperty(PropertyName = "nPlanCod", Order = 1)]
        public long nPlanCod { get; set; }

        [JsonProperty(PropertyName = "cPlanCod", Order = 2)]
        public string cPlanCod { get; set; }

        [JsonProperty(PropertyName = "cPlanDescripcion", Order = 3)]
        public string cPlanDescripcion { get; set; }

        [JsonProperty(PropertyName = "dFechaImplement", Order = 4)]
        public DateTime dFechaImplement { get; set; }

        [JsonProperty(PropertyName = "bSugerenciaGM", Order = 5)]
        public bool bSugerenciaGM { get; set; }

        [JsonProperty(PropertyName = "nEstado", Order = 6)]
        public int nEstado { get; set; }

        [JsonProperty(PropertyName = "cEstado", Order = 7)]
        public string cEstado { get; set; }

        [JsonProperty(PropertyName = "cComentario", Order = 8)]
        public string cComentario { get; set; }

        [JsonProperty(PropertyName = "dFechaRegistro", Order = 9)]
        public DateTime dFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "dFechaUltimaActualizacion", Order = 10)]
        public DateTime dFechaUltimaActualizacion { get; set; }

        [JsonProperty(PropertyName = "oReponsablePlanAccion", Order = 11)]
        public ResponsablePlanAccion oReponsablePlanAccion { get; set; }

        [JsonProperty(PropertyName = "cNombreDocBD", Order = 12)]
        public string cNombreDocBD { get; set; }

        [JsonProperty(PropertyName = "cNombreDoc", Order = 13)]
        public string cNombreDoc { get; set; }

        [JsonProperty(PropertyName = "cIconoEstado", Order = 14)]
        public string cIconoEstado { get; set; }




    }

    public class Comentarios {
        [JsonProperty(PropertyName = "oPlanAccion", Order = 0)]
        public PlanAccion oPlanAccion { get; set; }

        [JsonProperty(PropertyName = "nItem", Order = 1)]
        public int nItem { get; set; }

        [JsonProperty(PropertyName = "cUsuario", Order = 2)]
        public string cUsuario { get; set; }

        [JsonProperty(PropertyName = "cComentario", Order = 2)]
        public string cComentario { get; set; }

        [JsonProperty(PropertyName = "dFecha", Order = 2)]
        public DateTime dFecha { get; set; }

    }
}
