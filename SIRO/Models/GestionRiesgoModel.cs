using CMACMaynas.Web.SIRO.Negocio;
using System.Collections.Generic;


namespace SIRO.Models
{
    public class GestionRiesgoModel
    {
        public DetalleRiesgo oRiesgo { get; set; }
        //public List<DatosRiesgos> oLstRiesgoOperacional { get; set; }
        public List<RiesgoOperacional> oLstRiesgoOperacional { get; set; }
        public List<Autoevaluacion> oLstDatosEvaluaciones { get; set; }

        public List<EventoPerdida> oLstEventoGrupo { get; set; }
        public List<Constante> oLstTipoEvaluacion { get; set; }
        public RiesgoOperacional oRiesgoOperacional { get; set; }
        public Autoevaluacion oEvaluaciones { get; set; }
        public RiesgoOperacional oDatosRiesgo { get; set; }
        public EventoPerdida oEventoPerdida { get; set; }
        public List<Constante> oLstFactorRiesgo { get; set; }
        public List<Constante> oLstEventoPerdida { get; set; }
        public List<Constante> oLstDescCortaEventoP{ get; set; }
        public List<LineaNegocio> oLstLineaNegocio { get; set; }
        public List<SubLineaNegocio> oLstSubLineaNegocio { get; set; }
        public List<SubClaseEventoPerdida> oLstSubEventoPerdida { get; set; }

        public Taller oTaller { get; set; }

        //public List<SubLineaNegocioEN> oListSubLineaNegocio { get; set; }
        public List<ProcesoArea> oLstProcesoAreas { get; set; }
        public List<SubProcesos> oLstSubProcesoAreas { get; set; }
        public List<ControlProceso> oLstControlProcesos { get; set; }
        public List<Producto> oLstProducto { get; set; }

        public List<SubProducto> oLstSubProducto { get; set; }

        public List<Constante> oLstProbabilidad { get; set; }
        public List<Constante> oLstImpacto { get; set; }
        public List<Constante> oLstTipoCobertura { get; set; }
        public List<Constante> oLstEstados { get; set; }
        public List<Constante> oLstProcesoRiesgo { get; set; }
        public List<Constante> oLstIncentivos { get; set; }

        public List<RiesgoResidual> oLstRiesgoResidual { get; set; }
        public RiesgoResidual oRiesgoResidual { get; set; }

        public List<CriteriosEvaluacion> oLstResponsableDef { get; set; }
        public List<CriteriosEvaluacion> oLstFrecuenciaDef { get; set; }
        public List<CriteriosEvaluacion> oLstEvidenciaControl { get; set; }
        public List<CriteriosEvaluacion> oLstTipoEjecucion { get; set; }
        public List<CriteriosEvaluacion> oLstCumpleObjetivo { get; set; }

        public EscalaNivelesRiesgo oEscalaNivRiesgo { get; set; }
        public int CalifEfecControl { get; set; }
        public int EstadoTaller { get; set; }

        public List<PlanAccion> oLstPlanAccion { get; set; }
        public List<Comentarios> oLstComentarios { get; set; }
        public PlanAccion oPlanAccion { get; set; }
       

        public List<Agencias> oLstAgencia { get; set; }
        public List<Areas> oLstAreas { get; set; }
        public List<CausaRiesgo> oLstCausas { get; set; }
        public List<ResponsablePlanAccion> oLstReponPlanAccionRiesgo { get; set; }
        public List<ResponsablePlanAccion> oLstReponPlanAccion { get; set; }

        //public Usuario oUsuarios { get; set; }
        //Modelo de datos para obtener informacion 
        public List<dynamic> oLstGastosEvento { get; set; }
        public List<dynamic> oLstCtaContEvento { get; set; }
        public List<dynamic> LstClaseEventoPerdida { get; set; }
        public List<dynamic> LstSubClaseEventoPerdida { get; set; }
        public List<string> sLstInfoGestionRiesgo { get; set; }
        public List<dynamic> lstObservacionesGestion { get; set; }



    }
}