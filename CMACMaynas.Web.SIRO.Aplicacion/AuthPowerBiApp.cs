using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;

namespace CMACMaynas.Web.SIRO.Aplicacion
{
    public class AuthPowerBiApp: IAuthPowerBiApp
    {
        readonly IAuthPowerBi powerbi;

        public AuthPowerBiApp(IAuthPowerBi powerbi)
        {
            this.powerbi = powerbi;
        }

        public AuthPoweBI ObtenerToken() {
            return powerbi.ObtenerToken();
        }
       
    }
}
