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
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objetcs.Printers.Interface;
using ClientPrsintsMethodList.ClientPrints.Method.sharMethod;

namespace ClinetPrints.CreatContorl
{
    public partial class printPiewControl : UserControl
    {
        public printPiewControl()
        {
            InitializeComponent();
        }

        [Description("打印机对象,使用时可传入")]
        public PrinterObjects PrinterObject
        {
            get { return _prinerObject; }
            set
            {
                if (value != null)
                {
                    this.cmb_page.Items.Add(value.pParams.page);
                    var dirPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ClientPrints");
                    var filePath = Path.Combine(dirPath, "pages.xml");
                    var file = new FileStream(filePath, FileMode.OpenOrCreate);
                    if (file == null)
                    {
                        return;
                    }
                    if (file.Length > 0)
                    {
                        var result = xml.Deserialize(file) as printPiewControlXml;
                        for (int i = 0; i < result.pages.Length; i++)
                        {
                            if (!this.cmb_page.Items.Contains(result.pages[i].page))
                                this.cmb_page.Items.Add(result.pages[i].page);
                        }
                    }
                    this.cmb_page.SelectedIndex = 0;
                    //this.cmb_printWipe.SelectedIndex = 1;
                    file.Flush();
                    file.Dispose();
                    file.Close();
                    _prinerObject = value;
                }
            }
        }
        [Browsable(false)]
        private PrinterObjects _prinerObject;
        [Description("图片路径地址")]
        public string fileAddress
        {
            get { return _fileAddress; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Bitmap map = new Bitmap(value);
                    oldmap = new Bitmap(map, new Size((int)(map.Width * 0.8), (int)(map.Height * 0.8)));
                    this.ptb_page.Image = oldmap;
                    imageScale = 0.8;
                }
                _fileAddress = value;
            }
        }
        [Browsable(false)]
        private string _fileAddress;
        [Description("纸张大小，列：500x600,或500*600；宽乘高,单位/毫米")]
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
                    if (_page == "")
                    {
                        return;
                    }
                    double width = 0;
                    double height = 0;
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
                    this.ptb_page.Size = new Size((int)(width * 0.8), (int)(height * 0.8));
                }
            }
        }
        [Description("打印的作业号,默认是1")]
        public string jobNum = "1";
        [Description("打印的数量，默认是1")]
        public int num = 1;
        [Browsable(false)]
        private string _page;
        [Browsable(false)]
        private XmlSerializer xml = new XmlSerializer(new printPiewControlXml().GetType());
        [Browsable(false)]
        private Bitmap oldmap;
        [Description("退出按钮事件")]
        public event Action<object, EventArgs> OnBtnClose;
        [Description("打印按钮事件")]
        public event Action<object, EventArgs> onBtnPrint;

        private void printPiewControl_Load(object sender, EventArgs e)
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(this.txb_customPage, "格式是500x600或500*600，其他格式将会出问题！");
            this.toolCob_Intgaiting.SelectedIndex = 5;
            ptb_page.MouseWheel += Ptb_page_MouseWheel;
        }
        /// <summary>
        /// 鼠标滚动是否响应响应，有些契合度选择之后不响应
        /// </summary>
        [Browsable(false)]
        private bool wheelEnabled = true;
        private void Ptb_page_MouseWheel(object sender, MouseEventArgs e)
        {
            if (wheelEnabled)
            {
                imageScale *= e.Delta > 0 ? 1.25 : 0.8;
                ReDraw(imageX, imageY, imageScale);
            }
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
                var filePath = Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
               "ClientPrints",
               "pages.xml");
                var file = new FileStream(filePath, FileMode.OpenOrCreate);
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
                file.Flush();
                file.Dispose();
                file.Close();
            }
        }

        private void toolBtn_clear_Click(object sender, EventArgs e)
        {
            if (this.cmb_page.Text != "")
            {
                if (this.cmb_page.SelectedIndex != -1)
                {
                    this.cmb_page.Items.RemoveAt(this.cmb_page.SelectedIndex);
                }
                else
                {
                    this.cmb_page.Items.Remove(cmb_page.Text);
                }
                if (cmb_page.Items.Count > 0)
                {
                    cmb_page.SelectedIndex = 0;
                }
                else
                {
                    this.cmb_page.Text = "";
                }
                var filePath = Path.Combine(
              Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
              "ClientPrints",
              "pages.xml");
                var file = new FileStream(filePath, FileMode.OpenOrCreate);
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
                file.Flush();
                file.Dispose();
                file.Close();
            }
        }

        private void toolBtn_close_Click(object sender, EventArgs e)
        {
            OnBtnClose?.Invoke(sender, e);
        }

        private void toolCob_Intgaiting_SelectedIndexChanged(object sender, EventArgs e)
        {
            int min = 0;
            int max = 0;
            Bitmap bmap = new Bitmap(ptb_page.Width, ptb_page.Height);
            switch (toolCob_Intgaiting.SelectedIndex)
            {
                case 0:

                    if (oldmap.Width > oldmap.Height)
                    {
                        min = oldmap.Height;
                    }
                    else
                    {
                        min = oldmap.Width;
                    }
                    if (ptb_page.Width > ptb_page.Height)
                    {
                        imageScale = ((double)ptb_page.Width / (double)min);
                        imageX = 0; imageY = 0;
                        ReDraw(imageX, imageY, imageScale);

                    }
                    else
                    {
                        imageX = 0; imageY = 0;
                        imageScale = ((double)ptb_page.Height / (double)min);
                        ReDraw(imageX, imageY, imageScale);
                    }
                    wheelEnabled = true;
                    moveTo=true;
                    break;
                case 1:
                    if (oldmap.Width > oldmap.Height)
                    {
                        max = oldmap.Width;
                    }
                    else
                    {
                        max = oldmap.Height;
                    }
                    if (ptb_page.Width > ptb_page.Height)
                    {
                        imageScale = ((double)ptb_page.Height / (double)max);
                        imageX = 0; imageY = 0;
                        ReDraw(imageX, imageY, imageScale);

                    }
                    else
                    {
                        imageX = 0; imageY = 0;
                        imageScale = ((double)ptb_page.Width / (double)max);
                        ReDraw(imageX, imageY, imageScale);
                    }
                    wheelEnabled = true;
                    moveTo = true;
                    break;
                case 2:
                    Graphics g = Graphics.FromImage(bmap);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.DrawImage(oldmap,
                        new Rectangle(
                            0,
                            0,
                            (int)(ptb_page.Width),
                            (int)(ptb_page.Height)),
                        new Rectangle(0, 0, oldmap.Width, oldmap.Height),
                        GraphicsUnit.Pixel);
                    g.Dispose();
                    ptb_page.Image = bmap;
                    wheelEnabled = false;
                    moveTo = false;
                    break;
                case 3:
                    int wscale = (ptb_page.Width % oldmap.Width) == 0 ? (ptb_page.Width / oldmap.Width) : (ptb_page.Width / oldmap.Width) + 1;
                    int hscale = (ptb_page.Height % oldmap.Height) == 0 ? (ptb_page.Height / oldmap.Height) : (ptb_page.Height / oldmap.Height) + 1;
                    for (int i = 0; i < wscale; ++i)
                    {
                        for (int j = 0; j < hscale; ++j)
                        {
                            Graphics g1 = Graphics.FromImage(bmap);
                            g1.SmoothingMode = SmoothingMode.HighQuality;
                            g1.CompositingQuality = CompositingQuality.HighQuality;
                            g1.DrawImage(oldmap,
                                new Rectangle(
                                    i * oldmap.Width,
                                    j * oldmap.Height,
                                    (int)(oldmap.Width),
                                    (int)(oldmap.Height)),
                                new Rectangle(0, 0, oldmap.Width, oldmap.Height),
                                GraphicsUnit.Pixel);
                            g1.Dispose();
                        }
                    }
                    ptb_page.Image = bmap;
                    wheelEnabled = false;
                    moveTo = false;
                    break;
                case 4:
                    imageX = ptb_page.Width / 2 - oldmap.Width / 2;
                    imageY = ptb_page.Height / 2 - oldmap.Height / 2;
                    imageScale = 0.8;
                    Graphics g2 = Graphics.FromImage(bmap);
                    g2.SmoothingMode = SmoothingMode.HighQuality;
                    g2.CompositingQuality = CompositingQuality.HighQuality;
                    g2.DrawImage(oldmap,
                        new Rectangle(
                            imageX,
                            imageY,
                            (int)(oldmap.Width),
                            (int)(oldmap.Height)),
                        new Rectangle(0, 0, oldmap.Width, oldmap.Height),
                        GraphicsUnit.Pixel);
                    g2.Dispose();
                    ptb_page.Image = bmap;
                    wheelEnabled = true;
                    moveTo = true;
                    break;
                case 5:
                    ptb_page.Image = oldmap;
                    imageX = 0;
                    imageY = 0;
                    imageScale = 0.8;
                    wheelEnabled = true;
                    moveTo = true;
                    break;
            }
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
        private bool moveTo = true;
        private void ptb_page_MouseUp(object sender, MouseEventArgs e)
        {
            if (moveTo)
            {
                imageX = imageX + (e.X - clickPiont.X);
                imageY = imageY + (e.Y - clickPiont.Y);
                ReDraw(imageX, imageY, imageScale);
            }
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
            if (this.toolCob_Intgaiting.SelectedIndex == 5)
            {
                ptb_page.Image = oldmap;
                imageX = 0;
                imageY = 0;
                imageScale = 0.8;
            }
            else
            {
                this.toolCob_Intgaiting.SelectedIndex = 5;
            }
        }

        private void toolBtn_save_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderB = new FolderBrowserDialog();
                DialogResult dr = folderB.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    var filePath = folderB.SelectedPath + "/" + DateTime.Now.ToString("yyyyMMdd HH.mm.ss") + ".bmp";
                    Bitmap bmap = new Bitmap((int)(ptb_page.Width * 1.25), (int)(ptb_page.Height * 1.25));
                    Graphics g = Graphics.FromImage(bmap);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.DrawImage(ptb_page.Image,
                        new Rectangle(
                            0,
                            0,
                            (int)(ptb_page.Width * 1.25),
                            (int)(ptb_page.Height * 1.25)),
                        new Rectangle(0, 0, ptb_page.Width, ptb_page.Height),
                        GraphicsUnit.Pixel);
                    g.Dispose();
                    bmap.Save(filePath);
                    MessageBox.Show("保存成功！");
                }
            }
            catch (Exception ex)
            {
                SharMethod.writeLog(string.Format("异常{0},追踪：{1}", ex, ex.StackTrace));
                MessageBox.Show(ex.Message);
            }

        }

        private void toolBtn_print_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.cmb_page.Text))
                {
                    string p = cmb_page.Text;
                    double width = 0;
                    double height = 0;
                    if (p.Contains("x"))
                    {
                        width = double.Parse(p.Substring(0, p.IndexOf('x')));
                        height = double.Parse(p.Substring(p.IndexOf('x') + 1));
                    }
                    else
                    {
                        width = double.Parse(p.Substring(0, p.IndexOf('*')));
                        height = double.Parse(p.Substring(p.IndexOf('*') + 1));
                    }
                    width = ((width / 10) / 2.54) * 300;
                    height = ((height / 10) / 2.54) * 300;
                    int nwidth = nNum(width);
                    int nheight = nNum(height);
                    if (nwidth > _prinerObject.pParams.maxWidth || nheight > _prinerObject.pParams.maxHeight)
                    {
                        MessageBox.Show("现在所设计的尺寸大小与实际设备的尺寸要大，不能打印！");
                        return;
                    }
                    printBtn(sender, e);
                }
            }
            catch (Exception ex)
            {
                SharMethod.writeLog(string.Format("异常{0},追踪：{1}", ex, ex.StackTrace));
                MessageBox.Show(ex.Message);
            }
        }

        private void printBtn(object sender, EventArgs e)
        {
            if (PrinterObject != null)
            {
                if (PrinterObject.stateCode == 4 || PrinterObject.stateCode == 5 || PrinterObject.stateCode == 6)
                {
                    MessageBox.Show("打印机：" + PrinterObject.alias + "状态不正常，不能打印！");
                    return;
                }
                if (PrinterObject.pParams.outJobNum >= 65000)
                {
                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        MessageBox.Show("打印工作号缓存数过大，请打印完成之后到监控控制界面进行重启该设备进行释放！！");
                    });
                }
                var filePath = Path.Combine(
              Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
              "ClientPrints",
              DateTime.Now.ToString("yyyyMMdd HH.mm.ss") + ".bmp");
                var method = PrinterObject.MethodsObject as IMethodObjects;
                //PrinterObject.pParams.bkBmpID = (byte)this.cmb_printWipe.SelectedIndex;
                Bitmap bmap = new Bitmap((int)(_prinerObject.pParams.maxWidth), (int)(_prinerObject.pParams.maxHeight));
                Graphics g = Graphics.FromImage(bmap);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.FillRectangle(Brushes.White, new Rectangle(0, 0, bmap.Width, bmap.Height));
                g.DrawImage(ptb_page.Image,
                    new Rectangle(
                        0,
                        0,
                        (int)(ptb_page.Width*1.25),
                        (int)(ptb_page.Height*1.25)),
                    new Rectangle(0, 0, ptb_page.Width, ptb_page.Height),
                    GraphicsUnit.Pixel);
                g.Dispose();
                bmap.Save(filePath);
                List<string> succese = method.writeDataToDev(filePath, PrinterObject, jobNum, num);
                if (succese[0] == "error")
                {
                    MessageBox.Show("打印失败！" + succese[1]);
                    return;
                }
                else
                {
                    MessageBox.Show("打印成功，将删除原来的作业任务!");
                }
                onBtnPrint?.Invoke(sender, e);
            }
        }

        private void cmb_page_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.cmb_page.Text))
                {
                    string p = cmb_page.Text;
                    double width = 0;
                    double height = 0;
                    if (p.Contains("x"))
                    {
                        width = double.Parse(p.Substring(0, p.IndexOf('x')));
                        height = double.Parse(p.Substring(p.IndexOf('x') + 1));
                    }
                    else
                    {
                        width = double.Parse(p.Substring(0, p.IndexOf('*')));
                        height = double.Parse(p.Substring(p.IndexOf('*') + 1));
                    }
                    width = ((width / 10) / 2.54) * 300;
                    height = ((height / 10) / 2.54) * 300;
                    int nwidth = nNum(width);
                    int nheight = nNum(height);
                    page = nwidth + "*" + nheight;
                }
            }
            catch (Exception ex)
            {
                SharMethod.writeLog(string.Format("异常{0},追踪：{1}", ex, ex.StackTrace));
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 四舍五入方法
        /// </summary>
        /// <param name="num">double类型的值</param>
        /// <returns></returns>
        private int nNum(double num)
        {
            if (num - (int)num >= 0.5)
            {
                return (int)num + 1;
            }
            else
            {
                return (int)num;
            }
        }
    }
}
