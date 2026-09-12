using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Web.Security;

namespace CMACMaynas.Web.SIRO.Seguridad
{
    public class RolesProvider : RoleProvider
    {
        string directorio = ConfigurationManager.AppSettings["LDAP"].ToString();
        public override string ApplicationName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            DirectoryEntry objDirectoryEntry = new DirectoryEntry(directorio);
            DirectorySearcher objDirectorySearcher = new DirectorySearcher(objDirectoryEntry);

            DirectoryEntry objGroupEntry;
            SearchResultCollection _objSearchResultCol;

            List<string> Grupos = new List<string>();
            string sGrupo;

            try
            {
                objDirectorySearcher.Filter = "(&(objectClass=group))";
                objDirectorySearcher.SearchScope = SearchScope.Subtree;
                _objSearchResultCol = objDirectorySearcher.FindAll();

                try
                {
                    if (_objSearchResultCol.Count != 0)
                    {
                        foreach (SearchResult objResult in _objSearchResultCol)
                        {
                            objGroupEntry = objResult.GetDirectoryEntry();

                            sGrupo = objGroupEntry.Name.Replace("CN=", "");
                            if (sGrupo.StartsWith("GRUPO"))
                                Grupos.Add(sGrupo);
                        }
                    }
                    else { throw new Exception("No se Encontraron Grupos"); }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                Grupos.Sort((p, q) => string.Compare(p, q));
                return Grupos.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> Grupos = new List<string>();
            int i = 0;
            int propertyCount = 0;

            DirectoryEntry myDE = new DirectoryEntry(directorio);
            DirectorySearcher mySearcher = new DirectorySearcher(myDE);

            mySearcher.Filter = "(SAMAccountName=" + username + ")";

            SearchResult myresult = mySearcher.FindOne();
            propertyCount = myresult.Properties["memberOf"].Count;

            string dn = "";
            int equalsIndex = 0;
            int commaIndex = 0;

            //Se va obteniendo los grupos al que pertenece el usuario
            for (i = 0; i < propertyCount; i++)
            {
                dn = myresult.Properties["memberOf"][i].ToString();

                equalsIndex = dn.IndexOf("=", 1);

                commaIndex = dn.IndexOf(",", 1);
                if (equalsIndex == -1)
                    return new string[] { "" };

                Grupos.Add(dn.Substring((equalsIndex + 1), +(commaIndex - equalsIndex) - 1));
            }
            return Grupos.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }


    }
}
