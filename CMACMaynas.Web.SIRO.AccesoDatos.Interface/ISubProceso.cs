using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
    public interface ISubProceso
    {
        List<SubProcesos> ObtenerSubProcesosAreas(string psCodProceso = "");

        List<SubProcesos> MostarSubprocesosAreas(string psCodProceso);

        int RegistrarGestionSubProcesosAreas(string psCodProceso, string psDescSubProceso, string psAbreviatura, string psUltimaActualizacion, int pnIdSubProceso = 0, int pnAccion = 0);

    }
}
