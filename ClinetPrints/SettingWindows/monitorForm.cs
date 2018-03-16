using System;
using System.Windows.Forms;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objects.Printers.JSON;
using Newtonsoft.Json;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using ClientPrsintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;
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
        System.Timers.Timer demandTime = new System.Timers.Timer(5000);
        private void monitorForm_Load(object sender, EventArgs e)
        {
            try
            {
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
            var stateStr = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x30 });
            if (stateStr == "false" || stateStr == "")
            {
                MessageBox.Show("设备可能已经离线，将主动关闭监控！");
                return;
            }
            int stateCode = 0;
            string majorState = "";
            string StateMessage = "";
            switch (printerObject.model)
            {
                case "DC-1300":
                    var keyState = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300State>(stateStr);
                    stateCode = keyState.stateCode;
                    majorState = keyState.majorState;
                    StateMessage = keyState.StateMessage;
                    break;
                case "DL-210":
                    var key210State = JsonConvert.DeserializeObject<PrinterDL210Json.PrinterDL210State>(stateStr);
                    stateCode = key210State.stateCode;
                    majorState = key210State.majorState;
                    StateMessage = key210State.StateMessage;
                    break;

            }
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
            var dataPorcessStr = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x32 });
            if (dataPorcessStr == "false")
            {
                MessageBox.Show("设备可能已经离线，将主动关闭监控！");
                return;
            }
            int datastateCode = 0;
            string datamajorState = "";
            string dataStateMessage = "";
            int dataworkIndex = 0;
            int dataFrames = 0;
            switch (printerObject.model)
            {
                case "DC-1300":
                    var dataPor = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300DataState>(dataPorcessStr);
                    datastateCode = dataPor.stateCode;
                    datamajorState = dataPor.majorState;
                    dataStateMessage = dataPor.StateMessage;
                    dataworkIndex = dataPor.workIndex;
                    dataFrames = dataPor.dataFrames;
                    break;
                case "DL-210":
                    var data210Por = JsonConvert.DeserializeObject<PrinterDL210Json.PrinterDL210DataState>(dataPorcessStr);
                    datastateCode = data210Por.stateCode;
                    datamajorState = data210Por.majorState;
                    dataStateMessage = data210Por.StateMessage;
                    dataworkIndex = data210Por.workIndex;
                    dataFrames = data210Por.dataFrames;
                    break;

            }

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

            var printOutPut = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x33 });
            if (printOutPut == "false")
            {
                MessageBox.Show("设备可能已经离线，将主动关闭监控！");
                return;
            }
            int OutstateCode = 0;
            string OutmajorState = "";
            int taskNumber = 0;
            int OutworkIndex = 0;
            int OutdataFrames = 0;
            int temperature = 0;
            string sensor = "";
            switch (printerObject.model)
            {
                case "DC-1300":
                    var printOut = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300PrintState>(printOutPut);
                    OutstateCode = printOut.stateCode;
                    OutmajorState = printOut.majorState;
                    OutworkIndex = printOut.workIndex;
                    OutdataFrames = printOut.dataFrames;
                    taskNumber = printOut.taskNumber;
                    temperature = printOut.temperature;
                    sensor = printOut.sensor;
                    break;
                case "DL-210":
                    var print210Out = JsonConvert.DeserializeObject<PrinterDL210Json.PrinterDL210PrintState>(printOutPut);
                    OutstateCode = print210Out.stateCode;
                    OutmajorState = print210Out.majorState;
                    OutworkIndex = print210Out.workIndex;
                    OutdataFrames = print210Out.dataFrames;
                    taskNumber = print210Out.taskNumber;
                    temperature = print210Out.temperature;
                    sensor = print210Out.sensor;
                    break;

            }

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
            if (!txb_outFarme.Text.Contains(OutdataFrames.ToString()))
            {
                txb_outFarme.Text = OutdataFrames.ToString();
            }
            if (!txb_tempertaure.Text.Contains(temperature.ToString()))
            {
                txb_tempertaure.Text = temperature.ToString();
            }
            if (!txb_sensor.Text.Contains(sensor))
            {
                txb_sensor.Text = sensor;
            }
            var printInfo = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x34 });
            if (printInfo == "false")
            {
                MessageBox.Show("设备可能已经离线，将主动关闭监控！");
                return;
            }
            int InCache = 0;
            int residueCache = 0;
            switch (printerObject.model)
            {
                case "DC-1300":
                    var printInfoJson = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300DataPortState>(printInfo);
                    InCache = printInfoJson.InCache;
                    residueCache = printInfoJson.residueCache;
                    break;
                case "DL-210":
                    var print210InfoJson = JsonConvert.DeserializeObject<PrinterDL210Json.PrinterDL210DataPortState>(printInfo);
                    InCache = print210InfoJson.InCache;
                    residueCache = print210InfoJson.residueCache;
                    break;
            }

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
                    str = method.reInformation(WDevCmdObjects.DEV_SET_OPER, printerObject.pHandle, new byte[] { (byte)(cmb_command.SelectedIndex + 1) });
                    if (str != "false")
                    {
                        txb_commandText.AppendText(DateTime.Now.ToString() + "：指令" + cmb_command.SelectedText + "已执行！");
                    }
                    else
                    {
                        txb_commandText.AppendText(DateTime.Now.ToString() + "：指令" + cmb_command.SelectedText + "执行失败！");
                    }
                }
                else
                {
                    MessageBox.Show("重启之后监控界面将自动关闭！");
                    str = method.reInformation(WDevCmdObjects.DEV_CMD_RESTART, printerObject.pHandle, new byte[0]);
                    if (str != "false")
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
