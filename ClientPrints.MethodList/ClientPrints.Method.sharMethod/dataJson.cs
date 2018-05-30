using ClientPrintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ClientPrintsMethodList.ClientPrints.Method.sharMethod
{
    public class dataJson
    {
        public void getDataJsonInfo(byte[] data, uint devEntpy)
        {
            IntPtr pt = Marshal.AllocCoTaskMem(data.Length);
            Marshal.Copy(data, 0, pt, data.Length);
            lodeFile();
            jsonKeyDic jk = new jsonKeyDic();
            if (devEntpy == 1)
            {
                foreach (var mk in jk.cfgKey)
                {
                    showNodeVal(devEntpy, pt, (uint)data.Length, mk);
                }
            }
        }
        /// <summary>
        /// 导入文件内容
        /// </summary>
        /// <returns></returns>
        public void lodeFile()
        {
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
            for (int i = 0; i < filestr.Length; i++)
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
                }
            }
        }

        /// <summary>
        /// 通过后辍名获取完整路径
        /// </summary>
        /// <param name="dinfo"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
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


        private void showNodeVal(uint entpy, IntPtr pt, uint len, string mk)
        {
            IntPtr npt = Marshal.AllocCoTaskMem(256);
            structClassDll.UNION union = new structClassDll.UNION()
            {
                lpDats = npt
            };
            structClassDll.NODEITEM_VAL mVal = new structClassDll.NODEITEM_VAL()
            {
                datLen = 256,
                union = union
            };
            structClassDll.JSVAL_INFO valInfo = new structClassDll.JSVAL_INFO()
            {
                jsEntry = entpy,
                keyPath = mk,
                tag = WDevCmdObjects.JSVAL_TAG_DATVAL
            };
            if (WDevJsonDll.dllFunc_getDevJsonVal(ref valInfo, pt, len, ref mVal))
            {
                switch (mVal.type)
                {
                    case WDevCmdObjects.NODE_VAL_NONE:
                        break;
                    case WDevCmdObjects.NODE_VAL_LONG:
                        break;
                    case WDevCmdObjects.NODE_VAL_STR:
                        break;
                    case WDevCmdObjects.NODE_VAL_LIST:
                        break;
                    case WDevCmdObjects.NODE_VAL_DATA:
                        break;
                    case WDevCmdObjects.NODE_VAL_MULTISTR:
                        break;
                }
                
            }
            
        }
    }
}
