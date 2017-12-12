using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using System.Windows.Forms;

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
        /// 设备的参数信息;0-擦除位图类型,1-卡片类型,2-进卡方式,3-出卡方式,4-打印温度,
        /// 5-打印对比度,6-打印速度,7-灰度温度,8-擦除速度,9-设置擦除温度,10-打印模式
        /// </summary>
        public PrinterParams pParams { get; set; }
        /// <summary>
        /// ListView控件记录下图片列表对象内容
        /// </summary>
        public ListView listviewObject { get; set; }


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
