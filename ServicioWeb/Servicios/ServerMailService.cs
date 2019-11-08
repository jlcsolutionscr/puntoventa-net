using OpenPop.Mime;
using OpenPop.Pop3;
using System.Collections.Generic;

namespace LeandroSoftware.ServicioWeb
{
    public interface IServerMailService
    {
        IList<POPEmail> ObtenerListadoMensaje(string strMailUserAddress, string strMailUserPassword);
        void EliminarMensaje(string strMailUserAddress, string strMailUserPassword, int intIdMensaje);
    }

    public class ServerMailService : IServerMailService
    {
        private string smtpHost;
        private int smtpPort;

        public ServerMailService(string strSmtpHost, string strSmptPort)
        {
            smtpHost = strSmtpHost;
            smtpPort = int.Parse(strSmptPort);
        }

        public IList<POPEmail> ObtenerListadoMensaje(string strMailUserAddress, string strMailUserPassword)
        {
            using (Pop3Client pop3Client = new Pop3Client())
            {
                pop3Client.Connect(smtpHost, smtpPort, false);
                pop3Client.Authenticate(strMailUserAddress, strMailUserPassword);
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

        public void EliminarMensaje(string strMailUserAddress, string strMailUserPassword, int intIdMensaje)
        {
            using (Pop3Client pop3Client = new Pop3Client())
            {
                pop3Client.Connect(smtpHost, smtpPort, false);
                pop3Client.Authenticate(strMailUserAddress, strMailUserPassword);
                pop3Client.DeleteMessage(intIdMensaje);
            }
        }
    }
}
