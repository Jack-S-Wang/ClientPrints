using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ClientPrintsMethodList.ClientPrints.Method.DevBmpDll
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

        [DllImport("ds_dev_bmp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool LoadBitmapFile(string imageFile);

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

        /// <summary>
        /// 压缩处理
        /// </summary>
        /// <param name="SupportedCompressMth">支持的压缩方式bit0: 
        /// 1 支持RLE，0：不支持bit1: 1 支持LZW，0：不支持  bit2: 1 支持Huffman，0：不支持,全支持选7</param>
        /// <param name="indata">输入的数据</param>
        /// <param name="inSize">输入的长度</param>
        /// <param name="Outdata">输出的数据</param>
        /// <param name="Outsize">输入时请指定输出空间大小，</param>
        /// <returns></returns>
        [DllImport("ds_dev_data.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern Int32 DS_Compress(byte SupportedCompressMth, IntPtr indata, int inSize, [Out] IntPtr Outdata,out int Outsize);

        [DllImport("ds_dev_bmp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool Save(string path, long x, long y);

    }
}
