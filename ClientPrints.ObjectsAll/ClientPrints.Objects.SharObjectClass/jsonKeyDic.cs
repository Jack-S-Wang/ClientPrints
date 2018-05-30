using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class jsonKeyDic
    {
        public List<string> cfgKey = new List<string>();
        public jsonKeyDic()
        {

            getCfg();
            getInfo();
            
        }
        private void getCfg()
        {
            cfgKey.Add("USB_ID");
            cfgKey.Add("Emulation");
            cfgKey.Add("WiFi Cfg.WiFi Enable");
            cfgKey.Add("WiFi Cfg.WiFi Mode");
            cfgKey.Add("WiFi Cfg.DHCP");
            cfgKey.Add("WiFi Cfg.SSID");
            cfgKey.Add("WiFi Cfg.IP Address");
            cfgKey.Add("WiFi Cfg.Subnet Mask");
            cfgKey.Add("WiFi Cfg.Gateway");
            cfgKey.Add("WiFi Cfg.DNS");
            cfgKey.Add("WiFi Cfg.Password");
            cfgKey.Add("Blue Tooth.Model");
            cfgKey.Add("Blue Tooth.Password");
            cfgKey.Add("Ethernet.IP Address");
            cfgKey.Add("Ethernet.Port");
            cfgKey.Add("Ethernet.Gateway");
            cfgKey.Add("Card Type");
            cfgKey.Add("Input Card");
            cfgKey.Add("Output Card");
            cfgKey.Add("Print Temp");
            cfgKey.Add("Print Speed");
            cfgKey.Add("Erase Temp");
            cfgKey.Add("WiFi Cfg.Subnet Mask");
            cfgKey.Add("WiFi Cfg.Subnet Mask");
            cfgKey.Add("WiFi Cfg.Subnet Mask");
            cfgKey.Add("WiFi Cfg.Subnet Mask");
            cfgKey.Add("WiFi Cfg.Subnet Mask");

        }
        public void getInfo()
        {

        }
    }
}
