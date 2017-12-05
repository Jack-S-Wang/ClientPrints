namespace ClinetPrints.SettingWindows
{
    partial class RemoveToOther
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoveToOther));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStBtn_back = new System.Windows.Forms.ToolStripButton();
            this.toolStBtn_next = new System.Windows.Forms.ToolStripButton();
            this.toolStBtn_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStBtn_serach = new System.Windows.Forms.ToolStripButton();
            this.toolStTxb_groupName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStBtn_backTop = new System.Windows.Forms.ToolStripButton();
            this.toolStBtn_sure = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStBtn_back,
            this.toolStBtn_next,
            this.toolStBtn_Exit,
            this.toolStBtn_serach,
            this.toolStTxb_groupName,
            this.toolStBtn_backTop,
            this.toolStBtn_sure});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(312, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStBtn_back
            // 
            this.toolStBtn_back.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_back.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_back.Image")));
            this.toolStBtn_back.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_back.Name = "toolStBtn_back";
            this.toolStBtn_back.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_back.Text = "返回上一级";
            this.toolStBtn_back.ToolTipText = "返回上一级";
            this.toolStBtn_back.Click += new System.EventHandler(this.toolStBtn_back_Click);
            // 
            // toolStBtn_next
            // 
            this.toolStBtn_next.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_next.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_next.Image")));
            this.toolStBtn_next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_next.Name = "toolStBtn_next";
            this.toolStBtn_next.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_next.Text = "进入下一级";
            this.toolStBtn_next.Click += new System.EventHandler(this.toolStBtn_next_Click);
            // 
            // toolStBtn_Exit
            // 
            this.toolStBtn_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_Exit.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_Exit.Image")));
            this.toolStBtn_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_Exit.Name = "toolStBtn_Exit";
            this.toolStBtn_Exit.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_Exit.Text = "退出";
            this.toolStBtn_Exit.Click += new System.EventHandler(this.toolStBtn_Exit_Click);
            // 
            // toolStBtn_serach
            // 
            this.toolStBtn_serach.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_serach.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_serach.Image")));
            this.toolStBtn_serach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_serach.Name = "toolStBtn_serach";
            this.toolStBtn_serach.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_serach.Text = "快速查询";
            this.toolStBtn_serach.ToolTipText = "快速模糊式查询";
            this.toolStBtn_serach.Click += new System.EventHandler(this.toolStBtn_serach_Click);
            // 
            // toolStTxb_groupName
            // 
            this.toolStTxb_groupName.Name = "toolStTxb_groupName";
            this.toolStTxb_groupName.Size = new System.Drawing.Size(100, 25);
            this.toolStTxb_groupName.ToolTipText = "输入关键字或全称进行快速查询";
            // 
            // toolStBtn_backTop
            // 
            this.toolStBtn_backTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_backTop.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_backTop.Image")));
            this.toolStBtn_backTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_backTop.Name = "toolStBtn_backTop";
            this.toolStBtn_backTop.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_backTop.Text = "直接回到顶级组";
            this.toolStBtn_backTop.ToolTipText = "直接回到顶级组";
            this.toolStBtn_backTop.Click += new System.EventHandler(this.toolStBtn_backTop_Click);
            // 
            // toolStBtn_sure
            // 
            this.toolStBtn_sure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_sure.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_sure.Image")));
            this.toolStBtn_sure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_sure.Name = "toolStBtn_sure";
            this.toolStBtn_sure.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_sure.Text = "确认添加到该组";
            this.toolStBtn_sure.Click += new System.EventHandler(this.toolStBtn_sure_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 488);
            this.panel1.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(312, 488);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "组名";
            this.columnHeader1.Width = 297;
            // 
            // RemoveToOther
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 513);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RemoveToOther";
            this.Text = "移位选择界面";
            this.Load += new System.EventHandler(this.RemoveToOther_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStBtn_back;
        private System.Windows.Forms.ToolStripButton toolStBtn_next;
        private System.Windows.Forms.ToolStripButton toolStBtn_Exit;
        private System.Windows.Forms.ToolStripButton toolStBtn_serach;
        private System.Windows.Forms.ToolStripTextBox toolStTxb_groupName;
        private System.Windows.Forms.ToolStripButton toolStBtn_backTop;
        private System.Windows.Forms.ToolStripButton toolStBtn_sure;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}