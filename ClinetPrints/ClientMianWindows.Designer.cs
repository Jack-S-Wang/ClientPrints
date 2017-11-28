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
            this.printerViewFlcok = new System.Windows.Forms.TreeView();
            this.printerViewSingle = new System.Windows.Forms.TreeView();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.单台打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.群打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pan_mianWin1_image = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_preview = new System.Windows.Forms.Button();
            this.btn_SelectImage = new System.Windows.Forms.Button();
            this.txb_pathImage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pB_image = new System.Windows.Forms.PictureBox();
            this.pan_mainWin1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.pan_mainWin1_tree.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.pan_mianWin1_image.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_image)).BeginInit();
            this.pan_mainWin1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.快速查询ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1263, 25);
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
            this.pan_mainWin1_tree.Controls.Add(this.printerViewFlcok);
            this.pan_mainWin1_tree.Controls.Add(this.printerViewSingle);
            this.pan_mainWin1_tree.Controls.Add(this.menuStrip2);
            this.pan_mainWin1_tree.Dock = System.Windows.Forms.DockStyle.Left;
            this.pan_mainWin1_tree.Location = new System.Drawing.Point(0, 0);
            this.pan_mainWin1_tree.Name = "pan_mainWin1_tree";
            this.pan_mainWin1_tree.Size = new System.Drawing.Size(209, 572);
            this.pan_mainWin1_tree.TabIndex = 1;
            // 
            // printerViewFlcok
            // 
            this.printerViewFlcok.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printerViewFlcok.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.printerViewFlcok.Location = new System.Drawing.Point(0, 25);
            this.printerViewFlcok.Name = "printerViewFlcok";
            this.printerViewFlcok.Size = new System.Drawing.Size(209, 547);
            this.printerViewFlcok.TabIndex = 2;
            this.printerViewFlcok.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.printerViewFlcok_AfterSelect);
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
            this.pan_mianWin1_image.Size = new System.Drawing.Size(1054, 572);
            this.pan_mianWin1_image.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.pB_image);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1054, 572);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印界面";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_preview);
            this.groupBox2.Controls.Add(this.btn_SelectImage);
            this.groupBox2.Controls.Add(this.txb_pathImage);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 487);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1048, 82);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "控制界面";
            // 
            // btn_preview
            // 
            this.btn_preview.AutoSize = true;
            this.btn_preview.Location = new System.Drawing.Point(712, 38);
            this.btn_preview.Name = "btn_preview";
            this.btn_preview.Size = new System.Drawing.Size(75, 23);
            this.btn_preview.TabIndex = 3;
            this.btn_preview.Text = "预览";
            this.btn_preview.UseVisualStyleBackColor = true;
            // 
            // btn_SelectImage
            // 
            this.btn_SelectImage.AutoSize = true;
            this.btn_SelectImage.Location = new System.Drawing.Point(569, 38);
            this.btn_SelectImage.Name = "btn_SelectImage";
            this.btn_SelectImage.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectImage.TabIndex = 2;
            this.btn_SelectImage.Text = "选择";
            this.btn_SelectImage.UseVisualStyleBackColor = true;
            this.btn_SelectImage.Click += new System.EventHandler(this.btn_SelectImage_Click);
            // 
            // txb_pathImage
            // 
            this.txb_pathImage.Location = new System.Drawing.Point(107, 40);
            this.txb_pathImage.Name = "txb_pathImage";
            this.txb_pathImage.Size = new System.Drawing.Size(409, 21);
            this.txb_pathImage.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(36, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择图片：";
            // 
            // pB_image
            // 
            this.pB_image.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pB_image.BackColor = System.Drawing.Color.Gray;
            this.pB_image.Location = new System.Drawing.Point(17, 29);
            this.pB_image.Name = "pB_image";
            this.pB_image.Size = new System.Drawing.Size(1025, 452);
            this.pB_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pB_image.TabIndex = 0;
            this.pB_image.TabStop = false;
            // 
            // pan_mainWin1
            // 
            this.pan_mainWin1.Controls.Add(this.pan_mianWin1_image);
            this.pan_mainWin1.Controls.Add(this.pan_mainWin1_tree);
            this.pan_mainWin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_mainWin1.Location = new System.Drawing.Point(0, 25);
            this.pan_mainWin1.Name = "pan_mainWin1";
            this.pan_mainWin1.Size = new System.Drawing.Size(1263, 572);
            this.pan_mainWin1.TabIndex = 3;
            // 
            // ClientMianWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 597);
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
            ((System.ComponentModel.ISupportInitialize)(this.pB_image)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pB_image;
        private System.Windows.Forms.Button btn_preview;
        private System.Windows.Forms.Button btn_SelectImage;
        private System.Windows.Forms.TextBox txb_pathImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pan_mainWin1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 单台打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 群打印ToolStripMenuItem;
        public System.Windows.Forms.TreeView printerViewFlcok;
        public System.Windows.Forms.TreeView printerViewSingle;
    }
}

