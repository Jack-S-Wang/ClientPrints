namespace ClinetPrints.CreatContorl
{
    partial class printPiewControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(printPiewControl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_printWipe = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_custom = new System.Windows.Forms.Button();
            this.txb_customPage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_page = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolBtn_print = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_save = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_Add = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_clear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolCob_Intgaiting = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtn_reMap = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_close = new System.Windows.Forms.ToolStripButton();
            this.panControlWheel1 = new ClinetPrints.CreatContorl.PanControlWheel();
            this.ptb_page = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panControlWheel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_page)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1155, 657);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panControlWheel1);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1155, 657);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印预览设置界面";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 67);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(274, 587);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmb_printWipe);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btn_custom);
            this.groupBox2.Controls.Add(this.txb_customPage);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmb_page);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(274, 587);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择纸张大小";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "毫米";
            // 
            // cmb_printWipe
            // 
            this.cmb_printWipe.FormattingEnabled = true;
            this.cmb_printWipe.Items.AddRange(new object[] {
            "不擦除背景",
            "卡片全部擦除",
            "按前景位图大小擦除",
            "按用户自定的位图擦除",
            "打印且擦除",
            "只擦除",
            "只打印"});
            this.cmb_printWipe.Location = new System.Drawing.Point(28, 331);
            this.cmb_printWipe.Name = "cmb_printWipe";
            this.cmb_printWipe.Size = new System.Drawing.Size(121, 20);
            this.cmb_printWipe.TabIndex = 6;
            this.cmb_printWipe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_printWipe_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "打印擦除设置：";
            // 
            // btn_custom
            // 
            this.btn_custom.Location = new System.Drawing.Point(178, 193);
            this.btn_custom.Name = "btn_custom";
            this.btn_custom.Size = new System.Drawing.Size(75, 23);
            this.btn_custom.TabIndex = 4;
            this.btn_custom.Text = "设置自定义";
            this.btn_custom.UseVisualStyleBackColor = true;
            this.btn_custom.Click += new System.EventHandler(this.btn_custom_Click);
            // 
            // txb_customPage
            // 
            this.txb_customPage.Enabled = false;
            this.txb_customPage.Location = new System.Drawing.Point(28, 195);
            this.txb_customPage.Name = "txb_customPage";
            this.txb_customPage.Size = new System.Drawing.Size(100, 21);
            this.txb_customPage.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "自定义纸张大小：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "纸张大小：";
            // 
            // cmb_page
            // 
            this.cmb_page.FormattingEnabled = true;
            this.cmb_page.Location = new System.Drawing.Point(28, 101);
            this.cmb_page.Name = "cmb_page";
            this.cmb_page.Size = new System.Drawing.Size(121, 20);
            this.cmb_page.TabIndex = 0;
            this.cmb_page.SelectedIndexChanged += new System.EventHandler(this.cmb_page_SelectedIndexChanged);
            this.cmb_page.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_page_KeyPress);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.DarkGray;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtn_print,
            this.toolBtn_save,
            this.toolBtn_Add,
            this.toolBtn_clear,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.toolCob_Intgaiting,
            this.toolStripSeparator3,
            this.toolBtn_reMap,
            this.toolBtn_close});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1149, 50);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolBtn_print
            // 
            this.toolBtn_print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtn_print.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolBtn_print.Image = ((System.Drawing.Image)(resources.GetObject("toolBtn_print.Image")));
            this.toolBtn_print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_print.Name = "toolBtn_print";
            this.toolBtn_print.Size = new System.Drawing.Size(23, 47);
            this.toolBtn_print.Text = "打印";
            this.toolBtn_print.ToolTipText = "按修改后的样子进行直接打印";
            this.toolBtn_print.Click += new System.EventHandler(this.toolBtn_print_Click);
            // 
            // toolBtn_save
            // 
            this.toolBtn_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtn_save.Image = ((System.Drawing.Image)(resources.GetObject("toolBtn_save.Image")));
            this.toolBtn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_save.Name = "toolBtn_save";
            this.toolBtn_save.Size = new System.Drawing.Size(23, 47);
            this.toolBtn_save.Text = "保存";
            this.toolBtn_save.ToolTipText = "另存为";
            this.toolBtn_save.Click += new System.EventHandler(this.toolBtn_save_Click);
            // 
            // toolBtn_Add
            // 
            this.toolBtn_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtn_Add.Image = ((System.Drawing.Image)(resources.GetObject("toolBtn_Add.Image")));
            this.toolBtn_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_Add.Name = "toolBtn_Add";
            this.toolBtn_Add.Size = new System.Drawing.Size(23, 47);
            this.toolBtn_Add.Text = "添加自定义纸张大小";
            this.toolBtn_Add.Click += new System.EventHandler(this.toolBtn_Add_Click);
            // 
            // toolBtn_clear
            // 
            this.toolBtn_clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtn_clear.Image = ((System.Drawing.Image)(resources.GetObject("toolBtn_clear.Image")));
            this.toolBtn_clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_clear.Name = "toolBtn_clear";
            this.toolBtn_clear.Size = new System.Drawing.Size(23, 47);
            this.toolBtn_clear.Text = "删除所选尺寸";
            this.toolBtn_clear.Click += new System.EventHandler(this.toolBtn_clear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(68, 47);
            this.toolStripLabel3.Text = "图片契合度";
            // 
            // toolCob_Intgaiting
            // 
            this.toolCob_Intgaiting.Items.AddRange(new object[] {
            "填充",
            "适应",
            "拉伸",
            "平铺",
            "居中",
            "正常"});
            this.toolCob_Intgaiting.Name = "toolCob_Intgaiting";
            this.toolCob_Intgaiting.Size = new System.Drawing.Size(121, 50);
            this.toolCob_Intgaiting.SelectedIndexChanged += new System.EventHandler(this.toolCob_Intgaiting_SelectedIndexChanged);
            this.toolCob_Intgaiting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolCob_Intgaiting_KeyPress);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 50);
            // 
            // toolBtn_reMap
            // 
            this.toolBtn_reMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtn_reMap.Image = ((System.Drawing.Image)(resources.GetObject("toolBtn_reMap.Image")));
            this.toolBtn_reMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_reMap.Name = "toolBtn_reMap";
            this.toolBtn_reMap.Size = new System.Drawing.Size(23, 47);
            this.toolBtn_reMap.Text = "复位";
            this.toolBtn_reMap.Click += new System.EventHandler(this.toolBtn_reMap_Click);
            // 
            // toolBtn_close
            // 
            this.toolBtn_close.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtn_close.Image = ((System.Drawing.Image)(resources.GetObject("toolBtn_close.Image")));
            this.toolBtn_close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_close.Name = "toolBtn_close";
            this.toolBtn_close.Size = new System.Drawing.Size(23, 47);
            this.toolBtn_close.Text = "退出";
            this.toolBtn_close.Click += new System.EventHandler(this.toolBtn_close_Click);
            // 
            // panControlWheel1
            // 
            this.panControlWheel1.AutoScroll = true;
            this.panControlWheel1.Controls.Add(this.ptb_page);
            this.panControlWheel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panControlWheel1.Location = new System.Drawing.Point(277, 67);
            this.panControlWheel1.Name = "panControlWheel1";
            this.panControlWheel1.Size = new System.Drawing.Size(875, 587);
            this.panControlWheel1.TabIndex = 2;
            // 
            // ptb_page
            // 
            this.ptb_page.BackColor = System.Drawing.SystemColors.Window;
            this.ptb_page.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ptb_page.Location = new System.Drawing.Point(4, 2);
            this.ptb_page.Name = "ptb_page";
            this.ptb_page.Size = new System.Drawing.Size(858, 577);
            this.ptb_page.TabIndex = 1;
            this.ptb_page.TabStop = false;
            this.ptb_page.WaitOnLoad = true;
            this.ptb_page.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ptb_page_MouseDown);
            this.ptb_page.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ptb_page_MouseUp);
            // 
            // printPiewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "printPiewControl";
            this.Size = new System.Drawing.Size(1155, 657);
            this.Load += new System.EventHandler(this.printPiewControl_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panControlWheel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptb_page)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_page;
        private System.Windows.Forms.Button btn_custom;
        private System.Windows.Forms.TextBox txb_customPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox toolCob_Intgaiting;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ComboBox cmb_printWipe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton toolBtn_Add;
        private System.Windows.Forms.ToolStripButton toolBtn_clear;
        private System.Windows.Forms.ToolStripButton toolBtn_save;
        private System.Windows.Forms.ToolStripButton toolBtn_reMap;
        private System.Windows.Forms.ToolStripButton toolBtn_close;
        private System.Windows.Forms.ToolStripButton toolBtn_print;
        private System.Windows.Forms.Label label4;
        private PanControlWheel panControlWheel1;
        private System.Windows.Forms.PictureBox ptb_page;
    }
}
