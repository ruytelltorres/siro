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
    public class ControlProcesoApp : IControlProcesoApp
    {
        readonly IControlProceso controlProceso;

        public ControlProcesoApp(IControlProceso controlProceso)
        {
            this.controlProceso = controlProceso;
        }

        public List<ControlProceso> MostrarControlesProceso(string psCodProceso = "")
        {
            return controlProceso.MostrarControlesProceso(psCodProceso);
        }

        public List<ControlProceso> ObtenerControlesProceso(string psCodProceso = "")
        {
            return controlProceso.ObtenerControlesProceso(psCodProceso);
        }

        public int RegistraGestionControlProceso(string psCodProceso, string psControlDesc, string psUltimaActualizacion, long pnCodControl = 0, int pnAccion = 0)
        {
            return controlProceso.RegistraGestionControlProceso(psCodProceso, psControlDesc, psUltimaActualizacion, pnCodControl, pnAccion);
        }
    }
}
