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
    public class ProcesoAreaApp : IProcesoAreaApp
    {
        readonly IProcesoArea procesoArea;
        public ProcesoAreaApp(IProcesoArea procesoArea)
        {
            this.procesoArea = procesoArea;
        }
        public List<ProcesoArea> MostrarProcesosAreas(string psCodArea = "")
        {
            return procesoArea.MostrarProcesosAreas(psCodArea);
        }

        public List<ProcesoArea> ObtenerProcesoAreas(string psAreaCod)
        {
            return procesoArea.ObtenerProcesoAreas(psAreaCod);
        }

        public int RegistrarGestionProcesoArea(string psAreaCod, string psUltimaActualizacion, string psNombreProceso = "", int pnProceso = 0)
        {
            return procesoArea.RegistrarGestionProcesoArea(psAreaCod, psUltimaActualizacion, psNombreProceso, pnProceso);
        }
    }
}
