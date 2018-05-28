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

        /// <summary>
        /// 将指令转换为指令数据（当前该方法无效）
        /// </summary>
        /// <param name="po"></param>
        void getRa(PrinterObjects po);

        /// <summary>
        /// 发送控制指令数据到wifi上
        /// </summary>
        /// <param name="number">number值</param>
        /// <param name="data">发送的指令数据</param>
        /// <returns></returns>
        byte[] setWifiControl(string number, byte[] data,int type);

        /// <summary>
        /// 根据不同的指令内容解析数据信息
        /// </summary>
        /// <param name="codeid">指令名称</param>
        /// <param name="length">长度</param>
        /// <param name="reData">数据</param>
        /// <returns></returns>
        string getDifferentString(string codeid, int length, byte[] reData);
    }
}
