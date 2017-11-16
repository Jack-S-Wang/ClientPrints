using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClientPrints.ObjectsAll.ClientPrints.Objects.Printers
{
    public class PrinterObjects:IComparable
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
        /// 图片的序号
        /// </summary>
        public int ImageIndex { get; set; }
        /// <summary>
        /// 界面显示的信息内容
        /// </summary>
        public string interfaceMessage { get; set; }
        /// <summary>
        /// 储存的地址信息
        /// </summary>
        public string addressMessage { get; set; }
        /// <summary>
        /// 状态码优先级序号
        /// </summary>
        public int stateCode { get; set; }
        /// <summary>
        /// 状态信息，read，busy，warn，error,line
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 状态的信息内容
        /// </summary>
        public string stateMessage { get; set; }
        /// <summary>
        /// 获取颜色
        /// </summary>
        public Color color { get; set; }
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
        public Object Methods { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string onlyAlias { get; set; }

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
}
