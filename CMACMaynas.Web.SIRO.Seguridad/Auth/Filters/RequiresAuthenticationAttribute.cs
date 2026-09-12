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
    public class RequiresAuthenticationAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if ((!filterContext.HttpContext.Request.IsAuthenticated) || (HttpContext.Current.Session["Usuario"] == null))
                {
                    JavaScriptResult result = new JavaScriptResult()
                    {
                        Script = "<script type=\"text/javascript\">function LogOff(){ window.location='/Login/Logout' }</script>"
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

            base.OnActionExecuting(filterContext);
        }
        
    }
}
