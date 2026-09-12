using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion
{
    public class InicioApp : IInicioApp
    {
        readonly IInicio inicio;
        public InicioApp(IInicio inicio)
        {
            this.inicio = inicio;
        }
        public List<dynamic> ObtenerInformacionInicio(string psUsuario)
        {
            return inicio.ObtenerInformacionInicio(psUsuario);
        }
    }
}
