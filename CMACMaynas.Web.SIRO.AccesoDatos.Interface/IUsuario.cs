using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
    public interface IUsuario
    {

        Usuario ObtenerDatosUsuario(string psUser);
        string ObtenerMailUsuario(string cUsuario);

        List<Usuario> ObtenerUsuarioAgenciaArea(string psAgeCod, string psAreaCod);

        List<Usuario> ObtenerUsuarioArea(string psAreaCod);

        List<Usuario> ObtenerUsuariosCargos(string psCargoCod);

        //bool GuardarDatosPerfil(string psUser, string psUrlImagen, int pnProceso = 1);


    }
}
