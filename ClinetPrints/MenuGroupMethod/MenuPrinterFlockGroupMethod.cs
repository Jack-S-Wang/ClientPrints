﻿using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrintsObjectsAll.ClientPrints.Objects.treeNodeObject;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using System.Windows.Forms;

namespace ClinetPrints.MenuGroupMethod
{
    public class MenuPrinterFlockGroupMethod
    {
        public MenuPrinterFlockGroupMethod(TreeNode tnode, ClientMianWindows clientForm)
        {
            TreeNode nodeParFlock = clientForm.printerViewFlock.Nodes[0];
            tnode.ContextMenu = null;
            MenuItem clearPrinter = new MenuItem("删除");
            MenuItem remove = new MenuItem("移位");
            clearPrinter.Click += (o, e) =>
            {
                if (clientForm.toolStTxb_printer.Text == tnode.Parent.Text)
                {
                    var col = clientForm.listView1.Columns[4] as listViewColumnTNode;
                    if (col.liPrinter.Contains((tnode as PrinterTreeNode).PrinterObject))
                    {
                        col.liPrinter.Remove((tnode as PrinterTreeNode).PrinterObject);
                    }
                }
                (tnode.Parent as GroupTreeNode).Remove((tnode as PrinterTreeNode));
                var file = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                SharMethod.SavePrinter(nodeParFlock, file);
            };
            foreach (TreeNode nod in nodeParFlock.Nodes)
            {
                //局部定义
                TreeNode cnode = nod;
                MenuItem group = new MenuItem(cnode.Name);
                group.Click += (o, e) =>
                {
                    if (clientForm.toolStTxb_printer.Text == tnode.Parent.Text)
                    {
                        var col = clientForm.listView1.Columns[4] as listViewColumnTNode;
                        if (col.liPrinter.Contains((tnode as PrinterTreeNode).PrinterObject))
                        {
                            col.liPrinter.Remove((tnode as PrinterTreeNode).PrinterObject);
                        }
                    }
                    var node = tnode as PrinterTreeNode;
                    (tnode.Parent as GroupTreeNode).Remove(node);
                    (cnode as GroupTreeNode).Add(node);
                    var file = SharMethod.FileCreateMethod(SharMethod.FLOCK);
                    SharMethod.SavePrinter(nodeParFlock, file);
                };
                remove.MenuItems.Add(group);
            }
            tnode.ContextMenu = new ContextMenu(new MenuItem[] { clearPrinter, remove });
        }
    }
}
