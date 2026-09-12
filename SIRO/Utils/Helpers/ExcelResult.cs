using ClosedXML.Excel;
using System.IO;
using System.Web.Mvc;

namespace SIRO.Utils.Helpers
{
    public class ExcelResult:ActionResult {

        private readonly XLWorkbook workbook;
        private readonly string NombreArchivo;


        public ExcelResult(XLWorkbook workbook, string nombrearchivo)
        {
            this.workbook = workbook;
            NombreArchivo = nombrearchivo;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.ContentType = "application/vnd.openxmlformats-officedocument."
                                 + "spreadsheetml.sheet";
            response.AddHeader("content-disposition",
                               "attachment;filename=\"" + NombreArchivo + ".xlsx\"");

            using (var memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(response.OutputStream);
            }
            response.End();
        }
    }
}