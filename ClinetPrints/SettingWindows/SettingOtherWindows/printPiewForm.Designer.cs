namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    partial class printPiewForm
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
            this.printPiewControl1 = new ClinetPrints.CreatContorl.printPiewControl();
            this.SuspendLayout();
            // 
            // printPiewControl1
            // 
            this.printPiewControl1.AutoScroll = true;
            this.printPiewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPiewControl1.fileAddress = null;
            this.printPiewControl1.Location = new System.Drawing.Point(0, 0);
            this.printPiewControl1.Name = "printPiewControl1";
            this.printPiewControl1.page = "";
            this.printPiewControl1.PrinterObject = null;
            this.printPiewControl1.Size = new System.Drawing.Size(1165, 603);
            this.printPiewControl1.TabIndex = 0;
            // 
            // printPiewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 603);
            this.Controls.Add(this.printPiewControl1);
            this.Name = "printPiewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "打印预览";
            this.Load += new System.EventHandler(this.printPiewForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CreatContorl.printPiewControl printPiewControl1;
    }
}