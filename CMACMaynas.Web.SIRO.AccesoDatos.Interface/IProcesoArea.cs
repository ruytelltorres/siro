using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
    public interface IProcesoArea
    {
         List<ProcesoArea> ObtenerProcesoAreas(string psAreaCod);

         List<ProcesoArea> MostrarProcesosAreas(string psCodArea = "");

         int RegistrarGestionProcesoArea(string psAreaCod, string psUltimaActualizacion, string psNombreProceso = "", int pnProceso = 0);
    }
}
