using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using CMACMaynas.Web.SIRO.Seguridad.Auth.Filters;
using SIRO.Controllers.Base;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace SIRO.Controllers
{
    public class HomeController : BaseController
    {

        private readonly IInicioApp inicio;

        public HomeController(IInicioApp inicio)
        {
            this.inicio = inicio;
        }

        //[BreadCrumb(Clear = true, Label = "SIRO")]
        [RequiresAuthenticationAttribute]
        public ActionResult Index()
        {

            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                List<dynamic> datos = inicio.ObtenerInformacionInicio(usuario.cRHCargoCod == "005011" ? "" : usuario.cUser);

                return View(datos);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }


        //[BreadCrumb(Clear = true, Label = "Manual de Usuario")]
        [RequiresAuthenticationAttribute]
        public ActionResult Manual()
        {

            try
            {
                //Usuario usuario = (Usuario)Session["Usuario"];
                //List<dynamic> datos = inicio.ObtenerInformacionInicio(usuario.cRHCargoCod == "005011" ? "" : usuario.cUser);

                return View();
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }






    }
}