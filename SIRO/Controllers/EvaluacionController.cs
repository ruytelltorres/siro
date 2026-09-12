using ClosedXML.Excel;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using CMACMaynas.Web.SIRO.Seguridad.Auth.Filters;
using Newtonsoft.Json;
using SIRO.Models;
using SIRO.Utils.Constantes;
using SIRO.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SIRO.Controllers
{
    public class EvaluacionController : Controller
    {

        #region Instancias
        private readonly IMaestroApp maestro;
        private readonly IConstantesApp constante;
        private readonly IConstSistemaApp constSistema;
        private readonly IAgenciasApp agencias;
        private readonly IAreasApp areas;
        private readonly ICausaRiesgoApp causas;
        private readonly IPlanAccionApp planAccion;
        private readonly IRiesgoResidualApp riesgoResidual;
        private readonly IRiesgoInherenteApp riesgoInherente;
        private readonly IProcesoAreaApp procesoArea;
        private readonly IControlProcesoApp controlProceso;
        private readonly ISubProcesoApp subProceso;
        private readonly IUsuarioApp iusuario;
        private readonly IGestionIncentivoApp gestionIncentivo;
        private readonly ILineaNegocioApp lineaNegocio;
        private readonly ICriterioEvaluacionApp criterioEvaluacion;
        private readonly IProductoApp producto;
        private readonly ISubProductoApp subProducto;
        private readonly IMontoPerdidaApp montoPerdida;
        private readonly ITallerApp taller;
        private readonly IAutoevaluacionApp ievaluacion;
        private readonly IRiesgoOperacionalApp iriesgo;

        public EvaluacionController(IMaestroApp maestro, IConstantesApp constante, IConstSistemaApp constSistema, IAgenciasApp agencias, IAreasApp areas, ICausaRiesgoApp causas, IPlanAccionApp planAccion,
                                    IRiesgoResidualApp riesgoResidual, IRiesgoInherenteApp riesgoInherente, IProcesoAreaApp procesoArea, IControlProcesoApp controlProceso,
                                    ISubProcesoApp subProceso, IUsuarioApp iusuario, IGestionIncentivoApp gestionIncentivo, ILineaNegocioApp lineaNegocio, ICriterioEvaluacionApp criterioEvaluacion,
                                    IProductoApp producto, ISubProductoApp subProducto, IMontoPerdidaApp montoPerdida, ITallerApp taller, IAutoevaluacionApp ievaluacion, IRiesgoOperacionalApp iriesgo)
        {
            this.maestro = maestro;
            this.constante = constante;
            this.constSistema = constSistema;
            this.agencias = agencias;
            this.areas = areas;
            this.causas = causas;
            this.planAccion = planAccion;
            this.riesgoResidual = riesgoResidual;
            this.riesgoInherente = riesgoInherente;
            this.procesoArea = procesoArea;
            this.controlProceso = controlProceso;
            this.subProceso = subProceso;
            this.iusuario = iusuario;
            this.gestionIncentivo = gestionIncentivo;
            this.lineaNegocio = lineaNegocio;
            this.criterioEvaluacion = criterioEvaluacion;
            this.producto = producto;
            this.subProducto = subProducto;
            this.montoPerdida = montoPerdida;
            this.taller = taller;
            this.ievaluacion = ievaluacion;
            this.iriesgo = iriesgo;
        }
        #endregion

        #region Registro de las evaluaciones
        [HttpGet]
        [RequiresAuthenticationAttribute]
        public ActionResult Registrar()
        {
            var model = new GestionRiesgoModel()
            {
                //oLstTipoEvaluacion = ievaluacion.ObtenerTipoEvaluacion(),
                oLstTipoEvaluacion = constante.ObtenerConstantes(Constantes.TiposEvaluacion),
                oLstAreas = areas.ObtenerAreas(),
                oLstCausas = causas.ObtenerCausasRiesgo()
            };

            return View(model);
        }

        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarEvaluacion(int pnCodEvaluacion, string psRiesgoIdentificado, string psCauasRiesgo,
                                              string psAgeCod = "", string psAreaCod = "", string psCodProceos = "", string psCodSubProceso = "")
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                var lsCodRiesgo = ievaluacion.RegistrarEvaluacion(pnCodEvaluacion, psRiesgoIdentificado, psCauasRiesgo, lsNroRiesgo, psAgeCod, psAreaCod, psCodProceos, psCodSubProceso);

                return Json(new { CodRiesgoEval = lsCodRiesgo });
            }
            catch { throw; }
        }

        #endregion

        #region Lista de Evaluaciones
        [RequiresAuthenticationAttribute]
        public ActionResult Lista()
        {
            try
            {
                //GestionRiesgoModel model = new GestionRiesgoModel()
                //{
                //    oLstTipoEvaluacion = constante.ObtenerConstantes(Constantes.TiposEvaluacion),
                //    oLstAreas = areas.ObtenerAreas(),
                //    oLstCausas = causas.ObtenerCausasRiesgo()
                //};
                //ViewBag.Modelo = model;
                return View();
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListaEvaluacionesEnProceso(int pnTpoBuscar, string psValorBuscar)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                var model = new GestionRiesgoModel()
                {
                    oLstDatosEvaluaciones = ievaluacion.MostrarEvaluacionesGestion(pnTpoBuscar, psValorBuscar, usuario.cUser)
                };

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [HttpGet]
        [RequiresAuthenticationAttribute]
        public ActionResult Detalle(string evaluacion)
        {
            try
            {
                var nNroRiesgo = Encripta.base64Decode(evaluacion);
                var detalleAutoevaluacion = ievaluacion.MostrarDetalleEvaluacion((long)Convert.ToInt64(nNroRiesgo));

                return View(detalleAutoevaluacion);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }
        #endregion

        #region Gestion de Evaluaciones
        [HttpGet]
        [RequiresAuthenticationAttribute]
        public ActionResult Gestion(string evaluacion)
        {
            try
            {
                var nNroRiesgo = Encripta.base64Decode(evaluacion);
                var model = new GestionRiesgoModel()
                {
                    oEvaluaciones = ievaluacion.MostrarDetalleEvaluacion((long)Convert.ToInt64(nNroRiesgo))
                };
                return View(model);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        [HttpGet]
        [RequiresAuthenticationAttribute]
        public ActionResult PlanAccion(string evaluacion, long planaccion)
        {
            try
            {
                var nNroRiesgo = Encripta.base64Decode(Convert.ToString(evaluacion));
                var model = new GestionRiesgoModel()
                {
                    oLstAreas = areas.ObtenerAreas(),
                    oPlanAccion = planAccion.ObtenerPlanAccionRiesgoPlan(Convert.ToInt64(nNroRiesgo), planaccion),
                    oLstReponPlanAccion = planAccion.MostrarDetalleResponsablesPlanesAccionRiesgo(Convert.ToInt64(nNroRiesgo), planaccion),
                    lstObservacionesGestion = planAccion.ObtenerFechasImplementacionPlanAccion(planaccion)
                };

                return View(model);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ActualizaFechaImplementacion(long pnCodPlan)
        {
            try
            {
                return Json(new { Exito = planAccion.ActualizaFechasImplementacionPlanAccion(pnCodPlan) });
            }
            catch { throw; }
        }

        /// <summary>
        /// Agrega las fechas de implementacion para los planes de accion
        /// </summary>
        /// <param name="pdFechaImplementacion"></param>
        /// <returns></returns>
        /// 
        [RequiresAuthenticationAttribute]
        public async Task<JsonResult> AgregarFechaImplementacion(long pnCodPlan, string pdFechaImplementacion, int pbImplementado)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                if (usuario.cRHCargoCod != ConstGeneral.Get.AnalistaRO)
                {
                    return Json(new { Mensaje = "Ud. no posee la elevación necesaria para realizar esta acción", Tipo = "advertencia" });
                }

                string[] respuesta = planAccion.AgregarFechasImplementacion(pnCodPlan, pdFechaImplementacion, Convert.ToBoolean(pbImplementado), lsNroRiesgo);
                string tipo = respuesta[(int)Mensaje.TipoMensaje];
                string mensaje = respuesta[(int)Mensaje.Mensaje];
                string emiteCorreo = respuesta[2];

                if (!String.IsNullOrEmpty(emiteCorreo) && !Convert.ToBoolean(pbImplementado))
                {
                    var configMail = new MailModel()
                    {
                        Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor,
                        Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor,
                        Pruebas = Convert.ToBoolean(Convert.ToInt32(constSistema.ObtenerConstanteSistema(200).cConsSisValor)),
                        CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor
                    };

                    List<string> mailDestino = new List<string>(), mailCC = null;
                    string lsAsunto = "", lsTitulo = "", lsContenido = "", lsFechas = "";
                    var oResponsables = planAccion.ObtenerResponsablePlanAccion(pnCodPlan);
                    for (int i = 0; i < oResponsables.Count; i++)
                    {
                        mailDestino.Add(SendMail.Get.ObtenerMailUsuario(oResponsables[i].cUserResponsable));
                    }

                    mailCC = new List<string>() { SendMail.Get.ObtenerMailUsuario(usuario.cUser) };

                    var oLstFechasImplementa = planAccion.ObtenerFechasImplementacionPlanAccion(pnCodPlan);
                    lsFechas += "<p>";
                    foreach (var fechas in oLstFechasImplementa)
                    {

                        lsFechas += fechas.FechaImplementa + new string(' ', 3) + "<strong>(" + fechas.Estado + ")</strong> </br>";
                    }
                    lsFechas += "</p>";
                    lsAsunto = "Notificación - Ampliación de Fecha de Implementación";

                    lsTitulo = "Plan de Acción " + Convert.ToString(pnCodPlan);
                    lsContenido += "Se informa que el Analista de Riesgo Operacional <strong>" + usuario.oPersona.cPersNombre + "</strong>, " +
                        "realizó la ampliación de la fecha de implementación del plan de acción código <strong>" + Convert.ToString(pnCodPlan) + "</strong>, " +
                        "el cual se detalla a continuación: <br/>" +
                        "<p><strong>Descripción Plan Acción</strong>" +
                        "<p>" + planAccion.ObtenerPlanAccionRiesgoPlan(0, pnCodPlan).cPlanDescripcion + "</p>" +
                        "<p><strong>Fecha(s) Programada(s)</strong>" + lsFechas;

                    var lsCorreo = ConstGeneral.Get.Correo(lsTitulo, lsContenido);

                    await SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC, String.Empty, configMail);

                }

                return Json(new { Mensaje = mensaje, Tipo = tipo });
            }
            catch { throw; }
        }

        //public JsonResult AgregarFechaImplementacion(long pnCodPlan, string pdFechaImplementacion, int pbImplementado)
        //{
        //    try
        //    {
        //        Usuario usuario = (Usuario)Session["Usuario"];
        //        string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

        //        if (usuario.cRHCargoCod != ConstGeneral.Get.AnalistaRO)
        //        {
        //            return Json(new { Mensaje = "Ud. no posee la elevación necesaria para realizar esta acción", Tipo = "advertencia" });
        //        }

        //        string[] respuesta = planAccion.AgregarFechasImplementacion(pnCodPlan, pdFechaImplementacion, Convert.ToBoolean(pbImplementado), lsNroRiesgo);
        //        string tipo = respuesta[(int)Mensaje.TipoMensaje];
        //        string mensaje = respuesta[(int)Mensaje.Mensaje];
        //        string emiteCorreo = respuesta[2];

        //        if (!String.IsNullOrEmpty(emiteCorreo) && !Convert.ToBoolean(pbImplementado))
        //        {
        //            var configMail = new MailModel()
        //            {
        //                Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor,
        //                Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor,
        //                Pruebas = Convert.ToBoolean(Convert.ToInt32(constSistema.ObtenerConstanteSistema(200).cConsSisValor)),
        //                CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor
        //            };

        //            List<string> mailDestino = new List<string>(), mailCC = null;
        //            string lsAsunto = "", lsTitulo = "", lsContenido = "", lsFechas = "";
        //            var oResponsables = planAccion.ObtenerResponsablePlanAccion(pnCodPlan);
        //            for (int i = 0; i < oResponsables.Count; i++)
        //            {
        //                mailDestino.Add(SendMail.Get.ObtenerMailUsuario(oResponsables[i].cUserResponsable));
        //            }

        //            mailCC = new List<string>() { SendMail.Get.ObtenerMailUsuario(usuario.cUser) };

        //            var oLstFechasImplementa = planAccion.ObtenerFechasImplementacionPlanAccion(pnCodPlan);
        //            lsFechas += "<p>";
        //            foreach (var fechas in oLstFechasImplementa)
        //            {

        //                lsFechas += fechas.FechaImplementa + new string(' ', 3) + "<strong>(" + fechas.Estado + ")</strong> </br>";
        //            }
        //            lsFechas += "</p>";
        //            lsAsunto = "Notificación - Ampliación de Fecha de Implementación";

        //            lsTitulo = "Plan de Acción " + Convert.ToString(pnCodPlan);
        //            lsContenido += "Se informa que el Analista de Riesgo Operacional <strong>" + usuario.oPersona.cPersNombre + "</strong>, " +
        //                "realizó la ampliación de la fecha de implementación del plan de acción código <strong>" + Convert.ToString(pnCodPlan) + "</strong>, " +
        //                "el cual se detalla a continuación: <br/>" +
        //                "<p><strong>Descripción Plan Acción</strong>" +
        //                "<p>" + planAccion.ObtenerPlanAccionRiesgoPlan(0, pnCodPlan).cPlanDescripcion + "</p>" +
        //                "<p><strong>Fecha(s) Programada(s)</strong>" + lsFechas;

        //            var lsCorreo = ConstGeneral.Get.Correo(lsTitulo, lsContenido);
        //            _ = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC, String.Empty, configMail);

        //        }

        //        return Json(new { Mensaje = mensaje, Tipo = tipo });
        //    }
        //    catch { throw; }
        //}

        [RequiresAuthenticationAttribute]
        public JsonResult ActualizarEstadoPlanAccion(long pnPlanCod, int pnEstado, string psDesEstado = "")
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                string xTpoMensaje = "", xMensaje = "", xNroRiesgo = "", xValRespuesta = "";


                string[] respuesta = planAccion.ActualizarEstadoPlanAccion(pnPlanCod, pnEstado, lsNroRiesgo);
                xTpoMensaje = respuesta[(int)Mensaje.TipoMensaje];
                xMensaje = respuesta[(int)Mensaje.Mensaje];
                xNroRiesgo = respuesta[2];
                xValRespuesta = respuesta[3];

                string lsContenido = "", lsTitulo = "Riesgo Finalizado", lsAsunto = "Finalización del Riesgo";
                List<string> mailDestino = null, mailCC = null;
                int tpoRiesgo = iriesgo.ObtenerTpoRiesgo((long)Convert.ToInt64(xNroRiesgo)); //Modified by TORE 20210324: adecuacion para el caso de confirmacion de los planes de accion, 
                //var oDataRiesgo = tpoRiesgo == (int)Riesgos.RiesgoOperacional ?
                //                    iriesgo.ObtenerInfoGeneralRiesgoOperacional(new Riesgo() {
                //                        nNroRiesgo = (long)Convert.ToInt64(xNroRiesgo)
                //                    }) :
                //                    ievaluacion.MostrarDetalleEvaluacion((long)Convert.ToInt64(xNroRiesgo));

                var oDataRiesgo =
                                   iriesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo()
                                   {
                                       nNroRiesgo = (long)Convert.ToInt64(xNroRiesgo)
                                   }) ;

                var configMail = new MailModel()
                {
                    Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor,
                    Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor,
                    Pruebas = Convert.ToBoolean(Convert.ToInt32(constSistema.ObtenerConstanteSistema(200).cConsSisValor)),
                    CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor
                };

                if (xValRespuesta == "xFinaliza")
                {
                    if (usuario.cRHCargoCod == ConstGeneral.Get.AnalistaRO)
                    {
                        lsContenido += "Se informa que el analista de Riesgo Operacional <strong>" + usuario.cUser.ToUpper() + "</strong>, dió por finalizado" +
                            " la gestión del riesgo operacional con código: <strong>" + oDataRiesgo.cCodRiesgo + "</strong>, el cuál presenta la siguiente descripción." +
                            "<p><strong>Riesgo Identificado:</strong><br/>" + oDataRiesgo.cRiesgoIdentiticado + "</p>";

                        var oPlanesAccion = planAccion.MostrarPlanesAccionRiesgo((long)Convert.ToInt64(xNroRiesgo)); //Buscamos los planes de accion asignados al riesgo

                        mailDestino = new List<string>();
                        for (int i = 0; i < oPlanesAccion.Count; i++) //Obtenemos todos los responsables de los planes de accion del riesgo
                        {
                            var oResponsablePlan = planAccion.ObtenerResponsablePlanAccion(oPlanesAccion[i].nPlanCod);
                            for (int j = 0; j < oResponsablePlan.Count; j++)
                            {
                                mailDestino.Add(SendMail.Get.ObtenerMailUsuario(oResponsablePlan[j].cUserResponsable));
                            }
                        }
                        mailCC = new List<string>() { SendMail.Get.ObtenerMailUsuario(usuario.cUser) };
                    }
                    else
                    {
                        mailDestino = new List<string>();
                        var oAnaRiesgo = iusuario.ObtenerUsuariosCargos(ConstGeneral.Get.AnalistaRO);
                        for (int i = 0; i < oAnaRiesgo.Count; i++)
                        {
                            mailDestino.Add(SendMail.Get.ObtenerMailUsuario(oAnaRiesgo[i].cUser));
                        }
                        lsContenido += "Se informa que el usuario <strong>" + usuario.cUser.ToUpper() + "</strong>, cambio de estado al plan de acción código : " + Convert.ToString(pnPlanCod);
                        mailCC = new List<string>() { SendMail.Get.ObtenerMailUsuario(usuario.cUser) };
                    }

                    var lsCorreo = ConstGeneral.Get.Correo(lsTitulo, lsContenido);
                    _ = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC, String.Empty, configMail);
                }
                else
                {
                    mailDestino = new List<string>();
                    if (pnEstado == 2 && tpoRiesgo == (int)Riesgos.RiesgoOperacional)
                    {
                        var oAnaRiesgo = iusuario.ObtenerUsuariosCargos(ConstGeneral.Get.AnalistaRO);
                        for (int i = 0; i < oAnaRiesgo.Count; i++)
                        {
                            mailDestino.Add(SendMail.Get.ObtenerMailUsuario(oAnaRiesgo[i].cUser));
                        }

                        mailCC = new List<string>() { SendMail.Get.ObtenerMailUsuario(usuario.cUser) };

                        var _PlanAccion = planAccion.ObtenerPlanAccionRiesgoPlan((long)Convert.ToInt32(xNroRiesgo), pnPlanCod);
                        lsAsunto = "Confirmación Plan Acción " + Convert.ToString(pnPlanCod);
                        lsTitulo = "RESPONSABLE: " + usuario.oPersona.cPersNombre;
                        lsContenido += "Se informa que el usuario <strong>[" + usuario.cUser.ToUpper() + "]-" + usuario.oPersona.cPersNombre + "</strong>, realizó la confirmación del plan de acción código <strong>" + Convert.ToString(pnPlanCod) + "</strong> " +
                            "el cual detalla lo siguiente: <br/>" +
                            "<p><strong>Descripción Plan Acción</strong><br/>" +
                            _PlanAccion.cPlanDescripcion + "</p>" +
                            "<p><strong>Fecha de Implementación</strong><br/>" +
                            Convert.ToDateTime(_PlanAccion.dFechaImplement).ToString("dd/MM/yyyy");

                        var lsCorreo = ConstGeneral.Get.Correo(lsTitulo, lsContenido);
                        _ = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC, String.Empty, configMail);
                    }
                }

                return Json(new { TpoMensaje = xTpoMensaje, Mensaje = xMensaje, ValRespuesta = xValRespuesta });
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult EstadosPlanAccion(long pnPlanCod)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                var Estados = constante.ObtenerConstantes(3000);
                var oEstados = new ReportGrafModel()
                {
                    data = Estados.Select(r => new
                    {
                        text = Convert.ToString(r.cConsDescripcion),
                        value = Convert.ToString(r.nConsValor),
                    }).ToList<object>(),
                };

                if (usuario.cRHCargoCod != ConstGeneral.Get.AnalistaRO)
                { //Solo los analista s de riesgos operacional podran realizar el rechazo o eliminacion del riesgo
                    oEstados.data.RemoveRange(3, 2); //Rechazado, Eliminado
                }

                return Json(new { EstadosPlanAccion = JsonConvert.SerializeObject(oEstados) });
            }
            catch { throw; }
        }

        #endregion

        #region Notificar Evaluaciones
        [HttpGet]
        [RequiresAuthenticationAttribute]
        public ActionResult Notificar()
        {
            try
            {

                return View();
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListaEvaluacionesNotificar()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                var model = new GestionRiesgoModel()
                {
                    oLstDatosEvaluaciones = ievaluacion.MostrarEvaluacionNotificacion(usuario.cUser)
                };

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult EnviarNotificionRiesgos(string psRiesgos)
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

            var lsCodTaller = taller.ObtenerTaller(usuario.oAreas.cAreaCod);

            //string Resultado = taller.AsignarRiesgosTaller(psRiesgos, lsCodTaller, lsNroRiesgo);
            //string cTpoMensaje = Resultado.Substring(0, Resultado.IndexOf("-"));
            //string cMensaje = Resultado.Replace((cTpoMensaje + "-"), "");

            string[] asigna = taller.AsignarRiesgosTaller(psRiesgos, lsCodTaller, lsNroRiesgo);
            //string cTpoMensaje = Resultado.Substring(0, Resultado.IndexOf("-"));
            //string cMensaje = Resultado.Replace((cTpoMensaje + "-"), "");
            var responseModel = new ResponseModel()
            {
                MensajeNotif = asigna[(int)Mensaje.Mensaje],
                TipoNotif = asigna[(int)Mensaje.TipoMensaje]
            };

            string lsListaAnalistas = "Lista de Analistas";

            if (responseModel.TipoNotif == "exito")
            {
                var oAnaRiesgo = iusuario.ObtenerUsuariosCargos(ConstGeneral.Get.AnalistaRO);
                var configMail = new MailModel()
                {
                    Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor,
                    Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor,
                    Pruebas = Convert.ToBoolean(Convert.ToInt32(constSistema.ObtenerConstanteSistema(200).cConsSisValor)),
                    CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor
                };
                List<string> mailDestino = new List<string>();
                for (int i = 0; i < oAnaRiesgo.Count; i++)
                { //Se hace el recorrido por si existen varios analistas de riesgos
                  //lsCorreoDestino += SendMail.ObtenerMailUsuario(oAnaRiesgo[i].cUser) + ";";
                    mailDestino.Add(SendMail.Get.ObtenerMailUsuario(oAnaRiesgo[i].cUser));
                    lsListaAnalistas += oAnaRiesgo[i].cUsuario + ",";
                }

                List<string> mailCC = new List<string>() { SendMail.Get.ObtenerMailUsuario(usuario.cUser) };
                var lsAsunto = string.Concat("Notificación Autoevaluación - Taller ", lsCodTaller);
                var lsTitulo = string.Concat("Taller: ", lsCodTaller);
                var lsContenido = "Se informa que el usuario [" + usuario.cUser + "]-" + usuario.oPersona.cPersNombre + ", resgitró y gestionó las autoevaluaciones " +
                                   "asignados al taller código <strong>" + lsCodTaller + "</strong>, el cual se detalla a continuación:<br/>" +
                                  "<p><strong>Datalle autoevaluaciones</strong></p><br/>";
                var oRiesgosTaller = taller.ObtenerRiesgosTaller(lsCodTaller);
                lsContenido += "<table style= 'color: #153643; border:1px solid #cccccc ;border-collapse: collapse; font-family: Arial, sans-serif; font-size: 12px;'>" +
                    "<tr style='background-color: #f1f1f1;'>" +
                    "<th style='padding: 10px;'><strong>Código</strong></th>" +
                    "<th style='padding: 10px;'><strong>Fecha Creación</strong></th>" +
                    "<th style='padding: 10px;'><strong>Tipo</strong></th>" +
                    "</tr>";
                for (int i = 0; i < oRiesgosTaller.Count; i++)
                {
                    lsContenido += "<tr>" +
                        "<td style='padding: 10px;'>" + oRiesgosTaller[i].oAutoevaluacion.cCodRiesgo + "</td>" +
                        "<td style='padding: 10px; text-align: center'>" + oRiesgosTaller[i].oAutoevaluacion.dFechaRegistro + "</td>" +
                        "<td style='padding: 10px; text-align: center'>" + oRiesgosTaller[i].oAutoevaluacion.oTipoEvaluacion.cConsDescripcion + "</td>" +
                        "</tr>";
                }
                lsContenido += "</table>";

                var lsCorreo = ConstGeneral.Get.Correo(lsTitulo, lsContenido);
                //var CorreoEnviado = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC);
                _ = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC, string.Empty, configMail);
            }

            return Json(new { response = responseModel, ListaAnalista = lsListaAnalistas });
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerUsuarioTpoEvaluacion(string psCodTpoEval)
        {
            try
            {
                var LstTallerUsuario = taller.ObtenerUsuarioCodTpoEvaluacion(psCodTpoEval);
                return Json(new { ListaUsuarios = LstTallerUsuario });
            }
            catch { throw; }

        }

        [RequiresAuthenticationAttribute]
        //public JsonResult ObtenerTallerUsuario(string psCodTpoEval, string psUser, string psEstado)
        public JsonResult ObtenerTallerUsuario(string psCodTpoEval)
        {
            try
            {
                List<Taller> LstTallerUsuario = new List<Taller>();
                //var TallerUsuario = taller.ObtenerTallerUsuario(psCodTpoEval, psUser, psEstado);
                var TallerUsuario = taller.ObtenerTallerUsuario(psCodTpoEval);
                if (TallerUsuario != null)
                {
                    LstTallerUsuario = TallerUsuario;
                }
                else { LstTallerUsuario = null; }

                return Json(new { Talleres = LstTallerUsuario });
            }
            catch { throw; }

        }

        [RequiresAuthenticationAttribute]
        public ActionResult ExportarAutoevaluacionNotificar()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoAutoEvaluacionNotificar.xlsx"), XLEventTracking.Disabled);
                IXLWorksheet ws = wb.Worksheet(1);

                int fila = 4;
                int[] matBucles = { 0 }; /*(0) - Causas*/

                var notificar = ievaluacion.MostrarEvaluacionNotificacion(usuario.cUser);
                foreach (var autoeval in notificar)
                {
                    var detalle = ievaluacion.MostrarDetalleEvaluacion(autoeval.nNroRiesgo);

                    ws.Cell(string.Concat("A", fila)).Value = detalle.cCodRiesgo;
                    ws.Cell(string.Concat("A", fila)).Style.Alignment.WrapText = true;
                    ws.Cell(string.Concat("A", fila)).Style.Font.FontName = "Segoe UI";
                    ws.Cell(string.Concat("A", fila)).Style.Font.FontSize = 9;
                    ws.Cell(string.Concat("A", fila)).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell(string.Concat("A", fila)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell(string.Concat("A", fila)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell(string.Concat("B", fila)).Value = detalle.cRiesgoIdentiticado;
                    ws.Cell(string.Concat("B", fila)).Style.Alignment.WrapText = true;
                    ws.Cell(string.Concat("B", fila)).Style.Font.FontName = "Segoe UI";
                    ws.Cell(string.Concat("B", fila)).Style.Font.FontSize = 9;
                    ws.Cell(string.Concat("B", fila)).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell(string.Concat("B", fila)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                    ws.Cell(string.Concat("B", fila)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell(string.Concat("C", fila)).Value = detalle.oTipoEvaluacion.cConsDescripcion;
                    ws.Cell(string.Concat("C", fila)).Style.Alignment.WrapText = true;
                    ws.Cell(string.Concat("C", fila)).Style.Font.FontName = "Segoe UI";
                    ws.Cell(string.Concat("C", fila)).Style.Font.FontSize = 9;
                    ws.Cell(string.Concat("C", fila)).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell(string.Concat("C", fila)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell(string.Concat("C", fila)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    switch (autoeval.oTipoEvaluacion.nConsValor)
                    {
                        case (int)TipoEvaluaciones.Procesos:

                            ws.Cell(string.Concat("D", fila)).Value = detalle.oAgencia.oArea.cAreaDescripcion;
                            ws.Cell(string.Concat("D", fila)).Style.Alignment.WrapText = true;
                            ws.Cell(string.Concat("D", fila)).Style.Font.FontName = "Segoe UI";
                            ws.Cell(string.Concat("D", fila)).Style.Font.FontSize = 9;
                            ws.Cell(string.Concat("D", fila)).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            ws.Cell(string.Concat("D", fila)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(string.Concat("D", fila)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell(string.Concat("E", fila)).Value = detalle.oProceso.cDescProceso;
                            ws.Cell(string.Concat("E", fila)).Style.Alignment.WrapText = true;
                            ws.Cell(string.Concat("E", fila)).Style.Font.FontName = "Segoe UI";
                            ws.Cell(string.Concat("E", fila)).Style.Font.FontSize = 9;
                            ws.Cell(string.Concat("E", fila)).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            ws.Cell(string.Concat("E", fila)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(string.Concat("E", fila)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell(string.Concat("F", fila)).Value = detalle.oProceso.oSubProceso.cDescSubProceso;
                            ws.Cell(string.Concat("F", fila)).Style.Alignment.WrapText = true;
                            ws.Cell(string.Concat("F", fila)).Style.Font.FontName = "Segoe UI";
                            ws.Cell(string.Concat("F", fila)).Style.Font.FontSize = 9;
                            ws.Cell(string.Concat("F", fila)).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            ws.Cell(string.Concat("F", fila)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                            ws.Cell(string.Concat("F", fila)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            break;
                        case (int)TipoEvaluaciones.Areas:

                            ws.Cell(string.Concat("D", fila)).Value = detalle.oAgencia.oArea.cAreaDescripcion;
                            ws.Cell(string.Concat("D", fila)).Style.Alignment.WrapText = true;
                            ws.Cell(string.Concat("D", fila)).Style.Font.FontName = "Segoe UI";
                            ws.Cell(string.Concat("D", fila)).Style.Font.FontSize = 9;
                            ws.Cell(string.Concat("D", fila)).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            ws.Cell(string.Concat("D", fila)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(string.Concat("D", fila)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            break;
                    }

                    var detCausas = causas.ObtenerCausaPorRiesgo(autoeval.nNroRiesgo);
                    if (detCausas.Count > 0)
                    {
                        int filaCausas = fila;
                        foreach (var det in detCausas)
                        {
                            ws.Cell(string.Concat("G", filaCausas)).Value = det.cCausaDesc;
                            ws.Cell(string.Concat("G", filaCausas)).Style.Alignment.WrapText = true;
                            ws.Cell(string.Concat("G", filaCausas)).Style.Font.FontName = "Segoe UI";
                            ws.Cell(string.Concat("G", filaCausas)).Style.Font.FontSize = 9;
                            ws.Cell(string.Concat("G", filaCausas)).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            ws.Cell(string.Concat("G", filaCausas)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                            ws.Cell(string.Concat("G", filaCausas)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            filaCausas++;
                        }
                        matBucles[0] = filaCausas - 1;
                    }

                    ws.Range(String.Concat("A", fila, ":", "A", matBucles.Max())).Merge();
                    ws.Range(String.Concat("B", fila, ":", "B", matBucles.Max())).Merge();
                    ws.Range(String.Concat("C", fila, ":", "C", matBucles.Max())).Merge();
                    ws.Range(String.Concat("D", fila, ":", "D", matBucles.Max())).Merge();
                    ws.Range(String.Concat("E", fila, ":", "E", matBucles.Max())).Merge();
                    ws.Range(String.Concat("F", fila, ":", "F", matBucles.Max())).Merge();
                    ws.Range(String.Concat("G", matBucles[0], ":", "G", matBucles.Max())).Merge();


                    fila = matBucles.Max();
                    fila++;
                }

                ws.Range(String.Format("A4:{0}", ws.LastCellUsed().Address.ToString())).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Range(String.Format("A4:{0}", ws.LastCellUsed().Address.ToString())).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);

                //}

                return new ExcelResult(wb, "Notificar Autoevaluación [" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        #endregion

        #region Monitorear Evaluciones
        
        [HttpGet]
        [RequiresAuthenticationAttribute]
        public ActionResult Evaluaciones() {
            try
            {
                var error = Utils.Error.GetError.GetErrorModel("¡Aviso!", (HttpStatusCode)404, "Esta sección esta siendo desarrollado");
                return View("Error", error);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        [HttpGet]
        [RequiresAuthenticationAttribute]
        public ActionResult Monitorear()
        {
            try
            {
                var model = new GestionRiesgoModel()
                {
                    oLstTipoEvaluacion = constante.ObtenerConstantes(1501),
                    oLstAreas = areas.ObtenerAreas(),
                    oLstCausas = causas.ObtenerCausasRiesgo()
                };

                return View(model);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }


        [RequiresAuthenticationAttribute]
        public JsonResult MostrarEvaluacionesVerificacion(string psCodTaller)
        {

            try
            {
                var model = new GestionRiesgoModel()
                {
                    oLstDatosEvaluaciones = ievaluacion.MostrarEvaluacionVerificacionTaller(psCodTaller),
                    oTaller = taller.ObtenerDatosTaller(psCodTaller)
                };

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }

        [RequiresAuthenticationAttribute]
        public JsonResult FinalizarEvaluacion(string psCodTaller)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                var oRiesgosTaller = taller.ObtenerRiesgosTaller(psCodTaller);

                var configMail = new MailModel()
                {
                    Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor,
                    Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor,
                    Pruebas = Convert.ToBoolean(Convert.ToInt32(constSistema.ObtenerConstanteSistema(200).cConsSisValor)),
                    CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor
                };

                //var oDatosTaller = 
                string lsTblCodRiego = "<table style=" + "'background-color: whitesmoke; font-family: " + "'Segoe UI'" + ", Tahoma, Geneva, Verdana, sans-serif; font-size: 12px; border-radius: 5px; border-color: whitesmoke;" + ">" +
                    "<thead>" +
                    "<tr>" +
                    "<td><strong>Código Evaluación</strong></td>" +
                    "<td><strong>Fecha Creación</strong></td>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody style=" + "'background - color: white;'" + ">";
                for (int i = 0; i < oRiesgosTaller.Count; i++)
                {
                    lsTblCodRiego += "<tr>" +
                        "<td>" + oRiesgosTaller[i].oAutoevaluacion.cCodRiesgo + "</td>" +
                        "<td>" + oRiesgosTaller[i].oAutoevaluacion.dFechaRegistro + "</td>" +
                        "</tr>";
                }
                lsTblCodRiego += "</tbody>" +
                    "</table>";
                //var lsDestino = SendMail.ObtenerMailUsuario(TallerLN.ObtenerDatosTaller(psCodTaller).oDatosRiesgo.oUsuarios.cUser);
                //var lsCC = SendMail.ObtenerMailUsuario(usuario.cUser);
                List<string> mailDestino = new List<string>() { SendMail.Get.ObtenerMailUsuario(taller.ObtenerDatosTaller(psCodTaller).cUltimaActualizacion) };
                List<string> mailCC = new List<string>() { SendMail.Get.ObtenerMailUsuario(usuario.cUser) };
                var lsTitulo = "Taller " + psCodTaller;
                var lsAsunto = "Autoevaluación del taller " + psCodTaller + " exitosa";
                var lsContenido = "Se le informa que se evaluó el taller " + "<strong>" + psCodTaller + "</strong>" + ", según se detalla" +
                                  "<p><strong>Autoevaluaciones asignados al taller:</strong></p><br/>" + lsTblCodRiego + "<br/>" +
                                  "El resultado de la autoevaluación fue exitosa";
                var lsCorreo = ConstGeneral.Get.Correo(lsTitulo, lsContenido);

                //var CorreoEnviado = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC);
                _ = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC, String.Empty, configMail);

                return Json(null);
                //return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }

        #endregion

        [RequiresAuthenticationAttribute]
        public JsonResult DevolverVistaParcial(string VistaParcial, object Modelo = null)
        {
            var objVista = new
            {
                view = RenderHelper.PartialView(this, VistaParcial, Modelo)
            };
            return Json(objVista);
            //return Json(JsonConvert.SerializeObject(objVista));
        }

    }
}