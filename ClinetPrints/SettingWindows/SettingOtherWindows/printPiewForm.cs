using ClinetPrints.CreatContorl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    public partial class printPiewForm : Form
    {
        public printPiewForm()
        {
            InitializeComponent();
        }
        public string page = "";
        public string fileAddress = "";
        public string jobNum = "1";
        public int num = 1;
        private void printPiewForm_Load(object sender, EventArgs e)
        {
            printPiewControl1 = new printPiewControl();
            printPiewControl1.file = new FileStream(@"./pages.xml", FileMode.OpenOrCreate);
            printPiewControl1.page = this.page;
            printPiewControl1.fileAddress = fileAddress;
            printPiewControl1.jobNum = jobNum;
            printPiewControl1.num = num;
            printPiewControl1.toolBtn_close.Click += ToolBtn_close_Click;
            printPiewControl1.toolBtn_print.Click += ToolBtn_print_Click;
        }

        private void ToolBtn_print_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void ToolBtn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
