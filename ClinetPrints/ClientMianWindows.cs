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
using ClinetPrints.sharClass;
using System.Collections.Generic;

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
                printerViewSingle.ShowNodeToolTips = true;
                printerViewFlcok.ShowNodeToolTips = true;
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
                this.printerViewFlcok.Nodes.Add(tnode);
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
                this.printerViewFlcok.Nodes.Add(tnode);
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
            TreeNode tnode=printerViewFlcok.Nodes[0];
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
                            TreeNode[] nodes = printerViewFlcok.Nodes[0].Nodes.Find(gn.name, true);
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
            printerViewSingle.Focus();
        }

        private void 群打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printerViewSingle.Enabled = false;
            printerViewSingle.Visible = false;
            printerViewFlcok.Enabled = true;
            printerViewFlcok.Visible = true;
            printerViewFlcok.Focus();
        }

       

        #region....//节点选择执行方法
        private void printerViewSingle_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node is PrinterTreeNode)
                {
                    var node = e.Node as PrinterTreeNode;
                    this.toolStTxb_printer.Text=node.Text;
                    addfile = 0;
                    //如果已经存在该列则删除该列重新赋值对象
                    if (this.listView1.Columns.Count>3)
                    {
                        this.listView1.Columns.RemoveAt(3);
                    }
                    this.listView1.Columns.Add(new listViewColumnTreeNode(node));
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
                    if (addfile > 10)
                    {
                        MessageBox.Show("最多只能添加十个作业！");
                        return;
                    }
                    OpenFileDialog openfile = new OpenFileDialog();
                    openfile.ShowDialog();
                    imageSubItems.Images.Add(new Bitmap(openfile.FileName));
                    //添加作业的时候加的图片
                    this.listView1.SmallImageList = imageSubItems;
                    Interlocked.Increment(ref addfile);
                    ListViewItem item = new ListViewItem(new ListViewSubItem[] { new ListViewSubItem(),new ListViewSubItem(),new ListViewSubItem()}, addfile-1);
                    item.SubItems[1].Text = addfile.ToString();
                    item.SubItems[2].Text = openfile.FileName;
                    item.Name = (addfile - 1).ToString();
                    this.listView1.Items.Add(item);
                    this.listView1.Items[addfile-1].ToolTipText = openfile.FileName;
                }else
                {
                    MessageBox.Show("请先选择打印机！");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void toolStBtn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lv_item.Count>0)
                {
                    foreach(var item in lv_item)
                    {
                        imageSubItems.Images.RemoveAt(Int32.Parse(item.Name));
                        item.Remove();
                        Interlocked.Decrement(ref addfile);
                    }

                }else
                {
                    DialogResult dr = MessageBox.Show("是要全部删除吗？否则请选择要删除的图片！", "提示警告！", MessageBoxButtons.OK);
                    if (dr == DialogResult.OK)
                    {
                        listView1.Items.Clear();
                        addfile = 0;
                        imageSubItems.Images.Clear();
                    }
                }
                //处理完毕必须滞空
                lv_item.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStbtn_moveUp_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStBtn_moveNext_Click(object sender, EventArgs e)
        {
            try
            {

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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static List<ListViewItem> lv_item = new List<ListViewItem>();
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if (e.Item != null)
                {
                    lv_item.Add(e.Item);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
