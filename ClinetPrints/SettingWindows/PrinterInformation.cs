﻿using System;
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
            timer1.Interval = 10000;
            timer1.Enabled = true;
            this.Close();
        }
        public string printerInformation = "";
        private void PrinterInformation_Load(object sender, EventArgs e)
        {
            this.lb_showInformation.Text = printerInformation;
        }
    }
}
