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
using System.Web.Mvc;
using System.Linq;
using System.Net;

namespace SIRO.Controllers
{
    public class EventoPerdidaController : Controller
    {
        #region Instancias
        private IMaestroApp maestro;
        private IConstantesApp constante;
        private IAgenciasApp agencias;
        private IAreasApp areas;
        private ICausaRiesgoApp causas;
        private IProcesoAreaApp procesoArea;
        private ISubProcesoApp subProceso;
        private ILineaNegocioApp lineaNegocio;
        private ISubLineaNegocioApp subLineaNegocio;
        private IEventoPerdidaApp ievento;

        public EventoPerdidaController(IMaestroApp maestro, IConstantesApp constante, IAgenciasApp agencias, IAreasApp areas, ICausaRiesgoApp causas, IProcesoAreaApp procesoArea,
                                        ISubProcesoApp subProceso, ILineaNegocioApp lineaNegocio, ISubLineaNegocioApp subLineaNegocio, IEventoPerdidaApp ievento)
        {
            this.maestro = maestro;
            this.constante = constante;
            this.agencias = agencias;
            this.areas = areas;
            this.causas = causas;
            this.procesoArea = procesoArea;
            this.subProceso = subProceso;
            this.lineaNegocio = lineaNegocio;
            this.subLineaNegocio = subLineaNegocio;
            this.ievento = ievento;
        }
        #endregion

        #region Registro de Evento de Perdida
        //[BreadCrumb(Clear = true, Label = "Registro Evento de Pérdida")]
        [RequiresAuthenticationAttribute]
        public ActionResult Registrar()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario.cRHCargoCod != ConstGeneral.Get.AnalistaRO)
                {
                    var validacion = Utils.Error.GetError.GetErrorModel("Acceso no Autorizado", (HttpStatusCode)404, "¡Al parecer no debería estar aqui!. Ud no tiene el cargo necesario para ingresar al módulo.");
                    return View("Error", validacion);
                }


