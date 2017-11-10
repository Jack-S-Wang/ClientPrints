using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using ClientPrsints.ObjectsAll.ClientPrints.Objects.DevDll;

namespace ClientPrints.MethodList.ClientPrints.Method.WDevDll
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
        [StructLayout(LayoutKind.Sequential,CharSet=CharSet.Unicode)]
        public struct DEVACK_INFO
        {
            /// <summary>
            /// 设备返回的数据
            /// </summary>
            public IntPtr lpBuf;		//设备返回的数据
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
        public class OVERLAPPED:IDisposable
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
                    dfuID=new byte[5],
                    ver=new byte[4],
                    fwDesc=new byte[WDevCmdObjects.DFU_FWTAG_LEN + 1]
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
            public IntPtr lpDat;		//设备请求数据
            /// <summary>
            /// 数据长度
            /// </summary>
            public uint datLen;			//数据长度
            /// <summary>
            /// 
            /// </summary>
            public uint datIdx;
            /// <summary>
            /// 设备数据包,设置长度512
            /// </summary>
            public byte[] devPktBuf;
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
            public IntPtr lpBuf;
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
            public byte[] devPktBuf;
            /// <summary>
            /// 设备返回的长度
            /// </summary>
            public ushort pktDatLen;
            /// <summary>
            /// 应答码,0表示成功
            /// </summary>
            public byte ackCode;
        }
    }
}
