using System.Collections.Generic;
using ClientPrintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;

namespace ClientPrintsMethodList.ClientPrints.Method.sharMethod
{
    public class PrinterUSBMethod
    {
        
        /// <summary>
        /// 枚举出地址信息
        /// </summary>
        /// <returns></returns>
        public string[] EnumPath()
        {
            char[] path=new char[1000];
            uint size=1000;
            string address = "";
            uint count=WDevDllMethod.dllFunc_EnumDevPath((ushort)WDevCmdObjects.USBPRN_PORT, path, ref size, 0, 0);//0x2867, 0x1802
            if (count > 0)
            {
                List<string> result = new List<string>();
                for (int i = 0, j = 0; i < path.Length; ++i)
                {
                    if (path[i] == '\0')
                    {
                        if (i == j)
                        {
                            break;
                        }
                        address = new string(path, j, i - j);
                        j = i + 1;
                        result.Add(address);
                    }
                }

                return result.ToArray();
            }
            else
            {
                return new string[0];
            }
        }

      
       /// <summary>
       /// 获取USB型的打印机信息
       /// </summary>
        public void getPrinterObjects()
        {
            string[] path = EnumPath();
            WDevDllMethod.dllFunc_OpenLog(@"./DevLog.log");
            foreach (string pathAddress in path)
            {
                new PrintersGeneralFunction(pathAddress,new byte[0]);
            }
        }
    }
}
