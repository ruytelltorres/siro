using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Negocio
{
    public class Item<T>
    {
        public T Id { get; set; }
        public string Nombre { get; set; }
    }
}
