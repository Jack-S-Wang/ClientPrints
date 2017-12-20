using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrsintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using ClinetPrints.CreatContorl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static ClinetPrints.CreatContorl.dataGrieViewControl1;

namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    public partial class parmSetting : Form
    {
        public parmSetting()
        {
            InitializeComponent();
        }
        public PrinterObjects printerObject;
        private void cmb_wipeType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_cardType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_inCardWay_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_outCardWay_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_printTemperature_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_printContrast_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_printSheep_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_grayTemperature_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_wipeSheep_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_wipeTemperatuer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_printModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_sureParm_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = new byte[20];
                data[0] = 0;
                data[1] = 1;
                data[2] = 11;
                data[3] = 0;
                data[4] = 16;
                data[5] = 0x81;
                data[6] = (byte)cmb_wipeType.SelectedIndex;
                data[7] = (byte)cmb_cardType.SelectedIndex;
                data[8] = (byte)cmb_inCardWay.SelectedIndex;
                data[9] = (byte)cmb_outCardWay.SelectedIndex;
                data[10] = (byte)cmb_printTemperature.SelectedIndex;
                data[11] = (byte)cmb_printContrast.SelectedIndex;
                data[12] = (byte)cmb_printSheep.SelectedIndex;
                data[13] = (byte)cmb_grayTemperature.SelectedIndex;
                data[14] = (byte)cmb_wipeSheep.SelectedIndex;
                data[15] = (byte)cmb_wipeTemperatuer.SelectedIndex;
                data[16] = (byte)cmb_printModel.SelectedIndex;
                var method = printerObject.MethodsObject as IMethodObjects;
                string str = method.reInformation(WDevCmdObjects.DEV_SET_SYSPARAM, printerObject.pHandle, data);
                if (str != "false")
                {
                    if (Int32.Parse(str) == 1)
                    {
                        MessageBox.Show("保存成功！");
                    }
                    byte[] setData = new byte[11];
                    Array.Copy(data, 6, setData, 0, 11);
                    printerObject.pParams.DevParm = setData;
                }
                else
                {
                    MessageBox.Show("设备可能已经离线！");
                }

            }
            catch (Exception ex)
            {
                SharMethod.writeLog(string.Format("有错误：{0}，跟踪：{1}", ex, ex.StackTrace));
                MessageBox.Show(ex.Message);
            }
        }

        private void parmSetting_Load(object sender, EventArgs e)
        {
            try
            {
                if (printerObject != null)
                {
                    byte[] data = printerObject.pParams.DevParm;
                    cmb_wipeType.SelectedIndex = data[0];
                    cmb_cardType.SelectedIndex = data[1];
                    cmb_inCardWay.SelectedIndex = data[2];
                    cmb_outCardWay.SelectedIndex = data[3];
                    cmb_printTemperature.SelectedIndex = data[4];
                    cmb_printContrast.SelectedIndex = data[5];
                    cmb_printSheep.SelectedIndex = data[6];
                    cmb_grayTemperature.SelectedIndex = data[7];
                    cmb_wipeSheep.SelectedIndex = data[8];
                    cmb_wipeTemperatuer.SelectedIndex = data[9];
                    cmb_printModel.SelectedIndex = data[10];
                    bool cfg = WDevDllMethod.dllFunc_LoadDevCfg(printerObject.pHandle, "");
                    if (cfg)
                    {
                        UserColumnHanderCollection headerColls = new UserColumnHanderCollection(this.dataGrieViewControl11, new UserColumnHander[] { new UserColumnHander("名称"), new UserColumnHander("值") });
                        this.dataGrieViewControl11.handers = headerColls;
                        List<CfgDataObjects> liob = new List<CfgDataObjects>();
                        char[] buf = new char[512];
                        ushort cnt = 512;
                        for (int i = 0; WDevDllMethod.dllFunc_GetName(printerObject.pHandle, ref i, buf, ref cnt, WDevCmdObjects.DEVCFG_FMT_INFO, 0); i++, cnt = 512)
                        {
                            string str = new string(buf).Replace('\0', ' ').TrimEnd();
                            string name = str.Substring(5, str.Substring(0, str.IndexOf(',')).Length - 5);
                            buf = new char[512];
                            cnt = 512;
                            bool fg = WDevDllMethod.dllFunc_GetVal(printerObject.pHandle, name, buf, ref cnt, WDevCmdObjects.DEVCFG_VAL_INFO, 0);
                            if (fg)
                            {
                                string val = new string(buf).Replace('\0', ' ').TrimEnd();
                                var dataCfg = new CfgDataObjects(str, val);
                                liob.Add(dataCfg);
                            }
                        }
                        UserItems items = new UserItems(this.dataGrieViewControl11);
                        foreach (var keyco in liob)
                        {
                            if (keyco.type == 3)
                            {
                                UserSubControl usersub = new UserSubControl(keyco.Name, false);
                                ComboBox box = new ComboBox();
                                for (int i = 0; i < keyco.typeCount; i++)
                                {
                                    box.Items.Add(keyco.liValues[i]);
                                }
                                box.SelectedIndex = Int32.Parse(keyco.value);
                                box.KeyPress += Box_KeyPress;
                                UserSubControl userComb = new UserSubControl(box, true);
                                items.Add(new UserSubControl[] { usersub, userComb });
                            }
                            else
                            {
                                UserSubControl usersub = new UserSubControl(keyco.Name, false);
                                UserSubControl usersubVal = new UserSubControl(keyco.value, true);
                                items.Add(new UserSubControl[] { usersub, usersubVal });
                            }
                        }
                        this.dataGrieViewControl11.items = items;
                    }

                }
            }
            catch (Exception ex)
            {
                SharMethod.writeLog(string.Format("有错误：{0}，跟踪：{1}", ex, ex.StackTrace));
                MessageBox.Show(ex.Message);
            }

        }

        private void Box_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_sureCfg_Click(object sender, EventArgs e)
        {
            try
            {
                string name = "";
                string val = "";
                for (int i = 0; i < dataGrieViewControl11.items.Count; i++)
                {
                    name = dataGrieViewControl11.items[i].Value[0].control.Text;
                    if (dataGrieViewControl11.items[i].Value[1].control is ComboBox)
                    {
                        var com = dataGrieViewControl11.items[i].Value[1].control as ComboBox;
                        val = com.SelectedIndex.ToString();
                    }
                    else
                    {
                        val = dataGrieViewControl11.items[i].Value[1].control.Text;
                    }
                    bool flge = WDevDllMethod.dllFunc_SetDevCfgInfo(printerObject.pHandle, name, val, 0, 1);
                    if (!flge)
                    {
                        MessageBox.Show(name + ":修改设置失败！");
                        return;
                    }
                }
                MessageBox.Show("修改成功！");
            }
            catch (Exception ex)
            {
                SharMethod.writeLog(string.Format("有错误：{0}，跟踪：{1}", ex, ex.StackTrace));
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
