using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass.tcpJsonrcp;

namespace ClientPrintsMethodList.ClientPrints.Method.Wifi
{
    public class TcpClientSend
    {
        /// <summary>
        /// 每次使用tcp时重新定义对象，是属于短连接,*不能全局定义
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public TcpClientSend(string ip, string port)
        {
            this.ip = ip;
            this.port = port;
        }
        string ip = "";
        string port = "";
        TcpClient client = new TcpClient();
        NetworkStream stream;
        /// <summary>
        ///通过tcp获取对应的数据内容
        /// </summary>
        /// <param name="owner">第一次请求获取数据对应的账号名称，其他请求为空</param>
        /// <param name="number">其他请求中所请求的设备number值，账户获取时可赋空值</param>
        /// <param name="Sendname">发送指令对应的名称</param>
        /// <param name="data">发送控制指令，数据指令或更新版本时的数据内容，无数据内容则赋空数据</param>
        /// <param name="page">发送数据指令时当前页面号，其他可为0</param>
        /// <param name="total">发送数据指令时总共页数,其他可为0</param>
        /// <returns></returns>
        public JObject getWifiData(string owner, string number, string Sendname, byte[] data, int page, int total)
        {
            JObject restrobj = new JObject();
            client.Connect(IPAddress.Parse(ip), Int32.Parse(port));
            if (client.Connected)
            {
                stream = client.GetStream();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (Sendname.Equals(SharMethod.FINDDEVICEMESSAGE))
                {
                    dic.Add("owner", owner);
                }
                else
                {
                    dic.Add("number", number);
                    dic.Add("data", Encoder(data));
                    if (Sendname.Equals(SharMethod.PRINTINSTRUCTION))
                    {
                        dic.Add("page", page);
                        dic.Add("total", total);
                    }
                    if (Sendname.Equals(SharMethod.CONTROLINSTRUCTION))
                    {
                        dic.Add("type", "1");
                    }
                }
                Guid guid = Guid.NewGuid();
                string id = guid.ToString("N");
                Dictionary<string, object> dicjson = new Dictionary<string, object>();
                dicjson.Add("method", Sendname);
                dicjson.Add("params", dic);
                dicjson.Add("id", id);
                dicjson.Add("jsonrpc", "2.0");
                var json = JsonConvert.SerializeObject(dicjson);
                byte[] setData = Encoding.UTF8.GetBytes(json);
                stream.Write(setData, 0, setData.Length);
                var sr = new System.IO.StreamReader(stream);
                var jtr = new JsonTextReader(sr);
                JsonSerializer js = new JsonSerializer();
                var jsonobj = (JObject)js.Deserialize(jtr);
                restrobj = jsonobj;
            }
            else
            {
                throw new Exception("TCP服务未连接成功！");
            }

            return restrobj;
        }

        string Encoder(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
    
}
