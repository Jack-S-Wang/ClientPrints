using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objects.Printers.JSON
{
    public class PrinterJson
    {
        public class PrinterDC1300State
        {
            /// <summary>
            /// 状态码
            /// </summary>
            public int stateCode { get; set; }
            /// <summary>
            /// 主状态信息
            /// </summary>
            public string majorState { get; set; }
            /// <summary>
            /// 状态错误信息内容
            /// </summary>
            public string StateMessage { get; set; }
        }

        public class PrinterDC1300DataState
        {
            /// <summary>
            /// 状态码
            /// </summary>
            public int stateCode { get; set; }
            /// <summary>
            /// 主状态信息
            /// </summary>
            public string majorState { get; set; }
            /// <summary>
            /// 状态信息内容
            /// </summary>
            public string StateMessage { get; set; }
            /// <summary>
            /// 正在处理作业号，高位在前
            /// </summary>
            public int workIndex { get; set; }
            /// <summary>
            /// 正在处理的数据帧,高位在前
            /// </summary>
            public int dataFrames { get; set; }
        }
        public class PrinterDC1300PrintState
        {
            /// <summary>
            /// 状态码
            /// </summary>
            public int stateCode { get; set; }
            /// <summary>
            /// 主状态信息
            /// </summary>
            public string majorState { get; set; }
            /// <summary>
            /// 输出的任务数
            /// </summary>
            public int taskNumber { get; set; }
            /// <summary>
            /// 输出的作业号，高位在前
            /// </summary>
            public int workIndex { get; set; }
            /// <summary>
            /// 输出的数据帧号，高位在前
            /// </summary>
            public int dataFrames { get; set; }
            /// <summary>
            /// 温度
            /// </summary>
            public int temperature { get; set; }
            /// <summary>
            /// 感应器
            /// </summary>
            public string sensor { get; set; }
        }
        public class PrinterDC1300DataPortState
        {
            /// <summary>
            /// 接受缓存大小
            /// </summary>
            public int InCache { get; set; }
            /// <summary>
            /// 剩余缓存大小
            /// </summary>
            public int residueCache { get; set; }
        }
        public class PrinterDC1300DataInfo
        {
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
        }
        public class PrinterDC1300PageInfo
        {
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
}
