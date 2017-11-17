using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClinetPrints.SettingWindows;
using ClientPrints.MethodList.ClientPrints.Method.sharMethod;
using ClientPrints.ObjectsAll.ClientPrints.Objects.Printers;
using ClientPrints.MethodList.ClientPrints.Method.Interfaces;

namespace ClinetPrints.MenuGroupMethod
{
    class MenuPrinterGroupAddMethod
    {
        public MenuPrinterGroupAddMethod(TreeNode tnode,ClientMianWindows clientForm)
        {
            //对没有设置到xml文档里添加进去
            SharMethod.addPeinterXmlGroup(tnode,1);
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
                        foreach (var key in SharMethod.dicPrinterAll)
                        {
                            if (key.Key.onlyAlias == tnode.Name)
                            {
                                Dictionary<string, string> dicxml = new Dictionary<string, string>();
                                key.Key.interfaceMessage = key.Key.onlyAlias + "(" + na.name + ")";
                                tnode.Text = key.Key.onlyAlias + "(" + na.name + ")";
                                dicxml.Add(tnode.Name,tnode.Text);
                                //修改配置文件的信息内容
                                SharMethod.renamePrintXmlGroup(dicxml,1,1);
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
                foreach (var key in SharMethod.dicPrinterAll)
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
                            SharMethod.dicPrinterAll.Remove(k.Key);
                            SharMethod.dicPrintTree.Remove(k.Value);
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
                    foreach (var key in SharMethod.dicPrinterAll)
                    {
                        if (key.Key.onlyAlias == tnode.Name)
                        {
                            var method = key.Key.Methods as IPrinterMethod;
                            method.writeDataToDev(SharMethod.pathImage, key.Key.pHandle);
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
                            SharMethod.dicFlockPrintTree.Add(cnode.Name, cnode);
                            SharMethod.addPeinterXmlGroup(tnode, 2);
                        }else
                        {
                            clientForm.showException("已经在群组了！");
                        }
                    };
                    menu5.MenuItems.Add(groupMenu);
                }
            }
            tnode.ContextMenu = new ContextMenu(new MenuItem[] { menu1, menu2, menu3, menu4,menu5 });
        }
        
    }
}
