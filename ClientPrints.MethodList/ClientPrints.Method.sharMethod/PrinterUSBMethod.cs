using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientPrints.MethodList.ClientPrints.Method.WDevDll;
using ClientPrsints.ObjectsAll.ClientPrints.Objects.DevDll;
using System.Threading;
using System.Runtime.InteropServices;
using ClientPrints.ObjectsAll.ClientPrints.Objects.Printers;
using ClientPrints.MethodList.ClientPrints.Method.Interfaces;
using ClientPrints.MethodList.ClientPrints.Method.DevBmpDll;

namespace ClientPrints.MethodList.ClientPrints.Method.sharMethod
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

      
        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IntPtr openPrinter(string path)
        {
            IntPtr pHandle = new IntPtr(-1);
             structClassDll.LPPORTINFO lpInfo = new structClassDll.LPPORTINFO()
            {
                path = path,
                readTimeout=0,
                writeTimeout=0,
                portType=(ushort)WDevCmdObjects.USBPRN_PORT,
                portMode = (ushort)WDevCmdObjects.PORTINFO_PMODE_ALL
            };
            //用指针模式可以执行成功
            //unsafe{
            //    fixed (char* pathd = path)
            //    {
            //        lpInfo.path = pathd;
            //    }
            //}
             do
             {
                 Thread.Sleep(200);
                 pHandle = WDevDllMethod.dllFunc_OpenDev(ref lpInfo);
             }
             while (pHandle == new IntPtr(-1));
             return pHandle;
        }


      

        /// <summary>
        /// 获取对应指令的信息内容
        /// </summary>
        /// <param name="ctrlCodeStr">命令字符串</param>
        /// <param name="pHandle">句柄值</param>
        /// <param name="data">指令数据</param>
        /// <returns></returns>
        public  string reInformation(string ctrlCodeStr, IntPtr pHandle,byte[] data)
        {
            string LogText = "";
            structClassDll.DEVACK_INFO outDats = new structClassDll.DEVACK_INFO()
            {
                lpBuf=Marshal.AllocHGlobal(512),
                datLen=0,
                bufLen=512,
                ackCode=0
            };
            if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, ctrlCodeStr, ref data, (uint)data.Length, ref outDats))
            {
                if ((outDats.datLen > 0) || (outDats.datLen == 0 && outDats.ackCode == 0))
                {
                    byte[] reData = new byte[1000];
                    Marshal.Copy(outDats.lpBuf, reData, 0, outDats.datLen);
                    if (outDats.datLen > 0)
                    {
                        LogText = getDifferentString(ctrlCodeStr, outDats.datLen, reData);
                        
                    }
                }
            }
            return LogText;
        }

    

        /// <summary>
        /// 通过不同的指令获取不同数据来表示不同的内容(由于字节获取内容不一样，因此有些直接转换为字符串格式，有些则用数字直接获取)
        /// </summary>
        /// <param name="codeid">命令字符串</param>
        /// <param name="length">返回的事迹长度</param>
        /// <param name="reData">返回的实际数组</param>
        /// <returns></returns>
        private  string getDifferentString(string codeid, int length, byte[] reData)
        {
            string strCode = "";
            switch (codeid)
            {
                case WDevCmdObjects.DEV_GET_MODEL:
                    strCode = Encoding.GetEncoding("GBK").GetString(reData, 1, length-1).Replace('\0', ' ').TrimEnd();//第一个字节是1，表示打印机
                    break;
                case WDevCmdObjects.DEV_GET_DEVNO:
                    strCode = Encoding.GetEncoding("GBK").GetString(reData, 0, length).Replace('\0', ' ').TrimEnd();
                    break;
                case WDevCmdObjects.DEV_GET_PROTVER:
                    for (int i = 0; i < length; i++)
                    {
                        if (reData[i] >= 0 && reData[i] <= 32)
                        {
                            strCode = strCode + Convert.ToInt32(reData[i]);
                        }
                    }
                    break;
                case WDevCmdObjects.DEV_GET_DEVSTAT://设备状态
                    if (length > 6)
                    {
                        for (int i = 0; i < length; ++i)//前面四个字节代表四个不同的状态意思，后两位需要转换为值
                        {
                            if (reData[i] <= 32)
                            {
                                strCode = strCode + Convert.ToInt32(reData[i]);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < length - 2; ++i)//前面四个字节代表四个不同的状态意思，后两位需要转换为值
                        {
                            if (reData[i] <= 32)
                            {
                                strCode = strCode + Convert.ToInt32(reData[i]);
                            }
                        }
                        ushort temperature = (ushort)((reData[4] << 8) + reData[5]);
                        strCode = strCode + ";" + temperature;
                    }
                    break;
                case WDevCmdObjects.DEV_GET_PWSSTAT://加密状态
                    strCode = "" + Convert.ToInt32(reData[0]);
                    break;
                case WDevCmdObjects.DEV_GET_DEVINFO://设备信息
                    strCode = Encoding.GetEncoding("GBK").GetString(reData, 0, 16).Replace('\0', ' ').TrimEnd();
                    strCode += ";";
                    strCode = strCode + Encoding.GetEncoding("GBK").GetString(reData, 16, 17).Replace('\0', ' ').TrimEnd();
                    break;
                case WDevCmdObjects.DEV_GET_WORKMODE://工作模式
                    strCode = "" + Convert.ToInt32(reData[0]);
                    break;
                case WDevCmdObjects.DEV_GET_USERDAT:
                    string str = Encoding.GetEncoding("GBK").GetString(reData, 0, length).Replace('\0', ' ').TrimEnd();
                    for (int i = 0; i < str.Length; i++)//因为获取到的标识值时不干净的值，值后面的\0后面还有值
                    {
                        if (str[i] == ' ')
                        {
                            break;
                        }
                        else
                        {
                            strCode += str[i];
                        }
                    }
                    break;
                case WDevCmdObjects.DEV_GET_STATISINFO:
                    for (int i = 0; i < length; i++)
                    {
                        if (i % 4 == 0 && i != 0) strCode = strCode + ";";
                        strCode = strCode + reData[i];
                    }
                    break;
                case WDevCmdObjects.DEV_GET_VERINFO:
                    for (int i = 0; i < length; i++)
                    {
                        if (i < 10)
                        {
                            strCode = strCode + reData[i];
                        }
                    }
                    //大于10以上的都是用字符串来直接获取
                    strCode = strCode + Encoding.GetEncoding("GBK").GetString(reData, 10, length - 10);
                    break;
                case WDevCmdObjects.DEV_GET_CFGINFOS:
                    for (int i = 0; i < length; i++)
                    {
                        strCode = strCode + reData[i];
                    }
                    break;
            }
            return strCode;
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
