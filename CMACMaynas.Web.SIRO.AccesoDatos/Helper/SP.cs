using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMACMaynas.Web.SIRO.AccesoDatos.Helper
{
    public class SP
    {

        #region Procedimientos Almacenados - Servidor
        public const string stp_sel_HoraServer = "stp_sel_HoraServer";
        public const string GenerarNroRiesgo = "GenerarNroRiesgo";

        #endregion

        #region Procedimientos Almacenados - Usuarios
        /// <summary>
        /// Obtener datos del usuario por el codigo del usuario
        /// </summary>
        public const string stp_sel_ObtenerDatosUsuario = "stp_sel_ObtenerDatosUsuario";
        public const string stp_upd_ActualizarDatosPerfilSiro = "stp_upd_ActualizarDatosPerfilSiro";
        public const string stp_sel_ObtenerUsuarioAgenciasAreas = "stp_sel_ObtenerUsuarioAgenciasAreas";
        public const string stp_sel_ObtenerUsuarioAreas = "stp_sel_ObtenerUsuarioAreas";
        public const string stp_sel_ObtenerUsuariosCargos = "stp_sel_ObtenerUsuariosCargos";
        public const string spt_sel_obtener_email_usuario = "spt_sel_obtener_email_usuario";
        #endregion

        #region Procedimeintos Almacenados - Menu
        /// <summary>
        /// Obtiene informacion del menu que pertenece al usuario
        /// </summary>
        public const string stp_sel_ObtenerMenuUser = "stp_sel_ObtenerMenuUser";
        public const string stp_sel_ObtenerMenuSistema = "stp_sel_ObtenerMenuSistema";
        public const string stp_sel_ObtenerPermisoCargoMenu = "stp_sel_ObtenerPermisoCargoMenu";
        public const string stp_sel_ObtenerCargoHabilitaOpcion = "stp_sel_ObtenerCargoHabilitaOpcion";

        public const string stp_ins_upd_ActivarDesactivarOpcionMenu = "stp_ins_upd_ActivarDesactivarOpcionMenu";
        #endregion

        #region Procedimientos Almacenados - Inicio/Home
        public const string stp_sel_ObtenerDatosPrincipal = "stp_sel_ObtenerDatosPrincipal";
        #endregion

        #region Procedimientos Almacenados - Areas
        /// <summary>
        /// Lista las areas provenientes de la BD Caja Maynas
        /// </summary>
        public const string stp_sel_ListarAreas = "stp_sel_ListarAreas";
        public const string stp_sel_ListarAreasAgencias = "stp_sel_ListarAreasAgencias";

        #endregion

        #region Procedimientos Almacenados - Agencias
        /// <summary>
        /// Lista las areas provenientes de la BD Caja Maynas
        /// </summary>
        public const string stp_sel_ListarAgencias = "stp_sel_ListarAgencias";
        #endregion

        #region Procedimientos Almacenados - Procesos/Subprocesos de las Areas
        /// <summary>
        /// Lista los procesos de cada área
        /// </summary>
        public const string stp_sel_MostrarProcesoxArea = "stp_sel_MostrarProcesoxArea";
        public const string stp_sel_ListarProcesoxArea = "stp_sel_ListarProcesoxArea";


        //Sub Procesos
        public const string stp_sel_MostrarSubProcesoAreas = "stp_sel_MostrarSubProcesoAreas";
        public const string stp_sel_ListarSubProcesoAreas = "stp_sel_ListarSubProcesoAreas";
        public const string stp_ins_upd_GestionSubProceso = "stp_ins_upd_GestionSubProceso";

        /// <summary>
        /// Registra el proceso del area correspondiente
        /// </summary>
        public const string stp_ins_upd_GestionProcesoArea = "stp_ins_upd_GestionProcesoArea";
        #endregion

        #region Procedimientos Almacenados - Linea de Negocio/Sub-Linea de Negocio
        public const string stp_sel_ListarLineasNegocio = "stp_sel_ListarLineasNegocio";

        //Sub Linea Negocio
        public const string stp_sel_ListarSubLineNegocio = "stp_sel_ListarSubLineNegocio";
        #endregion

        #region Procedimientos Almacenados - Producto
        public const string stp_sel_ObtenerProductoLineaNegocio = "stp_sel_ObtenerProductoLineaNegocio";
        public const string stp_sel_ListarProductoLineaNegocio = "stp_sel_ListarProductoLineaNegocio";
        #endregion

        #region Procedimientos Almacenados - SubProducto
        public const string stp_sel_ObtenerSubProducto = "stp_sel_ObtenerSubProducto";
        #endregion

        #region Procedimientos Almacenados - Criterios Evaluacion

        public const string stp_sel_ListarCriteriosEvalucion = "stp_sel_ListarCriteriosEvalucion";
        public const string stp_sel_ObtenerCriteriosEval = "stp_sel_ObtenerCriteriosEval";
        public const string stp_upd_ActualizaCriterioEval = "stp_upd_ActualizaCriterioEval";
        #endregion


        #region Procedimientos Almacenados - Controles de las Areas

        public const string stp_sel_MostrarControlesProceso = "stp_sel_MostrarControlesProceso";
        public const string stp_sel_ListarControlesxProceso = "stp_sel_ListarControlesxProceso";
        public const string stp_ins_upd_GestionControlProceso = "stp_ins_upd_GestionControlProceso";

        #endregion

        #region Procedimientos Almacenados - Controles del Riesgo Residual
        public const string stp_ins_GrabarControlesRiesgoResidual = "stp_ins_GrabarControlesRiesgoResidual";
        public const string stp_upd_ActualizarControlesRiesgoResidual = "stp_upd_ActualizarControlesRiesgoResidual";
        public const string stp_upd_EliminarControlRiesgoResidual = "stp_upd_EliminarControlRiesgoResidual";
        public const string stp_sel_ListarControlesRiesgoResidual = "stp_sel_ListarControlesRiesgoResidual";
        public const string stp_sel_ObtenerValorCriterioEvalControlRiesgoResidual = "stp_sel_ObtenerValorCriterioEvalControlRiesgoResidual";
        #endregion

        #region Procedimietntos Almacenados - Controles
        /// <summary>
        /// Lista los controles de las areas (Paran - Codigo Area)
        /// </summary>
        public const string stp_sel_ListarControlAreas = "stp_sel_ListarControlAreas";
        #endregion

        #region Procedimientos Almacenados - Eventos de Perdida
        /// <summary>
        /// Lista todos los registros de los evento de perdida (Param - "-")
        /// </summary>
        public const string stp_sel_ListarTodosEventosPerdidas = "stp_sel_ListarTodosEventosPerdidas";

        #endregion

        #region Procedimientos Almacenados - Riesgos Operacionales

        /// <summary>
        /// Lista los riesgo operacionales en la gestion de riesgo operacional
        /// </summary>
        public const string stp_sel_ListarRiesgoOperacional = "stp_sel_ListarRiesgoOperacional";
        public const string stp_sel_ObtenerDetalleRiesgoOperacional = "stp_sel_ObtenerDetalleRiesgoOperacional";
        public const string stp_sel_ObtenerTipoRiesgo = "stp_sel_ObtenerTipoRiesgo";

        /// <summary>
        /// Procedimiento almacenado para el registro de los riesgos operacionales
        /// </summary>
        public const string stp_ins_RegistrarRiesgoOperacional = "stp_ins_RegistrarRiesgoOperacional";
        public const string stp_ins_AgregarControlesRiesgo = "stp_ins_AgregarControlesRiesgo";
        public const string stp_upd_GuardarModificacionRiesgoOperacional = "stp_upd_GuardarModificacionRiesgoOperacional";
        public const string stp_del_EliminarControlesRiesgo = "stp_del_EliminarControlesRiesgo";

        public const string stp_upd_GrabarGestionRiesgoOperacionaPaso1 = "stp_upd_GrabarGestionRiesgoOperacionaPaso1";
        public const string stp_upd_GrabarGestionRiesgoOperacionaPaso2 = "stp_upd_GrabarGestionRiesgoOperacionaPaso2";
        public const string stp_upd_GrabarGestionRiesgoOperacionaPaso3 = "stp_upd_GrabarGestionRiesgoOperacionaPaso3";
        public const string stp_upd_GrabarGestionRiesgoOperacionaPaso4 = "stp_upd_GrabarGestionRiesgoOperacionaPaso4";
        public const string stp_upd_GrabarGestionRiesgoOperacionaPaso5 = "stp_upd_GrabarGestionRiesgoOperacionaPaso5";

        public const string stp_sel_ObtenerInfoProcesoGestionRiesgoOperacional = "stp_sel_ObtenerInfoProcesoGestionRiesgoOperacional";

        public const string stp_ins_GrabarObservacionGestion = "stp_ins_GrabarObservacionGestion";
        public const string stp_sel_ObtenerObservacionesRiesgo = "stp_sel_ObtenerObservacionesRiesgo";
        public const string stp_upd_GrabarConfirmacionModificacion = "stp_upd_GrabarConfirmacionModificacion";




        #endregion

        #region Acciones sobre el riesgo
        public const string stp_ins_RechazarRiesgo = "stp_ins_RechazarRiesgo";
        public const string stp_upd_EliminarRiesgo = "stp_upd_EliminarRiesgo";
        #endregion

        #region Procedimiento Almacenados -Taller
        /// <summary>
        /// Lista los talleres registrados por el usuario (Param - "cUser") 
        /// </summary>
        /// 
        public const string stp_sel_ObtenerCodTaller = "stp_sel_ObtenerCodTaller";
        public const string stp_sel_ObtenerUsuariosTpoEvaluacion = "stp_sel_ObtenerUsuariosTpoEvaluacion"; //Added by TORE, 20210329: adecuacion para el filtro de monitoreo
        public const string stp_sel_ObtenerTallerUsuario = "stp_sel_ObtenerTallerUsuario";
        public const string stp_sel_ObtenerRiesgosTaller = "stp_sel_ObtenerRiesgosTaller";
        public const string stp_ins_AsignarRiesgosTaller = "stp_ins_AsignarRiesgosTaller";
        public const string stp_sel_ObtenerDatosTaller = "stp_sel_ObtenerDatosTaller";
        #endregion

        #region Procedimientos Almacenados - Tipo Evaluación
        public const string stp_sel_ListarTipoEvaluacion = "stp_sel_ListarTipoEvaluacion";

        #endregion

        #region Procedimientos Almacenados - Sub Tipo Evaluacón
        public const string stp_sel_ListarSubTipoEvaluacion = "stp_sel_ListarSubTipoEvaluacion";
        #endregion

        #region Procedimientos Almacenados - Constantes
        public const string stp_sel_ObtenerConstante = "stp_sel_ObtenerConstante";
        public const string stp_sel_ObtenerDescripcionConstante = "stp_sel_ObtenerDescripcionConstante";
        #endregion

        #region Procedimientos Almacenados - ConstSistema
        public const string stp_sel_ObtenerConstSistema = "stp_sel_ObtenerConstSistema";
        #endregion

        #region Procedimiento Almacenados - Causas de los Riesgos
        public const string stp_sel_ObtenerCausasPorRiesgo = "stp_sel_ObtenerCausasPorRiesgo";
        public const string stp_sel_ObtenerCausasRiesgo = "stp_sel_ObtenerCausasRiesgo";
        public const string stp_sel_ListarCausaRiesgo = "stp_sel_ListarCausaRiesgo";
        public const string stp_ins_upd_GestionCausasRiesgo = "stp_ins_upd_GestionCausasRiesgo";



        #endregion

        #region Planes de Accion
        /// <summary>
        /// Obtiene todos los planes de accion asignado al riesgo
        /// </summary>
        public const string stp_sel_ObtenerPlanesAccion = "stp_sel_ObtenerPlanesAccion";
        public const string stp_upd_EliminarPlanAccion = "stp_upd_EliminarPlanAccion";
        /// <summary>
        /// Obtiene la informacion del plan de accion
        /// </summary>
        public const string stp_sel_ObtenerPlanAccionRiesgoPlan = "stp_sel_ObtenerPlanAccionRiesgoPlan";
        public const string stp_sel_MostrarRespndablesPlanesAccion = "stp_sel_MostrarRespndablesPlanesAccion";
        public const string stp_sel_ObtenerResponsablesPlanesAccion = "stp_sel_ObtenerResponsablesPlanesAccion";
        public const string stp_sel_ObtenerResponsablePlanAccion = "stp_sel_ObtenerResponsablePlanAccion";
        public const string stp_sel_GrabarPlanAccionRiesgoOperacional = "stp_sel_GrabarPlanAccionRiesgoOperacional";
        public const string stp_sel_GrabarActualizacionPlanAccion = "stp_sel_GrabarActualizacionPlanAccion";
        public const string stp_ins_GrabarUsuarioResponPlanAccion = "stp_ins_GrabarUsuarioResponPlanAccion";
        public const string stp_ins_GrabarUsuarioReasignadoPlanAccion = "stp_ins_GrabarUsuarioReasignadoPlanAccion";
        public const string stp_upd_QuitarUsuarioResponPlanAccion = "stp_upd_QuitarUsuarioResponPlanAccion";
        public const string stp_sel_ObtenerPlanesAccionAsignados = "stp_sel_ObtenerPlanesAccionAsignados";
        public const string stp_sel_ValidarResponsablesPlanAccion = "stp_sel_ValidarResponsablesPlanAccion";
        public const string stp_upd_ActualizaEstadoPlanAccion = "stp_upd_ActualizaEstadoPlanAccion";
        public const string stp_ins_AgregarFechaImplementacionPlanAccion = "stp_ins_AgregarFechaImplementacionPlanAccion";
        public const string stp_sel_ListaFechasImplementacionPlanAccion = "stp_sel_ListaFechasImplementacionPlanAccion";
        public const string stp_upd_ActualizaFechasImplementacionPlanAccion = "stp_upd_ActualizaFechasImplementacionPlanAccion";
        public const string stp_sel_ComentariosPlanAccion = "stp_sel_ComentariosPlanAccion";
        public const string stp_ins_ComentarioPlanAccion = "stp_ins_ComentarioPlanAccion";
        public const string stp_sel_ObtenerHistorialEstadosPlanAccion = "stp_sel_ObtenerHistorialEstadosPlanAccion";



        #endregion

        #region Procedimientos Almacenados - Escala de los niveles de riesgos
        public const string stp_sel_ObtenerNivelRiesgoEscala = "stp_sel_ObtenerNivelRiesgoEscala";
        public const string stp_sel_ObtenerNivelRiesgoEscalaxProbabilidadImpacto = "stp_sel_ObtenerNivelRiesgoEscalaxProbabilidadImpacto";
        #endregion

        #region Procedimientos Almacenados - Riesgo Inherente
        public const string stp_sel_ObtenerNivelRiesgoProbabilidadImpacto = "stp_sel_ObtenerNivelRiesgoProbabilidadImpacto";
        #endregion

        #region Procedimientos Almacenados - Monto de Perdida
        public const string stp_sel_ObtenerMontoPerdida = "stp_sel_ObtenerMontoPerdida";
        public const string stp_ins_GrabaMontoPerdida = "stp_ins_GrabaMontoPerdida";
        #endregion

        #region Procedimientos Almacenados - Reportes
        public const string stp_sel_UniversoRiesgo = "stp_sel_UniversoRiesgo";
        public const string stp_sel_ObtenerMatrizRiesgo = "stp_sel_ObtenerMatrizRiesgo";
        public const string stp_sel_ObtenerCantidadRiesgoEstado = "stp_sel_ObtenerCantidadRiesgoEstado";
        public const string stp_sel_ObtenerCantidadRiesgoPorNivelRiesgoResidual = "stp_sel_ObtenerCantidadRiesgoPorNivelRiesgoResidual";

        public const string stp_sel_MapaRiesgo = "stp_sel_MapaRiesgo";
        public const string stp_sel_ObtenerRiesgosNivelRiesgo = "stp_sel_ObtenerRiesgosNivelRiesgo";
        public const string stp_sel_ObtenerDetalleRiesgosNivelRiesgo = "stp_sel_ObtenerDetalleRiesgosNivelRiesgo";

        public const string stp_sel_CantidadRiesgoxAnio = "stp_sel_CantidadRiesgoxAnio";
        public const string stp_sel_ProcentajeRegistrosRiesgosCausas = "stp_sel_ProcentajeRegistrosRiesgosCausas";
        public const string stp_sel_RiesgoEstadoProceso = "stp_sel_RiesgoEstadoProceso";
        public const string stp_sel_ObtenerRiesgosFactoRiesgo = "stp_sel_ObtenerRiesgosFactoRiesgo";
        public const string stp_sel_ObtenerPlanesAccionEstado = "stp_sel_ObtenerPlanesAccionEstado";
        public const string stp_sel_ObtenerCantidaRiesgoProceso = "stp_sel_ObtenerCantidaRiesgoProceso";
        public const string stp_sel_PerdidaNetaClaseEventoPerdida = "stp_sel_PerdidaNetaClaseEventoPerdida";
        public const string stp_sel_PerdidaNetaVsMontoBrutoAnio = "stp_sel_PerdidaNetaVsMontoBrutoAnio";
        public const string stp_sel_MontoBrutoVsMontoRecup = "stp_sel_MontoBrutoVsMontoRecup";
        public const string stp_sel_PerdidaNetaVsMontoBrutoAgencia = "stp_sel_PerdidaNetaVsMontoBrutoAgencia";
        public const string stp_sel_CantidadEventoPerdidaAnio = "stp_sel_CantidadEventoPerdidaAnio";
        public const string stp_sel_ObtenerEventoPerdidaValorFiltro = "stp_sel_ObtenerEventoPerdidaValorFiltro";
        public const string stp_sel_ObtenerRiesgosOperacionalesRechazados = "stp_sel_ObtenerRiesgosOperacionalesRechazados";


        //Planes de Accion
        public const string stp_sel_ObtenerNroRoiesgoPlanAccion = "stp_sel_ObtenerNroRoiesgoPlanAccion";


        //Reporte varios
        public const string stp_sel_ObtenerRiesgosOperacionalesProceso = "stp_sel_ObtenerRiesgosOperacionalesProceso";
        #region Riesgo Operacional

        #region Plan de Accion
        public const string stp_sel_ObtenerRiesgoEstadoPlanAccion = "stp_sel_ObtenerRiesgoEstadoPlanAccion";
        //public const string stp_sel_ObtenerPlanesAccionEstado = "stp_sel_ObtenerPlanesAccionEstado";
        #endregion

        #region Incentivo
        public const string stp_sel_ObtenerIncentivoMotivo = "stp_sel_ObtenerIncentivoMotivo";
        public const string stp_sel_ObtenerIncentivosMonto = "stp_sel_ObtenerIncentivosMonto";

        #endregion

        #endregion


        #endregion

        #region  Procedimientos Almacenados - Gestion de Incentivos
        public const string stp_sel_ObtenerRiesgoGestionIncentivos = "stp_sel_ObtenerRiesgoGestionIncentivos";
        public const string stp_sel_ObtenerDetalleIncentivo = "stp_sel_ObtenerDetalleIncentivo";
        public const string stp_ins_GrabarGestionIncentivo = "stp_ins_GrabarGestionIncentivo";
        public const string stp_sel_GrabaConfigIncentivo = "stp_sel_GrabaConfigIncentivo";
        public const string stp_sel_MostrarConfigIncentivo = "stp_sel_MostrarConfigIncentivo";
        #endregion


        #region Evaluaciones
        /// <summary>
        /// Lista los tipos de evaluaciones
        /// </summary>
        public const string stp_sel_ObtenerTiposEvaluacion = "stp_sel_ObtenerTiposEvaluacion";

        public const string stp_ins_GrabarEvalucion = "stp_ins_GrabarEvalucion";
        public const string stp_sel_ListarEvaluacionesGestion = "stp_sel_ListarEvaluacionesGestion";
        public const string stp_sel_ObtenerDetalleEvaluacion = "stp_sel_ObtenerDetalleEvaluacion";
        public const string stp_sel_ObtenerEvaluacionesNotificacion = "stp_sel_ObtenerEvaluacionesNotificacion";
        public const string stp_sel_ObtenerEvaluacionesTaller = "stp_sel_ObtenerEvaluacionesTaller";
        #endregion

        #region Evento de Perdida
        public const string stp_sel_ObtenerClaseEventoPerdida = "stp_sel_ObtenerClaseEventoPerdida";
        public const string stp_sel_ObtenerSubClaseEventoPerdida = "stp_sel_ObtenerSubClaseEventoPerdida";
        public const string stp_sel_ListarEventosPerdida = "stp_sel_ListarEventosPerdida";
        public const string stp_sel_DetalleEventoPerdida = "stp_sel_DetalleEventoPerdida";
        public const string stp_sel_ObtenerSubEventoPerdida = "stp_sel_ObtenerSubEventoPerdida";
        public const string stp_ins_GrabaGestionEventoPerdida = "stp_ins_GrabaGestionEventoPerdida";
        public const string stp_sel_ObtenerCtaCont = "stp_sel_ObtenerCtaCont";
        public const string stp_sel_ObtenerTipoCambioxFecha = "stp_sel_ObtenerTipoCambioxFecha";

        public const string stp_ins_GrabarEventoPerdida = "stp_ins_GrabarEventoPerdida";
        public const string stp_upd_GrabarActulizacionEventoPerdida = "stp_upd_GrabarActulizacionEventoPerdida";
        public const string stp_ins_GrabarCuentasEventoPerdida = "stp_ins_GrabarCuentasEventoPerdida";
        public const string stp_del_EliminarCtaContEventoPerdida = "stp_del_EliminarCtaContEventoPerdida";
        public const string stp_ins_GrabarGastosEvento = "stp_ins_GrabarGastosEvento";
        public const string stp_del_EliminarGastosEventoPerdida = "stp_del_EliminarGastosEventoPerdida";
        public const string stp_sel_ObtenerDetGastoEventoPerdida = "stp_sel_ObtenerDetGastoEventoPerdida";
        public const string stp_sel_ObtenerCtaContablesEventoPerdida = "stp_sel_ObtenerCtaContablesEventoPerdida";
        public const string stp_sel_ListarMatrizEventosPerdida = "stp_sel_ListarMatrizEventosPerdida";

        public const string stp_ins_GrabarEventoPerdidaAgrupado = "stp_ins_GrabarEventoPerdidaAgrupado";
        //public const string stp_ins_GrabarAgrupacionEvento = "stp_ins_GrabarAgrupacionEvento";
        public const string stp_upd_EliminarEventoGrupo = "stp_upd_EliminarEventoGrupo";
        public const string stp_sel_ListaEventoAgrupar = "stp_sel_ListaEventoAgrupar";
        public const string stp_sel_ObtenerAgrupacionEvento = "stp_sel_ObtenerAgrupacionEvento";
        public const string stp_sel_ListarEventoNoGrupo = "stp_sel_ListarEventoNoGrupo";
        public const string stp_sel_VerificarEventoPerdida = "stp_sel_VerificarEventoPerdida";
        #endregion




    }
}
