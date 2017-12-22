namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    partial class DevPromotion
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txb_commandText = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btn_up = new System.Windows.Forms.Button();
            this.btn_getFile = new System.Windows.Forms.Button();
            this.txb_getFile = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listView1);
            this.groupBox3.Controls.Add(this.txb_commandText);
            this.groupBox3.Controls.Add(this.progressBar1);
            this.groupBox3.Controls.Add(this.btn_up);
            this.groupBox3.Controls.Add(this.btn_getFile);
            this.groupBox3.Controls.Add(this.txb_getFile);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(480, 538);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "固件升级";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(462, 149);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "打印机";
            this.columnHeader1.Width = 458;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 0;
            // 
            // txb_commandText
            // 
            this.txb_commandText.Location = new System.Drawing.Point(22, 329);
            this.txb_commandText.Multiline = true;
            this.txb_commandText.Name = "txb_commandText";
            this.txb_commandText.Size = new System.Drawing.Size(452, 203);
            this.txb_commandText.TabIndex = 5;
            this.txb_commandText.TextChanged += new System.EventHandler(this.txb_commandText_TextChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(22, 285);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(420, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 4;
            // 
            // btn_up
            // 
            this.btn_up.Location = new System.Drawing.Point(367, 245);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(75, 23);
            this.btn_up.TabIndex = 3;
            this.btn_up.Text = "升级";
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // btn_getFile
            // 
            this.btn_getFile.Location = new System.Drawing.Point(367, 197);
            this.btn_getFile.Name = "btn_getFile";
            this.btn_getFile.Size = new System.Drawing.Size(75, 23);
            this.btn_getFile.TabIndex = 2;
            this.btn_getFile.Text = "选择文件";
            this.btn_getFile.UseVisualStyleBackColor = true;
            this.btn_getFile.Click += new System.EventHandler(this.btn_getFile_Click);
            // 
            // txb_getFile
            // 
            this.txb_getFile.Location = new System.Drawing.Point(22, 199);
            this.txb_getFile.Name = "txb_getFile";
            this.txb_getFile.Size = new System.Drawing.Size(311, 21);
            this.txb_getFile.TabIndex = 1;
            // 
            // DevPromotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 562);
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.Name = "DevPromotion";
            this.Text = "固件升级";
            this.Load += new System.EventHandler(this.DevPromotion_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Button btn_getFile;
        private System.Windows.Forms.TextBox txb_getFile;
        private System.Windows.Forms.TextBox txb_commandText;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}