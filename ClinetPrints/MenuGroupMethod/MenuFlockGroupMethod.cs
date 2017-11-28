using ClientPrintsObjectsAll.ClientPrints.Objects.TreeNode;
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
            TreeNode nodeParFlock = clientForm.printerViewFlcok.Nodes[0];
            TreeNode nodeParSingle = clientForm.printerViewSingle.Nodes[0];
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
                        if (!tnode.Nodes.ContainsKey(na.name))//在同一级是否定义过
                        {
                            if (tnode.Nodes.Count > 15)
                            {
                                clientForm.showException("群打印分组最多只有15个组！");
                                return;
                            }
                            TreeNode nodeChild = new GroupTreeNode(na.name,0);
                            tnode.Nodes.Add(nodeChild);
                            new MenuFlockGroupMethod(nodeChild, clientForm);
                            SharMethod.ForEachNode(nodeParFlock, (no) =>
                            {
                                if(no is PrinterTreeNode)
                                {
                                    var newno = no as PrinterTreeNode;
                                    new MenuPrinterFlockGroupMethod(newno, clientForm);
                                }
                            });
                            SharMethod.ForEachNode(nodeParSingle, (no) => {
                                if(no is PrinterTreeNode)
                                {
                                    var singno = no as PrinterTreeNode;
                                    new MenuPrinterGroupAddMethod(singno, clientForm);
                                }
                            });
                            var file = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                            SharMethod.SavePrinter(nodeParFlock, file);
                            file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                            SharMethod.SavePrinter(nodeParSingle, file);                         
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
                MenuItem rename = new MenuItem("重命名");
                MenuItem clearGroup = new MenuItem("删除组");
                MenuItem printData = new MenuItem("打印");
                rename.Click += (o, e) =>
                {
                    groupName na = new groupName();
                    na.Owner = ClientMianWindows.ActiveForm;
                    na.StartPosition = FormStartPosition.CenterParent;
                    na.Text = "重命名组名";
                    na.ShowDialog();
                    if (na.name != "")
                    {
                        if (!tnode.Parent.Nodes.ContainsKey(na.name))
                        {
                            tnode.Name = na.name;
                            tnode.Text = na.name;
                            SharMethod.ForEachNode(nodeParFlock, (nod) => {
                                if(nod is PrinterTreeNode)
                                {
                                    var newnode= nod as PrinterTreeNode;
                                    new MenuPrinterFlockGroupMethod(nodeParFlock, clientForm);
                                }
                            });
                            SharMethod.ForEachNode(nodeParSingle, (nod) => {
                                if(nod is PrinterTreeNode)
                                {
                                    var newnode = nod as PrinterTreeNode;
                                    new MenuPrinterGroupAddMethod(nodeParSingle, clientForm);
                                }
                            });
                            var file = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                            SharMethod.SavePrinter(nodeParFlock, file);
                            file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                            SharMethod.SavePrinter(nodeParSingle, file);
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
                clearGroup.Click += (o, e) =>
                {
                    tnode.Remove();//全部删除子节点的任何打印机信息
                    SharMethod.ForEachNode(nodeParFlock, (nod) =>
                    {
                        if(nod is PrinterTreeNode)
                        {
                            var newnode = nod as PrinterTreeNode;
                            new MenuPrinterFlockGroupMethod(newnode, clientForm);
                        }
                    });
                    SharMethod.ForEachNode(nodeParSingle, (nod) =>
                    {
                        if(nod is PrinterTreeNode)
                        {
                            var newnode = nod as PrinterTreeNode;
                            new MenuPrinterGroupAddMethod(newnode, clientForm);
                        }
                    });
                    var file = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                    SharMethod.SavePrinter(nodeParFlock, file);
                    file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                    SharMethod.SavePrinter(nodeParSingle, file);

                };
                printData.Click += (o, e) =>
                {

                };
                tnode.ContextMenu = new ContextMenu(new MenuItem[] { rename, clearGroup,printData });
            }
        }
    }
}
