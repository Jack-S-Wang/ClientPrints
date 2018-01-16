using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface
{
    /// <summary>
    /// 调用不同打印机所执行方法的对象
    /// </summary>
    public interface IMethodObjects
    {
        /// <summary>
        /// USB获取对应指令的信息内容
        /// </summary>
        /// <param name="codeStr"></param>
        /// <param name="pHandle"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        string reInformation(string ctrlCodeStr, IntPtr pHandle, byte[] data);
        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="pathFile"></param>
        /// <param name="po"></param>
        /// <param name="jobnum"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        List<string> writeDataToDev(string pathFile, PrinterObjects po, string jobnum, int num);
        void getRa(PrinterObjects po);
    }
}
