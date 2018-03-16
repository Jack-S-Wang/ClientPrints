using System;
using System.Drawing;
using System.Windows.Forms;
using ClinetPrints.SettingWindows;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using ClinetPrints.MenuGroupMethod;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ClientPrintsObjectsAll.ClientPrints.Objects.treeNodeObject;
using System.Threading;
using static System.Windows.Forms.ListViewItem;
using System.Collections.Generic;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using static System.Windows.Forms.ListView;
using System.Runtime.InteropServices;
using ClientPrsintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using ClinetPrints.SettingWindows.SettingOtherWindows;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using Newtonsoft.Json;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objects.Printers.JSON;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using System.Xml.Serialization;
using ClinetPrints.CreatContorl;
using System.Drawing.Drawing2D;
using System.Speech.Synthesis;
using IWshRuntimeLibrary;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using System.ComponentModel;
using System.Net.Mail;

namespace ClinetPrints
{
    public partial class ClientMianWindows : Form
    {
        //任务栏中所显示的图片
        private System.Windows.Forms.NotifyIcon notifyIcon;

        //树形节点定义
        TreeNode nodeClientPrints = new TreeNode();
        public ClientMianWindows()
        {
            InitializeComponent();
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.ooopic_1502413293;
            notifyIcon.Text = "打印客户端程序";
            notifyIcon.Visible = true;
            notifyIcon.Click += (o, e) =>
            {
                this.Visible = true;
                timer1.Enabled = false;
                notifyIcon.Icon = Properties.Resources.ooopic_1502413293;
                this.WindowState = FormWindowState.Normal;
            };
            SharMethod.user = new UserCommend();

        }


        private IntPtr registration;
        /// <summary>
        /// 网上找的RegisterDevicNotification注册方法
        /// </summary>
        private void registerForHandle()
        {

            DEV_BROADCAST_DEVICEINTERFACE dbd = new DEV_BROADCAST_DEVICEINTERFACE();
            //DEV_BROADCAST_HDR deviceHandle = new DEV_BROADCAST_HDR();
            dbd.type = DBT_DEVTYPE.DBT_DEVTYP_HANDLE;
            dbd.interfaceGuid = DeviceNotifications.GUID_DEVINTERFACE_USBPRINT;
            dbd.reserved = 0;
            dbd.devicePath = string.Empty;
            IntPtr buffer = dbd.allocHGlobal();
            registration = DeviceNotifications.RegisterDeviceNotification(this.Handle, buffer, DeviceNotifications.RDN_FLAGS.DEVICE_NOTIFY_WINDOW_HANDLE);
            if (registration == IntPtr.Zero)
            {
                var code = Marshal.GetLastWin32Error();
                MessageBox.Show("USB检测注册失败:" + code);
            }
            Marshal.FreeHGlobal(buffer);
        }


        /// <summary>
        /// 设置一个定时器，检测打印机实时状态
        /// </summary>
        System.Timers.Timer tiState = new System.Timers.Timer();
        private void ClientMianWindows_Load(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                //注册系统检测USB插拔功能
                registerForHandle();
                string flod = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints";
                if (!System.IO.File.Exists(flod))
                    Directory.CreateDirectory(flod);
                printerViewSingle.Enabled = true;
                printerViewSingle.Visible = true;
                printerViewFlock.Enabled = false;
                printerViewFlock.Visible = false;
                printerViewSingle.ShowNodeToolTips = true;
                printerViewFlock.ShowNodeToolTips = true;
                ToolTip tool = new ToolTip();
                tool.SetToolTip(printerViewSingle, "双击打印机即可查看消息内容！");
                tool.SetToolTip(printerViewFlock, "双击打印机即可查看消息内容！");
                listView1.ShowItemToolTips = true;
                bool imageF = true;
                timer1.Tick += (o, ae) =>
                {
                    if (this.Visible)
                    {
                        notifyIcon.Icon = Properties.Resources.ooopic_1502413293;
                        imageF = true;
                        timer1.Enabled = false;
                        return;
                    }
                    else
                    {
                        if (imageF)
                        {
                            notifyIcon.Icon = Properties.Resources.ooopic_1502413321;
                            imageF = false;
                        }
                        else
                        {
                            notifyIcon.Icon = Properties.Resources.ooopic_1502413293;
                            imageF = true;
                        }
                    }
                };
                //添加图片
                AddImage();
                //主程序任务栏中右键显示的控制
                AddMunConten();
                //添加分组的排布
                AddGroupMap();
                //添加群打印机分组排布
                AddFlockGroupMap();
                //添加打印机信息
                AddPrinterMap();
                //添加群打印机
                AddFlockPrinterMap();
                //获取在某段时间所执行的定时查询
                getMonTime();
                //tiState.Interval = 5000;
                //tiState.Enabled = true;
                //tiState.Elapsed += TiState_Elapsed;
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                MessageBox.Show(ex.Message);
            }
        }



