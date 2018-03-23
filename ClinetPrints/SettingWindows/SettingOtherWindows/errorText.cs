using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    public partial class errorText : Form
    {
        public errorText()
        {
            InitializeComponent();
        }
        public string filepath = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                getFileMessage();
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void getFileMessage()
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(printerError));
                using (var fs = new FileStream(filepath, FileMode.Open))
                {
                    var pe = xml.Deserialize(fs) as printerError;
                    if (pe != null)
                    {
                        listView1.Items.Clear();
                        for (int i = 0; i < pe.Items.Count; i++)
                        {
                            ListViewItem item = new ListViewItem();
                            item.SubItems[0].Text = pe.Items[i].time;
                            item.SubItems.Add(pe.Items[i].message);
                            listView1.Items.Add(item);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("没有该错误信息的文档！");
            }
        }

        private void errorText_Load(object sender, EventArgs e)
        {
            try
            {
                getFileMessage();
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }
    }
}
