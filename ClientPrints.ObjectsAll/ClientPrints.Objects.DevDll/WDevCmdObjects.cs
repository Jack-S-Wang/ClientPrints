using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrsints.ObjectsAll.ClientPrints.Objects.DevDll
{
    public class WDevCmdObjects
    {
        #region...//通许协议对照字符串码
      
            //---通用协议------------
        /// <summary>
        /// 获取设备型号 1
        /// </summary>
        public const string DEV_GET_MODEL = "CTRL_CMDGET_MODEL";
        /// <summary>
        /// 获取设备序列号 2
        /// </summary>
        public const string DEV_GET_DEVNO = "CTRL_CMDGET_DEVNO";	//
        /// <summary>
        /// 获取得实仿真规范版本号 3
        /// </summary>
        public const string DEV_GET_PROTVER = "CTRL_CMDGET_PROTVER";	//
        /// <summary>
        /// 设备连接 4
        /// </summary>
        public const string DEV_CMD_CONNT = "CTRL_CMD_CONNT";		//  
        /// <summary>
        /// 设备断开 5
        /// </summary>
        public const string DEV_CMD_UNCONNT = "CTRL_CMD_UNCONNT";		//
        /// <summary>
        ///设置密码 6
        /// </summary>
        public const string DEV_SET_PSW = "CTRL_CMDSET_PSW";		//
        /// <summary>
        /// 获取自定义标识 07
        /// </summary>
        public const string DEV_GET_USERDAT = "CTRL_CMDGET_USERDAT";   //
        /// <summary>
        /// 设置自定义标识 08 现在不能实现
        /// </summary>
        public const string DEV_SET_USERDAT = "CTRL_CMDSET_USERDAT";  //
        /// <summary>
        /// 获取设备状态 09
        /// </summary>
        public const string DEV_GET_DEVSTAT = "CTRL_CMDGET_DEVSTAT";   //
        /// <summary>
        /// 获取加密状态 0A
        /// </summary>
        public const string DEV_GET_PWSSTAT = "CTRL_CMDGET_PWSSTAT";   //
        /// <summary>
        /// 设置加密 0B
        /// </summary>
        public const string DEV_SET_ENCRYPT = "CTRL_CMDSET_ENCRYPT";   //
        /// <summary>
        /// 获取设备信息 0C
        /// </summary>
        public const string DEV_GET_DEVINFO = "CTRL_CMDGET_DEVINFO";   //
        /// <summary>
        /// 恢复出厂设置 0D
        /// </summary>
        public const string DEV_CMD_RESETCFG = "CTRL_CMD_RESETCFG";		//
        /// <summary>
        /// 清除缓存 0E
        /// </summary>
        public const string DEV_CMD_CLSBUF = "CTRL_CMD_CLSBUF";		//
        /// <summary>
        /// 获取数据统计 0F
        /// </summary>
        public const string DEV_GET_STATISINFO = "CTRL_CMDGET_STATISINFO";	//
        /// <summary>
        /// 获取设备维修信息 10  现在不能实现
        /// </summary>
        public const string DEV_GET_MAINTAININFO = "CTRL_CMDGET_MAINTAININFO";  //     
        /// <summary>
        /// 设置设备重启 11
        /// </summary>
        public const string DEV_CMD_RESTART = "CTRL_CMD_DEVRESTART";	//
        /// <summary>
        /// 设备自检 12
        /// </summary>
        public const string DEV_CMD_CHKSLF = "CTRL_CMD_DEVCHKSLF";    //
        /// <summary>
        /// 获取工作模式 13
        /// </summary>
        public const string DEV_GET_WORKMODE = "CTRL_CMDGET_WORKMODE";  //
        /// <summary>
        /// 设置工作模式 14
        /// </summary>
        public const string DEV_SET_WORKMODE = "CTRL_CMDSET_WORKMODE";  //
        /// <summary>
        /// 获取固件版本号 15
        /// </summary>
        public const string DEV_GET_VERINFO = "CTRL_CMDGET_VERINFO";   //
        /// <summary>
        /// 获取设备配置信息 17
        /// </summary>
        public const string DEV_GET_CFGINFOS = "CTRL_CMDGET_CFGINFOS";  // 
        /// <summary>
        /// 设备配置 18 现在不能实现
        /// </summary>
        public const string DEV_SET_CFGINFOS = "CTRL_CMDSET_CFGINFOS";	//
        /// <summary>
        /// 获取配置格式 1A
        /// </summary>
        public const string DEV_GET_CFGFMT = "CTRL_CMDGET_CFGFMT";	//
        /// <summary>
        /// 设置设备系统参数 1B 现在不能实现，指令是1B却写成了1C，转换了
        /// </summary>
        public const string DEV_SET_SYSPARAM = "CTRL_CMDSET_SYSPARAM";	//
        /// <summary>
        /// 获取设备系统参数 1C 现在不能实现
        /// </summary>
        public const string DEV_GET_SYSPARAM = "CTRL_CMDGET_SYSPARAM";	// 
        /// <summary>
        /// 设备控制操作 1D 现在不能实现
        /// </summary>
        public const string DEV_SET_OPER = "CTRL_CMDSET_OPER";		//

        //--设备协议------------
        /// <summary>
        /// 设备专用命令码 80-9f规范命令码
        /// </summary>
        public const string DEV_CMD_CUSTOMIZE = "CTRL_DEVCMD_ST";	//
    
        #endregion
        #region
        //---------------------------------
        public static int DSEMUL_PVER = 0x0100;	//协议版本号
        public static int DSEMUL_DVER = 0x0001; //设备版本号

        //----------------work mode--------
        public static int DSINFO_MODE_NORMAL = 0;
        public static int DSINFO_MODE_SAMPLE = 1;//数据采样
        public static int DSINFO_MODE_BOOT = 2;//boot
        public static int DSINFO_MODE_FACTORY = 3;//
        public static int DSINFO_MODE_UDF = 4; //fw版本更新

        //-----------MODEL TYPE--------------
        public static int DS_DEVTYPE_PRINTER = 0x01;
        public static int DS_DEVTYPE_HEALTH = 0x02;
        public static int DS_DEVTYPE_LED = 0x03;

        //-----------DEVNO TYPE--------------
        public static int DS_DATTYPE_STR = 0x01;
        public static int DS_DATTYPE_HEX = 0x02;


        //--设备应答码-----------
        public static int HCTRL_ACK_OK = 0x00;
        public static int HCTRL_ERR_UNSUPPORT = 0x01;	//不支持的指令
        public static int HCTRL_ERR_SUM = 0x02;	//校验和不匹配
        public static int HCTRL_ERR_UNCONNT = 0x03;	//设备尚未连接
        public static int HCTRL_ERR_PARAM = 0x04;	//参数错误
        public static int HCTRL_ERR_LEN = 0x05;	//数据长度错误
        public static int HCTRL_ERR_MATCH = 0x06;	//设备未连接相应模块
        public static int HCTRL_ERR_PASSWD = 0x07;	//密码错误
        public static int HCTRL_ERR_BUSY = 0x08;	//设备忙
        public static int HCTRL_ERR_MODE = 0x80;	//模式错误
        public static int HCTRL_ERR_CMDFAIL = 0x81;	//执行失败

        //---DFU_FWTAG_LEN结构体的值---
        public static int DFU_FWTAG_LEN = 32;

        //--配置数据 tag--
        public static int DEVCFG_FMT_INFO = 0x00;
        public static int DEVCFG_VAL_INFO = 0x01;
        //-portMode---------------
        public static int PORTINFO_PMODE_DATA = 0x01;
        public static int PORTINFO_PMODE_CTRL = 0x02;

        public static int PORTINFO_PMODE_ALL = 0x03;

        //----portType------------
        public static int INVALID_PORT = 0x0000;
        public static int UART_PORT = 0x0100;
        public static int LPT_PORT = 0x0200;

        public static int USB_PORT = 0x0300;
        /// <summary>
        /// HID与USB双端口
        /// </summary>
        public static int USBPRN_PORT = 0x0301;
        public static int HID_PORT = 0x0302;
        public static int NET_PORT = 0x0400;
        public static int NET_DPORT = 0x0401;
        public static int NET_CPORT = 0x0402;

        //typedef int PORT_HANDLE;
        public static int INVALID_PORT_HANDLE = -1;

        //---设备解析标识---
        public static int RAWMODE_DEV_REQ = 0x01;	//解析成设备请求
        public static int RAWMODE_DEV_RESP = 0x02;	//解析设备响应数据
        #endregion
    }
}
