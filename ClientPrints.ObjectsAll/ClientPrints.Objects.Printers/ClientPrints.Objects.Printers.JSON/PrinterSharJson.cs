using ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.Printers.ClientPrints.Objects.Printers.JSON
{
    public static class PrinterSharJson
    {
        public static string GenericSEP { get; set; }
        public static string GenericDatas { get; set; }
        public static string InterfaceRecvice { get; set; }
        public static string InterfaceFrame { get; set; }
        public static string InterfaceCompress { get; set; }
        public static string PageWidth { get; set; }
        public static string PageHeight { get; set; }
        public static string PageMargin { get; set; }
        public static string PageXDPI { get; set; }
        public static string PageYDPI { get; set; }
        public static string PageBPPS { get; set; }
        public static string PageBMP_FMT { get; set; }
        public static string PageDSBMP { get; set; }
        public static string SSRunState { get; set; }
        public static string SSError { get; set; }
        public static string DSProcess { get; set; }
        public static string DSJobNumber { get; set; }
        public static string DSFrameNumber { get; set; }
        public static string DSErrorCode { get; set; }
        public static string PSOutputState { get; set; }
        public static string PSNumberOfPrint { get; set; }
        public static string PSCompletedJobNumber { get; set; }
        public static string PSCompletedFrameNumber { get; set; }
        public static string PSTemperature { get; set; }
        public static string PSSensor0 { get; set; }
        public static string PSSensor1 { get; set; }
        public static string PSSensor2 { get; set; }
        public static string PSSensor3 { get; set; }
        public static string DISInCache { get; set; }
        public static string DISResidueCache { get; set; }

        public static void getJsonKey()
        {
            using (FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\jsonXml\\info\\info.xml", FileMode.Open))
            {
                if (file.Length > 0)
                {
                    XmlSerializer xml = new XmlSerializer(typeof(jsonKeySave));
                    var result = xml.Deserialize(file) as jsonKeySave;
                    foreach(var item in result.list)
                    {
                        switch (item.keyName)
                        {
                           
                            case "GenericSEP":
                                GenericSEP = item.jsonKeyName;
                                break;
                            case "GenericDatas":
                                GenericDatas = item.jsonKeyName;
                                break;
                            case "InterfaceRecvice":
                                InterfaceRecvice = item.jsonKeyName;
                                break;
                            case "InterfaceFrame":
                                InterfaceFrame = item.jsonKeyName;
                                break;
                            case "InterfaceCompress":
                                InterfaceCompress = item.jsonKeyName;
                                break;
                            case "PageWidth":
                                PageWidth = item.jsonKeyName;
                                break;
                            case "PageHeight":
                                PageHeight = item.jsonKeyName;
                                break;
                            case "PageMargin":
                                PageMargin = item.jsonKeyName;
                                break;
                            case "PageXDPI":
                                PageXDPI = item.jsonKeyName;
                                break;
                            case "PageYDPI":
                                PageYDPI = item.jsonKeyName;
                                break;
                            case "PageBPPS":
                                PageBPPS = item.jsonKeyName;
                                break;
                            case "PageBMP_FMT":
                                PageBMP_FMT = item.jsonKeyName;
                                break;
                            case "PageDSBMP":
                                PageDSBMP = item.jsonKeyName;
                                break;
                            case "SSRunState":
                                SSRunState = item.jsonKeyName;
                                break;
                            case "SSError":
                                SSError = item.jsonKeyName;
                                break;
                            case "DSProcess":
                                DSProcess = item.jsonKeyName;
                                break;
                            case "DSJobNumber":
                                DSJobNumber = item.jsonKeyName;
                                break;
                            case "DSFrameNumber":
                                DSFrameNumber = item.jsonKeyName;
                                break;
                            case "DSErrorCode":
                                DSErrorCode = item.jsonKeyName;
                                break;
                            case "PSOutputState":
                                PSOutputState = item.jsonKeyName;
                                break;
                            case "PSNumberOfPrint":
                                PSNumberOfPrint = item.jsonKeyName;
                                break;
                            case "PSCompletedJobNumber":
                                PSCompletedJobNumber = item.jsonKeyName;
                                break;
                            case "PSCompletedFrameNumber":
                                PSCompletedFrameNumber = item.jsonKeyName;
                                break;
                            case "PSTemperature":
                                PSTemperature = item.jsonKeyName;
                                break;
                            case "PSSensor0":
                                PSSensor0 = item.jsonKeyName;
                                break;
                            case "PSSensor1":
                                PSSensor1 = item.jsonKeyName;
                                break;
                            case "PSSensor2":
                                PSSensor2 = item.jsonKeyName;
                                break;
                            case "PSSensor3":
                                PSSensor3 = item.jsonKeyName;
                                break;
                            case "DISInCache":
                                DISInCache = item.jsonKeyName;
                                break;
                            case "DISResidueCache":
                                DISResidueCache = item.jsonKeyName;
                                break;
                        }
                    }
                }
            }
        }
    }
}
