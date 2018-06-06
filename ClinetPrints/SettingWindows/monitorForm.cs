using System;
using System.Windows.Forms;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objects.Printers.JSON;
using Newtonsoft.Json;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using ClientPrintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using System.Threading;

namespace ClinetPrints.SettingWindows
{

    public partial class monitorForm : Form
    {
        public monitorForm()
        {
            InitializeComponent();
        }
        public PrinterObjects printerObject;
        /// <summary>
        /// 记录系统状态码
        /// </summary>
        private int stateType = -1;
        /// <summary>
        /// 记录数据处理状态码
        /// </summary>
        private int dataStateType = -1;
        /// <summary>
        /// 打印输出状态码
        /// </summary>
        private int printStateType = -1;
        bool ative = true;
        System.Timers.Timer demandTime = new System.Timers.Timer(8000);
        private void monitorForm_Load(object sender, EventArgs e)
        {
            try
            {
                //switch (printerObject.model)
                //{
                //    case "DC-1300":

                //        break;
                //    case "DL-210":
                //        break;
                //}
                this.cmb_command.Items.Add("暂停");
                this.cmb_command.Items.Add("恢复");
                this.cmb_command.Items.Add("清除打印作业");
                this.cmb_command.Items.Add("进卡");
                this.cmb_command.Items.Add("退卡");
                this.cmb_command.Items.Add("清洁打印头");
                this.cmb_command.Items.Add("测试卡1");
                this.cmb_command.Items.Add("测试卡2");
                this.cmb_command.Items.Add("卡擦除");
                this.cmb_command.Items.Add("重启设备");

                this.cmb_command.SelectedIndex = 0;
                demandTime.Enabled = true;
                demandTime.Elapsed += ((b, o) =>
                {
                    if (IsHandleCreated)
                    {
                        if (ative)
                            Invoke(new Action(readDevState));
                    }
                });
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                MessageBox.Show(ex.Message);
                this.Close();
                return;
            }

        }
        private bool readS = true;
        private void readDevState()
        {
            if (!readS)
            {
                return;
            }
            var method = printerObject.MethodsObject as IMethodObjects;
            //系统状态
            byte[] redata = new byte[] { 0 };
            var stateStr = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, ref redata);
            if (stateStr.Contains("false"))
            {
                MessageBox.Show("设备可能已经离线，将主动关闭监控！");
                return;
            }
            dataJson dj = new dataJson();
            int stateCode = 0;
            string majorState = "";
            string mainState = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.SSRunState,false);
            if (mainState.Contains("idle"))
            {
                majorState = "空闲";
                stateCode = 1;
            }
            else if (mainState.Contains("At work"))
            {
                majorState = "工作中";
                stateCode = 3;
            }
            else if (mainState.Contains("Ready"))
            {
                majorState = "就绪";
                stateCode = 2;
            }
            else if (mainState.Contains("Busy"))
            {
                majorState = "繁忙";
                stateCode = 4;
            }else if (mainState.Contains("Pause"))
            {
                majorState = "暂停";
                stateCode = 5;
            }
            else if (mainState.Contains("Error"))
            {
                majorState = "异常";
                stateCode = 6;
            }

