namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    partial class printNumber
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
            this.button1 = new System.Windows.Forms.Button();
            this.ckb_allOrSing = new System.Windows.Forms.CheckBox();
            this.UpDown_num = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_num)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.ckb_allOrSing);
            this.panel1.Controls.Add(this.UpDown_num);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 123);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ckb_allOrSing
            // 
            this.ckb_allOrSing.AutoSize = true;
            this.ckb_allOrSing.ForeColor = System.Drawing.Color.Red;
            this.ckb_allOrSing.Location = new System.Drawing.Point(18, 75);
            this.ckb_allOrSing.Name = "ckb_allOrSing";
            this.ckb_allOrSing.Size = new System.Drawing.Size(168, 16);
            this.ckb_allOrSing.TabIndex = 2;
            this.ckb_allOrSing.Text = "是否要设置所有的打印任务";
            this.ckb_allOrSing.UseVisualStyleBackColor = true;
            this.ckb_allOrSing.CheckedChanged += new System.EventHandler(this.ckb_allOrSing_CheckedChanged);
            // 
            // UpDown_num
            // 
            this.UpDown_num.Location = new System.Drawing.Point(63, 31);
            this.UpDown_num.Name = "UpDown_num";
            this.UpDown_num.Size = new System.Drawing.Size(152, 21);
            this.UpDown_num.TabIndex = 1;
            this.UpDown_num.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数量：";
            // 
            // printNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 153);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "printNumber";
            this.Text = "打印数量";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.printNumber_FormClosing);
            this.Load += new System.EventHandler(this.printNumber_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_num)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown UpDown_num;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox ckb_allOrSing;
    }
}