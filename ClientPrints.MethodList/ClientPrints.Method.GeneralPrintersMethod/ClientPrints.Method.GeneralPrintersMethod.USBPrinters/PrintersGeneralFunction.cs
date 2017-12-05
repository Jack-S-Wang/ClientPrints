using ClientPrintsMethodList.ClientPrints.Method.DevBmpDll;
using ClientPrintsMethodList.ClientPrints.Method.Interfaces;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objects.Printers.JSON;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrsintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters
{
    public class PrintersGeneralFunction
    {
        public string path = "";
        public string printerModel = "";
        public PrintersGeneralFunction(string address)
        {
            this.path = address;
            //打开设备
            IntPtr phandle = openPrinter(path);
            //添加打印机对象
            printerMessage(path, phandle);

        }

        /// <summary>
        /// 获取打印机的详细信息内容，并记录到对象中
        /// </summary>
        /// <param name="pathAddress">枚举的地址信息</param>
        /// <param name="pHandle">句柄值</param>
        private void printerMessage(string pathAddress, IntPtr pHandle)
        {
            string stateMessage = "";
            string state = "";
            int stateType = 0;
            //打开设备连接默认没有密码
            if (reInformation(WDevCmdObjects.DEV_CMD_CONNT, pHandle, new byte[0]).Equals("false"))
            {
                return;
            }
            //设备型号           
            string model = reInformation(WDevCmdObjects.DEV_GET_MODEL, pHandle, new byte[0]);
            printerModel = model;
            //序列号
            string sn = reInformation(WDevCmdObjects.DEV_GET_DEVNO, pHandle, new byte[0]);
            //版本号
            string version = reInformation(WDevCmdObjects.DEV_GET_PROTVER, pHandle, new byte[0]);
            //标识
            string onlyAlias = reInformation(WDevCmdObjects.DEV_GET_USERDAT, pHandle, new byte[] { 0x00, 0x00 });

            //系统状态
            string jsonState = reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, pHandle, new byte[] { 0x30 });
            var keyState=JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300State>(jsonState);
            stateType = keyState.stateCode;
            stateMessage = keyState.majorState + ":" + keyState.StateMessage;
            state = keyState.majorState;
            if (onlyAlias == "")
            {
                Guid gu = new Guid();
                byte[] data = Encoding.UTF8.GetBytes(gu.ToString("N"));
                byte[] data1 = new byte[data.Length + 2];
                Array.Copy(data, 0, data1, 2, data.Length);
                //设置标识
                reInformation(WDevCmdObjects.DEV_SET_USERDAT, pHandle,data1);
                onlyAlias = reInformation(WDevCmdObjects.DEV_GET_USERDAT, pHandle, new byte[] { 0,0});
            }
            string alias = onlyAlias;
            //厂商
            string vendor = "DASCOM";
            //设备通用信息
            string DevInfo=reInformation(WDevCmdObjects.DEV_GET_DEVINFO, pHandle, new byte[] { 1 });
            //设备数据信息
            string dataInfo= reInformation(WDevCmdObjects.DEV_GET_DEVINFO, pHandle, new byte[] { 2 });
            var Datajson = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300DataInfo>(dataInfo);
            //设备页面信息
            string pageInfo= reInformation(WDevCmdObjects.DEV_GET_DEVINFO, pHandle, new byte[] { 3 });
            var Pagejson = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300PageInfo>(pageInfo);

            var printerParams = new PrinterParams()
            {
                devInfo=DevInfo,
                InCache=Datajson.InCache,
                maxFrames=Datajson.maxFrames,
                compressType=Datajson.compressType,
                colorDepth=Pagejson.colorDepth,
                confin=Pagejson.confin,
                isSupport=Pagejson.isSupport,
                maxHeight=Pagejson.maxHeight,
                maxWidth=Pagejson.maxWidth,
                pixelformat=Pagejson.pixelformat,
                xDPL=Pagejson.xDPL,
                yDPL=Pagejson.yDPL
            };


            var printers = new PrinterObjects()
            {
                sn = sn,
                model = model,
                version = version,
                alias = alias,
                vedor = vendor,
                pHandle = pHandle,
                MethodsObject = this,
                addressMessage = pathAddress,
                onlyAlias = onlyAlias,
                stateMessage = stateMessage,
                state = state,
                stateCode = stateType,
                pParams=printerParams
            };
            SharMethod.dicPrinterUSB.Add(pathAddress, printers);
        }

        /// <summary>
        /// 获取对应指令的信息内容
        /// </summary>
        /// <param name="ctrlCodeStr">命令字符串</param>
        /// <param name="pHandle">句柄值</param>
        /// <param name="data">指令数据</param>
        /// <returns></returns>
        public string reInformation(string ctrlCodeStr, IntPtr pHandle, byte[] data)
        {
            string LogText = "";
            structClassDll.DEVACK_INFO outDats = new structClassDll.DEVACK_INFO()
            {
                lpBuf = Marshal.AllocHGlobal(512),
                datLen = 0,
                bufLen = 512,
                ackCode = 0
            };
            if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, ctrlCodeStr, data, (uint)data.Length, ref outDats))
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
            else
            {
                LogText = "false";
            }
            return LogText;
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
                readTimeout = 0,
                writeTimeout = 0,
                portType = (ushort)WDevCmdObjects.USBPRN_PORT,
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
        /// 通过不同的指令获取不同数据来表示不同的内容(由于字节获取内容不一样，因此有些直接转换为字符串格式，有些则用数字直接获取)
        /// </summary>
        /// <param name="codeid">命令字符串</param>
        /// <param name="length">返回的事迹长度</param>
        /// <param name="reData">返回的实际数组</param>
        /// <returns></returns>
        private string getDifferentString(string codeid, int length, byte[] reData)
        {
            string strCode = "";
            switch (codeid)
            {
                case WDevCmdObjects.DEV_GET_MODEL:
                    strCode = Encoding.GetEncoding("GBK").GetString(reData, 1, length - 1).Replace('\0', ' ').TrimEnd();//第一个字节是1，表示打印机
                    break;
                case WDevCmdObjects.DEV_GET_DEVNO:
                    if (reData[0] == 1)
                    {
                        strCode = Encoding.GetEncoding("GBK").GetString(reData, 1, length - 1).Replace('\0', ' ').TrimEnd();
                    }
                    else
                    {
                        for (int i = 1; i < length; i++)
                        {
                            strCode = strCode + Convert.ToInt32(reData[i]);
                        }
                    }
                    break;
                case WDevCmdObjects.DEV_GET_PROTVER:
                    for (int i = 0; i < length; i++)
                    {
                        if (reData[i] >= 0 && reData[i] <= 32)
                        {
                            strCode = strCode + Convert.ToInt32(reData[i]) + ".";
                        }
                    }
                    strCode = strCode.Substring(0, strCode.Length - 1);
                    break;
                case WDevCmdObjects.DEV_GET_DEVSTAT://设备状态
                    if (printerModel.Contains("DC-1300"))
                    {
                        IUSBPrinterOnlyMethod onlyMethod = new PrinterDC1300();
                        strCode=onlyMethod.getPrinterState(reData);
                    }
                    break;
                case WDevCmdObjects.DEV_GET_PWSSTAT://加密状态
                    strCode = "" + Convert.ToInt32(reData[0]);
                    break;
                case WDevCmdObjects.DEV_GET_DEVINFO://设备信息
                    if (printerModel.Contains("DC-1300"))
                    {
                        IUSBPrinterOnlyMethod onlyMethod = new PrinterDC1300();
                        strCode = onlyMethod.getDevInfo(reData);
                    }
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
        public bool writeDataToDev(string pathFile, PrinterObjects po)
        {
            if (pathFile == "")
                return false;

           
            var devProt = new structBmpClass.DeviceProterty()
            {
                dmThicken = 1,//01指2位数，就是2色的意思
                nWidth = 648,
                nHeight = 1016,
                dmPrintQuality = 0,
                dmYResolution = 0,
                dmTag = 0x01
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

                var devprop = new structClassDll.DEVPROP_PRNOUT()
                {
                    bkBmpID = (byte)WDevCmdObjects.BMP_DEVPROP_BKNONE,
                    cardInputMode = 1,
                    cardOutputMode = 1,
                    cardType = 0,
                    devType = (byte)WDevCmdObjects.BMP_DEVPROP_PRN,
                    eraseTemp = 10,
                    graySpeed = 10,
                    grayTemp = 10,
                    printContrast = 10,
                    printMode = 0,
                    printSpeed = 10,
                    printTemp = 15,
                    revs = new byte[3],
                    propSize=(byte)Marshal.SizeOf(typeof(structClassDll.DEVPROP_PRNOUT))
                };
                
                var devinfo = new structClassDll.DEVPROP_INFO()
                {
                    revs = new byte[2],
                    size=(ushort)(4+devprop.propSize),
                    prnProp=devprop
                   
                };
                var devbm = new structClassDll.DEV_BMP()
                {
                    bkPixelH = 0,
                    txPixelH = 1016,
                    bmpType = po.pParams.pixelformat,
                    bpps = (byte)po.pParams.colorDepth,
                    dpi = (ushort)po.pParams.DIP,
                    ID = (ushort)WDevCmdObjects.DEVBMP_ID,
                    pixelW = 648,
                    posX=0,
                    posY=0,
                    ret=new byte[4],
                    devInfo=devinfo
                };
              
                var tmp = new byte[len];
                Marshal.Copy(bites, tmp, 0, len);

                var memblockSize = Marshal.SizeOf(devbm) + len;
                var memblock = Marshal.AllocHGlobal(memblockSize);

                Marshal.StructureToPtr(devbm, memblock, false);
                var bmpPtr = IntPtr.Add(memblock, Marshal.SizeOf(devbm));
                Marshal.Copy(tmp, 0, bmpPtr, len);

                var lope = new structClassDll.UNCMPR_INFO()
                {
                    cmprLen =0,
                    uncmprLen =0,
                    stat = 0,
                    jobNumber = 1,
                    resultTag = 0,
                    cmprType = 0,
                    frmIdx = 0,
                    userParm= "wDevObj.log"
                };
                tmp = null;
                bool success = WDevDllMethod.dllFunc_WriteEx(po.pHandle, memblock, (uint)memblockSize, (uint)3, ref lope);
                Marshal.FreeHGlobal(memblock);
                WDevDllMethod.dllFunc_CloseLog(po.pHandle);
                return success;
               
            }
            else
            {
                return false;
            }
        }

    }
}
