using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
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
            new addCommend(SharMethod.user, button1.Name, "确认执行的任务数");
            num = UpDown_num.Value.ToString();
            MessageBox.Show("设置完成！");
            this.Close();
        }

        private void printNumber_FormClosing(object sender, FormClosingEventArgs e)
        {
            new addCommend(SharMethod.user, "printNumber_FormClosing", "直接退出执行任务数");
            if (num == "")
                num = "1";
        }
        private void printNumber_Load(object sender, EventArgs e)
        {
            try
            {
                UpDown_num.Value = decimal.Parse(num);
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }
    }
}
