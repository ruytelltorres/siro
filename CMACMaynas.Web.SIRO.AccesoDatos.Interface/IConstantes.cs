using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;


namespace CMACMaynas.Web.SIRO.AccesoDatos.Interface
{
    public interface IConstantes
    {
        List<Constante> ObtenerConstantes(int psCodConstante);
        Constante ObtenerDescConstante(int pnConsCod, int pnConstValor);
        
    }
}
