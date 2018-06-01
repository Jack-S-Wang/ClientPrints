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
            string mainState = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "System State.Run State");
            if (mainState.Contains("0"))
            {
                majorState = "空闲";
                stateCode = 1;
            }
            else if (mainState.Contains("1"))
            {
                majorState = "工作中";
                stateCode = 3;
            }
            else if (mainState.Contains("2"))
            {
                majorState = "就绪";
                stateCode = 2;
            }
            else if (mainState.Contains("3"))
            {
                majorState = "繁忙";
                stateCode = 4;
            }
            else if (mainState.Contains("255"))
            {
                majorState = "异常";
                stateCode = 6;
            }

            string StateMessage = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "System State.Error");
            StateMessage = StateMessage.Substring((StateMessage.IndexOf(';') + 1));
            //switch (printerObject.model)
            //{
            //    case "DC-1300":
            //        var keyState = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300State>(stateStr);
            //        stateCode = keyState.stateCode;
            //        majorState = keyState.majorState;
            //        StateMessage = keyState.StateMessage;
            //        break;
            //    case "DL-210":
            //        var key210State = JsonConvert.DeserializeObject<PrinterDL210Json.PrinterDL210State>(stateStr);
            //        stateCode = key210State.stateCode;
            //        majorState = key210State.majorState;
            //        StateMessage = key210State.StateMessage;
            //        break;

            //}
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
            string ds = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Data State.Process");
            int datastateCode = Int32.Parse(ds.Substring(0, ds.IndexOf(';')));
            string datamajorState = ds.Substring(ds.IndexOf(';') + 1);
            string error = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Data State.Error Code");
            string dataStateMessage = error.Substring(error.IndexOf(';') + 1);
            int dataworkIndex = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Data State.Job Number"));
            int dataFrames = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Data State.Frame Number"));
            //redata = new byte[] { 0x32 };
            //var dataPorcessStr = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle,ref redata );
            //if (dataPorcessStr.Contains("false"))
            //{
            //    MessageBox.Show("设备可能已经离线，将主动关闭监控！");
            //    return;
            //}
            //switch (printerObject.model)
            //{
            //    case "DC-1300":
            //        var dataPor = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300DataState>(dataPorcessStr);
            //        datastateCode = dataPor.stateCode;
            //        datamajorState = dataPor.majorState;
            //        dataStateMessage = dataPor.StateMessage;
            //        dataworkIndex = dataPor.workIndex;
            //        dataFrames = dataPor.dataFrames;
            //        break;
            //    case "DL-210":
            //        var data210Por = JsonConvert.DeserializeObject<PrinterDL210Json.PrinterDL210DataState>(dataPorcessStr);
            //        datastateCode = data210Por.stateCode;
            //        datamajorState = data210Por.majorState;
            //        dataStateMessage = data210Por.StateMessage;
            //        dataworkIndex = data210Por.workIndex;
            //        dataFrames = data210Por.dataFrames;
            //        break;

            //}

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
            System.Diagnostics.Trace.TraceInformation("++++++++++++++++++ 1");
            string oc = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Print State.Output State");
            int OutstateCode = Int32.Parse(oc.Substring(0,oc.IndexOf(';')));
            string OutmajorState = oc.Substring(oc.IndexOf(';')+1);
            System.Diagnostics.Trace.TraceInformation("++++++++++++++++++ 2");
            int taskNumber = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Print State.Number of Print"));
            System.Diagnostics.Trace.TraceInformation("++++++++++++++++++ 3");
            int OutworkIndex = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Print State.Completed Job Number"));
            System.Diagnostics.Trace.TraceInformation("++++++++++++++++++ 4");
            int OutdataFrames = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Print State.Completed Frame Number"));
            System.Diagnostics.Trace.TraceInformation("++++++++++++++++++ 5");
            int temperature = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Print State.Device Temperature"));
            System.Diagnostics.Trace.TraceInformation("++++++++++++++++++ 6");
            string sensor0 = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Print State.Sensor0");
            System.Diagnostics.Trace.TraceInformation("++++++++++++++++++ 7");
            string sensor1 = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Print State.Sensor1");
            System.Diagnostics.Trace.TraceInformation("++++++++++++++++++ 8");
            string sensor2 = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Print State.Sensor2");
            System.Diagnostics.Trace.TraceInformation("++++++++++++++++++ 9");
            string sensor3 = dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Print State.Sensor3");
            //redata = new byte[] { 0x33 };
            //var printOutPut = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, ref redata);
            //if (printOutPut.Contains("false"))
            //{
            //    MessageBox.Show("设备可能已经离线，将主动关闭监控！");
            //    return;
            //}

            //switch (printerObject.model)
            //{
            //    case "DC-1300":
            //        var printOut = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300PrintState>(printOutPut);
            //        OutstateCode = printOut.stateCode;
            //        OutmajorState = printOut.majorState;
            //        OutworkIndex = printOut.workIndex;
            //        OutdataFrames = printOut.dataFrames;
            //        taskNumber = printOut.taskNumber;
            //        temperature = printOut.temperature;
            //        sensor = printOut.sensor;
            //        break;
            //    case "DL-210":
            //        var print210Out = JsonConvert.DeserializeObject<PrinterDL210Json.PrinterDL210PrintState>(printOutPut);
            //        OutstateCode = print210Out.stateCode;
            //        OutmajorState = print210Out.majorState;
            //        OutworkIndex = print210Out.workIndex;
            //        OutdataFrames = print210Out.dataFrames;
            //        taskNumber = print210Out.taskNumber;
            //        temperature = print210Out.temperature;
            //        sensor = print210Out.sensor;
            //        break;

            //}

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
            //if (!txb_outFarme.Text.Contains(OutdataFrames.ToString()))
            //{
            //    txb_outFarme.Text = OutdataFrames.ToString();
            //}
            if (!txb_tempertaure.Text.Contains(temperature.ToString()))
            {
                txb_tempertaure.Text = temperature.ToString();
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
            //redata = new byte[] { 0x34 };
            //var printInfo = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, ref redata);
            //if (printInfo.Contains("false"))
            //{
            //    MessageBox.Show("设备可能已经离线，将主动关闭监控！");
            //    return;
            //}
            System.Diagnostics.Trace.TraceInformation("++++++++++++++++++ 10");
            int InCache = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Data Interface State.The percentage point ratio of the received buffer data"));
            System.Diagnostics.Trace.TraceInformation("++++++++++++++++++ 11");
            int residueCache = Int32.Parse(dj.getDataJsonInfo(redata, (uint)WDevCmdObjects.DEVJSON_INFO_ENTRY, "Data Interface State.The remaining space in the receiving buffer"));
            //switch (printerObject.model)
            //{
            //    case "DC-1300":
            //        var printInfoJson = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300DataPortState>(printInfo);
            //        InCache = printInfoJson.InCache;
            //        residueCache = printInfoJson.residueCache;
            //        break;
            //    case "DL-210":
            //        var print210InfoJson = JsonConvert.DeserializeObject<PrinterDL210Json.PrinterDL210DataPortState>(printInfo);
            //        InCache = print210InfoJson.InCache;
            //        residueCache = print210InfoJson.residueCache;
            //        break;
            //}

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
