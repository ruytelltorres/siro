using System;
using System.Configuration;

namespace SIRO.Servicios.PowerBi
{
    public class ConfigValidatorService
    {
        public static readonly string ApplicationId = ConfigurationManager.AppSettings["applicationId"];
        public static readonly Guid WorkspaceId = GetParamGuid(ConfigurationManager.AppSettings["workspaceId"]);
        public static readonly Guid ReportId = GetParamGuid(ConfigurationManager.AppSettings["reportId"]);
        public static readonly string AuthenticationType = ConfigurationManager.AppSettings["authenticationType"];
        public static readonly string ApplicationSecret = ConfigurationManager.AppSettings["applicationSecret"];
        public static readonly string Tenant = ConfigurationManager.AppSettings["tenant"];
        public static readonly string Username = ConfigurationManager.AppSettings["pbiUsername"];
        public static readonly string Password = ConfigurationManager.AppSettings["pbiPassword"];

        /// <summary>
        /// Check if web.config embed parameters have valid values.
        /// </summary>
        /// <returns>Null if web.config parameters are valid, otherwise returns specific error string.</returns>
        public static string GetWebConfigErrors()
        {
            string message = null;
            Guid result;

            // Application Id must have a value.
            if (string.IsNullOrWhiteSpace(ApplicationId))
            {
                message = "ApplicationId está vacío. Registre su aplicación como aplicación nativa en https://dev.powerbi.com/apps y complete la identificación del cliente en web.config.";
            }
            // Application Id must be a Guid object.
            else if (!Guid.TryParse(ApplicationId, out result))
            {
                message = "ApplicationId debe ser un objeto Guid. por favor registre su aplicación como aplicación nativa en https://dev.powerbi.com/apps y complete la identificación de la aplicación en web.config.";
            }
            // Workspace Id must have a value.
            else if (WorkspaceId == Guid.Empty)
            {
                message = "WorkspaceId está vacío o no es un Guid válido. Complete su ID correctamente en web.config";
            }
            // Report Id must have a value.
            else if (ReportId == Guid.Empty)
            {
                message = "ReportId está vacío o no es un Guid válido. Complete su ID correctamente en web.config";
            }
            else if (AuthenticationType.Equals("masteruser", StringComparison.InvariantCultureIgnoreCase))
            {
                // Username must have a value.
                if (string.IsNullOrWhiteSpace(Username))
                {
                    message = "El nombre de usuario está vacío. Complete el nombre de usuario de Power BI en web.config";
                }

                // Password must have a value.
                if (string.IsNullOrWhiteSpace(Password))
                {
                    message = "La contraseña está vacía. Introduzca la contraseña del nombre de usuario de Power BI en web.config";
                }
            }
            else if (AuthenticationType.Equals("serviceprincipal", StringComparison.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(ApplicationSecret))
                {
                    //ApplicationSecret está vacío.por favor registre su aplicación como aplicación web y complete appSecret en web.config.
                    message = "No se definió correctamente la configuración para la autentificación con el informe";
                }
                // Must fill tenant Id
                else if (string.IsNullOrWhiteSpace(Tenant))
                {
                    message = "Inquilino no válido.Ingrese la identificación del inquilino en el archivo de configuración";
                }
            }
            else
            {
                message = "Tipo de autenticación no válido";
            }

            return message;
        }

        private static Guid GetParamGuid(string param)
        {
            Guid paramGuid = Guid.Empty;
            Guid.TryParse(param, out paramGuid);
            return paramGuid;
        }
    }
}