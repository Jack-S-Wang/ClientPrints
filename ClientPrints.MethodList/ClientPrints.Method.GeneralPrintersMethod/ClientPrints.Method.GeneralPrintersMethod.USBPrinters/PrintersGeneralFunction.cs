using ClientPrintsMethodList.ClientPrints.Method.DevBmpDll;
using ClientPrintsMethodList.ClientPrints.Method.Interfaces;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objects.Printers.JSON;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrsintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters
{
    public class PrintersGeneralFunction : IMethodObjects
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
            //设备通用信息
            //序列号
            string sn = reInformation(WDevCmdObjects.DEV_GET_DEVNO, pHandle, new byte[0]);
            //版本号
            string version = reInformation(WDevCmdObjects.DEV_GET_PROTVER, pHandle, new byte[0]);

            //标识
            string onlyAlias = reInformation(WDevCmdObjects.DEV_GET_USERDAT, pHandle, new byte[] { 0x00, 0x00 });
            string DevInfo = reInformation(WDevCmdObjects.DEV_GET_DEVINFO, pHandle, new byte[] { 1 });
            if (!DevInfo.Contains("01.01.00.03"))
            {
                WDevDllMethod.dllFunc_CloseDev(pHandle);
                throw (new Exception("设备：" + onlyAlias + ":版本不一致,需要固件更新！"));
            }

            //系统状态
            string jsonState = reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, pHandle, new byte[] { 0x30 });
            var keyState = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300State>(jsonState);
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
                reInformation(WDevCmdObjects.DEV_SET_USERDAT, pHandle, data1);
                onlyAlias = reInformation(WDevCmdObjects.DEV_GET_USERDAT, pHandle, new byte[] { 0, 0 });
            }
            string alias = onlyAlias;
            //厂商
            string vendor = "DASCOM";

            //设备数据信息
            string dataInfo = reInformation(WDevCmdObjects.DEV_GET_DEVINFO, pHandle, new byte[] { 2 });
            var Datajson = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300DataInfo>(dataInfo);
            //设备页面信息
            string pageInfo = reInformation(WDevCmdObjects.DEV_GET_DEVINFO, pHandle, new byte[] { 3 });
            var Pagejson = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300PageInfo>(pageInfo);
            //设备系统参数信息
            string DevParmInfo = reInformation(WDevCmdObjects.DEV_GET_SYSPARAM, pHandle, new byte[] { 0x81 });
            var devParmInfoJson = JsonConvert.DeserializeObject<PrinterJson.PrinterParmInfo>(DevParmInfo);
            //输出作业
            var printOutPut = reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, pHandle, new byte[] { 0x33 });
            var printOut = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300PrintState>(printOutPut);

            var printerParams = new PrinterParams()
            {
                devInfo = DevInfo,
                InCache = Datajson.InCache,
                maxFrames = Datajson.maxFrames,
                compressType = Datajson.compressType,
                colorDepth = Pagejson.colorDepth,
                confin = Pagejson.confin,
                isSupport = Pagejson.isSupport,
                maxHeight = Pagejson.maxHeight,
                maxWidth = Pagejson.maxWidth,
                pixelformat = Pagejson.pixelformat,
                xDPL = Pagejson.xDPL,
                yDPL = Pagejson.yDPL,
                DevParm = devParmInfoJson.parmData,
                outJobNum = printOut.workIndex
            };
            if (printOut.workIndex == 65535)
            {
                printerParams.outJobNum = 0;
            }
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
                pParams = printerParams
            };
            if (model.Contains("DC-1300"))
            {
                printers.pParams.page = (((double)Pagejson.maxWidth / 300) * 25.4).ToString("0.00") + "*" + (((double)Pagejson.maxHeight / 300) * 25.4).ToString("0.00");
            }
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
                    else
                    {
                        LogText = "true";
                    }
                }
                else
                {
                    LogText = "false";
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
                case WDevCmdObjects.DEV_GET_PROTVER://仿真规范版本
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
                        strCode = onlyMethod.getPrinterState(reData);
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
                case WDevCmdObjects.DEV_GET_USERDAT://用户自定义标识
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
                case WDevCmdObjects.DEV_GET_STATISINFO://数据统计
                    for (int i = 0; i < length; i++)
                    {
                        if (i % 4 == 0 && i != 0) strCode = strCode + ";";
                        strCode = strCode + reData[i];
                    }
                    break;
                case WDevCmdObjects.DEV_GET_VERINFO://固件版本号
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
                case WDevCmdObjects.DEV_GET_CFGINFOS://配置信息
                    for (int i = 0; i < length; i++)
                    {
                        strCode = strCode + reData[i];
                    }
                    break;
                case WDevCmdObjects.DEV_GET_SYSPARAM://设备参数
                    if (printerModel.Contains("DC-1300"))
                    {
                        IUSBPrinterOnlyMethod onlyMethod = new PrinterDC1300();
                        strCode = onlyMethod.getDevParmInfo(reData);
                    }
                    break;
                case WDevCmdObjects.DEV_SET_SYSPARAM://设置设备参数
                    strCode = reData[0] + "";
                    break;
                case WDevCmdObjects.DEV_GET_CFGFMT://设备格式，只要有信息说明成功！
                    strCode = Encoding.UTF8.GetString(reData);
                    break;
            }
            return strCode;
        }


        private int outJobNum = 0;
        /// <summary>
        /// 对指定图片进行打印
        /// </summary>
        /// <param name="pathFile">图片路径</param>
        /// <param name="po">打印机对象</param>
        /// <param name="jobnum">业务号</param>
        /// <param name="num">打印数量</param>
        public List<string> writeDataToDev(string pathFile, PrinterObjects po, string jobnum, int num)
        {
            try
            {
                List<string> li = new List<string>();
                if (pathFile == "")
                {
                    li.Add("error");
                    li.Add("无图片路径");
                    return li;
                }
                var devProt = new structBmpClass.DeviceProterty()
                {
                    dmThicken = (short)po.pParams.colorDepth,//01指2位数，就是2色的意思
                    nWidth = (short)(po.pParams.maxWidth + 1),
                    nHeight = (short)(po.pParams.maxHeight + 1),
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
                        bkBmpID = po.pParams.bkBmpID,
                        cardInputMode = po.pParams.DevParm[2],
                        cardOutputMode = po.pParams.DevParm[3],
                        cardType = po.pParams.DevParm[1],
                        devType = (byte)WDevCmdObjects.BMP_DEVPROP_PRN,
                        eraseTemp = po.pParams.DevParm[9],
                        wipeSpeed = po.pParams.DevParm[8],
                        grayTemp = po.pParams.DevParm[7],
                        printContrast = po.pParams.DevParm[5],
                        printMode = po.pParams.DevParm[10],
                        printSpeed = po.pParams.DevParm[6],
                        printTemp = po.pParams.DevParm[4],
                        revs = new byte[3],
                        propSize = (byte)Marshal.SizeOf(typeof(structClassDll.DEVPROP_PRNOUT))
                    };

                    var devinfo = new structClassDll.DEVPROP_INFO()
                    {
                        revs = new byte[2],
                        size = (ushort)(4 + devprop.propSize),
                        prnProp = devprop

                    };
                    var devbm = new structClassDll.DEV_BMP()
                    {
                        bkPixelH = 0,
                        txPixelH = (ushort)po.pParams.maxHeight,
                        bmpType = po.pParams.pixelformat,
                        bpps = (byte)po.pParams.colorDepth,
                        dpi = (ushort)po.pParams.DIP,
                        ID = (ushort)WDevCmdObjects.DEVBMP_ID,
                        pixelW = (ushort)po.pParams.maxWidth,
                        posX = po.pParams.posX,
                        posY = po.pParams.posY,
                        ret = new byte[4],
                        devInfo = devinfo
                    };

                    var tmp = new byte[len];
                    Marshal.Copy(bites, tmp, 0, len);

                    var memblockSize = Marshal.SizeOf(devbm) + len;
                    var memblock = Marshal.AllocHGlobal(memblockSize);

                    Marshal.StructureToPtr(devbm, memblock, false);
                    var bmpPtr = IntPtr.Add(memblock, Marshal.SizeOf(devbm));
                    Marshal.Copy(tmp, 0, bmpPtr, len);

                    tmp = null;
                    bool success = false;
                    string error = "";
                    if (Int32.Parse(jobnum) == 1)//业务号为一时
                    {
                        outJobNum = po.pParams.outJobNum;
                    }

                    for (int i = 0; i < num; i++)
                    {

                        outJobNum = outJobNum + 1;
                        var lope = new structClassDll.UNCMPR_INFO()
                        {
                            cmprLen = 0,
                            uncmprLen = 0,
                            stat = 0,
                            jobNumber = (ushort)outJobNum,
                            resultTag = 0,
                            cmprType = 0,
                            frmIdx = 0,
                            userParm = "DevLog.log"
                        };
                        try
                        {
                            //System.IO.FileStream file = new System.IO.FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClinetPrints\\" + DateTime.Now.ToString("HH.mm.ss") + "image.cmpr", System.IO.FileMode.OpenOrCreate);
                            //var tmp1 = new byte[memblockSize];
                            //Marshal.Copy(memblock, tmp1, 0, memblockSize);
                            //file.Write(tmp1, 0, memblockSize);
                            //file.Flush();
                            //file.Dispose();
                            //file.Close();
                            success = WDevDllMethod.dllFunc_WriteEx(po.pHandle, memblock, (uint)memblockSize, (uint)3, ref lope);
                        }
                        catch (Exception ex)
                        {
                            li.Add("error");
                            li.Add("打印方法执行失败！");
                            SharMethod.writeErrorLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":动态库调用方法执行异常！"+string.Format("异常追踪：{0}",ex.StackTrace));
                        }
                        if (!success)
                        {
                            error = "打印方法执行失败！已打印:" + (outJobNum - (po.pParams.outJobNum + 1)) + "份";
                            break;
                        }
                    }
                    Marshal.FreeHGlobal(memblock);
                    WDevDllMethod.dllFunc_CloseLog(po.pHandle);
                    if (!success)
                    {
                        li.Add("error");
                        li.Add(error);
                        return li;
                    }
                    else
                    {
                        li.Add("Ok");
                        li.Add("任务号：" + jobnum + "已发送完毕！");
                        return li;
                    }
                }
                else
                {
                    li.Add("error");
                    li.Add("打印失败！");
                    return li;
                }
            }
            finally
            {
                if (System.IO.File.Exists(pathFile))
                {
                    System.IO.File.Delete(pathFile);
                }
            }

        }


    }
}
