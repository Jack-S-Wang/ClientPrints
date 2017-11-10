using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClinetPrints.SettingWindows;
using ClientPrints.MethodList.ClientPrints.Method.sharMethod;
using System.Xml.Linq;
using System.Xml;

namespace ClinetPrints.MenuGroupMethod
{
    class MenuGroupAddMethod
    {
        public MenuGroupAddMethod(TreeNode node,ClientMianWindows clientForm)
        {
            //添加控件
            MenuItem menuItemGroup1 = new MenuItem("添加分组");//选择哪个节点就在那个节点上进行分组
            menuItemGroup1.Click += (o, e) =>
            {
                groupName na = new groupName();
                na.Owner = ClientMianWindows.ActiveForm;
                na.StartPosition = FormStartPosition.CenterParent;
                na.ShowDialog();
                try
                {

                    if (na.name != "")
                    {
                        if (!SharMethod.dicTree.ContainsKey(na.name))//是否定义过
                        {
                            TreeNode nodeChild = node.Nodes.Add(na.name, na.name);
                            //添加容器中对应的分组信息
                            SharMethod.dicTree.Add(na.name, nodeChild);
                            MenuGroupAddMethod menu = new MenuGroupAddMethod(nodeChild, clientForm);
                            //将处理过的数据获取到并发送给服务器保存
                            SharMethod.setXmlGroup(nodeChild);
                        }
                        else
                        {
                            clientForm.showException("已经定义过该组名称了！");
                        }
                    }
                    else
                    {
                        clientForm.showException("不能传入空的名称!");
                    }
                }
                catch (Exception ex)
                {

                    clientForm.showException(ex.Message);
                }
            };
            if (node.Name != "打印机序列" && node.Name != "所有打印机")
            {
                MenuItem menuItemGroup2 = new MenuItem("删除该组");
                MenuItem menuItemGroup3 = new MenuItem("重命名");
                
                menuItemGroup2.Click += (o, e) =>
                {
                    if (node.Nodes.Count > 0)
                    {
                        DialogResult dr = clientForm.showException("该组有其他数据，确认将所有的设备放置到所有列表中", "提示信息", MessageBoxButtons.OK);
                        if (dr == DialogResult.OK)
                        {
                            //遍历找到对应要清理的节点名称，并进行容器的清理
                            forTreeNodeName(node);
                            liName.Add(node.Name);
                            //对配置文件中的相关数据进行清理
                            SharMethod.ClearXmlData(liName);
                            foreach (string name in liName)
                            {
                                SharMethod.dicTree.Remove(name);
                            }
                            node.Parent.Nodes.Remove(node);
                        }
                    }
                    else
                    {
                        liName.Add(node.Name);
                        //对配置文件中的相关数据进行清理
                        SharMethod.ClearXmlData(liName);
                        foreach (string name in liName)
                        {
                            SharMethod.dicTree.Remove(name);
                        }
                        node.Parent.Nodes.Remove(node);
                    }
                };

                node.ContextMenu = new ContextMenu(new MenuItem[] { menuItemGroup1, menuItemGroup2, menuItemGroup3 });
            }
            else
            {
                node.ContextMenu = new ContextMenu(new MenuItem[] { menuItemGroup1 });
            }

        }

       

        List<string> liName = new List<string>();
        /// <summary>
        /// 获取要清理的分组名称
        /// </summary>
        /// <param name="tnode"></param>
        private void forTreeNodeName(TreeNode tnode)
        {
            foreach (TreeNode nd in tnode.Nodes)
            {
                if (nd.Nodes.Count > 0)
                {
                    forTreeNodeName(nd);
                }
                if (SharMethod.dicTree.ContainsKey(nd.Name))
                {
                    liName.Add(nd.Name);
                }
            }
        }
       
    }
}
