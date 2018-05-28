using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class tcpJsonrcp
    {
        public class messageRpc
        {
            public string result { get; set; }
            public printerMessage[] printer_message { get; set; }
            public string id { get; set; }
            public string josnrpc = "2.0";
        }
        public class printerMessage
        {
            public string number { get; set; }//设备编号
            public DateTime reg_date { get; set; }//设备注册时间
            public DateTime login_date { get; set; }//设备登录时间
            public string alias { get; set; }//别名
            public bool alive { get; set; }//是否在线
            public string owner { get; set; }//用户名
            //public printerAlert alert { get; set; }
            public printerStatus status { get; set; }
            public printerInfo info { get; set; }//包含打印机的各种信息
        }

        /// <summary>
        /// 报警手段
        /// </summary>
        public class printerAlert
        {
            public string email { get; set; }//管理员邮箱
            public string url { get; set; }//Http地址
            public bool need { get; set; }//是否报警
        }

        public class printerStatus
        {
            public string main { get; set; }//主状态
            public List<string> subs { get; set; }//子状态
            public DateTime newest { get; set; }//操作时间
        }

        public class printerInfo
        {
            public string sn { get; set; }
            public string vendor { get; set; }
            public string model { get; set; }
            public int dpi { get; set; }
            public string pageWidth { get; set; }

            //wifi运用的
            //public string wifi_model { get; set; }
            //public string wifi_protocol_version { get; set; }
            //public string wifi_firmware_version { get; set; }
            //public string wifi_mainboard_number { get; set; }
            //public string wifi_mac { get; set; }
            //public string wifi_ssid { get; set; }
        }

        public class otherRPC
        {
            public string result { get; set; }
            public string id { get; set; }
            public string data { get; set; }//bit64位字符串
            public string jsonrpc = "2.0";
        }
    }
}
