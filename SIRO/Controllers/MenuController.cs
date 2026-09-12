using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using CMACMaynas.Web.SIRO.Seguridad.Auth.Filters;
using Newtonsoft.Json;
using SIRO.Controllers.Base;
using SIRO.Models;
using SIRO.Utils.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace SIRO.Controllers
{
    [RequiresAuthenticationAttribute]
    public class MenuController : BaseController
    {
        private readonly IMenuApp menu;
        public MenuController(IMenuApp menu)
        {
            this.menu = menu;
        }


        //public async Task<ActionResult> ObtenerMenuUsuario()
        [HttpGet]
        public ActionResult ObtenerMenuUsuario()
        {
            try
            {
                Usuario user = (Usuario)Session["Usuario"];

                /*Descomentar cuando haya conexion al directorio activo*/
                //string[] grupos = Roles.GetRolesForUser(user.cUser);
                //string ListaGrupos = String.Join(",", grupos);
                //List<Menu> LM = menu.ObtenerMenuUsuario(ListaGrupos, user.cRHCargoCod).ToList<Menu>();
                /*Descomentar cuando haya conexion al directorio activo*/

                List<Menu> LM = menu.ObtenerMenuUsuario(user.cUser, user.cRHCargoCod).ToList<Menu>();
                List<Menu> LP = (from Menu m in LM where m.cMenuId == m.cMenuPadre select m).ToList<Menu>();

                AgregarItem(ref LP, LM);

                return PartialView("_MenuPartial", LP);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        public void AgregarItem(ref List<Menu> Lista, List<Menu> LP)
        {
            foreach (Menu menu in Lista)
            {
                List<Menu> sl = (from Menu sm in LP where menu.cMenuId == sm.cMenuPadre && menu.cMenuId != sm.cMenuId select sm).ToList<Menu>();

                if (sl != null && sl.Count() > 0)
                {
                    menu.oListaMenu = sl;

                    AgregarItem(ref sl, LP);
                }
                else menu.oListaMenu = new List<Menu>();
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult Grupos()
        {
            var grupos = Roles.GetAllRoles();
            List<Constante> lstConstante = new List<Constante>();
            Constante _constante = null;
            foreach (var data in grupos) {
                _constante = new Constante() {
                    cConsDescripcion = Convert.ToString(data)
                };
                lstConstante.Add(_constante);
            }
            return Json(JsonConvert.SerializeObject(lstConstante));
        }

        [RequiresAuthenticationAttribute]
        public JsonResult MenuSistema()
        {
            List<NodeModel> Nodos = new List<NodeModel>();
            var Menu = menu.ObtenerMenuSistema(); //Obtenemos los menu padres

            var MenuPadre = menu.ObtenerMenuSistema().Where(x=> x.nNivel == (int)NivelMenu.PrimerNivel).ToList(); //Obtenemos los menu padres
            foreach (var np in MenuPadre)
            {
                Nodos.Add(new NodeModel
                {
                    icon = np.cIcono,
                    id = np.cMenuId,
                    text = np.cTitulo,
                    nodeId = Convert.ToInt32(np.cMenuId),
                    nodes = NodoHijo(Menu, np.cMenuId)
                });
            }
            return Json(JsonConvert.SerializeObject(Nodos));
        }

        private List<NodeModel> NodoHijo(List<Menu> oltsMenu, string psMenuPadre)
        {
            var MenuHijos = oltsMenu.Where(x => x.cMenuPadre == psMenuPadre && x.nNivel == (int)NivelMenu.SegundoNivel).OrderBy(x => x.nPosicion).ToList(); //menu.ObtenerMenuSistema((int)NivelMenu.SegundoNivel, psMenuHijoPadre); //Obtenemos los menu hijos de los hijos 
            List<NodeModel> nodo = new List<NodeModel>();
            foreach (var nh in MenuHijos)
            {
                nodo.Add(new NodeModel
                {
                    id = nh.cMenuId,
                    text = nh.cTitulo,
                    nodeId = Convert.ToInt32(nh.cMenuId),
                    nodes = NodoNietos(oltsMenu, nh.cMenuId)
                });
            }
            return nodo;
        }

        private List<NodeModel> NodoNietos(List<Menu> oltsMenu, string psMenuHijoPadre)
        {
            var MenuHijos = oltsMenu.Where(x => x.cMenuPadre == psMenuHijoPadre && x.nNivel == (int)NivelMenu.SegundoNivel).OrderBy(x=> x.nPosicion).ToList(); //menu.ObtenerMenuSistema((int)NivelMenu.SegundoNivel, psMenuHijoPadre); //Obtenemos los menu hijos de los hijos 
            List<NodeModel> nodo = new List<NodeModel>();
            foreach (var nh in MenuHijos)
            {
                nodo.Add(new NodeModel
                {
                    id = nh.cMenuId,
                    text = nh.cTitulo,
                    nodeId = Convert.ToInt32(nh.cMenuId)

                });
            }
            return nodo;
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerOpcionesMenuUsuario(string psUsuario)
        {
            List<Menu> MenuUsuario = menu.ObtenerMenuUsuario(psUsuario);
            //var MenuUsuario = MenuLN.ObtenerMenuUsuario(psUsuario);
            return Json(JsonConvert.SerializeObject(MenuUsuario));
        }

        public JsonResult ActivarMenuSistema(string psGrupoUsu, string psMenuId, int Opcion)
        {
            var lsMensaje = menu.ActivarDesactivarOpcionMenu(psGrupoUsu, psMenuId, Opcion);
            //return Json(JsonConvert.SerializeObject(new { Mensaje = lsMensaje }));
            return Json(new { Mensaje = lsMensaje });
        }
        //public JsonResult ObtenerInfPermisoMenu(string cMenuId, string cUsuario, int Proceso)
        //{
        //    var data = MenuLN.ObtenerCargosMenu(cMenuId, cUsuario, Proceso);
        //    return Json(JsonConvert.SerializeObject(data));
        //}














    }

}
