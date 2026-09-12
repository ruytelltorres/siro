using ClosedXML.Excel;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using CMACMaynas.Web.SIRO.Negocio;
using CMACMaynas.Web.SIRO.Seguridad.Auth.Filters;
using Microsoft.Ajax.Utilities;
using Microsoft.Rest;
using Newtonsoft.Json;
using SIRO.Models;
using SIRO.Servicios.PowerBi;
using SIRO.Utils.Constantes;
using SIRO.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SIRO.Controllers
{
    public class ReporteController : Controller
    {

        #region Instancias


        private readonly IConstantesApp constante;
        private readonly IConstSistemaApp constSistema;
        private readonly ICausaRiesgoApp causaRiesgo;
        private readonly IRiesgoInherenteApp riesgoInherente;
        private readonly IRiesgoResidualApp riesgoResidual;
        private readonly IPlanAccionApp planAccion;
        private readonly IEventoPerdidaApp ievento;
        private readonly IRiesgoOperacionalApp iriesgo;
        private readonly IAutoevaluacionApp ievaluacion;
        private readonly IReportesApp ireportes;
        private readonly IGestionIncentivoApp incentivo;
        private readonly IMontoPerdidaApp montoPerdida;
        private readonly IAreasApp areas;
        private readonly IAuthPowerBiApp powerbi;

        public ReporteController(IConstantesApp constante, IConstSistemaApp constSistema, ICausaRiesgoApp causaRiesgo, IRiesgoInherenteApp riesgoInherente, IRiesgoResidualApp riesgoResidual,
                                    IPlanAccionApp planAccion, IEventoPerdidaApp ievento, IRiesgoOperacionalApp iriesgo, IAutoevaluacionApp ievaluacion, IReportesApp ireportes, IGestionIncentivoApp incentivo,
                                    IMontoPerdidaApp montoPerdida, IAreasApp areas, IAuthPowerBiApp powerbi)
        {
            this.constante = constante;
            this.constSistema = constSistema;
            this.causaRiesgo = causaRiesgo;
            this.riesgoInherente = riesgoInherente;
            this.riesgoResidual = riesgoResidual;
            this.planAccion = planAccion;
            this.ievento = ievento;
            this.iriesgo = iriesgo;
            this.ievaluacion = ievaluacion;
            this.ireportes = ireportes;
            this.incentivo = incentivo;
            this.montoPerdida = montoPerdida;
            this.areas = areas;
            this.powerbi = powerbi;


            //m_errorMessage = ConfigValidatorService.GetWebConfigErrors();
        }

        #endregion

        #region Leer Excel
        //using (XLWorkbook workbook = new XLWorkbook(Plantilla))
        //{
        //    IXLWorksheet worksheet = workbook.Worksheet(1);
        //   
        //    //bool FirstRow = true;
        //    ////Range for reading the cells based on the last cell used.  
        //    //string readRange = "1:1";
        //    //foreach (IXLRow row in worksheet.RowsUsed())
        //    //{
        //    //    //Si lee la primera fila (usado), agréguelos como nombre de columna
        //    //    if (FirstRow)
        //    //    {
        //    //        //Verificación de la última celda utilizada para la generación de columnas en la tabla de datos
        //    //        readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
        //    //        foreach (IXLCell cell in row.Cells(readRange))
        //    //        {
        //    //            //dt.Columns.Add(cell.Value.ToString());
        //    //        }
        //    //        FirstRow = false;
        //    //    }
        //    //    else
        //    //    {
        //    //        //Agregar una fila en la tabla de datos
        //    //        //dt.Rows.Add();
        //    //        int cellIndex = 0;
        //    //        //Updating the values of datatable  
        //    //        foreach (IXLCell cell in row.Cells(readRange))
        //    //        {
        //    //          //  dt.Rows[dt.Rows.Count - 1][cellIndex] = cell.Value.ToString();
        //    //            cellIndex++;
        //    //        }
        //    //    }
        //    //}
        //    ////If no data in Excel file  
        //    //if (FirstRow)
        //    //{
        //    //    ViewBag.Message = "Empty Excel File!";
        //    //}
        #endregion

        #region Matriz Riesgo
        //[BreadCrumb(Clear = true, Label = "Matriz de Riesgo")]
        [RequiresAuthenticationAttribute]
        public ActionResult MatrizRiesgo()
        {
            try
            {
                var model = new GestionRiesgoModel()
                {
                    oLstAreas = areas.ObtenerAreas()
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
        public JsonResult ObtenerDatosMatrizRiesgo(int pnTpoRiesgo, bool pbFiltro = false, string psAreaCod = "", string psDesde = "", string psHasta = "")
        {
            ReportesModel model = new ReportesModel();
            try
            {
                var oLstMatriz = ireportes.MatrizRiesgo(pnTpoRiesgo, NormalizarFiltroFechasMatriz(pbFiltro, ref psDesde, ref psHasta), psAreaCod, psDesde, psHasta);
                if (oLstMatriz != null)
                {
                    model.oLstMatrizRiesgos = oLstMatriz;
                }
                else { model.oLstMatrizRiesgos = null; }


                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }

        [RequiresAuthenticationAttribute]
        public ActionResult ExportarInformeMatrizRiesgo(int pnTpoRiesgo, bool pbFiltro, string psAreaCod, string psDesde, string psHasta)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];


                string TituloExcel = "";

                if (pnTpoRiesgo == (int)Riesgos.RiesgoOperacional)
                {
                    TituloExcel = "Matriz de Riesgo Operacional";
                }
                else if (pnTpoRiesgo == (int)Riesgos.Autoevaluaciones)
                {
                    TituloExcel = "Matriz de Evaluaciones";
                }

                var ReporteBaseRiesgoOperacionales = ireportes.MatrizRiesgo(pnTpoRiesgo, NormalizarFiltroFechasMatriz(pbFiltro, ref psDesde, ref psHasta), psAreaCod, psDesde, psHasta);
                var MatrizRiesgo = ExcelInformeMatrizRiesgo(ReporteBaseRiesgoOperacionales, pnTpoRiesgo);

                return new ExcelResult(MatrizRiesgo, TituloExcel + "[" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }


        }

        private static bool NormalizarFiltroFechasMatriz(bool aplicar, ref string desde, ref string hasta)
        {
            if (!aplicar || string.IsNullOrWhiteSpace(desde) || string.IsNullOrWhiteSpace(hasta))
            {
                desde = "";
                hasta = "";
                return false;
            }

            DateTime fechaDesde;
            DateTime fechaHasta;
            if (!DateTime.TryParseExact(desde.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out fechaDesde) ||
                !DateTime.TryParseExact(hasta.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out fechaHasta) || fechaDesde > fechaHasta)
            {
                throw new ArgumentException("El rango de fechas de la matriz de riesgos no es v?lido.");
            }

            desde = fechaDesde.ToString("yyyyMMdd");
            hasta = fechaHasta.ToString("yyyyMMdd");
            return true;
        }

        private XLWorkbook ExcelInformeMatrizRiesgo(List<dynamic> datos, int tipo_riesgo)
        {
            var Plantilla = Server.MapPath("~/Plantillas/FormatoMatriz.xlsx");
            XLWorkbook wb = new XLWorkbook(Plantilla);

            try
            {
                #region Generado
                IXLWorksheet ws = wb.Worksheet(1); //Hoja 

                int[] matBucles = { 0, 0, 0, 0 }; /*(0) - Bucle Causas, (1) - Bucle Controles, (2) - Bucle Planes Accion, (3) - Responsables Planes Accion*/

                int PosicionFila = 5;

                /******************************************** Configuracion segun tipo de riesgo *******************************************/
                if (tipo_riesgo == (int)Riesgos.RiesgoOperacional)
                {
                    ws.Column("D").Hide();
                }
                else if (tipo_riesgo == (int)Riesgos.Autoevaluaciones)
                {
                    ws.Cell("D3").Value = "Tipo de Evaluación";
                }
                /******************************************** End Configuracion ************************************************************/


                /******************************************** Cuerpo *******************************************/
                foreach (var riesgooperacional in datos)
                {
                    matBucles[0] = PosicionFila;

                    #region Datos Principales
                    ws.Cell("A" + PosicionFila).Value = riesgooperacional.cCodRiesgo;
                    ws.Cell("A" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("A" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("A" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("A" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("A" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ////wb.Worksheets.Worksheet(1).Cell("A" + j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ////ws.Cell("A" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                    //ws.Cell("A" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    //wb.Worksheets.Worksheet(1).Cell("A" + j).Style.NumberFormat.Format = "00000";

                    ws.Cell("B" + PosicionFila).Value = riesgooperacional.cRiesgoIdentiticado;
                    ws.Cell("B" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("B" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("B" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("B" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    ws.Cell("B" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("D" + PosicionFila).Value = riesgooperacional.cEvalDesc;
                    ws.Cell("D" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("D" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("D" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("D" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("B" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("E" + PosicionFila).Value = riesgooperacional.cFactorRiesgo;
                    ws.Cell("E" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("E" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("E" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("E" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("E" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("F" + PosicionFila).Value = riesgooperacional.cEventoPerdida;
                    ws.Cell("F" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("F" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("F" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("F" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("F" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                    ws.Cell("G" + PosicionFila).Value = riesgooperacional.cDescLineaNeg;
                    ws.Cell("G" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("G" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("G" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("G" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("G" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("H" + PosicionFila).Value = riesgooperacional.cDescProducto;
                    ws.Cell("H" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("H" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("H" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("H" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("H" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("I" + PosicionFila).Value = riesgooperacional.cDescSubProducto;
                    ws.Cell("I" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("I" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("I" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("I" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("I" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("J" + PosicionFila).Value = riesgooperacional.cProbabilidadInherente;
                    ws.Cell("J" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("J" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("J" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("J" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("J" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("K" + PosicionFila).Value = riesgooperacional.cImpactoInhrente;
                    ws.Cell("K" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("K" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("K" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("K" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("K" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("L" + PosicionFila).Value = riesgooperacional.cNivelRiesgoInherente;
                    ws.Cell("L" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("L" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("L" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("L" + PosicionFila).Style.Fill.BackgroundColor = XLColor.FromHtml(ConstGeneral.Get.ColorNivelRiesgo(riesgoInherente.ObtenerNivelRiesgoInherente(riesgooperacional.nProbalidadInherente, riesgooperacional.nImpactoInherente).nValorEscala));
                    ws.Cell("L" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("L" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("M" + PosicionFila).Value = riesgooperacional.nMontoPerdida;
                    ws.Cell("M" + PosicionFila).Style.Alignment.WrapText = true;
                    ws.Cell("M" + PosicionFila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("M" + PosicionFila).Style.Font.FontSize = 9;
                    ws.Cell("M" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    ws.Cell("M" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    #endregion

                    #region Insertar Causas del riesgo
                    var oCausasRiesgo = causaRiesgo.ObtenerCausaPorRiesgo(riesgooperacional.nNroRiesgo);
                    if (oCausasRiesgo.Count > 0)
                    {
                        int pfCausa = PosicionFila;
                        foreach (var causas in oCausasRiesgo)
                        {
                            ws.Cell("C" + pfCausa).Value = causas.cCausaDesc;
                            ws.Cell("C" + pfCausa).Style.Alignment.WrapText = true;
                            ws.Cell("C" + pfCausa).Style.Font.FontName = "Segoe UI";
                            ws.Cell("C" + pfCausa).Style.Font.FontSize = 9;
                            ws.Cell("C" + pfCausa).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            ws.Cell("C" + pfCausa).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            pfCausa++;
                        }
                        matBucles[0] = pfCausa - 1;
                    }
                    #endregion

                    #region Controles para el riesgo residual
                    var oControlRiesgo = riesgoResidual.ObtenerControlRiesgoResidual(riesgooperacional.nNroRiesgo);
                    int lnTotalControles = 0, lnSumEfectividadControl = 0;
                    if (oControlRiesgo.Count > 0)
                    {
                        int pfControles = PosicionFila;
                        foreach (var controlriesgoresidual in oControlRiesgo)
                        {
                            ws.Cell("N" + pfControles).Value = controlriesgoresidual.cComentario;
                            ws.Cell("N" + pfControles).Style.Alignment.WrapText = true;
                            ws.Cell("N" + pfControles).Style.Font.FontName = "Segoe UI";
                            ws.Cell("N" + pfControles).Style.Font.FontSize = 9;
                            ws.Cell("N" + pfControles).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            ws.Cell("N" + pfControles).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("P" + pfControles).Value = controlriesgoresidual.cResponsableDef;
                            ws.Cell("P" + pfControles).Style.Alignment.WrapText = true;
                            ws.Cell("P" + pfControles).Style.Font.FontName = "Segoe UI";
                            ws.Cell("P" + pfControles).Style.Font.FontSize = 9;
                            ws.Cell("P" + pfControles).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("P" + pfControles).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("Q" + pfControles).Value = controlriesgoresidual.cEjecucionControl;
                            ws.Cell("Q" + pfControles).Style.Alignment.WrapText = true;
                            ws.Cell("Q" + pfControles).Style.Font.FontName = "Segoe UI";
                            ws.Cell("Q" + pfControles).Style.Font.FontSize = 9;
                            ws.Cell("Q" + pfControles).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("Q" + pfControles).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("R" + pfControles).Value = controlriesgoresidual.cPeriodoEjecucion;
                            ws.Cell("R" + pfControles).Style.Alignment.WrapText = true;
                            ws.Cell("R" + pfControles).Style.Font.FontName = "Segoe UI";
                            ws.Cell("R" + pfControles).Style.Font.FontSize = 9;
                            ws.Cell("R" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("R" + pfControles).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("S" + pfControles).Value = controlriesgoresidual.cCumpleObjetivo;
                            ws.Cell("S" + pfControles).Style.Alignment.WrapText = true;
                            ws.Cell("S" + pfControles).Style.Font.FontName = "Segoe UI";
                            ws.Cell("S" + pfControles).Style.Font.FontSize = 9;
                            ws.Cell("S" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("S" + pfControles).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("T" + pfControles).Value = controlriesgoresidual.cEvidenciaControl;
                            ws.Cell("T" + pfControles).Style.Alignment.WrapText = true;
                            ws.Cell("T" + pfControles).Style.Font.FontName = "Segoe UI";
                            ws.Cell("T" + pfControles).Style.Font.FontSize = 9;
                            ws.Cell("T" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("T" + pfControles).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            lnSumEfectividadControl += controlriesgoresidual.nEfectividadControl;
                            lnTotalControles += 1;

                            pfControles++;
                        }

                        var lnCalifEfecControl = CalculoRedondeo(lnSumEfectividadControl / lnTotalControles);
                        var oEscalaNivRiesgo = riesgoResidual.ObtenerNivelRiesgoResidualEscala(lnCalifEfecControl);

                        var oConsEfecControl = constante.ObtenerDescConstante(1505, lnCalifEfecControl);
                        var oRiesgoResidual = constante.ObtenerDescConstante(1500, lnCalifEfecControl);
                        var oApetitoRiesgo = constante.ObtenerDescConstante(1510, oEscalaNivRiesgo.nValorEstala);

                        var lsComentarios = iriesgo.ObtenerInfoGestionRiesgo(riesgooperacional.nNroRiesgo, 3);

                        ws.Cell("U" + PosicionFila).Value = lsComentarios[2];
                        ws.Cell("U" + PosicionFila).Style.Alignment.WrapText = true;
                        ws.Cell("U" + PosicionFila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("U" + PosicionFila).Style.Font.FontSize = 9;
                        ws.Cell("U" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("U" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("V" + PosicionFila).Value = oConsEfecControl.cConsDescripcion;
                        ws.Cell("V" + PosicionFila).Style.Alignment.WrapText = true;
                        ws.Cell("V" + PosicionFila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("V" + PosicionFila).Style.Font.FontSize = 9;
                        ws.Cell("V" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("V" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("W" + PosicionFila).SetValue(oRiesgoResidual.cConsDescripcion).Comment.AddText("Probabilidad: " + oEscalaNivRiesgo.cProbabilidad).AddNewLine().AddText("Impacto: " + oEscalaNivRiesgo.cImpacto);
                        ws.Cell("W" + PosicionFila).Style.Alignment.WrapText = true;
                        ws.Cell("W" + PosicionFila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("W" + PosicionFila).Style.Font.FontSize = 9;
                        ws.Cell("W" + PosicionFila).Style.Fill.BackgroundColor = XLColor.FromHtml(ConstGeneral.Get.ColorNivelRiesgo(oRiesgoResidual.nConsValor));
                        ws.Cell("W" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("W" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("X" + PosicionFila).Value = oApetitoRiesgo.cConsDescripcion;
                        ws.Cell("X" + PosicionFila).Style.Alignment.WrapText = true;
                        ws.Cell("X" + PosicionFila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("X" + PosicionFila).Style.Font.FontSize = 9;
                        ws.Cell("X" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("X" + PosicionFila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        matBucles[1] = pfControles - 1;
                    }
                    #endregion

                    #region Insertar Planes de Accion
                    var oPlanAccion = planAccion.MostrarPlanesAccionRiesgo(riesgooperacional.nNroRiesgo);
                    if (oPlanAccion.Count > 0)
                    {
                        int pfPlanesAccion = PosicionFila;
                        foreach (var planesaccion in oPlanAccion)
                        {
                            ws.Cell("Y" + pfPlanesAccion).Value = planesaccion.cPlanDescripcion;
                            ws.Cell("Y" + pfPlanesAccion).Style.Alignment.WrapText = true;
                            ws.Cell("Y" + pfPlanesAccion).Style.Font.FontName = "Segoe UI";
                            ws.Cell("Y" + pfPlanesAccion).Style.Font.FontSize = 9;
                            ws.Cell("Y" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            ws.Cell("Y" + pfPlanesAccion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("AB" + pfPlanesAccion).Value = planesaccion.dFechaImplement;
                            ws.Cell("AB" + pfPlanesAccion).Style.Alignment.WrapText = true;
                            ws.Cell("AB" + pfPlanesAccion).Style.Font.FontName = "Segoe UI";
                            ws.Cell("AB" + pfPlanesAccion).Style.Font.FontSize = 9;
                            ws.Cell("AB" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("AB" + pfPlanesAccion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            //ws.Cell("AD" + posP).Style.DateFormat.Format = "dd-MM-yyyy";
                            //ws.Cell("AD" + posP).DataType = XLDataType.Text;

                            ws.Cell("AC" + pfPlanesAccion).Value = planesaccion.cComentario;
                            ws.Cell("AC" + pfPlanesAccion).Style.Alignment.WrapText = true;
                            ws.Cell("AC" + pfPlanesAccion).Style.Font.FontName = "Segoe UI";
                            ws.Cell("AC" + pfPlanesAccion).Style.Font.FontSize = 9;
                            ws.Cell("AC" + pfPlanesAccion).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            ws.Cell("AC" + pfPlanesAccion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("AD" + pfPlanesAccion).Value = planesaccion.cEstado;
                            ws.Cell("AD" + pfPlanesAccion).Style.Alignment.WrapText = true;
                            ws.Cell("AD" + pfPlanesAccion).Style.Font.FontName = "Segoe UI";
                            ws.Cell("AD" + pfPlanesAccion).Style.Font.FontSize = 9;
                            ws.Cell("AD" + pfPlanesAccion).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("AD" + pfPlanesAccion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            /****************** Responsable de Planes de Accion ****************/
                            var oResponsablePlanAccion = planAccion.MostrarDetalleResponsablesPlanesAccionRiesgo(riesgooperacional.nNroRiesgo, (planesaccion.nPlanCod));
                            int ncantres = oResponsablePlanAccion.Count;
                            if (oResponsablePlanAccion.Count > 0)
                            {
                                int pfResponsablePlanAccion = pfPlanesAccion;
                                //for (int ii = 0; ii < oResponsablePlanAccion.Count; ii++)
                                foreach (var responsable in oResponsablePlanAccion)
                                {
                                    ws.Cell("Z" + pfResponsablePlanAccion).Value = responsable.oDatosRiesgo.oAgencias.cAgeDescripcion;
                                    ws.Cell("Z" + pfResponsablePlanAccion).Style.Alignment.WrapText = true;
                                    ws.Cell("Z" + pfResponsablePlanAccion).Style.Font.FontName = "Segoe UI";
                                    ws.Cell("Z" + pfResponsablePlanAccion).Style.Font.FontSize = 9;
                                    ws.Cell("Z" + PosicionFila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    ws.Cell("Z" + pfResponsablePlanAccion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                    ws.Cell("AA" + pfResponsablePlanAccion).Value = "[" + responsable.cUserResponsable + "] - " + responsable.oPersona.cPersNombre.ToUpper();
                                    ws.Cell("AA" + pfResponsablePlanAccion).Style.Alignment.WrapText = true;
                                    ws.Cell("AA" + pfResponsablePlanAccion).Style.Font.FontName = "Segoe UI";
                                    ws.Cell("AA" + pfResponsablePlanAccion).Style.Font.FontSize = 9;
                                    ws.Cell("Z" + pfResponsablePlanAccion).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                    ws.Cell("AA" + pfResponsablePlanAccion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                    pfResponsablePlanAccion++;
                                }
                                matBucles[3] = pfResponsablePlanAccion - 1;
                            }
                            pfPlanesAccion++;
                        }
                        matBucles[2] = pfPlanesAccion - 1;
                    }
                    #endregion

                    ws.Range("C" + matBucles[0] + ":" + "C" + matBucles.Max()).Merge();
                    ws.Range("N" + matBucles[1] + ":" + "N" + matBucles.Max()).Merge();
                    ws.Range("P" + matBucles[1] + ":" + "P" + matBucles.Max()).Merge();
                    ws.Range("Q" + matBucles[1] + ":" + "Q" + matBucles.Max()).Merge();
                    ws.Range("R" + matBucles[1] + ":" + "R" + matBucles.Max()).Merge();
                    ws.Range("S" + matBucles[1] + ":" + "S" + matBucles.Max()).Merge();
                    ws.Range("T" + matBucles[1] + ":" + "T" + matBucles.Max()).Merge();

                    ws.Range("U" + PosicionFila + ":" + "U" + matBucles.Max()).Merge();
                    ws.Range("V" + PosicionFila + ":" + "V" + matBucles.Max()).Merge();
                    ws.Range("W" + PosicionFila + ":" + "W" + matBucles.Max()).Merge();
                    ws.Range("X" + PosicionFila + ":" + "X" + matBucles.Max()).Merge();

                    ws.Range("Y" + matBucles[2] + ":" + "Y" + matBucles.Max()).Merge();
                    ws.Range("Z" + matBucles[2] + ":" + "Z" + matBucles.Max()).Merge();
                    ws.Range("AA" + matBucles[2] + ":" + "AA" + matBucles.Max()).Merge();
                    ws.Range("AB" + PosicionFila + ":" + "AB" + matBucles.Max()).Merge();
                    ws.Range("AC" + PosicionFila + ":" + "AC" + matBucles.Max()).Merge();
                    ws.Range("AD" + PosicionFila + ":" + "AD" + matBucles.Max()).Merge();

                    ws.Range("A" + PosicionFila + ":" + "A" + matBucles.Max()).Merge();
                    ws.Range("B" + PosicionFila + ":" + "B" + matBucles.Max()).Merge();
                    ws.Range("D" + PosicionFila + ":" + "D" + matBucles.Max()).Merge();
                    ws.Range("E" + PosicionFila + ":" + "E" + matBucles.Max()).Merge();
                    ws.Range("F" + PosicionFila + ":" + "F" + matBucles.Max()).Merge();
                    ws.Range("G" + PosicionFila + ":" + "G" + matBucles.Max()).Merge();
                    ws.Range("H" + PosicionFila + ":" + "H" + matBucles.Max()).Merge();
                    ws.Range("I" + PosicionFila + ":" + "I" + matBucles.Max()).Merge();
                    ws.Range("J" + PosicionFila + ":" + "J" + matBucles.Max()).Merge();
                    ws.Range("K" + PosicionFila + ":" + "K" + matBucles.Max()).Merge();
                    ws.Range("L" + PosicionFila + ":" + "L" + matBucles.Max()).Merge();
                    ws.Range("M" + PosicionFila + ":" + "M" + matBucles.Max()).Merge();

                    PosicionFila = matBucles.Max();

                    PosicionFila++;
                }
                /***************************************** Fin Cuerpo ******************************************/

                ws.Range("A5:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Range("A5:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);

                return wb;
                #endregion

            }
            catch { throw; }

        }

        private int CalculoRedondeo(double pnValorRiesgoResidual)
        {
            int lnValorEntero = (int)pnValorRiesgoResidual;
            double lnValorRango = 0.0;
            int lnValorResultado = 0;

            lnValorRango = pnValorRiesgoResidual - lnValorEntero;

            if (lnValorRango >= 0.0 && lnValorRango <= 0.4)
            {
                lnValorResultado = lnValorEntero;
            }
            else if (lnValorRango >= 0.5 && lnValorRango <= 0.9)
            {
                lnValorResultado = lnValorEntero + 1;
            }

            lnValorResultado = lnValorResultado == 0 ? 1 : lnValorResultado;

            return lnValorResultado;
        }
        #endregion

        #region Matriz de Evento de Perdida
        //[BreadCrumb(Clear = true, Label = "Matriz de Evento de Pérdida")]
        [RequiresAuthenticationAttribute]
        public ActionResult MatrizEventoPerdida()
        {
            try
            {

                return View();
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        public JsonResult ListaMatrizEventoPerdida(string psDesde, string psHasta)
        {
            try
            {
                string lsFechaDesde = Convert.ToDateTime(psDesde).ToString("yyyyMMdd");
                string lsFechaHasta = Convert.ToDateTime(psHasta).ToString("yyyyMMdd");

                var oLstMatriz = ievento.ObtenerMatrizEventoPerdida(lsFechaDesde, lsFechaHasta);
                return Json(JsonConvert.SerializeObject(oLstMatriz));
            }
            catch { throw; }

        }

        [RequiresAuthenticationAttribute]
        public ActionResult ExportarMatrizEventoPerdida(string psDesde, string psHasta)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                var MatrizGenerado = GenerarMatrizEventoPerdida(Convert.ToDateTime(psDesde).ToString("yyyyMMdd"),
                                                                Convert.ToDateTime(psHasta).ToString("yyyyMMdd"));
                return new ExcelResult(MatrizGenerado, "Matriz de Evento de Pérdida [" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        private XLWorkbook GenerarMatrizEventoPerdida(string pdDesde, string pdHasta)
        {
            try
            {
                XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoMatrizEventoPerdida.xlsx"), XLEventTracking.Disabled);

                var oLstMatriz = ievento.ObtenerMatrizEventoPerdida(pdDesde, pdHasta);
                var lsUrl = constSistema.ObtenerConstanteSistema(21).cConsSisValor;

                IXLWorksheet ws = wb.Worksheet(1); //Hoja 1

                int fila = 4; //Fila de inicio
                int[] matBucles = { 0, 0 }; /*(0) - Bucle Gastos, (1) - Bucle Cuentas*/

                foreach (var evento in oLstMatriz)
                {
                    matBucles[0] = fila;
                    matBucles[1] = fila;

                    var detEvento = ievento.ObtenerDetalleEventoPerdida(evento.oDatosRiesgo.nNroRiesgo);


                    ws.Cell("A" + fila).Value = detEvento.oDatosRiesgo.cCodRiesgo;
                    ws.Cell("A" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("A" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("A" + fila).Style.Font.FontSize = 9;
                    ws.Cell("A" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("A" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    if (detEvento.bGrupo)
                    {
                        lsUrl = lsUrl + "EventoPerdida/ReporteEventosAgrupados?evento=";
                        ws.Cell("A" + fila).Hyperlink = new XLHyperlink(lsUrl + Convert.ToString(Encripta.base64Encode(Convert.ToString(detEvento.oDatosRiesgo.nNroRiesgo))), "Grupo " + detEvento.oDatosRiesgo.cCodRiesgo);
                    }

                    ws.Cell("B" + fila).Value = detEvento.oDatosRiesgo.cRiesgoIdentiticado;
                    ws.Cell("B" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("B" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("B" + fila).Style.Font.FontSize = 9;
                    ws.Cell("B" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                    ws.Cell("B" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("C" + fila).Value = detEvento.cMedidasCorrectivas; ;
                    ws.Cell("C" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("C" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("C" + fila).Style.Font.FontSize = 9;
                    ws.Cell("C" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                    ws.Cell("C" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("D" + fila).Value = detEvento.cAccionesRealizada;
                    ws.Cell("D" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("D" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("D" + fila).Style.Font.FontSize = 9;
                    ws.Cell("D" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                    ws.Cell("D" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("E" + fila).Value = detEvento.oDescCortaEventoPerdida.cConsDescripcion;
                    ws.Cell("E" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("E" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("E" + fila).Style.Font.FontSize = 9;
                    ws.Cell("E" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("E" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("F" + fila).Value = detEvento.oDatosRiesgo.oAgencias.cAgeDescripcion;
                    ws.Cell("F" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("F" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("F" + fila).Style.Font.FontSize = 9;
                    ws.Cell("F" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("F" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("G" + fila).Value = detEvento.oDatosRiesgo.oAreas.cAreaDescripcion;
                    ws.Cell("G" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("G" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("G" + fila).Style.Font.FontSize = 9;
                    ws.Cell("G" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("G" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("H" + fila).Value = detEvento.dFechaOcurrencia;
                    ws.Cell("H" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("H" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("H" + fila).Style.Font.FontSize = 9;
                    ws.Cell("H" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("H" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("I" + fila).Value = detEvento.dFechaDescubrimiento;
                    ws.Cell("I" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("I" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("I" + fila).Style.Font.FontSize = 9;
                    ws.Cell("I" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("I" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("J" + fila).Value = detEvento.dFechaRegCont;
                    ws.Cell("J" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("J" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("J" + fila).Style.Font.FontSize = 9;
                    ws.Cell("J" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("J" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("K" + fila).Value = detEvento.cAnio;
                    ws.Cell("K" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("K" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("K" + fila).Style.Font.FontSize = 9;
                    ws.Cell("K" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("K" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("K" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("L" + fila).Value = detEvento.oClasesEventoPerdida.cDescClasEvento;
                    ws.Cell("L" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("L" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("L" + fila).Style.Font.FontSize = 9;
                    ws.Cell("L" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("L" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("M" + fila).Value = detEvento.oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento;
                    ws.Cell("M" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("M" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("M" + fila).Style.Font.FontSize = 9;
                    ws.Cell("M" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("M" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("N" + fila).Value = detEvento.bAsociaRiesgo ? "SI" : "NO";
                    ws.Cell("N" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("N" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("N" + fila).Style.Font.FontSize = 9;
                    ws.Cell("N" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("N" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("O" + fila).Value = detEvento.oDatosRiesgo.oProceso.cDescProceso;
                    ws.Cell("O" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("O" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("O" + fila).Style.Font.FontSize = 9;
                    ws.Cell("O" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("O" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("P" + fila).Value = detEvento.oDatosRiesgo.oProceso.oSubProceso.cDescSubProceso;
                    ws.Cell("P" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("P" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("P" + fila).Style.Font.FontSize = 9;
                    ws.Cell("P" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("P" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("Q" + fila).Value = detEvento.oLineaNeg.cDescLineaNeg;
                    ws.Cell("Q" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("Q" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("Q" + fila).Style.Font.FontSize = 9;
                    ws.Cell("Q" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("Q" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("R" + fila).Value = detEvento.oLineaNeg.oSubLineaNeg.cDescSubLineaNeg;
                    ws.Cell("R" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("R" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("R" + fila).Style.Font.FontSize = 9;
                    ws.Cell("R" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("R" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("S" + fila).Value = detEvento.oCobertura.cConsDescripcion;
                    ws.Cell("S" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("S" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("S" + fila).Style.Font.FontSize = 9;
                    ws.Cell("S" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("S" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("T" + fila).Value = detEvento.nMontoBruto;
                    ws.Cell("T" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("T" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("T" + fila).Style.Font.FontSize = 9;
                    ws.Cell("T" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("T" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("U" + fila).Value = detEvento.nPerdidaNeta;
                    ws.Cell("U" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("U" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("U" + fila).Style.Font.FontSize = 9;
                    ws.Cell("U" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("U" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("V" + fila).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(detEvento.cPenMontoPerdida));
                    ws.Cell("V" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("V" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("V" + fila).Style.Font.FontSize = 9;
                    ws.Cell("V" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("V" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("W" + fila).Value = detEvento.nMontoPerdida;
                    ws.Cell("W" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("W" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("W" + fila).Style.Font.FontSize = 9;
                    ws.Cell("W" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("W" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("X" + fila).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(detEvento.cPenMontoRecup));
                    ws.Cell("X" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("X" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("X" + fila).Style.Font.FontSize = 9;
                    ws.Cell("X" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("X" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("Y" + fila).Value = detEvento.nMontoRecup;
                    ws.Cell("Y" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("Y" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("Y" + fila).Style.Font.FontSize = 9;
                    ws.Cell("Y" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("Y" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("Z" + fila).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(detEvento.cPenMontoProvision));
                    ws.Cell("Z" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("Z" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("Z" + fila).Style.Font.FontSize = 9;
                    ws.Cell("Z" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("Z" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("AA" + fila).Value = detEvento.nMontoProvision;
                    ws.Cell("AA" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("AA" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("AA" + fila).Style.Font.FontSize = 9;
                    ws.Cell("AA" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("AA" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("AG" + fila).Value = detEvento.cUserRegistra;
                    ws.Cell("AG" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("AG" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("AG" + fila).Style.Font.FontSize = 9;
                    ws.Cell("AG" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("AG" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("AH" + fila).Value = detEvento.dFechaRegistro;
                    ws.Cell("AH" + fila).Style.Alignment.WrapText = true;
                    ws.Cell("AH" + fila).Style.Font.FontName = "Segoe UI";
                    ws.Cell("AH" + fila).Style.Font.FontSize = 9;
                    ws.Cell("AH" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("AH" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    //Detalle de los gastos
                    var LstGastos = ievento.ObtenerDetGastosEventoPerdida(evento.oDatosRiesgo.nNroRiesgo);
                    if (LstGastos.Count > 0)
                    {
                        int filaGasto = fila;

                        foreach (var gasto in LstGastos)
                        {
                            ws.Cell("AB" + filaGasto).Value = ConstGeneral.Get.PenSimbolo((int)gasto.Moneda);
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
                            ws.Cell("AD" + filaGasto).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("AD" + filaGasto).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            filaGasto++;
                        }
                        matBucles[0] = filaGasto - 1;
                    }

                    //Detalle de las cuentas contables
                    var LstCtaCont = ievento.ObtenerDetCtaContablesEventoPerdida(evento.oDatosRiesgo.nNroRiesgo);
                    if (LstCtaCont.Count > 0)
                    {
                        int filaCta = fila;
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

                    ws.Range("A" + fila + ":" + "A" + matBucles.Max()).Merge();
                    ws.Range("B" + fila + ":" + "B" + matBucles.Max()).Merge();
                    ws.Range("C" + fila + ":" + "C" + matBucles.Max()).Merge();
                    ws.Range("D" + fila + ":" + "D" + matBucles.Max()).Merge();
                    ws.Range("E" + fila + ":" + "E" + matBucles.Max()).Merge();
                    ws.Range("F" + fila + ":" + "F" + matBucles.Max()).Merge();
                    ws.Range("G" + fila + ":" + "G" + matBucles.Max()).Merge();
                    ws.Range("H" + fila + ":" + "H" + matBucles.Max()).Merge();
                    ws.Range("I" + fila + ":" + "I" + matBucles.Max()).Merge();
                    ws.Range("J" + fila + ":" + "J" + matBucles.Max()).Merge();
                    ws.Range("K" + fila + ":" + "K" + matBucles.Max()).Merge();
                    ws.Range("L" + fila + ":" + "L" + matBucles.Max()).Merge();
                    ws.Range("M" + fila + ":" + "M" + matBucles.Max()).Merge();
                    ws.Range("N" + fila + ":" + "N" + matBucles.Max()).Merge();
                    ws.Range("O" + fila + ":" + "O" + matBucles.Max()).Merge();
                    ws.Range("P" + fila + ":" + "P" + matBucles.Max()).Merge();
                    ws.Range("Q" + fila + ":" + "Q" + matBucles.Max()).Merge();
                    ws.Range("R" + fila + ":" + "R" + matBucles.Max()).Merge();
                    ws.Range("S" + fila + ":" + "S" + matBucles.Max()).Merge();
                    ws.Range("T" + fila + ":" + "T" + matBucles.Max()).Merge();
                    ws.Range("U" + fila + ":" + "U" + matBucles.Max()).Merge();
                    ws.Range("V" + fila + ":" + "V" + matBucles.Max()).Merge();
                    ws.Range("W" + fila + ":" + "W" + matBucles.Max()).Merge();
                    ws.Range("X" + fila + ":" + "X" + matBucles.Max()).Merge();
                    ws.Range("Y" + fila + ":" + "Y" + matBucles.Max()).Merge();
                    ws.Range("Z" + fila + ":" + "Z" + matBucles.Max()).Merge();
                    ws.Range("AA" + fila + ":" + "AA" + matBucles.Max()).Merge();
                    ws.Range("AG" + fila + ":" + "AG" + matBucles.Max()).Merge();
                    ws.Range("AH" + fila + ":" + "AH" + matBucles.Max()).Merge();

                    ws.Range("AB" + matBucles[0] + ":" + "AB" + matBucles.Max()).Merge();
                    ws.Range("AC" + matBucles[0] + ":" + "AC" + matBucles.Max()).Merge();
                    ws.Range("AD" + matBucles[0] + ":" + "AD" + matBucles.Max()).Merge();

                    ws.Range("AE" + matBucles[1] + ":" + "AE" + matBucles.Max()).Merge();
                    ws.Range("AF" + matBucles[1] + ":" + "AF" + matBucles.Max()).Merge();

                    fila = matBucles.Max();

                    fila++;
                }
                /***************************************** Fin Cuerpo ******************************************/
                ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);

                return wb;
            }
            catch { throw; }
        }


        #endregion

        #region Mapa de Riesgo
        //[BreadCrumb(Clear = true, Label = "Mapa de Riesgo")]
        [RequiresAuthenticationAttribute]
        public ActionResult MapaRiesgo()
        {
            try
            {

                return View();
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public JsonResult GenerarMapaRiesgo(int pnTpoRiesgo, int TpoNivRiesgo)
        {
            ReportesModel model = new ReportesModel();
            try
            {

                var oMapaRiesgo = ireportes.ObtenerMapaRiesgo(pnTpoRiesgo, TpoNivRiesgo);
                if (oMapaRiesgo != null)
                {
                    model.oLstDataMapaRiesgo = oMapaRiesgo;
                }
                else { model.oLstDataMapaRiesgo = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }

        [RequiresAuthenticationAttribute]
        public JsonResult ObtenerRiesgosProbabilidadImpacto(int pnProbabilidad, int pnImpacto, int pnTpoRiesgo, int pnTpoNivelRiesgo)
        {
            ReportesModel model = new ReportesModel();
            try
            {
                var oDataDetRiesgos = ireportes.ObtenerDatosMapaDet(pnProbabilidad, pnImpacto, pnTpoRiesgo, pnTpoNivelRiesgo);
                if (oDataDetRiesgos != null)
                {
                    model.oLstDatosRiesgoDet = oDataDetRiesgos;

                    model.oProbabilidad = constante.ObtenerDescConstante(1012, pnProbabilidad);
                    model.oImpacto = constante.ObtenerDescConstante(1011, pnImpacto);
                }
                else { model.oLstDatosRiesgoDet = null; }

                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }

        [RequiresAuthenticationAttribute]
        public ActionResult ExportarInformeMapaCalorRiesgo(int pnTpoRiesgo, int pnTpoNivelRiesgo)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                var MapaCalor = ExcelInformeMapaRiesgo(pnTpoRiesgo, pnTpoNivelRiesgo);
                return new ExcelResult(MapaCalor, "Mapa de Riesgo [" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        private XLWorkbook ExcelInformeMapaRiesgo(int pnTpoRiesgo, int pnTpoNivelRiesgo)
        {
            try
            {
                XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoMapaCalorRiesgo.xlsx"), XLEventTracking.Disabled);
                /******************************************** Cuerpo *******************************************/
                //Hoja  1: 
                //obtengo la informacion de los riesgo por nivel de riesgo
                var oDetMapaRiesgo = ireportes.ObtenerDetalleRiesgoNivelRiesgo(pnTpoRiesgo, pnTpoNivelRiesgo);
                int i = 3, tmpTipoNivel = 0;
                for (int ii = 0; ii < oDetMapaRiesgo.Count; ii++)
                {
                    if (tmpTipoNivel != oDetMapaRiesgo[ii].nValorNivel)
                    {
                        wb.Worksheets.Worksheet(1).Range("A" + i + ":H" + i).Merge();
                        wb.Worksheets.Worksheet(1).Cell("A" + i).Value = oDetMapaRiesgo[ii].cDescNivel;
                        wb.Worksheets.Worksheet(1).Cell("A" + i).Style.Fill.BackgroundColor = XLColor.FromHtml(ConstGeneral.Get.ColorNivelRiesgo(oDetMapaRiesgo[ii].nValorNivel));
                        wb.Worksheets.Worksheet(1).Cell("A" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        wb.Worksheets.Worksheet(1).Cell("A" + i).Style.Font.FontColor = XLColor.White;
                        //wb.Worksheets.Worksheet(1).Cell("A" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                        wb.Worksheets.Worksheet(1).Cell("A" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        i++;
                    }

                    wb.Worksheets.Worksheet(1).Cell("A" + i).Value = oDetMapaRiesgo[ii].cCodRiesgo;
                    wb.Worksheets.Worksheet(1).Cell("A" + i).Style.Alignment.WrapText = true;
                    wb.Worksheets.Worksheet(1).Cell("A" + i).Style.Font.FontName = "Segoe UI";
                    wb.Worksheets.Worksheet(1).Cell("A" + i).Style.Font.FontSize = 9;
                    wb.Worksheets.Worksheet(1).Cell("A" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    wb.Worksheets.Worksheet(1).Cell("A" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Worksheets.Worksheet(1).Cell("A" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    wb.Worksheets.Worksheet(1).Cell("B" + i).Value = oDetMapaRiesgo[ii].cRiesgoIdentiticado;
                    wb.Worksheets.Worksheet(1).Cell("B" + i).Style.Alignment.WrapText = true;
                    wb.Worksheets.Worksheet(1).Cell("B" + i).Style.Font.FontName = "Segoe UI";
                    wb.Worksheets.Worksheet(1).Cell("B" + i).Style.Font.FontSize = 9;
                    wb.Worksheets.Worksheet(1).Cell("B" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    wb.Worksheets.Worksheet(1).Cell("B" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                    wb.Worksheets.Worksheet(1).Cell("B" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    wb.Worksheets.Worksheet(1).Cell("C" + i).Value = oDetMapaRiesgo[ii].cUser;
                    wb.Worksheets.Worksheet(1).Cell("C" + i).Style.Alignment.WrapText = true;
                    wb.Worksheets.Worksheet(1).Cell("C" + i).Style.Font.FontName = "Segoe UI";
                    wb.Worksheets.Worksheet(1).Cell("C" + i).Style.Font.FontSize = 9;
                    wb.Worksheets.Worksheet(1).Cell("C" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    wb.Worksheets.Worksheet(1).Cell("C" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Worksheets.Worksheet(1).Cell("C" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    wb.Worksheets.Worksheet(1).Cell("D" + i).Value = oDetMapaRiesgo[ii].cAgeDescripcion;
                    wb.Worksheets.Worksheet(1).Cell("D" + i).Style.Alignment.WrapText = true;
                    wb.Worksheets.Worksheet(1).Cell("D" + i).Style.Font.FontName = "Segoe UI";
                    wb.Worksheets.Worksheet(1).Cell("D" + i).Style.Font.FontSize = 9;
                    wb.Worksheets.Worksheet(1).Cell("D" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    wb.Worksheets.Worksheet(1).Cell("D" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Worksheets.Worksheet(1).Cell("D" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    wb.Worksheets.Worksheet(1).Cell("E" + i).Value = oDetMapaRiesgo[ii].cAreaDescripcion;
                    wb.Worksheets.Worksheet(1).Cell("E" + i).Style.Alignment.WrapText = true;
                    wb.Worksheets.Worksheet(1).Cell("E" + i).Style.Font.FontName = "Segoe UI";
                    wb.Worksheets.Worksheet(1).Cell("E" + i).Style.Font.FontSize = 9;
                    wb.Worksheets.Worksheet(1).Cell("E" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    wb.Worksheets.Worksheet(1).Cell("E" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Worksheets.Worksheet(1).Cell("E" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    wb.Worksheets.Worksheet(1).Cell("F" + i).Value = oDetMapaRiesgo[ii].cProcRiesgo;
                    wb.Worksheets.Worksheet(1).Cell("F" + i).Style.Alignment.WrapText = true;
                    wb.Worksheets.Worksheet(1).Cell("F" + i).Style.Font.FontName = "Segoe UI";
                    wb.Worksheets.Worksheet(1).Cell("F" + i).Style.Font.FontSize = 9;
                    wb.Worksheets.Worksheet(1).Cell("F" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    wb.Worksheets.Worksheet(1).Cell("F" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Worksheets.Worksheet(1).Cell("F" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    wb.Worksheets.Worksheet(1).Cell("G" + i).Value = oDetMapaRiesgo[ii].cEstadoRiesgo;
                    wb.Worksheets.Worksheet(1).Cell("G" + i).Style.Alignment.WrapText = true;
                    wb.Worksheets.Worksheet(1).Cell("G" + i).Style.Font.FontName = "Segoe UI";
                    wb.Worksheets.Worksheet(1).Cell("G" + i).Style.Font.FontSize = 9;
                    wb.Worksheets.Worksheet(1).Cell("G" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    wb.Worksheets.Worksheet(1).Cell("G" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Worksheets.Worksheet(1).Cell("G" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    wb.Worksheets.Worksheet(1).Cell("H" + i).Value = oDetMapaRiesgo[ii].dFechaDeteccion;
                    wb.Worksheets.Worksheet(1).Cell("H" + i).Style.Alignment.WrapText = true;
                    wb.Worksheets.Worksheet(1).Cell("H" + i).Style.Font.FontName = "Segoe UI";
                    wb.Worksheets.Worksheet(1).Cell("H" + i).Style.Font.FontSize = 9;
                    wb.Worksheets.Worksheet(1).Cell("H" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    wb.Worksheets.Worksheet(1).Cell("H" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Worksheets.Worksheet(1).Cell("H" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    tmpTipoNivel = oDetMapaRiesgo[ii].nValorNivel;

                    i++;
                }
                //Escribimos en la Hoja 2
                int NivRiesgoBajo = 0, NivRiesgoModerado = 0, NivRiesgoAlto = 0, NivRiesgoExtremo = 0;
                var oGenMapaRiesgo = ireportes.ObtenerMapaRiesgo(pnTpoRiesgo, pnTpoNivelRiesgo);
                for (int jj = 0; jj < oGenMapaRiesgo.Count; jj++)
                {
                    switch (oGenMapaRiesgo[jj].nConsCod)
                    {
                        case 1:
                            switch (oGenMapaRiesgo[jj].nConsValor)
                            {
                                case 1: wb.Worksheets.Worksheet(2).Cell("C4").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoModerado = NivRiesgoModerado + oGenMapaRiesgo[jj].nCantidad; break;
                                case 2: wb.Worksheets.Worksheet(2).Cell("E4").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 3: wb.Worksheets.Worksheet(2).Cell("G4").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 4: wb.Worksheets.Worksheet(2).Cell("I4").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 5: wb.Worksheets.Worksheet(2).Cell("K4").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                            }
                            break;
                        case 2:
                            switch (oGenMapaRiesgo[jj].nConsValor)
                            {
                                case 1: wb.Worksheets.Worksheet(2).Cell("C6").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoModerado = NivRiesgoModerado + oGenMapaRiesgo[jj].nCantidad; break;
                                case 2: wb.Worksheets.Worksheet(2).Cell("E6").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 3: wb.Worksheets.Worksheet(2).Cell("G6").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 4: wb.Worksheets.Worksheet(2).Cell("I6").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 5: wb.Worksheets.Worksheet(2).Cell("K6").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                            }
                            break;
                        case 3:
                            switch (oGenMapaRiesgo[jj].nConsValor)
                            {
                                case 1: wb.Worksheets.Worksheet(2).Cell("C8").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoBajo = NivRiesgoBajo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 2: wb.Worksheets.Worksheet(2).Cell("E8").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoModerado = NivRiesgoModerado + oGenMapaRiesgo[jj].nCantidad; break;
                                case 3: wb.Worksheets.Worksheet(2).Cell("G8").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 4: wb.Worksheets.Worksheet(2).Cell("I8").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 5: wb.Worksheets.Worksheet(2).Cell("K8").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                            }
                            break;
                        case 4:
                            switch (oGenMapaRiesgo[jj].nConsValor)
                            {
                                case 1: wb.Worksheets.Worksheet(2).Cell("C10").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoBajo = NivRiesgoBajo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 2: wb.Worksheets.Worksheet(2).Cell("E10").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoBajo = NivRiesgoBajo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 3: wb.Worksheets.Worksheet(2).Cell("G10").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoModerado = NivRiesgoModerado + oGenMapaRiesgo[jj].nCantidad; break;
                                case 4: wb.Worksheets.Worksheet(2).Cell("I10").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 5: wb.Worksheets.Worksheet(2).Cell("K10").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                            }
                            break;
                        case 5:
                            switch (oGenMapaRiesgo[jj].nConsValor)
                            {
                                case 1: wb.Worksheets.Worksheet(2).Cell("C12").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoBajo = NivRiesgoBajo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 2: wb.Worksheets.Worksheet(2).Cell("E12").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoBajo = NivRiesgoBajo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 3: wb.Worksheets.Worksheet(2).Cell("G12").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoModerado = NivRiesgoModerado + oGenMapaRiesgo[jj].nCantidad; break;
                                case 4: wb.Worksheets.Worksheet(2).Cell("I12").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 5: wb.Worksheets.Worksheet(2).Cell("K12").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                            }
                            break;
                    }
                }

                wb.Worksheets.Worksheet(2).Cell("C16").Value = NivRiesgoBajo;
                wb.Worksheets.Worksheet(2).Cell("C17").Value = NivRiesgoModerado;
                wb.Worksheets.Worksheet(2).Cell("C18").Value = NivRiesgoAlto;
                wb.Worksheets.Worksheet(2).Cell("C19").Value = NivRiesgoExtremo;
                /***************************************** Fin Cuerpo ******************************************/

                return wb;
            }
            catch { throw; }
        }


        [RequiresAuthenticationAttribute]
        public ActionResult ExportarInformeMapaCalorDetalleRiesgo(int pnTpoRiesgo, int pnTpoNivelRiesgo, int pnProbabilidad, int pnImpacto)
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            try
            {
                XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoMapaCalorRiesgo.xlsx"), XLEventTracking.Disabled);
                IXLWorksheet ws = wb.Worksheet(1);
                /******************************************** Cuerpo *******************************************/
                //Hoja  1: 
                //obtengo la informacion de los riesgo por nivel de riesgo
                var oDetMapaRiesgo = ireportes.ObtenerDetalleRiesgoNivelRiesgo(pnTpoRiesgo, pnTpoNivelRiesgo).Where(x => x.nProbabilidad == pnProbabilidad && x.nImpacto == pnImpacto).ToList();
                int i = 3, tmpTipoNivel = 0;
                for (int ii = 0; ii < oDetMapaRiesgo.Count; ii++)
                {
                    if (tmpTipoNivel != oDetMapaRiesgo[ii].nValorNivel)
                    {
                        ws.Range("A" + i + ":H" + i).Merge();
                        ws.Cell("A" + i).Value = oDetMapaRiesgo[ii].cDescNivel;
                        ws.Cell("A" + i).Style.Fill.BackgroundColor = XLColor.FromHtml(ConstGeneral.Get.ColorNivelRiesgo(oDetMapaRiesgo[ii].nValorNivel));
                        ws.Cell("A" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("A" + i).Style.Font.FontColor = XLColor.White;
                        //ws.Cell("A" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                        ws.Cell("A" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        i++;
                    }

                    ws.Cell("A" + i).Value = oDetMapaRiesgo[ii].cCodRiesgo;
                    ws.Cell("A" + i).Style.Alignment.WrapText = true;
                    ws.Cell("A" + i).Style.Font.FontName = "Segoe UI";
                    ws.Cell("A" + i).Style.Font.FontSize = 9;
                    ws.Cell("A" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("A" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("A" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("B" + i).Value = oDetMapaRiesgo[ii].cRiesgoIdentiticado;
                    ws.Cell("B" + i).Style.Alignment.WrapText = true;
                    ws.Cell("B" + i).Style.Font.FontName = "Segoe UI";
                    ws.Cell("B" + i).Style.Font.FontSize = 9;
                    ws.Cell("B" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("B" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                    ws.Cell("B" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("C" + i).Value = oDetMapaRiesgo[ii].cUser;
                    ws.Cell("C" + i).Style.Alignment.WrapText = true;
                    ws.Cell("C" + i).Style.Font.FontName = "Segoe UI";
                    ws.Cell("C" + i).Style.Font.FontSize = 9;
                    ws.Cell("C" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("C" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("C" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("D" + i).Value = oDetMapaRiesgo[ii].cAgeDescripcion;
                    ws.Cell("D" + i).Style.Alignment.WrapText = true;
                    ws.Cell("D" + i).Style.Font.FontName = "Segoe UI";
                    ws.Cell("D" + i).Style.Font.FontSize = 9;
                    ws.Cell("D" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("D" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("D" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("E" + i).Value = oDetMapaRiesgo[ii].cAreaDescripcion;
                    ws.Cell("E" + i).Style.Alignment.WrapText = true;
                    ws.Cell("E" + i).Style.Font.FontName = "Segoe UI";
                    ws.Cell("E" + i).Style.Font.FontSize = 9;
                    ws.Cell("E" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("E" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("E" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("F" + i).Value = oDetMapaRiesgo[ii].cProcRiesgo;
                    ws.Cell("F" + i).Style.Alignment.WrapText = true;
                    ws.Cell("F" + i).Style.Font.FontName = "Segoe UI";
                    ws.Cell("F" + i).Style.Font.FontSize = 9;
                    ws.Cell("F" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("F" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("F" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("G" + i).Value = oDetMapaRiesgo[ii].cEstadoRiesgo;
                    ws.Cell("G" + i).Style.Alignment.WrapText = true;
                    ws.Cell("G" + i).Style.Font.FontName = "Segoe UI";
                    ws.Cell("G" + i).Style.Font.FontSize = 9;
                    ws.Cell("G" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("G" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("G" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    ws.Cell("H" + i).Value = oDetMapaRiesgo[ii].dFechaDeteccion;
                    ws.Cell("H" + i).Style.Alignment.WrapText = true;
                    ws.Cell("H" + i).Style.Font.FontName = "Segoe UI";
                    ws.Cell("H" + i).Style.Font.FontSize = 9;
                    ws.Cell("H" + i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Cell("H" + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell("H" + i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    tmpTipoNivel = oDetMapaRiesgo[ii].nValorNivel;

                    i++;
                }
                //Escribimos en la Hoja 2
                int NivRiesgoBajo = 0, NivRiesgoModerado = 0, NivRiesgoAlto = 0, NivRiesgoExtremo = 0;
                var oGenMapaRiesgo = ireportes.ObtenerMapaRiesgo(pnTpoRiesgo, pnTpoNivelRiesgo).Where(x => x.nConsCod == pnProbabilidad && x.nConsValor == pnImpacto).ToList(); ;
                ws = wb.Worksheet(2);
                for (int jj = 0; jj < oGenMapaRiesgo.Count; jj++)
                {
                    switch (oGenMapaRiesgo[jj].nConsCod)
                    {
                        case 1:
                            switch (oGenMapaRiesgo[jj].nConsValor)
                            {
                                case 1: ws.Cell("C4").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoModerado = NivRiesgoModerado + oGenMapaRiesgo[jj].nCantidad; break;
                                case 2: ws.Cell("E4").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 3: ws.Cell("G4").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 4: ws.Cell("I4").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 5: ws.Cell("K4").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                            }
                            break;
                        case 2:
                            switch (oGenMapaRiesgo[jj].nConsValor)
                            {
                                case 1: ws.Cell("C6").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoModerado = NivRiesgoModerado + oGenMapaRiesgo[jj].nCantidad; break;
                                case 2: ws.Cell("E6").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 3: ws.Cell("G6").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 4: ws.Cell("I6").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 5: ws.Cell("K6").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                            }
                            break;
                        case 3:
                            switch (oGenMapaRiesgo[jj].nConsValor)
                            {
                                case 1: ws.Cell("C8").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoBajo = NivRiesgoBajo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 2: ws.Cell("E8").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoModerado = NivRiesgoModerado + oGenMapaRiesgo[jj].nCantidad; break;
                                case 3: ws.Cell("G8").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 4: ws.Cell("I8").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 5: ws.Cell("K8").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                            }
                            break;
                        case 4:
                            switch (oGenMapaRiesgo[jj].nConsValor)
                            {
                                case 1: ws.Cell("C10").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoBajo = NivRiesgoBajo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 2: ws.Cell("E10").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoBajo = NivRiesgoBajo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 3: ws.Cell("G10").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoModerado = NivRiesgoModerado + oGenMapaRiesgo[jj].nCantidad; break;
                                case 4: ws.Cell("I10").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 5: ws.Cell("K10").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoExtremo = NivRiesgoExtremo + oGenMapaRiesgo[jj].nCantidad; break;
                            }
                            break;
                        case 5:
                            switch (oGenMapaRiesgo[jj].nConsValor)
                            {
                                case 1: ws.Cell("C12").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoBajo = NivRiesgoBajo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 2: ws.Cell("E12").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoBajo = NivRiesgoBajo + oGenMapaRiesgo[jj].nCantidad; break;
                                case 3: ws.Cell("G12").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoModerado = NivRiesgoModerado + oGenMapaRiesgo[jj].nCantidad; break;
                                case 4: ws.Cell("I12").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                                case 5: ws.Cell("K12").Value = oGenMapaRiesgo[jj].nCantidad; NivRiesgoAlto = NivRiesgoAlto + oGenMapaRiesgo[jj].nCantidad; break;
                            }
                            break;
                    }
                }

                ws.Cell("C16").Value = NivRiesgoBajo;
                ws.Cell("C17").Value = NivRiesgoModerado;
                ws.Cell("C18").Value = NivRiesgoAlto;
                ws.Cell("C19").Value = NivRiesgoExtremo;
                /***************************************** Fin Cuerpo ******************************************/

                return new ExcelResult(wb, String.Concat("Detalle Mapa de Riesgo [", usuario.cUser, DateTime.Now.ToString("yyyyMMddhhmmss"), "]"));
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }


        #endregion

        #region Reportes Graficos
        //[BreadCrumb(Clear = true, Label = "Reporte Gráficos")]
        [RequiresAuthenticationAttribute]
        public ActionResult Grafico()
        {
            try
            {

                return View();
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        #region Graficos de Riesgos Operacionales
        [RequiresAuthenticationAttribute]
        public JsonResult GraficosRiesgosOperacionales(int pnIndex)
        {
            try
            {
                ReportesModel model = new ReportesModel();

                var datos = ireportes.ReporteGraficoRiesgoOperacional(pnIndex, (int)Riesgos.RiesgoOperacional);
                switch (pnIndex)
                {
                    case 0:
                        model.modelGraf = new ReportGrafModel()
                        {
                            data = datos.Select(r => new
                            {
                                name = r.cProceso,
                                y = Convert.ToInt32(r.nCantidad),
                                drilldown = r.nProceso
                            }).ToList<object>(),
                            interval = 25,
                            ymin = 10,
                            ymax = datos.Sum(x => x.nCantidad),

                        };
                        break;
                    case 1:
                        model.modelGraf = new ReportGrafModel()
                        {
                            data = datos.Select(r => r.nCantidad).ToList<object>(),
                            categories = datos.Select(r => ((object)r.nAnio).ToString()).ToList(),
                            interval = 25,
                            ymin = 10,
                        };
                        break;
                    case 2:
                        model.modelGraf = new ReportGrafModel()
                        {
                            data = datos.Select(r => new
                            {
                                name = r.cCausaDesc,
                                y = Convert.ToDecimal(r.nProcentaje),
                                drilldown = r.cCodCausa
                            }).ToList<object>()
                        };
                        break;
                    case 3:
                        model.modelGraf = new ReportGrafModel()
                        {
                            data = new List<object>() {
                                new {
                                    name = "Factor Riesgo",
                                    data = datos.Select(
                                        r => new {
                                            name = r.cFactor,
                                            y = Convert.ToInt32(r.nProcentaje),
                                            drilldown = r.cFactor
                                        }
                                ).ToList<object>()},
                            },

                            //data = datos.Select(r => new
                            //{
                            //    name = r.cFactor,
                            //    y = Convert.ToDecimal(r.nCantidad),
                            //    drilldown = r.cFactor
                            //}).ToList<object>(),
                        };
                        break;
                    case 4:
                        model.modelGraf = new ReportGrafModel()
                        {
                            data = new List<object>() {
                                new {
                                    name = "Riesgo Operacional",
                                    data = datos.Select(
                                        r => new {
                                            name = r.cEstado,
                                            y = Convert.ToInt32(r.nCantidad)
                                        }
                                ).ToList<object>()},
                            },
                            //drilldown = new List<object>() {
                            //    new {
                            //        //name = "Riesgo Operacional",
                            //        series = datos.Select(
                            //            r => new {
                            //                name = r.cEstado,
                            //                id = r.cEstado,
                            //                data = detalle.Select(
                            //                                rr => new {
                            //                                    name = r.cEstado,
                            //                                    y = Convert.ToInt32(rr.nCantidad)
                            //                                }
                            //                        ).ToList<object>()
                            //            }
                            //    ).ToList<object>()},
                            //},

                        };
                        break;

                }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }

        #endregion

        #region Graficos de Evaluaciones
        [RequiresAuthenticationAttribute]
        public JsonResult ReportesGrafEvaluaciones()
        {

            return null;
        }
        #endregion

        #region Graficos de Eventos de Perdida
        [RequiresAuthenticationAttribute]
        public JsonResult GraficosEventoPerdida(int pnIndex)
        {
            ReportesModel model = new ReportesModel();
            try
            {
                var datos = ireportes.ReportesGraficosEventoPerdida(pnIndex);
                switch (pnIndex)
                {
                    case 0:

                        model.modelGraf = new ReportGrafModel()
                        {
                            data = datos.Select(r => new
                            {
                                name = r.DescClaseEvento,
                                y = Convert.ToDouble(r.PerdidaNeta)
                            }).ToList<object>(),
                            dataReport = datos
                        };
                        break;
                    case 1:
                        model.modelGraf = new ReportGrafModel()
                        {
                            categories = datos.Select(c => ((object)c.Anio).ToString()).ToList(),
                            data = new List<object>() {
                            new { name= "Pérdida Neta", data = datos.Select(r => r.PerdidaNeta).ToList<object>()},
                            new { name= "Monto Bruto", data = datos.Select(r => r.MontoBruto).ToList<object>()}
                            },
                            interval = 10000,
                            ymin = 10000,
                            dataReport = datos
                        };
                        break;
                    case 2:
                        model.modelGraf = new ReportGrafModel()
                        {
                            categories = datos.Select(c => ((object)c.DescClaseEvento).ToString()).ToList(),
                            data = new List<object>() {
                            new { name= "Monto Bruto", data = datos.Select(r => r.MontoBruto).ToList<object>()},
                            new { name= "Monto Recuperado", data = datos.Select(r => r.MontoRecuperado).ToList<object>()}
                        },
                            interval = 10000,
                            ymin = 10000,
                            dataReport = datos
                        };
                        break;
                    case 3:
                        model.modelGraf = new ReportGrafModel()
                        {
                            categories = datos.Select(c => ((object)c.Agencia).ToString()).ToList(),
                            data = new List<object>() {
                                new { name= "Pérdida Neta", data = datos.Select(r => r.PerdidaNeta).ToList<object>()},
                                new { name= "Monto Bruto", data = datos.Select(r => r.MontoBruto).ToList<object>()}
                            },
                            interval = 10000,
                            ymin = 10000,
                            dataReport = datos
                        };
                        break;
                    case 4:
                        model.modelGraf = new ReportGrafModel()
                        {
                            categories = datos.Select(c => ((object)c.Anio).ToString()).ToList(),
                            data = new List<object>() {
                            new { name= "Total", data = datos.Select(r => r.Cantidad).ToList<object>()},
                        },
                            interval = 10000,
                            ymin = 10000,
                            dataReport = datos
                        };
                        break;
                    default:
                        model.modelGraf = null;
                        break;
                }
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }

        }
        #endregion

        /// <summary>
        /// Muesta la cantidad de riesgo y el proceso actual del riesgo
        /// </summary>
        /// <param name="pnTpoRiesgo"></param>
        /// <returns></returns>
        //public JsonResult JSReporteGrafico(int pnTpoRiesgo, int pnReporteGraf)
        //{
        //    ReportesModel model = new ReportesModel();
        //    try
        //    {
        //        if (pnReporteGraf == 0)
        //        {
        //            List<Reportes> datos = ireportes.ReporteGraficoCantidadRiesgoEstado(pnTpoRiesgo);
        //            model.modelGraf = new ReportGrafModel()
        //            {
        //                data = datos.Select(r => new
        //                {
        //                    x = r.cTpoRiesgo,
        //                    y0 = Convert.ToInt32(r.cEstadoRiesgo[0]),
        //                    y1 = Convert.ToInt32(r.cEstadoRiesgo[1]),
        //                    y2 = Convert.ToInt32(r.cEstadoRiesgo[2]),
        //                    y3 = Convert.ToInt32(r.cEstadoRiesgo[3]),
        //                    y4 = Convert.ToInt32(r.cEstadoRiesgo[4]),
        //                    y5 = Convert.ToInt32(r.cEstadoRiesgo[5]),
        //                    y6 = Convert.ToInt32(r.cEstadoRiesgo[6])
        //                }).ToList<object>(),
        //                xkey = "x",
        //                ykeys = new List<string>() { "y0", "y1", "y2", "y3", "y4", "y5", "y6" },
        //                labels = new List<string>() { "Registrados", "Paso 1", "Paso 2", "Paso 3", "Paso 4", "Paso 5", "Finalizado" },
        //                barColors = new List<string>() { "#1C1741", "#313F86", "#26618D", "#455064", "#242d3c", "#707f9b", "#455064" },
        //                ymax = "100" //Convert.ToString(datos.Count)
        //            };
        //        }
        //        else if (pnReporteGraf == 1)
        //        {
        //            List<dynamic> dReporte = ireportes.ReporteGraficoCantidadAnio(Convert.ToString(pnTpoRiesgo));
        //            model.modelGraf = new ReportGrafModel()
        //            {
        //                data = dReporte.Select(r => new
        //                {
        //                    y = r.nAnio,
        //                    xA = r.x1,
        //                    xB = r.x2
        //                }).ToList<object>(),
        //                xkey = "y",
        //                ykeys = new List<string>() { "xA", "xB" },
        //                labels = new List<string>() { "Riesgo Operacional", "Evaluación" }

        //            };
        //        }

        //        return Json(JsonConvert.SerializeObject(model));
        //    }
        //    catch { throw; }

        //}

        public JsonResult GrafRiesgoNivelRiesgoResidual(int pnTpoRiesgo)
        {
            ReportesModel model = new ReportesModel();
            try
            {
                var datos = ireportes.ReporteGraficoCantidadRiesgoNivelResidual(pnTpoRiesgo);
                model.modelGraf = new ReportGrafModel()
                {
                    data = datos.Select(d => new { label = d.cConsDescripcion, value = d.nCantidad, formatted = (d.nConsCod + "%") }).ToList<object>(),
                    colors = new List<string>() { "#1FAA00", "#FFEB3B", "#FF6D00", "#D50000" }
                };
                return Json(JsonConvert.SerializeObject(model));
            }
            catch { throw; }
        }

        #endregion

        #region Reporte Varios
        //[BreadCrumb(Clear = true, Label = "Reporte Varios")]
        [RequiresAuthenticationAttribute]
        public ActionResult Varios()
        {
            try
            {
                var model = new GestionRiesgoModel()
                {
                    oLstProcesoRiesgo = constante.ObtenerConstantes(1002),
                    oLstIncentivos = constante.ObtenerConstantes(1515),
                    oLstEstados = constante.ObtenerConstantes(3000)

                };
                return View(model);
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
                return View("Error", error);
            }
        }

        #region Reporte Varios - Riesgo Operacional

        [RequiresAuthenticationAttribute]
        public ActionResult ReportesRiesgosRechazados(string psDesde, string psHasta)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoRiesgoOperacionalRechazados.xlsx"), XLEventTracking.Disabled);
                IXLWorksheet ws = wb.Worksheet(1);

                var rechazados = ireportes.RiesgosOperacinalesRechazados(psDesde, psHasta);
                int fila = 4;
                int[] matBucles = { 0, 0, 0, 0 }; /*(0) - Bucle Causas, (1) - Bucle Controles, (2) - Bucle Planes Accion, (3) - Responsables Planes Accion*/
                if (rechazados.Count > 0)
                {
                    foreach (var riesgos in rechazados)
                    {
                        matBucles[0] = fila;

                        #region Datos Principales
                        var detalle = iriesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo()
                        {
                            nNroRiesgo = (long)riesgos.nNroRiesgo
                        });

                        //Codigo
                        ws.Cell("A" + fila).Value = detalle.cCodRiesgo;
                        ws.Cell("A" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("A" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("A" + fila).Style.Font.FontSize = 9;
                        ws.Cell("A" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("A" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //Riesgo Identificado
                        ws.Cell("B" + fila).Value = detalle.cRiesgoIdentiticado;
                        ws.Cell("B" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("B" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("B" + fila).Style.Font.FontSize = 9;
                        ws.Cell("B" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("B" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //Agencia
                        ws.Cell("D" + fila).Value = detalle.oAgencia.cAgeDescripcion;
                        ws.Cell("D" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("D" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("D" + fila).Style.Font.FontSize = 9;
                        ws.Cell("D" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("D" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //Area
                        ws.Cell("E" + fila).Value = detalle.oAgencia.oArea.cAreaDescripcion;
                        ws.Cell("E" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("E" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("E" + fila).Style.Font.FontSize = 9;
                        ws.Cell("E" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("E" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //Proceso
                        ws.Cell("F" + fila).Value = detalle.oProceso.cDescProceso;
                        ws.Cell("F" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("F" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("F" + fila).Style.Font.FontSize = 9;
                        ws.Cell("F" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("F" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //Sub Proceso
                        ws.Cell("G" + fila).Value = detalle.oProceso.oSubProceso.cDescSubProceso;
                        ws.Cell("G" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("G" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("G" + fila).Style.Font.FontSize = 9;
                        ws.Cell("G" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("G" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ////Control Existente                                               
                        //ws.Cell("H" + fila).Value = detalle.oControlAreas.cControlDescripcion;
                        //ws.Cell("H" + fila).Style.Alignment.WrapText = true;
                        //ws.Cell("H" + fila).Style.Font.FontName = "Segoe UI";
                        //ws.Cell("H" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("H" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        //ws.Cell("H" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //Fecha de Deteccion
                        ws.Cell("I" + fila).Value = detalle.dFechaDeteccion;
                        ws.Cell("I" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("I" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("I" + fila).Style.Font.FontSize = 9;
                        ws.Cell("I" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("I" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //Efectos del Riesgo
                        ws.Cell("J" + fila).Value = detalle.cEfectosRiesgo;
                        ws.Cell("J" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("J" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("J" + fila).Style.Font.FontSize = 9;
                        ws.Cell("J" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("J" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //Motivo del Rechazo
                        ws.Cell("K" + fila).Value = riesgos.cMotivoRechazo;
                        ws.Cell("K" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("K" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("K" + fila).Style.Font.FontSize = 9;
                        ws.Cell("K" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("K" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //Usuario del Rechazo
                        ws.Cell("L" + fila).Value = riesgos.cUserRechazo;
                        ws.Cell("L" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("L" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("L" + fila).Style.Font.FontSize = 9;
                        ws.Cell("L" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("L" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //Fecha del Rechazo
                        ws.Cell("M" + fila).Value = riesgos.dFechaRechazo;
                        ws.Cell("M" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("M" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("M" + fila).Style.Font.FontSize = 9;
                        ws.Cell("M" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("M" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                        #endregion

                        #region Insertar Causas del riesgo
                        var oCausasRiesgo = causaRiesgo.ObtenerCausaPorRiesgo((long)riesgos.nNroRiesgo);
                        if (oCausasRiesgo.Count > 0)
                        {
                            int pfCausa = fila;
                            foreach (var causas in oCausasRiesgo)
                            {
                                ws.Cell("C" + pfCausa).Value = causas.cCausaDesc;
                                ws.Cell("C" + pfCausa).Style.Alignment.WrapText = true;
                                ws.Cell("C" + pfCausa).Style.Font.FontName = "Segoe UI";
                                ws.Cell("C" + pfCausa).Style.Font.FontSize = 9;
                                ws.Cell("C" + pfCausa).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                ws.Cell("C" + pfCausa).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                pfCausa++;
                            }
                            matBucles[0] = pfCausa - 1;
                        }
                        #endregion

                        ws.Range("A" + fila + ":" + "A" + matBucles.Max()).Merge();
                        ws.Range("B" + fila + ":" + "B" + matBucles.Max()).Merge();
                        ws.Range("D" + fila + ":" + "D" + matBucles.Max()).Merge();
                        ws.Range("E" + fila + ":" + "E" + matBucles.Max()).Merge();
                        ws.Range("F" + fila + ":" + "F" + matBucles.Max()).Merge();
                        ws.Range("G" + fila + ":" + "G" + matBucles.Max()).Merge();
                        ws.Range("H" + fila + ":" + "H" + matBucles.Max()).Merge();
                        ws.Range("I" + fila + ":" + "I" + matBucles.Max()).Merge();
                        ws.Range("J" + fila + ":" + "J" + matBucles.Max()).Merge();
                        ws.Range("K" + fila + ":" + "K" + matBucles.Max()).Merge();
                        ws.Range("L" + fila + ":" + "L" + matBucles.Max()).Merge();
                        ws.Range("M" + fila + ":" + "M" + matBucles.Max()).Merge();

                        fila = matBucles.Max();

                        fila++;
                    }
                    ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                }
                return new ExcelResult(wb, "Reporte Riesgos Rechazados [" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }


        #region Plan de Accion 
        [RequiresAuthenticationAttribute]
        public ActionResult ReportesPlanAccionEstado(int pnTpoRiesgo, int pnEstado)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                var lstData = ireportes.ObtenerRiesgoEstadoPlanAccion(pnTpoRiesgo, pnEstado);

                var reporte = GenerarReportePlanesAccion(lstData);

                return new ExcelResult(reporte, "Reporte Plan Acción por Estado [" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");

            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ReportePlanesAccion(int pnTpoBuscar, int pnTpoRiesgo, string psBuscar)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                string buscar = "";
                var MatrizGenerado = GenerarReportePlanesAccion(ireportes.ObtenerRiesgoPlanAccionReporte(pnTpoBuscar, pnTpoRiesgo, psBuscar));
                switch (pnTpoBuscar)
                {
                    case 3000:
                        buscar = "Estado";
                        break;
                    case 5000:
                        buscar = "Riesgo";
                        break;
                    default:
                        buscar = "ErrorFiltro";
                        break;
                }

                return new ExcelResult(MatrizGenerado, "Reporte Plan Accion por " + buscar + "  [" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }

        private XLWorkbook GenerarReportePlanesAccion(List<dynamic> _reporte)
        {
            try
            {
                XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoPlanesAccion.xlsx"));

                IXLWorksheet ws = wb.Worksheet(1);

                if (_reporte.Count > 0)
                {
                    int nPosicion = 5; //Fila de inicio
                    int[] mPosicion = { 0, 0 }; /*(0) - Plan Accion, (1) - Responsable Plan Acicion*/

                    foreach (var riesgo in _reporte)
                    {

                        int TipoRiesgo = iriesgo.ObtenerTpoRiesgo((long)Convert.ToInt32(riesgo.NroRiesgo));
                        //var riesgos = TipoRiesgo == (int)Riesgos.RiesgoOperacional ? iriesgo.ObtenerInfoGeneralRiesgoOperacional(new Riesgo() { nNroRiesgo = (long)Convert.ToInt32(riesgo.NroRiesgo) }) : ievaluacion.MostrarDetalleEvaluacion((long)Convert.ToInt32(riesgo.NroRiesgo));
                        var riesgos = iriesgo.ObtenerInfoGeneralRiesgoOperacional(new DetalleRiesgo() { nNroRiesgo = (long)Convert.ToInt32(riesgo.NroRiesgo) });

                        //mPosicion[0] = nPosicion; //Plan  de Accion
                        ws.Cell("A" + nPosicion).Value = riesgos.cCodRiesgo;
                        ws.Cell("A" + nPosicion).Style.Alignment.WrapText = true;
                        ws.Cell("A" + nPosicion).Style.Font.FontName = "Segoe UI";
                        ws.Cell("A" + nPosicion).Style.Font.FontSize = 9;
                        ws.Cell("A" + nPosicion).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("A" + nPosicion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("B" + nPosicion).Value = riesgos.cRiesgoIdentiticado;
                        ws.Cell("B" + nPosicion).Style.Alignment.WrapText = true;
                        ws.Cell("B" + nPosicion).Style.Font.FontName = "Segoe UI";
                        ws.Cell("B" + nPosicion).Style.Font.FontSize = 9;
                        ws.Cell("B" + nPosicion).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                        ws.Cell("B" + nPosicion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        var PlanAccion = planAccion.MostrarPlanesAccionRiesgo((long)Convert.ToInt64(riesgo.NroRiesgo));
                        if (PlanAccion.Count > 0)
                        {
                            int filaPlanAccion = nPosicion;
                            foreach (var plan in PlanAccion)
                            {
                                mPosicion[0] = filaPlanAccion; //Plan de accion 
                                ws.Cell("C" + filaPlanAccion).Value = Convert.ToString(plan.nPlanCod);
                                ws.Cell("C" + filaPlanAccion).Style.Alignment.WrapText = true;
                                ws.Cell("C" + filaPlanAccion).Style.Font.FontName = "Segoe UI";
                                ws.Cell("C" + filaPlanAccion).Style.Font.FontSize = 9;
                                ws.Cell("C" + filaPlanAccion).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("C" + filaPlanAccion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("D" + filaPlanAccion).Value = plan.cPlanDescripcion;
                                ws.Cell("D" + filaPlanAccion).Style.Alignment.WrapText = true;
                                ws.Cell("D" + filaPlanAccion).Style.Font.FontName = "Segoe UI";
                                ws.Cell("D" + filaPlanAccion).Style.Font.FontSize = 9;
                                ws.Cell("D" + filaPlanAccion).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                                ws.Cell("D" + filaPlanAccion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("G" + filaPlanAccion).Value = plan.dFechaImplement;
                                ws.Cell("G" + filaPlanAccion).Style.Alignment.WrapText = true;
                                ws.Cell("G" + filaPlanAccion).Style.Font.FontName = "Segoe UI";
                                ws.Cell("G" + filaPlanAccion).Style.Font.FontSize = 9;
                                ws.Cell("G" + filaPlanAccion).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("G" + filaPlanAccion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("H" + filaPlanAccion).Value = plan.cComentario;
                                ws.Cell("H" + filaPlanAccion).Style.Alignment.WrapText = true;
                                ws.Cell("H" + filaPlanAccion).Style.Font.FontName = "Segoe UI";
                                ws.Cell("H" + filaPlanAccion).Style.Font.FontSize = 9;
                                ws.Cell("H" + filaPlanAccion).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                                ws.Cell("H" + filaPlanAccion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("I" + filaPlanAccion).Value = plan.cEstado;
                                ws.Cell("I" + filaPlanAccion).Style.Alignment.WrapText = true;
                                ws.Cell("I" + filaPlanAccion).Style.Font.FontName = "Segoe UI";
                                ws.Cell("I" + filaPlanAccion).Style.Font.FontSize = 9;
                                ws.Cell("I" + filaPlanAccion).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("I" + filaPlanAccion).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                var Responsables = planAccion.MostrarDetalleResponsablesPlanesAccionRiesgo((long)Convert.ToInt64(riesgo.NroRiesgo), plan.nPlanCod);
                                if (Responsables.Count > 0)
                                {
                                    int filaResponsable = filaPlanAccion;
                                    foreach (var responsable in Responsables)
                                    {
                                        mPosicion[1] = filaResponsable;
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
                                        ws.Cell("F" + filaResponsable).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                                        ws.Cell("F" + filaResponsable).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                        filaResponsable++;
                                    }
                                    mPosicion[1] = filaResponsable - 1;
                                }
                                filaPlanAccion++;
                            }
                            mPosicion[0] = filaPlanAccion - 1;
                        }

                        ws.Range("A" + nPosicion + ":" + "A" + mPosicion.Max()).Merge();
                        ws.Range("B" + nPosicion + ":" + "B" + mPosicion.Max()).Merge();
                        ws.Range("C" + mPosicion[0] + ":" + "C" + mPosicion.Max()).Merge();
                        ws.Range("D" + mPosicion[0] + ":" + "D" + mPosicion.Max()).Merge();
                        ws.Range("E" + mPosicion[1] + ":" + "E" + mPosicion.Max()).Merge();
                        ws.Range("F" + mPosicion[1] + ":" + "F" + mPosicion.Max()).Merge();
                        ws.Range("G" + mPosicion[0] + ":" + "G" + mPosicion.Max()).Merge();
                        ws.Range("H" + nPosicion + ":" + "H" + mPosicion.Max()).Merge();
                        ws.Range("I" + mPosicion[0] + ":" + "I" + mPosicion.Max()).Merge();

                        nPosicion = mPosicion.Max();

                        nPosicion++;
                    }
                    /***************************************** Fin Cuerpo ******************************************/
                    ws.Range("A5:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Range("A5:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                }
                return wb;
            }
            catch { throw; }
        }
        #endregion


        public ActionResult ReporteRiesgoOpeEstado(string psProceso)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                var MatrizGenerado = GenerarReporteRiesgoOpeEstado(psProceso);

                return new ExcelResult(MatrizGenerado, String.Concat("Reporte Riesgo Operacinales Estado [", usuario.cUser, DateTime.Now.ToString("yyyyMMddhhmmss"), "]"));
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }

        private XLWorkbook GenerarReporteRiesgoOpeEstado(string psProceso)
        {
            try
            {
                XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoRiesgoOpeEstado.xlsx"), XLEventTracking.Disabled);

                IXLWorksheet ws = wb.Worksheet(1); //Hoja 1

                var universoRiesgo = ireportes.ObtenerRiesgoRiesgosOperacionalesProceso(psProceso);

                if (universoRiesgo.Count > 0)
                {
                    int fila = 4; //Fila de inicio
                    int[] matBucles = { 0, 0 }; /*(0) Causas*/

                    foreach (var riesgo in universoRiesgo)
                    {
                        matBucles[0] = fila;

                        var detRiesgo = iriesgo.ObtenerInfoGeneralRiesgoOperacional(Convert.ToInt64(riesgo.NroRiesgo));

                        ws.Cell("A" + fila).Value = detRiesgo.cCodRiesgo;
                        ws.Cell("A" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("A" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("A" + fila).Style.Font.FontSize = 9;
                        ws.Cell("A" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("A" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("B" + fila).Value = detRiesgo.cRiesgoIdentiticado;
                        ws.Cell("B" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("B" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("B" + fila).Style.Font.FontSize = 9;
                        ws.Cell("B" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                        ws.Cell("B" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("D" + fila).Value = detRiesgo.oProceso.cDescProceso;
                        ws.Cell("D" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("D" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("D" + fila).Style.Font.FontSize = 9;
                        ws.Cell("D" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("D" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("E" + fila).Value = detRiesgo.oSubProceso.cDescSubProceso;
                        ws.Cell("E" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("E" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("E" + fila).Style.Font.FontSize = 9;
                        ws.Cell("E" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("E" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("F" + fila).Value = detRiesgo.oControlAreas.cControlDescripcion;
                        ws.Cell("F" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("F" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("F" + fila).Style.Font.FontSize = 9;
                        ws.Cell("F" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("F" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("G" + fila).Value = detRiesgo.dFechaDeteccion;
                        ws.Cell("G" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("G" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("G" + fila).Style.Font.FontSize = 9;
                        ws.Cell("G" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("G" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("H" + fila).Value = detRiesgo.cObservacion;
                        ws.Cell("H" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("H" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("H" + fila).Style.Font.FontSize = 9;
                        ws.Cell("H" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell("H" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        var Causas = causaRiesgo.ObtenerCausaPorRiesgo(Convert.ToInt64(riesgo.NroRiesgo));
                        if (Causas.Count > 0)
                        {
                            int filaCausas = fila;
                            foreach (var causa in Causas)
                            {
                                ws.Cell("C" + filaCausas).Value = causa.cCausaDesc;
                                ws.Cell("C" + filaCausas).Style.Alignment.WrapText = true;
                                ws.Cell("C" + filaCausas).Style.Font.FontName = "Segoe UI";
                                ws.Cell("C" + filaCausas).Style.Font.FontSize = 9;
                                ws.Cell("C" + filaCausas).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("C" + filaCausas).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                filaCausas++;
                            }
                            matBucles[0] = filaCausas - 1;
                        }

                        ws.Range("A" + fila + ":" + "A" + matBucles.Max()).Merge();
                        ws.Range("B" + fila + ":" + "B" + matBucles.Max()).Merge();
                        ws.Range("D" + fila + ":" + "D" + matBucles.Max()).Merge();
                        ws.Range("E" + fila + ":" + "E" + matBucles.Max()).Merge();
                        ws.Range("F" + fila + ":" + "F" + matBucles.Max()).Merge();
                        ws.Range("G" + fila + ":" + "G" + matBucles.Max()).Merge();
                        ws.Range("H" + fila + ":" + "H" + matBucles.Max()).Merge();
                        ws.Range("I" + fila + ":" + "I" + matBucles.Max()).Merge();

                        fila = matBucles.Max();

                        fila++;
                    }
                    /***************************************** Fin Cuerpo ******************************************/
                    ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);

                    ws.Name = "Riesgo Operacionales";
                }
                return wb;
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ReportesIncentivos(int pnReporte, int pnTpoRiesgo, int pnMotivo = 0, decimal pnMonto = 0)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                var reporte = ireportes.ObtenerObjetoReporteIncentivo(pnReporte, pnTpoRiesgo, pnMotivo, pnMonto);

                var ReporteGenerado = GenerarReporteIncentivos(reporte);

                return new ExcelResult(ReporteGenerado, "Reporte Gestión de Incentivos [" + usuario.cUser + DateTime.Now.ToString("yyyyMMddhhmmss") + "]");
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }

        private XLWorkbook GenerarReporteIncentivos(List<dynamic> oListaRiesgo)
        {
            try
            {
                XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoIncentivos.xlsx"), XLEventTracking.Disabled);

                IXLWorksheet ws = wb.Worksheet(1);

                decimal TotalIncentivado = 0.0M;
                int fila = 5;

                if (oListaRiesgo.Count > 0)
                {
                    foreach (var riesgo in oListaRiesgo)
                    {

                        var detalleRiesgo = iriesgo.ObtenerInfoGeneralRiesgoOperacional(Convert.ToInt64(riesgo.nNroRiesgo));

                        ws.Cell("A" + fila).Value = detalleRiesgo.cCodRiesgo;
                        ws.Cell("A" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("A" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("A" + fila).Style.Font.FontSize = 9;
                        ws.Cell("A" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("A" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("A" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("B" + fila).Value = detalleRiesgo.cRiesgoIdentiticado;
                        ws.Cell("B" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("B" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("B" + fila).Style.Font.FontSize = 9;
                        ws.Cell("B" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("B" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                        ws.Cell("B" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("C" + fila).Value = detalleRiesgo.oUsuarios.cUser;
                        ws.Cell("C" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("C" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("C" + fila).Style.Font.FontSize = 9;
                        ws.Cell("C" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("C" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("C" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        var _inherente = iriesgo.ObtenerInfoGestionRiesgo(Convert.ToInt64(riesgo.nNroRiesgo), 2);
                        int valProbabilidad = Convert.ToInt32(_inherente[1]);
                        int valImpacto = Convert.ToInt32(_inherente[2]);
                        ws.Cell("D" + fila).Value = constante.ObtenerConstantes(1012).ElementAt(valProbabilidad).cConsDescripcion;
                        ws.Cell("D" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("D" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("D" + fila).Style.Font.FontSize = 9;
                        ws.Cell("D" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("D" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("D" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("E" + fila).Value = constante.ObtenerConstantes(1011).ElementAt(valImpacto).cConsDescripcion;
                        ws.Cell("E" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("E" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("E" + fila).Style.Font.FontSize = 9;
                        ws.Cell("E" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("E" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("E" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        var _valEscala = riesgoInherente.ObtenerNivelRiesgoInherente(valProbabilidad, valImpacto).nValorEscala;
                        ws.Cell("F" + fila).Value = ConstGeneral.Get.DescripcionNivelRiesgo(_valEscala);
                        ws.Cell("F" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("F" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("F" + fila).Style.Font.FontSize = 9;
                        ws.Cell("F" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("F" + fila).Style.Fill.BackgroundColor = XLColor.FromHtml(ConstGeneral.Get.ColorNivelRiesgo(_valEscala));
                        ws.Cell("F" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("F" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //ws.Cell("G" + fila).Value = montoPerdida.ObtenerMontoPerdida((int)_inherente[1], (int)_inherente[2]).cMontoPerdida;
                        //ws.Cell("G" + fila).Value = montoPerdida.ObtenerMontoPerdida(valProbabilidad, valImpacto).cMontoPerdida; //Adecuacion por mejoras en el proceso de registro de monto de perdida
                        ws.Cell("G" + fila).Value = montoPerdida.ObtenerMontoPerdida().Where(x => x.nProbabilidad == valProbabilidad && x.nImpacto == valImpacto && x.bEstado == true).FirstOrDefault().nMontoPerdida;
                        ws.Cell("G" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("G" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("G" + fila).Style.Font.FontSize = 9;
                        ws.Cell("G" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("G" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("G" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                        //Detalle de la gestion
                        var detalleGestion = incentivo.ObtenerDetalleIncentivo(Convert.ToInt64(riesgo.nNroRiesgo));
                        if (detalleGestion != null)
                        {

                            TotalIncentivado += (decimal)detalleGestion.nMontoIncentivo;
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

                        int filasUsadas = ws.LastRowUsed().RowNumber();

                        ws.Cell("I" + (filasUsadas + 1)).Value = TotalIncentivado;
                        ws.Cell("I" + (filasUsadas + 1)).Style.Alignment.WrapText = true;
                        ws.Cell("I" + (filasUsadas + 1)).Style.Font.FontName = "Segoe UI";
                        ws.Cell("I" + (filasUsadas + 1)).Style.Font.FontSize = 9;
                        ws.Cell("I" + (filasUsadas + 1)).Style.Font.Bold = true;
                        ws.Cell("I" + (filasUsadas + 1)).Style.Fill.BackgroundColor = XLColor.FromHtml("#E7E6E6");
                        ws.Cell("I" + (filasUsadas + 1)).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("I" + (filasUsadas + 1)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("I" + (filasUsadas + 1)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        fila++;
                    }

                }
                /*Otras configuraciones*/
                ws.Column("H").Delete();
                return wb;
            }
            catch { throw; }
        }

        #endregion

        #region Reporte Varios - Evaluaciones

        #endregion

        #region Reporte Varios - Evento de Perdida
        [RequiresAuthenticationAttribute]
        public ActionResult MatrizEventoValorFiltro(string psValFiltro, string psNombreCol = "ep.nMontoBruto")
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                var MatrizGenerado = GenerarMatrizEventoValorFiltro(psNombreCol, psValFiltro);
                string lsNombreDoc = "Matriz Evento Perdida";
                if (psValFiltro == ">=1000")
                {
                    lsNombreDoc += " Mayor igual a 1000";
                }
                else if (psValFiltro == "<1000")
                {
                    lsNombreDoc += " Menor a 1000";
                }

                return new ExcelResult(MatrizGenerado, String.Concat(lsNombreDoc, " [", usuario.cUser, DateTime.Now.ToString("yyyyMMddhhmmss"), "]"));
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }

        }

        private XLWorkbook GenerarMatrizEventoValorFiltro(string psNombreCol, string psValFiltro)
        {
            try
            {
                XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoMatrizEventoPerdida.xlsx"), XLEventTracking.Disabled);

                IXLWorksheet ws = wb.Worksheet(1); //Hoja 1
                ws.Name = psValFiltro;

                var oLsEventos = ireportes.ObtenerEventoPerdidaValorFiltro(psNombreCol, psValFiltro);

                if (oLsEventos.Count > 0)
                {

                    int fila = 4; //Fila de inicio
                    int[] matBucles = { 0, 0 }; /*(0) - Bucle Gastos, (1) - Bucle Causas*/

                    foreach (var evento in oLsEventos)
                    {
                        matBucles[0] = fila;

                        var detEvento = ievento.ObtenerDetalleEventoPerdida((long)Convert.ToInt64(evento.Evento));

                        ws.Cell("A" + fila).Value = detEvento.oDatosRiesgo.cCodRiesgo;
                        ws.Cell("A" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("A" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("A" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("A" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("A" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("A" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("B" + fila).Value = detEvento.oDatosRiesgo.cRiesgoIdentiticado;
                        ws.Cell("B" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("B" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("B" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("B" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("B" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                        ws.Cell("B" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("C" + fila).Value = detEvento.oClasesEventoPerdida.cDescClasEvento;
                        ws.Cell("C" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("C" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("C" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("C" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("C" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("C" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("D" + fila).Value = detEvento.oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento;
                        ws.Cell("D" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("D" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("D" + fila).Style.Font.FontSize = 9;
                        ws.Cell("D" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("D" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("D" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("E" + fila).Value = detEvento.oDatosRiesgo.oAgencias.cAgeDescripcion;
                        ws.Cell("E" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("E" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("E" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("E" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("E" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("E" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("F" + fila).Value = detEvento.oDatosRiesgo.oAreas.cAreaDescripcion;
                        ws.Cell("F" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("F" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("F" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("F" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("F" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("F" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("G" + fila).Value = detEvento.oLineaNeg.cDescLineaNeg;
                        ws.Cell("G" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("G" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("G" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("G" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("G" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("G" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("H" + fila).Value = detEvento.oLineaNeg.oSubLineaNeg.cDescSubLineaNeg;
                        ws.Cell("H" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("H" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("H" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("H" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("H" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("H" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("I" + fila).Value = detEvento.dFechaOcurrencia;
                        ws.Cell("I" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("I" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("I" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("I" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("I" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("I" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("J" + fila).Value = detEvento.dFechaDescubrimiento;
                        ws.Cell("J" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("J" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("J" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("J" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("J" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("J" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("K" + fila).Value = detEvento.dFechaRegCont;
                        ws.Cell("K" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("K" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("K" + fila).Style.Font.FontSize = 9;
                        ws.Cell("K" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("K" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("K" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("L" + fila).Value = detEvento.oDatosRiesgo.oProceso.cDescProceso;
                        ws.Cell("L" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("L" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("L" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("L" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("L" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("L" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("M" + fila).Value = detEvento.oDatosRiesgo.oProceso.oSubProceso.cDescSubProceso;
                        ws.Cell("M" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("M" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("M" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("M" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("M" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("M" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("N" + fila).Value = detEvento.nMontoBruto;
                        ws.Cell("N" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("N" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("N" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("N" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("N" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("N" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("O" + fila).Value = detEvento.cPenMontoRecup == "1" ? "S/" : "$";
                        ws.Cell("O" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("O" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("O" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("O" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("O" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("O" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("P" + fila).Value = detEvento.nMontoRecup;
                        ws.Cell("P" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("P" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("P" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("P" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("P" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("P" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        //Detalle de los gastos

                        var LstGastos = ievento.ObtenerDetGastosEventoPerdida((long)Convert.ToInt64(evento.Evento));
                        if (LstGastos.Count > 0)
                        {
                            int filaGasto = fila;
                            foreach (var gasto in LstGastos)
                            {
                                ws.Cell("Q" + filaGasto).Value = gasto.Moneda == 1 ? "S/" : "$";
                                ws.Cell("Q" + filaGasto).Style.Alignment.WrapText = true;
                                ws.Cell("Q" + filaGasto).Style.Font.FontName = "Segoe UI";
                                ws.Cell("Q" + filaGasto).Style.Font.FontSize = 9;
                                //ws.Cell("Q" + filaGasto).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                                ws.Cell("Q" + filaGasto).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("Q" + filaGasto).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("R" + filaGasto).Value = gasto.Monto;
                                ws.Cell("R" + filaGasto).Style.Alignment.WrapText = true;
                                ws.Cell("R" + filaGasto).Style.Font.FontName = "Segoe UI";
                                ws.Cell("R" + filaGasto).Style.Font.FontSize = 9;
                                ws.Cell("R" + filaGasto).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                                ws.Cell("R" + filaGasto).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("R" + filaGasto).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("S" + filaGasto).Value = gasto.Concepto;
                                ws.Cell("S" + filaGasto).Style.Alignment.WrapText = true;
                                ws.Cell("S" + filaGasto).Style.Font.FontName = "Segoe UI";
                                ws.Cell("S" + filaGasto).Style.Font.FontSize = 9;
                                //ws.Cell("S" + filaGasto).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                                ws.Cell("S" + filaGasto).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("S" + filaGasto).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                filaGasto++;
                            }
                            matBucles[0] = filaGasto - 1;
                        }

                        ws.Cell("T" + fila).Value = detEvento.cPenMontoProvision == "1" ? "S/" : "$";
                        ws.Cell("T" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("T" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("T" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("T" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("T" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("T" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("U" + fila).Value = detEvento.nMontoProvision;
                        ws.Cell("U" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("U" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("U" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("U" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("U" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("U" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Cell("V" + fila).Value = detEvento.cCtaContDesc;
                        ws.Cell("V" + fila).Style.Alignment.WrapText = true;
                        ws.Cell("V" + fila).Style.Font.FontName = "Segoe UI";
                        ws.Cell("V" + fila).Style.Font.FontSize = 9;
                        //ws.Cell("V" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell("V" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell("V" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        ws.Range("A" + fila + ":" + "A" + matBucles.Max()).Merge();
                        ws.Range("B" + fila + ":" + "B" + matBucles.Max()).Merge();
                        ws.Range("C" + fila + ":" + "C" + matBucles.Max()).Merge();
                        ws.Range("D" + fila + ":" + "D" + matBucles.Max()).Merge();
                        ws.Range("E" + fila + ":" + "E" + matBucles.Max()).Merge();
                        ws.Range("F" + fila + ":" + "F" + matBucles.Max()).Merge();
                        ws.Range("G" + fila + ":" + "G" + matBucles.Max()).Merge();
                        ws.Range("H" + fila + ":" + "H" + matBucles.Max()).Merge();
                        ws.Range("I" + fila + ":" + "I" + matBucles.Max()).Merge();
                        ws.Range("J" + fila + ":" + "J" + matBucles.Max()).Merge();
                        ws.Range("K" + fila + ":" + "K" + matBucles.Max()).Merge();
                        ws.Range("L" + fila + ":" + "L" + matBucles.Max()).Merge();
                        ws.Range("M" + fila + ":" + "M" + matBucles.Max()).Merge();
                        ws.Range("N" + fila + ":" + "N" + matBucles.Max()).Merge();
                        ws.Range("O" + fila + ":" + "O" + matBucles.Max()).Merge();
                        ws.Range("P" + fila + ":" + "P" + matBucles.Max()).Merge();
                        ws.Range("T" + fila + ":" + "T" + matBucles.Max()).Merge();
                        ws.Range("U" + fila + ":" + "U" + matBucles.Max()).Merge();
                        ws.Range("V" + fila + ":" + "V" + matBucles.Max()).Merge();

                        fila = matBucles.Max();

                        fila++;
                    }
                    /***************************************** Fin Cuerpo ******************************************/
                    ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                }
                return wb;
            }
            catch { throw; }
        }

        [RequiresAuthenticationAttribute]
        public ActionResult MatrizEventoConsolidado()
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                var MatrizGenerado = GenerarMatrizEventoConsolidado();

                return new ExcelResult(MatrizGenerado, String.Concat("Matriz Evento Perdida Consolidado [", usuario.cUser, DateTime.Now.ToString("yyyyMMddhhmmss"), "]"));
            }
            catch
            {
                var error = Utils.Error.GetError.GetErrorModel("Error Encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer experimentamos algún problema con el aplicativo.");
                return View("Error", error);
            }
        }
        private XLWorkbook GenerarMatrizEventoConsolidado()
        {
            try
            {
                XLWorkbook wb = new XLWorkbook(Server.MapPath("~/Plantillas/FormatoMatrizEventoPerdidaConsolidado.xlsx"), XLEventTracking.Disabled);

                List<dynamic> LsEventos;
                IXLWorksheet ws;
                for (int i = 1; i <= 2; i++) //usamos las dos primeras hojas
                {
                    ws = wb.Worksheet(i);
                    LsEventos = ireportes.ObtenerEventoPerdidaValorFiltro("ep.nMontoBruto", i == 1 ? ">=1000" : "<1000");
                    if (LsEventos.Count > 0)
                    {

                        int fila = 4; //Fila de inicio
                        int[] matBucles = { 0, 0 }; /*(0) - Bucle Gastos, (1) - Bucle Causas*/

                        foreach (var evento in LsEventos)
                        {
                            matBucles[0] = fila;

                            var detEvento = ievento.ObtenerDetalleEventoPerdida((long)Convert.ToInt64(evento.Evento));

                            ws.Cell("A" + fila).Value = detEvento.oDatosRiesgo.cCodRiesgo;
                            ws.Cell("A" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("A" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("A" + fila).Style.Font.FontSize = 9;
                            ws.Cell("A" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("A" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("B" + fila).Value = detEvento.oDatosRiesgo.cRiesgoIdentiticado;
                            ws.Cell("B" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("B" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("B" + fila).Style.Font.FontSize = 9;
                            ws.Cell("B" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                            ws.Cell("B" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("C" + fila).Value = detEvento.cMedidasCorrectivas; ;
                            ws.Cell("C" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("C" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("C" + fila).Style.Font.FontSize = 9;
                            ws.Cell("C" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                            ws.Cell("C" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("D" + fila).Value = detEvento.cAccionesRealizada;
                            ws.Cell("D" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("D" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("D" + fila).Style.Font.FontSize = 9;
                            ws.Cell("D" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                            ws.Cell("D" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("E" + fila).Value = detEvento.oDescCortaEventoPerdida.cConsDescripcion;
                            ws.Cell("E" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("E" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("E" + fila).Style.Font.FontSize = 9;
                            ws.Cell("E" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("E" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("F" + fila).Value = detEvento.oDatosRiesgo.oAgencias.cAgeDescripcion;
                            ws.Cell("F" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("F" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("F" + fila).Style.Font.FontSize = 9;
                            ws.Cell("F" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("F" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("G" + fila).Value = detEvento.oDatosRiesgo.oAreas.cAreaDescripcion;
                            ws.Cell("G" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("G" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("G" + fila).Style.Font.FontSize = 9;
                            ws.Cell("G" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("G" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("H" + fila).Value = detEvento.dFechaOcurrencia;
                            ws.Cell("H" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("H" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("H" + fila).Style.Font.FontSize = 9;
                            ws.Cell("H" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("H" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("I" + fila).Value = detEvento.dFechaDescubrimiento;
                            ws.Cell("I" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("I" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("I" + fila).Style.Font.FontSize = 9;
                            ws.Cell("I" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("I" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("J" + fila).Value = detEvento.dFechaRegCont;
                            ws.Cell("J" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("J" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("J" + fila).Style.Font.FontSize = 9;
                            ws.Cell("J" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("J" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("K" + fila).Value = detEvento.cAnio;
                            ws.Cell("K" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("K" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("K" + fila).Style.Font.FontSize = 9;
                            ws.Cell("K" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            ws.Cell("K" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("K" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("L" + fila).Value = detEvento.oClasesEventoPerdida.cDescClasEvento;
                            ws.Cell("L" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("L" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("L" + fila).Style.Font.FontSize = 9;
                            ws.Cell("L" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("L" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("M" + fila).Value = detEvento.oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento;
                            ws.Cell("M" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("M" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("M" + fila).Style.Font.FontSize = 9;
                            ws.Cell("M" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("M" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("N" + fila).Value = detEvento.bAsociaRiesgo ? "SI" : "NO";
                            ws.Cell("N" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("N" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("N" + fila).Style.Font.FontSize = 9;
                            ws.Cell("N" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("N" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("O" + fila).Value = detEvento.oDatosRiesgo.oProceso.cDescProceso;
                            ws.Cell("O" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("O" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("O" + fila).Style.Font.FontSize = 9;
                            ws.Cell("O" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("O" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("P" + fila).Value = detEvento.oDatosRiesgo.oProceso.oSubProceso.cDescSubProceso;
                            ws.Cell("P" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("P" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("P" + fila).Style.Font.FontSize = 9;
                            ws.Cell("P" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("P" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("Q" + fila).Value = detEvento.oLineaNeg.cDescLineaNeg;
                            ws.Cell("Q" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("Q" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("Q" + fila).Style.Font.FontSize = 9;
                            ws.Cell("Q" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("Q" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("R" + fila).Value = detEvento.oLineaNeg.oSubLineaNeg.cDescSubLineaNeg;
                            ws.Cell("R" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("R" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("R" + fila).Style.Font.FontSize = 9;
                            ws.Cell("R" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("R" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("S" + fila).Value = detEvento.oCobertura.cConsDescripcion;
                            ws.Cell("S" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("S" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("S" + fila).Style.Font.FontSize = 9;
                            ws.Cell("S" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("S" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("T" + fila).Value = detEvento.nMontoBruto;
                            ws.Cell("T" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("T" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("T" + fila).Style.Font.FontSize = 9;
                            ws.Cell("T" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("T" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("U" + fila).Value = detEvento.nPerdidaNeta;
                            ws.Cell("U" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("U" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("U" + fila).Style.Font.FontSize = 9;
                            ws.Cell("U" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("U" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("V" + fila).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(detEvento.cPenMontoPerdida));
                            ws.Cell("V" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("V" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("V" + fila).Style.Font.FontSize = 9;
                            ws.Cell("V" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("V" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("W" + fila).Value = detEvento.nMontoPerdida;
                            ws.Cell("W" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("W" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("W" + fila).Style.Font.FontSize = 9;
                            ws.Cell("W" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("W" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("X" + fila).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(detEvento.cPenMontoRecup));
                            ws.Cell("X" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("X" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("X" + fila).Style.Font.FontSize = 9;
                            ws.Cell("X" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("X" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("Y" + fila).Value = detEvento.nMontoRecup;
                            ws.Cell("Y" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("Y" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("Y" + fila).Style.Font.FontSize = 9;
                            ws.Cell("Y" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("Y" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("Z" + fila).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(detEvento.cPenMontoProvision));
                            ws.Cell("Z" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("Z" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("Z" + fila).Style.Font.FontSize = 9;
                            ws.Cell("Z" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("Z" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("AA" + fila).Value = detEvento.nMontoProvision;
                            ws.Cell("AA" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("AA" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("AA" + fila).Style.Font.FontSize = 9;
                            ws.Cell("AA" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("AA" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("AG" + fila).Value = detEvento.cUserRegistra;
                            ws.Cell("AG" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("AG" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("AG" + fila).Style.Font.FontSize = 9;
                            ws.Cell("AG" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("AG" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            ws.Cell("AH" + fila).Value = detEvento.dFechaRegistro;
                            ws.Cell("AH" + fila).Style.Alignment.WrapText = true;
                            ws.Cell("AH" + fila).Style.Font.FontName = "Segoe UI";
                            ws.Cell("AH" + fila).Style.Font.FontSize = 9;
                            ws.Cell("AH" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell("AH" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            //Detalle de los gastos
                            var LstGastos = ievento.ObtenerDetGastosEventoPerdida((long)evento.Evento);
                            if (LstGastos.Count > 0)
                            {
                                int filaGasto = fila;
                                foreach (var gasto in LstGastos)
                                {
                                    ws.Cell("AB" + filaGasto).Value = ConstGeneral.Get.PenSimbolo((int)gasto.Moneda);
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
                                    ws.Cell("AD" + filaGasto).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    ws.Cell("AD" + filaGasto).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                    filaGasto++;
                                }
                                matBucles[0] = filaGasto - 1;
                            }

                            //Detalle de las cuentas contables
                            var LstCtaCont = ievento.ObtenerDetCtaContablesEventoPerdida((long)evento.Evento);
                            if (LstCtaCont.Count > 0)
                            {
                                int filaCta = fila;
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

                            ws.Range("A" + fila + ":" + "A" + matBucles.Max()).Merge();
                            ws.Range("B" + fila + ":" + "B" + matBucles.Max()).Merge();
                            ws.Range("C" + fila + ":" + "C" + matBucles.Max()).Merge();
                            ws.Range("D" + fila + ":" + "D" + matBucles.Max()).Merge();
                            ws.Range("E" + fila + ":" + "E" + matBucles.Max()).Merge();
                            ws.Range("F" + fila + ":" + "F" + matBucles.Max()).Merge();
                            ws.Range("G" + fila + ":" + "G" + matBucles.Max()).Merge();
                            ws.Range("H" + fila + ":" + "H" + matBucles.Max()).Merge();
                            ws.Range("I" + fila + ":" + "I" + matBucles.Max()).Merge();
                            ws.Range("J" + fila + ":" + "J" + matBucles.Max()).Merge();
                            ws.Range("K" + fila + ":" + "K" + matBucles.Max()).Merge();
                            ws.Range("L" + fila + ":" + "L" + matBucles.Max()).Merge();
                            ws.Range("M" + fila + ":" + "M" + matBucles.Max()).Merge();
                            ws.Range("N" + fila + ":" + "N" + matBucles.Max()).Merge();
                            ws.Range("O" + fila + ":" + "O" + matBucles.Max()).Merge();
                            ws.Range("P" + fila + ":" + "P" + matBucles.Max()).Merge();
                            ws.Range("Q" + fila + ":" + "Q" + matBucles.Max()).Merge();
                            ws.Range("R" + fila + ":" + "R" + matBucles.Max()).Merge();
                            ws.Range("S" + fila + ":" + "S" + matBucles.Max()).Merge();
                            ws.Range("T" + fila + ":" + "T" + matBucles.Max()).Merge();
                            ws.Range("U" + fila + ":" + "U" + matBucles.Max()).Merge();
                            ws.Range("V" + fila + ":" + "V" + matBucles.Max()).Merge();
                            ws.Range("W" + fila + ":" + "W" + matBucles.Max()).Merge();
                            ws.Range("X" + fila + ":" + "X" + matBucles.Max()).Merge();
                            ws.Range("Y" + fila + ":" + "Y" + matBucles.Max()).Merge();
                            ws.Range("Z" + fila + ":" + "Z" + matBucles.Max()).Merge();
                            ws.Range("AA" + fila + ":" + "AA" + matBucles.Max()).Merge();
                            ws.Range("AG" + fila + ":" + "AG" + matBucles.Max()).Merge();
                            ws.Range("AH" + fila + ":" + "AH" + matBucles.Max()).Merge();

                            ws.Range("AB" + matBucles[0] + ":" + "AB" + matBucles.Max()).Merge();
                            ws.Range("AC" + matBucles[0] + ":" + "AC" + matBucles.Max()).Merge();
                            ws.Range("AD" + matBucles[0] + ":" + "AD" + matBucles.Max()).Merge();

                            ws.Range("AE" + matBucles[1] + ":" + "AE" + matBucles.Max()).Merge();
                            ws.Range("AF" + matBucles[1] + ":" + "AF" + matBucles.Max()).Merge();

                            fila = matBucles.Max();

                            fila++;
                        }
                        /***************************************** Fin Cuerpo ******************************************/
                        ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                    }
                }

                ws = wb.Worksheet(3); //usamos la tercera hoja
                var LstEventoGrupo = ievento.ObtenerEventoPerdidaAgrupados();
                if (LstEventoGrupo.Count > 0)
                {

                    int fila = 4; //Fila de inicio
                    int[] matBucles = { 0, 0, 0 }; /*(0) - Bucle Gastos, (1) - Bucle Causas*/
                    string tmpEvento = "";
                    foreach (var grupo in LstEventoGrupo)
                    {

                        var LstAgrupado = ievento.ObtenerAgrupacionEvento(grupo.oDatosRiesgo.nNroRiesgo);
                        if (LstAgrupado.Count > 0)
                        {
                            foreach (var agrupado in LstAgrupado)
                            {
                                matBucles[0] = fila;

                                var detEvento = ievento.ObtenerDetalleEventoPerdida(agrupado.oDatosRiesgo.nNroRiesgo);

                                if (tmpEvento != grupo.oDatosRiesgo.cCodRiesgo)
                                {
                                    tmpEvento = grupo.oDatosRiesgo.cCodRiesgo;
                                    matBucles[2] = fila;
                                }


                                ws.Cell("A" + fila).Value = detEvento.cGrupoEvento;
                                ws.Cell("A" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("A" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("A" + fila).Style.Font.FontSize = 9;
                                ws.Cell("A" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("A" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("B" + fila).Value = Convert.ToInt32(detEvento.oDatosRiesgo.cCodRiesgo);
                                ws.Cell("B" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("B" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("B" + fila).Style.Font.FontSize = 9;
                                ws.Cell("B" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("B" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("C" + fila).Value = detEvento.oDatosRiesgo.cRiesgoIdentiticado;
                                ws.Cell("C" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("C" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("C" + fila).Style.Font.FontSize = 9;
                                ws.Cell("C" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                                ws.Cell("C" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("D" + fila).Value = detEvento.cMedidasCorrectivas; ;
                                ws.Cell("D" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("D" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("D" + fila).Style.Font.FontSize = 9;
                                ws.Cell("D" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                                ws.Cell("D" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("E" + fila).Value = detEvento.cAccionesRealizada;
                                ws.Cell("E" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("E" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("E" + fila).Style.Font.FontSize = 9;
                                ws.Cell("E" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                                ws.Cell("E" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("F" + fila).Value = detEvento.oDescCortaEventoPerdida.cConsDescripcion;
                                ws.Cell("F" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("F" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("F" + fila).Style.Font.FontSize = 9;
                                ws.Cell("F" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("F" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("G" + fila).Value = detEvento.oDatosRiesgo.oAgencias.cAgeDescripcion;
                                ws.Cell("G" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("G" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("G" + fila).Style.Font.FontSize = 9;
                                ws.Cell("G" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("G" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("H" + fila).Value = detEvento.oDatosRiesgo.oAreas.cAreaDescripcion;
                                ws.Cell("H" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("H" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("H" + fila).Style.Font.FontSize = 9;
                                ws.Cell("H" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("H" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("I" + fila).Value = detEvento.dFechaOcurrencia;
                                ws.Cell("I" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("I" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("I" + fila).Style.Font.FontSize = 9;
                                ws.Cell("I" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("I" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("J" + fila).Value = detEvento.dFechaDescubrimiento;
                                ws.Cell("J" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("J" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("J" + fila).Style.Font.FontSize = 9;
                                ws.Cell("J" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("J" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("K" + fila).Value = detEvento.dFechaRegCont;
                                ws.Cell("K" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("K" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("K" + fila).Style.Font.FontSize = 9;
                                ws.Cell("K" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("K" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("L" + fila).Value = detEvento.cAnio;
                                ws.Cell("L" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("L" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("L" + fila).Style.Font.FontSize = 9;
                                ws.Cell("L" + fila).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                                ws.Cell("L" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("L" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("M" + fila).Value = detEvento.oClasesEventoPerdida.cDescClasEvento;
                                ws.Cell("M" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("M" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("M" + fila).Style.Font.FontSize = 9;
                                ws.Cell("M" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("M" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("N" + fila).Value = detEvento.oClasesEventoPerdida.oSubClaseEventoPerdida.cDescSubClasEvento;
                                ws.Cell("N" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("N" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("N" + fila).Style.Font.FontSize = 9;
                                ws.Cell("N" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("N" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("O" + fila).Value = detEvento.bAsociaRiesgo ? "SI" : "NO";
                                ws.Cell("O" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("O" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("O" + fila).Style.Font.FontSize = 9;
                                ws.Cell("O" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("O" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("P" + fila).Value = detEvento.oDatosRiesgo.oProceso.cDescProceso;
                                ws.Cell("P" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("P" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("P" + fila).Style.Font.FontSize = 9;
                                ws.Cell("P" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("P" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("Q" + fila).Value = detEvento.oDatosRiesgo.oProceso.oSubProceso.cDescSubProceso;
                                ws.Cell("Q" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("Q" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("Q" + fila).Style.Font.FontSize = 9;
                                ws.Cell("Q" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("Q" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("R" + fila).Value = detEvento.oLineaNeg.cDescLineaNeg;
                                ws.Cell("R" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("R" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("R" + fila).Style.Font.FontSize = 9;
                                ws.Cell("R" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("R" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("S" + fila).Value = detEvento.oLineaNeg.oSubLineaNeg.cDescSubLineaNeg;
                                ws.Cell("S" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("S" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("S" + fila).Style.Font.FontSize = 9;
                                ws.Cell("S" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("S" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("T" + fila).Value = detEvento.oCobertura.cConsDescripcion;
                                ws.Cell("T" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("T" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("T" + fila).Style.Font.FontSize = 9;
                                ws.Cell("T" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("T" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("U" + fila).Value = detEvento.nMontoBruto;
                                ws.Cell("U" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("U" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("U" + fila).Style.Font.FontSize = 9;
                                ws.Cell("U" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("U" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("V" + fila).Value = detEvento.nPerdidaNeta;
                                ws.Cell("V" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("V" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("V" + fila).Style.Font.FontSize = 9;
                                ws.Cell("V" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("V" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("W" + fila).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(detEvento.cPenMontoPerdida));
                                ws.Cell("W" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("W" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("W" + fila).Style.Font.FontSize = 9;
                                ws.Cell("W" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("W" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("X" + fila).Value = detEvento.nMontoPerdida;
                                ws.Cell("X" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("X" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("X" + fila).Style.Font.FontSize = 9;
                                ws.Cell("X" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("X" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("Y" + fila).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(detEvento.cPenMontoRecup));
                                ws.Cell("Y" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("Y" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("Y" + fila).Style.Font.FontSize = 9;
                                ws.Cell("Y" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("Y" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("Z" + fila).Value = detEvento.nMontoRecup;
                                ws.Cell("Z" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("Z" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("Z" + fila).Style.Font.FontSize = 9;
                                ws.Cell("Z" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("Z" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("AA" + fila).Value = ConstGeneral.Get.PenSimbolo(Convert.ToInt32(detEvento.cPenMontoProvision));
                                ws.Cell("AA" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("AA" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("AA" + fila).Style.Font.FontSize = 9;
                                ws.Cell("AA" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("AA" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("AB" + fila).Value = detEvento.nMontoProvision;
                                ws.Cell("AB" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("AB" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("AB" + fila).Style.Font.FontSize = 9;
                                ws.Cell("AB" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("AB" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("AH" + fila).Value = detEvento.cUserRegistra;
                                ws.Cell("AH" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("AH" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("AH" + fila).Style.Font.FontSize = 9;
                                ws.Cell("AH" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("AH" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                ws.Cell("AI" + fila).Value = detEvento.dFechaRegistro;
                                ws.Cell("AI" + fila).Style.Alignment.WrapText = true;
                                ws.Cell("AI" + fila).Style.Font.FontName = "Segoe UI";
                                ws.Cell("AI" + fila).Style.Font.FontSize = 9;
                                ws.Cell("AI" + fila).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Cell("AI" + fila).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                //Detalle de los gastos
                                var LstGastos = ievento.ObtenerDetGastosEventoPerdida(agrupado.oDatosRiesgo.nNroRiesgo);
                                if (LstGastos.Count > 0)
                                {
                                    int filaGasto = fila;
                                    foreach (var gasto in LstGastos)
                                    {
                                        ws.Cell("AC" + filaGasto).Value = ConstGeneral.Get.PenSimbolo((int)gasto.Moneda);
                                        ws.Cell("AC" + filaGasto).Style.Alignment.WrapText = true;
                                        ws.Cell("AC" + filaGasto).Style.Font.FontName = "Segoe UI";
                                        ws.Cell("AC" + filaGasto).Style.Font.FontSize = 9;
                                        ws.Cell("AC" + filaGasto).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        ws.Cell("AC" + filaGasto).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                        ws.Cell("AD" + filaGasto).Value = gasto.Concepto;
                                        ws.Cell("AD" + filaGasto).Style.Alignment.WrapText = true;
                                        ws.Cell("AD" + filaGasto).Style.Font.FontName = "Segoe UI";
                                        ws.Cell("AD" + filaGasto).Style.Font.FontSize = 9;
                                        ws.Cell("AD" + filaGasto).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                        ws.Cell("AD" + filaGasto).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                        ws.Cell("AE" + filaGasto).Value = gasto.Monto;
                                        ws.Cell("AE" + filaGasto).Style.Alignment.WrapText = true;
                                        ws.Cell("AE" + filaGasto).Style.Font.FontName = "Segoe UI";
                                        ws.Cell("AE" + filaGasto).Style.Font.FontSize = 9;
                                        ws.Cell("AE" + filaGasto).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                                        ws.Cell("AE" + filaGasto).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                        filaGasto++;
                                    }
                                    matBucles[0] = filaGasto - 1;
                                }

                                //Detalle de las cuentas contables
                                var LstCtaCont = ievento.ObtenerDetCtaContablesEventoPerdida(agrupado.oDatosRiesgo.nNroRiesgo);
                                if (LstCtaCont.Count > 0)
                                {
                                    int filaCta = fila;
                                    foreach (var cuenta in LstCtaCont)
                                    {
                                        ws.Cell("AF" + filaCta).Value = cuenta.Codigo;
                                        ws.Cell("AF" + filaCta).Style.Font.FontName = "Segoe UI";
                                        ws.Cell("AF" + filaCta).Style.Font.FontSize = 9;
                                        ws.Cell("AF" + filaCta).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        ws.Cell("AF" + filaCta).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                        ws.Cell("AG" + filaCta).Value = cuenta.cCtaContDesc.Substring(Convert.ToInt32(cuenta.cCtaContDesc.IndexOf("]")) + 1);
                                        ws.Cell("AG" + filaCta).Style.Font.FontName = "Segoe UI";
                                        ws.Cell("AG" + filaCta).Style.Font.FontSize = 9;
                                        ws.Cell("AG" + filaCta).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                        ws.Cell("AG" + filaCta).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                        filaCta++;
                                    }
                                    matBucles[1] = filaCta - 1;
                                }

                                ws.Range("A" + matBucles[2] + ":" + "A" + matBucles.Max()).Merge();
                                ws.Range("B" + fila + ":" + "B" + matBucles.Max()).Merge();
                                ws.Range("C" + fila + ":" + "C" + matBucles.Max()).Merge();
                                ws.Range("D" + fila + ":" + "D" + matBucles.Max()).Merge();
                                ws.Range("E" + fila + ":" + "E" + matBucles.Max()).Merge();
                                ws.Range("F" + fila + ":" + "F" + matBucles.Max()).Merge();
                                ws.Range("G" + fila + ":" + "G" + matBucles.Max()).Merge();
                                ws.Range("H" + fila + ":" + "H" + matBucles.Max()).Merge();
                                ws.Range("I" + fila + ":" + "I" + matBucles.Max()).Merge();
                                ws.Range("J" + fila + ":" + "J" + matBucles.Max()).Merge();
                                ws.Range("K" + fila + ":" + "K" + matBucles.Max()).Merge();
                                ws.Range("L" + fila + ":" + "L" + matBucles.Max()).Merge();
                                ws.Range("M" + fila + ":" + "M" + matBucles.Max()).Merge();
                                ws.Range("N" + fila + ":" + "N" + matBucles.Max()).Merge();
                                ws.Range("O" + fila + ":" + "O" + matBucles.Max()).Merge();
                                ws.Range("P" + fila + ":" + "P" + matBucles.Max()).Merge();
                                ws.Range("Q" + fila + ":" + "Q" + matBucles.Max()).Merge();
                                ws.Range("R" + fila + ":" + "R" + matBucles.Max()).Merge();
                                ws.Range("S" + fila + ":" + "S" + matBucles.Max()).Merge();
                                ws.Range("T" + fila + ":" + "T" + matBucles.Max()).Merge();
                                ws.Range("U" + fila + ":" + "U" + matBucles.Max()).Merge();
                                ws.Range("V" + fila + ":" + "V" + matBucles.Max()).Merge();
                                ws.Range("W" + fila + ":" + "W" + matBucles.Max()).Merge();
                                ws.Range("X" + fila + ":" + "X" + matBucles.Max()).Merge();
                                ws.Range("Y" + fila + ":" + "Y" + matBucles.Max()).Merge();
                                ws.Range("Z" + fila + ":" + "Z" + matBucles.Max()).Merge();
                                ws.Range("AA" + fila + ":" + "AA" + matBucles.Max()).Merge();
                                ws.Range("AB" + fila + ":" + "AB" + matBucles.Max()).Merge();
                                ws.Range("AH" + fila + ":" + "AH" + matBucles.Max()).Merge();
                                ws.Range("AI" + fila + ":" + "AI" + matBucles.Max()).Merge();

                                ws.Range("AC" + matBucles[0] + ":" + "AC" + matBucles.Max()).Merge();
                                ws.Range("AD" + matBucles[0] + ":" + "AD" + matBucles.Max()).Merge();
                                ws.Range("AE" + matBucles[0] + ":" + "AE" + matBucles.Max()).Merge();

                                ws.Range("AI" + matBucles[1] + ":" + "AI" + matBucles.Max()).Merge();
                                ws.Range("AI" + matBucles[1] + ":" + "AI" + matBucles.Max()).Merge();

                                fila = matBucles.Max();

                                fila++;

                            }

                            /***************************************** Fin Cuerpo ******************************************/
                            ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            ws.Range("A4:" + ws.LastCellUsed().Address.ToString()).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                        }
                    }


                }

                return wb;
            }
            catch { throw; }
        }
        #endregion

        #endregion

        #region Informes de Power Bi
        //[RequiresAuthenticationAttribute]
        //public ActionResult PBEventoPerdida()
        //{
        //    try
        //    {
        //        //var userName = ClaimsPrincipal.Current.FindFirst("name").Value;
        //        var accessToken = TokenManager.GetAccessToken(PowerBIPermissionScopes.ReadUserWorkspaces);
        //        AuthDetails authDetails = new AuthDetails
        //        {
        //            //UserName = userName,
        //            UserName = "",
        //            AccessToken = accessToken
        //        };
        //        //return View("PBEventoPerdida", authDetails);
        //        return View(authDetails);
        //    }
        //    catch //(Exception ex)
        //    {
        //        var error = Utils.Error.GetError.GetErrorModel("Error encontrado", (HttpStatusCode)500, "¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo.");
        //        return View("Error", error);
        //    }
        //}

        [RequiresAuthenticationAttribute]
        public async Task<ActionResult> DashboardPBI()
        {
            try
            {
                var validaConfig = ConfigValidatorService.GetWebConfigErrors();
                if (!validaConfig.IsNullOrWhiteSpace())
                {
                    var error = Utils.Error.GetError.GetErrorModel("Error Configuración", (HttpStatusCode)500, validaConfig);
                    return View("Error", error);
                }

                var embedResult = await EmbedService.GetEmbedParams(ConfigValidatorService.WorkspaceId, ConfigValidatorService.ReportId);
                return View(embedResult);
            }
            catch (HttpOperationException ex)
            {
                var error = Utils.Error.GetError.GetErrorModel("Power BI", (HttpStatusCode)505, string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", ex.Response.StatusCode, (int)ex.Response.StatusCode, ex.Response.Content, ex.Response.Headers["RequestId"].FirstOrDefault()));
                return View("Error", error);
            }
            catch (Exception ex)
            {
                var error = Utils.Error.GetError.GetErrorModel("Power BI", (HttpStatusCode)505, ex.Message /*"¡Lo sentimos! Al parecer encontramos con algún problema en el aplicativo."*/);
                return View("Error", error);
            }
        }


        #endregion


    }
}