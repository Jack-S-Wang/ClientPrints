using ClientPrintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClientPrintsMethodList.ClientPrints.Method.sharMethod
{
    public class dataJson
    {
        public void getDataJsonInfo(byte[] data, uint devEntpy)
        {
            var dic=lodeFile();
            foreach(var dk in dic)
            {
                if (dk.Value == devEntpy)
                {
                    JObject jo = JObject.Parse(dk.Key);
                    if ((int)jo.First.First["DevID"] == data[0] && (int)jo.First.First["CfgID"] ==data[1] 
                        && (int)jo.First.First["CfgVer"] ==data[2] && devEntpy==1)
                    {
                        foreach (var jtokn in jo)
                        {
                            if (jtokn.Key != "Info")
                            {
                                foreach (var ck in jtokn.Value)
                                {
                                    string p = ck.Path;
                                }
                            }
                        }
                    }else if((int)jo.First.First["DevID"] == data[1] && (int)jo.First.First["ByteUnit"] == data[2]
                        && devEntpy == 2)
                    {

                    }
                }
            }
        }
        /// <summary>
        /// 导入文件内容并返回
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, uint> lodeFile()
        {
            Dictionary<string, uint> dic = new Dictionary<string, uint>();
            string path = "C:\\Project\\wDevJsonLib\\json";
            string[] filestr = new string[]
            {
                "\\config\\","\\info\\"
            };
            uint[] entpystr = new uint[]
            {
                (uint)WDevCmdObjects.DEVJSON_CFG_ENTRY,
                (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY
            };
            for (int i=0;i<filestr.Length;i++)
            {
                string loadP = path + filestr[i];
                var li = getFilePath(new DirectoryInfo(loadP), "*.json");
                foreach (var pathName in li)
                {
                    List<byte> code = new List<byte>();
                    using (FileStream file = new FileStream(pathName, FileMode.Open))
                    {
                        byte[] buff = new byte[1024];
                        while (true)
                        {
                            int count = file.Read(buff, 0, buff.Length);
                            if (count == 0)
                            {
                                break;
                            }
                            byte[] rdata = new byte[count];
                            Array.Copy(buff, rdata, count);
                            code.AddRange(rdata);
                        }
                    }
                    WDevJsonDll.dllFunc_loadDevJson(code.ToArray(), entpystr[i]);
                    string str = Encoding.UTF8.GetString(code.ToArray());
                    dic.Add(str, entpystr[i]);
                }
            }
            return dic;
        }

        private List<string> getFilePath(DirectoryInfo dinfo, string pattern)
        {
            List<string> li = new List<string>();
            if (dinfo.Exists || pattern.Trim() != string.Empty)
            {
                foreach (FileInfo info in dinfo.GetFiles(pattern))
                {
                    li.Add(info.FullName);
                }
            }
            return li;
        }
    }
}
