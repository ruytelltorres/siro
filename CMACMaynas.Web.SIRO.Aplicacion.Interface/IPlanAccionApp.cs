using CMACMaynas.Web.SIRO.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.Aplicacion.Interface
{
    public interface IPlanAccionApp
    {
        List<PlanAccion> MostrarPlanesAccionRiesgo(long pnNroRiesgo);

        List<ResponsablePlanAccion> MostrarResponsablesPlanesAccionRiesgo(long pnNroRiesgo);

        List<ResponsablePlanAccion> MostrarDetalleResponsablesPlanesAccionRiesgo(long pnNroRiesgo, long pnPlanCod);

        PlanAccion ObtenerPlanAccionRiesgoPlan(long pnNroRiesgo, long pnCodPlan);

        List<PlanAccion> ObtenerPlanAccionResponsable(string psUsuario, int pnTpoRiesgo, string psFiltro = "");

        int RegistrarPlanAccion(long pnNroRiesgo, string psDescPlanAccion, DateTime pdFechImplement, int pnSugerenciaGM, string psComentarios, string psUltimaActualizacion, string psNombreDoc = "", string psNombreDocBD = "");

        int ActualizarPlanAccion(long pnCodPlan, string psDescPlanAccion, DateTime pdFechImplement, int pnSugerenciaGM, string psComentarios, string psUltimaActualizacion, string psNombreDoc = "", string psNombreDocBD = "");

        int RegistrarResponsablePlanAccion(long pnPlanCod, string psUserResponsable, string psUltimaActualizacion);
        int RegistrarResponsablePlanAccionReasignado(long pnPlanCod, string psUserResponsable, string psUserReasignado, string psReasignacion);

        int QuitarResponsablePlanAccion(long pnPlanCod, int pnItemResponsable, string psUltimaActualizacion);

        int EliminarPlanAccion(long pnNroRiesgo, long pnPlanCod, string psUltimaActualizacion);

        List<ResponsablePlanAccion> ObtenerResponsablePlanAccion(long pnPlanCod);

        string ValidarResponsablesPlanAccion(long pnNroRiesgo);

        string[] ActualizarEstadoPlanAccion(long pnCodPlanAccion, int pnEstadoPlan, string psUltimaActualizacion);

        string[] AgregarFechasImplementacion(long pnCodPlanAccion, string pdFechaImplementacion, bool pbImplementado, string psUltimaActualizacion);

        int ActualizaFechasImplementacionPlanAccion(long pnCodPlanAccion);

        List<dynamic> ObtenerFechasImplementacionPlanAccion(long pnPlanCod);

        int GuardarComentariosPlanAccion(long pnCodPlan, string psUsuario, string psComentario);

        List<dynamic> ObtenerComenariosPlanAccion(long pnPlanCod);

        List<PlanAccion> ObtenerHisotialEstadoPlanAccion(long pnPlanCod);
    }
}
