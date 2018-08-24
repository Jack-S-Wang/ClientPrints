using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinetPrints.SettingWindows
{
    public partial class RemoveJobNum : Form
    {
        public RemoveJobNum()
        {
            InitializeComponent();
        }

        public int index = -1;
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
            new addCommend(SharMethod.user, btn_sure.Name, "移动作业任务位置");
            try
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
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }
    }
}
