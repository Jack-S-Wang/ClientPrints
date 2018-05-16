using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientPrintsMethodList.ClientPrints.Method.Wifi
{
    public class TcpClientSend
    {
        TcpClient client;
        NetworkStream stream;
        byte[] offset = new byte[1000];
        public byte[] getWifiData(string ip, string port, string number,string Sendname, byte[] data,int page,int total)
        {
            try
            {
                client.Connect(IPAddress.Parse(ip), Int32.Parse(port));
                if (client.Connected)
                {
                    stream=client.GetStream();
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("number", number);
                    dic.Add("data", Encoder(data));
                    if (Sendname.Equals("print_instruction"))
                    {
                        dic.Add("page", page);
                        dic.Add("total", total);
                    }
                    //stream.Write();
                }
                return new byte[0];
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                return new byte[0];
            }
        }

        string Encoder(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
    
}
