using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace OrderPrinter
{
    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };

    public partial class Printing : ServiceBase
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
        private System.Collections.Specialized.NameValueCollection appSettings;
        private EventLog eventLog;
        private int eventId = 1;
        private List<ClsLineaImpresion> lineas = new List<ClsLineaImpresion> { };
        private CancellationTokenSource _cts;
        private Task _printingTask;

        public Printing()
        {
            InitializeComponent();
            eventLog = new EventLog();
            if (!EventLog.SourceExists("JLC-Ticket-Printing"))
            {
                EventLog.CreateEventSource("JLC-Ticket-Printing", "ActivityLog");
            }
            eventLog.Source = "JLC-Ticket-Printing";
            eventLog.Log = "ActivityLog";
            try
            {
                appSettings = ConfigurationManager.AppSettings;
            } catch (Exception ex)
            {
                eventLog.WriteEntry("Error reading configuration file: " + ex.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnStart(string[] args)
        {
            ServiceStatus serviceStatus = new ServiceStatus
            {
                dwCurrentState = ServiceState.SERVICE_START_PENDING,
                dwWaitHint = 100000
            };
            SetServiceStatus(ServiceHandle, ref serviceStatus);
            eventLog.WriteEntry("In OnStart.");
            System.Timers.Timer timer = new System.Timers.Timer
            {
                Interval = 60000
            };
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {
            ServiceStatus serviceStatus = new ServiceStatus
            {
                dwCurrentState = ServiceState.SERVICE_STOP_PENDING,
                dwWaitHint = 100000
            };
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            eventLog.WriteEntry("In OnStop.");
            _cts.Cancel();
            _printingTask.Wait(TimeSpan.FromSeconds(5));
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        public void TestInConsole(string[] args)
        {
            Console.WriteLine($"Service starting...");
            this.OnStart(args);
            Console.WriteLine($"Service started. Press any key to stop.");
            Console.ReadKey();
            Console.WriteLine($"Service stopping...");
            this.OnStop();
            Console.WriteLine($"Service stopped. Closing in 5 seconds.");
            Thread.Sleep(5000);
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog.WriteEntry("OnTimer execution", EventLogEntryType.Information, eventId++);

            _cts = new CancellationTokenSource();
            // Launch the task but do not 'await' it here
            _printingTask = RequestPendingTickets(_cts.Token);
            Task.Run(async () => await _printingTask);
        }

        private async Task RequestPendingTickets(CancellationToken cancelToken)
        {
            if (!cancelToken.IsCancellationRequested)
            {
                try
                {
                    string strServicioURL = ConfigurationManager.AppSettings["ServicioURL"];
                    int intIdEmpresa = int.Parse(ConfigurationManager.AppSettings["IdEmpresa"]);
                    int intIdSucursal = int.Parse(ConfigurationManager.AppSettings["IdSucursal"]);
                    string strNombreImpresora = ConfigurationManager.AppSettings["NombreImpresora"];
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    using (var httpClient = new HttpClient())
                    {
                        HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadotiqueteordenserviciopendiente?idempresa=" + intIdEmpresa + "&idsucursal=" + intIdSucursal + "&impresora=" + strNombreImpresora, cancelToken);

                        if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                        {
                            string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                            throw new Exception(strError);
                        }
                        if (httpResponse.StatusCode != HttpStatusCode.OK)
                            throw new Exception(httpResponse.ReasonPhrase);
                        string response = await httpResponse.Content.ReadAsStringAsync();
                        List<TiqueteOrdenServicio> listado = new List<TiqueteOrdenServicio>();
                        if (response != "")
                            listado = JsonConvert.DeserializeObject<List<TiqueteOrdenServicio>>(response);
                        foreach (TiqueteOrdenServicio tiquete in listado)
                        {
                            lineas = JsonConvert.DeserializeObject< List<ClsLineaImpresion>>(tiquete.DetalleTiqueteOrdenServicio);
                            ImprimirTiquete(strNombreImpresora);
                        }
                    }
                }
                catch (Exception ex)
                {
                    eventLog.WriteEntry("Error requesting tickets to be printed: " + ex.Message, EventLogEntryType.Error);
                }
            }
        }

        private void ImprimirTiquete(string szPrinterName)
        {
            try
            {
                PrintDocument doc = new PrintDocument();
                doc.PrinterSettings.PrinterName = szPrinterName;
                doc.PrintPage += new PrintPageEventHandler(ProvideContent);
                doc.Print();
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Error sending ticket lines to Printer: " + szPrinterName + " Error message: " + ex.Message, EventLogEntryType.Error);
            }
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
