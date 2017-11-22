using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using System.Threading;
using System.Drawing;
using ClientPrsintsMethodList.ClientPrints.Method.Interfaces;

namespace ClientPrsintsMethodList.ClientPrints.Method.sharMethod
{
    public class SharMethod
    {
        /// <summary>
        /// 记入子类为键值，父类为值的画布布局容器
        /// </summary>
        public static SortedDictionary<string, string> limap = new SortedDictionary<string, string>();

        /// <summary>
        /// 记录从配置文件中获取的子类和父类的值
        /// </summary>
        public static SortedDictionary<string, string> liprintmap = new SortedDictionary<string, string>();

        /// <summary>
        /// 记录在xml文档中的界面显示内容
        /// </summary>
        public static SortedDictionary<string, string> liprintInterface = new SortedDictionary<string, string>();

        /// <summary>
        /// 记录在xml文档中的群打印机的子类和父类
        /// </summary>
        public static SortedDictionary<string, string> liprinterFlockMap = new SortedDictionary<string, string>();
 
        /// <summary>
        ///记录树形分组的键值和节点
        /// </summary>
        public static SortedDictionary<string, TreeNode> dicTree = new SortedDictionary<string, TreeNode>();

        /// <summary>
        /// 记录树形分组群的键和节点
        /// </summary>
        public static SortedDictionary<string, TreeNode> dicFlockTree = new SortedDictionary<string, TreeNode>();

        /// <summary>
        /// 记录群打印机的树形节点信息
        /// </summary>
        public static SortedDictionary<string, TreeNode> dicFlockPrintTree = new SortedDictionary<string, TreeNode>();
        /// <summary>
        /// 记录群打印机的对象与节点,如果群点了删除按钮该对应的打印机也得删除
        /// </summary>
        public static SortedDictionary<PrinterObjects, TreeNode> dicFlockPrinterObjectTree = new SortedDictionary<PrinterObjects, TreeNode>();

        /// <summary>
        /// 记录树形打印机的键值和节点
        /// </summary>
        public static SortedDictionary<string, TreeNode> dicPrintTree = new SortedDictionary<string, TreeNode>();

        /// <summary>
        /// 记录打印机的对象与节点
        /// </summary>
        public static SortedDictionary<PrinterObjects, TreeNode> dicPrinterObjectTree = new SortedDictionary<PrinterObjects, TreeNode>();

        /// <summary>
        /// 记录枚举到的地址信息和打印机对象，针对于USB插口的
        /// </summary>
        public static SortedDictionary<string, PrinterObjects> dicPrinterUSB = new SortedDictionary<string, PrinterObjects>();

        /// <summary>
        /// 所有打印机的对象和父类节点的名称
        /// </summary>
        public static SortedDictionary<PrinterObjects,string> dicPrinterAll = new SortedDictionary<PrinterObjects,string>();

        /// <summary>
        /// 记录打印机没有标识信息的数量
        /// </summary>
        public static int emptyCount = 0;

        /// <summary>
        /// 导入的图片的绝对路径
        /// </summary>
        public static string pathImage = "";

