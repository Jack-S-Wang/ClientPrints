using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using System.Windows.Forms;

namespace ClinetPrints.SettingWindows
{
    public partial class groupName : Form
    {
        public groupName()
        {
            InitializeComponent();
        }
        public string name = "";
        private void groupName_KeyDown(object sender, KeyEventArgs e)
        {
            new addCommend(SharMethod.user, "groupName_KeyDown", "键盘确认键");
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txb_groupText.Text != "")
                {
                    name = txb_groupText.Text.Trim();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("请先定义组名再保存！");
                    return;
                }
            }
        }

        private void txb_groupText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 47)
            {
                e.Handled = true;
                MessageBox.Show("/该符号已做特殊符号不能使用！");
            }
        }

    }
}
