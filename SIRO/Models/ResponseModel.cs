using System.Collections.Generic;

namespace SIRO.Models
{
    public class ResponseModel
    {
        public List<object> oLstObjeto { get; set; }
        public object obj { get; set; }
        public int valor { get; set; }
        public string mensaje { get; set; }

        public int ValorNotif { get; set; }
        public string TipoNotif { get; set; }
        public string MensajeNotif { get; set; }
    }
}