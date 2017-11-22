using System;
using System.Collections.Generic;
using System.Text;
using ClientPrsintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using System.Threading;
using System.Runtime.InteropServices;
using ClientPrsintsMethodList.ClientPrints.Method.Interfaces;
using ClientPrintsMethodList.ClientPrints.Method.DevBmpDll;
using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;

namespace ClientPrsintsMethodList.ClientPrints.Method.sharMethod
{
    public class PrinterUSBMethod:IPrinterMethod
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
            uint count=WDevDllMethod.dllFunc_EnumDevPath((ushort)WDevCmdObjects.USBPRN_PORT, path, ref size, 0x2867, 0x1802);
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

        public PrintersGeneralFunction general;
        public void getPrinterObjects()
        {
            string[] path = EnumPath();
            
            foreach (string pathAddress in path)
            {
                general = new PrintersGeneralFunction(pathAddress,this);
                
            }
        }

        /// <summary>
        /// 对指定图片进行打印
        /// </summary>
        /// <param name="pathFile">图片路径</param>
        /// <param name="pHandle">句柄值</param>
        public bool writeDataToDev(string pathFile,IntPtr pHandle)
        {
            if (pathFile == "")
                return false;
            var devProt = new structBmpClass.DeviceProterty()
            {
                dmThicken=1,//01指2位数，就是2色的意思
                nWidth=1016,
                nHeight=648,
                dmPrintQuality=0,
                dmYResolution=0,
                dmTag=0x01
            };
            var dsp = new structBmpClass.DS_PARAMETER()
            {
                devp = devProt,
                RGBPalete = IntPtr.Zero,
                RGBParameter = IntPtr.Zero
            };
            DevBmpDllMethod.setDeviceProterty(ref devProt);
            if (DevBmpDllMethod.LoadBitmapFilePara(pathFile, ref dsp))
            {
                IntPtr bites = DevBmpDllMethod.GetBits();
                int len = DevBmpDllMethod.GetLength();
                //IntPtr outBites = IntPtr.Zero;
                //int outSize = len+1;
                //int output = 0;
                //outBites = Marshal.AllocCoTaskMem(len+1);
                //output = DevBmpDllMethod.DS_Compress(7, bites, len,  outBites, out outSize);
                //uint outLen = 0;
                //if (WDevDllMethod.dllFunc_Write(pHandle, outBites, (uint)output, out outLen, IntPtr.Zero))
                if (WDevDllMethod.dllFunc_WriteEx(pHandle, bites, (uint)len, (uint)1, IntPtr.Zero))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
    }
}
