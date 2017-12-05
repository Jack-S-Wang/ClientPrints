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
        private void monitorForm_Load(object sender, EventArgs e)
        {
            System.Timers.Timer demandTime = new System.Timers.Timer(5000);
            demandTime.Enabled = true;
            demandTime.Elapsed += ((b, o) =>
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
                        txb_error.Text = keyState.StateMessage;
                        txb_runState.Text = keyState.majorState;
                    }
                    //数据处理
                    var dataDisposeStr = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x32 });
                    var dataDis = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300DataState>(dataDisposeStr);
                    if (dataStateType != dataDis.stateCode)
                    {
                        dataStateType = dataDis.stateCode;
                        txb_dataDispose.Text = dataDis.majorState;
                        txb_disposeError.Text = dataDis.StateMessage;
                    }
                    if (!txb_jobIndex.Text.Contains(dataDis.workIndex.ToString()))
                    {
                        txb_jobIndex.Text = dataDis.workIndex.ToString();
                    }
                    if (!txb_frame.Text.Contains(dataDis.dataFrames.ToString()))
                    {
                        txb_frame.Text = dataDis.dataFrames.ToString();
                    }
                    var printOutPut = method.reInformation(WDevCmdObjects.DEV_GET_DEVSTAT, printerObject.pHandle, new byte[] { 0x33 });
                    var printOut = JsonConvert.DeserializeObject<PrinterJson.PrinterDC1300PrintState>(printOutPut);

                }
            });
        }
    }
}
