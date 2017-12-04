using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class listViewColumnTNode:ColumnHeader
    {
        private TreeNode _ColTnode;
        public ListViewItem ColItem;
        public TreeNode ColTnode
        {
            get
            {
                return _ColTnode;
            }
            set
            {
                Name = value.Name;
                Width = 0;
            }
        }
        /// <summary>
        /// 将对应的节点信息保存在列
        /// </summary>
        /// <param name="tnode"></param>
        public listViewColumnTNode(TreeNode tnode)
        {
            ColTnode = tnode;
        }
        /// <summary>
        /// 保存该项目的所有信息
        /// </summary>
        /// <param name="item"></param>
        public void saveItem(ListViewItem item)
        {
            ColItem = item;
        }
    }
}
