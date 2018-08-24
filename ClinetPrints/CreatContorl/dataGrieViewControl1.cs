using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using ClientPrintsMethodList.ClientPrints.Method.sharMethod;

namespace ClinetPrints.CreatContorl
{
    public partial class dataGrieViewControl1 : UserControl
    {
        public dataGrieViewControl1()
        {
            InitializeComponent();
        }

        private UserColumnHanderCollection _handers;
        [Description("标头信息集合"),Browsable(false)]
        public UserColumnHanderCollection handers
        {
            get { return _handers; }
            set
            {
                try
                {
                    _handers = value;
                    if (_handers != null)
                    {
                        for (int i = 0; i < _handers.Count; i++)
                        {
                            _handers[i].Location = new Point(10 + (i * _handers[i].Width), 10);

                        }
                    }
                    else
                    {
                        _handers = new UserColumnHanderCollection(this);
                    }
                }
                catch (Exception ex)
                {
                    string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                    SharMethod.writeErrorLog(str);
                }
            }

        }
        private UserItems _items;
        [Description("字项目集合"),Browsable(false)]
        public UserItems items
        {
            get { return _items; }
            set
            {
                try
                {
                    _items = value;
                    if (_items != null)
                    {
                    }
                    else
                    {
                        _items = new UserItems(this);
                    }
                }
                catch (Exception ex)
                {
                    string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                    SharMethod.writeErrorLog(str);
                }
            }
        }

        private void dataGrieViewControl1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 标头的集合（列）
        /// </summary>
        public class UserColumnHanderCollection : ControlCollection
        {
            List<UserColumnHander> colls = new List<UserColumnHander>();
            public UserColumnHanderCollection(dataGrieViewControl1 owner) : base(owner)
            {
            }
            public UserColumnHanderCollection(dataGrieViewControl1 owner, UserColumnHander[] handers) : base(owner)
            {
                foreach (var key in handers)
                {
                    Add(key);
                }
            }
            /// <summary>
            /// 添加标头列
            /// </summary>
            /// <param name="value"></param>
            public void Add(UserColumnHander value)
            {
                try
                {
                    (Owner as dataGrieViewControl1).Controls.Add(value);
                    colls.Add(value);
                }
                catch (Exception ex)
                {
                    string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                    SharMethod.writeErrorLog(str);
                }
            }
            /// <summary>
            /// 清空集合
            /// </summary>
            public new void Clear()
            {
                (Owner as dataGrieViewControl1).Controls.Clear();
                colls.Clear();
            }
            /// <summary>
            /// 删除某一个列标头
            /// </summary>
            /// <param name="value"></param>
            public void Remove(UserColumnHander value)
            {
                (Owner as dataGrieViewControl1).Controls.Remove(value);
                colls.Remove(value);
            }

            /// <summary>
            /// 找到对应的标头
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public new UserColumnHander this[int index]
            {
                get
                {
                    return colls[index];
                }
            }
            /// <summary>
            /// 找到对应的标头
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public new UserColumnHander this[string key]
            {
                get
                {
                    foreach (var item in colls)
                    {
                        if (item.Name == key)
                        {
                            return item;
                        }
                    }

                    return null;
                }
            }

            public override int Count
            {
                get
                {
                    return colls.Count;
                }
            }
        }

        /// <summary>
        /// 项目的集合（行）
        /// </summary>
        public class UserItems : ControlCollection
        {
            List<newSubControl> li = new List<newSubControl>();
            public UserItems(dataGrieViewControl1 owner) : base(owner)
            {
            }
            //public UserItems(dataGrieViewControl1 owner, UserSubControl[][] value) : base(owner)
            //{
            //    foreach (var key in value)
            //    {
            //        var contr = new newSubControl(key, (Owner as dataGrieViewControl1));
            //        if (contr != null)
            //        {
            //            Add(contr);
            //        }
            //    }
            //}
            /// <summary>
            /// 添加一行项目中所有子项集合
            /// </summary>
            /// <param name="value"></param>
            public void Add(UserSubControl[] value)
            {
                var contr = new newSubControl(value, (Owner as dataGrieViewControl1), li.Count);
                if (contr != null)
                {
                    li.Add(contr);
                }
            }
            /// <summary>
            /// 删除对应一行项目信息
            /// </summary>
            /// <param name="subControl"></param>
            public void Remove(newSubControl subControl)
            {
                try
                {
                    foreach (var key in subControl.Value)
                    {
                        (Owner as dataGrieViewControl1).Controls.Remove(key.control);
                    }
                    li.Remove(subControl);
                }
                catch (Exception ex)
                {
                    string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                    SharMethod.writeErrorLog(str);
                }
            }
            /// <summary>
            /// 清楚所有行项目
            /// </summary>
            public new void Clear()
            {
                (Owner as dataGrieViewControl1).Controls.Clear();
                li.Clear();
            }
            public override int Count
            {
                get
                {
                    return li.Count;
                }
            }
            /// <summary>
            /// 返回改行项目
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public new newSubControl this[int index]
            {
                get
                {
                    return li[index];
                }
            }
            /// <summary>
            /// 返回对应的改行的子项项目
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public new newSubControl this[string key]
            {
                get
                {
                    foreach (newSubControl control in li)
                    {
                        foreach (var con in control.Value)
                        {
                            if (con.control.Name == key)
                            {
                                return control;
                            }
                        }
                    }
                    return null;
                }
            }

