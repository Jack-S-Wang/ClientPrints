using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class CfgDataObjects
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 该类型所包含值的数
        /// </summary>
        private int typeCount = 0;
        /// <summary>
        /// 类型所有值的集合
        /// </summary>
        private List<string> li = new List<string>();
        /// <summary>
        /// 创建的控件对象
        /// </summary>
        public object ob { get; set; }
       

        /// <summary>
        /// 创建一个配置信息的对象数据
        /// </summary>
        /// <param name="cfgInfo">遍历获取到的配置信息内容</param>
        /// <param name="val">获取的值</param>
        public CfgDataObjects(string cfgInfo,string val)
        {
            Name = cfgInfo.Substring(5, cfgInfo.Substring(0, cfgInfo.IndexOf(',')).Length - 5);
            int count = 0;
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            string valDatas = "";
            //获取所需要的值进行赋值
            for (int n = 0; n < cfgInfo.Length; n++)//循环获取名称内容和最后的状态内容模式
            {
                if (cfgInfo[n] == ',')
                {
                    count++;
                }
                else if (count == 1)
                {
                    if (cfgInfo[n] == '=')
                    {
                        count1++;
                    }
                    else if (count1 == 1)
                    {
                        type = Int32.Parse(cfgInfo[n].ToString());//类型
                    }
                }
                else if (count == 2)
                {
                    if (cfgInfo[n] == '=')
                    {
                        count2++;
                    }
                    else if (count2 == 1)
                    {
                        typeCount = Int32.Parse(cfgInfo[n].ToString()) ;//获取状态类型的总数
                    }
                }
                else if (count == 4)
                {
                    if (cfgInfo[n] == '=')
                    {
                        count3++;
                    }
                    else if (count3 == 1)
                    {
                        if (cfgInfo[n] == ';')
                        {
                            li.Add(valDatas);
                            valDatas = "";
                        }
                        valDatas = valDatas + cfgInfo[n];//获取状态名称内容
                        if (n == cfgInfo.Length - 1)//最后一个值存进去
                        {
                            li.Add(valDatas);
                        }
                    }
                }
            }

            switch (type)
            {
                case 0:
                    TextBox txb = new TextBox();
                    txb.Text = val;
                    ob = txb;
                    break;
                case 1:
                    TextBox txbInt = new TextBox();
                    txbInt.Text = val;
                    ob = txbInt;
                    break;
                case 2:
                    TextBox txbStr = new TextBox();
                    txbStr.Text = val;
                    ob = txbStr;
                    break;
                case 3:
                    ComboBox cmb = new ComboBox();
                    for (int i = 0; i < typeCount; i++)
                    {
                        cmb.Items.Add(li[i]);
                    }
                    cmb.SelectedIndex = Int32.Parse(val);
                    ob = cmb;
                    break;
            }
        }
    }
}
