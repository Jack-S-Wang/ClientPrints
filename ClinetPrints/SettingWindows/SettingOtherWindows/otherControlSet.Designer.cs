namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    partial class otherControlSet
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txb_redata = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_findPassword = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_Send = new System.Windows.Forms.Button();
            this.ckb_Hex = new System.Windows.Forms.CheckBox();
            this.txb_data = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Control = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txb_redata);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(9, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 406);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制界面";
            // 
            // txb_redata
            // 
            this.txb_redata.Location = new System.Drawing.Point(13, 160);
            this.txb_redata.Multiline = true;
            this.txb_redata.Name = "txb_redata";
            this.txb_redata.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txb_redata.Size = new System.Drawing.Size(569, 233);
            this.txb_redata.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_findPassword);
            this.groupBox2.Controls.Add(this.btn_clear);
            this.groupBox2.Controls.Add(this.btn_Send);
            this.groupBox2.Controls.Add(this.ckb_Hex);
            this.groupBox2.Controls.Add(this.txb_data);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmb_Control);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(10, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 119);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "指令输入";
            // 
            // btn_findPassword
            // 
            this.btn_findPassword.Location = new System.Drawing.Point(476, 29);
            this.btn_findPassword.Name = "btn_findPassword";
            this.btn_findPassword.Size = new System.Drawing.Size(75, 23);
            this.btn_findPassword.TabIndex = 7;
            this.btn_findPassword.Text = "找回密码";
            this.btn_findPassword.UseVisualStyleBackColor = true;
            this.btn_findPassword.Click += new System.EventHandler(this.btn_findPassword_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(379, 29);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 6;
            this.btn_clear.Text = "清除";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(265, 29);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 23);
            this.btn_Send.TabIndex = 5;
            this.btn_Send.Text = "发送";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // ckb_Hex
            // 
            this.ckb_Hex.AutoSize = true;
            this.ckb_Hex.Location = new System.Drawing.Point(524, 74);
            this.ckb_Hex.Name = "ckb_Hex";
            this.ckb_Hex.Size = new System.Drawing.Size(42, 16);
            this.ckb_Hex.TabIndex = 4;
            this.ckb_Hex.Text = "Hex";
            this.ckb_Hex.UseVisualStyleBackColor = true;
            // 
            // txb_data
            // 
            this.txb_data.Location = new System.Drawing.Point(59, 72);
            this.txb_data.Name = "txb_data";
            this.txb_data.Size = new System.Drawing.Size(451, 21);
            this.txb_data.TabIndex = 3;
            this.txb_data.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_data_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据";
            // 
            // cmb_Control
            // 
            this.cmb_Control.FormattingEnabled = true;
            this.cmb_Control.Location = new System.Drawing.Point(59, 31);
            this.cmb_Control.Name = "cmb_Control";
            this.cmb_Control.Size = new System.Drawing.Size(141, 20);
            this.cmb_Control.TabIndex = 1;
            this.cmb_Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_Control_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "指令";
            // 
            // otherControlSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 428);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "otherControlSet";
            this.Text = "其它指令控制";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.otherControlSet_FormClosing);
            this.Load += new System.EventHandler(this.otherControlSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckb_Hex;
        private System.Windows.Forms.TextBox txb_data;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Control;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_redata;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.Button btn_findPassword;
    }
}