using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using ClientPrints.ObjectsAll.ClientPrints.Objects.Printers;
using ClientPrsints.ObjectsAll.ClientPrints.Objects.DevDll;
using System.Threading;
using System.Drawing;
using ClientPrints.MethodList.ClientPrints.Method.Interfaces;

namespace ClientPrints.MethodList.ClientPrints.Method.sharMethod
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
        /// 记录树形打印机的键值和节点
        /// </summary>
        public static SortedDictionary<string, TreeNode> dicPrintTree = new SortedDictionary<string, TreeNode>();

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
            //获取打印机
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
                    ImageIndex=5,
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
            string[] path = usbMethod.EnumPath();
            IntPtr phandle=new IntPtr(-1);
            foreach (string pathAddress in path)
            {
                phandle = usbMethod.openPrinter(pathAddress);
                //添加打印机对象
                printerMessage(usbMethod,pathAddress, phandle);
            }
        }

        /// <summary>
        /// 获取打印机的详细信息内容，并记录到对象中
        /// </summary>
        /// <param name="usbMethod">调用的对象方法</param>
        /// <param name="pathAddress">枚举的地址信息</param>
        /// <param name="pHandle">句柄值</param>
        private static void printerMessage(IPrinterMethod usbMethod,string pathAddress, IntPtr pHandle)
        {
            string onlyAlias="";
            int imageIndex = 0;
            string stateMessage = "";
            string state = "";
            Color cl = Color.Gray;
            int stateType = 0;
            //打开设备连接
            usbMethod.reInformation(WDevCmdObjects.DEV_CMD_CONNT, pHandle, new byte[0]);
            //设备型号           
            string model = usbMethod.reInformation(WDevCmdObjects.DEV_GET_MODEL, pHandle,new byte[0]);
            //序列号
            string sn = usbMethod.reInformation(WDevCmdObjects.DEV_GET_DEVNO, pHandle, new byte[0]);
            //版本号
            string version = usbMethod.reInformation(WDevCmdObjects.DEV_GET_PROTVER, pHandle, new byte[0]);
            //标识
            string alias = usbMethod.reInformation(WDevCmdObjects.DEV_GET_USERDAT, pHandle, new byte[0]);
            //状态
            var dicState = getOtherState(usbMethod, pHandle);
            if (dicState.Count > 0)
            {
                foreach (KeyValuePair<int, string> keyValue in dicState)
                {
                    if (keyValue.Key == 1)
                    {
                        imageIndex = 1;
                        stateMessage = keyValue.Value;
                        state = "ready";
                        cl = System.Drawing.Color.Green;
                    }
                    else if (keyValue.Key == 2)
                    {
                        imageIndex = 2;
                        stateMessage = "繁忙：" + keyValue.Value;
                        state = "busy";
                        cl = System.Drawing.Color.Blue;
                    }
                    else if (keyValue.Key == 3)
                    {
                        imageIndex = 3;
                        stateMessage = "警告：" + keyValue.Value;
                        state = "warn";
                        cl = System.Drawing.Color.Yellow;
                    }
                    else
                    {
                        imageIndex = 4;
                        stateMessage = "错误：" + keyValue.Value;
                        state = "error";
                        
                        cl = System.Drawing.Color.Red;
                    }
                    stateType = keyValue.Key;
                }

            }
            else
            {
                imageIndex = 4;
                state = "error";
                stateMessage = "错误：未找到该设备的状态信息";
                stateType = 4;
                cl = System.Drawing.Color.Red;
            }
            if (alias == "")
            {
                Interlocked.Increment(ref emptyCount);
                //设置标识
                //usbMethod.reInformation(WDevCmdObjects.DEV_SET_USERDAT, pHandle,Encoding.UTF8.GetBytes("本地"+emptyCount));//该方法现在是无法执行成功，需要修改dll
                //alias = usbMethod.reInformation(WDevCmdObjects.DEV_GET_USERDAT, pHandle, new byte[0]);
                onlyAlias = "本地" + emptyCount;
            }
            else
            {
                onlyAlias = alias;
            }
            //厂商
            string vendor = usbMethod.reInformation(WDevCmdObjects.DEV_GET_DEVINFO, pHandle, new byte[0]);
            
            var printers = new PrinterObjects()
            {
                sn = sn,
                model = model,
                version = version,
                alias = alias,
                vedor = vendor,
                pHandle=pHandle,
                Methods=usbMethod,
                addressMessage=pathAddress,
                onlyAlias=onlyAlias,
                color=cl,
                ImageIndex=imageIndex,
                interfaceMessage=onlyAlias+"("+model+")",
                stateMessage=stateMessage,
                state=state,
                stateCode=stateType
            };
            dicPrinterUSB.Add(pathAddress, printers);
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
            if (elm.Element(Child) == null)
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

        /// <summary>
        /// 获取设备的状态信息
        /// </summary>
        /// <returns>Dictionary<int ,string>键值对，获取状态的优先级排位和信息内容</returns>
        private static Dictionary<int, string> getOtherState(IPrinterMethod usbMethod,IntPtr pHandle)
        {
            Dictionary<int, string> dicr = new Dictionary<int, string>();
            string strReady = "";
            int readyIndex = -1;
            string strError = "";
            int errorIndex = -1;
            string strWarn = "";
            int warnIndex = -1;
            string strBusy = "";
            int busyIndex = -1;
            var data = new byte[] { 0x01 };
            string messageStr = usbMethod.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, pHandle, data);

            if (messageStr.Length == 11)
            {
                if (messageStr[1] == '0')//说明是10
                {
                    strError += "印头传感器错;";
                    errorIndex = 4;
                }
                else if (messageStr[1] == '1')//11
                {
                    strError += "Flash 错;";
                    errorIndex = 4;
                }
                if (messageStr[2] == '2')
                {

                    strBusy += "接收缓冲满;";//busy
                    errorIndex = 2;
                }

                if (messageStr[3] == '1')
                {

                    strWarn += "建议清洁;";//warn
                    warnIndex = 3;
                }
                else if (messageStr[3] == '2')
                {

                    strError += "必须清洁;";//error
                    errorIndex = 4;
                }

                if (messageStr[5] == '1')
                {

                    strError += "前进传感器缺卡;";//前面报了对应的错误才对应显示内容

                }

                if (messageStr[6] == '1')
                {

                    strError += "计时传感器缺卡;";

                }

                if (messageStr[7] == '1')
                {

                    strError += "卡盒传感器缺卡;";

                }

                if (messageStr[8] == '1')
                {

                    strError += "风扇打开;";

                }

                if (messageStr[9] == '1')
                {

                    strError += "前盖打开;";

                }

                if (messageStr[10] == '1')
                {

                    strError += "凸轮传感器状态（检测打印头）未还原;";

                }

            }
            else if (messageStr.Length == 10)
            {
                if (messageStr[0] == '0')
                {
                    strReady += "打印机就绪;";

                }
                else if (messageStr[0] == '1')
                {
                    strError += "缺卡错;";
                    errorIndex = 4;
                }
                else if (messageStr[0] == '2')
                {
                    strError += "卡卡错;";
                    errorIndex = 4;
                }
                else if (messageStr[0] == '3')
                {
                    strError += "归位错;";
                    errorIndex = 4;
                }
                else if (messageStr[0] == '4')
                {
                    strError += "译码错;";
                    errorIndex = 4;
                }
                else if (messageStr[0] == '5')
                {
                    strError += "卡片延时错;";
                    errorIndex = 4;
                }
                else if (messageStr[0] == '6')
                {
                    strError += "前盖打开错;";
                    errorIndex = 4;
                }
                else if (messageStr[0] == '7')
                {
                    strError += "出卡错;";
                    errorIndex = 4;
                }
                else if (messageStr[0] == '8')
                {
                    strWarn += "印头高温错;";//警告
                    warnIndex = 3;
                }
                else if (messageStr[0] == '9')
                {
                    strWarn += "印头低温错;";//警告
                    warnIndex = 3;
                }

                if (messageStr[1] == '2')
                {
                    strBusy += "接收缓冲满;";//繁忙
                    busyIndex = 2;
                }

                if (messageStr[2] == '1')
                {
                    strWarn += "建议清洁;";//警告
                    warnIndex = 3;
                }
                else if (messageStr[2] == '2')
                {
                    strError += "必须清洁;";//错误
                    errorIndex = 4;
                }

                if (messageStr[4] == '1')
                {
                    strError += "前进传感器缺卡;";

                }

                if (messageStr[5] == '1')
                {
                    strError += "计时传感器缺卡;";

                }

                if (messageStr[6] == '1')
                {
                    strError += "卡盒传感器缺卡;";

                }

                if (messageStr[7] == '1')
                {
                    strError += "风扇打开;";

                }

                if (messageStr[8] == '1')
                {
                    strError += "前盖打开;";

                }

                if (messageStr[9] == '1')
                {
                    strError += "凸轮传感器状态（检测打印头）未还原;";

                }

            }
            if (errorIndex == 4)//说明有错误就不管警告和繁忙了
            {
                dicr.Add(errorIndex, strError);
            }
            else if (warnIndex == 3)//说明无错误有警告或繁忙则先显示警告
            {
                dicr.Add(warnIndex, strWarn);
            }
            else if (busyIndex == 2)//说明即无错误也无警告只有繁忙
            {
                dicr.Add(busyIndex, strBusy);
            }
            else if (readyIndex == 1)//说明一切正常
            {
                dicr.Add(readyIndex, strReady);
            }
            else
            {
                dicr.Add(4, "未获取信息！");
            }
            return dicr;
        }
      
    }
}
