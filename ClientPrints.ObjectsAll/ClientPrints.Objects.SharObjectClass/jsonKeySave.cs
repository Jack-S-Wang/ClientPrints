using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class jsonKeySave
    {
        private List<DataItem> _list = new List<DataItem>();
        public List<DataItem> list
        {
            get
            {
                return _list;
            }
            set
            {
                _list = list;
            }
        }

        public void add(DataItem item)
        {          
            _list.Add(item); 
        }
        public class DataItem
        {
            /// <summary>
            /// 登记的键值
            /// </summary>
            public string keyName { get; set; }
           /// <summary>
           /// 界面显示的名称
           /// </summary>
            public string showName { get; set; }
            /// <summary>
            /// json查询键值名称
            /// </summary>
            public string jsonKeyName { get; set; }
        }
    }
}
