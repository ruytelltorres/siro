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
    public class SubProcesoApp : ISubProcesoApp
    {
        readonly ISubProceso subProceso;
        public SubProcesoApp(ISubProceso subProceso)
        {
            this.subProceso = subProceso;
        }
        public List<SubProcesos> MostarSubprocesosAreas(string psCodProceso)
        {
            return subProceso.MostarSubprocesosAreas(psCodProceso);
        }

        public List<SubProcesos> ObtenerSubProcesosAreas(string psCodProceso = "")
        {
            return subProceso.ObtenerSubProcesosAreas(psCodProceso);
        }

        public int RegistrarGestionSubProcesosAreas(string psCodProceso, string psDescSubProceso, string psAbreviatura, string psUltimaActualizacion, int pnIdSubProceso = 0, int pnAccion = 0)
        {
            return subProceso.RegistrarGestionSubProcesosAreas(psCodProceso, psDescSubProceso, psAbreviatura, psUltimaActualizacion, pnIdSubProceso, pnAccion);
        }
    }
}
