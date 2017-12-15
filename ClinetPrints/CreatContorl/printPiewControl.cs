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
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

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
        public printPiewControl(string page, string fileAddress)
        {
            InitializeComponent();
            this.page = page;
            this.fileAddress = fileAddress;
        }
        [Description("打印机对象,使用时可传入")]
        public object PrinterObject { get; set; }
        [Description("图片路径地址")]
        public string fileAddress { get; set; }
        [Description("纸张大小，列：500x600,或500*600；宽乘高")]
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
                else
                {
                    int width = 0;
                    int height = 0;
                    string p = _page.ToLower();
                    if (p.Contains("x"))
                    {
                        width = Int32.Parse(p.Substring(0, p.IndexOf('x')));
                        height = Int32.Parse(p.Substring(p.IndexOf('x') + 1));
                    }
                    else
                    {
                        width = Int32.Parse(p.Substring(0, p.IndexOf('*')));
                        height = Int32.Parse(p.Substring(p.IndexOf('*') + 1));
                    }
                    this.ptb_page.Size = new Size((int)(width * 0.7), (int)(height * 0.7));
                }
            }
        }
        [Browsable(false)]
        private string _page;
        [Browsable(false)]
        private FileStream file = new FileStream(@"./pages.xml", FileMode.OpenOrCreate);
        [Browsable(false)]
        private XmlSerializer xml = new XmlSerializer(new printPiewControlXml().GetType());
        [Browsable(false)]
        private Bitmap oldmap;

        private void printPiewControl_Load(object sender, EventArgs e)
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(this.txb_customPage, "格式是500x600或500*600，其他格式将会出问题！");
            this.toolCob_Intgaiting.SelectedIndex = 0;
            if (file.Length > 0)
            {
                var result = xml.Deserialize(file) as printPiewControlXml;
                for (int i = 0; i < result.pages.Length; i++)
                {
                    this.cmb_page.Items.Add(result.pages[i].page);
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
            Bitmap map = new Bitmap(fileAddress);
            oldmap = new Bitmap(map, new Size((int)(map.Width * 0.7), (int)(map.Height * 0.7)));
            this.ptb_page.Image = oldmap;
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
                this.cmb_page.Text = txb_customPage.Text;
                printPiewControlXml xmlp = new printPiewControlXml();
                List<Page> li = new List<Page>();
                foreach (string key in this.cmb_page.Items)
                {
                    Page p = new Page();
                    p.page = key;
                    li.Add(p);
                }
                xmlp.pages = li.ToArray();
                if (file.Length > 0)
                {
                    file.SetLength(0);
                    file.Position = 0;
                }
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
                if (cmb_page.Items.Count > 0)
                {
                    cmb_page.SelectedIndex = 0;
                }
                else
                {

                    this.cmb_page.Text = "";
                }
                printPiewControlXml xmlp = new printPiewControlXml();
                List<Page> li = new List<Page>();
                foreach (string key in this.cmb_page.Items)
                {
                    Page p = new Page();
                    p.page = key;
                    li.Add(p);
                }
                xmlp.pages = li.ToArray();
                if (file.Length > 0)
                {
                    file.SetLength(0);
                    file.Position = 0;
                }
                xml.Serialize(file, xmlp);
            }
        }

        private void toolBtn_close_Click(object sender, EventArgs e)
        {
            file.Flush();
            file.Dispose();
            file.Close();
            this.Parent.Controls.Remove(this);
            this.DestroyHandle();
            this.Dispose();
        }

        private void toolCob_Intgaiting_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        [Browsable(false)]
        private Point clickPiont;
        private void ptb_page_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clickPiont.X = e.X;
                clickPiont.Y = e.Y;
            }
        }

        private Bitmap setNewBitmap(Image image, double proportion)
        {
            Bitmap map = new Bitmap(image, new Size((int)(image.Width * proportion), (int)(image.Height * proportion)));
            return map;
        }

        private void ptb_page_MouseEnter(object sender, EventArgs e)
        {
            MouseWheel += PrintPiewControl_MouseWheel;
        }
        private void PrintPiewControl_MouseWheel(object sender, MouseEventArgs e)
        {
            imageScale *= e.Delta > 0 ? 1.25 : 0.8;
            ReDraw(imageX, imageY, imageScale);
        }

        private void ptb_page_MouseLeave(object sender, EventArgs e)
        {
            MouseWheel -= PrintPiewControl_MouseWheel;
        }

        private void ptb_page_MouseUp(object sender, MouseEventArgs e)
        {
            imageX = imageX + (e.X - clickPiont.X);
            imageY = imageY + (e.Y - clickPiont.Y);
            ReDraw(imageX, imageY, imageScale);
        }

        private void ReDraw(int x, int y, double scale)
        {
            Bitmap bmap = new Bitmap(ptb_page.Width, ptb_page.Height);
            Graphics g = Graphics.FromImage(bmap);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(oldmap,
                new Rectangle(
                    imageX,
                    imageY,
                    (int)(oldmap.Width * imageScale),
                    (int)(oldmap.Height * imageScale)),
                new Rectangle(0, 0, oldmap.Width, oldmap.Height),
                GraphicsUnit.Pixel);
            g.Dispose();
            ptb_page.Image = bmap;
        }
        /// <summary>
        /// 记录图片的x坐标
        /// </summary>
        [Browsable(false)]
        private int imageX = 0;
        /// <summary>
        /// 记录图片的y坐标
        /// </summary>
        [Browsable(false)]
        private int imageY = 0;
        /// <summary>
        /// 记录图片缩放的比例
        /// </summary>
        [Browsable(false)]
        private double imageScale = 1.0;

        private void toolBtn_reMap_Click(object sender, EventArgs e)
        {
            this.ptb_page.Image = oldmap;
            imageX = 0;
            imageY = 0;
            imageScale = 1.0;
        }

        private void toolBtn_save_Click(object sender, EventArgs e)
        {
            FileStream fileImage = new FileStream(@"./printerNewImage/" + DateTime.Now.ToString("yyyyMMdd HH.mm.ss") + ".png", FileMode.Create);
            this.ptb_page.Image.Save(@"./printerNewImage/" + DateTime.Now.ToString("yyyyMMdd HH.mm.ss") + ".png");
            fileImage.Flush();
            fileImage.Dispose();
            file.Close();
            MessageBox.Show("保存成功！");
        }
    }
}
