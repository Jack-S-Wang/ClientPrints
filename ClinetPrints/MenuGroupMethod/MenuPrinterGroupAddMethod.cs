using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClinetPrints.SettingWindows;
using ClientPrints.MethodList.ClientPrints.Method.sharMethod;

namespace ClinetPrints.MenuGroupMethod
{
    class MenuPrinterGroupAddMethod
    {
        public MenuPrinterGroupAddMethod(TreeNode tnode,ClientMianWindows clientForm)
        {
            //对没有设置到xml文档里添加进去
            SharMethod.addPeinterXmlGroup(tnode);
            //打印机重命名
            MenuItem menu1 = new MenuItem("重命名");
            //打印机移位
            MenuItem menu2 = new MenuItem("移位");
            //删除打印机
            MenuItem menu3 = new MenuItem("删除");
            //打印
            MenuItem menu4 = new MenuItem("打印");
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
                                key.Key.interfaceMessage = key.Key.onlyAlias + "(" + na.name + ")";
                                tnode.Text = key.Key.onlyAlias + "(" + na.name + ")";
                                //修改配置文件的信息内容
                                SharMethod.renamePrintXmlGroup(tnode.Name, na.name,1);
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
                    group.Click += (o, e) =>
                    {
                        var oldtnode = tnode;
                        //清理对应的打印机节点
                        tnode.Parent.Nodes.Remove(tnode);
                        //再添加到对应的分组上去
                        key.Value.Nodes.Add(oldtnode);
                        //修改xml文件内容
                        SharMethod.renamePrintXmlGroup(oldtnode.Name, key.Value.Name, 2);
                    };
                }
            }
            tnode.ContextMenu = new ContextMenu(new MenuItem[] { menu1, menu2, menu3, menu4 });
        }
    }
}
