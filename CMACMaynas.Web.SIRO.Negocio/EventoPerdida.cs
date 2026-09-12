using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class EventoPerdida
    {
        [JsonProperty(PropertyName = "oDatosRiesgo", Order = 0)]
        public DatosRiesgos oDatosRiesgo { get; set; }

        [JsonProperty(PropertyName = "cGrupoEvento", Order = 1)]
        public string cGrupoEvento { get; set; }

        [JsonProperty(PropertyName = "cMedidasCorrectivas", Order = 2)]
        public string cMedidasCorrectivas { get; set; }
        
        [JsonProperty(PropertyName = "cAccionesRealizada", Order = 3)]
        public string cAccionesRealizada { get; set; }
        
        [JsonProperty(PropertyName = "bReportado", Order = 4)]
        public bool bReportado { get; set; }

        [JsonProperty(PropertyName = "oCobertura", Order = 5)]
        public Constante oCobertura { get; set; }

        [JsonProperty(PropertyName = "dFechaRegCont", Order = 6)]
        public DateTime dFechaRegCont { get; set; }

        [JsonProperty(PropertyName = "dFechaOcurrencia", Order = 7)]
        public DateTime dFechaOcurrencia { get; set; }

        [JsonProperty(PropertyName = "dFechaDescubrimiento", Order = 8)]
        public DateTime dFechaDescubrimiento { get; set; }
        
        [JsonProperty(PropertyName = "cPenMontoPerdida", Order = 9)]
        public string cPenMontoPerdida { get; set; }

        [JsonProperty(PropertyName = "nMontoPerdida", Order = 10)]
        public double nMontoPerdida { get; set; }

        [JsonProperty(PropertyName = "oListaGastos", Order = 11)]
        public List<dynamic> oListaGastos { get; set; }
        
        [JsonProperty(PropertyName = "cPenMontoRecup", Order = 12)]
        public string cPenMontoRecup { get; set; }

        [JsonProperty(PropertyName = "nMontoRecupe", Order = 13)]
        public double nMontoRecup { get; set; }
        
        [JsonProperty(PropertyName = "nMontoBruto", Order = 14)]
        public double nMontoBruto { get; set; }
        
        [JsonProperty(PropertyName = "cPenMontoProvision", Order = 15)]
        public string cPenMontoProvision { get; set; }

        [JsonProperty(PropertyName = "nMontoProvision", Order = 16)]
        public double nMontoProvision { get; set; }
        
        [JsonProperty(PropertyName = "nPerdidaNeta", Order = 17)]
        public double nPerdidaNeta { get; set; }

        [JsonProperty(PropertyName = "cCtaContCod", Order = 18)]
        public string cCtaContCod { get; set; }

        [JsonProperty(PropertyName = "cCtaContDesc", Order = 19)]
        public string cCtaContDesc { get; set; }

        [JsonProperty(PropertyName = "bAsociaRiesgo", Order = 20)]
        public bool bAsociaRiesgo { get; set; }

        [JsonProperty(PropertyName = "cAnio", Order = 21)]
        public string cAnio { get; set; }

        [JsonProperty(PropertyName = "nItemGrupo", Order = 22)]
        public int nItemGrupo { get; set; }

        [JsonProperty(PropertyName = "oLineaNeg", Order = 23)]
        public LineaNegocio oLineaNeg { get; set; }

        [JsonProperty(PropertyName = "oClasesEventoPerdida", Order = 24)]
        public ClaseEventoPerdida oClasesEventoPerdida { get; set; }

        [JsonProperty(PropertyName = "oDescCortaEventoPerdida", Order = 25)]
        public Constante oDescCortaEventoPerdida { get; set; }

        [JsonProperty(PropertyName = "bGrupo", Order = 26)]
        public bool bGrupo { get; set; }
        
        [JsonProperty(PropertyName = "dFechaRegistro", Order = 27)]
        public DateTime dFechaRegistro { get; set; }

        [JsonProperty(PropertyName = "cUserRegistra", Order = 28)]
        public string cUserRegistra { get; set; }

    }

    public class ClaseEventoPerdida {
        [JsonProperty(PropertyName = "nCodClasEventoPerdida", Order = 0)]
        public int nCodClasEvento { get; set; }

        [JsonProperty(PropertyName = "cDescClasEvento", Order = 1)]
        public string cDescClasEvento { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 2)]
        public bool bEstado { get; set; }
        
        [JsonProperty(PropertyName = "oSubClaseEventoPerdida", Order = 3)]
        public SubClaseEventoPerdida oSubClaseEventoPerdida { get; set; }

    }

    public class SubClaseEventoPerdida
    {
        [JsonProperty(PropertyName = "nCodSubClasEvento", Order = 0)]
        public int nCodSubClasEvento { get; set; }

        [JsonProperty(PropertyName = "nCodClasEvento", Order = 1)]
        public int nCodClasEvento { get; set; }

        [JsonProperty(PropertyName = "cDescSubClasEvento", Order = 2)]
        public string cDescSubClasEvento { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 3)]
        public bool bEstado { get; set; }

    }


}
