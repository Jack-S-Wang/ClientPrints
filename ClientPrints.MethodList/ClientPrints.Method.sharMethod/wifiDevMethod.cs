using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

                HttpWebRequest request = WebRequest.Create("") as HttpWebRequest;
                request.ContentType = "application/json";
                request.Method = WebRequestMethods.Http.Post;
                System.Net.ServicePointManager.DefaultConnectionLimit = 10000;

                return false;
                
            }catch(Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                return false;
            }
        }
    }
}
