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
            public string keyName { get; set; }
            public string showName { get; set; }
            public string jsonKeyName { get; set; }
        }
    }
}
