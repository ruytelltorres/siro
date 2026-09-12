using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Usuario
    {
        public Persona oPersona { get; set; }
        public Perfil oPerfil { get; set; }

        [JsonProperty(PropertyName = "cUser", Order = 0)]
        public string cUser { get; set; }

        [JsonProperty(PropertyName = "cUsuario", Order = 1)]
        public string cUsuario { get; set; }

        [JsonProperty(PropertyName = "cRHCargoCod", Order = 2)]
        public string cRHCargoCod { get; set; }

        [JsonProperty(PropertyName = "cRHCargoDescripcion", Order = 3)]
        public string cRHCargoDescripcion { get; set; }

        public Areas oAreas { get; set; }

        public Agencias oAgencia { get; set; }

        [JsonProperty(PropertyName = "GruposUsuario", Order = 4)]
        public string[] GruposUsuario { get; set; }
    }
}
