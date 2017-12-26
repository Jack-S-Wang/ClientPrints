using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class printerError
    {
        public class Item
        {
            public string time;
            public string message;
        }
        
        public void Add(string time, string message)
        {
            while (Items.Count >= 5)
            {
                Items.RemoveAt(0);
            }

            Items.Add(new Item {
                time = time,
                message = message
            });
        }

        /// <summary>
        /// 记录上报时间和日记信息
        /// </summary>
        public List<Item> Items = new List<Item>();
    }
}
