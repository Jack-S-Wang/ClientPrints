using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    public partial class LoginPassword : Form
    {
        public LoginPassword()
        {
            InitializeComponent();
        }
        PrintersGeneralFunction pGf = new PrintersGeneralFunction();
        public ClientMianWindows client;
        IntPtr pHandle = new IntPtr(-1);
        private void LoginPassword_Load(object sender, EventArgs e)
        {
            try
            {
                getDev();
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }
        private void getDev()
        {
            PrinterUSBMethod pu = new PrinterUSBMethod();
            string[] path = pu.EnumPath();
            foreach (string pathAddress in path)
            {
                if (SharMethod.passwordError.Contains(pathAddress))
                {
                    
                    structClassDll.LPPORTINFO lpInfo = new structClassDll.LPPORTINFO()
                    {
                        path = pathAddress,
                        readTimeout = 0,
                        writeTimeout = 0,
                        portType = (ushort)WDevCmdObjects.USBPRN_PORT,
                        portMode = (ushort)WDevCmdObjects.PORTINFO_PMODE_ALL
                    };
                    //用指针模式可以执行成功
                    //unsafe{
                    //    fixed (char* pathd = path)
                    //    {
                    //        lpInfo.path = pathd;
                    //    }
                    //}
                    do
                    {
                        Thread.Sleep(200);
                        pHandle = WDevDllMethod.dllFunc_OpenDev(ref lpInfo);
                    }
                    while (pHandle == new IntPtr(-1));
                    byte[] data = new byte[0];
                    structClassDll.DEVACK_INFO outDats = new structClassDll.DEVACK_INFO()
                    {
                        lpBuf = Marshal.AllocHGlobal(512),
                        datLen = 0,
                        bufLen = 512,
                        ackCode = 0
                    };

                    //设备型号
                    byte[] redata = new byte[0];           
                    string model = pGf.reInformation(WDevCmdObjects.DEV_GET_MODEL, pHandle, ref redata );
                    //序列号
                    redata = new byte[0];
                    string sn = pGf.reInformation(WDevCmdObjects.DEV_GET_DEVNO, pHandle,ref redata);
                    redata = new byte[] { 0x00, 0x00 };
                    string strCode = pGf.reInformation(WDevCmdObjects.DEV_GET_USERDAT, pHandle, ref redata);
                    ListViewItem item = new ListViewItem();
                    if (strCode.Contains("false") || strCode == "")
                    {
                        strCode = pathAddress;
                    }
                    item.SubItems[0].Text = strCode;
                    item.SubItems.Add(pathAddress);
                    item.SubItems.Add(model);
                    item.SubItems.Add(sn);
                    listView1.Items.Add(item);
                }
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                new addCommend(SharMethod.user, listView1.Name, "双击选择设备");
                if (listView1.SelectedItems.Count > 0)
                {
                    WDevDllMethod.dllFunc_CloseDev(pHandle);
                    importPassWord imp = new importPassWord();
                    imp.Owner = this;
                    imp.client = client;
                    imp.pathAddress = listView1.SelectedItems[0].SubItems[1].Text;
                    imp.StartPosition = FormStartPosition.CenterParent;
                    imp.ShowDialog();
                    this.listView1.Clear();
                    getDev();
                }
            }catch(Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }
    }
}
