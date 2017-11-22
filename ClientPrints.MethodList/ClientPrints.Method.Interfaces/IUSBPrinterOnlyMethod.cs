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
    }
}
