using ClosedXML.Excel;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using CMACMaynas.Web.SIRO.Seguridad.Auth.Filters;
using Newtonsoft.Json;
using SIRO.Models;
using SIRO.Utils.Constantes;
using SIRO.Utils.Helpers;
using SIRO.Utils.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SIRO.Controllers
{
    public class RiesgoOperacionalController : Controller
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
        private readonly ISubLineaNegocioApp subLineaNegocio;
        private readonly ISubProcesoApp subProceso;
        private readonly IUsuarioApp iusuario;
        private readonly IGestionIncentivoApp gestionIncentivo;
        private readonly ILineaNegocioApp lineaNegocio;
        private readonly ICriterioEvaluacionApp criterioEvaluacion;
        private readonly IProductoApp producto;
        private readonly ISubProductoApp subProducto;
        private readonly IMontoPerdidaApp montoPerdida;
        private readonly IAutoevaluacionApp evaluacion;
        private readonly IRiesgoOperacionalApp riesgo;
        private readonly IEventoPerdidaApp evento;

        GeneralModel model;

        public RiesgoOperacionalController(IMaestroApp maestro, IConstantesApp constante, IConstSistemaApp constSistema, IAgenciasApp agencias, IAreasApp areas, ICausaRiesgoApp causas,
                                            IPlanAccionApp planAccion, IRiesgoResidualApp riesgoResidual, IRiesgoInherenteApp riesgoInherente, IProcesoAreaApp procesoArea,
                                            IControlProcesoApp controlProceso, ISubLineaNegocioApp subLineaNegocio, ISubProcesoApp subProceso, IUsuarioApp iusuario,
                                            IGestionIncentivoApp gestionIncentivo, ILineaNegocioApp lineaNegocio, ICriterioEvaluacionApp criterioEvaluacion, IProductoApp producto,
                                            ISubProductoApp subProducto, IMontoPerdidaApp montoPerdida, IAutoevaluacionApp evaluacion, IRiesgoOperacionalApp riesgo, IEventoPerdidaApp evento)
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
            this.subLineaNegocio = subLineaNegocio;
            this.subProceso = subProceso;
            this.iusuario = iusuario;
            this.gestionIncentivo = gestionIncentivo;
            this.lineaNegocio = lineaNegocio;
            this.criterioEvaluacion = criterioEvaluacion;
            this.producto = producto;
            this.subProducto = subProducto;
            this.montoPerdida = montoPerdida;
            this.evaluacion = evaluacion;
            this.riesgo = riesgo;
            this.evento = evento;
        }
        #endregion

        #region Registro de Riesgos Operacinales
        //[BreadCrumb(Clear = true, Label = "Registro Riesgo Operacional")]
        [RequiresAuthenticationAttribute]
        public ActionResult Registrar()
        {
            try
            {
                var model = new GestionRiesgoModel()
                {
                    oRiesgoOperacional = null,
                    oLstAgencia = agencias.ObtenerAgencias(),
                    oLstAreas = areas.ObtenerAreas(),
                    oLstCausas = causas.ObtenerCausasRiesgo(),
                };

                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error($"Error producido: {nameof(RiesgoOperacionalController)}=>({nameof(Registrar)}): " + ex.Message);
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }


        [HttpPost]
        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarRiesgoOperacional(string __psRiesgoIdent, string psAgeCod, string psFechaDetec, string psAreaCod, string psCausasRiesgo, string psProceso,
                                                  int pnSubProceso, string psControlProceso, int pbEfectividadCtrl, string __psObservacion = "", string psControles = "")
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);


                List<dynamic> ldControles = null;
                if (!String.IsNullOrEmpty(psControles))
                {
                    dynamic dControles = JsonConvert.DeserializeObject(psControles);
                    ldControles = new List<dynamic>(dControles);
                }
                
                var lsCodRiesgo = riesgo.Registrar(new RiesgoOperacional()
                {
                    cNroRiesgo = lsNroRiesgo,
                    cRiesgoIdentiticado = __psRiesgoIdent.Trim(),
                    oAgencia = new Agencias()
                    {
                        cAgeCod = psAgeCod,
                        oArea = new Areas()
                        {
                            cAreaCod = psAreaCod
                        }
                    },
                    oCausas = new CausaRiesgo()
                    {
                        cCodCausa = psCausasRiesgo
                    },
                    oProceso = new ProcesoArea()
                    {
                        cCodProceso = psProceso,
                        oSubProceso = new SubProcesos()
                        {
                            nCodSubProceso = pnSubProceso
                        }
                    },
                    lstControles = ldControles,
                    bControlEfectivo = Convert.ToBoolean(pbEfectividadCtrl),
                    cEfectosRiesgo = __psObservacion.Trim(),
                    dFechaDeteccion = Convert.ToDateTime(psFechaDetec)
                });



                return Json(JsonConvert.SerializeObject(new ResponseModel()
                {
                    obj = lsCodRiesgo,
                    valor = (int)Respuesta.exito,
                    mensaje = "Registro de riesgo operacional exitoso.",
                }));
            }
            catch (Exception ex)
            {
                Log.Error($"Error producido: {nameof(RiesgoOperacionalController)}=>({nameof(RegistrarRiesgoOperacional)}): " + ex.Message);
                return Json(JsonConvert.SerializeObject(new ResponseModel()
                {
                    valor = (int)Respuesta.error,
                    mensaje = "No se pudo realizar el registro del riesgo operacional."
                }));
            }
        }

        //[BreadCrumb(Label = "Modificación Riesgo Operacional")]
        [RequiresAuthenticationAttribute]
        public ActionResult Modificar(string riesgo)
        {
            try
            {
                //var model = new GestionRiesgoModel()
                //{
                //    oRiesgoOperacional = this.riesgo.ObtenerInfoGeneralRiesgoOperacional((long)Convert.ToInt32(nNroRiesgo)),
                //    lstObservacionesGestion = this.riesgo.ObtenerObservacionesGestion((long)Convert.ToInt32(nNroRiesgo), (int)ProcedenciaObservaciones.SolicitudModificacionRiesgo),
                //    oLstAgencia = agencias.ObtenerAgencias(),
                //    oLstAreas = areas.ObtenerAreas(),
                //    oLstProcesoAreas = procesoArea.ObtenerProcesoAreas(.oRiesgoOperacional.oAreas.cAreaCod),
                //    oLstSubProcesoAreas = subProceso.ObtenerSubProcesosAreas(model.oRiesgoOperacional.oProceso.cCodProceso),
                //    oLstControlProcesos = controlProceso.ObtenerControlesProceso(model.oRiesgoOperacional.oProceso.cCodProceso),
                //    oLstCausas = causas.ObtenerCausasRiesgo()
                //};

                var nNroRiesgo = Encripta.base64Decode(Convert.ToString(riesgo));

                GestionRiesgoModel model = new GestionRiesgoModel();
                model.oRiesgoOperacional = this.riesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo() { nNroRiesgo = (long)Convert.ToInt32(nNroRiesgo) });
                model.lstObservacionesGestion = this.riesgo.ObtenerObservacionesGestion((long)Convert.ToInt32(nNroRiesgo), (int)ProcedenciaObservaciones.SolicitudModificacionRiesgo);
                model.oLstAgencia = agencias.ObtenerAgencias();
                //model.oLstAreas = areas.ObtenerAreasAgencia(model.oRiesgoOperacional.oAreas.cAreaCod); //Comennted by TORE: las agencias no dependeran de las areas.
                model.oLstAreas = areas.ObtenerAreas();
                model.oLstProcesoAreas = procesoArea.ObtenerProcesoAreas(model.oRiesgoOperacional.oAgencia.oArea.cAreaCod);
                model.oLstSubProcesoAreas = subProceso.ObtenerSubProcesosAreas(model.oRiesgoOperacional.oProceso.cCodProceso);
                model.oLstControlProcesos = controlProceso.ObtenerControlesProceso(model.oRiesgoOperacional.oProceso.cCodProceso);
                model.oLstCausas = causas.ObtenerCausasRiesgo();
                model.oLstRiesgoResidual = riesgoResidual.ObtenerControlRiesgoResidual(new DetalleRiesgo() { nNroRiesgo = Convert.ToInt64(nNroRiesgo) });

                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error($"Error producido: {nameof(RiesgoOperacionalController)}=>({nameof(Modificar)}): " + ex.Message);
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        [HttpGet]
        [RequiresAuthenticationAttribute]
        public JsonResult ActualizarRiesgoOperacional(long pnNroRiesgo, string __psRiesgoIdent, string psAgeCod, string psFechaDetec, string psAreaCod, string psCausasRiesgo, string psProceso,
                                                int pnSubProceso, string psControlProceso, int pbEfectividadCtrl, string __psObservacion = "",
                                                string psControles = "")
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                ///*Validaciones para la actualizacion del riesgo*/
                //var controles = riesgoResidual.ObtenerControlRiesgoResidual(pnNroRiesgo);
                //if (controles.Where(x => x.nResponsableDef > 0 && x.nPeriodoEjecucion > 0 && x.nEvidenciaControl > 0).ToList().Count == 0) //identificar que no existe aun control que no hay sido gestionado
                //{
                //    return Json(JsonConvert.SerializeObject(new ResponseModel()
                //    {
                //        ValorNotif = (int)TiposNotificacion.informacion,
                //        TipoNotif = TiposNotificacion.informacion.ToString(),
                //        MensajeNotif = "No es posible realizar la acción debi"
                //    }));
                //}

                //ResponseModel respuesta = new ResponseModel();
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);
                List<dynamic> ldControles = null;
                if (!String.IsNullOrEmpty(psControles))
                {
                    dynamic dControles = JsonConvert.DeserializeObject(psControles);
                    ldControles = new List<dynamic>(dControles);
                }

                //var nExito = riesgo.Actualizar(pnNroRiesgo, __psRiesgoIdent, psAgeCod, psFechaDetec, psAreaCod, psCausasRiesgo, psProceso,
                //                                  pnSubProceso, psControlProceso, pbEfectividadCtrl, lsNroRiesgo, __psObservacion,
                //                                  ldControles);

                var nExito = riesgo.Actualizar(new RiesgoOperacional()
                {
                    nNroRiesgo = pnNroRiesgo,
                    cNroRiesgo = lsNroRiesgo,
                    cRiesgoIdentiticado = __psRiesgoIdent.Trim(),
                    oAgencia = new Agencias()
                    {
                        cAgeCod = psAgeCod,
                        oArea = new Areas()
                        {
                            cAreaCod = psAgeCod
                        }
                    },
                    oCausas = new CausaRiesgo()
                    {
                        cCodCausa = psCausasRiesgo
                    },
                    oProceso = new ProcesoArea()
                    {
                        cCodProceso = psProceso,
                        oSubProceso = new SubProcesos()
                        {
                            nCodSubProceso = pnSubProceso
                        }
                    },
                    lstControles = ldControles,
                    bControlEfectivo = Convert.ToBoolean(pbEfectividadCtrl),
                    cEfectosRiesgo = __psObservacion.Trim(),
                    dFechaDeteccion = Convert.ToDateTime(psFechaDetec)
                });



                if (nExito > 0)
                {
                    if (usuario.cRHCargoCod != ConstGeneral.Get.AnalistaRO)
                    {
                        var oAnaRiesgo = iusuario.ObtenerUsuariosCargos(ConstGeneral.Get.AnalistaRO);
                        var oDatosRiesgo = riesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo() { nNroRiesgo = pnNroRiesgo });

                        var configMail = new MailModel()
                        {
                            Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor,
                            Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor,
                            Pruebas = Convert.ToBoolean(Convert.ToInt32(constSistema.ObtenerConstanteSistema(200).cConsSisValor)),
                            CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor
                        };

                        //string lsDestinaratios = "", CorreosDestino = "";
                        List<string> mailDestino = new List<string>(), mailCC = new List<string>();
                        for (int i = 0; i < oAnaRiesgo.Count; i++)
                        {
                            //Se hace el recorrido por si existen varios analistas de riesgos
                            mailDestino.Add(SendMail.Get.ObtenerMailUsuario(oAnaRiesgo[i].cUser));
                        }
                        mailCC.Add(SendMail.Get.ObtenerMailUsuario(usuario.cUser));
                        var lsAsunto = "Confirmación de Modificación del Riesgo Operacional"; //+ lsCodTaller;
                        var lsTitulo = "Modificación Realizada";
                        var lsContenido = "Se informa que el usuario " + usuario.cUsuario + "(" + usuario.cUser + ")" + " culminó con la modificación solicitada sobre el riesgo operacional código " +
                            oDatosRiesgo.cCodRiesgo + " " +
                            "<p><strong>Riesgo Indentificado: </strong>" + oDatosRiesgo.cRiesgoIdentiticado + "</p>";
                        var lsCorreo = ConstGeneral.Get.Correo(lsTitulo, lsContenido);

                        _ = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC, String.Empty, configMail);
                    }
                }

                return Json(new { Exito = nExito });
                //return Json(JsonConvert.SerializeObject(respuesta));
            }
            catch (Exception ex)
            {
                Log.Error($"Error producido: {nameof(RiesgoOperacionalController)}=>({nameof(ActualizarRiesgoOperacional)}): " + ex.Message);
                throw;
            }
        }

        #endregion

        #region Acciones sobre el Riesgo Operacional
        [RequiresAuthenticationAttribute]
        public JsonResult EliminarRiesgo(long pnNroRiesgo)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                int lnExito = riesgo.EliminarRiesgo(pnNroRiesgo, lsNroRiesgo);

                //return Json(JsonConvert.SerializeObject(exito));
                return Json(new { Exito = lnExito });

            }
            catch (Exception ex)
            {
                Log.Error($"Error producido: {nameof(RiesgoOperacionalController)}=>({nameof(EliminarRiesgo)}): " + ex.Message);
                throw;
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult RechazarRiesgo(long pnNroRiesgo, string psMotivoRechazoRiesgo)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                int lnExito = riesgo.RechazarRiesgo(pnNroRiesgo, psMotivoRechazoRiesgo, lsNroRiesgo);

                if (lnExito > 0)
                {
                    var oDetRiesgo = riesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo() { nNroRiesgo = pnNroRiesgo });
                    var lsAsunto = "Riesgo rechazado";
                    var lsTitulo = "Rechazo del riesgo " + oDetRiesgo.cCodRiesgo;
                    var configMail = new MailModel()
                    {
                        Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor,
                        Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor,
                        Pruebas = Convert.ToBoolean(Convert.ToInt32(constSistema.ObtenerConstanteSistema(200).cConsSisValor)),
                        CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor
                    };
                    List<string> mailDestino = new List<string>() { SendMail.Get.ObtenerMailUsuario(oDetRiesgo.oUsuario.cUser) };
                    //var lsDestino = SendMail.ObtenerMailUsuario(oDetRiesgo.oUsuarios.cUser);
                    var lsContenido = "<p><strong>Riesgo identificado</strong><br/>" + oDetRiesgo.cRiesgoIdentiticado + "</p>" +
                                      "<p><strong>Razón del rechazo</strong><br/>" + psMotivoRechazoRiesgo + "</p>" +
                                      "<p><strong>Fecha registro</strong><br/>" + DateTime.Now.ToString("dd/MM/yyyy") + "</p>";

                    var lsCorreo = ConstGeneral.Get.Correo(lsTitulo, lsContenido);

                    _ = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, null, String.Empty, configMail);


                }
                return Json(new { Exito = lnExito });
            }
            catch (Exception ex)
            {
                Log.Error($"Error producido: {nameof(RiesgoOperacionalController)}=>({nameof(RechazarRiesgo)}): " + ex.Message);
                throw;
            }
        }
        #endregion

        #region  Lista Riesgo Operacional
        //[BreadCrumb(Clear = true, Label = "Lista Riesgo Operacional")]
        [RequiresAuthenticationAttribute]
        public ActionResult ListaGestion()
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
        public JsonResult MostrarRiesgosOperacionales()
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                //var oLstRiesgosOperacionales = riesgo.ObtenerDatosRiesgoOperacional((usuario.cRHCargoCod == ConstGeneral.Get.AnalistaRO ? String.Empty : usuario.cUser));
                var oLstRiesgosOperacionales = riesgo.ObtenerDatosRiesgoOperacional(new DetalleRiesgo()
                {
                    oUsuario = new Usuario()
                    {
                        cUser = usuario.cRHCargoCod == ConstGeneral.Get.AnalistaRO ? String.Empty : usuario.cUser
                    }
                });
                if (oLstRiesgosOperacionales != null)
                {
                    model.oLstRiesgoOperacional = oLstRiesgosOperacionales;
                }
                else { model.oLstRiesgoOperacional = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public ActionResult GenerarExcelRiesgoOperacional()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                var data = riesgo.ObtenerDatosRiesgoOperacional(new DetalleRiesgo()
                {
                    oUsuario = new Usuario()
                    {
                        cUser = usuario.cRHCargoCod == ConstGeneral.Get.AnalistaRO ? String.Empty : usuario.cUser
                    }
                });

                return new ExcelResult(ExcelRiesgosOperacionales(data), "Riesgo Operacionales en Gestión " + "[" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }

        }

        #region Armando Formato Excel
        //private XLWorkbook ExcelRiesgosOperacionales(List<DatosRiesgosEN> datos)
        //{
        //    XLWorkbook wb = new XLWorkbook();
        //    var ws = wb.Worksheets.Add("RiesgosOperacionales");


        //    /****************************************** Inicio Cabecera ******************************************/

        //    ws.Range("A1:H1").Merge();
        //    ws.Cell("A1").Value = "Riesgos Operacionales";

        //    //ws.Columns(1, 3).AdjustToContents();

        //    var rangoCab = ws.Range("A1:H2");
        //    rangoCab.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
        //    rangoCab.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
        //    rangoCab.Style.Border.SetOutsideBorderColor(XLColor.White);
        //    rangoCab.Style.Border.SetInsideBorderColor(XLColor.White);

        //    rangoCab.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //    rangoCab.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        //    rangoCab.Style.Font.FontName = "Segoe UI";
        //    rangoCab.Style.Font.FontSize = 10;
        //    rangoCab.Style.Alignment.WrapText = true;
        //    rangoCab.Style.Fill.BackgroundColor = XLColor.FromArgb(192, 0, 0);
        //    rangoCab.Style.Font.FontColor = XLColor.WhiteSmoke;

        //    ws.Column("A").Width = 15;
        //    ws.Cell("A2").Value = "Código Riesgo";

        //    ws.Column("B").Width = 50;
        //    ws.Cell("B2").Value = "Riesgo Identificado";

        //    ws.Column("C").Width = 12;
        //    ws.Cell("C2").Value = "Usuario";
        //    //ws.Cell("C2").Style.Font.Bold = true;

        //    ws.Column("D").Width = 18;
        //    ws.Cell("D2").Value = "Agencia Afectada";

        //    ws.Column("E").Width = 18;
        //    ws.Cell("E2").Value = "Área Afectada";

        //    ws.Column("F").Width = 18;
        //    ws.Cell("F2").Value = "Proceso Actual";

        //    ws.Column("G").Width = 18;
        //    ws.Cell("G2").Value = "Estado Actual";

        //    ws.Column("H").Width = 20;
        //    ws.Cell("H2").Value = "Fecha Detección";
        //    /******************************************** Fin Cabecera *******************************************/

        //    /******************************************** Cuerpo *******************************************/
        //    int j = 3;
        //    for (int i = 0; i < datos.Count; i++)
        //    {
        //        ws.Cell("A" + j).Value = datos[i].cCodRiesgo;
        //        ws.Cell("A" + j).Style.Alignment.WrapText = true;
        //        ws.Cell("A" + j).Style.Font.FontName = "Segoe UI";
        //        ws.Cell("A" + j).Style.Font.FontSize = 9;
        //        ws.Cell("A" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
        //        ws.Cell("A" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        ws.Cell("A" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        //        ws.Cell("A" + j).Style.NumberFormat.Format = "00000";


        //        ws.Cell("B" + j).Value = datos[i].cRiesgoIdentiticado;
        //        ws.Cell("B" + j).Style.Alignment.WrapText = true;
        //        ws.Cell("B" + j).Style.Font.FontName = "Segoe UI";
        //        ws.Cell("B" + j).Style.Font.FontSize = 9;
        //        ws.Cell("B" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
        //        ws.Cell("B" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
        //        ws.Cell("B" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        //        ws.Cell("C" + j).Value = datos[i].oUsuarios.cUser;
        //        ws.Cell("C" + j).Style.Alignment.WrapText = true;
        //        ws.Cell("C" + j).Style.Font.FontName = "Segoe UI";
        //        ws.Cell("C" + j).Style.Font.FontSize = 9;
        //        ws.Cell("C" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
        //        ws.Cell("C" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        ws.Cell("C" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        //        ws.Cell("D" + j).Value = datos[i].oAgencias.cAgeDescripcion;
        //        ws.Cell("D" + j).Style.Alignment.WrapText = true;
        //        ws.Cell("D" + j).Style.Font.FontName = "Segoe UI";
        //        ws.Cell("D" + j).Style.Font.FontSize = 9;
        //        ws.Cell("D" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
        //        ws.Cell("D" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        ws.Cell("D" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        //        ws.Cell("E" + j).Value = datos[i].oAreas.cAreaDescripcion;
        //        ws.Cell("E" + j).Style.Alignment.WrapText = true;
        //        ws.Cell("E" + j).Style.Font.FontName = "Segoe UI";
        //        ws.Cell("E" + j).Style.Font.FontSize = 9;
        //        ws.Cell("E" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
        //        ws.Cell("E" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        ws.Cell("E" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        //        ws.Cell("F" + j).Value = datos[i].cProcRiesgo;
        //        ws.Cell("F" + j).Style.Alignment.WrapText = true;
        //        ws.Cell("F" + j).Style.Font.FontName = "Segoe UI";
        //        ws.Cell("F" + j).Style.Font.FontSize = 9;
        //        ws.Cell("F" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
        //        ws.Cell("F" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        ws.Cell("F" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        //        ws.Cell("G" + j).Value = datos[i].cEstadoRiesgo;
        //        ws.Cell("G" + j).Style.Alignment.WrapText = true;
        //        ws.Cell("G" + j).Style.Font.FontName = "Segoe UI";
        //        ws.Cell("G" + j).Style.Font.FontSize = 9;
        //        ws.Cell("G" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
        //        ws.Cell("G" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        ws.Cell("G" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        //        ws.Cell("H" + j).Value = datos[i].dFechaDeteccion;
        //        ws.Cell("H" + j).Style.Alignment.WrapText = true;
        //        ws.Cell("H" + j).Style.Font.FontName = "Segoe UI";
        //        ws.Cell("H" + j).Style.Font.FontSize = 9;
        //        ws.Cell("H" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
        //        ws.Cell("H" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        ws.Cell("H" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        //        j++;
        //    }
        //    /***************************************** Fin Cuerpo ******************************************/

        //    return wb;

        //}
        #endregion

        //private XLWorkbook ExcelRiesgosOperacionales(List<DatosRiesgos> datos) 
        private XLWorkbook ExcelRiesgosOperacionales(List<RiesgoOperacional> datos)
        {
            XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoListaGestionRiesgo.xlsx"), XLEventTracking.Disabled);
            IXLWorksheet ws = wb.Worksheet(1); //Hoja 1

            int j = 3;
            for (int i = 0; i < datos.Count; i++)
            {
                ws.Cell("A" + j).Value = datos[i].cCodRiesgo;
                ws.Cell("A" + j).Style.Alignment.WrapText = true;
                ws.Cell("A" + j).Style.Font.FontName = "Segoe UI";
                ws.Cell("A" + j).Style.Font.FontSize = 9;
                ws.Cell("A" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Cell("A" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("A" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Cell("A" + j).Style.NumberFormat.Format = "00000";


                ws.Cell("B" + j).Value = datos[i].cRiesgoIdentiticado;
                ws.Cell("B" + j).Style.Alignment.WrapText = true;
                ws.Cell("B" + j).Style.Font.FontName = "Segoe UI";
                ws.Cell("B" + j).Style.Font.FontSize = 9;
                ws.Cell("B" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Cell("B" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                ws.Cell("B" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                ws.Cell("C" + j).Value = datos[i].oUsuario.cUser;
                ws.Cell("C" + j).Style.Alignment.WrapText = true;
                ws.Cell("C" + j).Style.Font.FontName = "Segoe UI";
                ws.Cell("C" + j).Style.Font.FontSize = 9;
                ws.Cell("C" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Cell("C" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("C" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                ws.Cell("D" + j).Value = datos[i].oAgencia.cAgeDescripcion;
                ws.Cell("D" + j).Style.Alignment.WrapText = true;
                ws.Cell("D" + j).Style.Font.FontName = "Segoe UI";
                ws.Cell("D" + j).Style.Font.FontSize = 9;
                ws.Cell("D" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Cell("D" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("D" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                ws.Cell("E" + j).Value = datos[i].oAgencia.oArea.cAreaDescripcion;
                ws.Cell("E" + j).Style.Alignment.WrapText = true;
                ws.Cell("E" + j).Style.Font.FontName = "Segoe UI";
                ws.Cell("E" + j).Style.Font.FontSize = 9;
                ws.Cell("E" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Cell("E" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("E" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                //ws.Cell("F" + j).Value = datos[i].nProcRiesgo == 0 ? "Registrado" : datos[i].cProcRiesgo;
                ws.Cell("F" + j).Value = datos[i].oProcRiesgo.nConsValor == 0 ? "Registrado" : datos[i].oProcRiesgo.cConsDescripcion;
                ws.Cell("F" + j).Style.Alignment.WrapText = true;
                ws.Cell("F" + j).Style.Font.FontName = "Segoe UI";
                ws.Cell("F" + j).Style.Font.FontSize = 9;
                ws.Cell("F" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Cell("F" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("F" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                ws.Cell("G" + j).Value = datos[i].oEstado.cConsDescripcion;
                ws.Cell("G" + j).Style.Alignment.WrapText = true;
                ws.Cell("G" + j).Style.Font.FontName = "Segoe UI";
                ws.Cell("G" + j).Style.Font.FontSize = 9;
                ws.Cell("G" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Cell("G" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("G" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                ws.Cell("H" + j).Value = datos[i].dFechaDeteccion;
                ws.Cell("H" + j).Style.Alignment.WrapText = true;
                ws.Cell("H" + j).Style.Font.FontName = "Segoe UI";
                ws.Cell("H" + j).Style.Font.FontSize = 9;
                ws.Cell("H" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Cell("H" + j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("H" + j).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                j++;
            }

            return wb;
        }
        #endregion

        #region Detalle de Riesgo
        //[BreadCrumb(Label = "Detalle Riesgo Operacional")]
        [RequiresAuthenticationAttribute]
        public ActionResult DetalleRiesgo(string riesgo)
        {
            try
            {
                var nNroRiesgo = Encripta.base64Decode(riesgo);
                var detalleRiesgoOperacional = this.riesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo() { nNroRiesgo = (long)Convert.ToInt32(nNroRiesgo) });

                return View(detalleRiesgoOperacional);
            }
            catch(Exception ex)
            {
                Log.Error($"Error producido: {nameof(RiesgoOperacionalController)}=>({nameof(DetalleRiesgo)}): " + ex.Message);
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ReportePlanesAsignadosRiesgo(string riesgo)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                var Reporte = GenerarReportePlanesAccionAsignadosRiesgos(riesgo);
                return new ExcelResult(Reporte, "Planes Acción [" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer nos encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        private XLWorkbook GenerarReportePlanesAccionAsignadosRiesgos(string psRiesgo)
        {
            try
            {
                var Plantilla = Server.MapPath("~/Plantillas/FormatoPlanesAccion.xlsx");
                XLWorkbook wb = new XLWorkbook(Plantilla);

                IXLWorksheet ws = wb.Worksheet(1);

                var nNroRiesgo = Encripta.base64Decode(psRiesgo);
                var Riesgos = riesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo() { nNroRiesgo = (long)Convert.ToInt32(nNroRiesgo) });

                ws.Cell("A5").Value = Riesgos.cCodRiesgo;
                ws.Cell("A5").Style.Alignment.WrapText = true;
                ws.Cell("A5").Style.Font.FontName = "Segoe UI";
                ws.Cell("A5").Style.Font.FontSize = 9;
                ws.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("A5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                ws.Cell("B5").Value = Riesgos.cRiesgoIdentiticado;
                ws.Cell("B5").Style.Alignment.WrapText = true;
                ws.Cell("B5").Style.Font.FontName = "Segoe UI";
                ws.Cell("B5").Style.Font.FontSize = 9;
                ws.Cell("B5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                ws.Cell("B5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                var ListaPlanes = planAccion.MostrarPlanesAccionRiesgo((long)Convert.ToInt32(nNroRiesgo));
                int filaPlan = 5; //Fila de inicio
                int[] matBucles = { 0, 0 }; /*(0) - Plan Accion, (1) - Responsable Plan Acicion*/

                if (ListaPlanes.Count > 0)
                {
                    matBucles[0] = filaPlan;
                    foreach (var planes in ListaPlanes)
                    {
                        ws.Cell("C" + filaPlan).Value = Convert.ToString(planes.nPlanCod);
                        ws.Cell("C" + filaPlan).Style.Alignment.WrapText = true;
                        ws.Cell("C" + filaPlan).Style.Font.FontName = "Segoe UI";
                        ws.Cell("C" + filaPlan).Style.Font.FontSize = 9;
                        ws.Cell("C" + filaPlan).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("C" + filaPlan).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("D" + filaPlan).Value = planes.cPlanDescripcion;
                        ws.Cell("D" + filaPlan).Style.Alignment.WrapText = true;
                        ws.Cell("D" + filaPlan).Style.Font.FontName = "Segoe UI";
                        ws.Cell("D" + filaPlan).Style.Font.FontSize = 9;
                        ws.Cell("D" + filaPlan).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("D" + filaPlan).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("G" + filaPlan).Value = planes.dFechaImplement;
                        ws.Cell("G" + filaPlan).Style.Alignment.WrapText = true;
                        ws.Cell("G" + filaPlan).Style.Font.FontName = "Segoe UI";
                        ws.Cell("G" + filaPlan).Style.Font.FontSize = 9;
                        ws.Cell("G" + filaPlan).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("G" + filaPlan).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("H" + filaPlan).Value = planes.cComentario;
                        ws.Cell("H" + filaPlan).Style.Alignment.WrapText = true;
                        ws.Cell("H" + filaPlan).Style.Font.FontName = "Segoe UI";
                        ws.Cell("H" + filaPlan).Style.Font.FontSize = 9;
                        ws.Cell("H" + filaPlan).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("H" + filaPlan).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("I" + filaPlan).Value = planes.cEstado;
                        ws.Cell("I" + filaPlan).Style.Alignment.WrapText = true;
                        ws.Cell("I" + filaPlan).Style.Font.FontName = "Segoe UI";
                        ws.Cell("I" + filaPlan).Style.Font.FontSize = 9;
                        ws.Cell("I" + filaPlan).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("I" + filaPlan).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                        var Responsables = planAccion.MostrarDetalleResponsablesPlanesAccionRiesgo((long)Convert.ToInt32(nNroRiesgo), planes.nPlanCod);
                        if (Responsables.Count > 0)
                        {
                            int filaResponsable = filaPlan;
                            foreach (var responsable in Responsables)
                            {

                                ws.Cell("E" + filaResponsable).Value = responsable.oDatosRiesgo.oAgencias.cAgeDescripcion;
                                ws.Cell("E" + filaResponsable).Style.Alignment.WrapText = true;
                                ws.Cell("E" + filaResponsable).Style.Font.FontName = "Segoe UI";
                                ws.Cell("E" + filaResponsable).Style.Font.FontSize = 9;
                                ws.Cell("E" + filaResponsable).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("E" + filaResponsable).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("F" + filaResponsable).Value = "[" + responsable.cUserResponsable + "] - " + responsable.oPersona.cPersNombre.ToUpper();
                                ws.Cell("F" + filaResponsable).Style.Alignment.WrapText = true;
                                ws.Cell("F" + filaResponsable).Style.Font.FontName = "Segoe UI";
                                ws.Cell("F" + filaResponsable).Style.Font.FontSize = 9;
                                ws.Cell("F" + filaResponsable).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("F" + filaResponsable).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                filaResponsable++;
                            }
                            matBucles[1] = filaResponsable - 1;
                        }

                        ws.Range("A5" + ":" + "A" + matBucles.Max()).Merge();
                        ws.Range("B5" + ":" + "B" + matBucles.Max()).Merge();

                        ws.Range("E" + filaPlan + ":" + "E" + matBucles.Max()).Merge();
                        ws.Range("F" + filaPlan + ":" + "F" + matBucles.Max()).Merge();
                        ws.Range("G" + filaPlan + ":" + "G" + matBucles.Max()).Merge();
                        ws.Range("H" + filaPlan + ":" + "H" + matBucles.Max()).Merge();
                        ws.Range("I" + filaPlan + ":" + "I" + matBucles.Max()).Merge();

                        filaPlan = matBucles.Max();
                        filaPlan++;
                    }
                }
                /***************************************** Fin Cuerpo ******************************************/
                ws.Range("A5:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Range("A5:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);

                return wb;
            }
            catch { throw; }
        }


        #endregion

        #region Gestion de Riesgos Operacionales
        //[BreadCrumb(Label = "Gestión Riesgo Operacional")]
        [RequiresAuthenticationAttribute]
        public ActionResult GestionRiesgo(string psNroRiesgo)
        {
            try
            {
                var nNroRiesgo = Encripta.base64Decode(Convert.ToString(psNroRiesgo));
                var model = new GestionRiesgoModel()
                {
                    oDatosRiesgo = riesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo() { nNroRiesgo = (long)Convert.ToInt32(nNroRiesgo) })
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
        public JsonResult GrabarProcesoPaso1(long pnNroRiesgo, int pnFactorRiesgo, int pnEventoPerdida, int pnSubEventoPerdida, string psLineaNeg, string psProducto, string psSubProducto)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                string[] Mensaje = riesgo.GrabarProcesoPaso1(pnNroRiesgo, pnFactorRiesgo, pnEventoPerdida, pnSubEventoPerdida, psLineaNeg, psProducto, psSubProducto, lsNroRiesgo);
                string cTpoMensaje = Mensaje[(int)Utils.Constantes.Mensaje.TipoMensaje];
                string cMensaje = Mensaje[(int)Utils.Constantes.Mensaje.Mensaje];
                int nObservaciones = riesgo.ObtenerObservacionesGestion(pnNroRiesgo).Count;

                return Json(new { TpoMensaje = cTpoMensaje, MensajeSis = cMensaje, Observaciones = nObservaciones });
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult GrabarProcesoPaso2(long pnNroRiesgo, int pnProbabilidad, int pnImpacto)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                string[] Mensaje = riesgo.GrabarProcesoPaso2(pnNroRiesgo, pnProbabilidad, pnImpacto, lsNroRiesgo);
                string cTpoMensaje = Mensaje[(int)Utils.Constantes.Mensaje.TipoMensaje];
                string cMensaje = Mensaje[(int)Utils.Constantes.Mensaje.Mensaje];
                int nObservaciones = riesgo.ObtenerObservacionesGestion(pnNroRiesgo).Count;

                //var response = new ResponseModel(){
                //    ValResponse
                //};

                return Json(new { TpoMensaje = cTpoMensaje, MensajeSis = cMensaje, Observaciones = nObservaciones });
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult GrabarProcesoPaso3(long pnNroRiesgo, string __psComentarios)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                string[] Mensaje = riesgo.GrabarProcesoPaso3(pnNroRiesgo, __psComentarios, lsNroRiesgo);
                string cTpoMensaje = Mensaje[(int)Utils.Constantes.Mensaje.TipoMensaje];
                string cMensaje = Mensaje[(int)Utils.Constantes.Mensaje.Mensaje];

                return Json(new { TpoMensaje = cTpoMensaje, MensajeSis = cMensaje });
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult GrabarProcesoPaso4(long pnNroRiesgo)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                string[] Mensaje = riesgo.GrabarProcesoPaso4(pnNroRiesgo, lsNroRiesgo);
                string cTpoMensaje = Mensaje[(int)Utils.Constantes.Mensaje.TipoMensaje];
                string cMensaje = Mensaje[(int)Utils.Constantes.Mensaje.Mensaje];

                return Json(new { TpoMensaje = cTpoMensaje, MensajeSis = cMensaje });
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public async Task<JsonResult> GrabarProcesoPaso5(long pnNroRiesgo)
        {
            bool asignacionGuardada = false;
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                /*Aplicamos las validaciones para el proceso de asignacion de planes de accion*/
                string validacion = planAccion.ValidarResponsablesPlanAccion(pnNroRiesgo);
                int lnExitoGestion = 0; string lsMensaje = "";
                if (String.IsNullOrEmpty(validacion))
                {
                    lnExitoGestion = riesgo.GrabarProcesoPaso5(pnNroRiesgo, lsNroRiesgo);
                    if (lnExitoGestion <= 0)
                    {
                        return Json(new { ExitoGestion = 0, Mensaje = "No se pudo guardar la asignación de los responsables." });
                    }
                    asignacionGuardada = true;

                    int lnTpoRiesgo = riesgo.ObtenerTpoRiesgo(pnNroRiesgo);

                    //var oDetRiesgo = lnTpoRiesgo == (int)Riesgos.RiesgoOperacional ? riesgo.ObtenerInfoGeneralRiesgoOperacional(new Riesgo() { nNroRiesgo = pnNroRiesgo }) : evaluacion.MostrarDetalleEvaluacion(pnNroRiesgo);
                    var oDetRiesgo = riesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo() { nNroRiesgo = pnNroRiesgo });
                    var LstPlanesAccion = planAccion.MostrarPlanesAccionRiesgo(pnNroRiesgo);

                    var configMail = new MailModel()
                    {
                        Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor,
                        Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor,
                        Pruebas = Convert.ToBoolean(Convert.ToInt32(constSistema.ObtenerConstanteSistema(200).cConsSisValor)),
                        CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor
                    };

                    foreach (var oPlanAccion in LstPlanesAccion)
                    {
                        var LstRespondables = planAccion.ObtenerResponsablePlanAccion(oPlanAccion.nPlanCod);
                        string lsResponsables = "";
                        foreach (var responsables in LstRespondables)
                        {
                            lsResponsables += Convert.ToString(responsables.nItemResponPlan) + ". " + new string(' ', 2) + iusuario.ObtenerDatosUsuario(responsables.cUserResponsable).oPersona.cPersNombre + "<br/>";
                        }
                        foreach (var oResponsable in LstRespondables)
                        {
                            List<string> maildestino = new List<string> { configMail.Pruebas ? configMail.CorreoTest : SendMail.Get.ObtenerMailUsuario(oResponsable.cUserResponsable) };
                            List<string> mailcc = new List<string> { configMail.Pruebas ? configMail.CorreoTest : SendMail.Get.ObtenerMailUsuario(usuario.cUser) };

                            var odetplanaccion = planAccion.ObtenerPlanAccionRiesgoPlan(pnNroRiesgo, oPlanAccion.nPlanCod);
                            var lstitulo = "Asignación de plan de acción";
                            var lsasunto = "Notificación de asignación de plan de acción n°" + Convert.ToString(oPlanAccion.nPlanCod);
                            var lscontenido = "<p>Ud. fue asignado como responsable del plan de acción con código <strong>" + Convert.ToString(oPlanAccion.nPlanCod) + "</strong>" +
                                              " del riesgo con código <strong>" + oDetRiesgo.cCodRiesgo + "</strong>, el cual se detalla a continuación." + "</p>" +
                                              "<p><strong>Riesgo identificado</strong><br/>" + oDetRiesgo.cRiesgoIdentiticado + "</p>" +
                                              "<p><strong>Plan de acción</strong><br/>" + odetplanaccion.cPlanDescripcion + "</p>" +
                                              "<p><strong>Comentarios</strong><br/>" + odetplanaccion.cComentario + "</p>" +
                                              "<p><strong>Responsable(s)</strong><br/>" + lsResponsables + "</p>";
                            var lscorreo = ConstGeneral.Get.Correo(lstitulo, lscontenido);

                            await SendMail.Get.EnvioMail(maildestino, lsasunto, lscorreo, mailcc, string.Empty, configMail);
                        }
                    }
                    lsMensaje = "Se asignó correctamente a los responsables de los planes de acción y se enviaron las notificaciones";

                }
                else
                {
                    lnExitoGestion = 0;
                    lsMensaje = validacion;
                }
                return Json(new { ExitoGestion = lnExitoGestion, Mensaje = lsMensaje });
            }
            catch (Exception ex)
            {
                if (!asignacionGuardada) { throw; }
                Log.Error("No se completaron las notificaciones del paso 5 del riesgo " + pnNroRiesgo + ". Tipo de error: " + ex.GetType().Name);
                return Json(new { ExitoGestion = 0, Mensaje = "La asignación se guardó, pero no se completó el envío de las notificaciones. Revise la configuración de correo y las direcciones de los responsables antes de reintentar; algunos correos podrían haberse enviado." });
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerInfoGestionRiesgo(long pnNroRiesgo, int pnNroProceso)
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {
                int nTpoRiesgo = riesgo.ObtenerTpoRiesgo(pnNroRiesgo);
                if (nTpoRiesgo == (int)Riesgos.RiesgoOperacional)
                {
                    model.oRiesgoOperacional = riesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo() { nNroRiesgo = pnNroRiesgo });
                }
                else if (nTpoRiesgo == (int)Riesgos.Autoevaluaciones)
                {
                    //model.oDatosRiesgo = evaluacion.MostrarDetalleEvaluacion(pnNroRiesgo);
                }
                model.lstObservacionesGestion = riesgo.ObtenerObservacionesGestion(pnNroRiesgo);

                if (pnNroProceso == (int)ProcesoGestion.Paso_1)
                {
                    model.oLstFactorRiesgo = constante.ObtenerConstantes(1021);
                    //model.oLstEventoPerdida = constante.ObtenerConstantes(1022);
                    model.LstClaseEventoPerdida = evento.ObtenerClaseEventoPerdida();
                    model.oLstLineaNegocio = lineaNegocio.ObtenerLineaNegocio();
                }
                else if (pnNroProceso == (int)ProcesoGestion.Paso_2)
                {
                    model.oLstProbabilidad = constante.ObtenerConstantes(1012);
                    model.oLstImpacto = constante.ObtenerConstantes(1011);
                }
                else if (pnNroProceso == (int)ProcesoGestion.Paso_3)
                {
                    int lnTotalControles = 0, lnSumEfectividadControl = 0;
                    var oLtsControlRiesgosResidual = riesgoResidual.ObtenerControlRiesgoResidual(new DetalleRiesgo() { nNroRiesgo = pnNroRiesgo });
                    if (oLtsControlRiesgosResidual.Count > 0)
                    {
                        model.oLstRiesgoResidual = oLtsControlRiesgosResidual;
                        //if (model.oLstRiesgoResidual.Where(x => x.oResposableDefinido == 0 && x.nPeriodoEjecucion == 0 && x.nEvidenciaControl == 0).ToList().Count == 0) //identificar que no existe aun control que no hay sido gestionado
                        if (model.oLstRiesgoResidual.Where(x => Convert.ToInt32(x.oResposableDefinido[0].Id) == 0 &&
                                                                Convert.ToInt32(x.oPeriodoEfecucion[0].Id) == 0 &&
                                                                Convert.ToInt32(x.oEvidenciaControl[0].Id) == 0).ToList().Count == 0)
                        {

                            var oInfRiesgoInherente = riesgo.ObtenerInfoGestionRiesgo(pnNroRiesgo, 2);
                            var lnValorEscalaRiesgoInherente = riesgoResidual.ObtenerEscalaNivelRiesgo(Convert.ToInt32(oInfRiesgoInherente[1]), Convert.ToInt32(oInfRiesgoInherente[2])).nValorEstala;

                            foreach (var controles in model.oLstRiesgoResidual)
                            {
                                //lnSumEfectividadControl += controles.nEfectividadControl;
                                lnSumEfectividadControl += Convert.ToInt32(controles.oEfectividadControl[0].Id);
                                lnTotalControles += 1;
                            }
                            var lnCalifEfecControl = CalculoRedondeo((double)lnSumEfectividadControl / lnTotalControles);
                            model.CalifEfecControl = lnCalifEfecControl;
                            model.oEscalaNivRiesgo = riesgoResidual.ObtenerNivelRiesgoResidualEscala(lnValorEscalaRiesgoInherente / lnCalifEfecControl);
                        }

                        //var oInfRiesgoInherente = riesgo.ObtenerInfoGestionRiesgo(pnNroRiesgo, 2);
                        //var lnValorEscalaRiesgoInherente = riesgoResidual.ObtenerEscalaNivelRiesgo(Convert.ToInt32(oInfRiesgoInherente[1]), Convert.ToInt32(oInfRiesgoInherente[2])).nValorEstala;

                        //foreach (var controles in model.oLstRiesgoResidual)
                        //{
                        //    lnSumEfectividadControl += controles.nEfectividadControl;
                        //    lnTotalControles += 1;
                        //}
                        //var lnCalifEfecControl = CalculoRedondeo((double)lnSumEfectividadControl / lnTotalControles);
                        //model.CalifEfecControl = lnCalifEfecControl;
                        //model.oEscalaNivRiesgo = riesgoResidual.ObtenerNivelRiesgoResidualEscala(lnValorEscalaRiesgoInherente / lnCalifEfecControl);
                    }
                    else { model.oLstRiesgoResidual = null; }

                    model.oLstResponsableDef = criterioEvaluacion.ObtenerCriteriosEvaluacion(101);
                    model.oLstFrecuenciaDef = criterioEvaluacion.ObtenerCriteriosEvaluacion(102);
                    model.oLstEvidenciaControl = criterioEvaluacion.ObtenerCriteriosEvaluacion(103);
                    model.oLstTipoEjecucion = criterioEvaluacion.ObtenerCriteriosEvaluacion(104);
                    model.oLstCumpleObjetivo = criterioEvaluacion.ObtenerCriteriosEvaluacion(105);

                }
                else if (pnNroProceso == (int)ProcesoGestion.Paso_4)
                {
                    var oModelPlanAccion = planAccion.MostrarPlanesAccionRiesgo(pnNroRiesgo);
                    if (oModelPlanAccion != null)
                    {
                        model.oLstPlanAccion = oModelPlanAccion;
                    }
                    else { model.oLstPlanAccion = null; }
                }
                else if (pnNroProceso == (int)ProcesoGestion.Paso_5)
                {
                    model.oLstAreas = areas.ObtenerAreas();
                    var oModelResponPlanAccion = planAccion.MostrarResponsablesPlanesAccionRiesgo(pnNroRiesgo);
                    if (oModelResponPlanAccion != null)
                    {
                        model.oLstReponPlanAccionRiesgo = oModelResponPlanAccion;
                    }
                    else { model.oLstReponPlanAccion = null; }
                }

                var LstInfoGestion = riesgo.ObtenerInfoGestionRiesgo(pnNroRiesgo, pnNroProceso);
                if (LstInfoGestion != null || LstInfoGestion.Count > 0)
                {
                    model.sLstInfoGestionRiesgo = LstInfoGestion;
                    if (pnNroProceso == (int)ProcesoGestion.Paso_1)
                    {
                        if (!String.IsNullOrEmpty(LstInfoGestion[1]) && !String.IsNullOrEmpty(LstInfoGestion[2]) && !String.IsNullOrEmpty(LstInfoGestion[3]))
                        {
                            var oModelProductos = producto.ObtenerProducto(model.sLstInfoGestionRiesgo[4]);
                            var oModelSubProducto = subProducto.ObtenerSubProducto(model.sLstInfoGestionRiesgo[5]);
                            var oModelSubEventoPerdida = evento.ObtenerSubClaseEventoPerdida(Convert.ToInt32(model.sLstInfoGestionRiesgo[2]));
                            if (oModelProductos != null && oModelSubProducto != null)
                            {
                                model.oLstProducto = oModelProductos;
                                model.oLstSubProducto = oModelSubProducto;
                                model.LstSubClaseEventoPerdida = oModelSubEventoPerdida;
                            }
                            else
                            {
                                model.oLstProducto = null;
                                model.oLstSubProducto = null;
                                model.LstSubClaseEventoPerdida = null;
                            }
                        }
                        else { model.sLstInfoGestionRiesgo = null; }
                    }
                    else if (pnNroProceso == (int)ProcesoGestion.Paso_2)
                    {
                        if (String.IsNullOrEmpty(LstInfoGestion[1]) && String.IsNullOrEmpty(LstInfoGestion[2]))
                        {
                            model.sLstInfoGestionRiesgo = null;
                        }
                    }
                    else if (pnNroProceso == (int)ProcesoGestion.Paso_3)
                    {
                        if (String.IsNullOrEmpty(LstInfoGestion[1]) && String.IsNullOrEmpty(LstInfoGestion[2]))
                        {
                            model.sLstInfoGestionRiesgo = null;
                        }
                    }


                }
                else { model.sLstInfoGestionRiesgo = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult GrabarObservacionesGestion(long pnNroRiesgo, string poObservaciones)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                dynamic d = JsonConvert.DeserializeObject(poObservaciones);
                List<dynamic> Observaciones = new List<dynamic>(d);
                int lnExito = riesgo.GrabarObservacionGestion(pnNroRiesgo, Observaciones, lsNroRiesgo);

                /*Realizar el correo de la notitifacion de modicacion*/
                if (lnExito > 0)
                {
                    int nTpoRiesgo = riesgo.ObtenerTpoRiesgo(pnNroRiesgo);
                    //var oRiesgoDet = nTpoRiesgo == (int)Riesgos.RiesgoOperacional ? riesgo.ObtenerInfoGeneralRiesgoOperacional(new Riesgo() { nNroRiesgo = pnNroRiesgo }) : evaluacion.MostrarDetalleEvaluacion(pnNroRiesgo);
                    var oRiesgoDet = riesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo() { nNroRiesgo = pnNroRiesgo });
                    var lsAsunto = "Notificación de Modificación " + oRiesgoDet.cCodRiesgo;
                    var lsContenidoPasos = "";

                    var configMail = new MailModel()
                    {
                        Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor,
                        Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor,
                        Pruebas = Convert.ToBoolean(Convert.ToInt32(constSistema.ObtenerConstanteSistema(200).cConsSisValor)),
                        CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor
                    };

                    for (int i = 0; i < Observaciones.Count; i++)
                    {
                        string cDesccripcionPaso = "";
                        switch ((int)Observaciones[i].nProceso.Value)
                        {
                            case 0:
                                cDesccripcionPaso = ConstGeneral.Get.DescProceso0;
                                break;
                            case 1:
                                cDesccripcionPaso = ConstGeneral.Get.DescProceso1;
                                break;
                            case 2:
                                cDesccripcionPaso = ConstGeneral.Get.DescProceso2;
                                break;
                            case 3:
                                cDesccripcionPaso = ConstGeneral.Get.DescProceso3;
                                break;
                            case 4:
                                cDesccripcionPaso = ConstGeneral.Get.DescProceso4;
                                break;
                            case 5:
                                cDesccripcionPaso = ConstGeneral.Get.DescProceso5;
                                break;
                        }
                        if (Observaciones[i].nProceso.Value == 0)
                        {
                            lsContenidoPasos += "<p><strong>" + "Observación Analista: " + "</strong><br />" + Observaciones[i].cObservacion.Value + "</p>";
                        }
                        else
                        {
                            lsContenidoPasos += "<p><strong>Paso " + Observaciones[i].nProceso.Value + " (" + cDesccripcionPaso + "):</strong><br />" + Observaciones[i].cObservacion.Value + "</p>";
                        }
                    }

                    //var lsTitulo = "Notificación de Modificación";
                    //var lsTitulo = "Modificación del riesgo " + oRiesgoDet.cCodRiesgo;
                    //List<string> mailDestino = new List<string>() { SendMail.Get.ObtenerMailUsuario(oRiesgoDet.oUsuarios.cUser) };

                    //string lsContenido = "";
                    //if (oRiesgoDet.nProcRiesgo == 0 && nTpoRiesgo == (int)Riesgos.RiesgoOperacional)
                    //{
                    //    lsContenido = "<p>El <strong>analista de riesgo operacional</strong>, solicitó la corrección y/o modificación " +
                    //                  "en el registro del riesgo operacional " + oRiesgoDet.cCodRiesgo + " según se detalla a continuación:</p>" +
                    //                  lsContenidoPasos +
                    //                  "<br/>";
                    //}
                    //else
                    //{
                    //    lsContenido = "<p>El <strong>analista de riesgo operacional</strong>, solicitó la corrección " +
                    //                      "de los siguientes pasos identificados en la gestión " + (nTpoRiesgo == (int)Riesgos.RiesgoOperacional ? "del riesgo operacional " : " de la evaluación ") + oRiesgoDet.cCodRiesgo +
                    //                      "según se detalla a continuación:</p>" +
                    //                      lsContenidoPasos +
                    //                      "<br/>";

                    //}

                    //var lsCorreo = ConstGeneral.Get.Correo(lsTitulo, lsContenido);

                    //_ = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, null, String.Empty, configMail);
                }

                return Json(new { ExitoGestion = lnExito, Mensaje = lnExito > 0 ? "Se realizó la notificación para la modifición del proceso" : "No se grabó observaciones en la evaluación del riesgo" });

            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerObservacionesGestion(long pnNroRiesgo)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                List<dynamic> datos = riesgo.ObtenerObservacionesGestion(pnNroRiesgo);
                //dynamic d = JsonConvert.DeserializeObject(lstObservaciones);
                //int lnExito = _riesgo.GrabarObservacionGestion(pnNroRiesgo, new List<dynamic>(d), lsNroRiesgo);

                /*Realizar el correo de la notitifacion de modicacion*/

                return Json(new { Observaciones = JsonConvert.SerializeObject(datos), Mensaje = "Observaciones obtenidas" });

            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult NotificarModificarRiesgo(long pnNroRiesgo)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                int nExito = riesgo.ConfirmarModificacionesRiesgo(pnNroRiesgo, lsNroRiesgo);

                /*Realizar el correo de la notitifacion de modicacion*/
                if (nExito > 0)
                {
                    var configMail = new MailModel()
                    {
                        Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor,
                        Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor,
                        Pruebas = Convert.ToBoolean(Convert.ToInt32(constSistema.ObtenerConstanteSistema(200).cConsSisValor)),
                        CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor
                    };

                    var oAnaRiesgo = this.iusuario.ObtenerUsuariosCargos(ConstGeneral.Get.AnalistaRO);
                    List<string> mailDestino = new List<string>(), mailCC = new List<string>();
                    for (int i = 0; i < oAnaRiesgo.Count; i++) { mailDestino.Add(SendMail.Get.ObtenerMailUsuario(oAnaRiesgo[i].cUser)); }
                    mailCC.Add(SendMail.Get.ObtenerMailUsuario(usuario.cUser));
                    var lsAsunto = "Notificación de Modificación"; //+ lsCodTaller;
                    var lsTitulo = "Modificacion Realizada";
                    var lsContenido = "Se informa que el usuario culminó con la modificación del riesgo, según se detalla";
                    var lsCorreo = ConstGeneral.Get.Correo(lsTitulo, lsContenido);

                    _ = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC, String.Empty, configMail);

                }
                return Json(new { Exito = nExito });
            }
            catch { throw; }
        }

        #region Gestion Riesgo - Riesgo Residual
        [HttpPost]
        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarControlesRiesgoResidual(long pnNroRiesgo, string __psComentario, int pnReponControl, int pnPeriEjec, int pnEvidenControl, int pnEjecControl,
                                                       int pnCumpleObj, int pnEfecControl)
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                var exito = riesgoResidual.RegistrarControlesRiesgoResidual(pnNroRiesgo, __psComentario, pnReponControl, pnPeriEjec, pnEvidenControl, pnEjecControl,
                                                                         pnCumpleObj, pnEfecControl, lsNroRiesgo);
                if (exito > 0)
                {
                    var oLtsControlRiesgosResidual = riesgoResidual.ObtenerControlRiesgoResidual(new DetalleRiesgo() { nNroRiesgo = pnNroRiesgo });
                    int lnTotalControles = 0, lnSumEfectividadControl = 0;
                    model.oLstRiesgoResidual = oLtsControlRiesgosResidual;

                    var oInfRiesgoInherente = riesgo.ObtenerInfoGestionRiesgo(pnNroRiesgo, 2);
                    var lnValorEscalaRiesgoInherente = riesgoResidual.ObtenerEscalaNivelRiesgo(Convert.ToInt32(oInfRiesgoInherente[1]), Convert.ToInt32(oInfRiesgoInherente[2])).nValorEstala;

                    foreach (var controles in model.oLstRiesgoResidual)
                    {
                        //lnSumEfectividadControl += controles.nEfectividadControl;
                        lnSumEfectividadControl += Convert.ToInt32(controles.oEfectividadControl[0].Id);
                        lnTotalControles += 1;
                    }
                    var lnCalifEfecControl = CalculoRedondeo((double)lnSumEfectividadControl / lnTotalControles);
                    model.CalifEfecControl = lnCalifEfecControl;
                    model.oEscalaNivRiesgo = riesgoResidual.ObtenerNivelRiesgoResidualEscala(lnValorEscalaRiesgoInherente / lnCalifEfecControl);
                }
                else { model.oLstRiesgoResidual = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ActualizarControlesRiesgoResidual(long pnNroRiesgo, int pnItem, string psComentario, int pnReponControl, int pnPeriEjec, int pnEvidenControl, int pnEjecControl,
                                                          int pnCumpleObj, int pnEfecControl)
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                var exito = riesgoResidual.ActualizarControlesRiesgoResidual(pnNroRiesgo, pnItem, psComentario, pnReponControl, pnPeriEjec, pnEvidenControl, pnEjecControl,
                                                                         pnCumpleObj, pnEfecControl, lsNroRiesgo);
                if (exito > 0)
                {
                    var oLtsControlRiesgosResidual = riesgoResidual.ObtenerControlRiesgoResidual(new DetalleRiesgo() { nNroRiesgo = pnNroRiesgo });
                    var oInfRiesgoInherente = riesgo.ObtenerInfoGestionRiesgo(pnNroRiesgo, 2);

                    var lnValorEscalaRiesgoInherente = riesgoResidual.ObtenerEscalaNivelRiesgo(Convert.ToInt32(oInfRiesgoInherente[1]), Convert.ToInt32(oInfRiesgoInherente[2])).nValorEstala;

                    int lnTotalControles = 0, lnSumEfectividadControl = 0;
                    model.oLstRiesgoResidual = oLtsControlRiesgosResidual;

                    if (model.oLstRiesgoResidual.Where(x => Convert.ToInt32(x.oResposableDefinido[0].Id) == 0 &&
                                                            Convert.ToInt32(x.oPeriodoEfecucion[0].Id) == 0 &&
                                                            Convert.ToInt32(x.oEvidenciaControl[0].Id) == 0).ToList().Count == 0)
                    {
                        foreach (var controles in model.oLstRiesgoResidual)
                        {
                            lnSumEfectividadControl += Convert.ToInt32(controles.oEfectividadControl[0].Id);
                            lnTotalControles += 1;
                        }
                        var lnCalifEfecControl = CalculoRedondeo((double)lnSumEfectividadControl / lnTotalControles);
                        model.CalifEfecControl = lnCalifEfecControl;
                        model.oEscalaNivRiesgo = riesgoResidual.ObtenerNivelRiesgoResidualEscala(lnValorEscalaRiesgoInherente / lnCalifEfecControl);
                    }


                }
                else { model.oLstRiesgoResidual = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult EliminarControlRiesgoResidual(long pnNroRiesgo, int pnItem)
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                var exito = riesgoResidual.EliminarControlRiesgoResidual(pnNroRiesgo, pnItem, lsNroRiesgo);

                if (exito > 0)
                {
                    var oLtsControlRiesgosResidual = riesgoResidual.ObtenerControlRiesgoResidual(new DetalleRiesgo() { nNroRiesgo = pnNroRiesgo });
                    if (oLtsControlRiesgosResidual.Count > 0)
                    {
                        int lnTotalControles = 0, lnSumEfectividadControl = 0;
                        model.oLstRiesgoResidual = oLtsControlRiesgosResidual;

                        var oInfRiesgoInherente = riesgo.ObtenerInfoGestionRiesgo(pnNroRiesgo, (int)ProcesoGestion.Paso_2);
                        var lnValorEscalaRiesgoInherente = riesgoResidual.ObtenerEscalaNivelRiesgo(Convert.ToInt32(oInfRiesgoInherente[1]), Convert.ToInt32(oInfRiesgoInherente[2])).nValorEstala;

                        foreach (var controles in model.oLstRiesgoResidual)
                        {
                            lnSumEfectividadControl += Convert.ToInt32(controles.oEfectividadControl[0].Id);
                            lnTotalControles += 1;
                        }
                        var lnCalifEfecControl = CalculoRedondeo((double)lnSumEfectividadControl / lnTotalControles);
                        model.CalifEfecControl = lnCalifEfecControl;
                        model.oEscalaNivRiesgo = riesgoResidual.ObtenerNivelRiesgoResidualEscala(lnValorEscalaRiesgoInherente / lnCalifEfecControl);
                    }
                    else { model.oLstRiesgoResidual = null; }

                }
                else { model.oLstRiesgoResidual = null; }


                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerCriteriosEvalControlRiesgoResidual(long pnNroRiesgo, int pnItem)
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {
                var oRiesgoResidualEN = riesgoResidual.ObtenerCriteriosEvalControlRiesgoResidual(pnNroRiesgo, pnItem);
                if (oRiesgoResidualEN != null)
                {
                    model.oRiesgoResidual = oRiesgoResidualEN;

                    model.oLstResponsableDef = criterioEvaluacion.ObtenerCriteriosEvaluacion(101);
                    model.oLstFrecuenciaDef = criterioEvaluacion.ObtenerCriteriosEvaluacion(102);
                    model.oLstEvidenciaControl = criterioEvaluacion.ObtenerCriteriosEvaluacion(103);
                    model.oLstTipoEjecucion = criterioEvaluacion.ObtenerCriteriosEvaluacion(104);
                    model.oLstCumpleObjetivo = criterioEvaluacion.ObtenerCriteriosEvaluacion(105);
                }
                //else { model.oRiesgoResidualEN = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ControlesRiesgoResidual(long pnNroRiesgo)
        {
            RiesgoResidualModel model = new RiesgoResidualModel();
            int lnTotalControles = 0, lnSumEfectividadControl = 0;
            try
            {
                var oLtsControlRiesgosResidual = riesgoResidual.ObtenerControlRiesgoResidual(new DetalleRiesgo() { nNroRiesgo = pnNroRiesgo });
                if (oLtsControlRiesgosResidual.Count > 0)
                {
                    model.oLstRiesgoResidual = oLtsControlRiesgosResidual;

                    var oInfRiesgoInherente = riesgo.ObtenerInfoGestionRiesgo(pnNroRiesgo, 2);
                    var lnValorEscalaRiesgoInherente = riesgoResidual.ObtenerEscalaNivelRiesgo(Convert.ToInt32(oInfRiesgoInherente[1]), Convert.ToInt32(oInfRiesgoInherente[2])).nValorEstala;

                    foreach (var controles in model.oLstRiesgoResidual)
                    {
                        //lnSumEfectividadControl += controles.nEfectividadControl;
                        //lnSumEfectividadControl += controles.oEfectividadControl;
                        lnTotalControles += 1;
                    }
                    var lnCalifEfecControl = CalculoRedondeo((double)lnSumEfectividadControl / lnTotalControles);
                    model.CalifEfecControl = lnCalifEfecControl;
                    model.oEscalaNivRiesgo = riesgoResidual.ObtenerNivelRiesgoResidualEscala(lnValorEscalaRiesgoInherente / lnCalifEfecControl);
                }
                else { model.oLstRiesgoResidual = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        /// <summary>
        /// El metodo redondea el valor de ControlesEfectividad/TotalControles
        /// </summary>
        /// <param name="pnValorRiesgoResidual"></param>
        /// <returns></returns>
        private int CalculoRedondeo(double pnValorRiesgoResidual)
        {
            int lnValorEntero = (int)pnValorRiesgoResidual;
            double lnValorRedondeo = Math.Round(pnValorRiesgoResidual);
            int lnValorResultado = 0;
            lnValorResultado = (int)(lnValorRedondeo);
            return lnValorResultado;
        }

        #endregion

        #region Gestion Riesgo - Riesgo Inherente
        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerNivelRiesgoInherente(int pnProbabilidad, int pnImpacto)
        {
            RiesgoInherenteModel model = new RiesgoInherenteModel();
            try
            {
                var oRiesgoInherente = riesgoInherente.ObtenerNivelRiesgoInherente(pnProbabilidad, pnImpacto);
                var oLstMontoPerdida = montoPerdida.ObtenerMontoPerdida();
                if (oLstMontoPerdida != null)
                {
                    model.oRiesgoInherente = oRiesgoInherente;
                    model.oMontoPerdida = oLstMontoPerdida.Where(x => x.nProbabilidad == pnProbabilidad && x.nImpacto == pnImpacto && x.bEstado == true).FirstOrDefault(); ;
                    model.oLstMontoPerdida = oLstMontoPerdida.Where(x => x.nProbabilidad == pnProbabilidad && x.nImpacto == pnImpacto).ToList();
                }
                else { model.oRiesgoInherente = null; model.oMontoPerdida = null; model.oLstMontoPerdida = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        #endregion

        #region Gestion Riesgo - Plan de Accion
        [RequiresAuthenticationAttribute]
        public JsonResult GrabarPlanAccion(long pnNroRiesgo, string __psDescPlanAccion, string pdFechaImplement, int pnSugerenciaGM, string __psComentarios, string psNombreDoc = "", string psNombreDocBD = "")
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);
                int exito = 0;
                exito = planAccion.RegistrarPlanAccion(pnNroRiesgo, __psDescPlanAccion,
                                                    Convert.ToDateTime(Regex.Replace(pdFechaImplement, @"\b(?<yyyy>\d{4})-(?<mm>\d{1,2})-(?<dd>\d{1,2})\b", "${dd}/${mm}/${yyyy}")),
                                                    pnSugerenciaGM, __psComentarios, lsNroRiesgo, psNombreDoc, psNombreDocBD);
                if (exito > 0)
                {
                    var oModelPlanesAccion = planAccion.MostrarPlanesAccionRiesgo(pnNroRiesgo);
                    model.oLstPlanAccion = oModelPlanesAccion;
                }
                else { model.oLstPlanAccion = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ActualizarPlanAccion(long pnNroRiesgo, long pnCodPlan, string psDescPlanAccion, string pdFechImplement, int pnSugerenciaGM, string psComentarios, string psUltimaActualizacion, string psNombreDoc = "", string psNombreDocBD = "")
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                int exito = planAccion.ActualizarPlanAccion(pnCodPlan, psDescPlanAccion,
                                                    Convert.ToDateTime(Regex.Replace(pdFechImplement, @"\b(?<yyyy>\d{4})-(?<mm>\d{1,2})-(?<dd>\d{1,2})\b", "${dd}/${mm}/${yyyy}")),
                                                    pnSugerenciaGM, psComentarios, lsNroRiesgo, psNombreDoc, psNombreDocBD);
                if (exito > 0)
                {
                    var oModelPlanesAccion = planAccion.MostrarPlanesAccionRiesgo(pnNroRiesgo);
                    model.oLstPlanAccion = oModelPlanesAccion;
                }
                else { model.oLstPlanAccion = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerPlanAccionRiesgoPlan(long pnNroRiesgo, long pnCodPlanAccion)
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {
                var oModelPlanAccion = planAccion.ObtenerPlanAccionRiesgoPlan(pnNroRiesgo, pnCodPlanAccion);
                if (oModelPlanAccion != null)
                {
                    model.oPlanAccion = oModelPlanAccion;
                }
                else { model.oPlanAccion = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }

        [RequiresAuthenticationAttribute]
        public JsonResult EliminarPlanAccion(long pnNroRiesgo, long pnPlanCod)
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);


                int exito = planAccion.EliminarPlanAccion(pnNroRiesgo, pnPlanCod, lsNroRiesgo);
                if (exito > 0)
                {
                    var oModelPlanesAccion = planAccion.MostrarPlanesAccionRiesgo(pnNroRiesgo);
                    model.oLstPlanAccion = oModelPlanesAccion;
                }
                else { model.oLstPlanAccion = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }

        [RequiresAuthenticationAttribute]
        public JsonResult MostarResponsablePlanAccion(long pnNroRiesgo, long pnCodPlanAccion)
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {
                var oModelReponPlanAccion = planAccion.MostrarDetalleResponsablesPlanesAccionRiesgo(pnNroRiesgo, pnCodPlanAccion);
                if (oModelReponPlanAccion != null)
                {
                    model.oLstReponPlanAccion = oModelReponPlanAccion;
                }
                else { model.oLstReponPlanAccion = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }


        //[HttpPost]
        //[EnableCors("_myAllowSpecificOrigins")]
        public ActionResult AdjuntarDocumento()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                ArchivoAdjunto loArchivos = new ArchivoAdjunto();

                string lsDirectorio = "~/Documentos";

                HttpFileCollectionBase archivos = Request.Files;
                HttpPostedFileBase archivo = archivos[0];

                bool existe = Directory.Exists(Server.MapPath(lsDirectorio));
                if (!existe)
                {
                    Directory.CreateDirectory(Server.MapPath(lsDirectorio));
                }

                loArchivos.lsNombreArchivo = Path.GetFileName(archivo.FileName);
                loArchivos.lsExtension = Path.GetExtension(archivo.FileName);
                loArchivos.lsNombreArchivoDB = usuario.cUser + (DateTime.Now.ToString("yyyyMMddHHmmss")).ToLower() + loArchivos.lsExtension;

                bool lbDirectExiste = Directory.Exists(Server.MapPath(lsDirectorio));

                if (!lbDirectExiste)
                {
                    Directory.CreateDirectory(Server.MapPath(lsDirectorio));
                }
                archivo.SaveAs(Server.MapPath(lsDirectorio + "/" + loArchivos.lsNombreArchivoDB));

                return Json(JsonConvert.SerializeObject(loArchivos));
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema al subir el archvivo en el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarResponsablePlanAccion(long pnNroRiesgo, long pnPlanCod, string psUserResponsable)
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {

                Usuario usuario = (Usuario)Session["Usuario"];

                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);
                int exito = 0;
                exito = planAccion.RegistrarResponsablePlanAccion(pnPlanCod, psUserResponsable, lsNroRiesgo);
                if (exito > 0)
                {
                    var oModelPlanesAccion = planAccion.MostrarResponsablesPlanesAccionRiesgo(pnNroRiesgo);
                    model.oLstReponPlanAccionRiesgo = oModelPlanesAccion;
                }
                else { model.oLstReponPlanAccion = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult QuitarResponsablePlanAccion(long pnNroRiesgo, long pnPlanCod, int pnItemResponsable)
        {
            GestionRiesgoModel model = new GestionRiesgoModel();
            try
            {

                Usuario usuario = (Usuario)Session["Usuario"];

                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);
                int exito = 0;
                exito = planAccion.QuitarResponsablePlanAccion(pnPlanCod, pnItemResponsable, lsNroRiesgo);
                if (exito > 0)
                {
                    model.oLstReponPlanAccionRiesgo = planAccion.MostrarResponsablesPlanesAccionRiesgo(pnNroRiesgo);
                    model.oLstReponPlanAccion = planAccion.MostrarDetalleResponsablesPlanesAccionRiesgo(pnNroRiesgo, pnPlanCod);

                }
                else { model.oLstReponPlanAccion = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarResponsableReasignado(long pnRiesgo, long pnPlanCod, string psUserResponsable, string psUserReasignado)
        {
            try
            {

                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                int exito = 0;
                var respuesta = new ResponseModel();
                exito = planAccion.RegistrarResponsablePlanAccionReasignado(pnPlanCod, psUserResponsable, psUserReasignado, lsNroRiesgo);

                var configMail = new MailModel()
                {
                    Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor,
                    Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor,
                    Pruebas = Convert.ToBoolean(Convert.ToInt32(constSistema.ObtenerConstanteSistema(200).cConsSisValor)),
                    CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor
                };
                if (exito > 0)
                {

                    List<string> mailDestino = new List<string>() { SendMail.Get.ObtenerMailUsuario(psUserReasignado) };
                    List<string> mailCC = new List<string>() { SendMail.Get.ObtenerMailUsuario(psUserResponsable) };

                    var LstRespondables = planAccion.ObtenerResponsablePlanAccion(pnPlanCod);
                    string lsResponsables = "";
                    foreach (var responsables in LstRespondables)
                    {
                        lsResponsables = Convert.ToString(responsables.nItemResponPlan) + ". " + new string(' ', 2) + iusuario.ObtenerDatosUsuario(responsables.cUserResponsable).oPersona.cPersNombre + "<br/>";
                    }

                    var oPlanAccion = planAccion.ObtenerPlanAccionRiesgoPlan(pnRiesgo, pnPlanCod);
                    var oRiesgo = riesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo()
                    {
                        nNroRiesgo = pnRiesgo
                    });
                    var lsTitulo = "Re-asignación de Plan de Acción";
                    var lsAsunto = "Notificación de Re-asignación de Plan de Acción " + Convert.ToString(pnPlanCod);
                    var lsContenido = "Ud. fue reasignado como responsable del plan de acción con código <strong>" + Convert.ToString(pnPlanCod) + "</strong>" +
                                      " del riesgo con código <strong>" + oRiesgo.cCodRiesgo + "</strong>, el cual se detalla a continuación." +
                                      "<p><strong>Riesgo identificado</strong><br/>" + oRiesgo.cRiesgoIdentiticado + "</p>" +
                                      "<p><strong>Plan de Acción</strong><br/>" + oPlanAccion.cPlanDescripcion + "</p>" +
                                      //"<p><strong>Comentarios</strong><br/>" + oDetPlanAccion.cComentario + "</p>" +
                                      "<p><strong>Responsable(s)</strong><br/>" + lsResponsables + "</p>";
                    var lsCorreo = ConstGeneral.Get.Correo(lsTitulo, lsContenido);

                    _ = SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC, String.Empty, configMail);

                    respuesta.ValorNotif = 1;
                    respuesta.MensajeNotif = "Se realizó la reasignación del responsable del plan de acción.";
                }
                else
                {
                    respuesta.ValorNotif = 0;
                    respuesta.MensajeNotif = "El responsable ya se encuentra asignado al plan de acción.";
                }
                return Json(JsonConvert.SerializeObject(respuesta));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        #endregion

        #endregion

        #region Planes de Accion 
        //[BreadCrumb(Clear = true, Label = "Planes de Acción")]
        [RequiresAuthenticationAttribute]
        public ActionResult PlanesAccion(int pnPagina = 1)
        {
            try
            {
                var modelAreas = areas.ObtenerAreas();
                return View(modelAreas);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        [HttpPost]
        [RequiresAuthenticationAttribute]
        public ActionResult ListadoPlanesAccion(string busqueda = "", int pagina = 1, int elementos = 10)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                var resultado = GetPlanesAccion(pagina, elementos, usuario.cRHCargoCod == ConstGeneral.Get.AnalistaRO ? "" : usuario.cUser, busqueda);
                return PartialView("_PlanesAccion", resultado);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        private Paginador<PlanAccion> GetPlanesAccion(int pnPagina, int pnRegitroPagina, string psUsuario = "", string psFiltro = "")
        {

            List<PlanAccion> lstPlanAccion = planAccion.ObtenerPlanAccionResponsable(psUsuario, (int)Riesgos.RiesgoOperacional, psFiltro);

            int total_registro = lstPlanAccion.Count;
            List<PlanAccion> PlanesAccion = lstPlanAccion.OrderByDescending(x => x.nPlanCod)
                                                            .Skip((pnPagina - 1) * pnRegitroPagina)
                                                            .Take(pnRegitroPagina)
                                                            .ToList();
            var total_paginas = (int)Math.Ceiling((double)total_registro / pnRegitroPagina);
            Paginador<PlanAccion> FiltroPlanesAccion = new Paginador<PlanAccion>()
            {
                registro_pagina = pnRegitroPagina,
                total_registros = total_paginas,
                total_paginas = total_paginas,
                pagina_actual = pnPagina,
                resultado = PlanesAccion
            };

            return FiltroPlanesAccion;
        }

        //[BreadCrumb(Label = "Comentarios del Plan de Acción")]
        [RequiresAuthenticationAttribute]
        public ActionResult ComentarioPlanAccion(string riesgo, long planaccion)
        {
            try
            {
                var nNroRiesgo = Encripta.base64Decode(Convert.ToString(riesgo));
                var TpoRiesgo = this.riesgo.ObtenerTpoRiesgo((long)Convert.ToInt32(nNroRiesgo));

                var model = new GestionRiesgoModel()
                {
                    oPlanAccion = planAccion.ObtenerPlanAccionRiesgoPlan((long)Convert.ToInt32(nNroRiesgo), planaccion),
                    //oDatosRiesgo = TpoRiesgo == (int)Riesgos.RiesgoOperacional ? this.riesgo.ObtenerInfoGeneralRiesgoOperacional(new Riesgo() { nNroRiesgo = (long)Convert.ToInt32(nNroRiesgo) }) : evaluacion.MostrarDetalleEvaluacion((long)Convert.ToInt32(nNroRiesgo))
                };
                return View(model);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ListaComentariosPlanAccion(long pnCodPlan)
        {
            try
            {
                var ListaComentarios = planAccion.ObtenerComenariosPlanAccion(pnCodPlan);

                return PartialView("_ComentariosPlanAccion", ListaComentarios);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        //public async Task<JsonResult> GuardaComentarioPlanAccion(long pnPlanCod, string psComentario)
        [RequiresAuthenticationAttribute]
        public JsonResult GuardaComentarioPlanAccion(long pnPlanCod, string psComentario)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                int lnExito = planAccion.GuardarComentariosPlanAccion(pnPlanCod, usuario.cUser, psComentario);

                if (lnExito > 0)
                {
                    #region Envio de Correo - Evaluar
                    /*Comentado hasta hacer asincrono el metodo de envio de correo*/
                    //var oPlanAccion = planAccion.ObtenerResponsablePlanAccion(pnPlanCod);
                    //var oAnalistaRiesgo = iusuario.ObtenerUsuariosCargos(ConstGeneral.AnalistaRO);

                    //List<string> mailCC = new List<string>(), mailDestino = new List<string>();
                    //string lsAsunto = "", lsTitulo = "", lsContenido = "", lsCorreo = "";
                    ///*Recorrido por todos los analistas de riesgo operacional*/
                    //for (int i = 0; i < oAnalistaRiesgo.Count; i++) { mailCC.Add(SendMail.Get.ObtenerMailUsuario(oAnalistaRiesgo[i].cUser)); }
                    ///*Recorrido por todos los responsables*/
                    //for (int i = 0; i < oPlanAccion.Count; i++) { mailDestino.Add(SendMail.Get.ObtenerMailUsuario(oPlanAccion[i].cUserResponsable)); }
                    //if (usuario.cRHCargoCod == ConstGeneral.AnalistaRO)
                    //{
                    //    lsAsunto = "Comentario del Analista de Riesgo Operacional: Plan Acción " + Convert.ToString(pnPlanCod); //+ lsCodTaller;
                    //    lsTitulo = "";
                    //    lsContenido = "El analista de riesgo operacional " + usuario.cUser + ", realizó comentarios sobre el plan de acción " + Convert.ToString(pnPlanCod);
                    //    lsCorreo = ConstGeneral.Correo(lsTitulo, lsContenido);
                    //}
                    //else
                    //{
                    //    lsAsunto = "Respuesta del Responsable: Plan Accion " + Convert.ToString(pnPlanCod);
                    //    lsTitulo = "";
                    //    lsContenido = "El responsable del plan de acción " + Convert.ToString(pnPlanCod) + ", respondió a los comentarios realizados " +
                    //        "sobre el plan de acción " + Convert.ToString(pnPlanCod);
                    //    lsCorreo = ConstGeneral.Correo(lsTitulo, lsContenido);
                    //}

                    //await SendMail.Get.EnvioMail(mailDestino, lsAsunto, lsCorreo, mailCC, "");
                    #endregion

                }

                return Json(new { Exito = lnExito });
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerFechaImplementacionPlanAccion(long pnPlanCod)
        {
            try
            {
                GestionRiesgoModel model = new GestionRiesgoModel();
                model.lstObservacionesGestion = planAccion.ObtenerFechasImplementacionPlanAccion(pnPlanCod); //utilizo "lstObservacionesGestion" por ser List<dynamic>

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        //[RequiresAuthenticationAttribute]
        //public JsonResult ObtenerResponsablePlanAccionReasinacion(long pnNroRiesgo, long pnPlanAccion) {
        //    try
        //    {
        //        var responsable = planAccion.ObtenerResponsablePlanAccion(pnPlanAccion);
        //        return Json(JsonConvert.SerializeObject(planAccion.MostrarResponsablesPlanesAccionRiesgo(pnNroRiesgo).Where(x=> x.oPlanAccion.nPlanCod == pnPlanAccion).ToList()));
        //    }
        //    catch { throw; }
        //}

        #endregion

        #region Gestion de Incentivos
        //[BreadCrumb(Clear = true, Label = "Gestión de Incentivos")]
        [RequiresAuthenticationAttribute]
        public ActionResult GestionIncentivo()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario.cRHCargoCod != ConstGeneral.Get.AnalistaRO)
                {
                    var error = Utils.Error.GetError.GetErrorModel("Acceso restringido", (HttpStatusCode)404, "¡Al parecer no debería estar aqui!. Ud. no tiene el cargo necesario para acceder a esta seccioón del sistema.");
                    return View("Error", error);
                }
                //var model = new GestionRiesgoModel()
                //{
                //    oLstIncentivos = constante.ObtenerConstantes(1515)
                //};
                //return View(model);
                return View();
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult MostrarRiesgosOperacionalesIncentivos(int pnTrimestre, int pnAnio)
        {
            IncentivoModel model = new IncentivoModel();
            try
            {
                var oRiesgosIncentivos = gestionIncentivo.ObtenerRiesgoOperacionalIncentivo(pnTrimestre, pnAnio);
                if (oRiesgosIncentivos != null)
                {
                    model.oLstIncentivos = oRiesgosIncentivos;
                }
                else { model.oLstIncentivos = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult GrabaGestionIncentivo(long pnNroRiesgo, int pnMotivo, string psComentario, decimal psMontoInc,
                                                    string psNombreDoc, string psNombreDocDB)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);
                var exito = gestionIncentivo.GrabaGestionIncentivo(pnNroRiesgo, pnMotivo, psComentario, psMontoInc, psNombreDoc, psNombreDocDB, lsNroRiesgo);
                return Json(JsonConvert.SerializeObject(exito));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerDetalleIncentivo(long pnNroRiesgo)
        {
            try
            {
                var incentivoDet = gestionIncentivo.ObtenerDetalleIncentivo(pnNroRiesgo);
                var tpoIncentivo = constante.ObtenerConstantes(1515);
                return Json(new { oTpoIncentivo = tpoIncentivo, oIncentivoDet = incentivoDet });
            }
            catch { throw; }
        }

        /*Reporte de Incentivo*/
        [RequiresAuthenticationAttribute]
        public ActionResult ExportarInformeIncentivo(int pnTrimestre, int pnAnio)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                var reporte = GenerarInformeGestionIncentivo(pnTrimestre, pnAnio);
                return new ExcelResult(reporte, "Gestión de Incentivos [" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        private XLWorkbook GenerarInformeGestionIncentivo(int pnTrimestre, int pnAnio)
        {
            try
            {
                var GestionIncentivos = gestionIncentivo.ObtenerRiesgoOperacionalIncentivo(pnTrimestre, pnAnio);

                XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoIncentivos.xlsx"), XLEventTracking.Disabled);

                IXLWorksheet ws = wb.Worksheet(1); //Hoja 1

                int fila = 5;
                decimal TotalPropuesto = 0.0M, TotalIncentivado = 0.0M;
                foreach (var incentivo in GestionIncentivos)
                {
                    ws.Cell("A" + fila).Value = incentivo.oDatosRiesgo.cCodRiesgo;
                    ws.Cell("A" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("A" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("A" + fila).Style.Font.FontSize = 9;
                    ws.Cell("A" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("A" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("A" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("B" + fila).Value = incentivo.oDatosRiesgo.cRiesgoIdentiticado;
                    ws.Cell("B" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("B" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("B" + fila).Style.Font.FontSize = 9;
                    ws.Cell("B" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("B" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                    ws.Cell("B" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("C" + fila).Value = incentivo.oDatosRiesgo.oUsuarios.cUser;
                    ws.Cell("C" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("C" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("C" + fila).Style.Font.FontSize = 9;
                    ws.Cell("C" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("C" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("C" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("D" + fila).Value = incentivo.oProbabilidadInherente.cConsDescripcion;
                    ws.Cell("D" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("D" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("D" + fila).Style.Font.FontSize = 9;
                    ws.Cell("D" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("D" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("D" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("E" + fila).Value = incentivo.oImpactoInherente.cConsDescripcion;
                    ws.Cell("E" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("E" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("E" + fila).Style.Font.FontSize = 9;
                    ws.Cell("E" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("E" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("E" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("F" + fila).Value = ConstGeneral.Get.DescripcionNivelRiesgo((int)incentivo.oNivelRiesgoInherente.nConsValor);
                    ws.Cell("F" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("F" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("F" + fila).Style.Font.FontSize = 9;
                    ws.Cell("F" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("F" + fila).Style.Fill.BackgroundColor = XLColor.FromHtml(ConstGeneral.Get.ColorNivelRiesgo((int)incentivo.oNivelRiesgoInherente.nConsValor));
                    ws.Cell("F" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("F" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("G" + fila).Value = incentivo.oMontoPerdida.nMontoPerdida;
                    ws.Cell("G" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("G" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("G" + fila).Style.Font.FontSize = 9;
                    ws.Cell("G" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("G" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("G" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                    ws.Cell("H" + fila).Value = incentivo.nMontoPropuesto;
                    ws.Cell("H" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("H" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("H" + fila).Style.Font.FontSize = 9;
                    ws.Cell("H" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("H" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("H" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    TotalPropuesto += (decimal)incentivo.nMontoPropuesto;
                    TotalIncentivado += (decimal)incentivo.nMontoIncentivo;

                    //Detalle de la gestion
                    var detalleGestion = gestionIncentivo.ObtenerDetalleIncentivo(incentivo.oDatosRiesgo.nNroRiesgo);
                    if (detalleGestion != null)
                    {

                        ws.Cell("I" + fila).Value = detalleGestion.nMontoIncentivo;
                        ws.Cell("I" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("I" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("I" + fila).Style.Font.FontSize = 9;
                        ws.Cell("I" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("I" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("I" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("J" + fila).Value = detalleGestion.cComentarioIncentivo;
                        ws.Cell("J" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("J" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("J" + fila).Style.Font.FontSize = 9;
                        ws.Cell("J" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("J" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("J" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;



                        ws.Cell("K" + fila).Value = detalleGestion.oTipoIncentivo.cConsDescripcion;
                        ws.Cell("K" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("K" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("K" + fila).Style.Font.FontSize = 9;
                        ws.Cell("K" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("K" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("K" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("L" + fila).Value = detalleGestion.bIncentivo ? "Gestionado" : "Por gestionar";
                        ws.Cell("L" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("L" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("L" + fila).Style.Font.FontSize = 9;
                        ws.Cell("L" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("L" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("L" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("M" + fila).Value = detalleGestion.dFechaGestion;
                        ws.Cell("M" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("M" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("M" + fila).Style.Font.FontSize = 9;
                        ws.Cell("M" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("M" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("M" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }
                    fila++;
                }

                int filasUsadas = ws.LastRowUsed().RowNumber();

                ws.Cell("H" + (filasUsadas + 1)).Value = TotalPropuesto;
                ws.Cell("H" + (filasUsadas + 1)).Style.Alignment.WrapText = true;
                ws.Cell("H" + (filasUsadas + 1)).Style.Font.FontName = "Segoe UI";
                ws.Cell("H" + (filasUsadas + 1)).Style.Font.FontSize = 9;
                ws.Cell("H" + (filasUsadas + 1)).Style.Font.Bold = true;
                ws.Cell("H" + (filasUsadas + 1)).Style.Fill.BackgroundColor = XLColor.FromHtml("#E7E6E6");
                ws.Cell("H" + (filasUsadas + 1)).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Cell("H" + (filasUsadas + 1)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("H" + (filasUsadas + 1)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                ws.Cell("I" + (filasUsadas + 1)).Value = TotalIncentivado;
                ws.Cell("I" + (filasUsadas + 1)).Style.Alignment.WrapText = true;
                ws.Cell("I" + (filasUsadas + 1)).Style.Font.FontName = "Segoe UI";
                ws.Cell("I" + (filasUsadas + 1)).Style.Font.FontSize = 9;
                ws.Cell("I" + (filasUsadas + 1)).Style.Font.Bold = true;
                ws.Cell("I" + (filasUsadas + 1)).Style.Fill.BackgroundColor = XLColor.FromHtml("#E7E6E6");
                ws.Cell("I" + (filasUsadas + 1)).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Cell("I" + (filasUsadas + 1)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("I" + (filasUsadas + 1)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                return wb;
            }
            catch { throw; }
        }

        #endregion

        #region Cargas principales


        [RequiresAuthenticationAttribute]
        public JsonResult ListaAgencias()
        {
            model = new GeneralModel();
            try
            {
                var oAgencias = agencias.ObtenerAgencias();
                if (oAgencias != null)
                {
                    model.oLstAgencia = oAgencias;
                }
                else { model.oLstAgencia = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarAreasAgencia(string psAgeCod)
        {
            model = new GeneralModel();
            try
            {
                var oAreasAgencias = areas.ObtenerAreasAgencia(psAgeCod);
                if (oAreasAgencias != null)
                {
                    model.oLstAreas = oAreasAgencias;
                }
                else { model.oLstAreas = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }
        public JsonResult ListaAreas()
        {
            model = new GeneralModel();
            try
            {
                var oAreas = areas.ObtenerAreas();
                if (oAreas != null)
                {
                    model.oLstAreas = oAreas;
                }
                else { model.oLstAreas = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarProcesosAreas(string psCodArea = "")
        {
            model = new GeneralModel();
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                var oProcesoArea = procesoArea.ObtenerProcesoAreas(psCodArea = (psCodArea.Length > 0) ? psCodArea : usuario.oAreas.cAreaCod);
                if (oProcesoArea != null)
                {
                    model.oLstProcesoArea = oProcesoArea;
                }
                else { model.oLstProcesoArea = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarSubProcesosAreas(string psCodProceso)
        {
            model = new GeneralModel();
            try
            {
                var oSubProcesoArea = subProceso.ObtenerSubProcesosAreas(psCodProceso);
                if (oSubProcesoArea != null)
                {
                    model.oLstSubProcesoArea = oSubProcesoArea;
                }
                else { model.oLstSubProcesoArea = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListaControlesProceso(string psCodProceso = "")
        {
            model = new GeneralModel();
            try
            {
                var oControlProceso = controlProceso.ObtenerControlesProceso(psCodProceso);
                if (oControlProceso != null)
                {
                    model.oLstControlesAreas = oControlProceso;
                }
                else { model.oLstControlesAreas = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarCausasRiesgo()
        {
            model = new GeneralModel();
            try
            {
                var oCausaRiesgo = causas.ObtenerCausasRiesgo();
                if (oCausaRiesgo != null)
                {
                    model.oLstCausaRiesgo = oCausaRiesgo;
                }
                else { model.oLstCausaRiesgo = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarConstante(int pnConsCod)
        {
            model = new GeneralModel();
            try
            {
                var oConstantes = constante.ObtenerConstantes(pnConsCod);
                if (oConstantes != null)
                {
                    model.oLstConstante = oConstantes;
                }
                else { model.oLstConstante = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarUsuarioAreaAgencia(string psAgeCod, string psAreaCod)
        {
            model = new GeneralModel();
            try
            {
                var oUsuarios = iusuario.ObtenerUsuarioAgenciaArea(psAgeCod, psAreaCod);
                if (oUsuarios != null)
                {
                    model.oLstUsuarios = oUsuarios;
                }
                else { model.oLstUsuarios = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarUsuarioArea(string psAreaCod)
        {
            model = new GeneralModel();
            try
            {
                var oUsuarios = iusuario.ObtenerUsuarioArea(psAreaCod);
                if (oUsuarios != null)
                {
                    model.oLstUsuarios = oUsuarios;
                }
                else { model.oLstUsuarios = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarLineaNegocio()
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

        [RequiresAuthenticationAttribute]
        public JsonResult ListarSubLineaNegocio(string psCodLineaNegocio)
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

        [RequiresAuthenticationAttribute]
        public JsonResult ListarProducto(string psCodLineaNegocio)
        {
            ProductoModel model = new ProductoModel();
            try
            {
                var oProducto = producto.ObtenerProducto(psCodLineaNegocio);
                if (oProducto != null)
                {
                    model.oLstProducto = oProducto;
                }
                else { model.oLstProducto = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarSubProducto(string psCodProducto)
        {
            SubProductoModel model = new SubProductoModel();
            try
            {
                var oSubProducto = subProducto.ObtenerSubProducto(psCodProducto);
                if (oSubProducto != null)
                {
                    model.oLstSubProducto = oSubProducto;
                }
                else { model.oLstSubProducto = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarCriteriosEval(int psCriterioCod)
        {
            CriteriosEvaluacionModel model = new CriteriosEvaluacionModel();
            try
            {
                var oCriteriosEval = criterioEvaluacion.ObtenerCriteriosEvaluacion(psCriterioCod);
                if (oCriteriosEval != null)
                {
                    model.oLstCriteriosEval = oCriteriosEval;
                }
                else { model.oLstCriteriosEval = null; }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        #endregion

        #region Redirecciones
        [RequiresAuthenticationAttribute]
        public JsonResult DevolverVista(string Vista, string Controlador)
        {
            return Json(Url.Action(Vista, Controlador));
        }

        [RequiresAuthenticationAttribute]
        public JsonResult DevolverVistaParam(string Vista, string Controlador, string Parametros)
        {
            dynamic parametro = JsonConvert.DeserializeObject(Parametros);
            //return Json(new { url = Url.Action(Vista, Controlador) + "?" + FormatParam(Convert.ToString(parametro)) });
            return Json(new { url = Url.Action(Vista, Controlador, new RouteValueDictionary(parametro)) });
        }
        //private string FormatParam(string parametros)
        //{
        //    var param = parametros.Replace("\r\n", "").Replace("\"", "").Replace(":", "=").Replace("{", "").Replace("}", "").Replace(",", "&").Replace(" ", "").Trim();
        //    return param;
        //}
        #endregion

    }
}
