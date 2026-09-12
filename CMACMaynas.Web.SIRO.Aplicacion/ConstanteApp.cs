using CMACMaynas.Web.SIRO.AccesoDatos.Interface;
using entidad = CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;

namespace CMACMaynas.Web.SIRO.Aplicacion
{
    public class ConstanteApp: IConstantesApp
    {
        readonly IConstantes constantes;

        public ConstanteApp(IConstantes constantes)
        {
            this.constantes = constantes;
        }

        public List<entidad.Constante> ObtenerConstantes(int psCodConstante)
        {
            return constantes.ObtenerConstantes(psCodConstante);
        }

        public entidad.Constante ObtenerDescConstante(int pnConsCod, int pnConstValor)
        {
            return constantes.ObtenerDescConstante(pnConsCod, pnConstValor);
        }






    }
}