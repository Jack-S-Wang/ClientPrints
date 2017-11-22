using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrsintsMethodList.ClientPrints.Method.Interfaces
{
    public interface IPrinterMethod
    {
        /// <summary>
        /// 获取对应USB类型的打印机
        /// </summary>
        /// <returns></returns>
        void getPrinterObjects();

        /// <summary>
        /// 对指定图片进行打印
        /// </summary>
        /// <param name="pathFile">图片路径</param>
        /// <param name="pHandle">句柄值</param>
        bool writeDataToDev(string pathFile, IntPtr pHandle);
    }
}
