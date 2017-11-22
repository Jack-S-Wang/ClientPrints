using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ClientPrintsMethodList.ClientPrints.Method.DevBmpDll
{
    public class structBmpClass
    {
        /// <summary>
        /// 生成得实位图的结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DeviceProterty
        {
            public short dmThicken;                 // 位图深度
            //以下信息除非需要用mm/inch等为单位画图或定位时才需要
            public short nWidth;                    // 打印机每页最大宽度
            public short nHeight;                   // 打印机每页最大高度
            public short dmPrintQuality;            // X分辨率
            public short dmYResolution;             // Y分辨率
            public UInt32 dmTag;                     // bit 0=1:缩放
        }

        /// <summary>
        /// 对应图片调色设置的结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DS_PARAMETER
        {
            public DeviceProterty devp;//得实位图结构体返回的值
            public IntPtr RGBParameter;//图片生成参数指针，根据不同深度有不同的结构体
            public IntPtr RGBPalete;   //调色板指针
        }

    }
}
