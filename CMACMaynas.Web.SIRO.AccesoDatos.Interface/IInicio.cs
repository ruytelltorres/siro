using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
    public interface IInicio
    {
        List<dynamic> ObtenerInformacionInicio(string psUsuario);

    }
}
