using ClientPrintsMethodList.ClientPrints.Method.Interfaces;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objects.Printers.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters
{
    public class PrinterDC1300 : IUSBPrinterOnlyMethod
    {
        public string getDevInfo(byte[] data)
        {
            string jsonstr = "";
            if (data[1] == 1)
            {
                jsonstr = Encoding.UTF8.GetString(data, 3, data.Length - 3).Replace('\0',' ').Trim();
            }else if (data[1] == 2)
            {
                int InCache = (data[2] << 24) + (data[3] << 16) + (data[4] << 8) + data[5];
                int maxFrames = (data[6] << 24) + (data[7] << 16) + (data[8] << 8) + data[9];
                byte compressType = data[10];
                var info = new PrinterJson.PrinterDC1300DataInfo()
                {
                    InCache=InCache,
                    maxFrames=maxFrames,
                    compressType=compressType
                };
                jsonstr = JsonConvert.SerializeObject(info);
            }else if (data[1] == 3)
            {
                int maxWidth = (data[2] << 8) + data[3];
                int maxHeight = (data[4] << 8) + data[5];
                string confin = string.Format("{0:x2}{1:x2}{2:x2}{3:x2}",data[6],data[7],data[8],data[9]);
                int xDPL = data[10];
                int yDOPL = data[11];
                int colorDepth = data[12];
                byte pixelformat = data[13];
                byte isSupport = data[14];
                var info = new PrinterJson.PrinterDC1300PageInfo()
                {
                    maxWidth=maxWidth,
                    maxHeight=maxHeight,
                    colorDepth=colorDepth,
                    confin=confin,
                    isSupport=isSupport,
                    pixelformat=pixelformat,
                    xDPL=xDPL,
                    yDPL=yDOPL
                };
                jsonstr = JsonConvert.SerializeObject(info);
            }
            return jsonstr;
        }

        public string getPrinterState(byte[] data)
        {
            string jsonstr = "";
            string majorState = "";
            int stateCode = 0;
            string stateMessage = "";
            if (data[1] == 0x30)//系统状态
            {
                //主状态，主要是针对于在线的，***离线设为0，在线状态中值越大说明状态问题越大
                switch (data[2])
                {
                    case 0:
                        stateCode = 1;
                        majorState = "空闲";
                        break;
                    case 1:
                        stateCode = 3;
                        majorState = "工作中";
                        break;
                    case 2:
                        stateCode = 2;
                        majorState = "就绪";
                        break;
                    case 3:
                        stateCode = 4;
                        majorState = "繁忙";
                        break;
                    case 4:
                        stateCode = 5;
                        majorState = "暂停";
                        break;
                    case 0xFF:
                        stateCode = 6;
                        majorState = "异常";
                        break;
                }
                switch (data[4])
                {
                    case 0:
                        stateMessage = "无错误";
                        break;
                    case 1:
                        stateMessage = "缺卡";
                        break;
                    case 2:
                        stateMessage = "卡堵";
                        break;
                    case 3:
                        stateMessage = "归位错误";
                        break;
                    case 4:
                        stateMessage = "编码错误";
                        break;
                    case 5:
                        stateMessage = "卡片延误";
                        break;
                    case 6:
                        stateMessage = "面盖被打开";
                        break;
                    case 7:
                        stateMessage = "出卡异常";
                        break;
                    case 8:
                        stateMessage = "高温";
                        break;
                    case 9:
                        stateMessage = "低温";
                        break;
                    case 10:
                        stateMessage = "温度异常";
                        break;
                    case 11:
                        stateMessage = "传感器异常";
                        break;
                    case 12:
                        stateMessage = "存储访问异常";
                        break;
                }
                PrinterJson.PrinterDC1300State Pstate = new PrinterJson.PrinterDC1300State()
                {
                    stateCode = stateCode,
                    majorState = majorState,
                    StateMessage = stateMessage
                };
                jsonstr = JsonConvert.SerializeObject(Pstate);
            }
            else if (data[1] == 0x32)//数据处理状态,这里的状态按原来的值表示
            {

                switch (data[2])
                {
                    case 0:
                        stateCode = 0;
                        majorState = "等待/扫描帧数据";
                        break;
                    case 1:
                        stateCode = 1;
                        majorState = "数据匹配";
                        break;
                    case 2:
                        stateCode = 2;
                        majorState = "数据处理开始";
                        break;
                    case 3:
                        stateCode = 3;
                        majorState = "数据处理完成";
                        break;
                    case 4:
                        stateCode = 4;
                        majorState = "数据丢弃";
                        break;
                    case 5:
                        stateCode = 5;
                        majorState = "数据验证";
                        break;
                }
                int workIndex = (data[3] << 8) + data[4];
                int dataFrames = (data[5] << 8) + data[6];
                switch (data[7])
                {
                    case 0:
                        stateMessage = "无错误";
                        break;
                    case 1:
                        stateMessage = "ID号不匹配";
                        break;
                    case 2:
                        stateMessage = "头信息不匹配";
                        break;
                    case 3:
                        stateMessage = "作业号和帧号不匹配";
                        break;
                    case 4:
                        stateMessage = "数据校验错误";
                        break;
                    case 5:
                        stateMessage = "用户取消";
                        break;
                    case 6:
                        stateMessage = "数据读取长度不匹配";
                        break;
                    case 7:
                        stateMessage = "位图格式错误";
                        break;
                }
                var dState = new PrinterJson.PrinterDC1300DataState()
                {
                    stateCode = stateCode,
                    majorState = majorState,
                    StateMessage = stateMessage,
                    dataFrames = dataFrames,
                    workIndex = workIndex
                };
                jsonstr = JsonConvert.SerializeObject(dState);
            }
            else if (data[1] == 0x33)//打印状态，不需要排队状态码没有先后顺序
            {
                switch (data[2])
                {
                    case 0:
                        stateCode = 0;
                        majorState = "无";
                        break;
                    case 1:
                        stateCode = 1;
                        majorState = "输出任务正在工作";
                        break;
                }
                int taskNumber = data[3];
                int workIndex = (data[4] << 8) + data[5];
                int dataFrames = (data[6] << 8) + data[7];
                int tempertaure = data[8];
                string sensor = "";
                if (data[10] == 1)
                {
                    sensor = "风扇开启";
                }
                else
                {
                    sensor = "" + data[10];
                }
                var ppState = new PrinterJson.PrinterDC1300PrintState()
                {
                    stateCode = stateCode,
                    sensor = sensor,
                    majorState = majorState,
                    dataFrames = dataFrames,
                    taskNumber = taskNumber,
                    temperature = tempertaure,
                    workIndex = workIndex
                };
                jsonstr = JsonConvert.SerializeObject(ppState);
            }
            else if (data[1] == 0x34)
            {
                int InCache = (data[3] << 8) + data[4];
                int residueCache = (data[5] << 24) + (data[6] << 16) + (data[7] << 8) + data[8];
                var dpState = new PrinterJson.PrinterDC1300DataPortState()
                {
                    InCache = InCache,
                    residueCache = residueCache
                };
                jsonstr = JsonConvert.SerializeObject(dpState);
            }
            return jsonstr;
        }

        public string getDevParmInfo(byte[] data)
        {
            string jsonstr="";
            if (data[1] == 0x81)
            {
                byte[] parmData = new byte[data.Length];
                Array.Copy(data, 0, parmData, 0, data.Length);
                var pData = new PrinterJson.PrinterParmInfo()
                {
                    parmData=parmData
                };
                jsonstr = JsonConvert.SerializeObject(pData);
            }
            return jsonstr;
        }

    }
}
