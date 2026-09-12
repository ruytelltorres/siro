using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIRO.Models
{
    public class ArchivoAdjunto
    {
        public long lnCodProcesoGestion { get; set; }
        public string lsNombreArchivo { get; set; }
        public string lsExtension { get; set; }
        public string lsNombreArchivoDB { get; set; }
    }
}