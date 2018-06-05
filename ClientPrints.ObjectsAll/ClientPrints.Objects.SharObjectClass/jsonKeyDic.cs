using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class jsonKeyDic
    {
        public static List<string> cfgD300Key = new List<string>();
        public static List<string> cfgL210Key = new List<string>();
        public static List<string> infoL210Key = new List<string>();
        public jsonKeyDic()
        {

            getCfg();
            getInfo();
            
        }
        private void getCfg()
        {
            using(FileStream file=new FileStream(AppDomain.CurrentDomain.BaseDirectory+"\\jsonXml\\cfg\\cfgD300.xml", FileMode.Open))
            {
                if (file.Length > 0)
                {
                    XmlSerializer xml = new XmlSerializer(typeof(jsonKeySave));
                    var result=xml.Deserialize(file) as jsonKeySave;
                    foreach(var item in result.list)
                    {
                        cfgD300Key.Add(item.jsonKeyName);
                    }
                }
            }

            using (FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\jsonXml\\cfg\\cfg210.xml", FileMode.Open))
            {
                if (file.Length > 0)
                {
                    XmlSerializer xml = new XmlSerializer(typeof(jsonKeySave));
                    var result = xml.Deserialize(file) as jsonKeySave;
                    foreach (var item in result.list)
                    {
                        cfgL210Key.Add(item.jsonKeyName);
                    }
                }
            }

        }
        public void getInfo()
        {

        }
    }
}
