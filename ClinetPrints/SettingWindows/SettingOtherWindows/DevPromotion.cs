using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrsintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    public partial class DevPromotion : Form
    {
        public DevPromotion()
        {
            InitializeComponent();
        }
        
        private void btn_getFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            DialogResult dr=of.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.txb_getFile.Text = of.FileName;
            }
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            if (this.txb_getFile.Text != "" && listView1.SelectedItems.Count>0)
            {
                if (WDevDllMethod.dllFunc_OpenDfu(new IntPtr(Int32.Parse(listView1.SelectedItems[0].SubItems[1].Text)), txb_getFile.Text, this.Handle))
                {
                    txb_commandText.AppendText("已加载固件文件！监控已关闭！");
                    uint tages = 0x01;
                    WDevDllMethod.dllFunc_DFUStart(new IntPtr(Int32.Parse(listView1.SelectedItems[0].SubItems[1].Text)), tages);
                    txb_commandText.AppendText("正在更新固件！");
                }
            }
        }
        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == WDevCmdObjects.MSG_WDFU_ERRCODE)
                {
                    // 动态库固件更新进度通知。

                    // TODO : 触发进度更新事件。
                    int w = m.WParam.ToInt32();
                    if (w >= WDevCmdObjects.MSG_WDFU_PROGRESS_BEGIN)
                    {
                        int progressPercent = w - WDevCmdObjects.MSG_WDFU_PROGRESS_BEGIN;
                        progressBar1.Value = progressPercent;
                        if (m.WParam.ToInt32() == WDevCmdObjects.MSG_WDFU_PROGRESS_END)
                        {
                            // TODO : 触发固件完成事件。
                            WDevDllMethod.dllFunc_CloseDfu(new IntPtr(Int32.Parse(listView1.SelectedItems[0].SubItems[1].Text)));
                            txb_commandText.AppendText("固件升级成功！将重新加载界面！");
                            this.listView1.Items.Clear();
                            getDev();
                        }
                    }

                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DevPromotion_Load(object sender, EventArgs e)
        {
            getDev();
        }

        private void getDev()
        {
            PrinterUSBMethod pu = new PrinterUSBMethod();
            string[] path = pu.EnumPath();
            foreach (string pathAddress in path)
            {
                IntPtr pHandle = new IntPtr(-1);
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

                //打开设备连接默认没有密码
                if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_CMD_CONNT, data, (uint)data.Length, ref outDats))
                {
                    data = new byte[] { 0x00, 0x00 };
                    string strCode = "";
                    WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_GET_USERDAT, data, (uint)data.Length, ref outDats);
                    if ((outDats.datLen > 0) || (outDats.datLen == 0 && outDats.ackCode == 0))
                    {
                        byte[] reData = new byte[1000];
                        Marshal.Copy(outDats.lpBuf, reData, 0, outDats.datLen);
                        if (outDats.datLen > 0)
                        {
                            string str = Encoding.GetEncoding("GBK").GetString(reData, 0, reData.Length).Replace('\0', ' ').TrimEnd();
                            for (int i = 0; i < str.Length; i++)//因为获取到的标识值时不干净的值，值后面的\0后面还有值
                            {
                                if (str[i] == ' ')
                                {
                                    break;
                                }
                                else
                                {
                                    strCode += str[i];
                                }
                            }
                        }
                        ListViewItem item = new ListViewItem();
                        if (strCode == "")
                        {
                            strCode = pathAddress;
                        }
                        item.SubItems[0].Text = strCode;
                        item.SubItems.Add(pHandle.ToString());
                        listView1.Items.Add(item);
                    }
                }

            }
        }

        private void txb_commandText_TextChanged(object sender, EventArgs e)
        {
            if (txb_commandText.TextLength >= 5000)
            {
                txb_commandText.Clear();
            }
        }
    }
}
