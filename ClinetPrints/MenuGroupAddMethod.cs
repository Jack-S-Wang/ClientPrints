using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lientPrints.MethodList.ClientPrints.Method.Windows;

namespace ClientPrints.MethodList.ClientPrints.Method.MenuItemAddMethod
{
    class MenuGroupAddMethod
    {
        public MenuGroupAddMethod(TreeNode node)
        {
            //添加控件
            MenuItem menuItemGroup1 = new MenuItem("添加分组");//选择哪个节点就在那个节点上进行分组
            MenuItem menuItemGroup2 = new MenuItem("删除该组");
            MenuItem menuItemGroup3 = new MenuItem("重命名");
            menuItemGroup1.Click += (o, e) =>
            {
                groupName na = new groupName();
                na.StartPosition = FormStartPosition.CenterParent;
                na.ShowDialog();
            };
        }
    }
}
