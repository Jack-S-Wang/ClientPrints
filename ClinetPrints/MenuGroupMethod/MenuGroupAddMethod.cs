using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClinetPrints.SettingWindows;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using System.Xml.Linq;
using System.Xml;
using ClientPrintsObjectsAll.ClientPrints.Objects.TreeNode;
using System.IO;

namespace ClinetPrints.MenuGroupMethod
{
    class MenuGroupAddMethod
    {
        public MenuGroupAddMethod(TreeNode node,ClientMianWindows clientForm)
        {
            TreeNode nodePar = clientForm.printerViewSingle.Nodes[0];
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
                        if (!node.Nodes.ContainsKey(na.name))//对该节点所对应的组是不允许有重复名，不在同一级是可以的
                        {
                            TreeNode nodeChild = new GroupTreeNode(na.name, 0);
                            node.Nodes.Add(nodeChild);
                            //添加容器中对应的分组信息
                            new MenuGroupAddMethod(nodeChild, clientForm);
                            SharMethod.ForEachNode(nodePar, (nd) =>
                            {
                                if (nd is PrinterTreeNode)
                                {
                                    var newnd=nd as PrinterTreeNode;
                                    new MenuPrinterGroupAddMethod(newnd, clientForm);
                                }
                            });

                            //将处理过的数据获取到并发送给配置文件保存
                            FileStream file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                            SharMethod.SavePrinter(nodePar, file);
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
                            
                            SharMethod.ForEachNode(nodePar, (no) => {
                                if(no is PrinterTreeNode)
                                {
                                    var newno = no as PrinterTreeNode;
                                    no.Remove();
                                    nodePar.Nodes["所有打印机"].Nodes.Add(newno);
                                   
                                }
                            });
                            node.Remove();
                            SharMethod.ForEachNode(nodePar, (no) => {
                                if (no is PrinterTreeNode)
                                {
                                    var newno = no as PrinterTreeNode;
                                    new MenuPrinterGroupAddMethod(newno, clientForm);
                                }
                            });
                            FileStream file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                            SharMethod.SavePrinter(nodePar, file);
                        }
                    }
                    else
                    {
                        node.Remove();
                        SharMethod.ForEachNode(nodePar, (no) => {
                            if (no is PrinterTreeNode)
                            {
                                var newno = no as PrinterTreeNode;
                                new MenuPrinterGroupAddMethod(newno, clientForm);
                            }
                        });
                        FileStream file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                        SharMethod.SavePrinter(nodePar, file);
                    }
                    
                };
                menuItemGroup3.Click += (o, e) =>
                {
                    groupName na = new groupName();
                    na.Owner = ClientMianWindows.ActiveForm;
                    na.StartPosition = FormStartPosition.CenterParent;
                    na.Text = "重命名组名";
                    na.ShowDialog();
                    if (na.name != "")
                    {
                        if (!node.Parent.Nodes.ContainsKey(na.name))
                        {
                            node.Name = na.name;
                            node.Text = na.name;
                            if (node.Nodes.Count > 0)
                            {
                                SharMethod.ForEachNode(nodePar, (no) => {
                                    if (no is PrinterTreeNode)
                                    {
                                        var noGroup = no as PrinterTreeNode;
                                        new MenuPrinterGroupAddMethod(nodePar, clientForm);
                                    }
                                });
                            }
                            var file=SharMethod.FileCreateMethod(SharMethod.SINGLE);
                            SharMethod.SavePrinter(nodePar, file);
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

                node.ContextMenu = new ContextMenu(new MenuItem[] { menuItemGroup1, menuItemGroup2, menuItemGroup3 });
            }
            else
            {
                node.ContextMenu = new ContextMenu(new MenuItem[] { menuItemGroup1 });
            }

        }
       
    }
}
