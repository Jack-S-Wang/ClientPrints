namespace ClinetPrints.SettingWindows.SettingOtherWindows
{
    partial class setJsonKey
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.data_View = new System.Windows.Forms.DataGridView();
            this.btn_Load = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.chk_entpy = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_fileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.jsonKeySaveBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.keyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.showNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jsonKeyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jsonKeySaveBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(11, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(431, 591);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txb_fileName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chk_entpy);
            this.groupBox2.Controls.Add(this.btn_save);
            this.groupBox2.Controls.Add(this.btn_Load);
            this.groupBox2.Location = new System.Drawing.Point(9, 455);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(419, 126);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "控制";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.data_View);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 439);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "列表";
            // 
            // data_View
            // 
            this.data_View.AutoGenerateColumns = false;
            this.data_View.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_View.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.keyNameDataGridViewTextBoxColumn,
            this.showNameDataGridViewTextBoxColumn,
            this.jsonKeyNameDataGridViewTextBoxColumn});
            this.data_View.DataSource = this.jsonKeySaveBindingSource;
            this.data_View.Location = new System.Drawing.Point(6, 20);
            this.data_View.Name = "data_View";
            this.data_View.RowTemplate.Height = 23;
            this.data_View.Size = new System.Drawing.Size(406, 413);
            this.data_View.TabIndex = 0;
            // 
            // btn_Load
            // 
            this.btn_Load.Location = new System.Drawing.Point(27, 84);
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.Size = new System.Drawing.Size(75, 23);
            this.btn_Load.TabIndex = 0;
            this.btn_Load.Text = "导入";
            this.btn_Load.UseVisualStyleBackColor = true;
            this.btn_Load.Click += new System.EventHandler(this.btn_Load_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(166, 84);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // chk_entpy
            // 
            this.chk_entpy.AutoSize = true;
            this.chk_entpy.Location = new System.Drawing.Point(290, 88);
            this.chk_entpy.Name = "chk_entpy";
            this.chk_entpy.Size = new System.Drawing.Size(102, 16);
            this.chk_entpy.TabIndex = 2;
            this.chk_entpy.Text = "是否是cfg类型";
            this.chk_entpy.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "文件名称：";
            // 
            // txb_fileName
            // 
            this.txb_fileName.Location = new System.Drawing.Point(110, 33);
            this.txb_fileName.Name = "txb_fileName";
            this.txb_fileName.Size = new System.Drawing.Size(131, 21);
            this.txb_fileName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(274, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "*无需后辍名";
            // 
            // jsonKeySaveBindingSource
            // 
            this.jsonKeySaveBindingSource.DataMember = "list";
            this.jsonKeySaveBindingSource.DataSource = typeof(ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass.jsonKeySave);
            // 
            // keyNameDataGridViewTextBoxColumn
            // 
            this.keyNameDataGridViewTextBoxColumn.DataPropertyName = "keyName";
            this.keyNameDataGridViewTextBoxColumn.HeaderText = "记录的键值";
            this.keyNameDataGridViewTextBoxColumn.Name = "keyNameDataGridViewTextBoxColumn";
            // 
            // showNameDataGridViewTextBoxColumn
            // 
            this.showNameDataGridViewTextBoxColumn.DataPropertyName = "showName";
            this.showNameDataGridViewTextBoxColumn.HeaderText = "界面显示的名称";
            this.showNameDataGridViewTextBoxColumn.Name = "showNameDataGridViewTextBoxColumn";
            this.showNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // jsonKeyNameDataGridViewTextBoxColumn
            // 
            this.jsonKeyNameDataGridViewTextBoxColumn.DataPropertyName = "jsonKeyName";
            this.jsonKeyNameDataGridViewTextBoxColumn.HeaderText = "json对应键值名称";
            this.jsonKeyNameDataGridViewTextBoxColumn.Name = "jsonKeyNameDataGridViewTextBoxColumn";
            this.jsonKeyNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // setJsonKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 612);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "setJsonKey";
            this.Text = "json格式键值添加界面";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data_View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jsonKeySaveBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView data_View;
        private System.Windows.Forms.Button btn_Load;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.CheckBox chk_entpy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_fileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource jsonKeySaveBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn showNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn jsonKeyNameDataGridViewTextBoxColumn;
    }
}