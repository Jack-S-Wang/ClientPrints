using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class DevPassword
    {
        private List<Item> _items=new List<Item>();
        public List<Item> items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }
        }
        public void addItem(Item item)
        {
            _items.Add(item);
        }
        public Item find(string passAddress)
        {
            Item item = null;
            foreach(var rk in _items)
            {
                if (rk.pathAddress.Equals(passAddress))
                {
                    item = rk;
                    break;
                }
            }
            return item;
        }
        
    }
        public class Item
        {
            /// <summary>
            /// 设备的地址信息
            /// </summary>
            public string pathAddress { get; set; }
            /// <summary>
            /// 设备设置的密码
            /// </summary>
            public byte[] password { get; set; }
            /// <summary>
            /// 内容格式是否为Hex
            /// </summary>
            public bool checkHex { get; set; }
        }
}
