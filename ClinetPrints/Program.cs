using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClinetPrints
{
    static class Program
    {
        /// <summary>
        /// 获取程序已经打开过的线程信息
        /// </summary>
        static System.Threading.Mutex _mutex;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isOPen;
            _mutex = new System.Threading.Mutex(true, "ClientPrints.exe", out isOPen);
            if (isOPen)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ClientMianWindows());
            }else
            {
                MessageBox.Show("已经打开了一个程序！");
            }
        }
    }
}
