using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
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
    public partial class setJsonKey : Form
    {
        public setJsonKey()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txb_fileName.Text != "")
                {
                    string name = txb_fileName.Text;
                    string fileV = "info";
                    if (chk_entpy.Checked)
                    {
                        fileV = "cfg";
                    }
                    var dbs = data_View.DataSource as BindingSource;
                    jsonKeySave jks = new jsonKeySave();
                    foreach (var item in dbs.List)
                    {
                        jks.add(item as jsonKeySave.DataItem);
                    }
                    using (FileStream file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\jsonXml\\"
                    +fileV+"\\"+ name  + ".xml", FileMode.OpenOrCreate))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(jsonKeySave));
                        if (file.Length > 0)
                        {
                            file.SetLength(0);
                            file.Seek(0, 0);
                        }
                        xml.Serialize(file, jks);
                    }
                    DialogResult dr= MessageBox.Show("保存成功！是否直接退出界面！", "提示信息", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("请输入名称");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileo = new OpenFileDialog();
                fileo.ShowDialog();
                string fileName = fileo.FileName;
                if (fileName != "")
                {
                    using (FileStream file = new FileStream(fileName, FileMode.Open))
                    {

                        txb_fileName.Text = fileo.SafeFileName.Substring(0,fileo.SafeFileName.Length - 4);
                        if (file.Length > 0)
                        {
                            XmlSerializer xml = new XmlSerializer(typeof(jsonKeySave));
                            var result = xml.Deserialize(file) as jsonKeySave;

                            BindingSource dbs = new BindingSource();
                            foreach (var item in result.list)
                            {
                                dbs.List.Add(item);
                            }
                            data_View.DataSource = dbs;
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
