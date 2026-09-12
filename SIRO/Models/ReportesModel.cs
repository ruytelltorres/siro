using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;

namespace SIRO.Models
{
    public class ReportesModel
    {
        public List<MatrizRiesgo> oLstMatrizRiesgo { get; set; }
        public List<dynamic> oLstMatrizRiesgos { get; set; } //Se paso a usar dynamic rn reemplazo de de la clase.
        public List<DatosRiesgos> oLstDatosRiesgoDet { get; set; }
        public List<Areas> oLstAreas { get; set; }
        public List<Constante> oLstConstante { get; set; }

        public List<Constante> oLstDataMapaRiesgo { get; set; }
        public Constante oProbabilidad { get; set; }
        public Constante oImpacto { get; set; }





        /************************* Modelo para los Reportes Grafico ******************************/
        public ReportGrafModel modelGraf { get; set; }

        /*****************************************************************************************/
    }
}
