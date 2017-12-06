using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace ClientPrsintsMethodList.ClientPrints.Method.WDevDll

{
    //是吴工写的方法构造体
     /// <summary>
    /// 表示设备类型的枚举。
    /// </summary>
    public enum DBT_DEVTYPE : uint
    {
        /// <summary>
        /// OEM设备。
        /// </summary>
        DBT_DEVTYP_OEM = 0x0,

        /// <summary>
        /// 设备节点号。
        /// </summary>
        DBT_DEVTYP_DEVNODE = 0x1,

        /// <summary>
        /// 逻辑卷。
        /// </summary>
        DBT_DEVTYP_VOLUME = 0x2,

        /// <summary>
        /// 端口设备，包括拨号猫、串口和并口。
        /// </summary>
        DBT_DEVTYP_PORT = 0x3,

        /// <summary>
        /// 网卡。
        /// </summary>
        DBT_DEVTYP_NET = 0x4,

        /// <summary>
        /// 指定的设备接口类型。
        /// </summary>
        DBT_DEVTYP_DEVICEINTERFACE = 0x5,

        /// <summary>
        /// 文件句柄。
        /// </summary>
        DBT_DEVTYP_HANDLE = 0x6
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DEV_BROADCAST_HDR
    {
        public uint size;
        public DBT_DEVTYPE type;
        public uint reserved;
    }

    public struct DEV_BROADCAST_DEVICEINTERFACE
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct MARSHAL_AIDER
        {
            public int size;
            public DBT_DEVTYPE type;
            public int reserved;
            public Guid interfaceGuid;
            public char devicePathFirstChar;
        }

        public DBT_DEVTYPE type;
        public int reserved;
        public Guid interfaceGuid;
        public string devicePath;

        public IntPtr allocHGlobal()
        {
            int size = Marshal.SizeOf(typeof(MARSHAL_AIDER));
            size += devicePath.Length * Marshal.SizeOf(typeof(short));

            var address = Marshal.AllocHGlobal(size);

            var aider = new MARSHAL_AIDER
            {
                size = size,
                type = DBT_DEVTYPE.DBT_DEVTYP_DEVICEINTERFACE,
                reserved = 0,
                interfaceGuid = interfaceGuid,
                devicePathFirstChar = '\0'
            };

            Marshal.StructureToPtr(aider, address, false);

            var devicePathBase = IntPtr.Add(address,
                Marshal.OffsetOf(
                    typeof(MARSHAL_AIDER),
                    "devicePathFirstChar"
                ).ToInt32()
            );

            var chars = (devicePath + "\0").ToCharArray();
            Marshal.Copy(chars, 0, devicePathBase, chars.Length);

            return address;
        }

        public void fromIntPtr(IntPtr address)
        {
            var aider = (MARSHAL_AIDER)Marshal.PtrToStructure(address, typeof(MARSHAL_AIDER));
            type = aider.type;
            reserved = aider.reserved;
            interfaceGuid = aider.interfaceGuid;

            var devicePathOffset = Marshal.OffsetOf(
                typeof(MARSHAL_AIDER),
                "devicePathFirstChar").ToInt32();
            var devicePathBase = IntPtr.Add(address, devicePathOffset);
            var devicePathChars = new char[(aider.size - devicePathOffset) / Marshal.SizeOf(typeof(short))];
            Marshal.Copy(devicePathBase, devicePathChars, 0, devicePathChars.Length);
            int realLength = 0;
            while (realLength < devicePathChars.Length &&
                devicePathChars[realLength] != '\0')
                ++realLength;
            devicePath = new string(devicePathChars, 0, realLength);
        }

        private static IntPtr getFieldAddress(IntPtr basePtr, string fieldName)
        {
            return IntPtr.Add(basePtr,
                Marshal.OffsetOf(typeof(MARSHAL_AIDER), fieldName).ToInt32());
        }

    }



    public class DeviceNotifications
    {
        public readonly static Guid GUID_DEVINTERFACE_USBPRINT = new Guid("{28d78fad-5a12-11d1-ae5b-0000f803a8c2}");


        [Flags]
        public enum RDN_FLAGS : uint
        {
            DEVICE_NOTIFY_WINDOW_HANDLE = 0x0,
            DEVICE_NOTIFY_ALL_INTERFACE_CLASSES = 0x4
        }

        /// <summary>
        /// 注册RegisterDeviceNotification
        /// </summary>
        /// <param name="handle">窗口或服务的句柄</param>
        /// <param name="filter">要注册的通知类型</param>
        /// <param name="flags"></param>
        /// <returns>注册句柄</returns>
        [DllImport("User32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
        public static extern IntPtr RegisterDeviceNotification(
            IntPtr handle,
            IntPtr filter,
            RDN_FLAGS flags);

        [DllImport("User32.dll", SetLastError=true)]
        public static extern bool UnregisterDeviceNotification(IntPtr registration);

    }
}
