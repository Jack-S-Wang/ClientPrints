using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using ClientPrintsMethodList.ClientPrints.Method.WDevDll;

namespace ClientPrintsMethodList.ClientPrints.Method.WDevDll
{
    public class structClassDll
    {
        /// <summary>
        /// 打开设备的结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct LPPORTINFO
        {
            /// <summary>
            /// 设备获取的地址信息
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string path;
            //public char* path;
            /// <summary>
            /// 读超时
            /// </summary>
            public int readTimeout;
            /// <summary>
            /// 写超时
            /// </summary>
            public int writeTimeout;
            /// <summary>
            /// 用于指定path代表的设备类型
            /// </summary>
            public ushort portType;
            /// <summary>
            /// 指定需要打开设备的端口，包括数据口和控制口。
            /// </summary>
            public ushort portMode;
        }
        /// <summary>
        /// 设备端口信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DEVACK_INFO
        {
            /// <summary>
            /// 设备返回的数据，必须先设置非托管内存大小
            /// </summary>
            public IntPtr lpBuf;		//设备返回的数据,
            /// <summary>
            /// 缓冲大小
            /// </summary>
            public ushort bufLen;		//缓冲大小
            /// <summary>
            /// 数据长度
            /// </summary>
            public ushort datLen;		//数据长度
            /// <summary>
            /// 应答码,0表示成功
            /// </summary>
            public byte ackCode;			//应答码,0表示成功
        }

        /// <summary>
        /// windows自带的构造体，吴工已经编写了，因此使用时直接new对象就行
        /// </summary>
        public class OVERLAPPED : IDisposable
        {
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            private struct _OVERLAPPED
            {
                public IntPtr Internal;
                public IntPtr InternalHigh;
                public int Offset;
                public int OffsetHigh;
                public IntPtr Event;
            }

            private static readonly int StructureSize;
            private static readonly int OffsetInternal;
            private static readonly int OffsetInternalHigh;
            private static readonly int OffsetOffset;
            private static readonly int OffsetOffsetHigh;
            private static readonly int OffsetEvent;

            static OVERLAPPED()
            {
                var t = typeof(_OVERLAPPED);
                StructureSize = Marshal.SizeOf(t);
                OffsetInternal = Marshal.OffsetOf(t, "Internal").ToInt32();
                OffsetInternalHigh = Marshal.OffsetOf(t, "InternalHigh").ToInt32();
                OffsetOffset = Marshal.OffsetOf(t, "Offset").ToInt32();
                OffsetOffsetHigh = Marshal.OffsetOf(t, "OffsetHigh").ToInt32();
                OffsetEvent = Marshal.OffsetOf(t, "Event").ToInt32();
            }

            public OVERLAPPED()
            {
                buffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(_OVERLAPPED)));
                Internal = IntPtr.Zero;
                InternalHigh = IntPtr.Zero;
                Offset = 0;
                OffsetHigh = 0;
                Event = null;
            }

            public IntPtr Internal
            {
                get
                {
                    return Marshal.ReadIntPtr(buffer, OffsetInternal);
                }

                set
                {
                    Marshal.WriteIntPtr(buffer, OffsetInternal, value);
                }
            }

            public IntPtr InternalHigh
            {
                get
                {
                    return Marshal.ReadIntPtr(buffer, OffsetInternalHigh);
                }

                set
                {
                    Marshal.WriteIntPtr(buffer, OffsetInternalHigh, value);
                }
            }

            public uint Offset
            {
                get
                {
                    return (uint)Marshal.ReadInt32(buffer, OffsetOffset);
                }

                set
                {
                    Marshal.WriteInt32(buffer, OffsetOffset, (int)value);
                }
            }

            public uint OffsetHigh
            {
                get
                {
                    return (uint)Marshal.ReadInt32(buffer, OffsetOffsetHigh);
                }

                set
                {
                    Marshal.WriteInt32(buffer, OffsetOffsetHigh, (int)value);
                }
            }

            private ManualResetEvent _Event;
            public ManualResetEvent Event
            {
                get { return _Event; }

                set
                {
                    _Event = value;
                    IntPtr eventHandle = _Event == null ?
                        IntPtr.Zero :
                        _Event.SafeWaitHandle.DangerousGetHandle();
                    Marshal.WriteIntPtr(buffer, OffsetEvent, eventHandle);
                }
            }

            public IntPtr AddressOf()
            {
                return buffer;
            }