            string StateMessage = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.SSError,false);
            StateMessage = StateMessage.Substring((StateMessage.IndexOf(';') + 1));

            if (stateType != stateCode)
            {
                stateType = stateCode;
                txb_runState.Text = majorState;
            }
            if (!txb_error.Text.Contains(StateMessage))
            {
                txb_error.Text = StateMessage;
            }
            //数据处理
            string ds = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.DSProcess,false);
            int datastateCode = Int32.Parse(ds.Substring(0, ds.IndexOf(';')));
            string datamajorState = ds.Substring(ds.IndexOf(';') + 1);
            string error = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.DSErrorCode,false);
            string dataStateMessage = error.Substring(error.IndexOf(';') + 1);
            string dataworkIndex = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.DSJobNumber,false);
            string dataFrames = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.DSFrameNumber,false);

            if (dataStateType != datastateCode)
            {
                dataStateType = datastateCode;
                txb_dataPorcess.Text = datamajorState;
            }
            if (!txb_porcessError.Text.Contains(dataStateMessage))
            {
                txb_porcessError.Text = dataStateMessage;
            }
            if (!txb_jobIndex.Text.Contains(dataworkIndex.ToString()))
            {
                txb_jobIndex.Text = dataworkIndex.ToString();
            }
            if (!txb_frame.Text.Contains(dataFrames.ToString()))
            {
                txb_frame.Text = dataFrames.ToString();
            }
            //打印输出
            string oc = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.PSOutputState,false);
            int OutstateCode = Int32.Parse(oc.Substring(0, oc.IndexOf(';')));
            string OutmajorState = oc.Substring(oc.IndexOf(';') + 1);
            int taskNumber = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.PSNumberOfPrint,false));
            int OutworkIndex = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.PSCompletedJobNumber,false));
            int OutdataFrames = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.PSCompletedFrameNumber,false));
            int temperature = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.PSTemperature,false));
            string sensor0 = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.PSSensor0,false);
            string sensor1 = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.PSSensor1,false);
            string sensor2 = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.PSSensor2,false);
            string sensor3 = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.PSSensor3,false);


            if (printStateType != OutstateCode)
            {
                printStateType = OutstateCode;
                txb_printPorcess.Text = OutmajorState;
            }
            if (!txb_workNum.Text.Contains(taskNumber.ToString()))
            {
                txb_workNum.Text = taskNumber.ToString();
            }
            if (!txb_outPutJobnum.Text.Contains(OutworkIndex.ToString()))
            {
                txb_outPutJobnum.Text = OutworkIndex.ToString();
            }

            if (!txb_tempertaure.Text.Contains(temperature.ToString()))
            {
                txb_tempertaure.Text = temperature.ToString();
            }
            if (!txb_outFarme.Text.Contains(OutdataFrames.ToString()))
            {
                txb_outFarme.Text = OutdataFrames.ToString();
            }
            if (!txb_sensor0.Text.Contains(sensor0))
            {
                txb_sensor0.Text = sensor0;
            }
            if (!txb_sensor1.Text.Contains(sensor1))
            {
                txb_sensor1.Text = sensor1;
            }
            if (!txb_sensor2.Text.Contains(sensor2))
            {
                txb_sensor2.Text = sensor2;
            }
            if (!txb_sensor3.Text.Contains(sensor3))
            {
                txb_sensor3.Text = sensor3;
            }

            int InCache = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.DISInCache,false));
            int residueCache = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, PrinterSharJson.DISResidueCache,true));


            if (!txb_cache.Text.Contains(InCache.ToString()))
            {
                txb_cache.Text = InCache.ToString();
            }
            if (!txb_acceptSpace.Text.Contains(residueCache.ToString()))
            {
                txb_acceptSpace.Text = residueCache.ToString();
            }

        }

        private void btn_sure_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, "控制选择命令的内容", cmb_command.Text);
            new addCommend(SharMethod.user, btn_sure.Name, "监控控制指令确认");
            try
            {
                var method = printerObject.MethodsObject as IMethodObjects;
                string str = "";
                if (cmb_command.SelectedIndex < 9)
                {
                    byte[] redata = new byte[] { (byte)(cmb_command.SelectedIndex + 1) };
                    str = method.reInformation(WDevCmdObjects.DEV_SET_OPER, printerObject.pHandle, ref redata);
                    if (!str.Contains("false"))
                    {
                        txb_commandText.AppendText(DateTime.Now.ToString() + "：指令" + cmb_command.SelectedText + "已执行！");
                    }
                    else
                    {
                        txb_commandText.AppendText(DateTime.Now.ToString() + "：指令" + cmb_command.SelectedText + "执行失败！" + str);
                    }
                }
                else
                {
                    MessageBox.Show("重启之后监控界面将自动关闭！");
                    byte[] redata = new byte[0];
                    str = method.reInformation(WDevCmdObjects.DEV_CMD_RESTART, printerObject.pHandle, ref redata);
                    if (!str.Contains("false"))
                    {
                        txb_commandText.AppendText(DateTime.Now.ToString() + ":设备已进行重启！");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void txb_commandText_TextChanged(object sender, EventArgs e)
        {
            if (txb_commandText.TextLength > 5000)
            {
                txb_commandText.Clear();
            }
        }

        private void cmb_command_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void monitorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ative = false;
        }

        private void btn_getFile_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, btn_getFile.Name, "监控选择文件");
            try
            {
                this.openFileDialog1.ShowDialog();
                this.txb_getFile.Text = this.openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, btn_up.Name, "监控更新");
            try
            {
                if (this.txb_getFile.Text != "")
                {
                    readS = false;
                    if (WDevDllMethod.dllFunc_OpenDfu(printerObject.pHandle, txb_getFile.Text, this.Handle))
                    {
                        txb_commandText.AppendText("已加载固件文件！监控已关闭！");
                        uint tages = 0x01;
                        WDevDllMethod.dllFunc_DFUStart(printerObject.pHandle, tages);
                        txb_commandText.AppendText("正在更新固件！");
                    }
                }
            }
            catch (Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
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
                            WDevDllMethod.dllFunc_CloseDfu(printerObject.pHandle);
                            MessageBox.Show("固件升级成功，将主动关闭界面！");
                            this.Close();
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
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
                MessageBox.Show(ex.Message);
            }
        }

        private void ckb_monitor_CheckedChanged(object sender, EventArgs e)
        {
            new addCommend(SharMethod.user, ckb_monitor.Name, "自主监控是否启用");
            if (ckb_monitor.Checked)
            {
                demandTime.Enabled = false;
            }
            else
            {
                demandTime.Enabled = true;
            }
        }
    }
}
