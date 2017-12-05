namespace ClinetPrints.SettingWindows
{
    partial class RemoveIndex
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
            this.label1 = new System.Windows.Forms.Label();
            this.txb_index = new System.Windows.Forms.TextBox();
            this.btn_sure = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_sure);
            this.panel1.Controls.Add(this.txb_index);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 97);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "作业号";
            // 
            // txb_index
            // 
            this.txb_index.Location = new System.Drawing.Point(85, 43);
            this.txb_index.Name = "txb_index";
            this.txb_index.Size = new System.Drawing.Size(100, 21);
            this.txb_index.TabIndex = 1;
            this.txb_index.Text = "1";
            this.txb_index.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_index_KeyPress);
            // 
            // btn_sure
            // 
            this.btn_sure.Location = new System.Drawing.Point(241, 41);
            this.btn_sure.Name = "btn_sure";
            this.btn_sure.Size = new System.Drawing.Size(75, 23);
            this.btn_sure.TabIndex = 2;
            this.btn_sure.Text = "确认";
            this.btn_sure.UseVisualStyleBackColor = true;
            this.btn_sure.Click += new System.EventHandler(this.btn_sure_Click);
            // 
            // RemoveIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 118);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "RemoveIndex";
            this.Load += new System.EventHandler(this.RemoveIndex_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_sure;
        private System.Windows.Forms.TextBox txb_index;
        private System.Windows.Forms.Label label1;
    }
}