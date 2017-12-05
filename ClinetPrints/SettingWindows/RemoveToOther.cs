using ClientPrintsObjectsAll.ClientPrints.Objects.treeNodeObject;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using ClinetPrints.MenuGroupMethod;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace ClinetPrints.SettingWindows
{
    public partial class RemoveToOther : Form
    {
        public RemoveToOther(TreeNode printerNode,ClientMianWindows clientForm)
        {
            InitializeComponent();
            this.printerNode = printerNode;
            this.clientForm = clientForm;
        }
        private TreeNode printerNode;
        private ClientMianWindows clientForm;
        /// <summary>
        /// 将对应节点传入
        /// </summary>
        public TreeNode PtNode;

        private void RemoveToOther_Load(object sender, EventArgs e)
        {
            this.listView1.ShowItemToolTips = true;
            this.toolStBtn_back.Enabled = false;
            if (PtNode != null)
            {
                this.listView1.Columns.RemoveAt(0);
                this.listView1.Columns.Add(new RemoveColumns(PtNode));
                SharMethod.ForEachNode(PtNode, (node) =>
                {
                    if (node is GroupTreeNode)
                    {
                        var nod = node as GroupTreeNode;
                        if (nod.Level == 1)
                        {
                            this.listView1.Items.Add(new RemoveItems(nod));
                        }
                    }
                });

            }
        }
        public TreeNode sureNode;
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item != null)
            {
                if (listView1.SelectedItems[0] == e.Item)
                {
                    var item = e.Item as RemoveItems;
                    sureNode = item.itemNode;
                }
           
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        public const int Back = 1;
        /// <summary>
        /// 下一级
        /// </summary>
        public const int Next = 2;
        /// <summary>
        /// 显示各个不同的层次的分组信息
        /// </summary>
        ///<param name="Pnode">父节点</param>
        /// <param name="type">返回与下一级的类型</param>
        private void setViewItems(TreeNode Pnode, int type)
        {

            if (type == Next)
            {
                this.toolStBtn_back.Enabled = true;
            }
            else
            {
                if (Pnode.Level == 0)
                {
                    this.toolStBtn_back.Enabled = false;
                }
            }
            this.listView1.Columns.RemoveAt(0);
            this.listView1.Columns.Add(new RemoveColumns(Pnode));
            this.listView1.Items.Clear();
            SharMethod.ForEachNode(PtNode, (node) =>
            {
                if (node is GroupTreeNode)
                {
                    var nod = node as GroupTreeNode;
                    if (nod.Name != "打印机序列")
                    {
                        if (nod.Parent.Name == Pnode.Name && nod.Level-1 == Pnode.Level)
                        {
                            this.listView1.Items.Add(new RemoveItems(nod));
                            
                        }
                    }
                }
            });
        }

        private void toolStBtn_back_Click(object sender, EventArgs e)
        {
            try
            {
                var col = this.listView1.Columns[0] as RemoveColumns;
                if (col.ColPnode != null)
                {
                    setViewItems(col.ColPnode, Back);
                }
                else
                {
                    setViewItems(col.Coltnode.Parent, Back);
                }
            }
            catch (Exception ex)
            {
                string str = string.Format("获取一个异常{0},跟踪异常内容{1}", ex.Message, ex.StackTrace);
                MessageBox.Show(str);
            }
        }

        private void toolStBtn_next_Click(object sender, EventArgs e)
        {
            try
            {
                if (sureNode != null)
                {
                    setViewItems(sureNode, Next);
                    sureNode = null;
                }
                else
                {
                    MessageBox.Show("请先选择要往下级寻找的组！");
                }
            }
            catch (Exception ex)
            {
                string str = string.Format("获取一个异常{0},跟踪异常内容{1}", ex.Message, ex.StackTrace);
                MessageBox.Show(str);
            }
        }

        private void toolStBtn_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                string str = string.Format("获取一个异常{0},跟踪异常内容{1}", ex.Message, ex.StackTrace);
                MessageBox.Show(str);
            }
        }

        private void toolStBtn_serach_Click(object sender, EventArgs e)
        {
            try
            {
                if (toolStTxb_groupName.Text != "")
                {
                    this.toolStBtn_back.Enabled = true;
                    var col=this.listView1.Columns[0] as RemoveColumns;
                    this.listView1.Columns.RemoveAt(0);
                    this.listView1.Columns.Add(new RemoveColumns(col.Coltnode,PtNode, toolStTxb_groupName.Text.Trim()));
                    this.listView1.Items.Clear();
                    int count = 0;
                    SharMethod.ForEachNode(PtNode, (node) =>
                    {
                        if (node is GroupTreeNode)
                        {
                            var nod = node as GroupTreeNode;
                            if (nod.Name.Contains(toolStTxb_groupName.Text.Trim()))
                            {
                                this.listView1.Items.Add(new RemoveItems(nod));
                                count++;
                            }
                        }
                    });
                    MessageBox.Show("查询到符合的组有：" + count+"个");
                }
                else
                {
                    MessageBox.Show("请先模糊输入你要查询的组名或是某个关键字");
                }
            }
            catch (Exception ex)
            {
                string str = string.Format("获取一个异常{0},跟踪异常内容{1}", ex.Message, ex.StackTrace);
                MessageBox.Show(str);
            }
        }

        private void toolStBtn_backTop_Click(object sender, EventArgs e)
        {
            try
            {
                this.toolStBtn_back.Enabled = false;
                this.listView1.Items.Clear();
                this.listView1.Columns.RemoveAt(0);
                this.listView1.Columns.Add(new RemoveColumns(PtNode));
                SharMethod.ForEachNode(PtNode, (node) =>
                {
                    if (node is GroupTreeNode)
                    {
                        var nod = node as GroupTreeNode;
                        if (nod.Level == 1)
                        {
                            this.listView1.Items.Add(new RemoveItems(nod));
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                string str = string.Format("获取一个异常{0},跟踪异常内容{1}", ex.Message, ex.StackTrace);
                MessageBox.Show(str);
            }
        }


        delegate void closeForm();
        delegate DialogResult messageShow(string str);
        private void toolStBtn_sure_Click(object sender, EventArgs e)
        {
            try
            {
                if (sureNode != null)
                {
                    clientForm.printerViewSingle.BeginInvoke(new MethodInvoker (()=>
                    {
                        clientForm.printerViewSingle.BeginUpdate();
                        var cnode = printerNode as PrinterTreeNode;
                        PrinterTreeNode nowNode;
                        if (!sureNode.Nodes.ContainsKey(cnode.Name))
                        {

                            if (cnode.PrinterObject != null)
                            {
                                nowNode = new PrinterTreeNode(cnode.PrinterObject);
                                sureNode.Nodes.Add(nowNode);
                                cnode.Remove();
                            }
                            else
                            {
                                nowNode = new PrinterTreeNode(cnode.Name, cnode.Text);
                                sureNode.Nodes.Add(nowNode);
                                cnode.Remove();
                            }
                            new MenuPrinterGroupAddMethod(nowNode, clientForm);
                            var file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                            SharMethod.SavePrinter(PtNode, file);
                            #region.....//这是设置主程序在其他线程中关闭时调用的委托
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new messageShow(MessageBox.Show), "移位成功！");
                                this.Invoke(new closeForm(this.Close));
                            }
                            #endregion
                        }else
                        {
                            MessageBox.Show("已经存在该打印机了！");
                        }
                        clientForm.printerViewSingle.EndUpdate();
                    }));
                }
            }
            catch (Exception ex)
            {
                string str = string.Format("获取一个异常{0},跟踪异常内容{1}", ex.Message, ex.StackTrace);
                MessageBox.Show(str);
            }
        }

        
    }
    public class RemoveItems : ListViewItem
    {
        private TreeNode _itemNode;
        private List<string> liNode = new List<string>();
        public TreeNode itemNode
        {
            get
            {
                return _itemNode;
            }
            set
            {
                Text = value.Name;
                _itemNode = value;
                SharMethod.ForTopEachNode(value, (n) =>
                {
                    liNode.Add(n.Name);
                });
                string str = "";
                for(int i=liNode.Count-1;i>0 && i < liNode.Count; i--)
                {
                    str += liNode[i] + "/";
                }
                ToolTipText = str;
            }
        }
        /// <summary>
        /// 创建保存节点的Item集合
        /// </summary>
        /// <param name="tnode">树形节点</param>
        public RemoveItems(TreeNode tnode)
        {
            itemNode = tnode;
        }
    }

    public class RemoveColumns : ColumnHeader
    {
        private TreeNode _Coltnode;
        private int count = 0;
        public TreeNode Coltnode
        {
            get
            {
                return _Coltnode;
            }
            set
            {
                foreach(var nod in value.Nodes)
                {
                    if(nod is GroupTreeNode)
                    {
                        count++;
                    }
                }
                Name = value.Text;
                Text = value.Text +" / "+count;
                Width = 297;
                _Coltnode = value;
            }
        }
        public TreeNode ColPnode;
        /// <summary>
        /// 创建保存节点的纵队Column
        /// </summary>
        /// <param name="tnode">树形节点</param>
        public RemoveColumns(TreeNode tnode)
        {
            Coltnode = tnode;
        }

        /// <summary>
        /// 创建查询对象并保存当前进入的节点信息
        /// </summary>
        /// <param name="Pnode">当前点击查询进入的节点信息</param>
        /// <param name="PtNode">根节点</param>
        /// <param name="name">模糊查询的名称</param>
        public RemoveColumns(TreeNode Pnode,TreeNode PtNode,string name)
        {
            SharMethod.ForEachNode(PtNode, (node) =>
            {
                if (node is GroupTreeNode)
                {
                    var nod = node as GroupTreeNode;
                    if (nod.Name.Contains(name))
                    {
                        count++;
                    }
                }
            });
            Name ="模糊查询的组";
            Text = "模糊查询的组 / " + count;
            Width = 297;
            ColPnode = Pnode;
        }
    }
}
