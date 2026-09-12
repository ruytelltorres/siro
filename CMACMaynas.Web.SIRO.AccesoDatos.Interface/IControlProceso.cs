using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
    public interface IControlProceso
    {
        List<ControlProceso> ObtenerControlesProceso(string psCodProceso = "");

        List<ControlProceso> MostrarControlesProceso(string psCodProceso = "");

        int RegistraGestionControlProceso(string psCodProceso, string psControlDesc, string psUltimaActualizacion, long pnCodControl = 0, int pnAccion = 0);
    }
}
