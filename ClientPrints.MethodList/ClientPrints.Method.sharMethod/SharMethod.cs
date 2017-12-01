using System.Collections.Generic;
using System.Windows.Forms;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace ClientPrsintsMethodList.ClientPrints.Method.sharMethod
{
    public class SharMethod
    {

        public static readonly int SINGLE = 1;
        public static readonly int FLOCK = 2;
      
        /// <summary>
        /// 记录枚举到的地址信息和打印机对象，针对于USB插口的
        /// </summary>
        public static SortedDictionary<string, PrinterObjects> dicPrinterUSB = new SortedDictionary<string, PrinterObjects>();

        public static List<PrinterObjects> liAllPrinter = new List<PrinterObjects>();

        /// <summary>
        /// 记录打印机没有标识信息的数量
        /// </summary>
        public static int emptyCount = 0;

        /// <summary>
        /// 导入的图片的绝对路径
        /// </summary>
        public static string pathImage = "";

       
        /// <summary>
        /// 获取所有打印机的信息
        /// </summary>
        public static void getPrinter()
        {
            //获取USB类型的打印机
            getUSBPrinter();
            //不同方式获取的打印机总信息
            getAllPrinterList();
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
        /// 获取所有的打印机，并处理创建
        /// </summary>
        private static void getAllPrinterList()
        {
            foreach(var key in dicPrinterUSB)
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
                file = new FileStream(@"./printerXml/printerSingle.bin", FileMode.OpenOrCreate);

            }else
            {
                file = new FileStream(@"./printerXml/printerFlock.bin", FileMode.OpenOrCreate);
            }
            return file;
        }

        /// <summary>
        /// 保存打印机信息到配置文件
        /// </summary>
        /// <param name="tnode">根节点</param>
        /// <param name="file">文件流</param>
        public static void SavePrinter(TreeNode tnode,FileStream file)
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
      
    }
}
