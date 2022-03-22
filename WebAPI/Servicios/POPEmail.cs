namespace LeandroSoftware.ServicioWeb
{
    public class POPEmail
    {
        public POPEmail()
        {
            this.Attachments = new List<Attachment>();
        }
        public int MessageNumber { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
        public List<Attachment> Attachments { get; set; }
    }

    public class Attachment
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
