using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
    public interface IAreas
    {
         List<Areas> ObtenerAreasAgencia(string psAgeCod);

         List<Areas> ObtenerAreas();

    }
}
