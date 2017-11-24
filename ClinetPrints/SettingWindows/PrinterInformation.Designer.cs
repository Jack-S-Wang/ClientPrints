namespace ClinetPrints.SettingWindows
{
    partial class PrinterInformation
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
            this.components = new System.ComponentModel.Container();
            this.lb_showInformation = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lb_showInformation
            // 
            this.lb_showInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_showInformation.Location = new System.Drawing.Point(0, 0);
            this.lb_showInformation.Name = "lb_showInformation";
            this.lb_showInformation.Size = new System.Drawing.Size(54, 26);
            this.lb_showInformation.TabIndex = 0;
            this.lb_showInformation.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PrinterInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(54, 26);
            this.ControlBox = false;
            this.Controls.Add(this.lb_showInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PrinterInformation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.PrinterInformation_Load);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label lb_showInformation;
        public System.Windows.Forms.Timer timer1;
    }
}