using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using CMACMaynas.Web.SIRO.Seguridad.Auth.Filters;
using Newtonsoft.Json;
using SIRO.Models;
using SIRO.Utils.Constantes;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SIRO.Controllers
{
    public class ConfiguracionController : Controller
    {
        #region Instancias
        private IMaestroApp maestro;
        private IConstantesApp constante;
        private IAreasApp areas;
        private ICausaRiesgoApp causas;
        private IProcesoAreaApp procesoArea;
        private IControlProcesoApp controlProceso;
        private ISubLineaNegocioApp subLineaNegocio;
        private ISubProcesoApp subProceso;
        private IGestionIncentivoApp gestionIncentivo;
        private ILineaNegocioApp lineaNegocio;
        private ICriterioEvaluacionApp criterioEvaluacion;
        private IMontoPerdidaApp montoPerdida;
        #endregion

        #region Puentes Breadcound
        [RequiresAuthenticationAttribute]
        public ActionResult Home()
        {
            return Elementos();
        }

        #endregion

        public ConfiguracionController(IMaestroApp maestro, IConstantesApp constante, IAreasApp areas, ICausaRiesgoApp causas, IProcesoAreaApp procesoArea,
                                        IControlProcesoApp controlProceso, ISubLineaNegocioApp subLineaNegocio, ISubProcesoApp subProceso,
                                        IGestionIncentivoApp gestionIncentivo, ILineaNegocioApp lineaNegocio, ICriterioEvaluacionApp criterioEvaluacion,
                                        IMontoPerdidaApp montoPerdida)
        {
            this.maestro = maestro;
            this.constante = constante;
            this.areas = areas;
            this.causas = causas;
            this.procesoArea = procesoArea;
            this.controlProceso = controlProceso;
            this.subLineaNegocio = subLineaNegocio;
            this.subProceso = subProceso;
            this.gestionIncentivo = gestionIncentivo;
            this.lineaNegocio = lineaNegocio;
            this.criterioEvaluacion = criterioEvaluacion;
            this.montoPerdida = montoPerdida;
        }



        //[BreadCrumb(Clear = true, Label = "Configuración de Elementos")]
        [RequiresAuthenticationAttribute]
        public ActionResult Elementos()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario.cRHCargoCod != ConstGeneral.Get.AnalistaRO)
                {
                    var error = Utils.Error.GetError.GetErrorModel("Acceso no autorizado", (HttpStatusCode)401, "¡Parece que no deberia estar aqui! Probablemente no posee el cargo autorizado.");
                    return View("Error", error);
                }

                return View();
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        //[BreadCrumb(Clear = true, Label = "Administración de Permisos de Usuarios")]
        [RequiresAuthenticationAttribute]
        public ActionResult AdminUser()
        {
            try
            {
                //ViewBag.LstAreas = areas.ObtenerAreas();
                return View();
                //Deshabilite esta pagina por motivos que los permisos se daran a nivel de grupos
                //var error = Utils.Error.GetError.GetErrorModel("Acceso no autorizado", (HttpStatusCode)404, "¡Parece que no deberia estar aqui! Probablemente no posee el cargo autorizado.");
                //return View("Error", error);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }


        public JsonResult MostrarModelosVista(int pnTab)
        {
            ConfiguracionModel model = new ConfiguracionModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                if (pnTab == 0) //Procesos de areas
                {


                    model.LstAreas = areas.ObtenerAreas();
                    var oProcesoArea = procesoArea.MostrarProcesosAreas(usuario.oAreas.cAreaCod); //(psCodArea = (psCodArea.Length > 0) ? psCodArea : (string)Session["cAreaCod"]);
                    if (oProcesoArea != null)
                    {
                        model.LstProcesosAreas = oProcesoArea;
                    }
                    else { model.LstProcesosAreas = null; }
                }
                else if (pnTab == 1)
                {
                    model.LstAreas = areas.ObtenerAreas();
                    //Se comento por que los controles se haran a partir de la seleccion del proceso
                    //Los controles esta amarrado con los procesos
                    //var oControlArea = ControlesAreasLN.MostrarControlesAreas(usuario.oAreas.cAreaCod);
                    //if (oControlArea != null)
                    //{
                    //model.LstControlesAreas = oControlArea;
                    //}
                    //else { model.LstControlesAreas = null; }
                }
                else if (pnTab == 2)
                {
                    var oCausasRiesgos = causas.MostrarCausasRiesgo();
                    if (oCausasRiesgos != null)
                    {
                        model.LstCausasRiesgos = oCausasRiesgos;
                    }
                    else { model.LstCausasRiesgos = null; }
                }
                else if (pnTab == 3)
                {
                    model.LstProbabilidad = constante.ObtenerConstantes(1012);
                    model.LstImpacto = constante.ObtenerConstantes(1011);
                }
                else if (pnTab == 4)
                {
                    model.LstCriteriosEval = criterioEvaluacion.ObtenerValorCriteriosEval();
                }
                else if (pnTab == 5) //Configuracion de Incentivos
                {
                    model.LstProbabilidad = constante.ObtenerConstantes(1012);
                    model.LstImpacto = constante.ObtenerConstantes(1011);
                    model.LstConfigIncentivos = gestionIncentivo.MostrarConfigMontoIncentos();
                }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        #region Procesos Areas

        public JsonResult ProcesosAreas(string psCodArea)
        {
            ConfiguracionModel model = new ConfiguracionModel();
            try
            {
                var oProcesoArea = procesoArea.MostrarProcesosAreas(psCodArea);
                if (oProcesoArea != null)
                {
                    model.LstProcesosAreas = oProcesoArea;
                }
                else
                {
                    model.LstProcesosAreas = null;
                }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult RegistrarGestionProcesoArea(string psAreaCod, string psNombreProceso = "", int pnProceso = 0)
        {
            // ProcesosAreasModel model = new ProcesosAreasModel();
            ConfiguracionModel model = new ConfiguracionModel();

            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                var exito = procesoArea.RegistrarGestionProcesoArea(psAreaCod, lsNroRiesgo, psNombreProceso, pnProceso);
                if (exito > 0)
                {
                    var oProcesoArea = procesoArea.MostrarProcesosAreas(psAreaCod); //(psCodArea = (psCodArea.Length > 0) ? psCodArea : (string)Session["cAreaCod"]);
                    if (oProcesoArea != null)
                    {
                        model.LstProcesosAreas = oProcesoArea;
                    }
                    else { model.LstProcesosAreas = null; }
                }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }
        #endregion

        #region Sub Procesos Areas
        public JsonResult SubProcesosAreas(string psCodProceso)
        {
            SubProcesosModel model = new SubProcesosModel();
            try
            {
                var oSubProcesos = subProceso.MostarSubprocesosAreas(psCodProceso);
                if (oSubProcesos != null)
                {
                    model.oLstSubprocesos = oSubProcesos;
                }
                else
                {
                    model.oLstSubprocesos = null;
                }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }

        public JsonResult RegistrarGestionSubProcesoAreas(string psCodProceso = "", string psDescSubProceso = "", string psAbreviatura = "", int pnIdSubProceso = 0, int pnAccion = 0)
        {
            SubProcesosModel model = new SubProcesosModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                var lnExito = subProceso.RegistrarGestionSubProcesosAreas(psCodProceso, psDescSubProceso, psAbreviatura, lsNroRiesgo, pnIdSubProceso, pnAccion);

                if (lnExito > 0)
                {
                    var oSubProcesos = subProceso.MostarSubprocesosAreas(pnAccion == 1 || pnAccion == 2 || pnAccion == 3 ? psCodProceso : psCodProceso);
                    if (oSubProcesos != null)
                    {
                        model.oLstSubprocesos = oSubProcesos;
                    }
                    else { model.oLstSubprocesos = null; }
                }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        #endregion

        #region  Control Procesos
        public JsonResult MostrarControlProceso(string psCodProceso = "")
        {
            ControlProcesoModel model = new ControlProcesoModel();
            try
            {
                var oControlProceso = controlProceso.MostrarControlesProceso(psCodProceso);
                if (oControlProceso != null)
                {
                    model.LstControlProceso = oControlProceso;
                }
                else { model.LstControlProceso = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult ObtenerProcesoAreas(string psCodArea = "")
        {
            ProcesosAreasModel model = new ProcesosAreasModel();
            try
            {
                var oProcesoArea = procesoArea.ObtenerProcesoAreas(psCodArea);
                if (oProcesoArea != null)
                {
                    model.LstProcesosAreas = oProcesoArea;
                }
                else { model.LstProcesosAreas = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult RegistrarGestionControlesProceso(string psCodProceso, string psControlDesc, long pnCodControl, int pnAccion)
        {
            ControlProcesoModel model = new ControlProcesoModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);
                //int exito = 0;
                var exito = controlProceso.RegistraGestionControlProceso(psCodProceso, psControlDesc, lsNroRiesgo, pnCodControl, pnAccion);
                if (exito > 0)
                {
                    var oControlArea = controlProceso.MostrarControlesProceso(psCodProceso);
                    if (oControlArea != null)
                    {
                        model.LstControlProceso = oControlArea;
                    }
                    else { model.LstControlProceso = null; }
                }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }
        #endregion

        #region Causas Riesgos
        //public JsonResult CausasRiesgos()
        //{
        //    CausasRiesgoModel model = new CausasRiesgoModel();
        //    try
        //    {
        //        var oCausasRiesgos = CausasRiesgoLN.MostrarCausasRiesgo();
        //        if (oCausasRiesgos != null)
        //        {
        //            model.LstCausasRiesgos = oCausasRiesgos;
        //        }
        //        else { model.LstCausasRiesgos = null; }

        //        return Json(JsonConvert.SerializeObject(model));
        //    }
        //    catch { throw; }
        //}

        public JsonResult RegistrarGestionCausasRiesgo(string psCodCausa, string psCausaDesc, int pnAccion = 0)
        {
            CausasRiesgoModel model = new CausasRiesgoModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                var lnExito = causas.RegistrarGestionCausaRiesgo(psCodCausa, psCausaDesc, lsNroRiesgo, pnAccion);

                if (lnExito > 0)
                {
                    var oCausasRiesgos = causas.MostrarCausasRiesgo();
                    if (oCausasRiesgos != null)
                    {
                        model.LstCausasRiesgos = oCausasRiesgos;
                    }
                    else { model.LstCausasRiesgos = null; }
                }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        #endregion

        #region Monto de Perdida
        public JsonResult GrabarNuevoMontoPerdida(int pnProbabilidad, int pnImpacto, decimal pnMontoPerdida, string psComentarios)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                var exito = montoPerdida.GrabaMontoPerdida(pnProbabilidad, pnImpacto, pnMontoPerdida, psComentarios, lsNroRiesgo);
                if (exito > 0)
                {
                    var montoPerdidas = montoPerdida.ObtenerMontoPerdida();
                    return Json(JsonConvert.SerializeObject(new ResponseModel()
                    {
                        oLstObjeto = montoPerdidas.Where(x => x.nProbabilidad == pnProbabilidad && x.nImpacto == pnImpacto).ToList<object>(),
                        TipoNotif = TiposNotificacion.informacion.ToString(),
                        MensajeNotif = "Se actualizo el monto de pérdida"
                    }));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject(new ResponseModel()
                    {
                        TipoNotif = TiposNotificacion.informacion.ToString(),
                        MensajeNotif = "No se actualizo el monto de pérdida, Favor de comunicar a TI"
                    }));
                }

            }
            catch {
                return Json(JsonConvert.SerializeObject(new ResponseModel()
                {
                    TipoNotif = TiposNotificacion.error.ToString(),
                    MensajeNotif = "Lo sentimos experimentamos problemas con el aplicativo. Por favor de comunicar al Departamento TI."
                }));
            }
        }

        #endregion

        public JsonResult LineaNegocio()
        {
            LineaNegocioModel model = new LineaNegocioModel();
            try
            {
                //return Json(JsonConvert.SerializeObject(LineaNegocioLN.ObtenerLineaNegocio()));
                var oLineaNegocio = lineaNegocio.ObtenerLineaNegocio();
                if (oLineaNegocio != null)
                {
                    model.oLstLineaNegocio = oLineaNegocio;
                }
                else { model.oLstLineaNegocio = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult SubLineaNegocio(string psCodLineaNegocio)
        {
            SubLineaNegocioModel model = new SubLineaNegocioModel();
            try
            {
                var oSubLineaNegocio = subLineaNegocio.ObtenerSubLineaNegocio(psCodLineaNegocio);
                if (oSubLineaNegocio != null)
                {
                    model.oListSubLineaNegocio = oSubLineaNegocio;
                }
                else { model.oListSubLineaNegocio = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult ObtenerProducto(string psCodLineaNegocio)
        {
            SubLineaNegocioModel model = new SubLineaNegocioModel();
            try
            {
                var oSubLineaNegocio = subLineaNegocio.ObtenerSubLineaNegocio(psCodLineaNegocio);
                if (oSubLineaNegocio != null)
                {
                    model.oListSubLineaNegocio = oSubLineaNegocio;
                }
                else { model.oListSubLineaNegocio = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        #region Criterios de Evaluacion
        public JsonResult ObtenerValorCriteriosEval()
        {
            CriteriosEvaluacionModel model = new CriteriosEvaluacionModel();
            try
            {
                var oCriteriosEval = criterioEvaluacion.ObtenerValorCriteriosEval();
                if (oCriteriosEval != null)
                {
                    model.oLstCriteriosEval = oCriteriosEval;
                }
                else { model.oLstCriteriosEval = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult GrabarConfiguracionCriterioEval(string poConfigCriterioEval)
        {
            CriteriosEvaluacionModel model = new CriteriosEvaluacionModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                dynamic d = JsonConvert.DeserializeObject(poConfigCriterioEval);
                List<dynamic> configCriterioEval = new List<dynamic>(d);

                var lnExito = criterioEvaluacion.GrabaConfigCriterioEval(configCriterioEval, lsNroRiesgo);

                model.oLstCriteriosEval = criterioEvaluacion.ObtenerValorCriteriosEval();

                return Json(new { Exito = lnExito, Data = JsonConvert.SerializeObject(model) });
            }
            catch { throw; }
        }



        #endregion

        #region Configuracion de Incentivos
        public JsonResult MostrarConfigIncentivo()
        {
            ConfiguracionModel model = new ConfiguracionModel();
            try
            {
                var LstConfigIncentivo = gestionIncentivo.MostrarConfigMontoIncentos();
                if (LstConfigIncentivo != null)
                {
                    model.LstConfigIncentivos = LstConfigIncentivo;
                }
                else { model.LstConfigIncentivos = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        public JsonResult GrabaConfigIncentivo(int pnProbabilidad, int pnImpacto, decimal psMontoInc)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                var oMensajeConfig = gestionIncentivo.GrabaConfigIncentivo(pnProbabilidad, pnImpacto, psMontoInc, lsNroRiesgo);

                return Json(new { Mensaje = oMensajeConfig });
            }
            catch { throw; }
        }



        #endregion





    }
}