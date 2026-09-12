using Newtonsoft.Json;
using System.Collections.Generic;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Menu
    {
        [JsonProperty(PropertyName = "cMenuId", Order = 0)]
        public string cMenuId { get; set; }

        [JsonProperty(PropertyName = "cMenuPadre", Order = 1)]
        public string cMenuPadre { get; set; }

        [JsonProperty(PropertyName = "cTitulo", Order = 2)]
        public string cTitulo { get; set; }

        [JsonProperty(PropertyName = "cDescripcion", Order = 3)]
        public string cDescripcion { get; set; }

        [JsonProperty(PropertyName = "cUrl", Order = 4)]
        public string cUrl { get; set; }

        [JsonProperty(PropertyName = "cIcono", Order = 5)]
        public string cIcono { get; set; }

        [JsonProperty(PropertyName = "nPosicion", Order = 6)]
        public int nPosicion { get; set; }

        [JsonProperty(PropertyName = "bEstado", Order = 7)]
        public bool bEstado { get; set; }

        [JsonProperty(PropertyName = "nNivel", Order = 8)]
        public int nNivel { get; set; }

        [JsonProperty(PropertyName = "oListaMenu", Order = 9)]
        public List<Menu> oListaMenu { get; set; }

        [JsonProperty(PropertyName = "oUsuario", Order = 10)]
        public Usuario oUsuario { get; set; }

        [JsonProperty(PropertyName = "cDescUrl", Order = 11)]
        public string  cDescUrl { get; set; }


    }



}
