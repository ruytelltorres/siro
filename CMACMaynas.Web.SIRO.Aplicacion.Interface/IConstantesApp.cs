using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface  IConstantesApp
    {
        List<Constante> ObtenerConstantes(int psCodConstante);
        Constante ObtenerDescConstante(int pnConsCod, int pnConstValor);

    }
}
