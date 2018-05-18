using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;
using System.Threading;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.Printers
{
    [Serializable]
    public class PrinterObjects : IComparable
    {
        /// <summary>
        /// 序列号
        /// </summary>
        public string sn { get; set; }
        /// <summary>
        /// 厂商
        /// </summary>
        public string vedor { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 句柄值
        /// </summary>
        public IntPtr pHandle { get; set; }

        /// <summary>
        /// 储存的地址信息
        /// </summary>
        public string addressMessage { get; set; }

        private int stateCode_;

        /// <summary>
        /// 状态属性值更新时发生事件
        /// </summary>
        public event Action<PrinterObjects> StateCodeChanged;

        /// <summary>
        /// 状态码优先级序号
        /// </summary>
        public int stateCode
        {
            get { return stateCode_; }
            set {
                if (stateCode_ != value)
                {
                    stateCode_ = value;
                    //StateCodeChanged对象可为空，不为空直接调用方法
                    StateCodeChanged?.Invoke(this);
                }
            }
        }
        /// <summary>
        /// 状态信息，read，busy，warn，error,line
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 状态的信息内容
        /// </summary>
        public string stateMessage { get; set; }
       
        /// <summary>
        /// 别名信息
        /// </summary>
        public string alias { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 对应获取执行打印方法的对象
        /// </summary>
        public IMethodObjects MethodsObject { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string onlyAlias { get; set; }
        /// <summary>
        /// 设备的参数信息;
        /// </summary>
        public PrinterParams pParams { get; set; }
        /// <summary>
        /// ListViewItems控件记录下图片列表对象内容
        /// </summary>
        public List<ListViewItem> listviewItemObject = new List<ListViewItem>();
        /// <summary>
        ///  ListViewItems控件记录下图片列表对象内容
        /// </summary>
        public List<Image> listviewImages = new List<Image>();
        /// <summary>
        /// 存储正在执行打印查询是否完成的工作线程
        /// </summary>
        public Thread threadObject { get; set; }
        /// <summary>
        /// 记录设备是否是wifi设备
        /// </summary>
        public bool isWifi { get; set; }

        public int CompareTo(object obj)
        {
            int result = 0;

            PrinterObjects o = (PrinterObjects)obj;
            if (this.stateCode > o.stateCode)
                result = 1;
            else if (this.stateCode == o.stateCode)
                return onlyAlias.CompareTo(o.onlyAlias);
            else
                result = -1;

            return result;
        }
    }
    public class printerClose
    {
        public static bool closeWindow = false;
    }
}
