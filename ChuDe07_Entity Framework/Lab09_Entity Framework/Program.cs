using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab09_Entity_Framework
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            // Use GDI+ text rendering by default to improve Unicode (Ti?ng Vi?t) rendering
            Application.SetCompatibleTextRenderingDefault(true);
            using (var login = new LoginForm())
            {
                if (login.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            Application.Run(new MainForm());
        }
    }
}
