using System;
using System.Windows.Forms;
using ClinetPrints.SettingWindows;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using System.Threading;
using ClientPrintsObjectsAll.ClientPrints.Objects.treeNodeObject;

namespace ClinetPrints.MenuGroupMethod
{
    class MenuPrinterGroupAddMethod
    {
        public MenuPrinterGroupAddMethod(TreeNode tnode, ClientMianWindows clientForm)
        {
            TreeNode nodeParSingle = clientForm.printerViewSingle.Nodes[0];
            TreeNode nodeParFlock = clientForm.printerViewFlock.Nodes[0];
            //打印机重命名
            MenuItem rename = new MenuItem("重命名");
            //打印机移位
            MenuItem remove = new MenuItem("移位");
            //删除打印机
            MenuItem clearPrinter = new MenuItem("删除");
            //打印
            MenuItem printData = new MenuItem("打印");
            //打印机添加到群
            MenuItem menu5 = new MenuItem("添加到群");
            rename.Click += (o, e) =>
            {
                groupName na = new groupName();
                na.Owner = ClientMianWindows.ActiveForm;
                na.StartPosition = FormStartPosition.CenterParent;
                na.Text = "打印机重命名";
                na.ShowDialog();
                try
                {
                    if (na.name != "")
                    {
                        if (tnode is PrinterTreeNode)
                        {
                            var nod = tnode as PrinterTreeNode;
                            string reName = nod.rename(na.name);
                            var result = nodeParFlock.Nodes.Find(tnode.Name, true);
                            if (result.Length > 0)
                            {
                                result[0].Text = reName;
                            }
                            var file=SharMethod.FileCreateMethod(SharMethod.SINGLE);
                            SharMethod.SavePrinter(nodeParSingle, file);
                            file = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                            SharMethod.SavePrinter(nodeParFlock, file);
                        }else
                        {
                            clientForm.showException("该对象不是打印机!");
                        }
                    }
                    else
                    {
                        clientForm.showException("不能定义为空值！");
                    }
                }
                catch (Exception ex)
                {
                    clientForm.showException(ex.Message);
                }
            };
            remove.Click += (o, e) =>
            {
                var threadShow = new Thread((d) =>
                  {
                      RemoveToOther removeTo = new RemoveToOther(tnode,clientForm);
                      removeTo.PtNode = nodeParSingle;
                      removeTo.Enabled = true;
                      removeTo.StartPosition = FormStartPosition.CenterParent;
                      removeTo.ShowDialog();
                      var thread = Thread.CurrentThread;
                      thread.Abort();
                  });
                threadShow.Start();
            };
            
            clearPrinter.Click += (o, e) =>
            {
                if (tnode is PrinterTreeNode)
                {
                    var node = tnode as PrinterTreeNode;

                    if (node.StateCode.ToString().Equals("0"))
                    {
                        DialogResult dr = clientForm.showException("确认删除该设备，下次该设备启用时将需要重新分配位置", "提示警告", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            if (nodeParFlock.Nodes.Find(tnode.Name, true).Length > 0)
                            {
                                dr = clientForm.showException("群打印中也有该设备，也需要删除群中的该设备吗？", "提示警告", MessageBoxButtons.OKCancel);
                                if (dr == DialogResult.OK)
                                {
                                    TreeNode flocknode = nodeParFlock.Nodes.Find(tnode.Name, true)[0];
                                    flocknode.Remove();
                                    var fileflock=SharMethod.FileCreateMethod(SharMethod.FLOCK);
                                    SharMethod.SavePrinter(nodeParFlock, fileflock);
                                }
                            }
                            tnode.Remove();
                            var file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                            SharMethod.SavePrinter(nodeParSingle, file);
                        }
                    }
                    else
                    {
                        clientForm.showException("不是离线设备不能删除！");
                        return;

                    }
                }else
                {
                    clientForm.showException("该对象不是打印机！");
                }
            };
            printData.Click += (o, e) =>
            {
                if (SharMethod.pathImage != "")
                {
                    var node = tnode as PrinterTreeNode;
                    if (node.StateCode.ToString().Equals("0"))
                    {
                        clientForm.showException("离线设备不能打印！");
                        return;
                    }else
                    {
                        var method = node.PrinterObject.MethodsObject as PrintersGeneralFunction;
                        if(method.writeDataToDev(SharMethod.pathImage, node.PrinterObject))
                        {

                        }
                    }
                }
                else
                {
                    clientForm.showException("请先选择图片再打印！");
                }
            };
            
            foreach(TreeNode nod in nodeParFlock.Nodes)
            {
                if (nod.Name != "打印机群")
                {
                    MenuItem groupMenu = new MenuItem(nod.Name);
                    TreeNode flockNode = nod;
                    groupMenu.Click += (o, e) =>
                    {
                        var np = tnode as PrinterTreeNode;
                        if (nodeParFlock.Nodes.Find(tnode.Name,true).Length<=0)
                        {
                            PrinterTreeNode cnode;
                            if (np.StateCode.ToString().Equals("0"))
                            {
                                 cnode = new PrinterTreeNode(np.Name, np.Text);
                            }else
                            {
                                 cnode = new PrinterTreeNode(np.PrinterObject);
                            }
                            flockNode.Nodes.Add(cnode);
                            new MenuPrinterFlockGroupMethod(cnode, clientForm);
                        }else
                        {
                            DialogResult dr = clientForm.showException("该打印机已经分配到一个组中，是否将它分配到现在的组中？", "提示警告", MessageBoxButtons.OKCancel);
                            if (dr == DialogResult.OK)
                            {
                                PrinterTreeNode cnode;
                                if (np.StateCode.ToString().Equals("0"))
                                {
                                    cnode = new PrinterTreeNode(np.Name, np.Text);
                                }
                                else
                                {
                                    cnode = new PrinterTreeNode(np.PrinterObject);
                                }
                                nodeParFlock.Nodes.Find(tnode.Name, true)[0].Remove();
                                flockNode.Nodes.Add(cnode);
                                new MenuPrinterFlockGroupMethod(cnode, clientForm); 
                            }
                        }
                        var file=SharMethod.FileCreateMethod(SharMethod.FLOCK);
                        SharMethod.SavePrinter(nodeParFlock, file);
                    };
                    menu5.MenuItems.Add(groupMenu);
                }
            }
            tnode.ContextMenu = new ContextMenu(new MenuItem[] { rename, remove, clearPrinter, printData, menu5 });
        }

    }
}
