using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.Printers
{
    public class PrinterParams
    {
        /// <summary>
        /// 设备最大的dip值
        /// </summary>
        public int DIP
        {
            get { return 203; }
        }
        /// <summary>
        /// 设备信息
        /// </summary>
        public string devInfo { get; set; }
        /// <summary>
        /// 接受数据缓存
        /// </summary>
        public int InCache { get; set; }
        /// <summary>
        /// 最大数据帧
        /// </summary>
        public int maxFrames { get; set; }
        /// <summary>
        /// 支持的压缩类型
        /// </summary>
        public byte compressType { get; set; }
        /// <summary>
        /// 最大页宽，像素单位,高位在前
        /// </summary>
        public int maxWidth { get; set; }
        /// <summary>
        /// 最大页高，像素单位
        /// </summary>
        public int maxHeight { get; set; }
        /// <summary>
        /// 页面边界，从左到右，上到下，单位像素
        /// </summary>
        public int confin { get; set; }
        /// <summary>
        /// 横向分辨率
        /// </summary>
        public int xDPL { get; set; }
        /// <summary>
        /// 纵向分辨率
        /// </summary>
        public int yDPL { get; set; }
        /// <summary>
        /// 颜色深度
        /// </summary>
        public int colorDepth { get; set; }
        /// <summary>
        /// 位图格式 -- 1：光栅位图，2：24针打印位图
        /// </summary>
        public byte pixelformat { get; set; }
        /// <summary>
        /// 是否支持DS设备位图 -- 1：支持
        /// </summary>
        public byte isSupport { get; set; }
    }
}
