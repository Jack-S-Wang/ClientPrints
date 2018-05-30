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
        /// 父类名称
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
       
        /// <summary>
        /// 类型所有值的集合
        /// </summary>
        public List<string> liValues = new List<string>();
        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 创建一个配置信息的对象数据
        /// </summary>
        /// <param name="cfgInfo">遍历获取到的配置信息内容</param>
        /// <param name="val">获取的值</param>
        public CfgDataObjects(string key, string val, string listVal, int type)
        {
            if (key.Contains("."))
            {
                int index = key.IndexOf('.');
                Name = key.Substring(index + 1);
                ParentName = key.Substring(0, index);
            }
            else
            {
                Name = key;
                ParentName = "";
            }
            value = val;
            this.Type = type;
            int count = 0;
            string valDatas = "";
            //获取所需要的值进行赋值
            for (int n = 0; n < listVal.Length; n++)//循环获取名称内容和最后的状态内容模式
            {
                if (listVal[n] == ';')
                {
                    count++;
                    liValues.Add(valDatas);
                    valDatas = "";
                }
                else
                {
                    valDatas+=listVal[n];
                    if (n == listVal.Length - 1)//说明最后一个了
                    {
                        liValues.Add(valDatas);
                    }
                }
            }
        }
    }
}
