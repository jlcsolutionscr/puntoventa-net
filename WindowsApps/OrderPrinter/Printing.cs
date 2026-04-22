using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Net;
using System.Net.Http;
using System.ServiceProcess;
using System.Timers;
namespace OrderPrinter
{
    public partial class Printing : ServiceBase
    {
        private System.Collections.Specialized.NameValueCollection appSettings;
        private EventLog eventLog;
        private int eventId = 1;
        private IList<ClsLineaImpresion> lineas = new List<ClsLineaImpresion>();
        public Printing()
        {
            InitializeComponent();
            eventLog = new EventLog();
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyNewLog");
            }
            eventLog.Source = "MySource";
            eventLog.Log = "MyNewLog";
            try
            {
                appSettings = ConfigurationManager.AppSettings;
            } catch (Exception ex)
            {
                eventLog.WriteEntry("Error reading configuration file");
            }
        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("In OnStart.");
            Timer timer = new Timer
            {
                Interval = 60000 // 60 seconds
            };
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("In OnStop.");
        }

        public async void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            string strServicioURL = ConfigurationManager.AppSettings["ServicioURL"];
            int intIdEmpresa = int.Parse(ConfigurationManager.AppSettings["IdEmpresa"]);
            int intIdSucursal = int.Parse(ConfigurationManager.AppSettings["IdSucursal"]);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await HttpClient.GetAsync(strServicioURL + "/obtenerlistadotiqueteordenserviciopendiente?idempresa=" + intIdEmpresa + "&idsucursal=" + intIdSucursal);
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string response = await httpResponse.Content.ReadAsStringAsync();
            List<ClsTiquete> listado = new List<ClsTiquete>();
            if (response != "")
                listado = JsonConvert.DeserializeObject<List<ClsTiquete>>(response);
            foreach (ClsTiquete tiquete in listado)
            { 
            }
        }

        private void ImprimirTiquete(string szPrinterName)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrinterSettings.PrinterName = szPrinterName;
            doc.PrintPage += new PrintPageEventHandler(ProvideContent);
            doc.Print();
        }


        private void ProvideContent(object sender, PrintPageEventArgs e)
        {
            //FontSize 8 41 Chars - FontSize 9 36 Chars - FontSize 10 32 Chars - FontSize 11 30 Chars - FontSize 12 27 Chars
            //FontSize 13 25 Chars - FontSize 14 23 Chars - FontSize 15 22 Chars - FontSize 16 20 Chars
            int i = 0;
            int charCount = int.Parse(appSettings.Get("AnchoLinea"));
            double paperWith = 3.5375 * charCount;
            Graphics graphics = e.Graphics;
            int positionY = 0;
            StringFormat sf = new StringFormat();
            while (i < lineas.Count)
            {
                ClsLineaImpresion linea = lineas[i];
                FontStyle fontStyle = linea.bolBold ? FontStyle.Bold : FontStyle.Regular;
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = (StringAlignment)linea.intAlineado;
                RectangleF rec = new RectangleF();
                rec.Width = (float)(paperWith * linea.intAncho / 100);
                rec.Height = 20;
                rec.X = (float)(paperWith * linea.intPosicionX / 100);
                rec.Y = positionY;
                int intFontSize = linea.intFuente / 80 * charCount;
                graphics.DrawString(linea.strTexto, new Font("Lucida Console", intFontSize, fontStyle), new SolidBrush(Color.Black), rec, sf);
                positionY += (20 * linea.intSaltos);
                i += 1;
            }
        }
    }
}
