using System;
using System.ServiceProcess;

namespace JLCSolutionsCR
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new PrintingService()
            };
            ServiceBase.Run(ServicesToRun);
            /*PrintingService service = new PrintingService();
            service.TestInConsole(args);*/
        }
    }
}
