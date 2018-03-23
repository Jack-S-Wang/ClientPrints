namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    partial class importPassWord
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
            this.btn_sure = new System.Windows.Forms.Button();
            this.txb_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_findPassword = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_findPassword);
            this.groupBox1.Controls.Add(this.btn_sure);
            this.groupBox1.Controls.Add(this.txb_password);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 141);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制界面";
            // 
            // btn_sure
            // 
            this.btn_sure.Location = new System.Drawing.Point(73, 90);
            this.btn_sure.Name = "btn_sure";
            this.btn_sure.Size = new System.Drawing.Size(75, 23);
            this.btn_sure.TabIndex = 2;
            this.btn_sure.Text = "确认";
            this.btn_sure.UseVisualStyleBackColor = true;
            this.btn_sure.Click += new System.EventHandler(this.btn_sure_Click);
            // 
            // txb_password
            // 
            this.txb_password.Location = new System.Drawing.Point(73, 47);
            this.txb_password.Name = "txb_password";
            this.txb_password.PasswordChar = '*';
            this.txb_password.Size = new System.Drawing.Size(177, 21);
            this.txb_password.TabIndex = 1;
            this.txb_password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "密码：";
            // 
            // btn_findPassword
            // 
            this.btn_findPassword.Location = new System.Drawing.Point(154, 90);
            this.btn_findPassword.Name = "btn_findPassword";
            this.btn_findPassword.Size = new System.Drawing.Size(96, 23);
            this.btn_findPassword.TabIndex = 3;
            this.btn_findPassword.Text = "直接找回密码";
            this.btn_findPassword.UseVisualStyleBackColor = true;
            this.btn_findPassword.Click += new System.EventHandler(this.btn_findPassword_Click);
            // 
            // importPassWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 158);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "importPassWord";
            this.Text = "输入密码";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txb_password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_sure;
        private System.Windows.Forms.Button btn_findPassword;
    }
}