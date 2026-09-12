using System;

namespace SIRO.Utils.Constantes
{
    public class ConstGeneral
    {
        private ConstGeneral() { }

        private static ConstGeneral instancia;

        public static ConstGeneral Get
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ConstGeneral();
                }
                return instancia;
            }
        }

        string CodigoAnalista = "005011";
        string DescripcionProceso0 = "Riesgo Registrado";
        string DescripcionProceso1 = "Identificación del Riesgo";
        string DescripcionProceso2 = "Nivel de Riesgo Inherente";
        string DescripcionProceso3 = "Nivel de Riesgo Residual";
        string DescripcionProceso4 = "Planes de Acción";
        string DescripcionProceso5 = "Responsables de Planes de Acción";

        public string AnalistaRO
        {
            get { return CodigoAnalista; }
        }
        public string DescProceso0
        {
            get { return DescripcionProceso0; }
        }
        public string DescProceso1
        {
            get { return DescripcionProceso1; }
        }
        public string DescProceso2
        {
            get { return DescripcionProceso2; }
        }
        public string DescProceso3
        {
            get { return DescripcionProceso3; }
        }
        public string DescProceso4
        {
            get { return DescripcionProceso4; }
        }
        public string DescProceso5
        {
            get { return DescripcionProceso5; }
        }

        public string PenDescripcion(int tipo)
        {
            string moneda = "";
            string[] PenMontos = { "Error Valor", "SOLES", "DOLARES" };
            moneda = Convert.ToString(PenMontos[tipo]);
            return moneda;
        }

        public string PenSimbolo(int tipo)
        {
            string moneda = "";
            string[] PenMontos = { "Error Valor", "S/", "$" };
            moneda = Convert.ToString(PenMontos[tipo]);
            return moneda;
        }
        
        public string ColorNivelRiesgo(int valor)
        {
            string _return = "";
            string[] Color = { "", "#1FAA00", "#FFEB3B", "#FF6D00", "#D50000" };
            _return = Convert.ToString(Color[valor]);
            return _return;
        }

        public string DescripcionNivelRiesgo(int valor)
        {
            string _return = "";
            string[] Descripcion = { "", "Bajo", "Moderado", "Alto", "Extremo" };
            _return = Convert.ToString(Descripcion[valor]);
            return _return;
        }


        public string Correo(string psTitulo, string psContenido)
        {
            string lsHTML = "";
            lsHTML += "<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>";
            lsHTML += "<html xmlns='http://www.w3.org/1999/xhtml'>";
            lsHTML += "<head>";
            lsHTML += "<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />";
            lsHTML += "<title>SIRO</title>";
            lsHTML += "<meta name='viewport' content='width=device-width, initial-scale=1.0'/>";
            lsHTML += "</head>";
            lsHTML += "<body style='margin: 0; padding: 0;'>";
            lsHTML += "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";
            lsHTML += "<tr>";
            lsHTML += "<td style='padding: 10px 0 30px 0;'>";
            lsHTML += "<table align='center' border='0' cellpadding='0' cellspacing='0' width='600' style='border: 1px solid #cccccc; border-collapse: collapse;'>";
            lsHTML += "<tr>";
            lsHTML += "<td align='center' bgcolor='#fff' style='padding: 40px 0 30px 0; color: #153643; font-size: 28px; font-weight: bold; font-family: Arial, sans-serif;'>";
            lsHTML += "<img src='cid:logocaja'  alt='Logo SIRO' width='200' height='200' style='display: block;' />";
            lsHTML += "</td>";
            lsHTML += "</tr>";
            lsHTML += "<tr>";
            lsHTML += "<td bgcolor='#ffffff' style='padding: 40px 30px 40px 30px;'>";
            lsHTML += "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";
            lsHTML += "<tr>";
            lsHTML += "<td style='color: #153643; font-family: Arial, sans-serif; font-size: 16px;'>";
            lsHTML += "<b>" + psTitulo + "</b>";
            lsHTML += "</td>";
            lsHTML += "</tr>";
            lsHTML += "<tr>";
            lsHTML += "<td style='padding: 10px 0 20px 0; color: #153643; font-family: Arial, sans-serif; font-size: 12px; line-height: 20px;'>" + psContenido;
            lsHTML += "</td>";
            lsHTML += "</tr>";
            lsHTML += "</table>";
            lsHTML += "</td>";
            lsHTML += "</tr>";
            lsHTML += "<tr>";
            lsHTML += "<td bgcolor='#065071' style = 'padding: 30px 30px 30px 30px;'>";
            lsHTML += "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";
            lsHTML += "<tr>";
            lsHTML += "<td style='color: #F6F7F9; font-family: Arial, sans-serif; font-size: 11.5px;' width='75 %'>";
            lsHTML += "Sistema de Gestión de Riesgo Operacional - SIRO<br/>";
            lsHTML += "</td>";
            lsHTML += "</tr>";
            lsHTML += "</table>";
            lsHTML += "</td>";
            lsHTML += "</tr>";
            lsHTML += "</table>";
            lsHTML += "</td>";
            lsHTML += "</tr>";
            lsHTML += "</table>";
            lsHTML += "</body>";
            lsHTML += "</html>";

            return lsHTML;
        }

        #region Comentado para usar simgleton
        //public const string AnalistaRO = "005011";
        //public const string DescProceso0 = "Riesgo Registrado";
        //public const string DescProceso1 = "Identificación del Riesgo";
        //public const string DescProceso2 = "Nivel de Riesgo Inherente";
        //public const string DescProceso3 = "Nivel de Riesgo Residual";
        //public const string DescProceso4 = "Planes de Acción";
        //public const string DescProceso5 = "Responsables de Planes de Acción";
        //public const string [] PenMontos = { "", "", ""};

        //public static string Correo(string psTitulo, string psContenido)
        //{
        //    string lsCorreo = "<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
        //    "<html xmlns='http://www.w3.org/1999/xhtml'>" +
        //    "<head>" +
        //    "<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />" +
        //    "<title>SIRO</title>" +
        //    "<meta name='viewport' content='width=device-width, initial-scale=1.0'/>" +
        //    "</head>" +
        //    "<body style='margin: 0; padding: 0;'>" +
        //    "<table border='0' cellpadding='0' cellspacing='0' width='100%'>" +
        //    "<tr>" +
        //    "<td style='padding: 10px 0 30px 0;'>" +
        //    "<table align='center' border='0' cellpadding='0' cellspacing='0' width='600' style='border: 1px solid #cccccc; border-collapse: collapse;'>" +
        //    "<tr>" +
        //    "<td align='center' bgcolor='#981B1B' style='padding: 40px 0 30px 0; color: #153643; font-size: 28px; font-weight: bold; font-family: Arial, sans-serif;'>" +
        //    "<img src='cid:logocaja'  alt='Logo Caja Maynas' width='160' height='100' style='display: block;' />" +
        //    //"<img src='" + HttpContext.Current.Server.MapPath("~/Content/Assets/images/cmac-maynas/logo/logo@2x.png") + "' alt='Logo Caja Maynas' width='160' height='100' style='display: block;' />" +
        //    "</td>" +
        //    "</tr>" +
        //    "<tr>" +
        //    "<td bgcolor='#ffffff' style='padding: 40px 30px 40px 30px;'>" +
        //    "<table border='0' cellpadding='0' cellspacing='0' width='100%'>" +
        //    "<tr>" +
        //    "<td style='color: #153643; font-family: Arial, sans-serif; font-size: 16px;'>" +
        //    "<b>" + psTitulo + "</b>" +
        //    "</td>" +
        //    "</tr>" +
        //    "<tr>" +
        //    "<td style='padding: 10px 0 20px 0; color: #153643; font-family: Arial, sans-serif; font-size: 12px; line-height: 20px;'>" + psContenido +
        //    "</td>" +
        //    "</tr>" +
        //    "</table>" +
        //    "</td>" +
        //    "</tr>" +
        //    "<tr>" +
        //    "<td bgcolor='#981B1B' style = 'padding: 30px 30px 30px 30px;'>" +
        //    "<table border='0' cellpadding='0' cellspacing='0' width='100%'>" +
        //    "<tr>" +
        //    "<td style='color: #ffffff; font-family: Arial, sans-serif; font-size: 11.5px;' width='75 %'>" +
        //    "Departamento de Gestión de Tecnologia de la Información - CMAC MAYNAS<br/>" +

        //    "</td>" +
        //    "</tr>" +
        //    "</table>" +
        //    "</td>" +
        //    "</tr>" +
        //    "</table>" +
        //    "</td>" +
        //    "</tr>" +
        //    "</table>" +
        //    "</body>" +
        //    "</html>";

        //    return lsCorreo;
        //}
        #endregion
    }


}