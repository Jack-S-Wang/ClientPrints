using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.treeNodeObject
{
    [Serializable()]
    public class GroupTreeNode:System.Windows.Forms.TreeNode
    {
        public GroupTreeNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
       
        public GroupTreeNode(string text,int imageNum)
        {
            this.Name = text;
            this.Text = text;
            this.ImageIndex = imageNum;
            this.SelectedImageIndex = imageNum;
            ForeColor = System.Drawing.Color.Green;
        }
        /// <summary>
        /// 添加打印机
        /// </summary>
        /// <param name="tnode">打印机对象节点</param>
        /// <returns></returns>
        public int Add(PrinterTreeNode tnode)
        {
            tnode.TagChanged += Tnode_TagChanged;
            return Nodes.Add(tnode);
        }

        /// <summary>
        /// 删除对应的打印机信息
        /// </summary>
        /// <param name="tnode">打印机对象节点</param>
        public void Remove(PrinterTreeNode tnode)
        {
            if (Nodes.Contains(tnode))
            {
                tnode.TagChanged -= Tnode_TagChanged;
                Nodes.Remove(tnode);
            }
        }

        /// <summary>
        /// 打印机Tag值改变时重新进行排序
        /// </summary>
        /// <param name="obj"></param>
        private void Tnode_TagChanged()
        {
            if (TreeView != null)
            {
                TreeView.BeginInvoke(new MethodInvoker(() =>
                {
                    TreeView.BeginUpdate();
                    SortPrinters();
                    TreeView.EndUpdate();
                }));
            }
            else
            {
                SortPrinters();
            }
        }

        private void SortPrinters()
        {
            var groups = Nodes.OfType<GroupTreeNode>().ToArray();
            PrinterTreeNode[] printers = Nodes.OfType<PrinterTreeNode>().ToArray();
            Nodes.Clear();
            Nodes.AddRange(groups);
            Array.Sort(printers);//升序
            Array.Reverse(printers);//倒序
            Nodes.AddRange(printers);
        }

        [NonSerialized()]
        public new string ToolTipText;

        protected override void Deserialize(SerializationInfo serializationInfo, StreamingContext context)
        {
            base.Deserialize(serializationInfo, context);

            foreach (var printer in Nodes.OfType<PrinterTreeNode>())
            {
                printer.TagChanged += Tnode_TagChanged;
            }
        }
    }
}
