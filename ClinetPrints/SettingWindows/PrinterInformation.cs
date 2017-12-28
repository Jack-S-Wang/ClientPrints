using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClinetPrints.SettingWindows
{
    public partial class PrinterInformation : Form
    {
        public PrinterInformation()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrinterInformation_Load(object sender, EventArgs e)
        {
            try
            {
                Point p = new Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
                this.Location = p;
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }
    }
}