            private IntPtr buffer;
            #region IDisposable Support
            private bool disposedValue = false; // 要检测冗余调用

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                    }

                    Marshal.FreeHGlobal(buffer);
                    buffer = IntPtr.Zero;

                    disposedValue = true;
                }
            }

            ~OVERLAPPED()
            {
                // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
                Dispose(false);
            }

            // 添加此代码以正确实现可处置模式。
            void IDisposable.Dispose()
            {
                // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            #endregion
        }

        /// <summary>
        /// 固件信息结构体，值已经赋值，直接new对象就行
        /// </summary>
        public class DFU_FWINFO
        {
            [StructLayout(LayoutKind.Sequential)]
            private struct _DFU_FWINFO
            {
                public byte[] dfuID;//长度5
                public byte[] ver;//长度为4
                public byte[] fwDesc;//长度为WDevCmdObjects.DFU_FWTAG_LEN + 1
            }

            public DFU_FWINFO()
            {
                new _DFU_FWINFO()
                {
                    dfuID = new byte[5],
                    ver = new byte[4],
                    fwDesc = new byte[WDevCmdObjects.DFU_FWTAG_LEN + 1]
                };
            }
        }

        /// <summary>
        /// 解析成设备请求时结构体，当命令符为CTRL_DEVCMD_ST时,表示设备命令,lpDat[0]则为命令码0x80 ~ 0x9f，devPktBuf数组设置长度为512
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DEVREQ_INFO2
        {
            /// <summary>
            /// 命令符
            /// </summary>
            public string cmdCodeStr;//命令符，
            /// <summary>
            /// 当命令符为CTRL_DEVCMD_ST时,表示设备命令,lpDat[0]则为命令码0x80 ~ 0x9f
            /// </summary>
            public byte[] lpDat;		//设备请求数据
            /// <summary>
            /// 数据长度
            /// </summary>
            public uint datLen;			//数据长度
            /// <summary>
            /// 返回的数据长度
            /// </summary>
            public uint datIdx;
            /// <summary>
            /// 设备数据包,设置长度512
            /// </summary>
            public IntPtr devPktBuf;
            /// <summary>
            /// 设备数据包长度
            /// </summary>
            public ushort pktDatLen;
        }


        /// <summary>
        /// 解析设备响应数据时的结构体,devPktBuf数组长度这只为512
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DEVACK_INFO2
        {
            /// <summary>
            /// 用户数据缓冲
            /// </summary>
            public byte[] lpBuf;
            /// <summary>
            /// 缓冲大小
            /// </summary>
            public uint bufLen;
            /// <summary>
            /// 数据长度
            /// </summary>
            public uint datIdx;
            /// <summary>
            /// 设备返回 长度为512
            /// </summary>
            public IntPtr devPktBuf;
            /// <summary>
            /// 设备返回的长度
            /// </summary>
            public ushort pktDatLen;
            /// <summary>
            /// 应答码,0表示成功
            /// </summary>
            public byte ackCode;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct UNCMPR_INFO
        {
            /// <summary>
            /// 压缩数据长度
            /// </summary>
            public uint cmprLen;
            /// <summary>
            /// 解压后的数据长度
            /// </summary>
            public uint uncmprLen;
            /// <summary>
            /// 结果状态
            /// </summary>
            public uint resultTag;
            /// <summary>
            /// 状态
            /// </summary>                
            public uint stat;
            /// <summary>
            /// 工作号
            /// </summary>
            public ushort jobNumber;
            /// <summary>
            /// 帧号从零开始
            /// </summary>
            public ushort frmIdx;
            /// <summary>
            /// 压缩类型
            /// </summary>
            public byte cmprType;
            /// <summary>
            /// 调试输出文件的文件名。
            /// </summary>
            public string userParm;
        }

        /// <summary>
        /// 数据发送中需要的全部设备参数信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DEV_BMP
        {
            /// <summary>
            /// 
            /// </summary>
            public ushort ID;     //
            /// <summary>
            /// 打印设备最大dpi
            /// </summary>
            public ushort dpi;    //
            /// <summary>
            /// 左上角坐标X
            /// </summary>
            public ushort posX;   //
            /// <summary>
            /// 左上角坐标Y
            /// </summary>
            public ushort posY;

            /// <summary>
            /// 位图宽
            /// </summary>
            public ushort pixelW;
            /// <summary>
            /// 前景位图高
            /// </summary>
            public ushort txPixelH;   //
            /// <summary>
            /// 背景位图高
            /// </summary>
            public ushort bkPixelH;   //
            /// <summary>
            /// 像素颜色深度(bits)
            /// </summary>
            public byte bpps;        //
            /// <summary>
            /// 设备位图类型，1：光栅位图
            /// </summary>
            public byte bmpType; //设备位图类型，1：光栅位图
            /// <summary>
            /// 4
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] ret;//4
            /// <summary>
            /// 设备参数传入
            /// </summary>
            public DEVPROP_INFO devInfo;
            /// <summary>
            /// 前景和背景位图数据,位图数据要拼凑进去
            /// </summary>
            //public IntPtr BmpDats;
        }

        /// <summary>
        /// 设备参数传入
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DEVPROP_INFO
        {
            public ushort size;
            /// <summary>
            /// 2
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] revs;
            //public byte[] Devprop;
            public DEVPROP_DL210 devprop;
        }


        /// <summary>
        /// 指DC-1300的结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DEVPROP_D300
        {
            /// <summary>
            /// 结构体的大小
            /// </summary>
            public byte propSize;    //
            /// <summary>
            /// BMP_DEVPROP_PRN 0x81
            /// </summary>
            public byte devType; //
            /// <summary>
            /// BMP_DEVPROP_BKNONE 0
            /// </summary>
            public byte bkBmpID; //
            /// <summary>
            /// 卡类型 -- 0:mblack, 1:mblue,2:rblack
            /// </summary>
            public byte cardType;    // 
            /// <summary>
            /// 进卡方式 -- 0:AutoFeed, 1:manual Feed , 2:Auto Select
            /// </summary>
            public byte cardInputMode;// 
            /// <summary>
            /// 出卡方式 -- 0:装卡盒接收,1:手动接收,2:后退卡,3:不退卡
            /// </summary>
            public byte cardOutputMode;// 
            /// <summary>
            /// 打印温度 -- 0 -20
            /// </summary>
            public byte printTemp;   // 
            /// <summary>
            /// 打印对比度 -- 0 -20
            /// </summary>
            public byte printContrast;// 
            /// <summary>
            /// 打印速度 -- 0-20
            /// </summary>
            public byte printSpeed;  // 
            /// <summary>
            /// 灰度温度 -- 0-20
            /// </summary>
            public byte grayTemp;    // 
            /// <summary>
            /// 擦除速度 -- 0-20
            /// </summary>
            public byte wipeSpeed;   // 
            /// <summary>
            /// 设置擦除温度 --0-20
            /// </summary>
            public byte eraseTemp;   // 
            /// <summary>
            /// 打印模式 0:Card Print, 1:Card Erase
            /// </summary>
            public byte printMode;   // 
            /// <summary>
            /// 3
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] revs;//3
        }
        /// <summary>
        /// 指DL210
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DEVPROP_DL210
        {   //基类 ： DSEMUL_DEVINFO_ITEM
            /// <summary>
            /// 结构体的大小
            /// </summary>
            public Byte propSize;
            /// <summary>
            /// BMP_DEVPROP_PRN
            /// </summary>
            public Byte propType;
            /// <summary>
            /// Page Length(dot)	Page_Length
            /// </summary>
            public ushort PageLength;
            /// <summary>
            /// Continuesly paper page length.	(^LL)
            /// </summary>
            public ushort ContPaperLength;
            /// <summary>
            /// Max label length (^ML)
            /// </summary>
            public ushort MaxMediaLength;
            /// <summary>
            /// Vertical position adjustment	(^LT)
            /// </summary>
            public short VerticalPosition;
            /// <summary>
            /// Tear off position adjustment	(^TA)
            /// </summary>
            public short TearOffAdjustPosition;
            /// <summary>
            /// 0:Transfer thermal(TT) 1:Direct thermal(DT)
            /// </summary>
            public Byte PrintMethod;
            /// <summary>
            /// Print paper type	0:Label sheet 1:Continuous paper
            /// </summary>
            public Byte PrintPaperType;
            /// <summary>
            /// GAP Length
            /// </summary>
            public Byte Gap_Length;
            /// <summary>
            /// Print speed.	(Prn_SpeedValue)
            /// </summary>
            public Byte PrintSpeed;
            /// <summary>
            /// ZPL Darkness (^MD, ^SD)
            /// </summary>
            public Byte ZPL_PrintDarkness;
            /// <summary>
            /// (Cutter)
            /// </summary>
            public Byte CutterOption;
            /// <summary>
            /// Peel option.	(PrnPeelOption)
            /// </summary>
            public Byte PeelOption;
            public Byte resv;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NODEITEM_VAL
        {
            /// <summary>
            /// NODE_VAL_NONE...NODE_VAL_MULTISTR
            /// </summary>
            public byte type;
            /// <summary>
            /// 3个字节
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] resv;
            //public byte resv0;
            //public byte resv1;
            //public byte resv2;
            public ushort tag;
            public ushort datLen;
            public UNION union;
            public int val;
        }
        [StructLayout(LayoutKind.Explicit,Size =4)]
        public struct UNION
        {
            [FieldOffset(0)]
            /// <summary>
            /// 起始位置和长度需字节对齐,并以0结尾.
            /// </summary>
            public  IntPtr lpStr;

            [FieldOffset(0)]
            /// <summary>
            ///  起始位置和长度需字节对齐
            /// </summary>
            public  IntPtr lpDats;
        }

        [StructLayout(LayoutKind.Sequential,CharSet =CharSet.Unicode)]
        public struct DEVJSON_INFO
        {
            public uint jsEntry;
            public IntPtr info; //info节点的匹配信息
            public ushort infoLen;
            public ushort tag;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct JSVAL_INFO
        {
            public uint jsEntry;  //
            public string keyPath;
            public uint tag;
        };
    }
}
