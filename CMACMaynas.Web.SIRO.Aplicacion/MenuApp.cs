using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion
{
    public class MenuApp : IMenuApp
    {
        readonly IMenu menu;
        public MenuApp(IMenu menu)
        {
            this.menu = menu;
        }
        public string ActivarDesactivarOpcionMenu(string psGrupoUsu, string psMenuId, int Opcion)
        {
            return menu.ActivarDesactivarOpcionMenu(psGrupoUsu, psMenuId, Opcion);
        }

        public List<Menu> ObtenerCargosMenu(string cMenuId, string cUsuario, int Proceso)
        {
            return menu.ObtenerCargosMenu(cMenuId, cUsuario, Proceso);
        }

        public List<Menu> ObtenerMenuSistema()
        {
            return menu.ObtenerMenuSistema();
        }

        public List<Menu> ObtenerMenuUsuario(string psGrupoUser, string psCargoUser = "")
        {
            return menu.ObtenerMenuUsuario(psGrupoUser, psCargoUser);
        }
    }
}
