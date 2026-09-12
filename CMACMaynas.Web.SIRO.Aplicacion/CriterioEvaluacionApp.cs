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
    public class CriterioEvaluacionApp : ICriterioEvaluacionApp
    {
        readonly ICriterioEvaluacion criterioEvaluacion;
        public CriterioEvaluacionApp(ICriterioEvaluacion criterioEvaluacion)
        {
            this.criterioEvaluacion = criterioEvaluacion;
        }
        public int GrabaConfigCriterioEval(List<dynamic> poConfigCriterio, string psUltimaActualizacion)
        {
            return criterioEvaluacion.GrabaConfigCriterioEval(poConfigCriterio, psUltimaActualizacion);
        }

        public List<CriteriosEvaluacion> ObtenerCriteriosEvaluacion(int pnCriterioCod)
        {
            return criterioEvaluacion.ObtenerCriteriosEvaluacion(pnCriterioCod);
        }

        public List<CriteriosEvaluacion> ObtenerValorCriteriosEval()
        {
            return criterioEvaluacion.ObtenerValorCriteriosEval();
        }
    }
}
