using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;
using System.Collections;

namespace ClinetPrints.CreatContorl
{
    public partial class dataGrieViewControl1 : UserControl
    {
        public dataGrieViewControl1()
        {
            InitializeComponent();
        }
        private UserColumnHanderCollection _handers;
        public UserColumnHanderCollection handers
        {
            get { return _handers; }
            set
            {
                _handers = value;
                if (_handers != null)
                {
                    for (int i = 0; i < _handers.Count; i++)
                    {
                        _handers[i].Location = new Point(this.Location.X + 10 + (i * _handers[i].Width), this.Location.Y + 10);
                    }
                }else
                {
                    _handers = new UserColumnHanderCollection(this);
                }
            }
        }
        private UserItems _items;
        public UserItems items
        {
            get { return _items; }
            set
            {
                _items = value;
                if (_items != null)
                {
                   foreach(newSubControl key in _items)
                    {
                        this.groupBox1.Controls.Add(key);
                    }
                }else
                {
                    _items = new UserItems(this);
                }
            }
        }

        private void dataGrieViewControl1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 表头的集合（列）
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

            public void Add(UserColumnHander value)
            {
                (Owner as dataGrieViewControl1).groupBox1.Controls.Add(value);
                colls.Add(value);
            }
            public new void Clear()
            {
                (Owner as dataGrieViewControl1).groupBox1.Controls.Clear();
                colls.Clear();
            }
            public void Remove(UserColumnHander value)
            {
                (Owner as dataGrieViewControl1).groupBox1.Controls.Remove(value);
                colls.Remove(value);
            }

            public new UserColumnHander this[int index]
            {
                get
                {
                    return colls[index];
                }
            }

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
            public void Add(UserSubControl[] value)
            {
                var contr = new newSubControl(value, (Owner as dataGrieViewControl1), li.Count);
                if (contr != null)
                {
                    li.Add(contr);
                }
            }
            public void Remove(newSubControl subControl)
            {
                (Owner as dataGrieViewControl1).groupBox1.Controls.Remove(subControl);
                li.Remove(subControl);
            }

            public new void Clear()
            {
                (Owner as dataGrieViewControl1).groupBox1.Controls.Clear();
                li.Clear();
            }
            public override int Count
            {
                get
                {
                    return li.Count;
                }
            }
            public new newSubControl this[int index]
            {
                get
                {
                    return li[index];
                }
            }

            public new newSubControl this[string key]
            {
                get
                {
                    foreach(newSubControl control in li)
                    {
                        if (control.Name == key)
                        {
                            return control;
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
        public class UserSubControl : Control
        {
            public UserSubControl(Control value)
            {
                this.Controls.Add(value);
            }
            public UserSubControl(string str)
            {
                TextBox txb = new TextBox();
                txb.Name = str;
                txb.Text = str;
                this.Controls.Add(txb);
            }
        }

        /// <summary>
        /// 生成一行项目的主控件
        /// </summary>
        public class newSubControl : UserControl
        {
            public dataGrieViewControl1 Owner { get; set; }

            public int Row { get; set; }

            public newSubControl(UserSubControl[] value, dataGrieViewControl1 owner, int row)
            {
                Row = row;
                Owner = owner;
                Size = owner.groupBox1.Size;
                if (owner.handers.Count >= value.Length)
                {
                    for (int i = 0; i < owner.handers.Count; i++)
                    {
                        if (i < value.Length)
                        {
                            value[i].Size = new Size(owner.handers[i].Width, 20);
                            value[i].Location = new Point(10 + (i * value[i].Width), 10 + (20 * Row));
                            this.Controls.Add(value[i]);
                        }
                        else
                        {
                            TextBox textbox = new TextBox();
                            textbox.BorderStyle = BorderStyle.FixedSingle;
                            Size = new Size(owner.handers[i].Width, 20);
                            textbox.Location = new Point(10 + (i * value[i].Width), 10 + (20 * Row));
                            this.Controls.Add(textbox);
                        }
                    }
                }

                owner.SizeChanged += Owner_SizeChanged;
            }

            private void Owner_SizeChanged(object sender, EventArgs e)
            {
                for (int i = 0; i < Controls.Count; ++i)
                {
                    var c = Controls[i];
                    c.Size = new Size(Owner.handers[i].Width, 20);
                    c.Location = new Point(10 + (i * c.Width), 10 + (20 * (Row + 1)));
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
                BorderStyle = BorderStyle.FixedSingle;
            }
            public void setSize(int Width,int Height)
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
