using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ClientPrsintsMethodList.ClientPrints.Method.WDevDll
{
    public class WDevDllMethod
    {
        /// <summary>
        /// 枚举设备的路径
        /// </summary>
        /// <param name="portType">设备端口类型包括 - HID_PORT、USBPRN_PORT、NET_PORT、NET_CPORT、NET_DPORT</param>
        /// <param name="pathStr">路径缓冲</param>
        /// <param name="size">缓冲大小，字符单位</param>
        /// <param name="VId">枚举相同的vID，0表示任意。</param>
        /// <param name="PId">同vID</param>
        /// <remarks>当枚举网络设备(NET_PORT、NET_CPORT、NET_DPORT)时,vID用于设置设备的UDP端口号。
        /// 输出 --
        ///pathStr : 路径字符，已多字符串的形式存放，字符串之间以0分割，以00结束
        ///sizes: 路径字符的总长度，字符单位，含结束00</remarks>
        /// <returns>0表示失败，非0表示枚举到的路径数</returns>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode,CallingConvention=CallingConvention.Cdecl)]
        public static extern uint dllFunc_EnumDevPath(ushort portType, [Out] char[] pathStr, ref uint size, int VId, int PId);

        /// <summary>
        /// 打开设备端口信息
        /// </summary>
        /// <param name="lpInfo">结构体内容</param>
        /// <returns>成功则返回端口句柄,否则返回INVLAID_PORT_HANDLE</returns>
        /// <remarks>最多可以同时打开10个设备端口，一个有效的句柄代表一个设备端口。
        /// 可以设置PORTINFO中portMode为控制端口PORTINFO_PMODE_CTRL、数据端口PORTINFO_PMODE_DATA或两者同时有效的方式打开设备。</remarks>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr dllFunc_OpenDev(ref structClassDll.LPPORTINFO lpInfo);

        /// <summary>
        /// 打开设备控制数据
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄。</param>
        /// <param name="ctrlCodeStr">设备提供的控制功能码。</param>
        /// <param name="inDats">命令码所携带的数据,当ctrlCodeStr为“CTRL_DEVCMD_ST”，inDtas[0]的应为设备命令码</param>
        /// <param name="inLen">携带的数据长度，字节单位</param>
        /// <param name="outDats">设备执行控制码时，设备的应答数据</param>
        /// <returns>成功则TRUE,否则FALSE</returns>
        /// <remarks>必须打开设备的控制端口。当ctrlCodeStr为“CTRL_DEVCMD_ST”，inDtas[0]的应为设备命令码。</remarks>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_DevIoCtrl(IntPtr pHandle, string ctrlCodeStr, [In] byte[] inDats, uint inLen, [In,Out] ref structClassDll.DEVACK_INFO outDats);

        /// <summary>
        /// 写数据到设备的数据端口
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <param name="inDats">需要写到设备的数据</param>
        /// <param name="inLen">数据长度，字节单位</param>
        /// <param name="outLen">实际写到设备的数据长度</param>
        /// <param name="lpOvlpd">同步则直接IntPtr.Zero,异步则调用结构体，并获取当前地址所对应的值</param>
        /// <returns>成功则TRUE,否则FALSE</returns>
        /// <remarks>必须打开设备的数据端口</remarks>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_Write(IntPtr pHandle, IntPtr inDats, uint inLen, out uint outLen, IntPtr overlapped);

        /// <summary>
        /// 写数据到设备的数据端口，已压缩过的数据
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <param name="inDats">需要写到设备的数据</param>
        /// <param name="inLen">数据长度，字节单位</param>
        /// <param name="modeTag">1-完整性数据发送，2-位图数据,3-两者都有</param>
        /// <param name="lpOut">输出的指针信息</param>
        /// <returns></returns>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_WriteEx(IntPtr pHandle, IntPtr inDats, uint inLen, uint modeTag, ref structClassDll.UNCMPR_INFO lpOut);
        /// <summary>
        /// 读数据到设备的数据端口
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <param name="inDats">需要接收设备数据的缓冲</param>
        /// <param name="inLen">缓冲长度，字节单位</param>
        /// <param name="outLen">实际读到设备的数据长度</param>
        /// <param name="lpOvlpd">windows的结构体，吴工已经编写好了，直接new对象就行了</param>
        /// <returns>v</returns>
        /// <remarks>必须打开设备的数据端口</remarks>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_Read(IntPtr pHandle, byte[] inDats, uint inLen, [Out] uint outLen, ref structClassDll.OVERLAPPED lpOvlpd);

      

        /// <summary>
        /// 关闭设备端口
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <returns>成功则TRUE,否则FALSE；</returns>
        /// <remarks>设备的数据/控制端口都将关闭，并释放设备句柄，如果有设备打开将重新被使用</remarks>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_CloseDev(IntPtr pHandle);

        /// <summary>
        /// 获取端口的系统设备句柄
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <param name="pType">端口类型，PORTINFO_PMODE_DATA表示数据端口，PORTINFO_PMODE_CTRL表示控制端口</param>
        /// <returns>INVALID_HANDLE_VALUE表示FALSE</returns>
        /// <remarks></remarks>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern int dllFunc_GetDevHandle(IntPtr pHandle, ushort pType);

        /// <summary>
        /// 启用设备日志
        /// </summary>
        /// <param name="filePath">日志文件路径</param>
        /// <returns>成功则TRUE,否则FALSE</returns>
        /// <remarks>相同的日志路径，打开时将被覆盖</remarks>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_OpenLog(string filePath);

        /// <summary>
        /// 输出字符信息到设备日志
        /// </summary>
        /// <param name="str">字符信息</param>
        /// <param name="newLine">TRUE表示添加换行到字符结尾</param>
        /// <param name="timeMode">添加时间到字符的开头</param>
        /// <param name="saveEn">表示将缓存的信息保存到日志文件中</param>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern void dllFunc_SetLogRecord(string str, bool newLine, bool timeMode, bool saveEn);

        /// <summary>
        /// 关闭设备日志
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern void dllFunc_CloseLog(IntPtr pHandle);

        /// <summary>
        /// 获取日志中的字符信息
        /// </summary>
        /// <param name="cnts">输入--输出 --字符信息长度，字符数</param>
        /// <param name="reset">TRUE表示清除记录缓存</param>
        /// <returns>字符信息</returns>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern string dllFunc_GetLogRecord(ref uint cnts, bool reset);

        /// <summary>
        /// 启动固件更新
        /// </summary>
        /// <param name="phandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <param name="filePath">固件的文件路径</param>
        /// <param name="hWnd">接收固件更新消息的窗口句柄</param>
        /// <returns>成功则TRUE,否则FALSE</returns>
        /// <remarks>必须打开设备的控制端口</remarks>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_OpenDfu(IntPtr pHandle, string filePath, IntPtr hWnd);

        /// <summary>
        /// 关闭固件更新
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <returns>成功则TRUE,否则FALSE</returns>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_CloseDfu(IntPtr pHandle);

        /// <summary>
        /// 获取固件信息
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <param name="fwInfo">固件信息</param>
        /// <param name="getMode">TRUE表示从固件中获取，FALSE表示设置</param>
        /// <returns>成功则TRUE,否则FALSE</returns>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_fwInfo(IntPtr pHandle, [In, Out] ref structClassDll.DFU_FWINFO fwInfo, bool getMode);

        /// <summary>
        /// 开始固件更新
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <param name="tags">标识</param>
        /// <param name="fwInfo">固件信息</param>
        /// <param name="getMode">TRUE表示从固件中获取，FALSE表示设置</param>
        /// <returns>成功则TRUE,否则FALSE</returns>
        /// <remarks>tags可按或(or)设置，包括 DFU_TAG_RESTART 表示更新后需启动设备</remarks>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_DFUStart(IntPtr pHandle, uint tags, [In, Out] ref structClassDll.DFU_FWINFO fwInfo, bool getMode);

        /// <summary>
        /// 加载设备配置的格式信息
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <param name="xmlPath">格式信息的文件路径 </param>
        /// <returns>成功则TRUE,否则FALSE</returns>
        /// <remarks>格式信息以XML的格式表述，如果设备pHandle有效，则从设备中加载，此时设备应处于连接状态。否则从xmlPath中加载</remarks>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_LoadDevCfg(IntPtr pHandle, string xmlPath);

        /// <summary>
        /// 获取设备配置数据
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <param name="lpParam">配置项</param>
        /// <param name="buf">数据缓冲</param>
        /// <param name="cnts">缓冲长度，字符数</param>
        /// <param name="tag">为DEVCFG_FMT_INFO表示获取格式信息，DEVCFG_VAL_INFO表示获取配置数据</param>
        /// <param name="noLoad">0表示从设备中加载所有配置数据</param>
        /// <returns>成功则TRUE,否则FALSE</returns>
        /// <remarks>当tag等于DEVCFG_FMT_INFO时，lpParam为指向(WORD*)的配置项索引号，tag等于DEVCFG_VAL_INFO时
        /// lpParam为指向字符串(LPCTSTR)的配置项名。
        /// 返回FALSE时，如果cnts也为0表示超出配置项的范围获取失败，cnts不为0表示需要缓冲的长度。</remarks>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_GetDevCfgInfo(IntPtr pHandle,object lpParam,out string buf,[In,Out] ref ushort cnts, ushort tag,ushort noLoad);

        /// <summary>
        /// 设置设备配置数据
        /// </summary>
        /// <param name="pHandle">由dllFunc_OpenDev返回的设备端口句柄</param>
        /// <param name="name">配置项名称</param>
        /// <param name="val">数据值，以字符串形式表示</param>
        /// <param name="cnts">无效</param>
        /// <param name="saveToDev">将配置数据发送到设备</param>
        /// <returns>成功则TRUE,否则FALSE</returns>
        [DllImport("WDevObj.dll", CharSet = CharSet.Unicode)]
        public static extern bool dllFunc_SetDevCfgInfo(IntPtr pHandle, string name, string val, ushort cnts, ushort saveToDev);

        /// <summary>
        /// 将用户数据解析成设备请求数据,或设备数据解析成用户数据
        /// </summary>
        /// <param name="inDat">用户数据</param>
        /// <param name="outDats">解析数据缓冲</param>
        /// <param name="modeTag">解析标识</param>
        /// <returns>modeTag为
        /// 1、RAWMODE_DEV_REQ时,inDats应为指向DEVREQ_INFO的数据结构，outDats应为接收数据缓冲,outLen为数据长度。
        /// 2、RAWMODE_DEV_RESP时,inDats为数据，inLen为数据长度，outDats应为DEVACK_INFO数据结构。</returns>
        [DllImport("WDevObj.dll", EntryPoint = "dllFunc_devRawDats", CharSet = CharSet.Unicode)]
        public static extern bool devRawDatsREQ(ref structClassDll.DEVREQ_INFO2 inDat, ref byte[] outDats, uint modeTag);

        [DllImport("WDevObj.dll", EntryPoint = "dllFunc_devRawDats", CharSet = CharSet.Unicode)]
        public static extern bool devRawDatsACK(ref byte[] inDat,ref structClassDll.DEVACK_INFO2 outDats,uint modeTag);
    }
}
