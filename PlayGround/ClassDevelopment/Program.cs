using System;
using System.Windows.Forms;
using Helper;

namespace ClassDevelopment
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmSplash());
            Application.Run(new FrmMain());
        }
    }
}
