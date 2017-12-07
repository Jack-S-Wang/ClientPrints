using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace ClinetPrints.CreatContorl
{
    public partial class dataGrieViewControl1 : UserControl
    {
        public dataGrieViewControl1()
        {
            InitializeComponent();
        }

        [DefaultValue(false), Description("为true则可以设计子项为一个控件")]
        public bool creatSubItemContorl;
        [Description("创建一个控件！")]
        public Control SubItemControl { get; set; }
        private void dataGrieViewControl1_Load(object sender, EventArgs e)
        {
            if (creatSubItemContorl)
            {
                ListViewSubItem sub = new ListViewSubItem();

            }
        }
    }
}
