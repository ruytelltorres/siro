using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Seguridad.Auth.ActiveDirectory
{
    public sealed class AuthActiveDirectory
    {
        //Singleton
        private static Lazy<AuthActiveDirectory> instancia = new Lazy<AuthActiveDirectory>(() => new AuthActiveDirectory());
        private static object bloqueo = new object();
        

         private AuthActiveDirectory() { }

        public static AuthActiveDirectory Get
        {
            get
            {
                return instancia.Value;
            }
        }

        string _directorio = ConfigurationManager.AppSettings["LDAP"].ToString();
        string _usuario = String.Empty;
        string _clave = String.Empty;

        public DirectoryEntry Directorio
        {
            get
            {
                DirectoryEntry directory = new DirectoryEntry(_directorio, _usuario, _clave, AuthenticationTypes.Secure);
                return directory;

            }
        }

        public bool ValidaUsuarioDirectorioActivo(string user, string pass)
        {
            try
            {
                DirectoryEntry oDE = new DirectoryEntry(_directorio, user, pass);
                DirectorySearcher oDS = new DirectorySearcher(oDE);
                DirectoryEntry directoryEntry;
                SearchResult searchResult;
                oDS.Filter = "(SAMAccountName=" + user + ")";
                searchResult = oDS.FindOne();
                directoryEntry = searchResult.GetDirectoryEntry();

                if (directoryEntry != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public DirectorySearcher ObtenerDirectorioActivo(string usuario, string clave)
        {
            this._usuario = usuario;
            this._clave = clave;

            DirectorySearcher searcher = new DirectorySearcher(Directorio);
            return searcher;

        }


        private static String ObtenerPropiedad(DirectoryEntry userDetail, String propertyName)
        {
            if (userDetail.Properties.Contains(propertyName))
            {
                return userDetail.Properties[propertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }






    }
}
