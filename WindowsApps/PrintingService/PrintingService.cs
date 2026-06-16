using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Reflection;
using System.Xml;

namespace JLCSolutionsCR
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
        private List<ClsLineaImpresion> ticketLines = new List<ClsLineaImpresion> { };
        private CancellationTokenSource _cts;
        private readonly ILog _logger = LogManager.GetLogger(typeof(PrintingService));
        private Task _printingTask;
        string strServicioURL = ConfigurationManager.AppSettings["ServicioURL"];

        public PrintingService()
        {
            InitializeComponent();
            try
            {
                appSettings = ConfigurationManager.AppSettings;
            } catch (Exception ex)
            {
                _logger.Error("Error reading configuration file: " + ex.Message);
            }
            try
            {
                XmlDocument log4netConfig = new XmlDocument();
                using (var fs = File.OpenRead("log4net.config"))
                {
                    log4netConfig.Load(fs);
                    var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
                    XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting logger config", ex);
            }
        }

        public void TestInConsole(string[] args)
        {
            _cts = new CancellationTokenSource();
            Task.Run(async () =>
            {
                await ProcesstPendingTickets(_cts.Token);
                _cts.Dispose();
            });
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }

        protected override void OnStart(string[] args)
        {
            ServiceStatus serviceStatus = new ServiceStatus
            {
                dwCurrentState = ServiceState.SERVICE_START_PENDING,
                dwWaitHint = 100000
            };
            SetServiceStatus(ServiceHandle, ref serviceStatus);
            _logger.Info("In OnStart.");
            System.Timers.Timer timer = new System.Timers.Timer
            {
                Interval = int.Parse(ConfigurationManager.AppSettings["Intervalo"])
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
            _logger.Info("In OnStop.");
            try
            {
                _cts.Cancel();
                _printingTask.Wait(TimeSpan.FromSeconds(5));
                _cts.Dispose();
            }
            catch (Exception ex)
            {
                _logger.Info("Token was not initialized..." + ex.Message);
            }
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            _cts = new CancellationTokenSource();
            _printingTask = ProcesstPendingTickets(_cts.Token);
            Task.Run(async () =>
            {
                await _printingTask;
                _cts.Dispose();
            });
        }

        private async Task ProcesstPendingTickets(CancellationToken cancelToken)
        {
            if (!cancelToken.IsCancellationRequested)
            {
                try
                {
                    int intIdEmpresa = int.Parse(ConfigurationManager.AppSettings["IdEmpresa"]);
                    int intIdSucursal = int.Parse(ConfigurationManager.AppSettings["IdSucursal"]);
                    string strImpresora = ConfigurationManager.AppSettings["Impresora"];
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    using (var httpClient = new HttpClient())
                    {
                        string response = "";
                        try
                        {
                            string strRequest = strServicioURL + "/obtenerlistadotiqueteordenserviciopendiente?idempresa=" + intIdEmpresa + "&idsucursal=" + intIdSucursal + "&impresora=" + strImpresora;
                            HttpResponseMessage httpResponse = await httpClient.GetAsync(strRequest);
                            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                            {
                                string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                                throw new Exception(strError);
                            }
                            if (httpResponse.StatusCode != HttpStatusCode.OK)
                                throw new Exception(httpResponse.ReasonPhrase);
                            response = await httpResponse.Content.ReadAsStringAsync();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error requesting tickets from server:", ex.Message);
                            _logger.Error("Error requesting tickets from server: " + ex.Message);
                        }
                        List<TiqueteOrdenServicio> listado = new List<TiqueteOrdenServicio>();
                        if (response != "") listado = JsonConvert.DeserializeObject<List<TiqueteOrdenServicio>>(response);
                        foreach (TiqueteOrdenServicio tiquete in listado)
                        {
                            try
                            {
                                _logger.Info("Processing ticket: " + tiquete.IdTiquete);
                                GenerateWorkingOrderTicket(tiquete);
                                PrintTicket(tiquete.Impresora);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("Error printing ticket id: " + tiquete.IdTiquete + " Error: " + ex.Message);
                            }
                            try
                            {
                                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/cambiarestadoaimpresotiqueteordenservicio?idtiquete=" + tiquete.IdTiquete + "&status=impreso", cancelToken);
                                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                                {
                                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                                    throw new Exception(strError);
                                }
                                if (httpResponse.StatusCode != HttpStatusCode.OK)
                                    throw new Exception(httpResponse.ReasonPhrase);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("Error updating ticket status for ticket id: " + tiquete.IdTiquete + " Error: " + ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error requesting tickets from server", ex.Message);
                    _logger.Error("Error requesting tickets from server: " + ex.Message);
                }
            }
        }

        private void PrintTicket(string strPrinterName)
        {
            try
            {
                PrintDocument doc = new PrintDocument();
                doc.PrinterSettings.PrinterName = strPrinterName;
                doc.PrintPage += new PrintPageEventHandler(ProvideContent);
                doc.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error printing ticket on printer: " + strPrinterName + " Error message: " + ex.Message);
                _logger.Error("Error printing ticket on printer: " + strPrinterName + " Error message: " + ex.Message);
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
                _logger.Error("Error creating printing content. Error message: " + ex.Message);
            }
        }

        private void GenerateWorkingOrderTicket(TiqueteOrdenServicio tiquete)
        {
            try
            {
                ticketLines = new List<ClsLineaImpresion>
                {
                    new ClsLineaImpresion(1, tiquete.Etiqueta, 0, 100, 14, (int)StringAlignment.Center, true),
                    new ClsLineaImpresion(1, "PEDIDO EN PROCESO", 0, 100, 14, (int)StringAlignment.Center, true),
                    new ClsLineaImpresion(2, tiquete.FechaEmision, 0, 100, 12, (int)StringAlignment.Center, false),
                    new ClsLineaImpresion(1, "DETALLE DE ORDEN", 0, 100, 12, (int)StringAlignment.Center, false),
                    new ClsLineaImpresion(1, new string('-', 46), 0, 100, 12, (int)StringAlignment.Center, false)
                };
                IList<DescripcionValor> detalle = JsonConvert.DeserializeObject<IList<DescripcionValor>>(tiquete.DetalleTiqueteOrdenServicio);
                foreach (DescripcionValor linea in detalle)
                {
                    string strDescription = linea.Descripcion;
                    while (strDescription.Length > 0)
                    {
                        if (strDescription.Length > 46)
                        {
                            ticketLines.Add(new ClsLineaImpresion(1, strDescription.Substring(0, 46), 0, 100, 10, (int)StringAlignment.Center, false));
                            strDescription = strDescription.Substring(46);
                        }
                        else
                        {
                            ticketLines.Add(new ClsLineaImpresion(1, strDescription, 0, 100, 10, (int)StringAlignment.Center, false));
                            strDescription = "";
                        }
                    }
                    ticketLines.Add(new ClsLineaImpresion(1, linea.Valor.ToString(), 0, 100, 10, (int)StringAlignment.Center, false));
                }
                ticketLines.Add(new ClsLineaImpresion(1, new string('-', 46), 0, 100, 12, (int)StringAlignment.Center, false));
                ticketLines.Add(new ClsLineaImpresion(2, "FIN DEL PEDIDO", 0, 100, 10, (int)StringAlignment.Center, false));
            }
            catch (Exception ex)
            {
                _logger.Error("Error generating ticket stream. Error message: " + ex.Message);
            }
        }
    }
}
