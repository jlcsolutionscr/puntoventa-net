using System.ServiceProcess;

namespace OrderPrinter
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
#if !DEBUG
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new PrintingService()
            };
            ServiceBase.Run(ServicesToRun);
#else
            PrintingService service = new PrintingService();
            service.TestInConsole(args);
#endif
        }
    }
}
