using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RCMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        public static string uid = "";
        public static string utype = "";

        public static string district = "";
        public static string csid = "";
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BasePage());
        }
    }
}
