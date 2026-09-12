using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using System;

namespace CMACMaynas.Web.SIRO.AccesoDatos
{
    public class AuthPowerBi : IAuthPowerBi
    {
        public Negocio.AuthPoweBI ObtenerToken()
        {
            try
            {
                //string userName = ClaimsPrincipal.Current.FindFirst("name").Value;
                //string accessToken = TokenManager.GetAccessToken(PowerBIPermissionScopes.ReadUserWorkspaces);

                var auth = new Negocio.AuthPoweBI()
                {
                    UserName = "",
                    AccessToken = ""
                };
                return auth;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public string ValidarConfiguracion()
        {
            try
            {
                //var config = ConfigValidatorService.ValidateConfig();
                return "";
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
