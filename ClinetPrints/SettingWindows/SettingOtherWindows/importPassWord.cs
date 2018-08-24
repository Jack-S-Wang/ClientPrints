using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrintsObjectsAll.ClientPrints.Objects.treeNodeObject;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using ClinetPrints.MenuGroupMethod;
using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    public partial class importPassWord : Form
    {
        public importPassWord()
        {
            InitializeComponent();
        }
        public string pathAddress = "";
        public ClientMianWindows client;
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122)) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btn_sure_Click(object sender, EventArgs e)
        {
            try
            {
                new addCommend(SharMethod.user, btn_sure.Name, "确认输入密码");
                if (this.txb_password.Text != "")
                {
                    string password = txb_password.Text;
                    byte[] data = Encoding.UTF8.GetBytes(password);
                    try
                    {
                        new PrintersGeneralFunction(pathAddress, data);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    if (!SharMethod.dicPrinterObject.ContainsKey(pathAddress))
                    {
                        if (SharMethod.banError.Count > 0)
                        {
                            MessageBox.Show("有设备版本不一致, 需要固件更新！");
                        }
                        if (SharMethod.passwordError.Count > 0)
                        {
                            MessageBox.Show("有设备需要密码才能登录，请到登录界面登录!");
                        }
                        if (SharMethod.banError.Count == 0 && SharMethod.passwordError.Count == 0)
                        {
                            MessageBox.Show("该上线设备不是得实设备或是暂时获取不到信息，请重新插拔设备进行连接！");
                        }
                        return;
                    }
                    SharMethod.liAllPrinter.Add(SharMethod.dicPrinterObject[pathAddress]);
                    string dev;
                    new addCommend(SharMethod.user, "usbs上线", "");
                    client.printerViewSingle.BeginInvoke(new MethodInvoker(() =>
                    {
                        client.printerViewSingle.BeginUpdate();
                        if (client.printerViewSingle.Nodes[0].Nodes.Find(SharMethod.dicPrinterObject[pathAddress].onlyAlias, true).Length > 0)//说明该设备正处于离线状态
                        {
                            var n = client.printerViewSingle.Nodes.Find(SharMethod.dicPrinterObject[pathAddress].onlyAlias, true)[0] as PrinterTreeNode;
                            n.PrinterObject = SharMethod.dicPrinterObject[pathAddress];
                            dev = n.Text;
                            if (client.printerViewFlock.Nodes[0].Nodes.Find(SharMethod.dicPrinterObject[pathAddress].onlyAlias, true).Length > 0)//说明在群里也有该打印机
                            {
                                client.printerViewFlock.BeginInvoke(new MethodInvoker(() =>
                                {
                                    client.printerViewFlock.BeginUpdate();
                                    var nf = client.printerViewFlock.Nodes[0].Nodes.Find(SharMethod.dicPrinterObject[pathAddress].onlyAlias, true)[0] as PrinterTreeNode;
                                    nf.PrinterObject = SharMethod.dicPrinterObject[pathAddress];
                                    var filef = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                                    SharMethod.SavePrinter(client.printerViewFlock.Nodes[0], filef);
                                    //是否是群打印设置里的其中一台设备
                                    if (client.listView1.Columns.Count > ClientMianWindows.colmunObject)//设置列表中有数据
                                    {
                                        var col = client.listView1.Columns[ClientMianWindows.colmunObject] as listViewColumnTNode;
                                        if (col.ColGroupNode != null)//说明是群打印
                                        {
                                            if (col.ColGroupNode.Nodes.ContainsKey(nf.Name))//是否是正在操作设置里的设备
                                            {
                                                col.liPrinter.Add(nf.PrinterObject);
                                            }
                                        }
                                    }
                                    if (client.liVewF != null)//说明可能刚才将群的设置信息已经保存下来了
                                    {
                                        if (client.liVewF.ColGroupNode != null)//说明是群打印
                                        {
                                            if (client.liVewF.ColGroupNode.Nodes.ContainsKey(nf.Name))//是否是正在操作设置里的设备
                                            {
                                                client.liVewF.liPrinter.Add(nf.PrinterObject);
                                            }
                                        }
                                    }
                                    client.printerViewFlock.EndUpdate();
                                }));
                            }
                        }
                        else
                        {

                            var nNode = new PrinterTreeNode(SharMethod.dicPrinterObject[pathAddress]);
                            dev = nNode.Text;
                            (client.printerViewSingle.Nodes[0].Nodes["所有打印机"] as GroupTreeNode).Add(nNode);
                            new MenuPrinterGroupAddMethod(nNode, client);

                        }
                        client.printerViewSingle.EndUpdate();
                    }));
                    var file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                    SharMethod.SavePrinter(client.printerViewSingle.Nodes[0], file);
                    SharMethod.passwordError.Remove(pathAddress);
                    MessageBox.Show("登录成功！");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("这里密码不能为空值");
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void btn_findPassword_Click(object sender, EventArgs e)
        {
            try
            {
                new addCommend(SharMethod.user, btn_findPassword.Name, "找回在该程序设置过的密码");
                string s = "";
                FileStream file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\printPassword.xml", FileMode.OpenOrCreate);
                XmlSerializer xml = new XmlSerializer(new DevPassword().GetType());
                if (file.Length != 0)
                {
                    var result = xml.Deserialize(file) as DevPassword;
                    if (result != null)
                    {
                        var rtm = result.find(pathAddress);
                        if (rtm != null)
                        {
                            string pass = Encoding.UTF8.GetString(rtm.password);
                            this.txb_password.Text = pass;
                        }
                        else
                        {
                            s = "该设备在该程序中未设置过密码，无法获取信息";
                        }
                    }
                    else
                    {
                        s = "配置文件获取失败！";
                    }
                }
                else
                {
                    s = "该设备在该程序中未设置过密码，无法获取信息";
                }
                MessageBox.Show(s);
                file.Flush();
                file.Close();
                file.Dispose();
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }
    }
}
