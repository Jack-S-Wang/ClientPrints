using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using static ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass.ServerSettingObject;

namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    public partial class ServerSetting : Form
    {
        public ServerSetting()
        {
            InitializeComponent();
        }
        FileStream file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\SeverSetting.xml", FileMode.OpenOrCreate);
        XmlSerializer xml = new XmlSerializer(new TcpSObject().GetType());
        private void ServerSetting_Load(object sender, EventArgs e)
        {
            try
            {
                if (file.Length != 0)
                {
                    var result = xml.Deserialize(file);
                    if (result != null)
                    {
                        txb_Ip.Text = (result as TcpSObject).TcpIp;
                        txb_port.Text = (result as TcpSObject).TcpPort;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void btn_sure_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, btn_sure.Name, "确认保存服务设置");
            try
            {

                if (txb_Ip.Text != "" && txb_port.Text != "")
                {

                    TcpSObject tcps = new TcpSObject()
                    {
                        TcpIp = txb_Ip.Text,
                        TcpPort = txb_port.Text
                    };
                    if (file.Length > 0)
                    {
                        file.SetLength(0);
                        file.Seek(0, SeekOrigin.Begin);
                    }
                    xml.Serialize(file, tcps);
                    MessageBox.Show("保存成功！");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_off_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void ServerSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            file.Flush();
            file.Close();
            file.Dispose();
        }
    }
}
