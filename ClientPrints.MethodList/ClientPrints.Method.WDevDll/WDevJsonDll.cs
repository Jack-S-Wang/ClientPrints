using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ClientPrintsMethodList.ClientPrints.Method.WDevDll
{
    public class WDevJsonDll
    {
        [DllImport("wDevJson.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool dllFunc_openLog(string filePath);

        [DllImport("wDevJson.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool dllFunc_loadDevJson(byte[] jsonDoc, uint devJsonEntry);

        [DllImport("wDevJson.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool dllFunc_resetDevJson(uint devJsonEntry);

        [DllImport("wDevJson.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool dllFunc_outputDevJson(uint devJsonEntry, string keyPath, IntPtr param);

        [DllImport("wDevJson.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool dllFunc_getDevJsonInfo(ref structClassDll.DEVJSON_INFO devJsonEntry, ref IntPtr buf,
            ref uint dLen, uint mode);

        [DllImport("wDevJson.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool dllFunc_getDevJsonVal(ref structClassDll.JSVAL_INFO valInfo,
            IntPtr dBuf, uint dLen, ref structClassDll.NODEITEM_VAL val);

        [DllImport("wDevJson.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool dllFunc_setDevJsonVal(ref structClassDll.JSVAL_INFO valInfo,
            IntPtr dBuf, uint dLen, ref structClassDll.NODEITEM_VAL val);

        

        [DllImport("wDevJson.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool dllFunc_fflushLog();

        [DllImport("wDevJson.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool dllFunc_closeLog();
    }
}
