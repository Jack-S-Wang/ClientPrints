using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClinetPrints.MenuGroupMethod
{
    public class MenuPrinterFlockGroupMethod
    {
        public MenuPrinterFlockGroupMethod(TreeNode tnode,ClientMianWindows clientForm)
        {
            tnode.ContextMenu = null;
            MenuItem menu1 = new MenuItem("删除");
            MenuItem menu2 = new MenuItem("移位");
            menu1.Click += (o, e) =>
            {
                string[] name = new string[] { tnode.Name };
                PrinterObjects pobject = new PrinterObjects();
                foreach(var key in SharMethod.dicFlockPrinterObjectTree)
                {
                    if (key.Value == tnode)
                    {
                        pobject = key.Key;
                        break;
                    }
                }
                if (pobject != null)
                    SharMethod.dicFlockPrinterObjectTree.Remove(pobject);
                SharMethod.ClearPrinterXmlGroup(name,2);
                tnode.Parent.Nodes.Remove(tnode);
            };
            foreach(var key in SharMethod.dicFlockTree)
            {
                string name = key.Key;
                TreeNode cnode = key.Value;
                TreeNode parNode = tnode.Parent;
                if (name != "打印机群")
                {
                    MenuItem group = new MenuItem(name);
                    group.Click += (o, e) =>
                    {
                        parNode.Nodes.Remove(tnode);
                        cnode.Nodes.Add(tnode);
                        var dicxml = new Dictionary<string, string>();
                        dicxml.Add(tnode.Name, name);
                        SharMethod.renamePrintXmlGroup(dicxml, 2, 2);
                    };
                    menu2.MenuItems.Add(group);
                } 
            }
            tnode.ContextMenu = new ContextMenu(new MenuItem[] { menu1, menu2 });
        }
    }
}
