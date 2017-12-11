using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrintsMethodList.ClientPrints.Method.GeneralPrintersMethod.ClientPrints.Method.GeneralPrintersMethod.USBPrinters;
using ClientPrsintsObjectsAll.ClientPrints.Objects.DevDll;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objects.Printers.JSON;
using Newtonsoft.Json;

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
        private void monitorForm_Load(object sender, EventArgs e)
        {
            this.cmb_command.SelectedIndex = 0;
            System.Timers.Timer demandTime = new System.Timers.Timer(5000);
            demandTime.Enabled = true;
            demandTime.Elapsed += ((b, o) =>
            {
                if (IsHandleCreated)
                {
                    Invoke(new Action(readDevState));
                }
            });
        }
        private void readDevState()
        {
            if (printerObject.model.Contains("DC-1300"))
            {
                var method = printerObject.MethodsObject as PrintersGeneralFunction;
                //系统状态
                var stateStr = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x30 });
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
                var printInfo= method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x34 });
                var printInfoJson= JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300DataPortState>(printInfo);
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
            var method = printerObject.MethodsObject as PrintersGeneralFunction;
            string str = "";
            if (cmb_command.SelectedIndex < 9)
            {
                str=method.reInformation(WDevCmdObjects.DEV_SET_OPER, printerObject.pHandle, new byte[] { (byte)(cmb_command.SelectedIndex + 1) });
                if (str != "false")
                {
                    txb_commandText.AppendText(DateTime.Now.ToString() + "指令" + cmb_command.SelectedText + "已执行！");
                }
            }
            else
            {
                MessageBox.Show("重启之后监控界面将自动关闭！");
                str=method.reInformation(WDevCmdObjects.DEV_CMD_RESTART, printerObject.pHandle, new byte[0]);
                if(str!="false")
                {
                    txb_commandText.AppendText(DateTime.Now.ToString()+":设备已进行重启！");
                    this.Close();
                }
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
    }
}