                var model = new GestionRiesgoModel()
                {
                    oLstAgencia = agencias.ObtenerAgencias(),
                    oLstAreas = areas.ObtenerAreas(),
                    LstClaseEventoPerdida = ievento.ObtenerClaseEventoPerdida(),
                    oLstTipoCobertura = constante.ObtenerConstantes(6000),
                    oLstDescCortaEventoP = constante.ObtenerConstantes(1023),
                    //oLstSubEventoPerdida = EventoPerdidaLN.ObtenerSubEventoPerdida(Convert.ToInt32(EventoPerdida.oDatosRiesgo.nNroRiesgo)),
                    oLstLineaNegocio = lineaNegocio.ObtenerLineaNegocio()
                };
                return View(model);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ValidarEventoPerdida(string evento)
        {
            try
            {
                var getValor = ievento.ValidarEventoPerdida(evento);
                return Json(new { Valor = getValor });
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarCuentasContables(string psFiltro)
        {
            try
            {
                return Json(JsonConvert.SerializeObject(ievento.ObtenerCuentasContables(psFiltro)));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerSubClasesEventoPerdida(int pnClaseEventoP)
        {
            try
            {
                return Json(JsonConvert.SerializeObject(ievento.ObtenerSubClaseEventoPerdida(pnClaseEventoP)));
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerTipoCambio(string psFecha)
        {
            try
            {
                var dFecha = Convert.ToDateTime(psFecha).ToString("yyyyMMdd");
                return Json(new { Valor = ievento.ObtenerTipoCambio(Convert.ToString(dFecha)) });
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult GrabarEventoPerdida(int pnCodDescCorta, string __psEventoDesc, string __psMedidasDesc, string __psAccionesDesc, string psAgeCod, string psAreaCod, string pdFechaOcurrencia, string pdFechaDescubrimiento, string pdFechaRegContable,
                                                string psAnio, int pnClasEventoP, int pnSubClasEventoP, int pnReportado, string psCtaCont, string psLineaNeg, string psSubLineaNeg,
                                                int pnCobertura, string psProceso, int pnSubProceso, int pnPenMontoPerdida, double pnMontoPerdida, int pnPenMontoRecupera, double pnMontoRecuperado,
                                                int pnPenMontoProvisiona, double pnMontoProvision, double pnMontoBruto, double pnPerdidaNeta, int pbAsocRiesgo, string psCodEventoPadre = "",
                                                string poDetCta = "", string poDetGastos = "")
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                List<dynamic> oCuentas = null;
                if (!String.IsNullOrEmpty(poDetCta))
                {
                    dynamic dCuentas = JsonConvert.DeserializeObject(poDetCta);
                    oCuentas = new List<dynamic>(dCuentas);
                }

                List<dynamic> oGastos = null;
                if (!String.IsNullOrEmpty(poDetGastos))
                {
                    dynamic dGastos = JsonConvert.DeserializeObject(poDetGastos);
                    oGastos = new List<dynamic>(dGastos);
                }

                var lsrespuesta = ievento.GrabarEventoPerdida(pnCodDescCorta, __psEventoDesc, __psMedidasDesc, __psAccionesDesc, psAgeCod, psAreaCod, pdFechaOcurrencia, pdFechaDescubrimiento, pdFechaRegContable,
                                                 psAnio, pnClasEventoP, pnSubClasEventoP, Convert.ToBoolean(pnReportado), psCtaCont, psLineaNeg, psSubLineaNeg,
                                                 pnCobertura, psProceso, pnSubProceso, pnPenMontoPerdida, pnMontoPerdida, pnPenMontoRecupera, pnMontoRecuperado,
                                                 pnPenMontoProvisiona, pnMontoProvision, pnMontoBruto, pnPerdidaNeta, Convert.ToBoolean(pbAsocRiesgo), lsNroRiesgo,
                                                 psCodEventoPadre, oCuentas, oGastos);

                return Json(new { respuesta = lsrespuesta });
            }
            catch { throw; }
        }


        #endregion

        #region Modificar Evento de Perdida
        //[BreadCrumb(Label = "Modificar Evento de Pérdida")]
        [RequiresAuthenticationAttribute]
        public ActionResult Modificar(string evento)
        {

            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario.cRHCargoCod == ConstGeneral.Get.AnalistaRO || usuario.cRHCargoCod == "006006")
                {
                    string nNroRiesgo = Encripta.base64Decode(Convert.ToString(evento));

                    GestionRiesgoModel model = new GestionRiesgoModel();
                    model.oEventoPerdida = ievento.ObtenerDetalleEventoPerdida((long)Convert.ToInt64(nNroRiesgo));
                    model.oLstAgencia = agencias.ObtenerAgencias();
                    model.oLstAreas = areas.ObtenerAreas();
                    model.LstClaseEventoPerdida = ievento.ObtenerClaseEventoPerdida();
                    model.LstSubClaseEventoPerdida = ievento.ObtenerSubClaseEventoPerdida(model.oEventoPerdida.oClasesEventoPerdida.nCodClasEvento);
                    model.oLstTipoCobertura = constante.ObtenerConstantes(6000);
                    model.oLstDescCortaEventoP = constante.ObtenerConstantes(1023);
                    model.oLstGastosEvento = ievento.ObtenerDetGastosEventoPerdida((long)Convert.ToInt64(nNroRiesgo));
                    model.oLstCtaContEvento = ievento.ObtenerDetCtaContablesEventoPerdida((long)Convert.ToInt32(nNroRiesgo));
                    model.oLstLineaNegocio = lineaNegocio.ObtenerLineaNegocio();
                    model.oLstSubLineaNegocio = subLineaNegocio.ObtenerSubLineaNegocio(model.oEventoPerdida.oLineaNeg.cCodLineaNeg);
                    model.oLstProcesoAreas = procesoArea.ObtenerProcesoAreas(model.oEventoPerdida.oDatosRiesgo.oAreas.cAreaCod);
                    model.oLstSubProcesoAreas = subProceso.ObtenerSubProcesosAreas(model.oEventoPerdida.oDatosRiesgo.oProceso.cCodProceso);

                    return View(model);
                }
                else {
                    var validacion = Utils.Error.GetError.GetErrorModel("Acceso no Autorizado", (HttpStatusCode)404, "¡Al parecer no debería estar aqui!. Ud no tiene el cargo necesario para ingresar al módulo.");
                    return View("Error", validacion);
                }
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult GrabarActualizacionEventoPerdida(long pnCodEvento, int pnCodDescCorta, string __psEventoDesc, string __psMedidasDesc, string __psAccionesDesc, string psAgeCod, string psAreaCod, string pdFechaOcurrencia, string pdFechaDescubrimiento, string pdFechaRegContable,
                                                           string psAnio, int pnClasEventoP, int pnSubClasEventoP, int pnReportado, string psCtaCont, string psLineaNeg, string psSubLineaNeg,
                                                           int pnCobertura, string psProceso, int pnSubProceso, int pnPenMontoPerdida, double pnMontoPerdida, int pnPenMontoRecupera, double pnMontoRecuperado,
                                                           int pnPenMontoProvisiona, double pnMontoProvision, double pnMontoBruto, double pnPerdidaNeta, int pbAsocRiesgo, string psCodEventoPadre = "",
                                                           string poDetCta = "", string poDetGastos = "")
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

                List<dynamic> oCuentas = null;
                if (!String.IsNullOrEmpty(poDetCta))
                {
                    dynamic dCuentas = JsonConvert.DeserializeObject(poDetCta);
                    oCuentas = new List<dynamic>(dCuentas);
                }

                List<dynamic> oGastos = null;
                if (!String.IsNullOrEmpty(poDetGastos))
                {
                    dynamic dGastos = JsonConvert.DeserializeObject(poDetGastos);
                    oGastos = new List<dynamic>(dGastos);
                }

                var lbRespuesta = ievento.GrabarActualizacionEventoPerdida(pnCodEvento, pnCodDescCorta, __psEventoDesc, __psMedidasDesc, __psAccionesDesc, psAgeCod, psAreaCod, pdFechaOcurrencia, pdFechaDescubrimiento, pdFechaRegContable,
                                                                           psAnio, pnClasEventoP, pnSubClasEventoP, Convert.ToBoolean(pnReportado), psCtaCont, psLineaNeg, psSubLineaNeg,
                                                                           pnCobertura, psProceso, pnSubProceso, pnPenMontoPerdida, pnMontoPerdida, pnPenMontoRecupera, pnMontoRecuperado,
                                                                           pnPenMontoProvisiona, pnMontoProvision, pnMontoBruto, pnPerdidaNeta, Convert.ToBoolean(pbAsocRiesgo), lsNroRiesgo,
                                                                           psCodEventoPadre, oCuentas, oGastos);

                return Json(new { respuesta = lbRespuesta });
            }
            catch { throw; }
        }

        #endregion

        #region Lista de Evento de Perdida
        //[BreadCrumb(Clear = true, Label = "Evento de Pérdida")]
        [RequiresAuthenticationAttribute]
        public ActionResult Lista()
        {
            try
            {
                return View();
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarEventosPerdidas()
        {
            try
            {
                var EventoPerdida = ievento.ObtenerEventosPerdida();
                return Json(JsonConvert.SerializeObject(EventoPerdida));
            }
            catch { throw; }
        }

        #endregion

        #region Detalle de Evento de Perdida
        //[BreadCrumb(Label = "Detalle del evento de pérdida")]
        [RequiresAuthenticationAttribute]
        public ActionResult Detalle(string evento)
        {
            try
            {
                var nNroRiesgo = Encripta.base64Decode(Convert.ToString(evento));
                var model = new GestionRiesgoModel()
                {
                    oEventoPerdida = ievento.ObtenerDetalleEventoPerdida((long)Convert.ToInt32(nNroRiesgo)),
                    oLstGastosEvento = ievento.ObtenerDetGastosEventoPerdida((long)Convert.ToInt32(nNroRiesgo)),
                    oLstCtaContEvento = ievento.ObtenerDetCtaContablesEventoPerdida((long)Convert.ToInt32(nNroRiesgo))
                };
                //var oEvento = ievento.ObtenerDetalleEventoPerdida((long)Convert.ToInt64(nNroRiesgo));

                return View(model);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }
        
        [RequiresAuthenticationAttribute]
        public ActionResult ReporteEventosAgrupados(string evento)
        {

            try
            {
                //Usuario usuario = (Usuario)Session["Usuario"];
                //string usuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string usuario = Environment.UserName;

                var nNroRiesgo = Encripta.base64Decode(Convert.ToString(evento));
                var Reporte = GenerarReporteGruposEventoPerdida((long)Convert.ToInt32(nNroRiesgo));
                //return new ExcelResult(Reporte, "Grupo de Eventos [" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
                return new ExcelResult(Reporte, "Grupo de Eventos [" + usuario.ToUpper() + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
            
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }

        private XLWorkbook GenerarReporteGruposEventoPerdida(long pnCodEvento)
        {
            try
            {
                var Plantilla = Server.MapPath("~/Plantillas/FormatoEventoPerdidaAgrupado.xlsx");
                XLWorkbook wb = new XLWorkbook(Plantilla);

                IXLWorksheet ws = wb.Worksheet(1);

                var Grupo = ievento.ObtenerAgrupacionEvento(pnCodEvento);
                int FilaGrupo = 4;
                int[] matBucles = { 0, 0 }; /*(0) - Gastos, (1) - Cta Contable*/

                if (Grupo.Count > 0)
                {
                    matBucles[0] = FilaGrupo;
                    foreach (var item in Grupo)
                    {
                        var Agrupado = ievento.ObtenerDetalleEventoPerdida(item.oDatosRiesgo.nNroRiesgo);

                        ws.Cell("A" + FilaGrupo).Value = Convert.ToInt32(item.oDatosRiesgo.cCodRiesgo);
                        ws.Cell("A" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("A" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("A" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("A" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("A" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("B" + FilaGrupo).Value = Agrupado.oDatosRiesgo.cRiesgoIdentiticado;
                        ws.Cell("B" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("B" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("B" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("B" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("B" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("C" + FilaGrupo).Value = Agrupado.cMedidasCorrectivas;
                        ws.Cell("C" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("C" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("C" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("C" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("C" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("D" + FilaGrupo).Value = Agrupado.cAccionesRealizada;
                        ws.Cell("D" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("D" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("D" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("D" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("D" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("E" + FilaGrupo).Value = Agrupado.oDescCortaEventoPerdida.cConsDescripcion;
                        ws.Cell("E" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("E" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("E" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("E" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("E" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("F" + FilaGrupo).Value = Agrupado.oDatosRiesgo.oAgencias.cAgeDescripcion;
                        ws.Cell("F" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("F" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("F" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("F" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("F" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("G" + FilaGrupo).Value = Agrupado.oDatosRiesgo.oAreas.cAreaDescripcion;
                        ws.Cell("G" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("G" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("G" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("G" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("G" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("H" + FilaGrupo).Value = Agrupado.dFechaOcurrencia;
                        ws.Cell("H" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("H" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("H" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("H" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("H" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("I" + FilaGrupo).Value = Agrupado.dFechaDescubrimiento;
                        ws.Cell("I" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("I" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("I" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("I" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("I" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("J" + FilaGrupo).Value = Agrupado.dFechaRegCont;
                        ws.Cell("J" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("J" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("J" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("J" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("J" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("K" + FilaGrupo).Value = Agrupado.cAnio;
                        ws.Cell("K" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("K" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("K" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("K" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("K" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("L" + FilaGrupo).Value = Agrupado.oClasesEventoPerdida.cDescClasEvento;
                        ws.Cell("L" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("L" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("L" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("L" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("L" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("M" + FilaGrupo).Value = Agrupado.oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento;
                        ws.Cell("M" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("M" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("M" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("M" + FilaGrupo).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("M" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("M" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("N" + FilaGrupo).Value = Agrupado.bAsociaRiesgo ? "SI" : "NO";
                        ws.Cell("N" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("N" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("N" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("N" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("N" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("O" + FilaGrupo).Value = Agrupado.oDatosRiesgo.oProceso.cDescProceso; ;
                        ws.Cell("O" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("O" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("O" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("O" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("O" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("P" + FilaGrupo).Value = Agrupado.oDatosRiesgo.oProceso.oSubProceso.cDescSubProceso;//Agrupado.nMontoBruto;
                        ws.Cell("P" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("P" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("P" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("P" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("P" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("Q" + FilaGrupo).Value = Agrupado.oLineaNeg.cDescLineaNeg;//ConstGeneral.Get.PenDescripcion(Convert.ToInt32(Agrupado.cPenMontoRecup));
                        ws.Cell("Q" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("Q" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("Q" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("Q" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("Q" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("R" + FilaGrupo).Value = Agrupado.oLineaNeg.oSubLineaNeg.cDescSubLineaNeg;//Agrupado.nMontoRecup;
                        ws.Cell("R" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("R" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("R" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("R" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("R" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("S" + FilaGrupo).Value = Agrupado.oCobertura.cConsDescripcion;//Agrupado.nMontoRecup;
                        ws.Cell("S" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("S" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("S" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("S" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("S" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("T" + FilaGrupo).Value = Agrupado.nMontoBruto;//Agrupado.nMontoRecup;
                        ws.Cell("T" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("T" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("T" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("T" + FilaGrupo).Style.NumberFormat.Format = "#,##0.00";
                        ws.Cell("T" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("T" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("U" + FilaGrupo).Value = Agrupado.nPerdidaNeta;//Agrupado.nMontoRecup;
                        ws.Cell("U" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("U" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("U" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("U" + FilaGrupo).Style.NumberFormat.Format = "#,##0.00";
                        ws.Cell("U" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("U" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("V" + FilaGrupo).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(Agrupado.cPenMontoPerdida));
                        ws.Cell("V" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("V" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("V" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("V" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("V" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("W" + FilaGrupo).Value = Agrupado.nMontoPerdida;
                        ws.Cell("W" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("W" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("W" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("W" + FilaGrupo).Style.NumberFormat.Format = "#,##0.00";
                        ws.Cell("W" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("W" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("X" + FilaGrupo).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(Agrupado.cPenMontoRecup));
                        ws.Cell("X" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("X" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("X" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("X" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("X" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("Y" + FilaGrupo).Value = Agrupado.nMontoRecup;
                        ws.Cell("Y" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("Y" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("Y" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("Y" + FilaGrupo).Style.NumberFormat.Format = "#,##0.00";
                        ws.Cell("Y" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("Y" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("Z" + FilaGrupo).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(Agrupado.cPenMontoProvision));//Agrupado.nMontoRecup;
                        ws.Cell("Z" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("Z" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("Z" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("Z" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("Z" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("AA" + FilaGrupo).Value = Agrupado.nMontoProvision;
                        ws.Cell("AA" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("AA" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("AA" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("AA" + FilaGrupo).Style.NumberFormat.Format = "#,##0.00";
                        ws.Cell("AA" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("AA" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("AG" + FilaGrupo).Value = Agrupado.cUserRegistra;
                        ws.Cell("AG" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("AG" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("AG" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("AG" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("AG" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("AH" + FilaGrupo).Value = Agrupado.dFechaRegistro;
                        ws.Cell("AH" + FilaGrupo).Style.Alignment.WrapText = true;
                        ws.Cell("AH" + FilaGrupo).Style.Font.FontName = "Segoe UI";
                        ws.Cell("AH" + FilaGrupo).Style.Font.FontSize = 9;
                        ws.Cell("AH" + FilaGrupo).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("AH" + FilaGrupo).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                        //Detalle de los gastos
                        var LstGastos = ievento.ObtenerDetGastosEventoPerdida(Agrupado.oDatosRiesgo.nNroRiesgo);
                        if (LstGastos.Count > 0)
                        {
                            int filaGasto = FilaGrupo;
                            foreach (var gasto in LstGastos)
                            {
                                ws.Cell("AB" + filaGasto).Value = ConstGeneral.Get.PenDescripcion(gasto.Moneda);
                                ws.Cell("AB" + filaGasto).Style.Alignment.WrapText = true;
                                ws.Cell("AB" + filaGasto).Style.Font.FontName = "Segoe UI";
                                ws.Cell("AB" + filaGasto).Style.Font.FontSize = 9;
                                ws.Cell("AB" + filaGasto).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("AB" + filaGasto).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("AC" + filaGasto).Value = gasto.Concepto;
                                ws.Cell("AC" + filaGasto).Style.Alignment.WrapText = true;
                                ws.Cell("AC" + filaGasto).Style.Font.FontName = "Segoe UI";
                                ws.Cell("AC" + filaGasto).Style.Font.FontSize = 9;
                                ws.Cell("AC" + filaGasto).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("AC" + filaGasto).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("AD" + filaGasto).Value = gasto.Monto;
                                ws.Cell("AD" + filaGasto).Style.Alignment.WrapText = true;
                                ws.Cell("AD" + filaGasto).Style.Font.FontName = "Segoe UI";
                                ws.Cell("AD" + filaGasto).Style.Font.FontSize = 9;
                                ws.Cell("AD" + filaGasto).Style.NumberFormat.Format = "#,##0.00";
                                ws.Cell("AD" + filaGasto).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("AD" + filaGasto).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                filaGasto++;
                            }

                            matBucles[0] = filaGasto - 1;
                        }

                        //Detalle de las cuentas contables
                        var LstCtaCont = ievento.ObtenerDetCtaContablesEventoPerdida(Agrupado.oDatosRiesgo.nNroRiesgo);
                        if (LstCtaCont.Count > 0)
                        {
                            int filaCta = FilaGrupo;
                            foreach (var cuenta in LstCtaCont)
                            {
                                ws.Cell("AE" + filaCta).Value = cuenta.Codigo;
                                ws.Cell("AE" + filaCta).Style.Alignment.WrapText = true;
                                ws.Cell("AE" + filaCta).Style.Font.FontName = "Segoe UI";
                                ws.Cell("AE" + filaCta).Style.Font.FontSize = 9;
                                ws.Cell("AE" + filaCta).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("AE" + filaCta).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("AF" + filaCta).Value = cuenta.cCtaContDesc.Substring(Convert.ToInt32(cuenta.cCtaContDesc.IndexOf("]")) + 1);
                                ws.Cell("AF" + filaCta).Style.Alignment.WrapText = true;
                                ws.Cell("AF" + filaCta).Style.Font.FontName = "Segoe UI";
                                ws.Cell("AF" + filaCta).Style.Font.FontSize = 9;
                                ws.Cell("AF" + filaCta).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                ws.Cell("AF" + filaCta).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                filaCta++;
                            }
                            matBucles[1] = filaCta - 1;
                        }

                        ws.Range("A" + FilaGrupo + ":" + "A" + matBucles.Max()).Merge();
                        ws.Range("B" + FilaGrupo + ":" + "B" + matBucles.Max()).Merge();
                        ws.Range("C" + FilaGrupo + ":" + "C" + matBucles.Max()).Merge();
                        ws.Range("D" + FilaGrupo + ":" + "D" + matBucles.Max()).Merge();
                        ws.Range("E" + FilaGrupo + ":" + "E" + matBucles.Max()).Merge();
                        ws.Range("F" + FilaGrupo + ":" + "F" + matBucles.Max()).Merge();
                        ws.Range("G" + FilaGrupo + ":" + "G" + matBucles.Max()).Merge();
                        ws.Range("H" + FilaGrupo + ":" + "H" + matBucles.Max()).Merge();
                        ws.Range("I" + FilaGrupo + ":" + "I" + matBucles.Max()).Merge();
                        ws.Range("J" + FilaGrupo + ":" + "J" + matBucles.Max()).Merge();
                        ws.Range("K" + FilaGrupo + ":" + "K" + matBucles.Max()).Merge();
                        ws.Range("L" + FilaGrupo + ":" + "L" + matBucles.Max()).Merge();
                        ws.Range("M" + FilaGrupo + ":" + "M" + matBucles.Max()).Merge();
                        ws.Range("N" + FilaGrupo + ":" + "N" + matBucles.Max()).Merge();
                        ws.Range("O" + FilaGrupo + ":" + "O" + matBucles.Max()).Merge();
                        ws.Range("P" + FilaGrupo + ":" + "P" + matBucles.Max()).Merge();
                        ws.Range("Q" + FilaGrupo + ":" + "Q" + matBucles.Max()).Merge();
                        ws.Range("R" + FilaGrupo + ":" + "R" + matBucles.Max()).Merge();
                        ws.Range("S" + FilaGrupo + ":" + "S" + matBucles.Max()).Merge();
                        ws.Range("T" + FilaGrupo + ":" + "T" + matBucles.Max()).Merge();
                        ws.Range("U" + FilaGrupo + ":" + "U" + matBucles.Max()).Merge();
                        ws.Range("V" + FilaGrupo + ":" + "V" + matBucles.Max()).Merge();
                        ws.Range("W" + FilaGrupo + ":" + "W" + matBucles.Max()).Merge();
                        ws.Range("X" + FilaGrupo + ":" + "X" + matBucles.Max()).Merge();
                        ws.Range("Y" + FilaGrupo + ":" + "Y" + matBucles.Max()).Merge();
                        ws.Range("Z" + FilaGrupo + ":" + "Z" + matBucles.Max()).Merge();
                        ws.Range("AA" + FilaGrupo + ":" + "AA" + matBucles.Max()).Merge();
                        ws.Range("AG" + FilaGrupo + ":" + "AG" + matBucles.Max()).Merge();
                        ws.Range("AH" + FilaGrupo + ":" + "AH" + matBucles.Max()).Merge();

                        ws.Range("AB" + matBucles[0] + ":" + "AB" + matBucles.Max()).Merge();
                        ws.Range("AC" + matBucles[0] + ":" + "AC" + matBucles.Max()).Merge();
                        ws.Range("AD" + matBucles[0] + ":" + "AD" + matBucles.Max()).Merge();
                        ws.Range("AE" + matBucles[1] + ":" + "AE" + matBucles.Max()).Merge();
                        ws.Range("AF" + matBucles[1] + ":" + "AF" + matBucles.Max()).Merge();

                        FilaGrupo = matBucles.Max();
                        FilaGrupo++;
                    }

                    var Evento = ievento.ObtenerDetalleEventoPerdida(pnCodEvento);
                    ws.Cell("A1").Value = "Grupo " + Evento.oDatosRiesgo.cCodRiesgo;
                    ws.Cell("A1").Style.Font.FontName = "Segoe UI";
                    ws.Cell("A1").Style.Font.FontSize = 9;
                }
                ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);


                return wb;
            }
            catch { throw; }
        }


        #endregion

        #region Grupo Evento 
        //[BreadCrumb(Label = "Gestión Grupo Eventos")]
        [RequiresAuthenticationAttribute]
        public ActionResult Grupo(string evento)
        {
            try
            {
                /*Esta opcion de deshabilito debido a que se corrigio el flujo del registro de los evento de perdida agrupados*/
                if (String.IsNullOrEmpty(evento)) {
                    var error = Utils.Error.GetError.GetErrorModel("Sección no disponible", (HttpStatusCode)404, "Esta sección no esta disponible.");
                    return View("Error", error);
                }


                var nNroRiesgo = Encripta.base64Decode(Convert.ToString(evento));
                var LstGrupo = ievento.ObtenerAgrupacionEvento((long)Convert.ToInt32(nNroRiesgo));

                List<EventoPerdida> ListaGrupo = new List<EventoPerdida>();
                EventoPerdida oEvento = null;
                if (LstGrupo.Count > 0)
                {
                    foreach (var item in LstGrupo)
                    {
                        var detalle = ievento.ObtenerDetalleEventoPerdida(item.oDatosRiesgo.nNroRiesgo);
                        oEvento = new EventoPerdida();
                        oEvento.oDatosRiesgo = new DatosRiesgos();
                        oEvento.oDatosRiesgo.nNroRiesgo = detalle.oDatosRiesgo.nNroRiesgo;
                        oEvento.oDatosRiesgo.cCodRiesgo = detalle.oDatosRiesgo.cCodRiesgo;
                        oEvento.oDatosRiesgo.cRiesgoIdentiticado = detalle.oDatosRiesgo.cRiesgoIdentiticado;
                        oEvento.cGrupoEvento = detalle.cGrupoEvento;
                        oEvento.nMontoBruto = detalle.nMontoBruto;
                        oEvento.nPerdidaNeta = detalle.nPerdidaNeta;
                        oEvento.cUserRegistra = item.cUserRegistra;
                        oEvento.dFechaRegistro = item.dFechaRegistro;

                        ListaGrupo.Add(oEvento);
                    }
                }
                else
                {
                    ListaGrupo = null;
                }
                var model = new GestionRiesgoModel()
                {
                    oEventoPerdida = ievento.ObtenerDetalleEventoPerdida((long)Convert.ToInt32(nNroRiesgo)),
                    oLstEventoGrupo = ListaGrupo
                };

                return View(model);

            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }

        public JsonResult EliminarEventoGrupo(long evento)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);
                int eliminado = ievento.EliminarEventoGrupo(evento);
                return Json(new { exito = eliminado });
            }
            catch { throw; }
        }

        #endregion

        #region Gestion de Evento de Perdida
        /*TORE 20210426: Se comento la gestion del evento de perdida por observacion del Analista de Riesgo Operacional*/

        //[BreadCrumb(Label = "Gestión del Evento de Pérdida")]
        //[RequiresAuthenticationAttribute]
        //public ActionResult GestionEventoPerdida(string evento)
        //{
        //    Usuario usuario = (Usuario)Session["Usuario"];


        //    var nNroRiesgo = Encripta.base64Decode(Convert.ToString(evento));

        //    GestionRiesgoModel model = new GestionRiesgoModel();
        //    model.oEventoPerdida = ievento.ObtenerDetalleEventoPerdida((long)Convert.ToInt64(nNroRiesgo)); ;
        //    model.oLstAgencia = agencias.ObtenerAgencias();
        //    model.oLstAreas = areas.ObtenerAreas();
        //    model.oLstFactorRiesgo = constante.ObtenerConstantes(1021);
        //    model.oLstCausas = causas.ObtenerCausasRiesgo();
        //    model.oLstTipoCobertura = constante.ObtenerConstantes(6000);
        //    //model.oLstSubEventoPerdida = EventoPerdidaLN.ObtenerSubEventoPerdida(Convert.ToInt32(EventoPerdida.oDatosRiesgo.nNroRiesgo));
        //    //model.oLstEventoPerdida = ConstantesLN.ObtenerConstantes(1022);
        //    model.oLstLineaNegocio = lineaNegocio.ObtenerLineaNegocio();
        //    return View(model);
        //}

        //public JsonResult GrabarGestionEventoPerdida(long pnNroRiesgo, string psAgeCod, string psAreaCod, string psLineaNeg, string psSubLineaNeg, string psCausas,
        //                                    string psProcesos, string psSubProcesos, int pnSubEventoPerdida, int pbRiesgoCrediticio, int pbExEventoPerdida,
        //                                    string psMedidasCorrectivas = "", string psAccionRealizada = "")
        //{
        //    try
        //    {
        //        Usuario usuario = (Usuario)Session["Usuario"];
        //        string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

        //        string[] Mensaje = ievento.GrabarGestionEventoPerdida(pnNroRiesgo, psAgeCod, psAreaCod, psLineaNeg, psSubLineaNeg, psCausas, psProcesos,
        //                                                                        psSubProcesos, pnSubEventoPerdida, Convert.ToBoolean(pbRiesgoCrediticio),
        //                                                                        Convert.ToBoolean(pbExEventoPerdida), lsNroRiesgo, psMedidasCorrectivas,
        //                                                                        psAccionRealizada);
        //        string tipo_mensaje = Mensaje[(int)Utils.Constantes.Mensaje.TipoMensaje];
        //        string mensaje = Mensaje[(int)Utils.Constantes.Mensaje.Mensaje];


        //        return Json(new { Tipo = tipo_mensaje, MensajeSis = mensaje });
        //    }
        //    catch { throw; }
        //}

        #endregion

        #region Agrupar Evento
        /*TORE 20210426: Se comento la gestion del evento de perdida por observacion del analista de 
         Riesgo Operacional*/

        //[BreadCrumb(Label = "Agrupar Evento de Pérdida")]
        //[RequiresAuthenticationAttribute]
        //public ActionResult Agrupar()
        //{

        //    return View();
        //}

        //public JsonResult GrabarAgrupacionEventoPerdida(long pnNroRiesgo, string psAgrupado)
        //{
        //    try
        //    {
        //        Usuario usuario = (Usuario)Session["Usuario"];
        //        string lsNroRiesgo = maestro.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

        //        string[] respuesta = ievento.GrabarAgrupacionEventoPerdida(pnNroRiesgo, psAgrupado, lsNroRiesgo);
        //        //var oLstAgrupados = EventoPerdidaLN.ObtenerAgrupacionEvento(pnNroRiesgo);
        //        var _Tipo = respuesta[0];
        //        var _Mensaje = respuesta[1];

        //        return Json(new { Tipo = _Tipo, MensajeSis = _Mensaje });
        //    }
        //    catch { throw; }
        //}

        //public JsonResult ListarEventosAgrupar()
        //{
        //    try
        //    {
        //        //Usuario usuario = (Usuario)Session["Usuario"];
        //        //string lsNroRiesgo = MaestroLN.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

        //        var Eventos = ievento.ObtenerEventoPerdidaAgrupados();
        //        return Json(JsonConvert.SerializeObject(Eventos));
        //    }
        //    catch { throw; }
        //}

        //public JsonResult ListarAgrupacionEvento(long pnNroRiesgo)
        //{
        //    try
        //    {
        //        //Usuario usuario = (Usuario)Session["Usuario"];
        //        //string lsNroRiesgo = MaestroLN.GenerarNroRiesgo("", usuario.oAgencia.cAgeCod, usuario.oAreas.cAreaCod, usuario.cUser);

        //        var Eventos = ievento.ObtenerAgrupacionEvento(pnNroRiesgo);
        //        return Json(JsonConvert.SerializeObject(Eventos));
        //    }
        //    catch { throw; }
        //}

        #endregion


    }
}