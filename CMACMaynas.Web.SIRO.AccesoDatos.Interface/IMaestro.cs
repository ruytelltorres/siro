using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
    public interface IMaestro
    {
        string GenerarNroRiesgo(string psFecha = "", string psAgencia = "01", string psArea = "000", string psUsuario = "SIST");
    }
}
