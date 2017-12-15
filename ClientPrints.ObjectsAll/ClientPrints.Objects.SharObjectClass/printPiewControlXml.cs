using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class printPiewControlXml
    {
        public Page[] pages { get; set; }
        public class Page
        {
            public string page { get; set; }
        }
    }
}
