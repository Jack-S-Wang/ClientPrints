using System;
using System.Windows.Forms;
using ClinetPrints.SettingWindows;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using System.Threading;
using ClientPrintsObjectsAll.ClientPrints.Objects.treeNodeObject;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using System.Drawing;

namespace ClinetPrints.MenuGroupMethod
{
    class MenuPrinterGroupAddMethod
    {
        public MenuPrinterGroupAddMethod(TreeNode tnode, ClientMianWindows clientForm)
        {
            try
            {
                TreeNode nodeParSingle = clientForm.printerViewSingle.Nodes[0];
                TreeNode nodeParFlock = clientForm.printerViewFlock.Nodes[0];
                //打印机重命名
                MenuItem rename = new MenuItem("重命名");
                //打印机移位
                MenuItem remove = new MenuItem("移位");
                //删除打印机
                MenuItem clearPrinter = new MenuItem("删除");
                //重新获取打印设置信息
                MenuItem reGetprintData = new MenuItem("重新获取打印设置信息");
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
                                if (clientForm.toolStTxb_printer.Text == nod.Text)
                                {
                                    clientForm.toolStTxb_printer.Text = na.name;
                                }
                                string reName = nod.rename(na.name);
                                var result = nodeParFlock.Nodes.Find(tnode.Name, true);
                                if (result.Length > 0)
                                {
                                    result[0].Text = reName;
                                }
                                var file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                                SharMethod.SavePrinter(nodeParSingle, file);
                                file = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                                SharMethod.SavePrinter(nodeParFlock, file);
                            }
                            else
                            {
                                clientForm.showException("该对象不是打印机!");
                            }
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
                          RemoveToOther removeTo = new RemoveToOther(tnode, clientForm);
                          removeTo.PtNode = nodeParSingle;
                          removeTo.Enabled = true;
                          removeTo.StartPosition = FormStartPosition.CenterParent;
                          removeTo.ShowDialog();
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
                                        var flocknode = nodeParFlock.Nodes.Find(tnode.Name, true)[0] as PrinterTreeNode;
                                        (flocknode.Parent as GroupTreeNode).Remove(flocknode);
                                        var fileflock = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                                        SharMethod.SavePrinter(nodeParFlock, fileflock);
                                    }
                                }
                                (tnode.Parent as GroupTreeNode).Remove(node);
                                var file = SharMethod.FileCreateMethod(SharMethod.SINGLE);
                                SharMethod.SavePrinter(nodeParSingle, file);
                            }
                        }
                        else
                        {
                            clientForm.showException("不是离线设备不能删除！");
                            return;

                        }
                    }
                    else
                    {
                        clientForm.showException("该对象不是打印机！");
                    }
                };
                reGetprintData.Click += (o, e) =>
                {
                    if ((tnode as PrinterTreeNode).PrinterObject.listviewItemObject != null)
                    {
                        if (clientForm.addfile > 0)
                        {
                            //将原来存在的信息记录下来，以便打印出问题时可以直接获取重新设置
                            var col = clientForm.listView1.Columns[4] as listViewColumnTNode;
                            if (col.ColTnode != null)//说明刚才选中的是单打印机
                            {
                                col.liPrinter[0].listviewImages.Clear();
                                col.liPrinter[0].listviewItemObject.Clear();
                                for (int i = 0; i < clientForm.listView1.Items.Count; i++)
                                {
                                    col.liPrinter[0].listviewItemObject.Add(clientForm.listView1.Items[i]); ;

                                }
                                foreach (Image key in clientForm.imageSubItems.Images)
                                {
                                    col.liPrinter[0].listviewImages.Add(key);
                                }
                            }
                        }
                        if (clientForm.listView1.Columns.Count > 4)
                        {
                            clientForm.listView1.Columns.RemoveAt(4);
                            clientForm.listView1.Columns.Add(new listViewColumnTNode(tnode as PrinterTreeNode));
                            clientForm.toolStTxb_printer.Text = (tnode as PrinterTreeNode).Text;
                            var item = (tnode as PrinterTreeNode).PrinterObject.listviewItemObject;
                            clientForm.imageSubItems.Images.Clear();
                            foreach (Image key in (tnode as PrinterTreeNode).PrinterObject.listviewImages)
                            {
                                clientForm.imageSubItems.Images.Add(key);
                            }
                            clientForm.listView1.SmallImageList = clientForm.imageSubItems;
                            clientForm.listView1.Items.Clear();
                            for (int i = 0; i < item.Count; i++)
                            {
                                clientForm.listView1.Items.Add(item[i]);
                            }
                            clientForm.addfile = item.Count;
                        }
                    }
                };

                foreach (TreeNode nod in nodeParFlock.Nodes)
                {
                    if (nod.Name != "打印机群")
                    {
                        MenuItem groupMenu = new MenuItem(nod.Name);
                        TreeNode flockNode = nod;
                        groupMenu.Click += (o, e) =>
                        {
                            var np = tnode as PrinterTreeNode;
                            if (flockNode.Nodes.ContainsKey(np.Name))
                            {
                                return;
                            }
                            if (nodeParFlock.Nodes.Find(tnode.Name, true).Length <= 0)
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
                                (flockNode as GroupTreeNode).Add(cnode);
                                new MenuPrinterFlockGroupMethod(cnode, clientForm);
                                //检查群打印记录中是否属于同一个组，是则将对象添加进去
                                if (clientForm.liVewF != null)
                                {
                                    if (flockNode.Equals(clientForm.liVewF.ColGroupNode))
                                    {
                                        clientForm.liVewF.liPrinter.Add(cnode.PrinterObject);
                                    }
                                }
                            }
                            else
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
                                    SharMethod.ForTopEachNode(nodeParFlock.Nodes.Find(tnode.Name, true)[0].Parent, (n) =>
                                    {
                                        if (n != null)
                                        {
                                            n.BackColor = Color.White;
                                        }
                                    });
                                    (nodeParFlock.Nodes.Find(tnode.Name, true)[0].Parent as GroupTreeNode).Remove((nodeParFlock.Nodes.Find(tnode.Name, true)[0] as PrinterTreeNode));
                                    (flockNode as GroupTreeNode).Add(cnode);
                                    new MenuPrinterFlockGroupMethod(cnode, clientForm);
                                    //检查群打印记录中是否属于同一个组，是则将对象添加进去
                                    if (clientForm.liVewF != null)
                                    {
                                        if (flockNode.Equals(clientForm.liVewF.ColGroupNode) && !clientForm.liVewF.liPrinter.Contains(cnode.PrinterObject))
                                        {
                                            clientForm.liVewF.liPrinter.Add(cnode.PrinterObject);
                                        }
                                    }
                                }
                            }
                            var file = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                            SharMethod.SavePrinter(nodeParFlock, file);
                        };
                        menu5.MenuItems.Add(groupMenu);
                    }
                }
                tnode.ContextMenu = new ContextMenu(new MenuItem[] { rename, remove, clearPrinter, reGetprintData, menu5 });
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

    }
}
