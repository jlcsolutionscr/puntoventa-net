using OpenPop.Mime;
using OpenPop.Pop3;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface ICorreoService
    {
        IList<POPEmail> ObtenerListadoMensaje(string strMailUserAddress, string strMailUserPassword);
        void EliminarMensaje(string strMailUserAddress, string strMailUserPassword, int intIdMensaje);
         void SendErrorEmail(string[] emailTo, string[] ccTo, string subject, string body, bool isBodyHtml, JArray? strAttachments = null);
        void SendSupportEmail(string[] emailTo, string[] ccTo, string subject, string body, bool isBodyHtml, JArray? strAttachments = null);
        void SendNotificationEmail(string[] emailTo, string[] ccTo, string subject, string body, bool isBodyHtml, JArray? strAttachments = null);
    }

    public class CorreoService : ICorreoService
    {
        private string smtpHost;
        private int smtpPort;
        private int pop3Port;
        private string mailNotificationAddress;
        private string mailNotificationPassword;
        private string mailErrorAddress;
        private string mailErrorPassword;
        private string mailSupportAddress;
        private string mailSupportPassword;

        private string sslHost;

        public CorreoService(IConfiguration config)
        {
            smtpHost = config.GetSection("appSettings").GetSection("smtpEmailHost").Value;
            smtpPort = int.Parse(config.GetSection("appSettings").GetSection("smtpEmailPort").Value);
            mailNotificationAddress = config.GetSection("appSettings").GetSection("smtpNotificationAccount").Value;
            mailNotificationPassword = config.GetSection("appSettings").GetSection("smtpNotificationPass").Value;
            mailErrorAddress = config.GetSection("appSettings").GetSection("smtpErrorAccount").Value;
            mailErrorPassword = config.GetSection("appSettings").GetSection("smtpErrorPass").Value;
            mailSupportAddress = config.GetSection("appSettings").GetSection("smtpSupportAccount").Value;
            mailSupportPassword = config.GetSection("appSettings").GetSection("smtpSupportPass").Value;
            sslHost = config.GetSection("appSettings").GetSection("smtpSSLHost").Value;
            pop3Port = int.Parse(config.GetSection("appSettings").GetSection("pop3EmailPort").Value);
        }

        public IList<POPEmail> ObtenerListadoMensaje(string strMailUserAddress, string strMailUserPassword)
        {
            using (Pop3Client pop3Client = new Pop3Client())
            {
                pop3Client.Connect(smtpHost, pop3Port, false);
                pop3Client.Authenticate(strMailUserAddress, strMailUserPassword);
                int count = pop3Client.GetMessageCount();
                var Emails = new List<POPEmail>();
                int counter = 0;
                for (int i = count; i >= 1; i--)
                {
                    try
                    {
                        Message message = pop3Client.GetMessage(i);
                        POPEmail email = new POPEmail()
                        {
                            MessageNumber = i,
                            Subject = message.Headers.Subject,
                            DateSent = message.Headers.DateSent,
                            From = string.Format("<a href = 'mailto:{1}'>{0}</a>", message.Headers.From.DisplayName, message.Headers.From.Address),
                        };
                        MessagePart body = message.FindFirstHtmlVersion();
                        if (body != null)
                        {
                            email.Body = body.GetBodyAsText();
                        }
                        else
                        {
                            body = message.FindFirstPlainTextVersion();
                            if (body != null)
                            {
                                email.Body = body.GetBodyAsText();
                            }
                        }
                        try
                        {
                            List<MessagePart> attachments = message.FindAllAttachments();
                            foreach (MessagePart attachment in attachments)
                            {
                                email.Attachments.Add(new Attachment
                                {
                                    FileName = attachment.FileName,
                                    ContentType = attachment.ContentType.MediaType,
                                    Content = attachment.Body
                                });
                            }
                        }
                        catch (Exception)
                        { }
                        Emails.Add(email);
                        counter++;
                    }
                    catch (Exception)
                    { }
                    if (counter > 30) break;
                }
                return Emails;
            }
        }

        public void EliminarMensaje(string strMailUserAddress, string strMailUserPassword, int intIdMensaje)
        {
            using (Pop3Client pop3Client = new Pop3Client())
            {
                pop3Client.Connect(smtpHost, pop3Port, false);
                pop3Client.Authenticate(strMailUserAddress, strMailUserPassword);
                pop3Client.DeleteMessage(intIdMensaje);
            }
        }

        public void SendSupportEmail(string[] emailTo, string[] ccTo, string subject, string body, bool isBodyHtml, JArray? strAttachments = null)
        {
            SendEmail(mailSupportAddress, mailSupportPassword, emailTo, ccTo, subject, body, isBodyHtml, strAttachments);
        }

        public void SendErrorEmail(string[] emailTo, string[] ccTo, string subject, string body, bool isBodyHtml, JArray? strAttachments = null)
        {
            SendEmail(mailErrorAddress, mailErrorPassword, emailTo, ccTo, subject, body, isBodyHtml, strAttachments);
        }

        public void SendNotificationEmail(string[] emailTo, string[] ccTo, string subject, string body, bool isBodyHtml, JArray? strAttachments = null)
        {
            SendEmail(mailNotificationAddress, mailNotificationPassword, emailTo, ccTo, subject, body, isBodyHtml, strAttachments);
        }

        private void SendEmail(string senderAccount, string senderPass, string[] emailTo, string[] ccTo, string subject, string body, bool isBodyHtml, JArray? strAttachments = null)
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
            SmtpClient smtpClient = new SmtpClient(smtpHost)
            {
                UseDefaultCredentials = false,
                Port = smtpPort,
                EnableSsl = sslHost == "S"
            };
            if (senderAccount != "" & senderPass != "")
                    smtpClient.Credentials = new NetworkCredential(senderAccount, senderPass);
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(senderAccount);
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
                if (strAttachments != null) {
                    System.Net.Mail.Attachment attachment;
                    foreach (JObject attachmentItem in strAttachments)
                    {
                        string strAttachmentName = attachmentItem.Property("nombre").Value.ToString();
                        byte[] content = Convert.FromBase64String(attachmentItem.Property("contenido").Value.ToString());
                        attachment = new System.Net.Mail.Attachment(new MemoryStream(content), strAttachmentName);
                        message.Attachments.Add(attachment);
                    }
                }
                smtpClient.Send(message);
            };
        }
    }
}
