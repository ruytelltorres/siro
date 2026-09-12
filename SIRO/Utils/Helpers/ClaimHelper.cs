using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIRO.Utils.Helpers
{
    public class ClaimHelper
    {
        private ClaimHelper() { }

        private static ClaimHelper instancia;

        public static ClaimHelper Get
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ClaimHelper();
                }
                return instancia;
            }
        }

        public string Claims(string psClaim)
        {
            var identity = (System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal;
            var principal = System.Threading.Thread.CurrentPrincipal as System.Security.Claims.ClaimsPrincipal;
            var name = identity.Claims.Where(c => c.Type == psClaim).Select(c => c.Value).FirstOrDefault();
            return name;
        }
    }
}