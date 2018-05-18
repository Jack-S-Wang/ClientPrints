using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using ClientPrintsMethodList.ClientPrints.Method.Wifi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass.tcpJsonrcp;

namespace ClientPrintsMethodList.ClientPrints.Method.sharMethod
{
    public class wifiDevMethod
    {
        public List<string> DevList = new List<string>(); 
        public bool getwifiDev()
        {
            try
            {
                TcpClientSend tcp = new TcpClientSend(SharMethod.serverIp, SharMethod.serverPort);
                JObject restr = tcp.getWifiData("anyone", "", SharMethod.FINDDEVICEMESSAGE, new byte[0], 0, 0);
                if (((string)restr["result"]).Equals("ok"))
                {
                    var pp = (string)restr["printer_message"];
                    var printers = JArray.Parse(pp);
                    foreach (var printer in printers)
                    {
                        new PrintersGeneralFunction(printer);
                        string number = (string)printer["number"];
                        DevList.Add(number);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                return false;
            }
        }
    }
}
