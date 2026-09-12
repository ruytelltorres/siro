using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Connect
{
    public class ConectarBD
    {

     
        private string ConnectionBD = ConfigurationManager.ConnectionStrings["ServerConnectDBSiro"].ToString();
        private static ConectarBD instancia;

        private ConectarBD() { }

        public static ConectarBD Get
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ConectarBD();
                }
                return instancia;
            }
        }

        public string ConnectionString()
        {
            return ConnectionBD;
        }

        //public string GetConnectionString()
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("config.json", optional: true, reloadOnChange: true);
        //    return builder.Build().GetSection("ConnectionStrings").GetSection("BaseDatos").Value;
        //}
        
    }
}
