using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace openVisio
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new NetMonitor());
            }
            catch (System.ComponentModel.LicenseException) 
            {
                MessageBox.Show("请从作者处取得认证再继续，联系人 18655429576");
            }
        }
    }
}
