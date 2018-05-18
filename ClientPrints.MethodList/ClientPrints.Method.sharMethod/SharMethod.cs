using System.Collections.Generic;
using System.Windows.Forms;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrintsMethodList.ClientPrints.Method.WDevDll;
using System.Text;

namespace ClientPrintsMethodList.ClientPrints.Method.sharMethod
{
    public class SharMethod
    {

        public static readonly int SINGLE = 1;
        public static readonly int FLOCK = 2;

        /// <summary>
        /// 服务Ip
        /// </summary>
        public static string serverIp = "";
        /// <summary>
        /// 服务port
        /// </summary>
        public static string serverPort = "";

        public static readonly string PRINTINSTRUCTION = "print_instruction";
        public static readonly string UPDATEINSTRUCTION = "update_instruction";
        public static readonly string CONTROLINSTRUCTION = "control_instruction";
        public static readonly string FINDDEVICEMESSAGE = "find_device_message";

        /// <summary>
        /// 记录枚举到的地址信息和打印机对象
        /// </summary>
        public static SortedDictionary<string, PrinterObjects> dicPrinterObject = new SortedDictionary<string, PrinterObjects>();
        

        /// <summary>
        /// 记录下不同方式获取到的全部打印机（在线）
        /// </summary>
        public static List<PrinterObjects> liAllPrinter = new List<PrinterObjects>();

        public static monitorTime monTime { get; set; }

        public static UserCommend user;

        /// <summary>
        /// 记录密码错误的设备
        /// </summary>
        public static List<string> passwordError = new List<string>();

        /// <summary>
        /// 记录版本号不一致的设备
        /// </summary>
        public static List<string> banError = new List<string>();
        /// <summary>
        /// 获取所有打印机的信息
        /// </summary>
        public static void getPrinter()
        {
            //获取USB类型的打印机
            getUSBPrinter();
            //得到wife方式的打印机信息
            getWifePrinter();
            //不同方式获取的打印机总信息
            getAllPrinterList();
        }

        private static void getWifePrinter()
        {
            
        }

        /// <summary>
        /// 遍历所有从配置文件中获取的打印机
        /// </summary>
        /// <param name="node">树节点</param>
        /// <param name="action">执行方法</param>
        public static void ForEachNode(TreeNode node, Action<TreeNode> action)
        {
            action(node);

            foreach (TreeNode n in node.Nodes)
            {
                ForEachNode(n, action);
            }
        }

        /// <summary>
        /// 从当前节点递归找到绝对的路径上的节点
        /// </summary>
        /// <param name="node">树节点</param>
        /// <param name="action">执行的方法</param>
        public static void ForTopEachNode(TreeNode node, Action<TreeNode> action)
        {
            if (node == null) { return; }
            action(node);
            if (node.Parent != null)
            {
                ForTopEachNode(node.Parent, action);
            }
        }

        /// <summary>
        /// 获取所有的打印机，并处理创建
        /// </summary>
        private static void getAllPrinterList()
        {
            foreach (var key in dicPrinterObject)
            {
                liAllPrinter.Add(key.Value);
            }
        }

        /// <summary>
        /// 按不同的类型生成文件流
        /// </summary>
        /// <param name="type">1-single，2-Flock</param>
        /// <returns></returns>
        public static FileStream FileCreateMethod(int type)
        {
            FileStream file;
            if (type == 1)
            {
                file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\printerSingle.bin", FileMode.OpenOrCreate);

            }
            else
            {
                file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\printerFlock.bin", FileMode.OpenOrCreate);
            }
            return file;
        }

        /// <summary>
        /// 保存打印机信息到配置文件
        /// </summary>
        /// <param name="tnode">根节点</param>
        /// <param name="file">文件流</param>
        public static void SavePrinter(TreeNode tnode, FileStream file)
        {
            var bf = new BinaryFormatter();
            if (file.Length > 0)
            {
                file.SetLength(0);
                file.Seek(0, SeekOrigin.Begin);
            }
            bf.Serialize(file, tnode);
            FileClose(file);
        }

        /// <summary>
        /// 对文件进行关闭操作
        /// </summary>
        /// <param name="file">文件流</param>
        public static void FileClose(FileStream file)
        {
            file.Flush();
            file.Dispose();
            file.Close();
        }

        /// <summary>
        /// USB插口方式获取的打印机信息
        /// </summary>
        private static void getUSBPrinter()
        {
            PrinterUSBMethod pum = new PrinterUSBMethod();
            pum.getPrinterObjects();
        }
        /// <summary>
        /// 调用设备动态库写日志文档
        /// </summary>
        /// <param name="str"></param>
        public static void writeLog(string str)
        {
            WDevDllMethod.dllFunc_SetLogRecord(str, true, true, true);
        }

        /// <summary>
        /// 记录下所有的日志错误信息！！以便上传报告
        /// </summary>
        /// <param name="str"></param>
        public static void writeErrorLog(string str)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\"+DateTime.Now.Date.ToString("yyyyMMdd")+"Error.Log";
            bool done = false;
            while (!done)
            {
                try
                {
                    File.AppendAllText(filePath, user.getCommendInfo().ToString()+"\r\n"+str+"\r\n");
                    done = true;
                }
                catch { };
            }
        }
    }
}
