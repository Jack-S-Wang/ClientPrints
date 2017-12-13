using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    public partial class timeSetting : Form
    {
        public timeSetting()
        {
            InitializeComponent();
        }

        private void txb_time_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        FileStream file = new FileStream(@"./printerXml/printMonitor.xml", FileMode.OpenOrCreate);
        XmlSerializer xml = new XmlSerializer(new monitorTime().GetType());
        private void timeSetting_Load(object sender, EventArgs e)
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(this.panel1, "设置该时间段按所设置的时间进行定时监控设备状态");
            if (file.Length != 0)
            {
                var result = xml.Deserialize(file);
                if (result != null)
                {
                    txb_time.Text = (result as monitorTime).time;
                    dateTimePicker1.Text = (result as monitorTime).Sdate;
                    dateTimePicker2.Text = (result as monitorTime).Edate;

                }
            }
        }

        private void timeSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            file.Flush();
            file.Dispose();
            file.Close();
        }

        private void btn_sureTime_Click(object sender, EventArgs e)
        {
            if (txb_time.Text != "" && dateTimePicker1.Text != "" && dateTimePicker2.Text != "")
            {
                monitorTime mon = new monitorTime()
                {
                    time = txb_time.Text,
                    Sdate = dateTimePicker1.Text,
                    Edate = dateTimePicker2.Text
                };
                SharMethod.monTime = mon;
                if (file.Length > 0)
                {
                    file.SetLength(0);
                    file.Seek(0, SeekOrigin.Begin);
                }
                xml.Serialize(file, mon);
                MessageBox.Show("保存成功！");
                this.Close();
            }
        }
    }
}