            public override IEnumerator GetEnumerator()
            {
                return li.GetEnumerator();
            }
        }

        /// <summary>
        /// 设计每一行中子项目的控件
        /// </summary>
        public class UserSubControl
        {
            public Control control { get; set; }

            /// <summary>
            /// 表示该控件的键值是否需要执行其他操作，对控件的循环判断选择，默认false
            /// </summary>
            public bool isUse { get; set; }
            /// <summary>
            /// 添加不同的控件信息
            /// </summary>
            /// <param name="value">控件</param>
            /// <param name="isEndable">设置控件是否可以响应</param>
            public UserSubControl(Control value, bool isEndable)
            {
                value.Enabled = isEndable;
                control = value;
            }
            /// <summary>
            /// 直接生成一个TextBox的文本框信息
            /// </summary>
            /// <param name="str">文本内容名称</param>
            /// <param name="isEndable">设置控件是否可以响应</param>
            public UserSubControl(string str, bool isEndable)
            {
                TextBox txb = new TextBox();
                txb.Name = str;
                txb.Text = str;
                txb.Enabled = isEndable;
                control = txb;
            }
        }

        /// <summary>
        /// 生成一行项目的主控件
        /// </summary>
        public class newSubControl
        {
            public dataGrieViewControl1 Owner { get; set; }
            public int Row;
            public UserSubControl[] Value { get; }
            /// <summary>
            /// 对主控件添加那些子项目中的所有控件
            /// </summary>
            /// <param name="value">子项目集合</param>
            /// <param name="owner">父类控件</param>
            /// <param name="row">当前的行数</param>
            public newSubControl(UserSubControl[] value, dataGrieViewControl1 owner, int row)
            {
                try
                {
                    Owner = owner;
                    Row = row;
                    Value = value;
                    if (owner.handers.Count >= value.Length)
                    {
                        for (int i = 0; i < owner.handers.Count; i++)
                        {
                            if (i < value.Length)
                            {

                                if ((value[i].control as Control) is ComboBox)
                                {
                                    var com = (value[i].control as Control) as ComboBox;
                                    com.Width = owner.handers[i].Width;
                                    com.Height = 20;
                                }
                                else
                                {
                                    value[i].control.Size = new Size(owner.handers[i].Width, 20);
                                }
                                value[i].control.Location = new Point(10 + (i * value[i].control.Width), 10 + 20 * (Row + 1));
                                Owner.Controls.Add(value[i].control);
                            }
                            else
                            {
                                TextBox textbox = new TextBox();
                                textbox.BorderStyle = BorderStyle.FixedSingle;
                                textbox.Size = new Size(owner.handers[i].Width, 20);
                                textbox.Location = new Point(10 + (i * value[i].control.Width), 10 + 20 * (Row + 1));
                                Owner.Controls.Add(textbox);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                    SharMethod.writeErrorLog(str);
                }
            }

        }

        /// <summary>
        /// 生成一个列标题
        /// </summary>
        public class UserColumnHander : TextBox
        {
            public UserColumnHander(string text)
            {
                Name = text;
                Text = text;
                Size = new Size(100, 20);
                BorderStyle = BorderStyle.Fixed3D;
                BackColor = Color.BurlyWood;

            }
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                base.OnKeyPress(e);
                e.Handled = true;
            }
            public void setSize(int Width, int Height)
            {
                Size = new Size(Width, Height);
            }
        }

        private void dataGrieViewControl1_Paint(object sender, PaintEventArgs e)
        {
            //foreach (Control c in Controls)
            //{
            //    c.Show();
            //    c.Update();
            //}
        }
    }



}
