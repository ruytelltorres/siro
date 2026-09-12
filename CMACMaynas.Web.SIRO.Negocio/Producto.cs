using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Producto
    {
        public LineaNegocio oLineaNeg { get; set; }
        public string cCodProducto { get; set; }
        public string cDescProducto { get; set; }
        public bool bEstado { get; set; }
        public DateTime dFechaRegistro { get; set; }
        public DateTime dFechaActualizacion { get; set; }

    }
}
