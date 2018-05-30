using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    public class jsonKeyDic
    {
        public List<string> cfgD300Key = new List<string>();
        public List<string> cfgL210Key = new List<string>();
        public jsonKeyDic()
        {

            getCfg();
            getInfo();
            
        }
        private void getCfg()
        {
            cfgD300Key.Add("USB_ID");
            cfgD300Key.Add("Emulation");
            cfgD300Key.Add("WiFi Cfg.WiFi Enable");
            cfgD300Key.Add("WiFi Cfg.WiFi Mode");
            cfgD300Key.Add("WiFi Cfg.DHCP");
            cfgD300Key.Add("WiFi Cfg.SSID");
            cfgD300Key.Add("WiFi Cfg.IP Address");
            cfgD300Key.Add("WiFi Cfg.Subnet Mask");
            cfgD300Key.Add("WiFi Cfg.Gateway");
            cfgD300Key.Add("WiFi Cfg.DNS");
            cfgD300Key.Add("WiFi Cfg.Password");
            cfgD300Key.Add("Blue Tooth.Model");
            cfgD300Key.Add("Blue Tooth.Password");
            cfgD300Key.Add("Ethernet.IP Address");
            cfgD300Key.Add("Ethernet.Port");
            cfgD300Key.Add("Ethernet.Gateway");
            cfgD300Key.Add("Card Type");
            cfgD300Key.Add("Input Card");
            cfgD300Key.Add("Output Card");
            cfgD300Key.Add("Print Temp");
            cfgD300Key.Add("Print Speed");
            cfgD300Key.Add("Erase Temp");
            cfgD300Key.Add("WiFi Cfg.Subnet Mask");
            cfgD300Key.Add("WiFi Cfg.Subnet Mask");
            cfgD300Key.Add("WiFi Cfg.Subnet Mask");
            cfgD300Key.Add("WiFi Cfg.Subnet Mask");
            cfgD300Key.Add("WiFi Cfg.Subnet Mask");


            cfgL210Key.Add("COM Port.Datas");
            cfgL210Key.Add("COM Port.Stop");
            cfgL210Key.Add("COM Port.Parity");
            cfgL210Key.Add("COM Port.Baud");
            cfgL210Key.Add("Cutter");
            cfgL210Key.Add("Language");
            cfgL210Key.Add("ASCII Font");
            cfgL210Key.Add("Auto label");
            cfgL210Key.Add("Paper Sensor");
            cfgL210Key.Add("Black Mark");
            cfgL210Key.Add("AD Value");
            cfgL210Key.Add("Command Set");
            cfgL210Key.Add("Gap Size");
            cfgL210Key.Add("Auto PaperOut");
            cfgL210Key.Add("Code Page");
            cfgL210Key.Add("Auto calibration");
            cfgL210Key.Add("Page Length");
            cfgL210Key.Add("Left Margin");
            cfgL210Key.Add("DA Value");
            cfgL210Key.Add("Page Type");
            cfgL210Key.Add("Print Media");
            cfgL210Key.Add("Page Mode");
            cfgL210Key.Add("AD_Lab Value");
            cfgL210Key.Add("AD_Gap Value");
            cfgL210Key.Add("AD_Black Value");
            cfgL210Key.Add("Print Speed");
            cfgL210Key.Add("Print Darkness"); 
            cfgL210Key.Add("Peel Option");
            cfgL210Key.Add("Start Adjustment");
            cfgL210Key.Add("Cut Adjustment");
            cfgL210Key.Add("Photo TRGain");
            cfgL210Key.Add("BMB FeedPos");
            cfgL210Key.Add("ZPL_Print Darkness");
            cfgL210Key.Add("Label Length");
            cfgL210Key.Add("Cont_Paper Length");
            cfgL210Key.Add("Vertical Adjustment");
            cfgL210Key.Add("Tear Adjustment");
            cfgL210Key.Add("IP");
            cfgL210Key.Add("Ethernet.SubnetMask");
            cfgL210Key.Add("Ethernet.Gateway");
            cfgL210Key.Add("Ethernet.DHCP"); 
            cfgL210Key.Add("Ethernet.Port Number");
            cfgL210Key.Add("Ethernet.Timeout");
            cfgL210Key.Add("CutMode");




        }
        public void getInfo()
        {

        }
    }
}
