using System;
using System.Windows.Forms;

namespace LeandroSoftware.Migracion.ClienteWeb
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Migracion());
        }
    }
}
