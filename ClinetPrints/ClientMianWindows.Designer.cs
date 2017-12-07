namespace ClinetPrints
{
    partial class ClientMianWindows
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientMianWindows));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.快速查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分组名称查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部展开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部折叠ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pan_mainWin1_tree = new System.Windows.Forms.Panel();
            this.printerViewFlock = new System.Windows.Forms.TreeView();
            this.printerViewSingle = new System.Windows.Forms.TreeView();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.单台打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.群打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pan_mianWin1_image = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStBtn_add = new System.Windows.Forms.ToolStripButton();
            this.toolStBtn_delete = new System.Windows.Forms.ToolStripButton();
            this.toolStbtn_moveUp = new System.Windows.Forms.ToolStripButton();
            this.toolStBtn_moveNext = new System.Windows.Forms.ToolStripButton();
            this.toolStBtn_monitor = new System.Windows.Forms.ToolStripButton();
            this.toolStTxb_printer = new System.Windows.Forms.ToolStripTextBox();
            this.toolStBtn_print = new System.Windows.Forms.ToolStripButton();
            this.pan_mainWin1 = new System.Windows.Forms.Panel();
            this.imageSubItems = new System.Windows.Forms.ImageList(this.components);
            this.toolStBtn_printPerview = new System.Windows.Forms.ToolStripButton();
            this.toolStBtn_parmSet = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.pan_mainWin1_tree.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.pan_mianWin1_image.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pan_mainWin1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.快速查询ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(843, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 快速查询ToolStripMenuItem
            // 
            this.快速查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.分组名称查询ToolStripMenuItem,
            this.全部展开ToolStripMenuItem,
            this.全部折叠ToolStripMenuItem});
            this.快速查询ToolStripMenuItem.Name = "快速查询ToolStripMenuItem";
            this.快速查询ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.快速查询ToolStripMenuItem.Text = "快速查询";
            // 
            // 分组名称查询ToolStripMenuItem
            // 
            this.分组名称查询ToolStripMenuItem.Name = "分组名称查询ToolStripMenuItem";
            this.分组名称查询ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.分组名称查询ToolStripMenuItem.Text = "分组名称查询";
            this.分组名称查询ToolStripMenuItem.Click += new System.EventHandler(this.分组名称查询ToolStripMenuItem_Click);
            // 
            // 全部展开ToolStripMenuItem
            // 
            this.全部展开ToolStripMenuItem.Name = "全部展开ToolStripMenuItem";
            this.全部展开ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.全部展开ToolStripMenuItem.Text = "全部展开";
            this.全部展开ToolStripMenuItem.Click += new System.EventHandler(this.全部展开ToolStripMenuItem_Click);
            // 
            // 全部折叠ToolStripMenuItem
            // 
            this.全部折叠ToolStripMenuItem.Name = "全部折叠ToolStripMenuItem";
            this.全部折叠ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.全部折叠ToolStripMenuItem.Text = "全部折叠";
            this.全部折叠ToolStripMenuItem.Click += new System.EventHandler(this.全部折叠ToolStripMenuItem_Click);
            // 
            // pan_mainWin1_tree
            // 
            this.pan_mainWin1_tree.Controls.Add(this.printerViewFlock);
            this.pan_mainWin1_tree.Controls.Add(this.printerViewSingle);
            this.pan_mainWin1_tree.Controls.Add(this.menuStrip2);
            this.pan_mainWin1_tree.Dock = System.Windows.Forms.DockStyle.Left;
            this.pan_mainWin1_tree.Location = new System.Drawing.Point(0, 0);
            this.pan_mainWin1_tree.Name = "pan_mainWin1_tree";
            this.pan_mainWin1_tree.Size = new System.Drawing.Size(209, 572);
            this.pan_mainWin1_tree.TabIndex = 1;
            // 
            // printerViewFlock
            // 
            this.printerViewFlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printerViewFlock.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.printerViewFlock.Location = new System.Drawing.Point(0, 25);
            this.printerViewFlock.Name = "printerViewFlock";
            this.printerViewFlock.Size = new System.Drawing.Size(209, 547);
            this.printerViewFlock.TabIndex = 2;
            // 
            // printerViewSingle
            // 
            this.printerViewSingle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printerViewSingle.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.printerViewSingle.Location = new System.Drawing.Point(0, 25);
            this.printerViewSingle.Name = "printerViewSingle";
            this.printerViewSingle.Size = new System.Drawing.Size(209, 547);
            this.printerViewSingle.TabIndex = 0;
            this.printerViewSingle.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.printerViewSingle_AfterSelect);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.单台打印ToolStripMenuItem,
            this.群打印ToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(209, 25);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // 单台打印ToolStripMenuItem
            // 
            this.单台打印ToolStripMenuItem.Name = "单台打印ToolStripMenuItem";
            this.单台打印ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.单台打印ToolStripMenuItem.Text = "单台打印";
            this.单台打印ToolStripMenuItem.Click += new System.EventHandler(this.单台打印ToolStripMenuItem_Click);
            // 
            // 群打印ToolStripMenuItem
            // 
            this.群打印ToolStripMenuItem.Name = "群打印ToolStripMenuItem";
            this.群打印ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.群打印ToolStripMenuItem.Text = "群打印";
            this.群打印ToolStripMenuItem.Click += new System.EventHandler(this.群打印ToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pan_mianWin1_image
            // 
            this.pan_mianWin1_image.Controls.Add(this.groupBox1);
            this.pan_mianWin1_image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_mianWin1_image.Location = new System.Drawing.Point(209, 0);
            this.pan_mianWin1_image.Name = "pan_mianWin1_image";
            this.pan_mianWin1_image.Size = new System.Drawing.Size(634, 572);
            this.pan_mianWin1_image.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 572);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制界面";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(3, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(614, 552);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "打印设置";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(3, 42);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(608, 507);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "图片";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "作业号";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "图片路径";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 200;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStBtn_add,
            this.toolStBtn_delete,
            this.toolStbtn_moveUp,
            this.toolStBtn_moveNext,
            this.toolStBtn_monitor,
            this.toolStTxb_printer,
            this.toolStBtn_print,
            this.toolStBtn_printPerview,
            this.toolStBtn_parmSet});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(608, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStBtn_add
            // 
            this.toolStBtn_add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_add.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_add.Image")));
            this.toolStBtn_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_add.Name = "toolStBtn_add";
            this.toolStBtn_add.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_add.Text = "添加";
            this.toolStBtn_add.ToolTipText = "添加作业";
            this.toolStBtn_add.Click += new System.EventHandler(this.toolStBtn_add_Click);
            // 
            // toolStBtn_delete
            // 
            this.toolStBtn_delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_delete.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_delete.Image")));
            this.toolStBtn_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_delete.Name = "toolStBtn_delete";
            this.toolStBtn_delete.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_delete.Text = "删除";
            this.toolStBtn_delete.ToolTipText = "删除作业";
            this.toolStBtn_delete.Click += new System.EventHandler(this.toolStBtn_delete_Click);
            // 
            // toolStbtn_moveUp
            // 
            this.toolStbtn_moveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStbtn_moveUp.Image = ((System.Drawing.Image)(resources.GetObject("toolStbtn_moveUp.Image")));
            this.toolStbtn_moveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStbtn_moveUp.Name = "toolStbtn_moveUp";
            this.toolStbtn_moveUp.Size = new System.Drawing.Size(23, 22);
            this.toolStbtn_moveUp.Text = "置前";
            this.toolStbtn_moveUp.ToolTipText = "作业置前";
            this.toolStbtn_moveUp.Click += new System.EventHandler(this.toolStbtn_moveUp_Click);
            // 
            // toolStBtn_moveNext
            // 
            this.toolStBtn_moveNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_moveNext.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_moveNext.Image")));
            this.toolStBtn_moveNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_moveNext.Name = "toolStBtn_moveNext";
            this.toolStBtn_moveNext.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_moveNext.Text = "置后";
            this.toolStBtn_moveNext.ToolTipText = "作业置后";
            this.toolStBtn_moveNext.Click += new System.EventHandler(this.toolStBtn_moveNext_Click);
            // 
            // toolStBtn_monitor
            // 
            this.toolStBtn_monitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_monitor.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_monitor.Image")));
            this.toolStBtn_monitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_monitor.Name = "toolStBtn_monitor";
            this.toolStBtn_monitor.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_monitor.Text = "监控打印机";
            this.toolStBtn_monitor.ToolTipText = "实时监控控制打印机";
            this.toolStBtn_monitor.Click += new System.EventHandler(this.toolStBtn_monitor_Click);
            // 
            // toolStTxb_printer
            // 
            this.toolStTxb_printer.Name = "toolStTxb_printer";
            this.toolStTxb_printer.Size = new System.Drawing.Size(100, 25);
            this.toolStTxb_printer.ToolTipText = "选择的打印机名称";
            // 
            // toolStBtn_print
            // 
            this.toolStBtn_print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_print.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_print.Image")));
            this.toolStBtn_print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_print.Name = "toolStBtn_print";
            this.toolStBtn_print.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_print.Text = "打印";
            this.toolStBtn_print.Click += new System.EventHandler(this.toolStBtn_print_Click);
            // 
            // pan_mainWin1
            // 
            this.pan_mainWin1.Controls.Add(this.pan_mianWin1_image);
            this.pan_mainWin1.Controls.Add(this.pan_mainWin1_tree);
            this.pan_mainWin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_mainWin1.Location = new System.Drawing.Point(0, 25);
            this.pan_mainWin1.Name = "pan_mainWin1";
            this.pan_mainWin1.Size = new System.Drawing.Size(843, 572);
            this.pan_mainWin1.TabIndex = 3;
            // 
            // imageSubItems
            // 
            this.imageSubItems.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageSubItems.ImageSize = new System.Drawing.Size(16, 16);
            this.imageSubItems.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStBtn_printPerview
            // 
            this.toolStBtn_printPerview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_printPerview.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_printPerview.Image")));
            this.toolStBtn_printPerview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_printPerview.Name = "toolStBtn_printPerview";
            this.toolStBtn_printPerview.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_printPerview.Text = "打印预览";
            // 
            // toolStBtn_parmSet
            // 
            this.toolStBtn_parmSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStBtn_parmSet.Image = ((System.Drawing.Image)(resources.GetObject("toolStBtn_parmSet.Image")));
            this.toolStBtn_parmSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStBtn_parmSet.Name = "toolStBtn_parmSet";
            this.toolStBtn_parmSet.Size = new System.Drawing.Size(23, 22);
            this.toolStBtn_parmSet.Text = "参数设置";
            this.toolStBtn_parmSet.Click += new System.EventHandler(this.toolStBtn_parmSet_Click);
            // 
            // ClientMianWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 597);
            this.Controls.Add(this.pan_mainWin1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ClientMianWindows";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户端打印控制";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientMianWindows_FormClosing);
            this.Load += new System.EventHandler(this.ClientMianWindows_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pan_mainWin1_tree.ResumeLayout(false);
            this.pan_mainWin1_tree.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.pan_mianWin1_image.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pan_mainWin1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 快速查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分组名称查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部展开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部折叠ToolStripMenuItem;
        private System.Windows.Forms.Panel pan_mainWin1_tree;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel pan_mianWin1_image;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pan_mainWin1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 单台打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 群打印ToolStripMenuItem;
        public System.Windows.Forms.TreeView printerViewFlock;
        public System.Windows.Forms.TreeView printerViewSingle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStBtn_add;
        private System.Windows.Forms.ToolStripButton toolStBtn_delete;
        private System.Windows.Forms.ToolStripButton toolStbtn_moveUp;
        private System.Windows.Forms.ToolStripButton toolStBtn_moveNext;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ToolStripButton toolStBtn_monitor;
        private System.Windows.Forms.ToolStripTextBox toolStTxb_printer;
        private System.Windows.Forms.ImageList imageSubItems;
        private System.Windows.Forms.ToolStripButton toolStBtn_print;
        private System.Windows.Forms.ToolStripButton toolStBtn_printPerview;
        private System.Windows.Forms.ToolStripButton toolStBtn_parmSet;
    }
}

