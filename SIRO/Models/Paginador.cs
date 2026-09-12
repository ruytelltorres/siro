using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIRO.Models
{
    public class Paginador<T> where T : class
    {
        public int pagina_actual { get; set; }
        public int registro_pagina { get; set; }
        public int total_registros { get; set; }
        public int total_paginas { get; set; }
        public IEnumerable<T> resultado { get; set; }
    }
}