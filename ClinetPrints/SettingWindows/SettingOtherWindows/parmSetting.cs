using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrintsMethodList.ClientPrints.Method.WDevDll;
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
        
        private void btn_sureParm_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, btn_sureParm.Name, "");
            try
            {
                byte[] alldata = new byte[4 + printerObject.pParams.DevParm.Length];
                switch (printerObject.model)
                {
                    case "DC-1300":
                        alldata[1] = 1;
                        alldata[2] = 1;
                        break;
                    case "DL-210":
                        alldata[1] = 2;
                        alldata[2] = 1;
                        break;
                }
                Array.Copy(printerObject.pParams.DevParm, 0, alldata, 4, printerObject.pParams.DevParm.Length);
                var method = printerObject.MethodsObject as IMethodObjects;
                string str = method.reInformation(WDevCmdObjects.DEV_SET_SYSPARAM, printerObject.pHandle,alldata);
                if (!str.Contains("false"))
                {
                    if (Int32.Parse(str) == 1)
                    {
                        MessageBox.Show("保存成功！");
                    }
                }
                else
                {
                    MessageBox.Show("设备可能已经离线！");
                }

            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                MessageBox.Show(ex.Message);
            }
        }

        private void parmSetting_Load(object sender, EventArgs e)
        {
            try
            {
                if (printerObject != null)
                {

                    if (printerObject.pParams.IsdevInfoParm)
                    {
                        byte[] data = printerObject.pParams.DevParm;
                        switch (printerObject.model)
                        {
                            case "DL-210":
                                addDL210(data);
                                break;
                            case "DC-1300":
                                addDC1300(data);
                                break;
                        }
                    }
                    else
                    {
                        this.dataGrieViewControl12.Enabled = false;
                        btn_sureParm.Enabled = false;
                        this.dataGrieViewControl12.Text = "该设备无此数据";
                    }
                    bool cfg = WDevDllMethod.dllFunc_LoadDevCfg(printerObject.pHandle, "");
                    if (cfg)
                    {
                        UserColumnHanderCollection headerColls = new UserColumnHanderCollection(this.dataGrieViewControl11, new UserColumnHander[] { new UserColumnHander("名称"), new UserColumnHander("值") });
                        this.dataGrieViewControl11.handers = headerColls;
                        List<CfgDataObjects> liob = new List<CfgDataObjects>();
                        char[] buf = new char[512];
                        ushort cnt = 512;
                        //L210最后一个参数为1
                        for (int i = 0; WDevDllMethod.dllFunc_GetName(printerObject.pHandle, ref i, buf, ref cnt, WDevCmdObjects.DEVCFG_FMT_INFO, 0); i++, cnt = 512)
                        {
                            string str = new string(buf).Replace('\0', ' ').TrimEnd();
                            string name = str.Substring(5, str.Substring(0, str.IndexOf(',')).Length - 5);
                            buf = new char[512];
                            cnt = 512;
                            bool fg = WDevDllMethod.dllFunc_GetVal(printerObject.pHandle, name, buf, ref cnt, WDevCmdObjects.DEVCFG_VAL_INFO, 1);
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
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                MessageBox.Show(ex.Message);
            }

        }

        private void Box_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_sureCfg_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, btn_sureCfg.Name, "");
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
                    bool flge = WDevDllMethod.dllFunc_SetDevCfgInfo(printerObject.pHandle, name, val, 1, 0);
                    if (!flge)
                    {
                        MessageBox.Show(name + ":修改设置失败！");
                        return;
                    }
                    if(i== (dataGrieViewControl11.items.Count - 1))//判断是否是最后一个数
                    {
                        //直接发送空数据，通知存入设备中
                         if(!WDevDllMethod.dllFunc_SetDevCfgInfo(printerObject.pHandle, null,null , 1, 1))
                        {
                            MessageBox.Show("修改到设备中失败！");
                            return;
                        }
                    }
                }
                MessageBox.Show("修改成功！将重新启动该设备！");
                string str = (printerObject.MethodsObject as IMethodObjects).reInformation(WDevCmdObjects.DEV_CMD_RESTART, printerObject.pHandle, new byte[0]);
                if (!str.Contains("false"))
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        /// 添加210的参数信息
        /// </summary>
        /// <param name="data"></param>
        private void addDL210(byte[] data)
        {
            //可以简化成一个集合，然后遍历所有值进行赋值添加控件，置换值就不太方便了
            UserColumnHanderCollection headerparm = new UserColumnHanderCollection(this.dataGrieViewControl12, new UserColumnHander[] { new UserColumnHander("名称"), new UserColumnHander("值") });
            this.dataGrieViewControl12.handers = headerparm;
            UserItems items = new UserItems(this.dataGrieViewControl12);
            UserSubControl usersub = new UserSubControl("PageLength", false);
            UserSubControl usersubVal = new UserSubControl((data[2] * 256 + data[3]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                string s = usersubVal.control.Text;
                printerObject.pParams.DevParm[2] = (byte)(short.Parse(s)>>8);
                printerObject.pParams.DevParm[3] = byte.Parse(s);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("ContPaperLength", false);
            usersubVal = new UserSubControl((data[4] * 256 + data[5]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                string s = usersubVal.control.Text;
                printerObject.pParams.DevParm[4] = (byte)(short.Parse(s) >> 8);
                printerObject.pParams.DevParm[5] = byte.Parse(s);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("MaxMediaLength", false);
            usersubVal = new UserSubControl((data[6] * 256 + data[7]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                string s = usersubVal.control.Text;
                printerObject.pParams.DevParm[6] = (byte)(short.Parse(s) >> 8);
                printerObject.pParams.DevParm[7] = byte.Parse(s);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("VerticalPosition", false);
            usersubVal = new UserSubControl((data[8] * 256 + data[9]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                string s = usersubVal.control.Text;
                printerObject.pParams.DevParm[8] = (byte)(short.Parse(s) >> 8);
                printerObject.pParams.DevParm[9] = byte.Parse(s);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("TearOffAdjustPosition", false);
            usersubVal = new UserSubControl((data[10] * 256 + data[11]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                string s = usersubVal.control.Text;
                printerObject.pParams.DevParm[10] = (byte)(short.Parse(s) >> 8);
                printerObject.pParams.DevParm[11] = byte.Parse(s);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("PrintMethod", false);
            usersubVal = new UserSubControl((data[12]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[12] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("PrintPaperType", false);
            usersubVal = new UserSubControl((data[13]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[13] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("Gap_Length", false);
            usersubVal = new UserSubControl((data[14]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[14] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("PrintSpeed", false);
            usersubVal = new UserSubControl((data[15]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[15] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("ZPL_PrintDarkness", false);
            usersubVal = new UserSubControl((data[16]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[16] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("CutterOption", false);
            usersubVal = new UserSubControl((data[17]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[17] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("PeelOption", false);
            usersubVal = new UserSubControl((data[18]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "")
                {
                    return;
                }
                    printerObject.pParams.DevParm[18] = byte.Parse(usersubVal.control.Text);
                
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            this.dataGrieViewControl12.items = items;

        }

        /// <summary>
        /// 添加1300的参数信息
        /// </summary>
        /// <param name="data"></param>
        private void addDC1300(byte[] data)
        {
            UserColumnHanderCollection headerparm = new UserColumnHanderCollection(this.dataGrieViewControl12, new UserColumnHander[] { new UserColumnHander("名称"), new UserColumnHander("值") });
            this.dataGrieViewControl12.handers = headerparm;
            UserItems items = new UserItems(this.dataGrieViewControl12);
            UserSubControl usersub = new UserSubControl("擦除位图类型", false);
            UserSubControl usersubVal = new UserSubControl((data[2]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[2] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("卡片类型", false);
            usersubVal = new UserSubControl((data[3]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[3] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("进卡方式", false);
            usersubVal = new UserSubControl((data[4]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[4] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("出卡方式", false);
            usersubVal = new UserSubControl((data[5]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[5] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("打印温度", false);
            usersubVal = new UserSubControl((data[6]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[6] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("打印对比度", false);
            usersubVal = new UserSubControl((data[7]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[7] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("打印速度", false);
            usersubVal = new UserSubControl((data[8]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[8] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("灰度温度", false);
            usersubVal = new UserSubControl((data[10]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[10] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("擦除速度", false);
            usersubVal = new UserSubControl((data[11]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[11] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("设置擦除温度", false);
            usersubVal = new UserSubControl((data[12]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[12] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            usersub = new UserSubControl("打印模式", false);
            usersubVal = new UserSubControl((data[13]).ToString(), true);
            usersubVal.control.TextChanged += (o, e) =>
            {
                if (usersubVal.control.Text == "") { return; }
                printerObject.pParams.DevParm[13] = byte.Parse(usersubVal.control.Text);
            };
            usersubVal.control.KeyPress += (o, e) =>
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            };
            items.Add(new UserSubControl[] { usersub, usersubVal });
            this.dataGrieViewControl12.items = items;
        }

    }
}
