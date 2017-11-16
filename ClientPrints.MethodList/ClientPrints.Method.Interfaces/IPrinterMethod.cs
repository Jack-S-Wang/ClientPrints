using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrints.MethodList.ClientPrints.Method.Interfaces
{
    public interface IPrinterMethod
    {
        /// <summary>
        /// 枚举设备路径地址
        /// </summary>
        /// <returns></returns>
        string[] EnumPath();

         /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IntPtr openPrinter(string path);

         /// <summary>
        /// 获取对应指令的信息内容
        /// </summary>
        /// <param name="pHandle">句柄值</param>
        /// <param name="ctrlCodeStr">命令字符串</param>
        /// <param name="data">指令数据</param>
        /// <returns></returns>
        string reInformation(string ctrlCodeStr, IntPtr pHandle, byte[] data);

        /// <summary>
        /// 对指定图片进行打印
        /// </summary>
        /// <param name="pathFile">图片路径</param>
        /// <param name="pHandle">句柄值</param>
        bool writeDataToDev(string pathFile, IntPtr pHandle);
    }
}
