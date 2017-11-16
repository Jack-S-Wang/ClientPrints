using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ClientPrints.MethodList.ClientPrints.Method.DevBmpDll
{
    public class DevBmpDllMethod
    {
        /// <summary>
        /// 生成得实位图
        /// </summary>
        /// <param name="devp">打印机参数信息的结构体</param>
        [DllImport("ds_dev_bmp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void setDeviceProterty(ref structBmpClass.DeviceProterty devp);

        /// <summary>
        /// 对图片进行设置调色设置
        /// </summary>
        /// <param name="imageFile">图片的绝对路径</param>
        /// <param name="para">结构体</param>
        /// <returns></returns>
        [DllImport("ds_dev_bmp.dll",CharSet=CharSet.Unicode,CallingConvention=CallingConvention.StdCall)]
        public static extern bool LoadBitmapFilePara(string imageFile,ref structBmpClass.DS_PARAMETER para);

        /// <summary>
        /// 生成位图数据
        /// </summary>
        /// <returns></returns>
        [DllImport("ds_dev_bmp.dll",CharSet=CharSet.Unicode,CallingConvention=CallingConvention.StdCall)]
        public static extern IntPtr GetBits();

        /// <summary>
        /// 生成位图的长度
        /// </summary>
        /// <returns></returns>
        [DllImport("ds_dev_bmp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 GetLength();
    }
}
