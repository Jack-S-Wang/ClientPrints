using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using static ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass.printPiewControlXml;

namespace ClinetPrints.CreatContorl
{
    public partial class printPiewControl : UserControl
    {
        public printPiewControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 添加打印预览控件
        /// </summary>
        /// <param name="page">纸张大小；500X600格式</param>
        /// <param name="fileAddress">图片路径</param>
        public printPiewControl(string page,string fileAddress)
        {
            InitializeComponent();
            this.page = page;
            this.fileAddress = fileAddress;
        }
        [Description("打印机对象,使用时可传入")]
        public object PrinterObject { get; set; }
        [Description("图片路径地址")]
        public string fileAddress { get; set; }
        [Description("纸张大小，列：500x600；宽乘高")]
        public string page
        {
            get { return _page; }
            set
            {
                _page = value;
                if (_page == null)
                {
                    _page = "";
                }
            }
        }
        [Browsable(false)]
        private string _page;
        [Browsable(false)]
        private FileStream file = new FileStream(@"./pages.xml", FileMode.OpenOrCreate);
        [Browsable(false)]
        private XmlSerializer xml = new XmlSerializer(new printPiewControlXml().GetType());
        private void printPiewControl_Load(object sender, EventArgs e)
        {
            this.toolCob_Intgaiting.SelectedIndex = 0;
            if (file.Length > 0)
            {
                var result = xml.Deserialize(file) as printPiewControlXml;
                for (int i = 0; i < result.page.Length; i++)
                {
                    this.cmb_page.Items.Add(result.page[i]);
                }
                if (this.cmb_page.Items.Contains(_page))
                {
                    this.cmb_page.SelectedValue = _page;
                }
                else
                {
                    this.cmb_page.SelectedIndex = 0;
                }
            }
            this.cmb_printWipe.SelectedIndex = 0;
            this.ptb_page.Image = Image.FromFile(fileAddress);
            this.ptb_page.Padding = new Padding(this.ptb_page.Location.X, ptb_page.Location.Y,ptb_page.Width-ptb_page.Image.Width,ptb_page.Height-ptb_page.Image.Height);
        }

        private void cmb_page_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void toolCob_Intgaiting_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_printWipe_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_custom_Click(object sender, EventArgs e)
        {
            this.txb_customPage.Enabled = true;
        }

        private void toolBtn_Add_Click(object sender, EventArgs e)
        {
            if (this.txb_customPage.Text != "")
            {
                this.cmb_page.Items.Add(this.txb_customPage.Text);
                printPiewControlXml xmlp = new printPiewControlXml();
                List<Page> li = new List<Page>();
                foreach (string key in this.cmb_page.Items)
                {
                    Page p = new Page();
                    p.page = key;
                    li.Add(p);
                }
                xmlp.page = li.ToArray();
                xml.Serialize(file, xmlp);
                this.txb_customPage.Text = "";
                this.txb_customPage.Enabled = false;
            }
        }

        private void toolBtn_clear_Click(object sender, EventArgs e)
        {
            if (this.cmb_page.Text != "")
            {
                this.cmb_page.Items.RemoveAt(this.cmb_page.SelectedIndex);
                printPiewControlXml xmlp = new printPiewControlXml();
                List<Page> li = new List<Page>();
                foreach (string key in this.cmb_page.Items)
                {
                    Page p = new Page();
                    p.page = key;
                    li.Add(p);
                }
                xmlp.page = li.ToArray();
                xml.Serialize(file, xmlp);
            }
        }

        private void toolBtn_close_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Visible = false;
        }

        private void toolCob_Intgaiting_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
