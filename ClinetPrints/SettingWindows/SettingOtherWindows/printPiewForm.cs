﻿using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
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
        public bool printTo = false;
        public string fileAddress = "";
        public string jobNum = "1";
        public int num = 1;
        public PrinterObjects lipo;
        private void printPiewForm_Load(object sender, EventArgs e)
        {
            try
            {
                printPiewControl1.fileAddress = fileAddress;
                printPiewControl1.jobNum = jobNum;
                printPiewControl1.num = num;
                printPiewControl1.PrinterObject = lipo;
                printPiewControl1.OnBtnClose += PrintPiewControl1_OnBtnClose;
                printPiewControl1.onBtnPrint += PrintPiewControl1_onBtnPrint;
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintPiewControl1_onBtnPrint(object sender,EventArgs obj)
        {
            printTo = true;
            this.Close();
        }

        private void PrintPiewControl1_OnBtnClose(object sender,EventArgs obj)
        {
            this.Close();
        }
    }
}
