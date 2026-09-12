using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface IMenuApp
    {
        List<Menu> ObtenerMenuUsuario(string psGrupoUser, string psCargoUser = "");

        List<Menu> ObtenerMenuSistema();

        string ActivarDesactivarOpcionMenu(string psGrupoUsu, string psMenuId, int Opcion);

        List<Menu> ObtenerCargosMenu(string cMenuId, string cUsuario, int Proceso);

    }
}
