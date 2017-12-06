using ClientPrintsObjectsAll.ClientPrints.Objects.treeNodeObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class listViewColumnTNode:ColumnHeader
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
            }
        }
        /// <summary>
        /// 生成一个列标题，记录打印机对象
        /// </summary>
        /// <param name="tnode"></param>
        public listViewColumnTNode(PrinterTreeNode tnode)
        {
            ColTnode = tnode;
        }
    }
}
