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
    public class AreasApp : IAreasApp
    {
        readonly IAreas areas;
        public AreasApp(IAreas areas)
        {
            this.areas = areas;
        }
        public List<Areas> ObtenerAreas()
        {
            return areas.ObtenerAreas();
        }

        public List<Areas> ObtenerAreasAgencia(string psAgeCod)
        {
            return areas.ObtenerAreasAgencia(psAgeCod);
        }
    }
}
