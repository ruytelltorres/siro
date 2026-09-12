using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Incentivos
    {
        [JsonProperty(PropertyName = "nCodConfigIncentivo", Order = 0)]
        public int nCodConfigIncentivo { get; set; }

        [JsonProperty(PropertyName = "nMontoPropuesto", Order = 1)]
        public decimal nMontoPropuesto { get; set; }

        [JsonProperty(PropertyName = "nMontoIncentivo", Order = 2)]
        public decimal nMontoIncentivo { get; set; }

        [JsonProperty(PropertyName = "oDatosRiesgo", Order = 3)]
        public DatosRiesgos oDatosRiesgo { get; set; }
        

        [JsonProperty(PropertyName = "oProbabilidadInherente", Order = 4)]
        public Constante oProbabilidadInherente { get; set; }

        [JsonProperty(PropertyName = "oImpactoInherente", Order = 5)]
        public Constante oImpactoInherente { get; set; }

        [JsonProperty(PropertyName = "oNivelRiesgoInherente", Order = 6)]
        public Constante oNivelRiesgoInherente { get; set; }

        //[JsonProperty(PropertyName = "oNivelRiesgoResidual", Order = 10)]
        //public ConstanteEN oNivelRiesgoResidual { get; set; }
        
        [JsonProperty(PropertyName = "oMontoPerdida", Order = 7)]
        public MontoPerdida oMontoPerdida { get; set; }

        [JsonProperty(PropertyName = "bIncentivo", Order = 8)]
        public bool bIncentivo { get; set; }

        [JsonProperty(PropertyName = "cComentarioIncentivo", Order = 9)]
        public string cComentarioIncentivo { get; set; }

        //[JsonProperty(PropertyName = "nMontoIncentivo", Order = 14)]
        //public decimal nMontoIncentivo { get; set; }

        [JsonProperty(PropertyName = "cNombreDocIncentivo", Order = 10)]
        public string cNombreDocIncentivo { get; set; }

        [JsonProperty(PropertyName = "cNombreDocIncentivoDB", Order = 11)]
        public string cNombreDocIncentivoDB { get; set; }

        [JsonProperty(PropertyName = "bEstadoIncentivo", Order = 12)]
        public bool bEstadoIncentivo{ get; set; }

        [JsonProperty(PropertyName = "dFechaGestion", Order = 13)]
        public DateTime dFechaGestion { get; set; }
        
        [JsonProperty(PropertyName = "dFechaActualizacion", Order = 14)]
        public DateTime dFechaActualizacion { get; set; }


        [JsonProperty(PropertyName = "oTipoIncentivo", Order = 15)]
        public Constante oTipoIncentivo { get; set; }






    }
}
