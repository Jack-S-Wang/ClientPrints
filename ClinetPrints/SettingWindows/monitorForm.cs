using System;
using System.Windows.Forms;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objects.Printers.JSON;
using Newtonsoft.Json;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using ClientPrsintsMethodList.ClientPrints.Method.WDevDll;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;

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
            if (printerObject.model.Contains("DC-1300"))
            {
                var method = printerObject.MethodsObject as IMethodObjects;
                //系统状态
                var stateStr = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x30 });
                if (stateStr == "false" || stateStr == "")
                {
                    MessageBox.Show("设备可能已经离线，将主动关闭监控！");
                    return;
                }
                var keyState = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300State>(stateStr);
                if (stateType != keyState.stateCode)
                {
                    stateType = keyState.stateCode;
                    txb_runState.Text = keyState.majorState;
                }
                if (!txb_error.Text.Contains(keyState.StateMessage))
                {
                    txb_error.Text = keyState.StateMessage;
                }
                //数据处理
                var dataPorcessStr = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x32 });
                if (dataPorcessStr == "false")
                {
                    MessageBox.Show("设备可能已经离线，将主动关闭监控！");
                    return;
                }
                var dataPor = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300DataState>(dataPorcessStr);
                if (dataStateType != dataPor.stateCode)
                {
                    dataStateType = dataPor.stateCode;
                    txb_dataPorcess.Text = dataPor.majorState;
                }
                if (!txb_porcessError.Text.Contains(dataPor.StateMessage))
                {
                    txb_porcessError.Text = dataPor.StateMessage;
                }
                if (!txb_jobIndex.Text.Contains(dataPor.workIndex.ToString()))
                {
                    txb_jobIndex.Text = dataPor.workIndex.ToString();
                }
                if (!txb_frame.Text.Contains(dataPor.dataFrames.ToString()))
                {
                    txb_frame.Text = dataPor.dataFrames.ToString();
                }
                //打印输出
                var printOutPut = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x33 });
                if (printOutPut == "false")
                {
                    MessageBox.Show("设备可能已经离线，将主动关闭监控！");
                    return;
                }
                var printOut = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300PrintState>(printOutPut);
                if (printStateType != printOut.stateCode)
                {
                    printStateType = printOut.stateCode;
                    txb_printPorcess.Text = printOut.majorState;
                }
                if (!txb_workNum.Text.Contains(printOut.taskNumber.ToString()))
                {
                    txb_workNum.Text = printOut.taskNumber.ToString();
                }
                if (!txb_outPutJobnum.Text.Contains(printOut.workIndex.ToString()))
                {
                    txb_outPutJobnum.Text = printOut.workIndex.ToString();
                }
                if (!txb_outFarme.Text.Contains(printOut.dataFrames.ToString()))
                {
                    txb_outFarme.Text = printOut.dataFrames.ToString();
                }
                if (!txb_tempertaure.Text.Contains(printOut.temperature.ToString()))
                {
                    txb_tempertaure.Text = printOut.temperature.ToString();
                }
                if (!txb_sensor.Text.Contains(printOut.sensor))
                {
                    txb_sensor.Text = printOut.sensor;
                }
                var printInfo = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x34 });
                if (printInfo == "false")
                {
                    MessageBox.Show("设备可能已经离线，将主动关闭监控！");
                    return;
                }
                var printInfoJson = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300DataPortState>(printInfo);
                if (!txb_cache.Text.Contains(printInfoJson.InCache.ToString()))
                {
                    txb_cache.Text = printInfoJson.InCache.ToString();
                }
                if (!txb_acceptSpace.Text.Contains(printInfoJson.residueCache.ToString()))
                {
                    txb_acceptSpace.Text = printInfoJson.residueCache.ToString();
                }
            }
        }

        private void btn_sure_Click(object sender, EventArgs e)
        {
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
