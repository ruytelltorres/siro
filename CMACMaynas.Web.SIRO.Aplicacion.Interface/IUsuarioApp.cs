using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface IUsuarioApp
    {
        Usuario ObtenerDatosUsuario(string psUser);
        string ObtenerMailUsuario(string cUsuario);

        List<Usuario> ObtenerUsuarioAgenciaArea(string psAgeCod, string psAreaCod);

        List<Usuario> ObtenerUsuarioArea(string psAreaCod);

        List<Usuario> ObtenerUsuariosCargos(string psCargoCod);



    }
}
