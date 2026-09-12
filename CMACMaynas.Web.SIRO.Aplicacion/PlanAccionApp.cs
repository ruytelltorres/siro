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
    public class PlanAccionApp : IPlanAccionApp
    {
        readonly IPlanAccion planAccion;
        public PlanAccionApp(IPlanAccion planAccion)
        {
            this.planAccion = planAccion;
        }
        public int ActualizaFechasImplementacionPlanAccion(long pnCodPlanAccion)
        {
            return planAccion.ActualizaFechasImplementacionPlanAccion(pnCodPlanAccion);
        }

        public string[] ActualizarEstadoPlanAccion(long pnCodPlanAccion, int pnEstadoPlan, string psUltimaActualizacion)
        {
            return planAccion.ActualizarEstadoPlanAccion(pnCodPlanAccion, pnEstadoPlan, psUltimaActualizacion);
        }

        public int ActualizarPlanAccion(long pnCodPlan, string psDescPlanAccion, DateTime pdFechImplement, int pnSugerenciaGM, string psComentarios, string psUltimaActualizacion, string psNombreDoc = "", string psNombreDocBD = "")
        {
            return planAccion.ActualizarPlanAccion(pnCodPlan, psDescPlanAccion, pdFechImplement, pnSugerenciaGM, psComentarios, psUltimaActualizacion, psNombreDoc, psNombreDocBD);
        }

        public string[] AgregarFechasImplementacion(long pnCodPlanAccion, string pdFechaImplementacion, bool pbImplementado, string psUltimaActualizacion)
        {
            return planAccion.AgregarFechasImplementacion(pnCodPlanAccion, pdFechaImplementacion, pbImplementado, psUltimaActualizacion);
        }

        public int EliminarPlanAccion(long pnNroRiesgo, long pnPlanCod, string psUltimaActualizacion)
        {
            return planAccion.EliminarPlanAccion(pnNroRiesgo, pnPlanCod, psUltimaActualizacion);
        }

        public int GuardarComentariosPlanAccion(long pnCodPlan, string psUsuario, string psComentario)
        {
            return planAccion.GuardarComentariosPlanAccion(pnCodPlan, psUsuario, psComentario);
        }

        public List<ResponsablePlanAccion> MostrarDetalleResponsablesPlanesAccionRiesgo(long pnNroRiesgo, long pnPlanCod)
        {
            return planAccion.MostrarDetalleResponsablesPlanesAccionRiesgo(pnNroRiesgo, pnPlanCod);
        }

        public List<PlanAccion> MostrarPlanesAccionRiesgo(long pnNroRiesgo)
        {
            return planAccion.MostrarPlanesAccionRiesgo(pnNroRiesgo);
        }

        public List<ResponsablePlanAccion> MostrarResponsablesPlanesAccionRiesgo(long pnNroRiesgo)
        {
            return planAccion.MostrarResponsablesPlanesAccionRiesgo(pnNroRiesgo);
        }

        public List<dynamic> ObtenerComenariosPlanAccion(long pnPlanCod)
        {
            return planAccion.ObtenerComenariosPlanAccion(pnPlanCod);
        }

        public List<dynamic> ObtenerFechasImplementacionPlanAccion(long pnPlanCod)
        {
            return planAccion.ObtenerFechasImplementacionPlanAccion(pnPlanCod);
        }

        public List<PlanAccion> ObtenerHisotialEstadoPlanAccion(long pnPlanCod)
        {
            return planAccion.ObtenerHisotialEstadoPlanAccion(pnPlanCod);
        }

        public List<PlanAccion> ObtenerPlanAccionResponsable(string psUsuario, int pnTpoRiesgo, string psFiltro = "")
        {
            return planAccion.ObtenerPlanAccionResponsable(psUsuario, pnTpoRiesgo, psFiltro);
        }

        public PlanAccion ObtenerPlanAccionRiesgoPlan(long pnNroRiesgo, long pnCodPlan)
        {
            return planAccion.ObtenerPlanAccionRiesgoPlan(pnNroRiesgo, pnCodPlan);
        }

        public List<ResponsablePlanAccion> ObtenerResponsablePlanAccion(long pnPlanCod)
        {
            return planAccion.ObtenerResponsablePlanAccion(pnPlanCod);
        }

        public int QuitarResponsablePlanAccion(long pnPlanCod, int pnItemResponsable, string psUltimaActualizacion)
        {
            return planAccion.QuitarResponsablePlanAccion(pnPlanCod, pnItemResponsable, psUltimaActualizacion);
        }

        public int RegistrarPlanAccion(long pnNroRiesgo, string psDescPlanAccion, DateTime pdFechImplement, int pnSugerenciaGM, string psComentarios, string psUltimaActualizacion, string psNombreDoc = "", string psNombreDocBD = "")
        {
            return planAccion.RegistrarPlanAccion(pnNroRiesgo, psDescPlanAccion, pdFechImplement, pnSugerenciaGM, psComentarios, psUltimaActualizacion, psNombreDoc, psNombreDocBD);
        }

        public int RegistrarResponsablePlanAccion(long pnPlanCod, string psUserResponsable, string psUltimaActualizacion)
        {
            return planAccion.RegistrarResponsablePlanAccion(pnPlanCod, psUserResponsable, psUltimaActualizacion);
        }
        public int RegistrarResponsablePlanAccionReasignado(long pnPlanCod, string psUserResponsable, string psUserReasignado, string psReasignacion)
        {
            return planAccion.RegistrarResponsablePlanAccionReasignado(pnPlanCod, psUserResponsable, psUserReasignado, psReasignacion);
        }

        public string ValidarResponsablesPlanAccion(long pnNroRiesgo)
        {
            return planAccion.ValidarResponsablesPlanAccion(pnNroRiesgo);
        }
    }
}
