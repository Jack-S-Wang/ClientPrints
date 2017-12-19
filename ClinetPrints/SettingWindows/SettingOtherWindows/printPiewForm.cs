using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
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
        public bool printTo=false;
        public string fileAddress = "";
        public string jobNum = "1";
        public int num = 1;
        public PrinterObjects lipo;
        private void printPiewForm_Load(object sender, EventArgs e)
        {
            printPiewControl1.fileAddress = fileAddress;
            printPiewControl1.jobNum = jobNum;
            printPiewControl1.num = num;
            printPiewControl1.PrinterObject = lipo;
            printPiewControl1.OnBtnClose += PrintPiewControl1_OnBtnClose;
            printPiewControl1.onBtnPrint += PrintPiewControl1_onBtnPrint;
        }

        private void PrintPiewControl1_onBtnPrint(EventArgs obj)
        {
            printTo = true;
            this.Close();
        }

        private void PrintPiewControl1_OnBtnClose(EventArgs obj)
        {
            this.Close();
        }
    }
}
