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

    public partial class PrintingService : ServiceBase
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
        private System.Collections.Specialized.NameValueCollection appSettings;
        private EventLog eventLog;
        private int eventId = 1;
        private List<ClsLineaImpresion> ticketLines = new List<ClsLineaImpresion> { };
        private CancellationTokenSource _cts;
        private Task _printingTask;
        string strServicioURL = ConfigurationManager.AppSettings["ServicioURL"];
        string strNombreImpresora = ConfigurationManager.AppSettings["NombreImpresora"];

        public PrintingService()
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
            Console.WriteLine($"DEBUG Mode - Test pending tickets task execution just once...");
            _cts = new CancellationTokenSource();
            _printingTask = RequestPendingTickets(_cts.Token);
            Task.Run(async () => await _printingTask);
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            eventLog.WriteEntry("OnTimer execution", EventLogEntryType.Information, eventId++);
            _cts = new CancellationTokenSource();
            _printingTask = RequestPendingTickets(_cts.Token);
            Task.Run(async () => await _printingTask);
        }

        private async Task RequestPendingTickets(CancellationToken cancelToken)
        {
            if (!cancelToken.IsCancellationRequested)
            {
                try
                {
                    int intIdEmpresa = int.Parse(ConfigurationManager.AppSettings["IdEmpresa"]);
                    int intIdSucursal = int.Parse(ConfigurationManager.AppSettings["IdSucursal"]);
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
                            ticketLines = GenerateWorkingOrderTicket(tiquete);
                            PrintTicket();
                            await httpClient.GetAsync(strServicioURL + "/cambiarestadoaimpresotiqueteordenservicio?idtiquete=" + tiquete.IdTiquete, cancelToken);
                            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                            {
                                string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                                throw new Exception(strError);
                            }
                            if (httpResponse.StatusCode != HttpStatusCode.OK)
                                throw new Exception(httpResponse.ReasonPhrase);
                        }
                    }
                }
                catch (Exception ex)
                {
                    eventLog.WriteEntry("Error requesting tickets to be printed: " + ex.Message, EventLogEntryType.Error);
                }
            }
        }

        private void PrintTicket()
        {
            try
            {
                PrintDocument doc = new PrintDocument();
                doc.PrinterSettings.PrinterName = strNombreImpresora;
                doc.PrintPage += new PrintPageEventHandler(ProvideContent);
                doc.Print();
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Error setting up Printer config: " + strNombreImpresora + " Error message: " + ex.Message, EventLogEntryType.Error);
            }
        }

        private void ProvideContent(object sender, PrintPageEventArgs e)
        {
            try
            {
                if (ticketLines.Count > 0)
                {
                    int i = 0;
                    int charCount = int.Parse(appSettings.Get("AnchoLinea"));
                    double paperWith = 3.5375 * charCount;
                    Graphics graphics = e.Graphics;
                    int positionY = 0;
                    StringFormat sf = new StringFormat();
                    while (i < ticketLines.Count)
                    {
                        ClsLineaImpresion linea = ticketLines[i];
                        FontStyle fontStyle = linea.bolBold ? FontStyle.Bold : FontStyle.Regular;
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = (StringAlignment)linea.intAlineado;
                        RectangleF rec = new RectangleF
                        {
                        Width = (float)(paperWith * linea.intAncho / 100),
                        Height = 20,
                        X = (float)(paperWith * linea.intPosicionX / 100),
                        Y = positionY
                        };
                        float fltFontSize = (float)linea.intFuente / 80 * charCount;
                        graphics.DrawString(linea.strTexto, new Font("Lucida Console", fltFontSize, fontStyle), new SolidBrush(Color.Black), rec, sf);
                        positionY += 20 * linea.intSaltos;
                        i += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Error sending ticket lines to Printer: " + strNombreImpresora + " Error message: " + ex.Message, EventLogEntryType.Error);
            }
        }

        private void GenerateWorkingOrderTicket(TiqueteOrdenServicio tiquete)
        {
            ticketLines = new List<ClsLineaImpresion> { };
            ticketLines.Add(new ClsLineaImpresion(1, tiquete.Etiqueta, 0, 100, 14, (int)StringAlignment.Center, true));
            ticketLines.Add(new ClsLineaImpresion(1, "PEDIDO EN PROCESO", 0, 100, 14, (int)StringAlignment.Center, true));
            ticketLines.Add(new ClsLineaImpresion(2, tiquete.FechaEmision, 0, 100, 12, (int)StringAlignment.Center, false));
            
            ticketLines.Add(new ClsLineaImpresion(1, "DETALLE DE ORDEN", 0, 100, 12, (int)StringAlignment.Center, false));
            foreach (DescipcionValor linea in tiquete.DetalleTiqueteOrdenServicio)
            {
                string strDescription = linea.Descripcion;
                while (strDescription.Length > 0)
                {
                    if (strDescription.Length > 30)
                    {
                        lineasDetalle.Add(new ClsLineaImpresion(1, strDescription.Substring(0, 30), 0, 100, 10, (int)StringAlignment.Near, false));
                        strDescription = strDescription.Substring(30);
                    }
                    else
                    {
                        lineasDetalle.Add(new ClsLineaImpresion(1, strDescription, 0, 100, 10, (int)StringAlignment.Near, false));
                        strDescription = "";
                    }
                }
                lineasDetalle.Add(new ClsLineaImpresion(1, linea.Valor.ToString(), 0, 100, 10, (int)StringAlignment.Near, false));
            }
            ticketLines.Add(new ClsLineaImpresion(2, "", 0, 100, 10, (int)StringAlignment.Near, false));
            string strDetails = tiquete.Descripcion;
            while (strDetails.Length > 0)
            {
                if (strDetails.Length > 30)
                {
                    ticketLines.Add(new ClsLineaImpresion(1, strDetails.Substring(0, 30), 0, 100, 10, (int)StringAlignment.Near, false));
                    strDetails = strDetails.Substring(30);
                }
                else
                {
                    ticketLines.Add(new ClsLineaImpresion(1, strDetails, 0, 100, 10, (int)StringAlignment.Near, false));
                    strDetails = "";
                }
            }
            ticketLines.Add(new ClsLineaImpresion(2, "", 0, 100, 10, (int)StringAlignment.Near, false));
        }
    }
}
