using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    public partial class otherControlSet : Form
    {
        public otherControlSet()
        {
            InitializeComponent();
        }
        public IntPtr pHandle = IntPtr.Zero;
        public string pathAddress = "";
        public PrinterObjects po;
        private void otherControlSet_Load(object sender, EventArgs e)
        {
            this.cmb_Control.Items.Add("进行连接");//0x4
            this.cmb_Control.Items.Add("断开连接");//0x5
            this.cmb_Control.Items.Add("设置密码");//0x6
            this.cmb_Control.Items.Add("获取用户自定义标识");//0x7
            this.cmb_Control.Items.Add("设置用户自定义标识");//0x8
            this.cmb_Control.Items.Add("获取加密状态");//0xa
            this.cmb_Control.Items.Add("设置连接加密");//0xb
            this.cmb_Control.Items.Add("恢复出厂设置");//0xd
            this.cmb_Control.Items.Add("系统复位");//0xe
            this.cmb_Control.Items.Add("获取数据统计");//0xf
            this.cmb_Control.Items.Add("获取设备维修信息");//0x10
            this.cmb_Control.Items.Add("设备检测");//0x12
            this.cmb_Control.Items.Add("获取设备工作模式");//0x13
            this.cmb_Control.Items.Add("设置设备工作模式");//0x14
            this.cmb_Control.Items.Add("获取固件信息");//0x15
            this.cmb_Control.Items.Add("数据传输");//0x19
            this.cmb_Control.Items.Add("获取设备配置格式信息");//0x1a
            this.cmb_Control.Items.Add("设置用户信息");//0x1e暂时无效
            this.cmb_Control.Items.Add("进入固件升级模式");//0x16孙总的dll无该指令
            this.cmb_Control.SelectedIndex = 0;
            ToolTip tool = new ToolTip();
            tool.SetToolTip(this.ckb_Hex, "选中表示输入的内容是16进制的数据");
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                new addCommend(SharMethod.user, btn_Send.Name, "发送指令");
                int index = this.cmb_Control.SelectedIndex;

                byte[] data = new byte[0];
                if (this.txb_data.Text != "")
                {
                    string s = this.txb_data.Text;
                    if (this.ckb_Hex.Checked)
                    {
                        if (s.Length % 2 != 0)
                        {
                            MessageBox.Show("16进制数据，一个字节有2个字符来表示");
                            return;
                        }
                        foreach (var c in s)
                        {
                            if ((c < 48 || c > 57) && (c < 65 || c > 70) && (c < 97 && c > 102))
                            {
                                MessageBox.Show("有不符合的字符存在!");
                                return;
                            }
                        }
                        data = new byte[s.Length / 2];
                        for (int i = 0; i < s.Length; i += 2)
                        {
                            data[i / 2] = Convert.ToByte(s[i] + "" + s[i + 1], 16);
                        }
                    }
                    else
                    {
                        data = Encoding.UTF8.GetBytes(s);
                    }
                }
                structClassDll.DEVACK_INFO outDats = new structClassDll.DEVACK_INFO()
                {
                    lpBuf = Marshal.AllocHGlobal(512),
                    datLen = 0,
                    bufLen = 512,
                    ackCode = 0
                };
                if (!po.isWifi)
                {
                    getDevInfo(index, data, ref outDats);
                }
                else
                {
                    getWifiInfo(index, data);
                }


            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        /// <summary>
        /// 本地设备指令返回结果
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        /// <param name="outDats"></param>
        private void getDevInfo(int index, byte[] data,ref structClassDll.DEVACK_INFO outDats)
        {
            string reStr = "";
            bool flge = false;
            switch (index)
            {
                case 0:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_CMD_CONNT, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 1:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_CMD_UNCONNT, data, (uint)data.Length, ref outDats))
                    {
                        MessageBox.Show("提示警告", "执行此结果将导致该设备无法正常获取，如果需要请重新执行连接指令！");
                        flge = true;
                    }
                    break;
                case 2:

                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_SET_PSW, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                        Item item = new Item()
                        {
                            pathAddress = pathAddress,
                            password = data,
                            checkHex = ckb_Hex.Checked
                        };
                        FileStream file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\printPassword.xml", FileMode.OpenOrCreate);
                        XmlSerializer xml = new XmlSerializer(new DevPassword().GetType());
                        if (file.Length != 0)
                        {
                            var result = xml.Deserialize(file) as DevPassword;
                            var rtm = result.find(pathAddress);
                            if (rtm != null)
                            {
                                rtm.password = data;
                                rtm.checkHex = ckb_Hex.Checked;
                            }
                            else
                            {
                                result.addItem(item);
                            }
                            file.SetLength(0);
                            file.Seek(0, 0);
                            xml.Serialize(file, result);
                        }
                        else
                        {
                            DevPassword dp = new DevPassword();
                            dp.addItem(item);
                            xml.Serialize(file, dp);
                        }
                        file.Flush();
                        file.Close();
                        file.Dispose();
                    }
                    break;
                case 3:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_GET_USERDAT, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 4:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_SET_USERDAT, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 5:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_GET_PWSSTAT, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 6:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_SET_ENCRYPT, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 7:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_CMD_RESETCFG, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 8:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_CMD_CLSBUF, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 9:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_GET_STATISINFO, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 10:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_GET_MAINTAININFO, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 11:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_CMD_CHKSLF, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 12:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_GET_WORKMODE, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 13:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_SET_WORKMODE, data, (uint)data.Length, ref outDats)) //??孙总demo不一样
                    {
                        flge = true;
                    }
                    break;
                case 14:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_GET_VERINFO, data, (uint)data.Length, ref outDats))
                    {
                        flge = true;
                    }
                    break;
                case 15:
                    //if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects, data, (uint)data.Length, ref outDats))
                    //{
                    //    flge = true;
                    //}
                    MessageBox.Show("无此命令执行");
                    break;
                case 16:
                    if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_GET_CFGFMT, data, (uint)data.Length, ref outDats))//??孙总demo不一样210不行，1300可以
                    {
                        flge = true;
                    }
                    break;
                case 17:
                    //if (WDevDllMethod.dllFunc_DevIoCtrl(pHandle, WDevCmdObjects.DEV_SET_PSW, data, (uint)data.Length, ref outDats))
                    //{
                    //    flge = true;
                    //}
                    MessageBox.Show("无此命令执行");
                    break;
                case 18:
                    MessageBox.Show("无此命令执行");
                    break;
            }

            if (flge)
            {
                if ((outDats.datLen > 0) || (outDats.datLen == 0 && outDats.ackCode == 0))
                {
                    byte[] reData = new byte[outDats.datLen];
                    Marshal.Copy(outDats.lpBuf, reData, 0, outDats.datLen);
                    if (outDats.datLen > 0)
                    {
                        for (int i = 0; i < outDats.datLen; i++)
                        {
                            reStr += reData[i];
                        }
                    }
                    else
                    {
                        reStr = "返回0个字节";
                    }
                }
                else
                {

                    reStr = "数据获取失败，错误码：" + outDats.ackCode;
                }
            }
            else
            {
                reStr = "执行方法失败！";
            }
            this.txb_redata.AppendText(reStr + "\r\n");
        }

        /// <summary>
        /// 得到wifi指令结果
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        private void getWifiInfo(int index, byte[] data)
        {
            bool flge = false;
            string reStr = "";
            byte[] Controldata = new byte[4 + data.Length];
            int code = 0;
            for (int i = 0; i < data.Length; i++)
            {
                code += data[i];
            }
            byte[] rdata = new byte[0];
            IMethodObjects imo = po.MethodsObject;
            switch (index)
            {
                case 0:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x04;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 4)
                    {
                        flge = true;
                    }
                    break;
                case 1:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x05;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 4)
                    {
                        MessageBox.Show("提示警告", "执行此结果将导致该设备无法正常获取，如果需要请重新执行连接指令！");
                        flge = true;
                    }
                    break;
                case 2:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x06;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 4)
                    {
                        flge = true;
                        Item item = new Item()
                        {
                            pathAddress = pathAddress,
                            password = data,
                            checkHex = ckb_Hex.Checked
                        };
                        using (FileStream file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\printPassword.xml", FileMode.OpenOrCreate))
                        {
                            XmlSerializer xml = new XmlSerializer(new DevPassword().GetType());
                            if (file.Length != 0)
                            {
                                var result = xml.Deserialize(file) as DevPassword;
                                var rtm = result.find(pathAddress);
                                if (rtm != null)
                                {
                                    rtm.password = data;
                                    rtm.checkHex = ckb_Hex.Checked;
                                }
                                else
                                {
                                    result.addItem(item);
                                }
                                file.SetLength(0);
                                file.Seek(0, 0);
                                xml.Serialize(file, result);
                            }
                            else
                            {
                                DevPassword dp = new DevPassword();
                                dp.addItem(item);
                                xml.Serialize(file, dp);
                            }
                        }
                    }
                    break;
                case 3:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x07;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 4:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x08;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 5:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x0a;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 6:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x0b;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 7:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x0d;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 8:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x0e;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 9:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x0f;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 10:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x10;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 11:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x12;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 12:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x13;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 13:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x14;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 14:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x15;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 15:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x19;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    else
                    {
                        MessageBox.Show("无此命令执行");
                    }
                    break;
                case 16:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x1a;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    break;
                case 17:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x1e;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    else
                    {
                        MessageBox.Show("无此命令执行");
                    }
                    break;
                case 18:
                    Array.Copy(data, 0, Controldata, 4, data.Length);
                    Controldata[0] = 0x10;
                    Controldata[1] = 0x16;
                    Controldata[2] = (byte)data.Length;
                    Controldata[3] = (byte)code;
                    rdata = imo.setWifiControl(po.onlyAlias, Controldata,1);
                    if (rdata.Length >= 0)
                    {

                        flge = true;
                    }
                    else
                    {
                        MessageBox.Show("无此命令执行");
                    }
                    break;
            }
            if (flge)
            {
                if ((rdata[2] > 0) || (rdata[2] == 0 && rdata[3] == 0))
                {
                    byte[] reData = new byte[rdata[2]];
                    Array.Copy(rdata, 4, reData, 0, rdata[2]);
                    if (rdata[2] > 0)
                    {
                        for (int i = 0; i < rdata[2]; i++)
                        {
                            reStr += reData[i];
                        }
                    }
                    else
                    {
                        reStr = "返回0个字节";
                    }
                }
                else
                {

                    reStr = "数据获取失败，错误码：" + rdata[3];
                }
            }
            else
            {
                reStr = "执行方法失败！";
            }
            this.txb_redata.AppendText(reStr + "\r\n");
        }

        private void cmb_Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txb_data_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ckb_Hex.Checked)
            {
                if (((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar < 65 || e.KeyChar > 70) && (e.KeyChar < 97 || e.KeyChar > 102)) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122)) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            try
            {
                new addCommend(SharMethod.user, btn_clear.Name, "清空返回数据信息内容");
                this.txb_redata.Clear();
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void btn_findPassword_Click(object sender, EventArgs e)
        {
            try
            {
                new addCommend(SharMethod.user, btn_findPassword.Name, "找回改程序设置的密码");
                string s = "";
                FileStream file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ClientPrints\\printPassword.xml", FileMode.OpenOrCreate);
                XmlSerializer xml = new XmlSerializer(new DevPassword().GetType());
                if (file.Length != 0)
                {
                    var result = xml.Deserialize(file) as DevPassword;
                    if (result != null)
                    {
                        var rtm = result.find(pathAddress);
                        if (rtm != null)
                        {
                            string pass = "";
                            if (rtm.checkHex)
                            {
                                for (int i = 0; i < rtm.password.Length; i++)
                                {
                                    pass += Convert.ToString(rtm.password[i], 2);
                                }

                            }
                            else
                            {
                                pass = Encoding.UTF8.GetString(rtm.password);
                            }
                            s = "密码：" + pass + "值Hex:" + rtm.checkHex;
                        }
                        else
                        {
                            s = "该设备在该程序中未设置过密码，无法获取信息";
                        }
                    }
                    else
                    {
                        s = "配置文件获取失败！";
                    }
                }
                else
                {
                    s = "该设备在该程序中未设置过密码，无法获取信息";
                }
                this.txb_redata.AppendText(s + "\r\n");
                file.Flush();
                file.Close();
                file.Dispose();
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void otherControlSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }
    }
}
