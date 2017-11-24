using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
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
        
    }
}