        /// <summary>
        /// 获取在某段时间所执行的定时查询
        /// </summary>
        private void getMonTime()
        {
            using (var file = new FileStream(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\printMonitor.xml", FileMode.OpenOrCreate))
            {
                if (file.Length > 0)
                {
                    var xml = new XmlSerializer(new monitorTime().GetType());
                    var result = xml.Deserialize(file) as monitorTime;
                    SharMethod.monTime = result;
                    if (result.checkedStart)
                    {
                        if (!System.IO.File.Exists(Environment.GetFolderPath(System.Environment.SpecialFolder.Startup) + "\\ClientPrints.lnk"))
                        {
                            string shortcutPath = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Startup), string.Format("{0}.lnk", "ClientPrints"));
                            WshShell shell = new WshShell();
                            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
                            shortcut.TargetPath = Application.ExecutablePath;//指定目标路径
                            shortcut.WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath);//设置起始位置
                            shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
                            shortcut.Save();//保存快捷方式
                        }
                    }
                }
            }

        }



        /// <summary>
        /// 定时执行查询状态事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TiState_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (IsHandleCreated && SetTiming)
                {
                    if (SharMethod.monTime != null)
                    {
                        if (SharMethod.monTime.chekedTime)
                        {
                            var startTime = DateTime.Parse(SharMethod.monTime.Sdate);
                            var endTime = DateTime.Parse(SharMethod.monTime.Edate);
                            if (startTime >= endTime)//说明这个结束时间是第二天的时间
                            {
                                endTime.AddDays(1);
                            }
                            if (DateTime.Now >= startTime && DateTime.Now <= endTime)
                            {
                                if (tiState.Interval != Int32.Parse(SharMethod.monTime.time) * 1000)//不相等就赋值
                                    tiState.Interval = Int32.Parse(SharMethod.monTime.time) * 1000;
                            }
                            else
                            {
                                if (tiState.Interval != 5000)//不相等就赋值,设置回原来的默认值
                                    tiState.Interval = 5000;
                            }
                        }
                    }
                    foreach (var key in SharMethod.liAllPrinter)
                    {
                        if (key.MethodsObject != null)
                        {
                            var method = key.MethodsObject as IMethodObjects;
                            var po = key;
                            string str = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, key.pHandle, new byte[] { 0x30 });
                            if (str != "false")
                            {
                                var keyState = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300State>(str);
                                if (key.stateCode != keyState.stateCode)
                                {
                                    this.printerViewSingle.BeginInvoke(new MethodInvoker(() =>
                                    {
                                        printerViewSingle.BeginUpdate();
                                        key.stateMessage = keyState.majorState + ":" + keyState.StateMessage;
                                        key.state = keyState.majorState;
                                        key.stateCode = keyState.stateCode;
                                        printerViewSingle.EndUpdate();
                                    }));
                                    if (keyState.stateCode == 4 || keyState.stateCode == 5 || keyState.stateCode == 6)
                                    {
                                        Interlocked.Increment(ref errorCount);
                                        SpeechSynthesizer sp = new SpeechSynthesizer();
                                        sp.Rate = 2;
                                        sp.Volume = 20;
                                        sp.SpeakAsync("设备异常");
                                        //errorToMail mail = new errorToMail(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")+" 设备" + po.alias+":"+ po.stateMessage);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
                                        ThreadPool.QueueUserWorkItem((o) =>
                                        {
                                            PrinterInformation pInfo = new PrinterInformation();
                                            pInfo.lb_DevInfo.Text = "设备" + po.alias + "出现了问题，需要处理！";
                                            pInfo.ShowDialog();
                                        });
                                        if (!this.Visible)
                                        {
                                            this.BeginInvoke(new MethodInvoker(() =>
                                            {
                                                timer1.Enabled = true;
                                            }));
                                        }
                                    }
                                    else
                                    {
                                        Interlocked.Decrement(ref errorCount);
                                        if (errorCount == 0)
                                        {
                                            timer1.Enabled = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        /// <summary>
        /// 当插拔等其它设备增加减少操作时关闭定时器来阻止定时查询状态而导致的并发问题
        /// </summary>
        private bool SetTiming = true;
        /// <summary>
        /// 重写WndProc来定义检测USB插拔从而获取打印机实时情况
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == WDevCmdObjects.WM_DEVICECHANGE)
                {
                    if (m.WParam.ToInt64() == WDevCmdObjects.DBT_DEVICEARRIVAL)//插入
                    {
                        DEV_BROADCAST_HDR hdr = (DEV_BROADCAST_HDR)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_HDR));
                        if (hdr.type == DBT_DEVTYPE.DBT_DEVTYP_DEVICEINTERFACE)
                        {
                            DEV_BROADCAST_DEVICEINTERFACE dbd = new DEV_BROADCAST_DEVICEINTERFACE();
                            dbd.fromIntPtr(m.LParam);
                            string path = dbd.devicePath.ToLower();
                            SetTiming = false;
                            Thread.Sleep(1000);
                            new PrintersGeneralFunction(path);
                            if (!SharMethod.dicPrinterUSB.ContainsKey(path))
                            {
                                MessageBox.Show("设备没有正常获取信息！请断电或重新插拔设备！");
                                return;
                            }
                            SharMethod.liAllPrinter.Add(SharMethod.dicPrinterUSB[path]);
                            string dev;
                            if (!SharMethod.dicPrinterUSB.ContainsKey(path))
                            {
                                MessageBox.Show("该上线设备不是得实设备或是暂时获取不到信息，请重新插拔设备进行连接！");
                                return;
                            }
                            new addCommend(SharMethod.user, "usbs上线", "");
                            if (printerViewSingle.Nodes[0].Nodes.Find(SharMethod.dicPrinterUSB[path].onlyAlias, true).Length > 0)//说明该设备正处于离线状态
                            {
                                var n = printerViewSingle.Nodes.Find(SharMethod.dicPrinterUSB[path].onlyAlias, true)[0] as PrinterTreeNode;
                                n.PrinterObject = SharMethod.dicPrinterUSB[path];
                                dev = n.Text;
                                if (printerViewFlock.Nodes[0].Nodes.Find(SharMethod.dicPrinterUSB[path].onlyAlias, true).Length > 0)//说明在群里也有该打印机
                                {
                                    var nf = printerViewFlock.Nodes[0].Nodes.Find(SharMethod.dicPrinterUSB[path].onlyAlias, true)[0] as PrinterTreeNode;
                                    nf.PrinterObject = SharMethod.dicPrinterUSB[path];
                                    var filef = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                                    SharMethod.SavePrinter(printerViewFlock.Nodes[0], filef);
                                    //是否是群打印设置里的其中一台设备
                                    if (this.listView1.Columns.Count > colmunObject)//设置列表中有数据
                                    {
                                        var col = this.listView1.Columns[colmunObject] as listViewColumnTNode;
                                        if (col.ColGroupNode != null)//说明是群打印
                                        {
                                            if (col.ColGroupNode.Nodes.ContainsKey(nf.Name))//是否是正在操作设置里的设备
                                            {
                                                col.liPrinter.Add(nf.PrinterObject);
                                            }
                                        }
                                    }
                                    if (liVewF != null)//说明可能刚才将群的设置信息已经保存下来了
                                    {
                                        if (liVewF.ColGroupNode != null)//说明是群打印
                                        {
                                            if (liVewF.ColGroupNode.Nodes.ContainsKey(nf.Name))//是否是正在操作设置里的设备
                                            {
                                                liVewF.liPrinter.Add(nf.PrinterObject);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var nNode = new PrinterTreeNode(SharMethod.dicPrinterUSB[path]);
                                dev = nNode.Text;
                                (printerViewSingle.Nodes[0].Nodes["所有打印机"] as GroupTreeNode).Add(nNode);
                                new MenuPrinterGroupAddMethod(nNode, this);
                            }
                            var file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                            SharMethod.SavePrinter(printerViewSingle.Nodes[0], file);

                            Thread.Sleep(100);
                            ThreadPool.QueueUserWorkItem((o) =>
                            {
                                PrinterInformation pInfo = new PrinterInformation();
                                pInfo.lb_DevInfo.Text = "设备:" + dev + "已上线！";
                                pInfo.ShowDialog();
                            });
                        }
                    }
                    else if (m.WParam.ToInt64() == WDevCmdObjects.DBT_DEVICEREMOVECOMPLETE)//拔出
                    {
                        DEV_BROADCAST_HDR hdr = (DEV_BROADCAST_HDR)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_HDR));
                        if (hdr.type == DBT_DEVTYPE.DBT_DEVTYP_DEVICEINTERFACE)
                        {
                            DEV_BROADCAST_DEVICEINTERFACE dbd = new DEV_BROADCAST_DEVICEINTERFACE();
                            dbd.fromIntPtr(m.LParam);
                            string path = dbd.devicePath.ToLower();
                            if (SharMethod.dicPrinterUSB.ContainsKey(path))
                            {
                                new addCommend(SharMethod.user, "usb下线", "");
                                SetTiming = false;
                                var node = this.printerViewSingle.Nodes[0].Nodes.Find(SharMethod.dicPrinterUSB[path].onlyAlias, true)[0];
                                if (node is PrinterTreeNode)
                                {
                                    var n = node as PrinterTreeNode;
                                    //删除已下线设备的所有的设置内容
                                    if (this.listView1.Columns.Count > colmunObject)//设置列表里有数据
                                    {
                                        var col = this.listView1.Columns[colmunObject] as listViewColumnTNode;
                                        if (col.ColTnode == n)//说明刚才选中的是离线的那台打印机
                                        {
                                            this.toolStTxb_printer.Text = "";
                                            this.listView1.Columns.RemoveAt(colmunObject);
                                            this.listView1.Items.Clear();
                                            addfile = 0;

                                        }
                                        else//判断是否是群里的最后一台设备下线了，全下线则清理，否则就只删除下线设备的对象不清理
                                        {
                                            if (col.ColGroupNode.Nodes.ContainsKey(n.Name))//是否存在该下线设备
                                            {
                                                int index = 0;
                                                for (int i = 0; i < col.liPrinter.Count; ++i)
                                                {
                                                    if (col.liPrinter[i] == n.PrinterObject)
                                                    {
                                                        index = i;
                                                        break;
                                                    }
                                                }
                                                col.liPrinter.RemoveAt(index);
                                                if (col.liPrinter.Count == 0)
                                                {
                                                    this.toolStTxb_printer.Text = "";
                                                    this.listView1.Columns.RemoveAt(colmunObject);
                                                    this.listView1.Items.Clear();
                                                    addfile = 0;
                                                }
                                            }
                                        }
                                    }
                                    if (liVewF != null)//记录群中是否也有该设备
                                    {
                                        if (liVewF.ColGroupNode != null)
                                        {
                                            if (liVewF.ColGroupNode.Nodes.ContainsKey(n.Name))//是否存在该下线设备
                                            {
                                                int index = 0;
                                                for (int i = 0; i < liVewF.liPrinter.Count; ++i)
                                                {
                                                    if (liVewF.liPrinter[i] == n.PrinterObject)
                                                    {
                                                        index = i;
                                                        break;
                                                    }
                                                }
                                                liVewF.liPrinter.RemoveAt(index);
                                                if (liVewF.liPrinter.Count == 0)
                                                {
                                                    liNameF = "";
                                                    liItemF.Clear();
                                                    liVeIamgeF.Clear();
                                                    liVewF = null;
                                                }
                                            }
                                        }
                                    }
                                    if (liVewS != null)//记录中单打印机是否是该设备
                                    {
                                        if (liVewS.ColTnode.Name == n.Name)//说明是该下线设备
                                        {
                                            liVewS = null;
                                            liVeIamgeS.Clear();
                                            liItemS.Clear();
                                            liNameS = "";
                                        }
                                    }

                                    SharMethod.liAllPrinter.Remove(n.PrinterObject);
                                    n.SetOffline();
                                    if (this.printerViewFlock.Nodes[0].Nodes.Find(SharMethod.dicPrinterUSB[path].onlyAlias, true).Length > 0)
                                    {
                                        node = this.printerViewFlock.Nodes[0].Nodes.Find(SharMethod.dicPrinterUSB[path].onlyAlias, true)[0];
                                        if (node is PrinterTreeNode)
                                        {
                                            var nf = node as PrinterTreeNode;
                                            nf.SetOffline();
                                            nf.showToMove = false;
                                            var filef = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                                            SharMethod.SavePrinter(this.printerViewFlock.Nodes[0], filef);
                                        }
                                    }
                                    var file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                                    SharMethod.SavePrinter(this.printerViewSingle.Nodes[0], file);
                                    string dev = n.Text;
                                    SharMethod.dicPrinterUSB.Remove(path);
                                    Thread.Sleep(100);
                                    ThreadPool.QueueUserWorkItem((o) =>
                                    {
                                        PrinterInformation pInfo = new PrinterInformation();
                                        pInfo.lb_DevInfo.Text = "设备:" + dev + "已下线！";
                                        pInfo.ShowDialog();
                                    });
                                }
                            }
                        }
                    }
                    SetTiming = true;
                }
                try
                {
                    base.WndProc(ref m);
                }
                catch (ThreadAbortException ex)
                {
                    Thread.ResetAbort();
                    MessageBox.Show("ThreadAbortException : " + ex.GetHashCode().ToString());
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                MessageBox.Show(ex.Message);
            }
        }


        #region.........//进入页面加载时所使用的方法内容

        /// <summary>
        /// 添加群打印组
        /// </summary>
        private void AddFlockGroupMap()
        {
            var bf = new BinaryFormatter();
            TreeNode tnode;
            FileStream file = SharMethod.FileCreateMethod(SharMethod.FLOCK);
            if (file.Length != 0)
            {
                tnode = bf.Deserialize(file) as GroupTreeNode;
                this.printerViewFlock.Nodes.Add(tnode);
                SharMethod.FileClose(file);
                SharMethod.ForEachNode(tnode, (nod) =>
                {
                    if (nod is GroupTreeNode)
                    {
                        var n = nod as GroupTreeNode;
                        n.BackColor = Color.White;
                        new MenuFlockGroupMethod(n, this);
                    }
                });
            }
            else
            {
                tnode = new GroupTreeNode("打印机群", 0);
                this.printerViewFlock.Nodes.Add(tnode);
                new MenuFlockGroupMethod(tnode, this);
                SharMethod.SavePrinter(tnode, file);
            }
        }

        /// <summary>
        /// 添加分组的排布
        /// </summary>
        private void AddGroupMap()
        {
            var bf = new BinaryFormatter();
            TreeNode tnode;
            FileStream file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
            if (file.Length != 0)
            {
                tnode = bf.Deserialize(file) as GroupTreeNode;
                this.printerViewSingle.Nodes.Add(tnode);
                SharMethod.FileClose(file);
                SharMethod.ForEachNode(tnode, (nod) =>
                {
                    if (nod is GroupTreeNode)
                    {
                        var n = nod as GroupTreeNode;
                        n.BackColor = Color.White;
                        new MenuGroupAddMethod(n, this);
                    }
                });
            }
            else
            {
                tnode = new GroupTreeNode("打印机序列", 0);
                this.printerViewSingle.Nodes.Add(tnode);
                new MenuGroupAddMethod(tnode, this);
                TreeNode cnode = new GroupTreeNode("所有打印机", 0);
                tnode.Nodes.Add(cnode);
                new MenuGroupAddMethod(cnode, this);
                SharMethod.SavePrinter(tnode, file);
            }
        }
        private volatile int errorCount = 0;
        /// <summary>
        /// 添加打印机信息到节点上
        /// </summary>
        private void AddPrinterMap()
        {
            TreeNode tnode = this.printerViewSingle.Nodes[0];
            //处理所有打印机
            SharMethod.getPrinter();
            SharMethod.ForEachNode(tnode, (node) =>
            {
                if (node is PrinterTreeNode)
                {
                    var ptn = node as PrinterTreeNode;
                    ptn.SetOffline();
                    new MenuPrinterGroupAddMethod(ptn, this);
                }
            });

            foreach (var keyva in SharMethod.liAllPrinter)
            {
                var results = tnode.Nodes.Find(keyva.onlyAlias, true);
                if (results.Length > 0)
                {
                    if (results[0] is PrinterTreeNode)
                    {
                        (results[0] as PrinterTreeNode).PrinterObject = keyva;
                        new MenuPrinterGroupAddMethod(results[0], this);
                    }
                }
                else
                {
                    GroupTreeNode all = tnode.Nodes.Find("所有打印机", false)[0] as GroupTreeNode;
                    var cnode = new PrinterTreeNode(keyva);
                    all.Add(cnode);
                    new MenuPrinterGroupAddMethod(cnode, this);
                }
                if (keyva.stateCode == 4 || keyva.stateCode == 5 || keyva.stateCode == 6)
                {
                    Interlocked.Increment(ref errorCount);
                }
            }
            if (errorCount > 0)
            {
                timer1.Enabled = true;
            }
            FileStream fileSingle = SharMethod.FileCreateMethod(SharMethod.SINGLE);
            SharMethod.SavePrinter(tnode, fileSingle);
        }

        /// <summary>
        /// 添加打印机到群组上
        /// </summary>
        private void AddFlockPrinterMap()
        {
            TreeNode tnode = printerViewFlock.Nodes[0];
            SharMethod.ForEachNode(tnode, (node) =>
            {
                if (node is PrinterTreeNode)
                {
                    var ptn = node as PrinterTreeNode;
                    ptn.SetOffline();
                    new MenuPrinterFlockGroupMethod(ptn, this);
                }
            });
            foreach (var keyva in SharMethod.liAllPrinter)
            {
                var results = tnode.Nodes.Find(keyva.onlyAlias, true);
                if (results.Length > 0)
                {
                    if (results[0] is PrinterTreeNode)
                    {
                        (results[0] as PrinterTreeNode).PrinterObject = keyva;
                        new MenuPrinterFlockGroupMethod(results[0], this);
                    }
                }
            }
            FileStream file = SharMethod.FileCreateMethod(SharMethod.FLOCK);
            SharMethod.SavePrinter(tnode, file);
        }

        /// <summary>
        /// 主程序右键所控制的按钮控件
        /// </summary>
        private void AddMunConten()
        {
            MenuItem menuItem1 = new MenuItem("显示窗体");
            MenuItem menuItem2 = new MenuItem("隐藏窗体");
            MenuItem promotionDev = new MenuItem("不一致版本固件更新");
            MenuItem menSet = new MenuItem("设置");
            MenuItem menuItem3 = new MenuItem("退出程序");//这个需要保留的按钮程序
            menuItem1.Click += (o, e) =>
            {
                new addCommend(SharMethod.user, menuItem1.Name, menuItem1.Text);
                timer1.Enabled = false;
                notifyIcon.Icon = Properties.Resources.ooopic_1502413293;
                this.Visible = true;
            };
            menuItem2.Click += (o, e) =>
            {
                new addCommend(SharMethod.user, menuItem2.Name, menuItem2.Text);
                this.Hide();
            };
            promotionDev.Click += (o, e) =>
            {
                new addCommend(SharMethod.user, promotionDev.Name, promotionDev.Text);
                Thread thread = new Thread(() =>
                  {
                      DevPromotion dp = new DevPromotion();
                      dp.StartPosition = FormStartPosition.CenterScreen;
                      dp.ShowDialog();
                  });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            };
            menSet.Click += (o, e) =>
            {
                new addCommend(SharMethod.user, menSet.Name, menSet.Text);
                (new Thread(() =>
                {
                    timeSetting set = new timeSetting();
                    set.StartPosition = FormStartPosition.CenterScreen;
                    set.ShowDialog();
                })).Start();
            };
            menuItem3.Click += (o, e) =>
            {
                new addCommend(SharMethod.user, menuItem3.Name, menuItem3.Text);
                timer1.Enabled = false;
                this.Dispose();
                printerClose.closeWindow = true;
                Application.Exit();
            };
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { menuItem1, menuItem2, promotionDev, menSet, menuItem3 });
        }
        /// <summary>
        /// 添加图片
        /// </summary>
        private void AddImage()
        {
            this.imageList1.Images.Add(Properties.Resources.ooopic_1502413453);//主图
            this.imageList1.Images.Add(Properties.Resources.ooopic_1502413456);//在线正常
            this.imageList1.Images.Add(Properties.Resources.ooopic_1502413436);//在线工作中
            this.imageList1.Images.Add(Properties.Resources.ooopic_1502413404);//在线繁忙
            this.imageList1.Images.Add(Properties.Resources.ooopic_1502413432);//在线暂停
            this.imageList1.Images.Add(Properties.Resources.ooopic_1502413424);//在线异常
            this.imageList1.Images.Add(Properties.Resources.ooopic_1502413428);//离线
            printerViewSingle.ImageList = imageList1;
            printerViewFlock.ImageList = imageList1;

        }

        #endregion
        /// <summary>
        /// 按下关闭那个窗体按钮不是真的关闭，真关闭在退出程序的按钮上执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientMianWindows_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        static object ObjectLock = new object();
        private void 分组名称查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, 分组名称查询ToolStripMenuItem.Name, 分组名称查询ToolStripMenuItem.Text);
            try
            {
                lock (ObjectLock)
                {
                    groupName gn = new groupName();
                    gn.Owner = this;
                    gn.StartPosition = FormStartPosition.CenterParent;
                    gn.Text = "快速查询分组信息";
                    gn.ShowDialog();
                    if (gn.name != "")
                    {
                        if (printerViewSingle.Enabled)
                        {
                            TreeNode[] nodes = printerViewSingle.Nodes[0].Nodes.Find(gn.name, true);
                            if (nodes.Length > 0)
                            {
                                for (int i = 0; i < nodes.Length; i++)
                                {
                                    nodes[i].EnsureVisible();
                                }
                            }
                            else
                            {
                                MessageBox.Show("未找到对应的组名信息！");
                            }
                        }
                        else
                        {
                            TreeNode[] nodes = printerViewFlock.Nodes[0].Nodes.Find(gn.name, true);
                            if (nodes.Length > 0)
                            {
                                for (int i = 0; i < nodes.Length; i++)
                                {
                                    nodes[i].EnsureVisible();
                                }
                            }
                            else
                            {
                                MessageBox.Show("未找到对应的组名信息！");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("输入的信息不能为空！");
                    }
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }



        private void 全部展开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printerViewSingle.Enabled)
            {
                printerViewSingle.ExpandAll();
            }
            else
            {
                printerViewFlock.ExpandAll();
            }
        }

        private void 全部折叠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printerViewSingle.Enabled)
            {
                printerViewSingle.CollapseAll();
            }
            else
            {
                printerViewFlock.CollapseAll();
            }
        }
        #region.......//对类一些显示内容进行显示
        /// <summary>
        /// 对类一些无法抛出的异常信息直接显示在form层显示出来
        /// </summary>
        /// <param name="ex"></param>
        public void showException(string ex)
        {
            MessageBox.Show(ex);
        }

        /// <summary>
        /// 重载对类一些无法抛出的异常信息直接显示在form层显示出来的方法
        /// </summary>
        /// <param name="ex">信息内容</param>
        /// <param name="title">标题信息</param>
        /// <param name="buttons">按钮</param>
        public DialogResult showException(string ex, string title, MessageBoxButtons buttons)
        {
            DialogResult dr = MessageBox.Show(ex, title, buttons);
            return dr;
        }
        #endregion
        #region....//切换单打印与群打印时所记录的信息
        public listViewColumnTNode liVewF = null;
        public List<Image> liVeIamgeF = new List<Image>();
        public List<ListViewItem> liItemF = new List<ListViewItem>();
        private bool sToF = false;
        public string liNameF = "";
        public listViewColumnTNode liVewS = null;
        public List<Image> liVeIamgeS = new List<Image>();
        public List<ListViewItem> liItemS = new List<ListViewItem>();
        public string liNameS = "";
        private bool fToS = false;
        #endregion
        private void 单台打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, 单台打印ToolStripMenuItem.Name, 单台打印ToolStripMenuItem.Text);
            try
            {
                if (printerViewSingle.Enabled == false && listView1.Columns.Count > colmunObject)
                {
                    liVeIamgeF.Clear();
                    liItemF.Clear();
                    liVewF = listView1.Columns[colmunObject] as listViewColumnTNode;
                    listView1.Columns.RemoveAt(colmunObject);
                    for (int i = 0; i < imageSubItems.Images.Count; i++)
                    {
                        liVeIamgeF.Add(imageSubItems.Images[i]);
                    }
                    foreach (ListViewItem item in listView1.Items)
                    {
                        liItemF.Add(item);
                    }
                    liNameF = toolStTxb_printer.Text;
                    imageSubItems.Images.Clear();
                    listView1.Items.Clear();
                    toolStTxb_printer.Text = "";
                    addfile = 0;
                }
                fToS = true;
                单台打印ToolStripMenuItem.BackColor = Color.Green;
                群打印ToolStripMenuItem.BackColor = Color.White;
                printerViewSingle.Enabled = true;
                printerViewSingle.Visible = true;
                printerViewFlock.Enabled = false;
                printerViewFlock.Visible = false;
                printerViewSingle.Focus();
                toolStBtn_printPerview.Enabled = true;
                if (fToS)
                {
                    if (liVewS != null)
                    {
                        listView1.Columns.Add(liVewS);
                        imageSubItems.Images.Clear();
                        listView1.Items.Clear();
                        foreach (Image img in liVeIamgeS)
                        {
                            imageSubItems.Images.Add(img);
                        }
                        listView1.SmallImageList = imageSubItems;
                        foreach (var item in liItemS)
                        {
                            listView1.Items.Add(item);
                        }
                        addfile = liItemS.Count;
                        toolStTxb_printer.Text = liNameS;
                    }
                }
                fToS = false;
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void 群打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, 群打印ToolStripMenuItem.Name, 群打印ToolStripMenuItem.Text);
            try
            {
                if (printerViewSingle.Enabled && listView1.Columns.Count > colmunObject)
                {
                    liVeIamgeS.Clear();
                    liItemS.Clear();
                    liVewS = listView1.Columns[colmunObject] as listViewColumnTNode;
                    listView1.Columns.RemoveAt(colmunObject);
                    for (int i = 0; i < imageSubItems.Images.Count; i++)
                    {
                        liVeIamgeS.Add(imageSubItems.Images[i]);
                    }
                    foreach (ListViewItem item in listView1.Items)
                    {
                        liItemS.Add(item);
                    }
                    liNameS = toolStTxb_printer.Text;
                    imageSubItems.Images.Clear();
                    listView1.Items.Clear();
                    toolStTxb_printer.Text = "";
                    addfile = 0;
                }
                sToF = true;
                单台打印ToolStripMenuItem.BackColor = Color.White;
                群打印ToolStripMenuItem.BackColor = Color.Green;
                printerViewSingle.Enabled = false;
                printerViewSingle.Visible = false;
                printerViewFlock.Enabled = true;
                printerViewFlock.Visible = true;
                printerViewFlock.Focus();
                toolStBtn_printPerview.Enabled = false;
                if (sToF)//说明是群转换为单打印过来的
                {
                    if (liVewF != null)
                    {
                        int count = 0;
                        var lp = liVewF.liPrinter;
                        if (count == lp.Count)//说明全下线了
                        {
                            return;
                        }
                        listView1.Columns.Add(liVewF);
                        imageSubItems.Images.Clear();
                        listView1.Items.Clear();
                        foreach (Image img in liVeIamgeF)
                        {
                            imageSubItems.Images.Add(img);
                        }
                        listView1.SmallImageList = imageSubItems;
                        foreach (var item in liItemF)
                        {
                            listView1.Items.Add(item);
                        }
                        addfile = liItemF.Count;
                        toolStTxb_printer.Text = liNameF;

                    }
                }
                sToF = false;
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }
        /// <summary>
        /// 设置listview中列保存对象的索引，会随着自定义增加其他列时需要改变，其他列是提前添加的！
        /// </summary>
        public const int colmunObject = 4;

        #region....//节点选择执行方法
        private void printerViewSingle_AfterSelect(object sender, TreeViewEventArgs e)
        {
            new addCommend(SharMethod.user, printerViewSingle.Name, "printerViewSingle_AfterSelect");
            try
            {
                if (e.Node is PrinterTreeNode)
                {
                    if (printerViewSingle.SelectedNode.Name == e.Node.Name)
                    {
                        var node = e.Node as PrinterTreeNode;
                        if (node.StateCode.Equals("0"))
                        {
                            MessageBox.Show("离线设备无法进行设置！");
                            return;
                        }
                        this.toolStTxb_printer.Text = node.Text;
                        addfile = 0;
                        //如果已经存在该列则删除该列重新赋值对象
                        if (this.listView1.Columns.Count > 4)
                        {
                            //将原来存在的信息记录下来，以便打印出问题时可以直接获取重新设置
                            var col = this.listView1.Columns[colmunObject] as listViewColumnTNode;
                            if (col.ColTnode != null)//说明刚才选中的是单打印机
                            {
                                col.liPrinter[0].listviewImages.Clear();
                                col.liPrinter[0].listviewItemObject.Clear();
                                for (int i = 0; i < this.listView1.Items.Count; i++)
                                {
                                    col.liPrinter[0].listviewItemObject.Add(this.listView1.Items[i]);
                                }
                                foreach (Image key in imageSubItems.Images)
                                {
                                    col.liPrinter[0].listviewImages.Add(key);
                                }
                            }
                            this.listView1.Columns.RemoveAt(colmunObject);

                        }
                        this.listView1.Columns.Add(new listViewColumnTNode(node));
                        this.listView1.Items.Clear();
                        imageSubItems.Images.Clear();
                        listView1.SmallImageList = imageSubItems;
                    }
                }
                else
                {
                    if (addfile == 0)
                    {
                        this.toolStTxb_printer.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }

        }

        #endregion

        /// <summary>
        /// 记录当前打印机对象的添加图片数量
        /// </summary>
        public volatile int addfile = 0;
        private void toolStBtn_add_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, toolStBtn_add.Name, toolStBtn_add.Text);
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {
                    if (addfile > 19)
                    {
                        MessageBox.Show("最多只能添加20个任务！");
                        return;
                    }
                    OpenFileDialog openfile = new OpenFileDialog();
                    openfile.ShowDialog();
                    if (openfile.FileName == "")
                    {
                        return;
                    }
                    try
                    {
                        imageSubItems.Images.Add(new Bitmap(openfile.FileName));
                    }
                    catch
                    {
                        MessageBox.Show("添加的图片有误！");
                        return;
                    }

                    //添加作业的时候加的图片
                    this.listView1.SmallImageList = imageSubItems;
                    Interlocked.Increment(ref addfile);
                    ListViewItem item = new ListViewItem(new ListViewSubItem[] { new ListViewSubItem(), new ListViewSubItem(), new ListViewSubItem(), new ListViewSubItem() }, addfile - 1);
                    item.SubItems[1].Text = addfile.ToString();
                    item.SubItems[2].Text = openfile.FileName;
                    item.SubItems[3].Text = "1";
                    item.Name = (addfile - 1).ToString();
                    this.listView1.Items.Add(item);
                    this.listView1.Items[addfile - 1].ToolTipText = openfile.FileName;
                }
                else
                {
                    MessageBox.Show("请先选择打印机或选择群打印组");
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void toolStBtn_delete_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, toolStBtn_delete.Name, toolStBtn_delete.Text);
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {
                    if (listView1.SelectedItems.Count == 0)
                    {
                        DialogResult dr = MessageBox.Show("是要全部删除吗？否则请选择要删除的图片！", "提示警告！", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            listView1.Items.Clear();
                            addfile = 0;
                            imageSubItems.Images.Clear();
                            listView1.SmallImageList = imageSubItems;
                        }
                        return;
                    }
                    while (listView1.SelectedItems.Count > 0)
                    {
                        imageSubItems.Images.RemoveAt(Int32.Parse(listView1.SelectedItems[0].Name));
                        listView1.SmallImageList = imageSubItems;
                        listView1.SelectedItems[0].Remove();
                        Interlocked.Decrement(ref addfile);
                        setItems();
                    }
                }
                else
                {
                    MessageBox.Show("请先选择打印机或选择群打印组");
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }
        /// <summary>
        /// 修改listView中的item对象
        /// </summary>
        private void setItems()
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].Name = i.ToString();
                listView1.Items[i].ImageIndex = i;
                listView1.Items[i].SubItems[1].Text = (i + 1).ToString();
            }
        }

        private void toolStbtn_moveUp_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, toolStbtn_moveUp.Name, toolStbtn_moveUp.Text);
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {
                    if (listView1.SelectedItems.Count == 0) return;
                    if (listView1.SelectedItems.Count > 0)
                    {
                        if (listView1.SelectedItems.ContainsKey("0"))
                        {
                            MessageBox.Show("选中了是第一个条目是无法上移的，请重新选择！");
                            return;
                        }
                        if (listView1.SelectedItems.ContainsKey("1") && listView1.SelectedItems.Count == 1)//说明上面只有一个直接上移
                        {
                            var image = imageSubItems.Images[1];
                            imageSubItems.Images.RemoveAt(1);
                            insertImage(image, 0);
                            listView1.SmallImageList = imageSubItems;
                            var item = listView1.SelectedItems[0];
                            listView1.SelectedItems[0].Remove();
                            listView1.Items.Insert(0, item);
                            setItems();
                        }
                        else
                        {
                            RemoveJobNum reIndex = new RemoveJobNum();
                            reIndex.Owner = this;
                            reIndex.StartPosition = FormStartPosition.CenterParent;
                            reIndex.Text = "上移到对应的作业号的前面";
                            for (int i = 1; i <= listView1.Items.Count; i++)
                            {
                                reIndex.items.Add(i);
                            }
                            reIndex.ShowDialog();
                            if (reIndex.index == -1)
                            {
                                MessageBox.Show("用户取消了移位操作！");
                                return;
                            }
                            else
                            {
                                while (listView1.SelectedItems.Count > 0)
                                {
                                    if (reIndex.index - 1 >= Int32.Parse(listView1.SelectedItems[0].Name))
                                    {
                                        MessageBox.Show("上移的位子不能大于当前所选择的项！");
                                        return;
                                    }
                                    var image = imageSubItems.Images[Int32.Parse(listView1.SelectedItems[0].Name)];
                                    imageSubItems.Images.RemoveAt(Int32.Parse(listView1.SelectedItems[0].Name));
                                    insertImage(image, reIndex.index - 1);
                                    listView1.SmallImageList = imageSubItems;
                                    var item = listView1.SelectedItems[0];
                                    listView1.SelectedItems[0].Remove();
                                    item.Selected = false;
                                    this.listView1.Items.Insert(reIndex.index - 1, item);
                                }
                                setItems();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先选择要上移的作业");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("请先选择打印机或选择群打印组");
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }
        /// <summary>
        /// 将对应的图片插入对应的位子
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="index">要插入到位子的索引号</param>
        private void insertImage(Image image, int index)
        {
            List<Image> li = new List<Image>();
            foreach (Image img in imageSubItems.Images)
            {
                li.Add(img);
            }
            imageSubItems.Images.Clear();
            for (int i = 0; i < li.Count;)
            {
                if (i == index)
                {
                    imageSubItems.Images.Add(image);
                    index = -1;
                }
                else
                {
                    imageSubItems.Images.Add(li[i]);
                    i++;
                }
            }
            //如果上面遍历没有获取到该图，则可能是要添加到最后一位的
            if (index == li.Count)
            {
                imageSubItems.Images.Add(image);
            }
        }



        private void toolStBtn_moveNext_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, toolStBtn_moveNext.Name, toolStBtn_moveNext.Text);
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {
                    if (listView1.SelectedItems.Count == 0) return;
                    if (listView1.SelectedItems.Count > 0)
                    {
                        if (listView1.SelectedItems.ContainsKey((listView1.Items.Count - 1).ToString()))
                        {
                            MessageBox.Show("选中了是最后一个条目是无法下移的，请重新选择！");
                            return;
                        }
                        if (listView1.SelectedItems.ContainsKey((listView1.Items.Count - 2).ToString()) && listView1.SelectedItems.Count == 1)//说明下面只有一个直接下移
                        {
                            var image = imageSubItems.Images[Int32.Parse(listView1.SelectedItems[0].Name)];
                            imageSubItems.Images.RemoveAt(Int32.Parse(listView1.SelectedItems[0].Name));
                            insertImage(image, imageSubItems.Images.Count);
                            listView1.SmallImageList = imageSubItems;
                            var item = listView1.SelectedItems[0];
                            listView1.SelectedItems[0].Remove();
                            listView1.Items.Add(item);
                            setItems();
                        }
                        else
                        {
                            RemoveJobNum reIndex = new RemoveJobNum();
                            reIndex.Owner = this;
                            reIndex.StartPosition = FormStartPosition.CenterParent;
                            for (int i = 1; i <= listView1.Items.Count; i++)
                            {
                                reIndex.items.Add(i);
                            }

                            reIndex.Text = "下移到对应的作业号的后面";
                            reIndex.ShowDialog();
                            if (reIndex.index == -1)
                            {
                                MessageBox.Show("用户取消了移位操作！");
                                return;
                            }
                            else
                            {
                                while (listView1.SelectedItems.Count > 0)
                                {
                                    if (reIndex.index <= Int32.Parse(listView1.SelectedItems[0].Name))
                                    {
                                        MessageBox.Show("下移的位子不能小于当前选择项的值");
                                        return;
                                    }
                                    var item = listView1.SelectedItems[0];
                                    listView1.SelectedItems[0].Remove();
                                    this.listView1.Items.Insert(reIndex.index - 1, item);
                                    var image = imageSubItems.Images[Int32.Parse(item.Name)];
                                    imageSubItems.Images.RemoveAt(Int32.Parse(item.Name));
                                    insertImage(image, reIndex.index - 1);
                                    listView1.SmallImageList = imageSubItems;
                                    item.Selected = false;
                                }
                                setItems();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先选择要下移的作业");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("请先选择打印机或选择群打印组");
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void toolStBtn_monitor_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, toolStBtn_monitor.Name, toolStBtn_monitor.Text);
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {
                    var col = listView1.Columns[colmunObject] as listViewColumnTNode;
                    for (int i = 0; i < col.liPrinter.Count; i++)
                    {
                        var po = col.liPrinter[i];
                        string str = col.liPrinter[i].alias;
                        Thread thread = new Thread(() =>
                          {
                              monitorForm monitor = new monitorForm();
                              monitor.StartPosition = FormStartPosition.CenterScreen;
                              monitor.printerObject = po;
                              monitor.Text = str + "实时监控控制";
                              monitor.ShowDialog();
                          });
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                    }
                }
                else
                {
                    MessageBox.Show("请先选择打印机或选择群打印组");
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }


        private void toolStBtn_print_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, toolStBtn_print.Name, toolStBtn_print.Text);
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {
                    string jobNum = "";
                    string fileAddress = "";
                    string num = "";
                    List<string>[] LiItems = new List<string>[listView1.Items.Count];
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        List<string> liItem = new List<string>();
                        jobNum = listView1.Items[i].SubItems[1].Text;
                        fileAddress = listView1.Items[i].SubItems[2].Text;
                        num = listView1.Items[i].SubItems[3].Text;
                        liItem.Add(jobNum);
                        liItem.Add(fileAddress);
                        liItem.Add(num);
                        LiItems[i] = liItem;
                    }
                    if (LiItems.Length <= 0)
                    {
                        MessageBox.Show("没有打印任务");
                        return;
                    }

                    var col = (this.listView1.Columns[colmunObject] as listViewColumnTNode).liPrinter;

                    for (int c = 0; c < col.Count; c++)
                    {
                        if (col[c].stateCode == 4 || col[c].stateCode == 5 || col[c].stateCode == 6)
                        {
                            string strc = col[c].alias;
                            ThreadPool.QueueUserWorkItem((o) =>
                            {
                                MessageBox.Show("打印机：" + strc + "状态有问题不能打印！");
                            });
                        }
                        else
                        {
                            if (col[c].pParams.outJobNum >= 65000)
                            {
                                ThreadPool.QueueUserWorkItem((o) =>
                                {
                                    MessageBox.Show("打印工作号缓存数过大，请打印完成之后到监控控制界面进行重启该设备进行释放！！");
                                });
                            }
                            Thread threadPrint = new Thread((printObject =>
                            {
                                var printOb = printObject as object[];
                                var printer = printOb[0] as PrinterObjects;
                                var liItems = printOb[1] as List<string>[];
                                int count = 0;
                                var method = printer.MethodsObject as IMethodObjects;
                                List<string> li = new List<string>();
                                for (int i = 0; i < liItems.Length; i++)
                                {
                                    var filePath = Path.Combine(
                      Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                      "ClientPrints",
                      printer.onlyAlias + " " + DateTime.Now.ToString("yyyyMMdd HH.mm.ss") + ".bmp");
                                    var image = new Bitmap(liItems[i][1]);
                                    Bitmap bmap = new Bitmap(printer.pParams.maxWidth, printer.pParams.maxHeight);
                                    Graphics g = Graphics.FromImage(bmap);
                                    g.SmoothingMode = SmoothingMode.HighQuality;
                                    g.CompositingQuality = CompositingQuality.HighQuality;
                                    g.FillRectangle(Brushes.White, new Rectangle(0, 0, bmap.Width, bmap.Height));
                                    g.DrawImage(image,
                                        new Rectangle(
                                            0,
                                            0,
                                            image.Width,
                                            image.Height),
                                        new Rectangle(0, 0, image.Width, image.Height),
                                        GraphicsUnit.Pixel);
                                    g.Dispose();
                                    bmap.Save(filePath);
                                    //赋值实际打印纸张大小
                                    printer.pParams.printWidth = printer.pParams.maxWidth;
                                    printer.pParams.printHeight = printer.pParams.maxHeight;
                                    List<string> succese = method.writeDataToDev(filePath, printer, liItems[i][0], Int32.Parse(LiItems[i][2]));
                                    count += Int32.Parse(LiItems[i][2]);
                                    if (succese[0].Equals("error"))
                                    {
                                        li.Add(succese[1]);
                                        break;
                                    }

                                }
                                if (li.Count > 0)
                                {
                                    PrinterInformation pi = new PrinterInformation();
                                    pi.lb_DevInfo.Text = "打印失败！" + li[0];
                                    pi.ShowDialog();
                                }
                                else
                                {
                                    printer.pParams.outJobNum += count;
                                    detectionPrint(printer, count);
                                }
                            }));

                            threadPrint.Start(new object[] { col[c], LiItems });
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请先选择打印机或选择群打印组");
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private static void detectionPrint(PrinterObjects printer, int count)
        {
            if (printer.threadObject == null)
            {
                Thread threadmontior = new Thread((ob) =>
                {
                    var obj = ob as object[];
                    PrinterObjects po = obj[0] as PrinterObjects;
                    var me = po.MethodsObject as IMethodObjects;
                    int c = (int)obj[1];
                    while (true)
                    {
                        Thread.Sleep(5000);
                        if (printerClose.closeWindow)
                        {
                            break;
                        }
                    //输出作业
                    var printOutPut = me.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, po.pHandle, new byte[] { 0x33 });
                        if (printOutPut == "false")
                        {
                            MessageBox.Show("打印机已离线或无法获取！");
                            break;
                        }
                        var printOut = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300PrintState>(printOutPut);

                        if (printOut.workIndex == po.pParams.outJobNum)
                        {
                            string str = "设备:" + po.alias + ",所有打印已完成！";
                            PrinterInformation pi = new PrinterInformation();
                            pi.lb_DevInfo.Text = str;
                            pi.ShowDialog();
                            break;
                        }
                    }
                    printer.threadObject = null;
                });
                threadmontior.SetApartmentState(ApartmentState.STA);
                printer.threadObject = threadmontior;
                threadmontior.Start(new object[] { printer, count });
            }
        }

        private void toolStBtn_parmSet_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, toolStBtn_parmSet.Name, toolStBtn_parmSet.Text);
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {
                    MessageBox.Show("打开较慢，请稍等！");
                    var col = listView1.Columns[colmunObject] as listViewColumnTNode;
                    for (int i = 0; i < col.liPrinter.Count; i++)
                    {
                        var po = col.liPrinter[i];
                        string str = col.liPrinter[i].alias;
                        Thread thread = new Thread(() =>
                          {
                              parmSetting parm = new parmSetting();
                              parm.StartPosition = FormStartPosition.CenterScreen;
                              parm.printerObject = po;
                              parm.Text = str + "参数设置界面";
                              parm.ShowDialog();
                          });
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                    }
                }
                else
                {
                    MessageBox.Show("请先选择打印机或选择群打印组");
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void printerViewFlock_AfterSelect(object sender, TreeViewEventArgs e)
        {
            new addCommend(SharMethod.user, printerViewFlock.Name, "printerViewFlock_AfterSelect");
            try
            {
                if (e.Node == printerViewFlock.SelectedNode && e.Node.Name != "打印机群")
                {
                    if (printerViewFlock.SelectedNode is GroupTreeNode)
                    {
                        this.toolStTxb_printer.Text = e.Node.Text;
                        addfile = 0;
                        //如果已经存在该列则删除该列重新赋值对象
                        if (this.listView1.Columns.Count > 4)
                        {
                            //将原来存在的信息记录下来，以便打印出问题时可以直接获取重新设置
                            var col = this.listView1.Columns[colmunObject] as listViewColumnTNode;
                            if (col.ColTnode != null)//说明刚才选中的是单打印机
                            {
                                col.liPrinter[0].listviewItemObject.Clear();
                                col.liPrinter[0].listviewImages.Clear();
                                for (int i = 0; i < this.listView1.Items.Count; i++)
                                {
                                    col.liPrinter[0].listviewItemObject.Add(this.listView1.Items[i]);

                                }
                                foreach (Image key in imageSubItems.Images)
                                {
                                    col.liPrinter[0].listviewImages.Add(key);
                                }
                            }
                            this.listView1.Columns.RemoveAt(colmunObject);
                        }
                        this.listView1.Columns.Add(new listViewColumnTNode((e.Node as GroupTreeNode)));
                        this.listView1.Items.Clear();
                        imageSubItems.Images.Clear();
                        listView1.SmallImageList = imageSubItems;
                    }
                }
                else
                {
                    if (addfile == 0)
                    {
                        toolStTxb_printer.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void toolStBtn_printPerview_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, toolStBtn_printPerview.Name, toolStBtn_printPerview.Text);
            try
            {
                if (this.toolStTxb_printer.Text == "")
                {
                    MessageBox.Show("请先选择打印机或群打印组！");
                    return;
                }
                if (listView1.SelectedItems.Count > 0)
                {
                    var po = (listView1.Columns[4] as listViewColumnTNode).liPrinter;
                    if (po.Count > 1)
                    {
                        MessageBox.Show("群打印不法对图片进行预览设置处理！");
                        return;
                    }
                    string addre = listView1.SelectedItems[0].SubItems[2].Text;
                    string job = listView1.SelectedItems[0].SubItems[1].Text;
                    int num = Int32.Parse(listView1.SelectedItems[0].SubItems[3].Text);
                    Thread thread = new Thread(() =>
                      {
                          printPiewForm pf = new printPiewForm();
                          pf.fileAddress = addre;
                          pf.jobNum = job;
                          pf.num = num;
                          pf.lipo = po[0];
                          pf.ShowDialog();
                          if (pf.printTo)//说明选择的任务进行了打印
                          {
                              Invoke(new Action<object, EventArgs>(toolStBtn_delete_Click), sender, e);
                              po[0].pParams.outJobNum += num;
                              detectionPrint(po[0], num);
                          }
                      });
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
                else
                {
                    MessageBox.Show("请先选择要预览的任务！");
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }



        private void 设置查询时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, 设置查询时间ToolStripMenuItem.Name, 设置查询时间ToolStripMenuItem.Text);
            try
            {
                timeSetting set = new timeSetting();
                set.ShowDialog();

            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, listView1.Name, "listView1_DoubleClick");
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    printNumber pn = new printNumber();
                    pn.num = listView1.SelectedItems[0].SubItems[3].Text;
                    pn.Owner = this;
                    pn.StartPosition = FormStartPosition.CenterParent;
                    pn.ShowDialog();
                    if (pn.checkVal)
                    {
                        foreach (ListViewItem item in listView1.Items)
                        {
                            item.SubItems[3].Text = pn.num;
                        }
                    }
                    else
                    {
                        listView1.SelectedItems[0].SubItems[3].Text = pn.num;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("将错误信息上传到服务中心，以便更快处理！", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                MessageBox.Show("没有服务器，还不能上传！");
            }
        }

        private void printerViewSingle_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            new addCommend(SharMethod.user, printerViewSingle.Name, "printerViewSingle_NodeMouseDoubleClick");
            try
            {
                if (e.Node == printerViewSingle.SelectedNode)
                {
                    if (e.Node is PrinterTreeNode)
                    {
                        var n = e.Node as PrinterTreeNode;
                        if (n.showToMove)
                        {
                            n.showToMove = false;
                        }
                        if (printerViewSingle.SelectedNode.Name == e.Node.Name)
                        {
                            var node = e.Node as PrinterTreeNode;
                            if (node.StateCode.Equals("0"))
                            {
                                MessageBox.Show("离线设备无法进行设置！");
                                return;
                            }
                            this.toolStTxb_printer.Text = node.Text;
                            addfile = 0;
                            //如果已经存在该列则删除该列重新赋值对象
                            if (this.listView1.Columns.Count > 4)
                            {
                                //将原来存在的信息记录下来，以便打印出问题时可以直接获取重新设置
                                var col = this.listView1.Columns[colmunObject] as listViewColumnTNode;
                                if (col.ColTnode != null)//说明刚才选中的是单打印机
                                {
                                    col.liPrinter[0].listviewImages.Clear();
                                    col.liPrinter[0].listviewItemObject.Clear();
                                    for (int i = 0; i < this.listView1.Items.Count; i++)
                                    {
                                        col.liPrinter[0].listviewItemObject.Add(this.listView1.Items[i]);
                                    }
                                    foreach (Image key in imageSubItems.Images)
                                    {
                                        col.liPrinter[0].listviewImages.Add(key);
                                    }
                                }
                                this.listView1.Columns.RemoveAt(colmunObject);

                            }
                            this.listView1.Columns.Add(new listViewColumnTNode(node));
                            this.listView1.Items.Clear();
                            imageSubItems.Images.Clear();
                            listView1.SmallImageList = imageSubItems;
                        }
                        errorText er = new errorText();
                        er.Owner = this;
                        er.StartPosition = FormStartPosition.CenterParent;
                        er.Text = "设备：" + n.Text;
                        er.filepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\" + n.PrinterObject.onlyAlias + ".xml";
                        er.Show();
                    }
                }

            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void printerViewFlock_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            new addCommend(SharMethod.user, printerViewFlock.Name, "printerViewFlock_NodeMouseDoubleClick");
            try
            {
                if (e.Node == printerViewFlock.SelectedNode)
                {
                    if (e.Node is PrinterTreeNode)
                    {
                        var n = e.Node as PrinterTreeNode;
                        if (n.showToMove)
                        {
                            n.showToMove = false;
                        }
                        errorText er = new errorText();
                        er.Owner = this;
                        er.StartPosition = FormStartPosition.CenterParent;
                        er.Text = "设备：" + n.Text;
                        er.filepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\" + n.PrinterObject.onlyAlias + ".xml";
                        er.Show();
                    }
                }
                if (e.Node == printerViewFlock.SelectedNode && e.Node.Name != "打印机群")
                {
                    if (printerViewFlock.SelectedNode is GroupTreeNode)
                    {
                        this.toolStTxb_printer.Text = e.Node.Text;
                        addfile = 0;
                        //如果已经存在该列则删除该列重新赋值对象
                        if (this.listView1.Columns.Count > 4)
                        {
                            //将原来存在的信息记录下来，以便打印出问题时可以直接获取重新设置
                            var col = this.listView1.Columns[colmunObject] as listViewColumnTNode;
                            if (col.ColTnode != null)//说明刚才选中的是单打印机
                            {
                                col.liPrinter[0].listviewItemObject.Clear();
                                col.liPrinter[0].listviewImages.Clear();
                                for (int i = 0; i < this.listView1.Items.Count; i++)
                                {
                                    col.liPrinter[0].listviewItemObject.Add(this.listView1.Items[i]);

                                }
                                foreach (Image key in imageSubItems.Images)
                                {
                                    col.liPrinter[0].listviewImages.Add(key);
                                }
                            }
                            this.listView1.Columns.RemoveAt(colmunObject);
                        }
                        this.listView1.Columns.Add(new listViewColumnTNode((e.Node as GroupTreeNode)));
                        this.listView1.Items.Clear();
                        imageSubItems.Images.Clear();
                        listView1.SmallImageList = imageSubItems;
                    }
                }
                else
                {
                    if (addfile == 0)
                    {
                        toolStTxb_printer.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Dispose();
            printerClose.closeWindow = true;
            Application.Exit();
        }

        //private void toolStripButton1_Click(object sender, EventArgs e)
        //{
        //    var cm=listView1.Columns[colmunObject] as listViewColumnTNode;
        //    var method=cm.liPrinter[0].MethodsObject as IMethodObjects;
        //    method.getRa(cm.liPrinter[0]);
        //}
    }
}
