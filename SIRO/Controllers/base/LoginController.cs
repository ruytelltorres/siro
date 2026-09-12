using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using CMACMaynas.Web.SIRO.Seguridad.Auth.Interfraces;
using CMACMaynas.Web.SIRO.Seguridad.Auth.Services;
using SIRO.Controllers.Base;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace SIRO.Controllers
{
    public class LoginController : BaseController
    {
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        private readonly IUsuarioApp _usuario;
        private readonly IConstSistemaApp _constSistema;

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        public LoginController(IUsuarioApp _usuario, IConstSistemaApp _constSistema)
        {
            this._usuario = _usuario;
            this._constSistema = _constSistema;
        }

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario != null)
                {
                    Redirect("/");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }

        [HttpPost]
        public JsonResult ValidarUsuario(string Usuario, string Clave)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(Usuario, Clave))
                {
                    FormsService.SignIn(Usuario, true);

                    Usuario usuario = new Usuario();
                    usuario = _usuario.ObtenerDatosUsuario(Usuario);

                    #region CLAIMS
                    //ClaimsIdentity claims = new ClaimsIdentity();

                    //IList<Claim> iClaims = new List<Claim>(){
                    //new Claim("claim:usuario", usuario.cUser),
                    //    new Claim("claim:codigopers", usuario.oPersona.cPersCod),
                    //    new Claim("claim:nombrepers", usuario.oPersona.cPersNombre),
                    //    new Claim("claim:cargocod", usuario.cRHCargoCod),
                    //    new Claim("claim:cargodesc", usuario.cRHCargoDescripcion),
                    //    new Claim("claim:areacod", usuario.oAreas.cAreaCod),
                    //    new Claim("claim:areadesc", usuario.oAreas.cAreaDescripcion),
                    //    new Claim("claim:agecod", usuario.oAgencia.cAgeCod),
                    //    new Claim("claim:agedesc", usuario.oAgencia.cAgeDescripcion),
                    //    new Claim("claim:sistema", _constSistema.ObtenerConstanteSistema(151).cConsSisValor),
                    //    new Claim("claim:version", _constSistema.ObtenerConstanteSistema(100).cConsSisValor),
                    //};

                    //var userIdentity = new ClaimsIdentity(iClaims, DefaultAuthenticationTypes.ApplicationCookie);
                    //Request.GetOwinContext().Authentication.SignIn(userIdentity);

                    //var claimsPrincipal = new ClaimsPrincipal(userIdentity);
                    //Thread.CurrentPrincipal = claimsPrincipal;
                    #endregion

                    if (usuario != null)
                    {
                        System.Web.HttpContext.Current.Session["Usuario"] = usuario;
                        //System.Web.HttpContext.Current.Session["VerSistema"] = _constSistema.ObtenerConstanteSistema(100).cConsSisValor;
                        //System.Web.HttpContext.Current.Session["NombreSistema"] = _constSistema.ObtenerConstanteSistema(151).cConsSisValor;
                    }
                    else {
                        return Json(new { URL = Url.Action("Login", "Login"), Estado = 0, Mensaje = "No se encontró información del usuario." });
                    }
                    return Json(new { URL = Url.Action("Index", "Home"), Estado = 1, Mensaje = "" });
                }
                else
                {
                    return Json(new { URL = Url.Action("Login", "Login"), Estado = 0, Mensaje = "Usuario ó Contraseña incorrectos" });
                }
            }
            return Json(new { URL = Url.Action("Login", "Login"), Estado = 0, Mensaje = "" });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //Request.GetOwinContext().Authentication.SignOut();
            //Roles.DeleteCookie();

            Session.RemoveAll();
            return RedirectToAction("Login", "Login");
        }






    }
}