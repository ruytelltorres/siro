using Microsoft.Owin;
using Owin;
using SIRO.App_Start;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(SIRO.Startup))]
namespace SIRO
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            IoCConfig.Configure();
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
