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
        void SendEmail(string[] emailTo, string[] ccTo, string subject, string body, bool isBodyHtml, JArray strAttachments);
    }

    public class CorreoService : ICorreoService
    {
        private string smtpHost;
        private int smtpPort;
        private int pop3Port;
        private string mailUserAddress;
        private string mailUserPassword;
        private string sslHost;

        public CorreoService(IConfiguration config)
        {
            smtpHost = config.GetSection("appSettings").GetSection("smtpEmailHost").Value;
            smtpPort = int.Parse(config.GetSection("appSettings").GetSection("smtpEmailPort").Value);
            mailUserAddress = config.GetSection("appSettings").GetSection("smtpEmailAccount").Value;
            mailUserPassword = config.GetSection("appSettings").GetSection("smtpEmailPass").Value;
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
            SmtpClient smtpClient = new SmtpClient(smtpHost)
            {
                UseDefaultCredentials = false,
                Port = smtpPort,
                EnableSsl = sslHost == "S"
            };
            if (mailUserAddress != "" & mailUserPassword != "")
                smtpClient.Credentials = new NetworkCredential(mailUserAddress, mailUserPassword);
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(mailUserAddress);
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
                System.Net.Mail.Attachment attachment;
                foreach (JObject attachmentItem in strAttachments)
                {
                    string strAttachmentName = attachmentItem.Property("nombre").Value.ToString();
                    byte[] content = Convert.FromBase64String(attachmentItem.Property("contenido").Value.ToString());
                    attachment = new System.Net.Mail.Attachment(new MemoryStream(content), strAttachmentName);
                    message.Attachments.Add(attachment);
                }
                smtpClient.Send(message);
            };
        }
    }
}
