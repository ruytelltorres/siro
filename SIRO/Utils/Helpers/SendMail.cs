using SIRO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using CMACMaynas.Web.SIRO.Aplicacion.Interface;
using System.Dynamic;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SIRO.Utils.Helpers
{
    public class SendMail
    {
        //Singleton
        private static SendMail instancia;
      
        private SendMail() { }
        public static SendMail Get
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new SendMail();
                }
                return instancia;
            }
        }
        
        public bool Ping()
        {
            var ping = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply pingreply;
            pingreply = ping.Send("192.168.0.2");

            if (pingreply.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                return true;
            }
            return false;
        }

        #region Envio de Correo - Valor Boleano
        //public bool EnviarEmail(List<string> Destinatarios, string Asunto, string Correo, List<string> Ccs = null, string Remitente = "")
        //{

        //    MailMessage message = new MailMessage();
        //    //var lsRemitente = "";

        //    dynamic SendConfig = new ExpandoObject();
        //    SendConfig.Eslogan = constSistema.ObtenerConstanteSistema(151).cConsSisValor;
        //    SendConfig.Remitente = constSistema.ObtenerConstanteSistema(50).cConsSisValor;
        //    SendConfig.Pruebas = constSistema.ObtenerConstanteSistema(200).cConsSisValor;
        //    SendConfig.CorreoTest = constSistema.ObtenerConstanteSistema(201).cConsSisValor;



        //    if (Convert.ToBoolean(Convert.ToInt32(SendConfig.Pruebas))) //added by TORE 20201206
        //    {
        //        var MailTest = new List<string>() { Convert.ToString(SendConfig.CorreoTest) };
        //        foreach (var _mail in MailTest)
        //        { message.To.Add(_mail); }

        //        //En caso de existir copiados
        //        if (Ccs != null)
        //        {
        //            foreach (string cc in MailTest)
        //            {
        //                message.CC.Add(cc);
        //            }
        //        }

        //        Correo = "<hr /><p>Correo de Prueba</p><hr />" + Correo;
        //    }
        //    else
        //    {
        //        foreach (var _mail in Destinatarios)
        //        { message.To.Add(_mail); }

        //        if (Ccs != null)
        //        {
        //            foreach (string cc in Ccs)
        //            {
        //                message.CC.Add(cc);
        //            }
        //        }
        //    }

        //    if (Remitente == "") { Remitente = SendConfig.Remitente; }

        //    /*Configuracion para el envio de correo con firma*/
        //    LinkedResource logoFirma = new LinkedResource(@"" + HttpContext.Current.Server.MapPath("~/Content/Assets/images/cmac-maynas/logo/logo@2x.png"));
        //    logoFirma.ContentId = "logocaja";
        //    logoFirma.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);

        //    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Correo, null, MediaTypeNames.Text.Html);
        //    htmlView.LinkedResources.Add(logoFirma);

        //    message.AlternateViews.Add(htmlView);
        //    /*end configuracion*/

        //    message.From = new MailAddress(Remitente, SendConfig.Eslogan, System.Text.Encoding.UTF8);
        //    message.Subject = Asunto;
        //    message.SubjectEncoding = System.Text.Encoding.UTF8;
        //    message.Body = Correo;
        //    message.BodyEncoding = System.Text.Encoding.UTF8;
        //    message.IsBodyHtml = true; //Si vas a enviar un correo con contenido html entonces cambia el valor a true

        //    //SmtpClient smtpClient = new SmtpClient();
        //    //if (Destinatarios != null)
        //    //{
        //    //    if (Destinatarios.Count > 0)
        //    //    {
        //    //        smtpClient.Credentials = new System.Net.NetworkCredential(Destinatarios[0], "");
        //    //    }
        //    //}
        //    //smtpClient.Port = 25;
        //    //smtpClient.Host = "192.168.0.2";
        //    //smtpClient.EnableSsl = true; //Esto es para que vaya a través de SSL que es obligatorio con GMail

        //    try
        //    {

        //        using (var mail = new SmtpClient())
        //        {
        //            if (Destinatarios != null)
        //            {
        //                if (Destinatarios.Count > 0)
        //                {
        //                    mail.Credentials = new System.Net.NetworkCredential(Destinatarios[0], "");
        //                }
        //            }
        //            mail.Port = 25;
        //            mail.Host = "192.168.0.2";
        //            //smtpClient.EnableSsl = true; //Esto es para que vaya a través de SSL que es obligatorio con GMail

        //            mail.Send(message);
        //        }



        //        return true;
        //    }
        //    //catch (Exception ex) { return false; }
        //    catch { return false; }
        //}
        #endregion

        public async Task EnvioMail(List<string> Destinatarios, string Asunto, string Correo, List<string> Ccs = null, string Remitente = "", MailModel configMail = null)
        {
            using (var message = new MailMessage())
            {
                if (configMail == null) { throw new ArgumentNullException("configMail"); }
                string smtpEmail = ConfigurationManager.AppSettings["SmtpEmail"];
                string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
                if (string.IsNullOrWhiteSpace(smtpEmail) || string.IsNullOrWhiteSpace(smtpPassword))
                {
                    throw new ConfigurationErrorsException("Configure SmtpEmail y SmtpPassword para enviar las notificaciones.");
                }
                smtpEmail = smtpEmail.Trim();
                if (configMail.Pruebas) //added by TORE 20201206
                {
                    var MailTest = new List<string>() { Convert.ToString(configMail.CorreoTest) };
                    foreach (var _mail in MailTest)
                    { message.To.Add(_mail); }

                    //En caso de existir copiados
                    if (Ccs != null)
                    {
                        foreach (string cc in MailTest)
                        {
                            message.CC.Add(cc);
                        }
                    }

                    Correo = "<hr /><p>Correo de Prueba</p><hr />" + Correo;
                }
                else
                {
                    foreach (var _mail in Destinatarios)
                    { message.To.Add(_mail); }

                    if (Ccs != null)
                    {
                        foreach (string cc in Ccs)
                        {
                            message.CC.Add(cc);
                        }
                    }
                }

                // Gmail autentica el envío con la misma cuenta que figura como remitente.
                Remitente = smtpEmail;

                LinkedResource logoFirma = new LinkedResource(@"" + HttpContext.Current.Server.MapPath("~/Content/Assets/images/default/logo-siro.png"));
                logoFirma.ContentId = "logocaja";
                logoFirma.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Correo, null, MediaTypeNames.Text.Html);
                htmlView.LinkedResources.Add(logoFirma);

                message.AlternateViews.Add(htmlView);
                message.From = new MailAddress(Remitente, configMail.Eslogan, System.Text.Encoding.UTF8);
                message.Subject = Asunto;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = Correo;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;


                using (var mail = new SmtpClient())
                {
                    mail.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mail.UseDefaultCredentials = false;
                    mail.Credentials = new System.Net.NetworkCredential(smtpEmail, smtpPassword.Replace(" ", ""));
                    mail.Port = 587;
                    mail.Host = "smtp.gmail.com";
                    mail.EnableSsl = true;

                    await mail.SendMailAsync(message);
                }
            }
        }

        public string ObtenerMailUsuario(string cUser)
        {
            var usuario = DependencyResolver.Current.GetService<IUsuarioApp>();
            return usuario.ObtenerMailUsuario(cUser);
        }

    }
}
