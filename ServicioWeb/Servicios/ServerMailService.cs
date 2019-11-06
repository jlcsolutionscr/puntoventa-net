using OpenPop.Mime;
using OpenPop.Pop3;
using System.Collections.Generic;

namespace LeandroSoftware.ServicioWeb
{
    public interface IServerMailService
    {
        IList<POPEmail> ObtenerListadoMensaje();
        void EliminarMensaje(int intIdMensaje);
    }

    public class ServerMailService : IServerMailService
    {
        private string smtpHost;
        private int smtpPort;
        private string mailUserAddress;
        private string mailUserPassword;

        public ServerMailService(string strSmtpHost, string strSmptPort, string strMailUserAddress, string strMailUserPassword)
        {
            smtpHost = strSmtpHost;
            smtpPort = int.Parse(strSmptPort);
            mailUserAddress = strMailUserAddress;
            mailUserPassword = strMailUserPassword;
        }

        public IList<POPEmail> ObtenerListadoMensaje()
        {
            using (Pop3Client pop3Client = new Pop3Client())
            {
                pop3Client.Connect(smtpHost, smtpPort, false);
                pop3Client.Authenticate(mailUserAddress, mailUserPassword);
                int count = pop3Client.GetMessageCount();
                var Emails = new List<POPEmail>();
                int counter = 0;
                for (int i = count; i >= 1; i--)
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
                    Emails.Add(email);
                    counter++;
                    if (counter > 2)
                    {
                        break;
                    }
                }
                return Emails;
            }
        }

        public void EliminarMensaje(int intIdMensaje)
        {
            using (Pop3Client pop3Client = new Pop3Client())
            {
                pop3Client.Connect(smtpHost, smtpPort, false);
                pop3Client.Authenticate(mailUserAddress, mailUserPassword);
                pop3Client.DeleteMessage(intIdMensaje);
            }
        }
    }
}
