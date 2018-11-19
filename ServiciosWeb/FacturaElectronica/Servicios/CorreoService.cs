using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace LeandroSoftware.FacturaElectronicaHacienda.Servicios
{
    public interface ICorreoService
    {
        void SendEmail(string[] emailTo, string[] ccTo, string subject, string body, bool isBodyHtml, JArray strAttachments);
    }

    public class CorreoService : ICorreoService
    {
        private string smtpHost;
        private int smtpPort;
        private string mailUserAddress;
        private string mailUserPassword;
        private string emailFrom;
        private string sslHost;

        public CorreoService(string strSmtpHost, string strSmptPort, string strMailUserAddress, string strMailUserPassword, string strEmailFrom, string strSSLHost)
        {
            smtpHost = strSmtpHost;
            smtpPort = int.Parse(strSmptPort);
            mailUserAddress = strMailUserAddress;
            mailUserPassword = strMailUserPassword;
            emailFrom = strEmailFrom;
            sslHost = strSSLHost;
        }

        public void SendEmail(string[] emailTo, string[] ccTo, string subject, string body, bool isBodyHtml, JArray strAttachments)
        {
            if (emailTo == null || emailTo.Length == 0)
            {
                throw new Exception("El valor del campo 'Enviar a:' no debe ser nulo o estar en blanco.");
            }
            if (subject == null || subject.Length == 0)
            {
                throw new Exception("El valor del campo 'Asunto:' no debe ser nulo o estar en blanco.");
            }
            if (body == null || body.Length == 0)
            {
                throw new Exception("El valor del campo 'Mensaje:' no debe ser nulo o estar en blanco.");
            }

            List<string> tempFiles = new List<string>();
            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            if (sslHost == "S")
                smtpClient.EnableSsl = true;
            if (mailUserAddress != "" & mailUserPassword != "")
                smtpClient.Credentials = new NetworkCredential(mailUserAddress, mailUserPassword);
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(emailFrom);
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = isBodyHtml;
                foreach (string email in emailTo)
                {
                    message.To.Add(email);
                }
                if (ccTo != null && ccTo.Length > 0)
                {
                    foreach (string emailCc in ccTo)
                    {
                        message.CC.Add(emailCc);
                    }
                }
                Attachment attachment;
                foreach (JObject attachmentItem in strAttachments)
                {
                    string strAttachmentName = attachmentItem.Property("nombre").Value.ToString();
                    byte[] content = Convert.FromBase64String(attachmentItem.Property("contenido").Value.ToString());
                    attachment = new Attachment(new MemoryStream(content), strAttachmentName);
                    message.Attachments.Add(attachment);
                }
                try
                {
                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al intentar enviar el mensaje: " + ex.Message);
                }
            };
        }
    }
}