        /// <summary>
        /// 清理配置文件里的信息内容
        /// </summary>
        /// <param name="liName">要处理的数据集合</param>
        /// <param name="type">1-单，2-群</param>
        public static void ClearXmlData(List<string> liName,int type)
        {
            string file = "";
            if (type == 1)
            {
                file = @"./printerXml/groupMap.xml";
            }else
            {
                file = @"./printerXml/groupFlockMap.xml";
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(file);
            var xd = xmlDoc.GetElementsByTagName("printMap")[0];
            foreach (string name in liName)
            {
                xd.RemoveChild(xmlDoc.GetElementsByTagName(name)[0]);
            }
            xmlDoc.Save(file);
            
        }

        /// <summary>
        /// 添加新增的分组信息到配置文件
        /// </summary>
        /// <param name="tnode">树形节点</param>
        /// <param name="type">1-单，2-群</param>
        public static void setXmlGroup(TreeNode tnode,int type)
        {
            string father = tnode.Parent.Name;
            string Child = tnode.Name;
            string file = "";
            if (type == 1)
            {
                file = @"./printerXml/groupMap.xml";
            }
            else
            {
                file = @"./printerXml/groupFlockMap.xml";
            }
            XElement xel = XElement.Load(file);
            var xelnew = xel.Element("printMap");
            XElement xt = new XElement(Child, father);
            xelnew.Add(xt);
            xel.Save(file);
        }

        /// <summary>
        /// 重命名分组名称
        /// </summary>
        /// <param name="dicxml">一个以子节点的键为键，修改的名称为值的容器</param>
        /// <param name="oldname">要命名的原组名称</param>
        /// <param name="type">1-单，2-群</param>
        public static void renameXmlGroup(Dictionary<string,string> dicxml,string oldname,int type)
        {
            string parname = "";
            string newname = "";
            string file = "";
            if (type == 1)
            {
                file = @"./printerXml/groupMap.xml";
            }
            else
            {
                file = @"./printerXml/groupFlockMap.xml";
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(file);
            foreach (var key in dicxml)
            {
                if (key.Key != oldname)
                {
                    xmlDoc.GetElementsByTagName(key.Key)[0].InnerText = key.Value;//节点的子类与父类
                }
                else//说明是修改的的那个组名
                {
                    //先获取原来的父类名称并删除原来的节点
                    parname = xmlDoc.GetElementsByTagName(key.Key)[0].InnerText;
                    newname = key.Value;
                    var xd = xmlDoc.GetElementsByTagName("printMap")[0];
                    xd.RemoveChild(xmlDoc.GetElementsByTagName(key.Key)[0]);
                }
            }
            xmlDoc.Save(file);
            //重新添加
            XElement xel = XElement.Load(file);
            var xelnew = xel.Element("printMap");
            XElement xt = new XElement(newname, parname);
            xelnew.Add(xt);
            xel.Save(file);
        }
        /// <summary>
        /// 获取所有打印机的信息
        /// </summary>
        public static void getPrinter()
        {
            //获取USB类型的打印机
            getUSBPrinter();
            //获取所有打印机的容器
            getAllPrinterList();
        }

        private static void getAllPrinterList()
        {
            List<string> li = new List<string>();
            foreach (var keyva in dicPrinterUSB)
            {
                if (liprintmap.ContainsKey(keyva.Value.onlyAlias))
                {
                    //先把对象中的界面显示改正过来
                    for (int i = 0; i < liprintInterface[keyva.Value.onlyAlias].Length; i++)
                    {
                        if (liprintInterface[keyva.Value.onlyAlias][i] == '(')
                            break;
                        keyva.Value.alias += liprintInterface[keyva.Value.onlyAlias][i];
                    }
                    keyva.Value.interfaceMessage = liprintInterface[keyva.Value.onlyAlias];
                    dicPrinterAll.Add(keyva.Value, liprintmap[keyva.Value.onlyAlias]);
                    li.Add(keyva.Value.onlyAlias);//要对limap进行清理
                }
                else
                {
                    dicPrinterAll.Add(keyva.Value, "所有打印机");
                }
            }
            //清理已经添加过的信息
            foreach (var key in li)
            {
                liprintmap.Remove(key);
                liprintInterface.Remove(key);
            }
            li.Clear();
            //如果没有其他方式获取到的打印机信息则处理最后遗留在配置文件中没有对应打印机的信息
            foreach (var key in liprintmap)
            {
                var printer = new PrinterObjects()
                {
                    onlyAlias=key.Key,
                    ImageIndex=6,
                    interfaceMessage=liprintInterface[key.Key],
                    stateMessage="离线",
                    color=Color.Gray,
                    stateCode=0
                };
                dicPrinterAll.Add(printer, key.Value);
                li.Add(key.Key);
            }
            //最后清理所有从配置文件中获取的信息内容
            foreach (var key in li)
            {
                liprintmap.Remove(key);
                liprintInterface.Remove(key);
            }
            li.Clear();
        }

        /// <summary>
        /// USB插口方式获取的打印机信息
        /// </summary>
        private static void getUSBPrinter()
        {
            IPrinterMethod usbMethod=new PrinterUSBMethod();
            usbMethod.getPrinterObjects();
        }

      

        /// <summary>
        /// 添加设备到xml文档中
        /// </summary>
        /// <param name="tnode">子节点</param>
        /// <param name="type">1-单，2-群</param>
        public static void addPeinterXmlGroup(TreeNode tnode,int type)
        {
            string file = "";
            if (type == 1)
            {
                file = @"./printerXml/printerMap.xml";
            }else
            {
                file = @"./printerXml/printerFlockMap.xml";
            }
            string father = tnode.Parent.Name;
            string Child = tnode.Name;
            string printInterface=tnode.Text;
            XElement xel = XElement.Load(file);
            var elm = xel.Element("printMap");
            if (elm.Element(@""+Child) == null)
            {
                XElement xt = new XElement(Child, father);
                elm.Add(xt);
                if (type == 1)
                {
                    elm = xel.Element("printInterface");
                    xt = new XElement(Child, printInterface);
                    elm.Add(xt);
                }
                xel.Save(file);
            }
            
        }

        /// <summary>
        /// 删除设备到xml文件中
        /// </summary>
        /// <param name="tnode">子节点</param>
        /// <param name="type">1-单，2-群</param>
        public static void ClearPrinterXmlGroup(string[] liName,int type)
        {
            string file = "";
            if (type == 1)
            {
                file = @"./printerXml/printerMap.xml";
            }
            else
            {
                file = @"./printerXml/printerFlockMap.xml";
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(file);
            var xd = xmlDoc.GetElementsByTagName("printMap")[0];
            foreach (var name in liName)
            {
                xd.RemoveChild(xmlDoc.GetElementsByTagName(name)[0]);
                xd = xmlDoc.GetElementsByTagName("printInterface")[0];
                xd.RemoveChild(xmlDoc.GetElementsByTagName(name)[0]);
            }
            xmlDoc.Save(file);
        }

        /// <summary>
        /// 重命名打印机的界面显示内容或移位到不同分组
        /// </summary>
        /// <param name="type">1-重命名，2-移位</param>
        /// <param name="dicxml">要处理的对象容器</param>
        /// <param name="singleOrFlock">1-单，2-群</param>
        public static void renamePrintXmlGroup(Dictionary<string, string> dicxml, int type, int singleOrFlock)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string file = "";
            if (singleOrFlock == 1)
            {
                file = @"./printerXml/printerMap.xml";
            }
            else
            {
                file = @"./printerXml/printerFlockMap.xml";
            }
                xmlDoc.Load(file);
            foreach (var key in dicxml)
            {
                if (type == 1)
                {
                    xmlDoc.GetElementsByTagName(key.Key)[1].InnerText = key.Value;//节点的子类与见面显示内容
                }
                else
                {
                    xmlDoc.GetElementsByTagName(key.Key)[0].InnerText = key.Value;//节点的子类与父类
                }
            }
            xmlDoc.Save(file);
        }

       
      
    }
}
