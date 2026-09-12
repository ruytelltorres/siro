using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SIRO.Models
{
    public class ErrorModel
    {
        public string Titulo { get; set; }
        public HttpStatusCode CodigoError { get; set; }
        public string MensajeError { get; set; }
        public string IconoError { get; set; }
        
    }
}