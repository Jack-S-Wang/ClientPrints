using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using ClinetPrints.SettingWindows;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using ClinetPrints.MenuGroupMethod;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using System.Threading;

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

        private void ClientMianWindows_Load(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                printerViewSingle.Enabled = true;
                printerViewSingle.Visible = true;
                printerViewFlcok.Enabled = false;
                printerViewFlcok.Visible = false;
                //添加图片
                AddImage();
                //主程序任务栏中右键显示的控制
                AddMunConten();

                //添加分组的排布
                AddGroupMap();
                //添加打印机信息
                AddPrinterMap();

                //添加群打印机分组排布
                AddFlockGroupMap();
                //添加群打印机
                AddFlockPrinterMap();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region.........//进入页面加载时所使用的方法内容

        /// <summary>
        /// 添加群打印组
        /// </summary>
        private void AddFlockGroupMap()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"./printerXml/groupFlockMap.xml");
            XmlNode xnode = xmlDoc.GetElementsByTagName("printMap")[0];
            TreeNode flockNode = new TreeNode();
            flockNode = this.printerViewFlcok.Nodes.Add("打印机群", "打印机群", 0);
            SharMethod.dicFlockTree.Add("打印机群", flockNode);
            new MenuFlockGroupMethod(flockNode, this);
            if (xnode.ChildNodes.Count > 0)
            {
                TreeNode cnode = new TreeNode();
                foreach (XmlNode xmlNode in xnode.ChildNodes)
                {
                    cnode = flockNode.Nodes.Add(xmlNode.Name, xmlNode.Name, 0);
                    SharMethod.dicFlockTree.Add(xmlNode.Name, cnode);
                    new MenuFlockGroupMethod(cnode, this);
                }
            }
        }

        /// <summary>
        /// 添加分组的排布
        /// </summary>
        private void AddGroupMap()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"./printerXml/groupMap.xml");
            if (xmlDoc.ChildNodes.Count > 0)
            {
                XmlNode xnode = xmlDoc.GetElementsByTagName("printMap")[0];
                GetMapPrints(xnode,1);
                nodeClientPrints = this.printerViewSingle.Nodes.Add("打印机序列", "打印机序列", 0);
                SharMethod.dicTree.Add("打印机序列", nodeClientPrints);
                new MenuGroupAddMethod(nodeClientPrints, this);
                if (SharMethod.limap.Count > 0)
                {
                    //按画布生成节点
                    createTree(nodeClientPrints);
                    SharMethod.limap.Clear();
                }
            }
            else
            {
                //定义树形节点
                nodeClientPrints = this.printerViewSingle.Nodes.Add("打印机序列", "打印机序列", 0);
                SharMethod.dicTree.Add("打印机序列", nodeClientPrints);
                new MenuGroupAddMethod(nodeClientPrints, this);
                TreeNode cnode = nodeClientPrints.Nodes.Add("所有打印机", "所有打印机", 0);
                SharMethod.dicTree.Add("所有打印机", cnode);
                new MenuGroupAddMethod(cnode, this);
            }
        }

        /// <summary>
        /// 添加打印机信息到节点上
        /// </summary>
        private void AddPrinterMap()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"./printerXml/printerMap.xml");
            XmlNode xnode = xmlDoc.GetElementsByTagName("printMap")[0];
            GetMapPrints(xnode, 2);
            xnode = xmlDoc.GetElementsByTagName("printInterface")[0];
            GetMapPrints(xnode, 3);
            //处理所有打印机
            SharMethod.getPrinter();
            createPrintTree(); 
        }

        /// <summary>
        /// 添加打印机到群组上
        /// </summary>
        private void AddFlockPrinterMap()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"./printerXml/printerFlockMap.xml");
            XmlNode xnode = xmlDoc.GetElementsByTagName("printMap")[0];
            foreach (XmlNode node in xnode.ChildNodes)
            {
                SharMethod.liprinterFlockMap.Add(node.Name, node.InnerText);
            }
            //处理所有打印机
            TreeNode cnode;
            foreach (var key in SharMethod.liprinterFlockMap)
            {
                foreach (var parkey in SharMethod.dicFlockTree)
                {
                    if (key.Value == parkey.Key)
                    {
                        cnode = parkey.Value.Nodes.Add(key.Key, SharMethod.dicPrintTree[key.Key].Text, SharMethod.dicPrintTree[key.Key].ImageIndex);
                        cnode.ForeColor = SharMethod.dicPrintTree[key.Key].ForeColor;
                        cnode.SelectedImageIndex = SharMethod.dicPrintTree[key.Key].ImageIndex;
                        var flockPrinter = new PrinterObjects()
                        {
                            onlyAlias = key.Key,
                            ImageIndex = 6,
                            interfaceMessage = SharMethod.liprintInterface[key.Key],
                            stateMessage = "离线",
                            color = Color.Gray,
                            stateCode = 0
                        };
                        SharMethod.dicFlockPrintTree.Add(key.Key, cnode);
                        new MenuPrinterFlockGroupMethod(cnode, this);
                    }
                }
            }
        }

        /// <summary>
        /// 按布局生成节点
        /// </summary>
        /// <param name="tnode">节点</param>
        private void createTree(TreeNode tnode)
        {
            TreeNode cnode = new TreeNode();
            for (int i = 0; i < SharMethod.limap.Count; i++)
            {
                if (tnode.Name == SharMethod.limap.ElementAt(i).Value)//父类相同则生成子类
                {
                    cnode = tnode.Nodes.Add(SharMethod.limap.ElementAt(i).Key, SharMethod.limap.ElementAt(i).Key, 0);
                    cnode.SelectedImageIndex = 0;
                    SharMethod.dicTree.Add(SharMethod.limap.ElementAt(i).Key, cnode);
                    new MenuGroupAddMethod(cnode,this);
                    if (SharMethod.limap.ContainsValue(cnode.Name))//子类当中是否还有子类
                    {
                        createTree(cnode);
                    }
                }
            }
        }

        /// <summary>
        ///按打印机信息进行布局生成节点
        /// </summary>
        private void createPrintTree()
        {
            var tnode = new TreeNode();
            var cnode = new TreeNode();
            foreach (var key in SharMethod.dicPrinterAll)
            {
                if (SharMethod.dicTree.ContainsKey(key.Value))
                {
                    tnode = SharMethod.dicTree[key.Value];
                    cnode = tnode.Nodes.Add(key.Key.onlyAlias, key.Key.interfaceMessage, key.Key.ImageIndex);
                    cnode.SelectedImageIndex = key.Key.ImageIndex;
                    cnode.ForeColor = key.Key.color;
                    SharMethod.dicPrinterObjectTree.Add(key.Key, cnode);
                    SharMethod.dicPrintTree.Add(key.Key.onlyAlias, cnode);
                    new MenuPrinterGroupAddMethod(cnode, this);
                }
            }
            //用完容器则清理
            SharMethod.dicPrinterAll.Clear();
        }

       
        /// <summary>
        /// 从xml文件中获取画布内容
        /// </summary>
        /// <param name="xmlDoc">xml文档的节点</param>
        /// <param name="type">1-分组，2-打印机,3-打印机的界面显示内容</param>
        private void GetMapPrints(XmlNode xmlDoc,int type)
        {
            if (type == 1)
            {
                foreach (XmlNode node in xmlDoc.ChildNodes)
                {
                    SharMethod.limap.Add(node.Name, node.InnerText);
                }
            }
            else if (type == 2)
            {
                foreach (XmlNode node in xmlDoc.ChildNodes)
                {
                    SharMethod.liprintmap.Add(node.Name, node.InnerText);
                }
            }
            else
            {
                foreach (XmlNode node in xmlDoc.ChildNodes)
                {
                    SharMethod.liprintInterface.Add(node.Name, node.InnerText);
                }
            }
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
            printerViewFlcok.ImageList = imageList1;
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
                            if (SharMethod.dicTree.ContainsKey(gn.name))
                            {
                                //递归的找到父类，从第一层开始展开
                                seriatimExpand(SharMethod.dicTree[gn.name]);
                            }
                            else
                            {
                                MessageBox.Show("未找到对应的组名信息！");
                            }
                        }
                        else
                        {
                            if (SharMethod.dicFlockTree.ContainsKey(gn.name))
                            {
                                //递归的找到父类，从第一层开始展开
                                seriatimExpand(SharMethod.dicFlockTree[gn.name]);
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

        /// <summary>
        /// 用递归的方式找到最顶级的父类，从父类开始逐一地全部展开到所要查找的节点
        /// </summary>
        /// <param name="node"></param>
        private void seriatimExpand(TreeNode node)
        {
            if (node.Parent != null)
            {
                seriatimExpand(node.Parent);
                node.Expand();
            }
            else
            {
                node.Expand();
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
                printerViewFlcok.ExpandAll();
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
                printerViewFlcok.CollapseAll();
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
            printerViewFlcok.Enabled = false;
            printerViewFlcok.Visible = false;
        }

        private void 群打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printerViewSingle.Enabled = false;
            printerViewSingle.Visible = false;
            printerViewFlcok.Enabled = true;
            printerViewFlcok.Visible = true;
        }

        private void btn_SelectImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.txb_pathImage.Text = ofd.FileName;
                    SharMethod.pathImage = ofd.FileName;
                    Image image = Image.FromFile(this.txb_pathImage.Text);
                    this.pB_image.Image = image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region....//单打印节点的控制方法
        private void printerViewSingle_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        public Form Information;
        private void printerViewSingle_MouseLeave(object sender, EventArgs e)
        {
            if (Information != null)
                Information.Close();
        }

        private void printerViewSingle_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            if (SharMethod.dicPrinterObjectTree.ContainsValue(e.Node))
            {
                PrinterInformation Info = new PrinterInformation();
                Information = Info;
                Info.AutoSize = true;
                Info.lb_showInformation.AutoSize = true;
                foreach (var key in SharMethod.dicPrinterObjectTree)
                {
                    if (key.Value == e.Node)
                    {
                        Info.printerInformation = key.Key.stateMessage;
                        int width = Info.lb_showInformation.Width;//代表了一行的宽度
                        int height = Info.lb_showInformation.Height;//代表了一行的高度
                        double fontNum = Info.printerInformation.Length;//字体实际长度
                        double stipluteNum = 25;//规定一行显示25个字
                        int ColNum = 0;//列数
                        if ((fontNum / stipluteNum) <= 0)
                        {
                            //小于一行则设置为一行
                            ColNum = 1;
                        }
                        else
                        {
                            if (fontNum % stipluteNum == 0)
                            {
                                ColNum = (int)(fontNum / stipluteNum);
                            }
                            else
                            {
                                ColNum = (int)(fontNum / stipluteNum) + 1;
                            }
                        }
                        double fontWidth = width / fontNum;//一个字的宽度
                        double fontHeight = height;//一个字的高度
                                                   //显示界面的宽度和高度
                        if (ColNum == 1)
                        {
                            Info.Width = width + 10;
                            Info.Height = height + 10;
                        }
                        else
                        {
                            Info.lb_showInformation.Width = (int)fontWidth * 25;
                            Info.lb_showInformation.Height = (int)FontHeight * ColNum;
                            Info.Width = Info.lb_showInformation.Width + 10;
                            Info.Height = Info.lb_showInformation.Height + 10;
                        }
                        Info.Location = this.PointToClient(MousePosition);
                        Info.ShowDialog();
                    }
                }

            }
        }
        #endregion
    }
}
