using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion
{
    public class MaestroApp : IMaestroApp
    {
        readonly IMaestro maestro;
        public MaestroApp(IMaestro maestro)
        {
            this.maestro = maestro;
        }
        public string GenerarNroRiesgo(string psFecha = "", string psAgencia = "01", string psArea = "000", string psUsuario = "SIST")
        {
            return maestro.GenerarNroRiesgo(psFecha, psAgencia, psArea, psUsuario);
        }
    }
}
