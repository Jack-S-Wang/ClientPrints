using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrintsObjectsAll.ClientPrints.Objects.treeNodeObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class listViewColumnTNode : ColumnHeader
    {
        private PrinterTreeNode _ColTnode;
        public PrinterTreeNode ColTnode
        {
            get
            {
                return _ColTnode;
            }
            set
            {
                Name = value.Name;
                _ColTnode = value;
                Text = "";
                liPrinter.Add(value.PrinterObject);
            }
        }
        private GroupTreeNode _ColGroupNode;
        public GroupTreeNode ColGroupNode
        {
            get
            {
                return _ColGroupNode;
            }
            set
            {
                Name = value.Name;
                _ColGroupNode = value;
                Text = "";
                foreach (PrinterTreeNode node in value.Nodes)
                {
                    if (node.PrinterObject.stateCode != 0)
                        liPrinter.Add(node.PrinterObject);
                }
            }
        }
        public List<PrinterObjects> liPrinter = new List<PrinterObjects>();

        /// <summary>
        /// 生成一个列标题，记录打印机对象
        /// </summary>
        /// <param name="tnode"></param>
        public listViewColumnTNode(PrinterTreeNode tnode)
        {
            ColTnode = tnode;
        }
        /// <summary>
        /// 生成一个列标题，记录群中所有在线设备对象
        /// </summary>
        /// <param name="tnode"></param>
        public listViewColumnTNode(GroupTreeNode tnode)
        {
            ColGroupNode = tnode;
        }

    }
}
