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
    public class RequiresAuthorizationAttribute: ActionFilterAttribute
    {


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if ((!filterContext.HttpContext.Request.IsAuthenticated) || (HttpContext.Current.Session["Usuario"] == null))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login" }));
            }
            //else if (HttpContext.Current.Session[CodigoOpcion] == null)
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));

            base.OnActionExecuting(filterContext);
        }



    }
}
