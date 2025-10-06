using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE6122_POE_Part_1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show("UI Thread exception:\n" + e.Exception, "Crash");
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                MessageBox.Show("Non-UI exception:\n" + e.ExceptionObject, "Crash");
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Startup exception:\n" + ex, "Crash");
            }
        }
    }
}
