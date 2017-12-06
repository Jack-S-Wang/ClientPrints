using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsMethodList.ClientPrints.Method.Interfaces
{
    public interface IUSBPrinterOnlyMethod
    {
        /// <summary>
        /// 获取打印机的状态信息，根据不同型号解析
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string getPrinterState(byte[] data);
        /// <summary>
        /// 获取设备信息格式内容
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string getDevInfo(byte[] data);
        /// <summary>
        /// 获取设备系统参数信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string getDevParmInfo(byte[] data);

    }
}
