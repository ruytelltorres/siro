
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
    public class UsuarioApp : IUsuarioApp
    {
        readonly IUsuario usuario;

        public UsuarioApp(IUsuario usuario) {
            this.usuario = usuario;
        }

        public Usuario ObtenerDatosUsuario(string psUser)
        {
            return usuario.ObtenerDatosUsuario(psUser);
        }

        public string ObtenerMailUsuario(string cUsuario)
        {
            return usuario.ObtenerMailUsuario(cUsuario);
        }

        public List<Usuario> ObtenerUsuarioAgenciaArea(string psAgeCod, string psAreaCod)
        {
            return usuario.ObtenerUsuarioAgenciaArea(psAgeCod, psAreaCod);
        }

        public List<Usuario> ObtenerUsuarioArea(string psAreaCod)
        {
            return usuario.ObtenerUsuarioArea( psAreaCod);
        }

        public List<Usuario> ObtenerUsuariosCargos(string psCargoCod)
        {
            return usuario.ObtenerUsuariosCargos( psCargoCod);
        }
        
        //bool GuardarDatosPerfil(string psUser, string psUrlImagen, int pnProceso = 1);







    }
}
