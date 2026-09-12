using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface IInicioApp
    {
        List<dynamic> ObtenerInformacionInicio(string psUsuario);
    }
}
