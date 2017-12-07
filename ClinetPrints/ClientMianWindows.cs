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
            notifyIcon.Icon = new Icon(@"./IocOrImage/ooopic_1502413293.ico");
            notifyIcon.Text = "打印客户端程序";
            notifyIcon.Visible = true;
            notifyIcon.Click += (o, e) =>
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            };
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
                MessageBox.Show("注册失败:" + code);
            }
            Marshal.FreeHGlobal(buffer);
        }




        private void ClientMianWindows_Load(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                //注册系统检测USB插拔功能
                registerForHandle();
                printerViewSingle.Enabled = true;
                printerViewSingle.Visible = true;
                printerViewFlock.Enabled = false;
                printerViewFlock.Visible = false;
                printerViewSingle.ShowNodeToolTips = true;
                printerViewFlock.ShowNodeToolTips = true;
                listView1.ShowItemToolTips = true;
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 当插拔等其它设备增加减少操作时关闭定时器来阻止定时查询状态而导致的并发问题
        /// </summary>
        private  bool SetTiming = true;
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
                            string dev;
                            if (!SharMethod.dicPrinterUSB.ContainsKey(path))
                            {
                                MessageBox.Show("该上线设备不是得实设备或是暂时获取不到信息，请重新插拔设备进行连接！");
                                return;
                            }
                            if (printerViewSingle.Nodes[0].Nodes.Find(SharMethod.dicPrinterUSB[path].onlyAlias, true).Length > 0)//说明以前已经添加过的
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
                                }
                            }
                            else
                            {
                                TreeNode nNode = new PrinterTreeNode(SharMethod.dicPrinterUSB[path]);
                                dev = nNode.Text;
                                printerViewSingle.Nodes[0].Nodes["所有打印机"].Nodes.Add(nNode);
                                SharMethod.liAllPrinter.Add(SharMethod.dicPrinterUSB[path]);
                                new MenuPrinterGroupAddMethod(nNode, this);
                            }
                            var file=SharMethod.FileCreateMethod(SharMethod.FLOCK);
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
                                SetTiming = false;
                                var node=this.printerViewSingle.Nodes[0].Nodes.Find(SharMethod.dicPrinterUSB[path].onlyAlias, true)[0];
                                if(node is PrinterTreeNode)
                                {
                                    var n = node as PrinterTreeNode;
                                    SharMethod.liAllPrinter.Remove(n.PrinterObject);
                                    n.SetOffline();
                                    if(this.printerViewFlock.Nodes[0].Nodes.Find(SharMethod.dicPrinterUSB[path].onlyAlias, true).Length > 0)
                                    {
                                        node = this.printerViewFlock.Nodes[0].Nodes.Find(SharMethod.dicPrinterUSB[path].onlyAlias, true)[0];
                                        if(node is PrinterTreeNode)
                                        {
                                            var nf = node as PrinterTreeNode;
                                            nf.SetOffline();
                                            var filef=SharMethod.FileCreateMethod(SharMethod.FLOCK);
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
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                string str = string.Format("发生了异常：{0}，追踪异常信息：{1}", ex.Message, ex.StackTrace);
                MessageBox.Show(str);
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
            if (file.Length!=0)
            {
                tnode = bf.Deserialize(file) as GroupTreeNode;
                this.printerViewSingle.Nodes.Add(tnode);
                SharMethod.FileClose(file);
                SharMethod.ForEachNode(tnode, (nod) =>
                {
                    if(nod is GroupTreeNode)
                    {
                        var n = nod as GroupTreeNode;
                        new MenuGroupAddMethod(n, this);
                    }  
                });
            } 
            else
            {
                tnode = new GroupTreeNode("打印机序列",0);
                this.printerViewSingle.Nodes.Add(tnode);
                new MenuGroupAddMethod(tnode, this);
                TreeNode cnode = new GroupTreeNode("所有打印机", 0);
                tnode.Nodes.Add(cnode);
                new MenuGroupAddMethod(cnode, this);
                SharMethod.SavePrinter(tnode, file);
            }
        }

        /// <summary>
        /// 添加打印机信息到节点上
        /// </summary>
        private void AddPrinterMap()
        {
            TreeNode tnode=this.printerViewSingle.Nodes[0];
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
                    var all = tnode.Nodes.Find("所有打印机", false)[0];
                    var cnode = new PrinterTreeNode(keyva);
                    all.Nodes.Add(cnode);
                    new MenuPrinterGroupAddMethod(cnode, this);
                }
            }
            FileStream fileSingle = SharMethod.FileCreateMethod(SharMethod.SINGLE);
            SharMethod.SavePrinter(tnode, fileSingle);
        }

        /// <summary>
        /// 添加打印机到群组上
        /// </summary>
        private void AddFlockPrinterMap()
        {
            TreeNode tnode=printerViewFlock.Nodes[0];
            SharMethod.ForEachNode(tnode, (node) =>
            {
                if (node is PrinterTreeNode)
                {
                    var ptn = node as PrinterTreeNode;
                    ptn.SetOffline();
                    new MenuPrinterFlockGroupMethod(ptn,this);
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
            MenuItem menuItem3 = new MenuItem("退出程序");//这个需要保留的按钮程序
            menuItem1.Click += (o, e) =>
            {
                this.Visible = true;
            };
            menuItem2.Click += (o, e) =>
            {
                this.Hide();
            };
            menuItem3.Click += (o, e) =>
            {
                this.Close();
                this.Dispose();
                Application.ExitThread();
            };
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { menuItem1, menuItem2, menuItem3 });
        }
        /// <summary>
        /// 添加图片
        /// </summary>
        private void AddImage()
        {
            this.imageList1.Images.Add(new Bitmap(@"./IocOrImage/ooopic_1502413453.ico"));//主图
            this.imageList1.Images.Add(new Bitmap(@"./IocOrImage/ooopic_1502413456.ico"));//在线正常
            this.imageList1.Images.Add(new Bitmap(@"./IocOrImage/ooopic_1502413436.ico"));//在线工作中
            this.imageList1.Images.Add(new Bitmap(@"./IocOrImage/ooopic_1502413404.ico"));//在线繁忙
            this.imageList1.Images.Add(new Bitmap(@"./IocOrImage/ooopic_1502413432.ico"));//在线暂停
            this.imageList1.Images.Add(new Bitmap(@"./IocOrImage/ooopic_1502413424.ico"));//在线异常
            this.imageList1.Images.Add(new Bitmap(@"./IocOrImage/ooopic_1502413428.ico"));//离线
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
                            if (nodes.Length>0)
                            {
                                for(int i = 0; i < nodes.Length; i++)
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
                            if (nodes.Length>0)
                            {
                               for(int i = 0; i < nodes.Length; i++)
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
                MessageBox.Show(ex.Message);
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
        public  void showException(string ex)
        {
            MessageBox.Show(ex);
        }

        /// <summary>
        /// 重载对类一些无法抛出的异常信息直接显示在form层显示出来的方法
        /// </summary>
        /// <param name="ex">信息内容</param>
        /// <param name="title">标题信息</param>
        /// <param name="buttons">按钮</param>
        public  DialogResult showException(string ex, string title, MessageBoxButtons buttons)
        {
            DialogResult dr = MessageBox.Show(ex, title, buttons);
            return dr;
        }
        #endregion

        private void 单台打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printerViewSingle.Enabled = true;
            printerViewSingle.Visible = true;
            printerViewFlock.Enabled = false;
            printerViewFlock.Visible = false;
            printerViewSingle.Focus();
        }

        private void 群打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printerViewSingle.Enabled = false;
            printerViewSingle.Visible = false;
            printerViewFlock.Enabled = true;
            printerViewFlock.Visible = true;
            printerViewFlock.Focus();
        }

       

        #region....//节点选择执行方法
        private void printerViewSingle_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node is PrinterTreeNode)
                {
                    if(printerViewSingle.SelectedNode.Name==e.Node.Name)
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
                        if (this.listView1.Columns.Count > 3)
                        {
                            this.listView1.Columns.RemoveAt(3);
                        }
                        this.listView1.Columns.Add(new listViewColumnTNode(node));
                        imageSubItems.Images.Clear();
                        listView1.SmallImageList = imageSubItems;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        #endregion

        /// <summary>
        /// 记录当前打印机对象的添加图片数量
        /// </summary>
        private volatile int addfile = 0; 
        private void toolStBtn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {
                    if (addfile > 19)
                    {
                        MessageBox.Show("最多只能添加20个作业！");
                        return;
                    }
                    OpenFileDialog openfile = new OpenFileDialog();
                    openfile.ShowDialog();
                    imageSubItems.Images.Add(new Bitmap(openfile.FileName));
                    //添加作业的时候加的图片
                    this.listView1.SmallImageList = imageSubItems;
                    Interlocked.Increment(ref addfile);
                    ListViewItem item = new ListViewItem(new ListViewSubItem[] { new ListViewSubItem(), new ListViewSubItem(), new ListViewSubItem() }, addfile - 1);
                    item.SubItems[1].Text = addfile.ToString();
                    item.SubItems[2].Text = openfile.FileName;
                    item.Name = (addfile - 1).ToString();
                    this.listView1.Items.Add(item);
                    this.listView1.Items[addfile - 1].ToolTipText = openfile.FileName;
                }
                else
                {
                    MessageBox.Show("请先选择打印机！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void toolStBtn_delete_Click(object sender, EventArgs e)
        {
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
                    MessageBox.Show("请先选择打印机！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                        if (listView1.SelectedItems.ContainsKey("1") && listView1.SelectedItems.Count==1)//说明上面只有一个直接上移
                        {
                            var image=imageSubItems.Images[1];
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
                            RemoveIndex reIndex = new RemoveIndex();
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
                            }else
                            {
                                while (listView1.SelectedItems.Count > 0)
                                {
                                    if (reIndex.index-1 >= Int32.Parse(listView1.SelectedItems[0].Name))
                                    {
                                        MessageBox.Show("上移的位子不能大于当前所选择的项！");
                                        return;
                                    }
                                    var image = imageSubItems.Images[Int32.Parse(listView1.SelectedItems[0].Name)];
                                    imageSubItems.Images.RemoveAt(Int32.Parse(listView1.SelectedItems[0].Name));
                                    insertImage(image, reIndex.index-1);
                                    listView1.SmallImageList = imageSubItems;
                                    var item = listView1.SelectedItems[0];
                                    listView1.SelectedItems[0].Remove();
                                    item.Selected = false;
                                    this.listView1.Items.Insert(reIndex.index-1, item);
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
                    MessageBox.Show("请先选择打印机！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 将对应的图片插入对应的位子
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="index">要插入到位子的索引号</param>
        private void insertImage(Image image,int index)
        {
            List<Image> li = new List<Image>();
           foreach(Image img in imageSubItems.Images)
            {
                li.Add(img);
            }
            imageSubItems.Images.Clear();
           for(int i = 0; i < li.Count;)
            {
                if (i == index)
                {
                    imageSubItems.Images.Add(image);
                    index = -1;
                }else
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
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {
                    if (listView1.SelectedItems.Count == 0) return;
                    if (listView1.SelectedItems.Count > 0)
                    {
                        if (listView1.SelectedItems.ContainsKey((listView1.Items.Count-1).ToString()))
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
                            RemoveIndex reIndex = new RemoveIndex();
                            reIndex.Owner = this;
                            reIndex.StartPosition = FormStartPosition.CenterParent;
                            for(int i=1;i<= listView1.Items.Count; i++)
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
                                    this.listView1.Items.Insert(reIndex.index-1, item);
                                    var image = imageSubItems.Images[Int32.Parse(item.Name)];
                                    imageSubItems.Images.RemoveAt(Int32.Parse(item.Name));
                                    insertImage(image, reIndex.index-1);
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
                    MessageBox.Show("请先选择打印机！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStBtn_monitor_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {
                    var col=listView1.Columns[3] as listViewColumnTNode;
                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        monitorForm monitor = new monitorForm();
                        monitor.StartPosition = FormStartPosition.CenterScreen;
                        monitor.printerObject = col.ColTnode.PrinterObject;
                        monitor.Text = col.ColTnode.Text;
                        monitor.ShowDialog();
                    });
                }
                else
                {
                    MessageBox.Show("请先选择打印机！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStBtn_print_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {

                }
                else
                {
                    MessageBox.Show("请先选择打印机！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStBtn_parmSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.toolStTxb_printer.Text != "")
                {
                    var col = listView1.Columns[3] as listViewColumnTNode;
                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        parmSetting parm = new parmSetting();
                        parm.StartPosition = FormStartPosition.CenterScreen;
                        parm.printerObject = col.ColTnode.PrinterObject;
                        parm.Text = col.ColTnode.Text+"参数设置界面";
                        parm.ShowDialog();
                    });
                }
                else
                {
                    MessageBox.Show("请先选择打印机！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
