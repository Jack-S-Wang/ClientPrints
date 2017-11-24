using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClinetPrints.SettingWindows;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrsintsMethodList.ClientPrints.Method.Interfaces;
using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;

namespace ClinetPrints.MenuGroupMethod
{
    class MenuPrinterGroupAddMethod
    {
        public MenuPrinterGroupAddMethod(TreeNode tnode, ClientMianWindows clientForm)
        {
            //对没有设置到xml文档里添加进去
            SharMethod.addPeinterXmlGroup(tnode, 1);
            //打印机重命名
            MenuItem menu1 = new MenuItem("重命名");
            //打印机移位
            MenuItem menu2 = new MenuItem("移位");
            //删除打印机
            MenuItem menu3 = new MenuItem("删除");
            //打印
            MenuItem menu4 = new MenuItem("打印");
            //打印机添加到群
            MenuItem menu5 = new MenuItem("添加到群");
            menu1.Click += (o, e) =>
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
                        foreach (var key in SharMethod.dicPrinterObjectTree)
                        {
                            if (key.Key.onlyAlias == tnode.Name)
                            {
                                Dictionary<string, string> dicxml = new Dictionary<string, string>();
                                key.Key.alias = na.name;
                                key.Key.interfaceMessage = key.Key.alias + "(" + key.Key.model + ")";
                                key.Value.Text=tnode.Text = key.Key.interfaceMessage;
                                dicxml.Add(tnode.Name,tnode.Text);
                                //修改配置文件的信息内容
                                SharMethod.renamePrintXmlGroup(dicxml,1,1);
                                dicxml.Clear();
                                foreach(var flockKey in SharMethod.dicFlockPrinterObjectTree)
                                {
                                    if (key.Key.onlyAlias == flockKey.Key.onlyAlias)
                                    {
                                        flockKey.Key.alias = na.name;
                                        flockKey.Key.interfaceMessage = key.Key.alias + "(" + key.Key.model + ")";
                                        flockKey.Value.Text = key.Key.interfaceMessage;
                                        dicxml.Add(flockKey.Value.Name, flockKey.Value.Text);
                                        //修改配置文件的信息内容
                                        SharMethod.renamePrintXmlGroup(dicxml,1,2);
                                        break;
                                    }
                                }
                                break;
                            }
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
            foreach (var key in SharMethod.dicTree)
            {
                if (key.Key != "打印机序列")
                {
                    MenuItem group = new MenuItem(key.Key);
                    group.Name = key.Key;
                    string groupname = key.Key;
                    group.Click += (o, e) =>
                    {
                        var oldtnode = tnode;
                        //清理对应的打印机节点
                        tnode.Parent.Nodes.Remove(tnode);
                        //再添加到对应的分组上去
                        SharMethod.dicTree[groupname].Nodes.Add(oldtnode);
                        //修改xml文件内容
                        Dictionary<string, string> dicxml = new Dictionary<string, string>();
                        dicxml.Add(oldtnode.Name,groupname );
                        SharMethod.renamePrintXmlGroup(dicxml, 2,1);
                    };
                    menu2.MenuItems.Add(group);
                }
            }
            menu3.Click += (o, e) =>
            {
                Dictionary<PrinterObjects, string> dicClear = new Dictionary<PrinterObjects, string>();
                foreach (var key in SharMethod.dicPrinterObjectTree)
                {
                    if (key.Key.onlyAlias == tnode.Name && key.Key.stateCode==0)
                    {
                        //找到对应要清楚的打印设备
                        dicClear.Add(key.Key, key.Key.onlyAlias);
                    }
                }
                if (dicClear.Count > 0)
                {
                    DialogResult dr = clientForm.showException("确认删除该设备，下次该设备启用时将需要重新分配位置", "提示警告", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        List<string> lname = new List<string>();
                        foreach (var k in dicClear)
                        {
                            //删除两个集合的内容
                            SharMethod.dicPrintTree.Remove(k.Value);
                            SharMethod.dicPrinterObjectTree.Remove(k.Key);
                            tnode.Parent.Nodes.Remove(tnode);
                            //将要修改配置文件的内容全记录下来
                            lname.Add(k.Value);
                        }
                        //配置文件统一删除掉
                        SharMethod.ClearPrinterXmlGroup(lname.ToArray(),1);
                    }
                }
                else
                {
                    clientForm.showException("不是离线设备不能删除！");
                }
            };
            menu4.Click += (o, e) =>
            {
                if (SharMethod.pathImage != "")
                {
                    foreach (var key in SharMethod.dicPrinterObjectTree)
                    {
                        if (key.Key.onlyAlias == tnode.Name)
                        {
                            var method = key.Key.MethodsObject as PrintersGeneralFunction;
                            method.writeDataToDev(SharMethod.pathImage,key.Key);
                            break;
                        }
                    }
                }
                else
                {
                    clientForm.showException("请先选择图片再打印！");
                }
            };
            
            foreach(var key in SharMethod.dicFlockTree)
            {
                if (key.Key != "打印机群")
                {
                    MenuItem groupMenu = new MenuItem(key.Key);
                    TreeNode flockNode = key.Value;
                    groupMenu.Click += (o, e) =>
                    {
                        if (!SharMethod.dicFlockPrintTree.ContainsKey(tnode.Name))
                        {
                            TreeNode cnode = flockNode.Nodes.Add(tnode.Name, tnode.Text, tnode.ImageIndex);
                            cnode.ForeColor = tnode.ForeColor;
                            cnode.SelectedImageIndex = tnode.ImageIndex;
                            SharMethod.dicFlockPrintTree.Add(cnode.Name, cnode);
                            foreach (var keyObject in SharMethod.dicPrinterObjectTree)
                            {
                                if (keyObject.Value == tnode)
                                {
                                    SharMethod.dicFlockPrinterObjectTree.Add(keyObject.Key, cnode);
                                    break;
                                }
                            }
                            SharMethod.addPeinterXmlGroup(cnode, 2);
                            new MenuPrinterFlockGroupMethod(cnode, clientForm);
                        }else
                        {
                            DialogResult dr = clientForm.showException("该打印机已经分配到一个组中，是否将它分配到现在的组中？", "提示警告", MessageBoxButtons.OKCancel);
                            if (dr == DialogResult.OK)
                            {
                                TreeNode cNode = SharMethod.dicFlockPrintTree[tnode.Name];
                                TreeNode parnode = cNode.Parent;
                                SharMethod.dicFlockPrintTree.Remove(tnode.Name);
                                parnode.Nodes.Remove(cNode);
                                cNode = flockNode.Nodes.Add(tnode.Name, tnode.Text, tnode.ImageIndex);
                                cNode.ForeColor = tnode.ForeColor;
                                cNode.SelectedImageIndex = tnode.ImageIndex;
                                SharMethod.dicFlockPrintTree.Add(cNode.Name, cNode);
                                foreach (var keyObject in SharMethod.dicPrinterObjectTree)
                                {
                                    if (keyObject.Value == tnode)
                                    {
                                        SharMethod.dicFlockPrinterObjectTree[keyObject.Key]=cNode;
                                        break;
                                    }
                                }
                                var dicxml = new Dictionary<string, string>();
                                dicxml.Add(cNode.Name, flockNode.Name);
                                SharMethod.renamePrintXmlGroup(dicxml, 2, 2);
                                new MenuPrinterFlockGroupMethod(cNode, clientForm);
                            }
                        }
                    };
                    menu5.MenuItems.Add(groupMenu);
                }
            }
            tnode.ContextMenu = new ContextMenu(new MenuItem[] { menu1, menu2, menu3, menu4, menu5 });
        }

    }
}
