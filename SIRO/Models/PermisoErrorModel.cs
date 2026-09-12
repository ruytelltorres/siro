using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIRO.Models
{
    public class PermisoErrorModel
    {
        public string Vista = "_PermisoError";
        public string Boton = "";
        public string Url = "";
        public string Icono = "entypo-attention";
        public string TituloError = "";
        //public string DescError = "";
        public string TituloPag = "Error";
        public bool isLink = true;
        public List<string> Errores = new List<string>();
        public List<string> Consejos = new List<string>();
    }
}