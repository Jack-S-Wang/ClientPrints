using ClientPrintsMethodList.ClientPrints.Method.DevBmpDll;
using ClientPrintsMethodList.ClientPrints.Method.Interfaces;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objects.Printers.JSON;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;

namespace ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters
{
    public class PrintersGeneralFunction : IMethodObjects
    {
        public string path = "";
        public string printerModel = "";

        public PrintersGeneralFunction(string address, byte[] Loginpassword)
        {
            this.path = address;
            //打开设备
            IntPtr phandle = openPrinter(path);
            //添加打印机对象
            printerMessage(path, phandle, Loginpassword);

        }
        /// <summary>
        /// 无参数设置对象
        /// </summary>
        public PrintersGeneralFunction()
        {

        }
        /// <summary>
        /// 获取wifi打印机信息
        /// </summary>
        /// <param name="printerWifi"></param>
        public PrintersGeneralFunction(JToken printerWifi)
        {
            //赋值wifi设备的信息内容
            setPrinterWifiInfo(printerWifi);
        }


        /// <summary>
        /// 获取wifi信息内容并存储
        /// </summary>
        /// <param name="printerWifi">获取的wifi设备数据信息</param>
        private void setPrinterWifiInfo(JToken printerWifi)
        {
            PrinterParams printerParams = new PrinterParams();
            int getmaxHeight = 0;
            int getmaxWidth = 0;
            string number = (string)printerWifi["number"];
            string alias = (string)printerWifi["alias"];
            bool alive = (bool)printerWifi["alive"];
            string main = (string)printerWifi["status"]["main"];
            string sub = "";
            try
            {
                JToken arr = printerWifi["status"]["subs"];
                foreach (JToken jk in arr)
                {
                    sub = (string)jk;
                }
            }
            catch
            {
                sub = "";
            }
            string sn = (string)printerWifi["info"]["sn"];
            string vendor = (string)printerWifi["info"]["vendor"];
            string model = (string)printerWifi["info"]["model"];
            printerModel = model;
            int dpi = (int)printerWifi["info"]["dpi"];
            string pageWidth = (string)printerWifi["info"]["pageWidth"];
            string onlyAlias = number;
            if (alias == "" || alias == null)
            {
                alias = onlyAlias;
            }
            int stateType = 0;
            string state = "";
            switch (main)
            {
                case "idle":
                    state = "空闲";
                    stateType = 1;
                    break;
                case "working":
                    state = "工作中";
                    stateType = 3;
                    break;
                case "ready":
                    state = "就绪";
                    stateType = 2;
                    break;
                case "busy":
                    state = "繁忙";
                    stateType = 4;
                    break;
                case "paused":
                    state = "暂停";
                    stateType = 5;
                    break;
                case "error":
                    state = "异常";
                    stateType = 6;
                    break;
            }
            if (!alive)
            {
                state = "离线";
                stateType = 0;
            }
            else
            {
                if (sub != "noConnectDevice")
                {
                    try
                    {
                        byte[] redata = new byte[0];


                        //设备model
                        byte[] data = new byte[] { 0x10, 0x01, 0x00, 0 };
                        redata = setWifiControl(number, data, 1);
                        byte[] rdata = new byte[redata[2]];
                        Array.Copy(redata, 4, rdata, 0, redata[2]);
                        string modelInfo = getDifferentString(WDevCmdObjects.DEV_GET_MODEL, redata[2], rdata);

                        printerModel = modelInfo;
                        model = modelInfo;




                        //设备数据信息
                        data = new byte[] { 0x10, 0x0C, 0x01, 0x02, 0x02 };
                        redata = setWifiControl(number, data, 1);
                        byte[] ndata = new byte[redata[2]];
                        Array.Copy(redata, 4, ndata, 0, redata[2]);
                        string dataInfo = getDifferentString(WDevCmdObjects.DEV_GET_DEVINFO, redata[2], ndata);
                        int InCache = 0;
                        int maxFrames = 0;
                        byte compressType = 0;
                        switch (model)
                        {
                            case "DC-1300":
                                var Datajson = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300DataInfo>(dataInfo);
                                InCache = Datajson.InCache;
                                maxFrames = Datajson.maxFrames;
                                compressType = Datajson.compressType;
                                break;
                            case "DL-210":
                                var Data210json = JsonConvert.DeserializeObject<PrinterDL210Json.PrinterDL210DataInfo>(dataInfo);
                                InCache = Data210json.InCache;
                                maxFrames = Data210json.maxFrames;
                                compressType = Data210json.compressType;
                                break;

                        }
                        //设备页面信息
                        data = new byte[] { 0x10, 0x0C, 0x01, 0x03, 0x03 };
                        redata = setWifiControl(number, data, 1);
                        ndata = new byte[redata[2]];
                        Array.Copy(redata, 4, ndata, 0, redata[2]);
                        string pageInfo = getDifferentString(WDevCmdObjects.DEV_GET_DEVINFO, redata[2], ndata);
                        int colorDepth = 0;
                        string confin = "";
                        byte isSupport = 0;
                        int maxHeight = 0;
                        int maxWidth = 0;
                        byte pixelformat = 0;
                        int xDPL = 0;
                        int yDPL = 0;
                        switch (model)
                        {
                            case "DC-1300":
                                var Pagejson = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300PageInfo>(pageInfo);
                                colorDepth = Pagejson.colorDepth;
                                confin = Pagejson.confin;
                                isSupport = Pagejson.isSupport;
                                maxHeight = Pagejson.maxHeight;
                                maxWidth = Pagejson.maxWidth;
                                pixelformat = Pagejson.pixelformat;
                                xDPL = Pagejson.xDPL;
                                yDPL = Pagejson.yDPL;
                                break;
                            case "DL-210":
                                var Page210json = JsonConvert.DeserializeObject<PrinterDL210Json.PrinterDL210PageInfo>(pageInfo);
                                colorDepth = Page210json.colorDepth;
                                confin = Page210json.confin;
                                isSupport = Page210json.isSupport;
                                maxHeight = Page210json.maxHeight;
                                maxWidth = Page210json.maxWidth;
                                pixelformat = Page210json.pixelformat;
                                xDPL = Page210json.xDPL;
                                yDPL = Page210json.yDPL;
                                break;
                        }
                        getmaxWidth = maxWidth;
                        getmaxHeight = maxHeight;
                        //设备系统参数信息
                        PrinterJson.PrinterParmInfo infoParm = new PrinterJson.PrinterParmInfo();
                        bool isInfoParm = false;
                        data = new byte[] { 0x10, 0x1B, 0x01, 0x81, 0x81 };
                        redata = setWifiControl(number, data, 1);
                        ndata = new byte[redata[2]];
                        Array.Copy(redata, 4, ndata, 0, redata[2]);
                        string DevParmInfo = getDifferentString(WDevCmdObjects.DEV_GET_SYSPARAM, redata[2], ndata);
                        if (!DevParmInfo.Contains("false"))
                        {
                            isInfoParm = true;
                            infoParm = JsonConvert.DeserializeObject<PrinterJson.PrinterParmInfo>(DevParmInfo);
                        }
                        //输出作业
                        data = new byte[] { 0x10, 0x09, 0x01, 0x33, 0x33 };
                        redata = setWifiControl(number, data, 1);
                        ndata = new byte[redata[2]];
                        Array.Copy(redata, 4, ndata, 0, redata[2]);
                        string printOutPut = getDifferentString(WDevCmdObjects.DEV_GET_DEVSTAT, redata[2], ndata);
                        int workIndex = 0;
                        switch (model)
                        {
                            case "DC-1300":
                                var printOut = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300PrintState>(printOutPut);
                                workIndex = printOut.workIndex;
                                break;
                            case "DL-210":
                                var print210Out = JsonConvert.DeserializeObject<PrinterDL210Json.PrinterDL210PrintState>(printOutPut);
                                workIndex = print210Out.workIndex;
                                break;
                        }

                        printerParams = new PrinterParams()
                        {
                            DIP = xDPL,
                            devInfo = "",
                            InCache = InCache,
                            maxFrames = maxFrames,
                            compressType = compressType,
                            colorDepth = colorDepth,
                            confin = confin,
                            isSupport = isSupport,
                            maxHeight = maxHeight,
                            maxWidth = maxWidth,
                            pixelformat = pixelformat,
                            xDPL = xDPL,
                            yDPL = yDPL,
                            DevParm = infoParm.parmData,
                            outJobNum = workIndex,
                            IsdevInfoParm = isInfoParm
                        };
                        if (workIndex == 65535)
                        {
                            printerParams.outJobNum = 0;
                        }
                        var edata = Encoding.GetEncoding("GBK").GetBytes("登录设备" + number + "成功!");
                        int acode = 0;
                        for (int i = 0; i < edata.Length; i++)
                        {
                            acode += edata[i];
                        }
                        byte[] dataYu = new byte[4 + edata.Length];
                        dataYu[0] = 0x10;
                        dataYu[1] = 0x70;
                        dataYu[2] = (byte)edata.Length;
                        dataYu[3] = (byte)acode;
                        Array.Copy(edata, 0, dataYu, 4, edata.Length);
                        setWifiControl(number, dataYu, 0);
                    }
                    catch (Exception ex)
                    {
                        string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0},wifi数据返回无法正确解析，追踪位置信息：{1}", ex, ex.StackTrace);
                        SharMethod.writeErrorLog(str);
                    }
                }
            }
            var printers = new PrinterObjects()
            {
                sn = sn,
                model = model,
                version = "",
                alias = alias,
                vedor = vendor,
                pHandle = IntPtr.Zero,
                MethodsObject = this,
                addressMessage = "",
                onlyAlias = onlyAlias,
                stateMessage = sub,
                state = state,
                stateCode = stateType,
                pParams = printerParams,
                isWifi = true
            };
            printers.pParams.page = (((double)getmaxWidth / 300) * 25.4).ToString("0.00") + "*" + (((double)getmaxHeight / 300) * 25.4).ToString("0.00");
            SharMethod.dicPrinterObject.Add(number, printers);
            SharMethod.liAllPrinter.Add(printers);
        }

