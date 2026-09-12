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
    public class ConstSistemaApp : IConstSistemaApp
    {

        //public IConstSistema constSistema { get; set; }

        readonly IConstSistema constSistema;
        public ConstSistemaApp(IConstSistema constSistema)
        {
            this.constSistema = constSistema;
        }

        //IUsuarioApp usuarioApp = Injector.GetServices<IUsuarioApp>();

        public ConstSistema ObtenerConstanteSistema(int psCodConstante)
        {
            return constSistema.ObtenerConstanteSistema(psCodConstante);
        }





    }
}
