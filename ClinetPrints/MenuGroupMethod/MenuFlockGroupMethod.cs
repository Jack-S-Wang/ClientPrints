using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using ClinetPrints.SettingWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClinetPrints.MenuGroupMethod
{
    public class MenuFlockGroupMethod
    {
        public MenuFlockGroupMethod(TreeNode tnode,ClientMianWindows clientForm)
        {
            if (tnode.Name == "打印机群")
            {
                MenuItem menu1 = new MenuItem("添加分组");
                menu1.Click += (o, e) =>
                {
                    groupName na = new groupName();
                    na.Owner = ClientMianWindows.ActiveForm;
                    na.StartPosition = FormStartPosition.CenterParent;
                    na.Text = "添加打印机群组";
                    na.ShowDialog();
                    if (na.name != "")
                    {
                        if (!SharMethod.dicFlockTree.ContainsKey(na.name))//是否定义过
                        {
                            TreeNode nodeChild = tnode.Nodes.Add(na.name, na.name);
                            //添加容器中对应的分组信息
                            SharMethod.dicFlockTree.Add(na.name, nodeChild);
                            //将处理过的数据获取到并发送给服务器保存
                            SharMethod.setXmlGroup(nodeChild,2);
                            foreach(var key in SharMethod.dicPrintTree)
                            {
                                new MenuPrinterGroupAddMethod(key.Value, clientForm);
                            }
                            foreach(var key in SharMethod.dicFlockPrintTree)
                            {
                            new MenuPrinterFlockGroupMethod(key.Value, clientForm);
                            }
                            new MenuFlockGroupMethod(nodeChild, clientForm);
                        }
                        else
                        {
                            clientForm.showException("已经定义过该组名称了！");
                        }
                    }
                };
                tnode.ContextMenu = new ContextMenu(new MenuItem[] { menu1});
            }
            else
            {
                MenuItem menu2 = new MenuItem("重命名");
                MenuItem menu3 = new MenuItem("删除组");
                MenuItem menu4 = new MenuItem("打印");
                menu2.Click += (o, e) =>
                {
                    groupName na = new groupName();
                    na.Owner = ClientMianWindows.ActiveForm;
                    na.StartPosition = FormStartPosition.CenterParent;
                    na.Text = "重命名组名";
                    na.ShowDialog();
                    if (na.name != "")
                    {
                        if (!SharMethod.dicFlockTree.ContainsKey(na.name))
                        {
                            string oldname = tnode.Name;
                            SharMethod.dicFlockTree.Remove(oldname);
                            tnode.Name = na.name;
                            tnode.Text = na.name;
                            SharMethod.dicFlockTree.Add(na.name, tnode);
                            Dictionary<string, string> dicGroupxml = new Dictionary<string, string>();
                            if (tnode.Nodes.Count > 0)
                            {
                                Dictionary<string, string> dicPrintxml = new Dictionary<string, string>();
                                foreach (TreeNode cnode in tnode.Nodes)
                                {
                                    if (SharMethod.dicFlockPrintTree.ContainsKey(cnode.Name))//如果是打印机信息
                                    {
                                        dicPrintxml.Add(cnode.Name, na.name);
                                    }
                                }
                                SharMethod.renamePrintXmlGroup(dicPrintxml, 2, 2);
                            }
                            dicGroupxml.Add(oldname, na.name);
                            SharMethod.renameXmlGroup(dicGroupxml, oldname, 2);
                            foreach(var key in SharMethod.dicPrintTree)
                            {
                                new MenuPrinterGroupAddMethod(key.Value, clientForm);
                            }
                           foreach(var key in SharMethod.dicFlockPrintTree)
                            {
                            new MenuPrinterFlockGroupMethod(key.Value, clientForm);
                            }
                            new MenuFlockGroupMethod(tnode, clientForm);
                        }
                        else
                        {
                            clientForm.showException("已经定义过改名称！");
                        }
                    }
                    else
                    {
                        clientForm.showException("值不能为空！");
                    }
                };
                menu3.Click += (o, e) =>
                {
                    //对配置文件中的相关数据进行清理
                    List<string> liname = new List<string>();
                    liname.Add(tnode.Name);
                    SharMethod.ClearXmlData(liname,2);
                    SharMethod.dicFlockTree.Remove(tnode.Name);
                    foreach (string name in liname)
                    {
                        SharMethod.dicFlockPrintTree.Remove(name);
                    }
                    //对存在于该组的打印机重新分配到所有打印机的列表中
                    List<string> dicxml = new List<string>();
                    foreach (TreeNode cnode in tnode.Nodes)
                    {
                        dicxml.Add(cnode.Name);
                    }
                    SharMethod.ClearPrinterXmlGroup(dicxml.ToArray(), 2);
                    TreeNode oldnode = tnode;
                    tnode.Parent.Nodes.Remove(tnode);
                    foreach(var key in SharMethod.dicPrintTree)
                    {
                        new MenuPrinterGroupAddMethod(key.Value, clientForm);
                    }
                };
                menu4.Click += (o, e) =>
                {

                };
                tnode.ContextMenu = new ContextMenu(new MenuItem[] { menu2, menu3,menu4 });
            }
        }
    }
}