        /// <summary>
        /// 发送控制指令数据到wifi上
        /// </summary>
        /// <param name="number"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] setWifiControl(string number, byte[] data, int type)
        {
            //TcpClientSend SendInfo = new TcpClientSend(SharMethod.serverIp, SharMethod.serverPort);
            //var getInfo = SendInfo.getWifiData("", number, SharMethod.CONTROLINSTRUCTION, data, 0, 0,type);
            //if (((string)getInfo["result"]).Equals("ok"))
            //{
            //string dataStr = (string)getInfo["data"];
            //byte[] redata = Convert.FromBase64String(dataStr);
            //return redata;
            //}
            //else
            //{
            return new byte[0];
            //}
        }

        /// <summary>
        /// 获取打印机的详细信息内容，并记录到对象中
        /// </summary>
        /// <param name="pathAddress">枚举的地址信息</param>
        /// <param name="pHandle">句柄值</param>
        private void printerMessage(string pathAddress, IntPtr pHandle, byte[] Loginpassword)
        {
            string stateMessage = "";
            string state = "";
            int stateType = 0;
            byte[] redata = new byte[0];
            //打开设备连接默认没有密码
            if (reInformation(WDevCmdObjects.DEV_CMD_CONNT, pHandle, ref Loginpassword).Contains("false"))
            {
                WDevDllMethod.dllFunc_CloseDev(pHandle);
                if (!SharMethod.passwordError.Contains(pathAddress))
                {
                    SharMethod.passwordError.Add(pathAddress);
                }
                return;
            }
            else
            {
                //设备型号
                redata = new byte[0];
                string model = reInformation(WDevCmdObjects.DEV_GET_MODEL, pHandle, ref redata);
                printerModel = model;
                if (model.Contains("false"))
                {
                    return;
                }
                //设备通用信息
                //序列号
                redata = new byte[0];
                string sn = reInformation(WDevCmdObjects.DEV_GET_DEVNO, pHandle, ref redata);
                //版本号
                redata = new byte[0];
                string version = reInformation(WDevCmdObjects.DEV_GET_PROTVER, pHandle, ref redata);

                //标识
                //string onlyAlias = reInformation(WDevCmdObjects.DEV_GET_USERDAT, pHandle, new byte[] { 0x00, 0x00 });

                //if (onlyAlias == "")
                //{
                //    Guid gu = Guid.NewGuid();
                //    byte[] data = Encoding.UTF8.GetBytes(gu.ToString("N"));
                //    byte[] data1 = new byte[data.Length + 2];
                //    Array.Copy(data, 0, data1, 2, data.Length);
                //    //设置标识
                //    if (!reInformation(WDevCmdObjects.DEV_SET_USERDAT, pHandle, data1).Contains("false"))
                //    {
                //        reInformation(WDevCmdObjects.DEV_SET_USERDAT, pHandle, new byte[] { 0xff, 0xff });
                //    }
                //    onlyAlias = reInformation(WDevCmdObjects.DEV_GET_USERDAT, pHandle, new byte[] { 0, 0 });
                //}
                string onlyAlias = sn;
                string alias = onlyAlias;


                //系统状态
                redata = new byte[] { 0x0 };

                string jsonState = reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, pHandle, ref redata);
               
                dataJson dj = new dataJson();
                //获取主次状态和输出作业号
                string mainState = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "System State.Run State");
                if (mainState.Contains("idle"))
                {
                    state = "空闲";
                    stateType = 1;
                }
                else if (mainState.Contains("At work"))
                {
                    state = "工作中";
                    stateType = 3;
                }
                else if (mainState.Contains("Ready"))
                {
                    state = "就绪";
                    stateType = 2;
                }
                else if (mainState.Contains("Busy"))
                {
                    state = "繁忙";
                    stateType = 4;
                }
                else if (mainState.Contains("Pause"))
                {
                    state = "暂停";
                    stateType = 5;
                }
                else if (mainState.Contains("Error"))
                {
                    state = "异常";
                    stateType = 6;
                }
                string error = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "System State.Error");
                error = error.Substring(error.IndexOf(';') + 1);
                stateMessage = state + ";" + error;
                string inwork = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Print State.Completed Job Number");
                int workIndex = Int32.Parse(inwork);

                
                //厂商
                string vendor = "DASCOM";

                //设备数据信息
                redata = new byte[] { 0 };
                string dataInfo = reInformation(WDevCmdObjects.DEV_GET_DEVINFO, pHandle, ref redata);
                int InCache = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Interface.Recvice"));
                int maxFrames = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Interface.Frame"));
                byte compressType = byte.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Interface.Compress"));
                bool bType = false;
                string DevInfo = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Generic.Datas");
                switch (printerModel)
                {
                    case "DC-1300":
                        if (!DevInfo.Contains("01.01.00.04"))
                        {
                            bType = true;
                        }
                        break;
                    case "DL-210":
                        if (!DevInfo.Contains("00.20.01.41"))
                        {
                            bType = true;
                        }
                        break;
                }


                if (bType)
                {
                    WDevDllMethod.dllFunc_CloseDev(pHandle);
                    SharMethod.banError.Add(pathAddress);
                    return;
                }
                
                //设备页面信息
                int maxWidth = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Page.Width"));
                int maxHeight = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Page.Height"));
                string confin= dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Page.Margin");
                int xDPL = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Page.XDPI"));
                int yDPL = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Page.YDPI"));
                int colorDepth = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Page.BPPS"));
                string p = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Page.BMP_FMT");
                p = p.Substring(0, p.IndexOf(';'));
                byte pixelformat = byte.Parse(p);
                string s = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Page.DSBMP");
                s = s.Substring(0, s.IndexOf(';'));
                byte isSupport = byte.Parse(s);
                
                //设备系统参数信息
                PrinterJson.PrinterParmInfo infoParm = new PrinterJson.PrinterParmInfo();
                bool isInfoParm = false;
                redata = new byte[] { 0x81 };
                string DevParmInfo = reInformation(WDevCmdObjects.DEV_GET_SYSPARAM, pHandle, ref redata);
                if (!DevParmInfo.Contains("false"))
                {
                    isInfoParm = true;
                    infoParm = JsonConvert.DeserializeObject<PrinterJson.PrinterParmInfo>(DevParmInfo);
                }


                var printerParams = new PrinterParams()
                {
                    DIP = xDPL,
                    devInfo = DevInfo,
                    InCache = InCache,
                    maxFrames = maxFrames,
                    compressType = compressType,
                    colorDepth = colorDepth,
                    confin = confin,
                    isSupport = isSupport,
                    maxHeight = maxHeight,
                    maxWidth = maxWidth,
                    pixelformat = pixelformat,
                    xDPL = xDPL,
                    yDPL = yDPL,
                    DevParm = infoParm.parmData,
                    outJobNum = workIndex,
                    IsdevInfoParm = isInfoParm
                };
                if (workIndex == 65535)
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
                    pParams = printerParams,
                    isWifi = false
                };
                printers.pParams.page = (((double)maxWidth / 300) * 25.4).ToString("0.00") + "*" + (((double)maxHeight / 300) * 25.4).ToString("0.00");
                SharMethod.dicPrinterObject.Add(pathAddress, printers);
            }
        }

        void getStateAndNumber(byte[] data, dataJson dj)
        {

        }

        /// <summary>
        /// 获取对应指令的信息内容
        /// </summary>
        /// <param name="ctrlCodeStr">命令字符串</param>
        /// <param name="pHandle">句柄值</param>
        /// <param name="data">指令M0--Mn个数据,并返回已经获取的数据</param>
        /// <returns></returns>
        public string reInformation(string ctrlCodeStr, IntPtr pHandle, ref byte[] data)
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
                    byte[] reData = new byte[outDats.datLen];
                    Marshal.Copy(outDats.lpBuf, reData, 0, outDats.datLen);
                    if (outDats.datLen > 0)
                    {
                        data = reData;
                        LogText = getDifferentString(ctrlCodeStr, outDats.datLen, reData);
                    }
                    else
                    {
                        LogText = "true";
                    }
                }
                else
                {
                    LogText = "false 有错误码：" + outDats.ackCode;
                }
            }
            else
            {
                LogText = "false 无错误码";
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
        public string getDifferentString(string codeid, int length, byte[] reData)
        {
            string strCode = "";
            IUSBPrinterOnlyMethod onlyMethod;
            switch (codeid)
            {
                case WDevCmdObjects.DEV_GET_MODEL:
                    strCode = Encoding.GetEncoding("GBK").GetString(reData, 1, length - 1).Replace('\0', ' ').Trim();//第一个字节是1，表示打印机
                    break;
                case WDevCmdObjects.DEV_GET_DEVNO:
                    if (reData[0] == 1)
                    {
                        strCode = Encoding.GetEncoding("GBK").GetString(reData, 1, length - 1).Replace('\0', ' ').Trim();
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
                    //switch (printerModel)
                    //{
                    //    case "DC-1300":
                    //        onlyMethod = new PrinterDC1300();
                    //        strCode = onlyMethod.getPrinterState(reData);
                    //        break;
                    //    case "DL-210":
                    //        onlyMethod = new PrinterDL210();
                    //        strCode = onlyMethod.getPrinterState(reData);
                    //        break;

                    //}
                    strCode = "true";
                    break;
                case WDevCmdObjects.DEV_GET_PWSSTAT://加密状态
                    strCode = "" + Convert.ToInt32(reData[0]);
                    break;
                case WDevCmdObjects.DEV_GET_DEVINFO://设备信息
                    //switch (printerModel)
                    //{
                    //    case "DC-1300":
                    //        onlyMethod = new PrinterDC1300();
                    //        strCode = onlyMethod.getDevInfo(reData);
                    //        break;
                    //    case "DL-210":
                    //        onlyMethod = new PrinterDL210();
                    //        strCode = onlyMethod.getDevInfo(reData);
                    //        break;
                    //}
                    strCode = "true";
                    break;
                case WDevCmdObjects.DEV_GET_WORKMODE://工作模式
                    strCode = "" + Convert.ToInt32(reData[0]);
                    break;
                case WDevCmdObjects.DEV_GET_USERDAT://用户自定义标识
                    int index = length;
                    for (int i = 0; i < length; i++)
                    {
                        if (reData[i] > 0x7f || reData[i] < 0x21)//取到特殊的字节无法解析就跳出，ASCII码
                        {
                            index = i - 1;
                            break;
                        }
                    }
                    if (index > 0)
                    {
                        byte[] data = new byte[index];
                        Array.Copy(reData, data, data.Length);
                        string str = Encoding.GetEncoding("GBK").GetString(data, 0, data.Length).Replace('\0', ' ').Trim();
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
                    }
                    else
                    {
                        return "";
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
                    switch (printerModel)
                    {
                        case "DC-1300":
                            onlyMethod = new PrinterDC1300();
                            strCode = onlyMethod.getDevParmInfo(reData);
                            break;
                        case "DL-210":
                            onlyMethod = new PrinterDL210();
                            strCode = onlyMethod.getDevParmInfo(reData);
                            break;

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
                    //加大一个像素是因为有些图片大小相等时无法打印出来                               
                    nWidth = (short)(po.pParams.printWidth),
                    nHeight = (short)(po.pParams.printHeight),
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

                if (DevBmpDllMethod.LoadBitmapFile(pathFile))
                {
                    IntPtr bites = DevBmpDllMethod.GetBits();
                    int len = DevBmpDllMethod.GetLength();


                    byte[] devinfo = new byte[] { 4, 0, 0, 0 };
                    if (po.pParams.IsdevInfoParm)
                    {
                        var dev = new byte[4 + po.pParams.DevParm.Length];
                        devinfo.CopyTo(dev, 0);
                        Array.Copy(po.pParams.DevParm, 0, dev, 4, po.pParams.DevParm.Length);
                        devinfo = dev;
                        devinfo[0] = (byte)(4 + po.pParams.DevParm.Length);
                    }

                    var bmp = new byte[20];
                    bmp[0] = (byte)(WDevCmdObjects.DEVBMP_ID);
                    bmp[1] = (byte)(WDevCmdObjects.DEVBMP_ID >> 8);
                    bmp[2] = (byte)(po.pParams.DIP);
                    bmp[3] = (byte)(po.pParams.DIP >> 8);
                    bmp[4] = (byte)(po.pParams.posX);
                    bmp[5] = (byte)(po.pParams.posX >> 8);
                    bmp[6] = (byte)(po.pParams.posY);
                    bmp[7] = (byte)(po.pParams.posY >> 8);
                    bmp[8] = (byte)(po.pParams.printWidth);
                    bmp[9] = (byte)(po.pParams.printWidth >> 8);
                    bmp[10] = (byte)(po.pParams.printHeight);
                    bmp[11] = (byte)(po.pParams.printHeight >> 8);
                    bmp[12] = bmp[13] = 0;
                    bmp[14] = (byte)po.pParams.colorDepth;
                    bmp[15] = (byte)po.pParams.pixelformat;
                    bmp[16] = bmp[17] = bmp[18] = bmp[19] = 0;

                    var bmpDev = new byte[bmp.Length + devinfo.Length];
                    Array.Copy(bmp, 0, bmpDev, 0, bmp.Length);
                    Array.Copy(devinfo, 0, bmpDev, bmp.Length, devinfo.Length);

                    var tmp = new byte[len];
                    Marshal.Copy(bites, tmp, 0, len);

                    var memblockSize = bmpDev.Length + len;
                    var memblock = Marshal.AllocHGlobal(memblockSize);

                    var bmpPtr1 = IntPtr.Add(memblock, 0);
                    Marshal.Copy(bmpDev, 0, bmpPtr1, bmpDev.Length);

                    var bmpPtr = IntPtr.Add(memblock, (bmpDev.Length));
                    Marshal.Copy(tmp, 0, bmpPtr, len);
                    bmpDev = null;

                    tmp = null;
                    bool success = false;
                    string error = "";
                    if (Int32.Parse(jobnum) == 1)//业务号为一时
                    {
                        outJobNum = po.pParams.outJobNum;
                    }

                    for (int i = 0; i < num; i++)
                    {
                        if (printerClose.closeWindow)
                        {
                            break;
                        }
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
                            //System.IO.FileStream file = new System.IO.FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\" + DateTime.Now.ToString("HH.mm.ss") + "image.cmpr", System.IO.FileMode.OpenOrCreate);
                            //var tmp1 = new byte[memblockSize];
                            //Marshal.Copy(memblock, tmp1, 0, memblockSize);
                            //file.Write(tmp1, 0, memblockSize);
                            //file.Flush();
                            //file.Close();
                            //file.Dispose();

                            do
                            {
                                if (printerClose.closeWindow)
                                {
                                    break;
                                }
                                if (!po.isWifi)
                                {
                                    byte[] redata = new byte[] { 0x30 };
                                    string jsonState = reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, po.pHandle, ref redata);
                                    var keyState = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300State>(jsonState);
                                    if (keyState.stateCode != 4)
                                    {
                                        success = WDevDllMethod.dllFunc_WriteEx(po.pHandle, memblock, (uint)memblockSize, (uint)3, ref lope);
                                        break;
                                    }
                                }
                                else
                                {
                                    byte[] data = new byte[] { 0x10, 0x09, 0x01, 0x30, 0x30 };
                                    byte[] redata = setWifiControl(po.onlyAlias, data, 1);
                                    byte[] ndata = new byte[redata[2]];
                                    Array.Copy(redata, 4, ndata, 0, redata[2]);
                                    string jsonState = getDifferentString(WDevCmdObjects.DEV_GET_DEVSTAT, redata[2], ndata);
                                    var keyState = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300State>(jsonState);
                                    if (keyState.stateCode != 4)
                                    {
                                        byte[] printData = new byte[memblockSize];
                                        Marshal.Copy(memblock, printData, 0, memblockSize);
                                        success = setWifiPrintData(po.onlyAlias, printData, 1, 1);
                                        //using (System.IO.FileStream file = new System.IO.FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\" + DateTime.Now.ToString("HH.mm.ss") + "printData.bin", System.IO.FileMode.OpenOrCreate))
                                        //{
                                        //    var tmp1 = new byte[memblockSize];
                                        //    Marshal.Copy(memblock, tmp1, 0, memblockSize);
                                        //    file.Write(tmp1, 0, memblockSize);
                                        //}
                                        //using (System.IO.FileStream file = new System.IO.FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\" + DateTime.Now.ToString("HH.mm.ss") + "printData.txt", System.IO.FileMode.OpenOrCreate))
                                        //{
                                        //    var tmp1 = new byte[memblockSize];
                                        //    Marshal.Copy(memblock, tmp1, 0, memblockSize);
                                        //    string str = Convert.ToBase64String(tmp1);
                                        //    byte[] datastr = Encoding.UTF8.GetBytes(str);
                                        //    file.Write(datastr, 0, datastr.Length);
                                        //}
                                        break;
                                    }
                                }
                            } while (true);
                        }
                        catch (Exception ex)
                        {
                            li.Add("error");
                            li.Add("打印方法执行失败！");
                            SharMethod.writeErrorLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":动态库调用方法执行异常！" + string.Format("异常追踪：{0}", ex.StackTrace));
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


        public void getRa(PrinterObjects po)
        {
            var info = new structClassDll.DEVREQ_INFO2()
            {
                //cmdCodeStr = WDevCmdObjects.DEV_GET_DEVNO,
                cmdCodeStr = WDevCmdObjects.DEV_CMD_CONNT,
                devPktBuf = Marshal.AllocHGlobal(512),
                pktDatLen = 512,
                lpDat = new byte[0],
                datIdx = 0,
                datLen = 0
            };
            bool f = WDevDllMethod.devRawDatsREQ(ref info, IntPtr.Zero, 1);
            var ack = new structClassDll.DEVACK_INFO2()
            {

            };
        }

        /// <summary>
        /// 发送打印数据到wifi设备上
        /// </summary>
        /// <param name="number">number值</param>
        /// <param name="data">打印数据</param>
        /// <param name="page">打印当前页</param>
        /// <param name="total">打印总页数</param>
        /// <returns></returns>
        public bool setWifiPrintData(string number, byte[] data, int page, int total)
        {
            try
            {
                //TcpClientSend sendPrint = new TcpClientSend(SharMethod.serverIp, SharMethod.serverPort);
                //var result = sendPrint.getWifiData("", number, SharMethod.PRINTINSTRUCTION, data, page, total,1);
                //if (((string)result["result"]).Equals("ok"))
                //{
                //    return true;
                //}
                //else
                //{
                return false;
                //}
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                return false;
            }
        }

    }
}
