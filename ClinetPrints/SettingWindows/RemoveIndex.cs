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
    public partial class RemoveIndex : Form
    {
        public RemoveIndex()
        {
            InitializeComponent();
        }

        public int index=-1;
        public List<int> items = new List<int>();
        private void RemoveIndex_Load(object sender, EventArgs e)
        {

        }

        private void txb_index_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btn_sure_Click(object sender, EventArgs e)
        {

            if (items.Contains(Int32.Parse(txb_index.Text.Trim())))
            {
                index = Int32.Parse(txb_index.Text.Trim());
                this.Close();
            }
            else
            {
                MessageBox.Show("没有对应的作业号,请重新选择！");
            }
        }
    }
}
