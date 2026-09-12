using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion
{
    public class AgenciasApp : IAgenciasApp
    {
        readonly IAgencias _agencias;
        public AgenciasApp(IAgencias _agencias) {
            this._agencias = _agencias;
        }

        public List<Agencias> ObtenerAgencias()
        {
            return _agencias.ObtenerAgencias();
        }

    }
}
