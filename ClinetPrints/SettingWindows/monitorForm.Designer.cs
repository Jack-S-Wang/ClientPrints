namespace ClinetPrints.SettingWindows
{
    partial class monitorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txb_commandText = new System.Windows.Forms.TextBox();
            this.btn_sure = new System.Windows.Forms.Button();
            this.cmb_command = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txb_outFarme = new System.Windows.Forms.TextBox();
            this.txb_outPutJobnum = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txb_tempertaure = new System.Windows.Forms.TextBox();
            this.txb_sensor = new System.Windows.Forms.TextBox();
            this.txb_workNum = new System.Windows.Forms.TextBox();
            this.txb_printPorcess = new System.Windows.Forms.TextBox();
            this.txb_acceptSpace = new System.Windows.Forms.TextBox();
            this.txb_cache = new System.Windows.Forms.TextBox();
            this.txb_dataPort = new System.Windows.Forms.TextBox();
            this.txb_frame = new System.Windows.Forms.TextBox();
            this.txb_porcessError = new System.Windows.Forms.TextBox();
            this.txb_jobIndex = new System.Windows.Forms.TextBox();
            this.txb_dataPorcess = new System.Windows.Forms.TextBox();
            this.txb_error = new System.Windows.Forms.TextBox();
            this.txb_runState = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txb_getFile = new System.Windows.Forms.TextBox();
            this.btn_getFile = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 624);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.txb_commandText);
            this.groupBox2.Controls.Add(this.btn_sure);
            this.groupBox2.Controls.Add(this.cmb_command);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(356, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 624);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "控制";
            // 
            // txb_commandText
            // 
            this.txb_commandText.Location = new System.Drawing.Point(18, 502);
            this.txb_commandText.Multiline = true;
            this.txb_commandText.Name = "txb_commandText";
            this.txb_commandText.Size = new System.Drawing.Size(236, 116);
            this.txb_commandText.TabIndex = 3;
            this.txb_commandText.TextChanged += new System.EventHandler(this.txb_commandText_TextChanged);
            // 
            // btn_sure
            // 
            this.btn_sure.Location = new System.Drawing.Point(113, 165);
            this.btn_sure.Name = "btn_sure";
            this.btn_sure.Size = new System.Drawing.Size(75, 23);
            this.btn_sure.TabIndex = 2;
            this.btn_sure.Text = "确认";
            this.btn_sure.UseVisualStyleBackColor = true;
            this.btn_sure.Click += new System.EventHandler(this.btn_sure_Click);
            // 
            // cmb_command
            // 
            this.cmb_command.FormattingEnabled = true;
            this.cmb_command.Items.AddRange(new object[] {
            "暂停",
            "恢复",
            "清洁打印头",
            "进卡",
            "退卡",
            "测试卡1",
            "测试卡2",
            "卡擦除",
            "清除打印作业",
            "重启设备"});
            this.cmb_command.Location = new System.Drawing.Point(91, 107);
            this.cmb_command.Name = "cmb_command";
            this.cmb_command.Size = new System.Drawing.Size(142, 20);
            this.cmb_command.TabIndex = 1;
            this.cmb_command.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_command_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(20, 110);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "控制选择：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txb_outFarme);
            this.groupBox1.Controls.Add(this.txb_outPutJobnum);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txb_tempertaure);
            this.groupBox1.Controls.Add(this.txb_sensor);
            this.groupBox1.Controls.Add(this.txb_workNum);
            this.groupBox1.Controls.Add(this.txb_printPorcess);
            this.groupBox1.Controls.Add(this.txb_acceptSpace);
            this.groupBox1.Controls.Add(this.txb_cache);
            this.groupBox1.Controls.Add(this.txb_dataPort);
            this.groupBox1.Controls.Add(this.txb_frame);
            this.groupBox1.Controls.Add(this.txb_porcessError);
            this.groupBox1.Controls.Add(this.txb_jobIndex);
            this.groupBox1.Controls.Add(this.txb_dataPorcess);
            this.groupBox1.Controls.Add(this.txb_error);
            this.groupBox1.Controls.Add(this.txb_runState);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 624);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "监控";
            // 
            // txb_outFarme
            // 
            this.txb_outFarme.Location = new System.Drawing.Point(118, 590);
            this.txb_outFarme.Name = "txb_outFarme";
            this.txb_outFarme.ReadOnly = true;
            this.txb_outFarme.Size = new System.Drawing.Size(153, 21);
            this.txb_outFarme.TabIndex = 29;
            // 
            // txb_outPutJobnum
            // 
            this.txb_outPutJobnum.Location = new System.Drawing.Point(118, 554);
            this.txb_outPutJobnum.Name = "txb_outPutJobnum";
            this.txb_outPutJobnum.ReadOnly = true;
            this.txb_outPutJobnum.Size = new System.Drawing.Size(153, 21);
            this.txb_outPutJobnum.TabIndex = 28;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 593);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 27;
            this.label15.Text = "完成帧号：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 557);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 26;
            this.label14.Text = "输出作业号：";
            // 
            // txb_tempertaure
            // 
            this.txb_tempertaure.Location = new System.Drawing.Point(118, 510);
            this.txb_tempertaure.Name = "txb_tempertaure";
            this.txb_tempertaure.ReadOnly = true;
            this.txb_tempertaure.Size = new System.Drawing.Size(153, 21);
            this.txb_tempertaure.TabIndex = 25;
            // 
            // txb_sensor
            // 
            this.txb_sensor.Location = new System.Drawing.Point(118, 470);
            this.txb_sensor.Name = "txb_sensor";
            this.txb_sensor.ReadOnly = true;
            this.txb_sensor.Size = new System.Drawing.Size(153, 21);
            this.txb_sensor.TabIndex = 24;
            // 
            // txb_workNum
            // 
            this.txb_workNum.Location = new System.Drawing.Point(118, 430);
            this.txb_workNum.Name = "txb_workNum";
            this.txb_workNum.ReadOnly = true;
            this.txb_workNum.Size = new System.Drawing.Size(153, 21);
            this.txb_workNum.TabIndex = 23;
            // 
            // txb_printPorcess
            // 
            this.txb_printPorcess.Location = new System.Drawing.Point(118, 390);
            this.txb_printPorcess.Name = "txb_printPorcess";
            this.txb_printPorcess.ReadOnly = true;
            this.txb_printPorcess.Size = new System.Drawing.Size(153, 21);
            this.txb_printPorcess.TabIndex = 22;
            // 
            // txb_acceptSpace
            // 
            this.txb_acceptSpace.Location = new System.Drawing.Point(118, 350);
            this.txb_acceptSpace.Name = "txb_acceptSpace";
            this.txb_acceptSpace.ReadOnly = true;
            this.txb_acceptSpace.Size = new System.Drawing.Size(153, 21);
            this.txb_acceptSpace.TabIndex = 21;
            // 
            // txb_cache
            // 
            this.txb_cache.Location = new System.Drawing.Point(118, 310);
            this.txb_cache.Name = "txb_cache";
            this.txb_cache.ReadOnly = true;
            this.txb_cache.Size = new System.Drawing.Size(153, 21);
            this.txb_cache.TabIndex = 20;
            // 
            // txb_dataPort
            // 
            this.txb_dataPort.Location = new System.Drawing.Point(118, 270);
            this.txb_dataPort.Name = "txb_dataPort";
            this.txb_dataPort.ReadOnly = true;
            this.txb_dataPort.Size = new System.Drawing.Size(153, 21);
            this.txb_dataPort.TabIndex = 19;
            this.txb_dataPort.Text = "保留";
            // 
            // txb_frame
            // 
            this.txb_frame.Location = new System.Drawing.Point(118, 230);
            this.txb_frame.Name = "txb_frame";
            this.txb_frame.ReadOnly = true;
            this.txb_frame.Size = new System.Drawing.Size(153, 21);
            this.txb_frame.TabIndex = 18;
            // 
            // txb_porcessError
            // 
            this.txb_porcessError.Location = new System.Drawing.Point(118, 190);
            this.txb_porcessError.Name = "txb_porcessError";
            this.txb_porcessError.ReadOnly = true;
            this.txb_porcessError.Size = new System.Drawing.Size(153, 21);
            this.txb_porcessError.TabIndex = 17;
            // 
            // txb_jobIndex
            // 
            this.txb_jobIndex.Location = new System.Drawing.Point(118, 150);
            this.txb_jobIndex.Name = "txb_jobIndex";
            this.txb_jobIndex.ReadOnly = true;
            this.txb_jobIndex.Size = new System.Drawing.Size(153, 21);
            this.txb_jobIndex.TabIndex = 16;
            // 
            // txb_dataPorcess
            // 
            this.txb_dataPorcess.Location = new System.Drawing.Point(118, 110);
            this.txb_dataPorcess.Name = "txb_dataPorcess";
            this.txb_dataPorcess.ReadOnly = true;
            this.txb_dataPorcess.Size = new System.Drawing.Size(153, 21);
            this.txb_dataPorcess.TabIndex = 15;
            // 
            // txb_error
            // 
            this.txb_error.Location = new System.Drawing.Point(118, 70);
            this.txb_error.Name = "txb_error";
            this.txb_error.ReadOnly = true;
            this.txb_error.Size = new System.Drawing.Size(153, 21);
            this.txb_error.TabIndex = 14;
            // 
            // txb_runState
            // 
            this.txb_runState.Location = new System.Drawing.Point(118, 30);
            this.txb_runState.Name = "txb_runState";
            this.txb_runState.ReadOnly = true;
            this.txb_runState.Size = new System.Drawing.Size(153, 21);
            this.txb_runState.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(39, 513);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 12;
            this.label13.Text = "温度：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 473);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "传感器：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 433);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "任务数：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 393);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "打印处理：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 353);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "接受余空间：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 313);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "缓存比：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 273);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "数据端口：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "帧号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "处理错误：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "作业号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据处理：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "系统错误：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "运行状态：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.progressBar1);
            this.groupBox3.Controls.Add(this.btn_up);
            this.groupBox3.Controls.Add(this.btn_getFile);
            this.groupBox3.Controls.Add(this.txb_getFile);
            this.groupBox3.Location = new System.Drawing.Point(18, 217);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(233, 268);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "固件升级";
            // 
            // txb_getFile
            // 
            this.txb_getFile.Location = new System.Drawing.Point(22, 53);
            this.txb_getFile.Name = "txb_getFile";
            this.txb_getFile.Size = new System.Drawing.Size(194, 21);
            this.txb_getFile.TabIndex = 1;
            // 
            // btn_getFile
            // 
            this.btn_getFile.Location = new System.Drawing.Point(141, 91);
            this.btn_getFile.Name = "btn_getFile";
            this.btn_getFile.Size = new System.Drawing.Size(75, 23);
            this.btn_getFile.TabIndex = 2;
            this.btn_getFile.Text = "选择文件";
            this.btn_getFile.UseVisualStyleBackColor = true;
            this.btn_getFile.Click += new System.EventHandler(this.btn_getFile_Click);
            // 
            // btn_up
            // 
            this.btn_up.Location = new System.Drawing.Point(141, 131);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(75, 23);
            this.btn_up.TabIndex = 3;
            this.btn_up.Text = "升级";
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 176);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(221, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // monitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 624);
            this.Controls.Add(this.panel1);
            this.Name = "monitorForm";
            this.Text = "监控控制界面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.monitorForm_FormClosing);
            this.Load += new System.EventHandler(this.monitorForm_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_tempertaure;
        private System.Windows.Forms.TextBox txb_sensor;
        private System.Windows.Forms.TextBox txb_workNum;
        private System.Windows.Forms.TextBox txb_printPorcess;
        private System.Windows.Forms.TextBox txb_acceptSpace;
        private System.Windows.Forms.TextBox txb_cache;
        private System.Windows.Forms.TextBox txb_dataPort;
        private System.Windows.Forms.TextBox txb_frame;
        private System.Windows.Forms.TextBox txb_porcessError;
        private System.Windows.Forms.TextBox txb_jobIndex;
        private System.Windows.Forms.TextBox txb_dataPorcess;
        private System.Windows.Forms.TextBox txb_error;
        private System.Windows.Forms.TextBox txb_runState;
        private System.Windows.Forms.TextBox txb_outFarme;
        private System.Windows.Forms.TextBox txb_outPutJobnum;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txb_commandText;
        private System.Windows.Forms.Button btn_sure;
        private System.Windows.Forms.ComboBox cmb_command;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Button btn_getFile;
        private System.Windows.Forms.TextBox txb_getFile;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}