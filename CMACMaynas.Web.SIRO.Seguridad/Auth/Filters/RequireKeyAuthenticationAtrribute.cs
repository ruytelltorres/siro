using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMACMaynas.Web.SIRO.Seguridad.Auth.Filters
{
    public class RequireKeyAuthenticationAtrribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var llave = filterContext.HttpContext.Request.Headers.GetValues("llave");
            //var cUser = filterContext.HttpContext.Request.Headers.GetValues("user");

            //if (llave != null)
            //{
            //    HojaRutaLN handler = new HojaRutaLN();
            //    string cllave = llave[0];
            //    cllave = FunGlobales.decodificar(cllave).ToUpper();
            //    string cSeguridad = handler.obtieneSeguridadIngresoSesion(cUser[0]);
            //    cSeguridad = (cUser[0] + cSeguridad).ToUpper();
            //    if (cSeguridad != cllave)
            //    {
            //        filterContext.Result = new RedirectToRouteResult(
            //            new RouteValueDictionary(new { controller = "HojaRuta", action = "RemoteNoAuntenticado" }
            //        ));
            //    }
            //}
            //else
            //{

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if ((!filterContext.HttpContext.Request.IsAuthenticated) || (HttpContext.Current.Session["Usuario"] == null))
                {
                    JavaScriptResult result = new JavaScriptResult()
                    {
                        Script = "<script type=\"text/javascript\">window.location='/Login/Logout' </script>"
                    };
                    filterContext.Result = result;
                }
            }
            else
            {
                if (!filterContext.HttpContext.Request.IsAuthenticated || (HttpContext.Current.Session["Usuario"] == null))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Login", action = "Login" }
                    ));
                }
            }
            //}
            base.OnActionExecuting(filterContext);
        }


    }

}
