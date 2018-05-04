namespace ClinetPrints.SettingWindows
{
    partial class groupName
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
            this.txb_groupText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txb_groupText
            // 
            this.txb_groupText.Location = new System.Drawing.Point(12, 31);
            this.txb_groupText.Name = "txb_groupText";
            this.txb_groupText.Size = new System.Drawing.Size(247, 21);
            this.txb_groupText.TabIndex = 0;
            this.txb_groupText.Text = "未命名";
            this.txb_groupText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_groupText_KeyPress);
            // 
            // groupName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 80);
            this.Controls.Add(this.txb_groupText);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "groupName";
            this.Text = "添加分组名称";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.groupName_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_groupText;
    }
}