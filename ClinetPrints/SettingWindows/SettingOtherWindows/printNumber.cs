using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    public partial class printNumber : Form
    {
        public printNumber()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 确认是否要全部更新
        /// </summary>
        public bool checkVal = false;
        private void ckb_allOrSing_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_allOrSing.Checked)
            {
                checkVal = true;
            }
            else
            {
                checkVal = false;
            }
        }
        /// <summary>
        /// 确认要执行的任务数
        /// </summary>
        public string num = "";
        private void button1_Click(object sender, EventArgs e)
        {
            num = UpDown_num.Value.ToString();
            MessageBox.Show("设置完成！");
            this.Close();
        }

        private void printNumber_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (num == "")
                num = "1";
        }
    }
}
