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
        public List<CfgDataObjects> listCfg = new List<CfgDataObjects>();
        public dataJson()
        {
            lodeFile();
        }

        /// <summary>
        /// 循环获取所有对应key值的value值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="devEntpy"></param>
        /// <param name="keyList"></param>
        /// <param name="isCfgObject"></param>
        public void getDataJsonInfo(byte[] data, uint devEntpy, List<string> keyList, bool isCfgObject)
        {
            IntPtr pt = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, pt, data.Length);
            foreach (var mk in keyList)
            {
                string name = mk;
                showNodeVal(devEntpy, pt, (uint)data.Length, ref name, true);
            }
        }

        public string getDataJsonInfo(byte[] data, uint devEntpy, string key)
        {
            IntPtr pt = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, pt, data.Length);
            string kk = key;
            showNodeVal(devEntpy, pt, (uint)data.Length, ref kk, false);
            return kk;
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="devEntpy"></param>
        /// <param name="mk"></param>
        /// <param name="value"></param>
        /// <param name="selectIndex"></param>
        public void setDataJsonInfo(ref byte[] data, uint devEntpy, string mk, string value, int selectIndex)
        {
            IntPtr pt = Marshal.AllocCoTaskMem(data.Length);
            Marshal.Copy(data, 0, pt, data.Length);
            data = setNodeVal(devEntpy, pt, (uint)data.Length, mk, value, selectIndex);
        }
        /// <summary>
        /// 导入文件内容
        /// </summary>
        /// <returns></returns>
        private bool lodeFile()
        {
            bool flag = false;
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
                    if (WDevJsonDll.dllFunc_loadDevJson(code.ToArray(), entpystr[i]))
                    {
                        flag = true;
                    }
                }
            }
            return flag;
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

        /// <summary>
        /// 获取对应键值的信息内容
        /// </summary>
        /// <param name="entpy">类型，区分cfg与info</param>
        /// <param name="pt">数据指针</param>
        /// <param name="len">数据长度</param>
        /// <param name="mk">查询的键值</param>
        /// <param name="isCfgObject">是否将获取的值进行存入cfg数据对象里</param>
        private void showNodeVal(uint entpy, IntPtr pt, uint len, ref string mk, bool isCfgObject)
        {
            string reVal = "";
            structClassDll.NODEITEM_VAL mVal = new structClassDll.NODEITEM_VAL();
            structClassDll.JSVAL_INFO valInfo = new structClassDll.JSVAL_INFO()
            {
                jsEntry = entpy,
                keyPath = mk,
                tag = WDevCmdObjects.JSVAL_TAG_DATVAL
            };
            int dlen = 0;
            int mcount = 0;
            while (!WDevJsonDll.dllFunc_getDevJsonVal(ref valInfo, pt, len, ref mVal))
            {
                dlen = mVal.datLen;
                IntPtr npt = Marshal.AllocHGlobal(mVal.datLen*2);
                structClassDll.UNION union = new structClassDll.UNION()
                {
                    lpDats = npt
                };
                mVal.union = union;
                mVal.datLen = (ushort)(mVal.datLen * 2);
                mcount++;
                if (mcount == 3)
                {
                    break;
                }
            }
            IntPtr npt1 = Marshal.AllocHGlobal(dlen*2);
            structClassDll.UNION union1 = new structClassDll.UNION()
            {
                lpDats = npt1
            };
            mVal = new structClassDll.NODEITEM_VAL();
            mVal.union = union1;
            mVal.datLen = (ushort)(dlen*2);
            if (WDevJsonDll.dllFunc_getDevJsonVal(ref valInfo, pt, len, ref mVal))
            {

                switch (mVal.type)
                {
                    case WDevCmdObjects.NODE_VAL_NONE:
                        break;
                    case WDevCmdObjects.NODE_VAL_LONG:
                        if (isCfgObject)
                        {
                            CfgDataObjects cdo = new CfgDataObjects(mk, mVal.val.ToString(), "", mVal.type);
                            listCfg.Add(cdo);
                        }
                        reVal = mVal.val.ToString();
                        break;
                    case WDevCmdObjects.NODE_VAL_STR:
                        byte[] datastr = new byte[mVal.datLen];
                        Marshal.Copy(mVal.union.lpDats, datastr, 0, datastr.Length);
                        int index = datastr.Length;
                        for (int i = 0; i < datastr.Length; i++)
                        {
                            if (datastr[i] == '\0')
                            {
                                index = i;
                                break;
                            }
                        }
                        byte[] ndata = new byte[index];
                        Array.Copy(datastr, ndata, index);
                        reVal = Encoding.UTF8.GetString(ndata);
                        if (isCfgObject)
                        {
                            CfgDataObjects cdo = new CfgDataObjects(mk, reVal, "", mVal.type);
                            listCfg.Add(cdo);
                        }
                        break;
                    case WDevCmdObjects.NODE_VAL_LIST:
                        string mmk = (mk + ".ListVal");
                        showNodeVal(entpy, pt, len, ref mmk, false);
                        if (isCfgObject)
                        {
                            CfgDataObjects cdo = new CfgDataObjects(mk, mVal.val.ToString(), mmk, mVal.type);
                            listCfg.Add(cdo);
                        }
                        byte[] liStr = new byte[mVal.datLen];
                        Marshal.Copy(mVal.union.lpDats, liStr, 0, liStr.Length);
                        int ind = liStr.Length;
                        for (int i = 0; i < liStr.Length; i++)
                        {
                            if (liStr[i] == '\0')
                            {
                                ind = i;
                                break;
                            }

                        }
                        byte[] ldata = new byte[ind];
                        Array.Copy(liStr, ldata, ind);
                        string str = Encoding.UTF8.GetString(ldata);
                        reVal = mVal.val.ToString() + ";" + str;
                        break;
                    case WDevCmdObjects.NODE_VAL_DATA:
                        byte[] dataS = new byte[mVal.datLen];
                        Marshal.Copy(mVal.union.lpDats, dataS, 0, dataS.Length);
                        if (mVal.tag == WDevCmdObjects.ITEMFMT_TAG_IP)
                        {
                            reVal = dataS[0] + "." + dataS[1] + "." + dataS[2] + "." + dataS[3];
                        }
                        else
                        {
                            int i = 0;
                            while (i < mVal.datLen)
                            {
                                if (i >= 8)
                                {
                                    reVal += string.Format("{0:X2},{1:X2},{2:X2},{3:X2},{4:X2},{5:X2},{6:X2},{7:X2}",
                                        dataS[i], dataS[i + 1], dataS[i + 2], dataS[i + 3], dataS[i + 4], dataS[i + 5]
                                        + dataS[i + 6], dataS[i + 7]);
                                    i += 8;
                                }
                                else
                                {
                                    reVal += string.Format("{0:x2}", dataS[i]);
                                    i++;
                                }
                            }
                        }
                        if (isCfgObject)
                        {
                            CfgDataObjects cdo = new CfgDataObjects(mk, reVal, "", mVal.type);
                            listCfg.Add(cdo);
                        }
                        break;
                    case WDevCmdObjects.NODE_VAL_MULTISTR:
                        byte[] ms = new byte[mVal.datLen];
                        Marshal.Copy(mVal.union.lpDats, ms, 0, ms.Length);
                        //int idx = ms.Length;
                        //for (int i = 0; i < ms.Length; i++)
                        //{
                        //    if (ms[i] == '\0')
                        //    {
                        //        idx = i;
                        //        break;
                        //    }
                        //}
                        //byte[] md = new byte[idx];
                        //Array.Copy(ms, md, idx);
                        reVal = Encoding.UTF8.GetString(ms);
                        if (isCfgObject)
                        {
                            CfgDataObjects cdo = new CfgDataObjects(mk, reVal, "", mVal.type);
                            listCfg.Add(cdo);
                        }
                        break;
                }
            }

            mk = reVal;
        }

        private byte[] setNodeVal(uint entpy, IntPtr pt, uint len, string mk, string value, int selectIndex)
        {
            IntPtr npt = Marshal.AllocHGlobal(500);
            structClassDll.UNION union = new structClassDll.UNION()
            {
                lpDats = npt
            };
            structClassDll.NODEITEM_VAL mVal = new structClassDll.NODEITEM_VAL()
            {
                datLen = 500,
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
                if (mVal.type == 1)
                {
                    mVal.val = Int32.Parse(value);
                    mVal.union.lpDats = Marshal.StringToBSTR(value);
                }
                else if (mVal.type == 3 || mVal.type == 2)
                {
                    mVal.val = selectIndex;
                    mVal.union.lpDats = Marshal.StringToBSTR(value);
                }
                else if (mVal.type == 4)
                {
                    mVal.val = selectIndex;
                    int count = 0;
                    if (mVal.tag == 4)
                    {
                        byte[] dt = new byte[4];
                        string v = "";
                        for (int i = 0; i < value.Length; i++)
                        {
                            if (value[i] == '.')
                            {
                                dt[count] = (byte)Int32.Parse(v);
                                v = "";
                                count++;
                            }
                            else
                            {
                                v += value[i];
                                if (i == value.Length - 1)
                                {
                                    dt[count] = Byte.Parse(v);
                                }
                            }
                        }
                        Marshal.Copy(dt, 0, mVal.union.lpDats, 4);
                    }
                    else
                    {

                    }

                }

                if (WDevJsonDll.dllFunc_setDevJsonVal(ref valInfo, pt, len, ref mVal))
                {
                    byte[] redata = new byte[len];
                    Marshal.Copy(pt, redata, 0, (int)len);
                    return redata;
                }
            }
            return new byte[0];
        }
    }
}
