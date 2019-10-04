using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace PPPrinter
{
    public partial class XReport : DevExpress.XtraReports.UI.XtraReport
    {
        public XReport(string cBarCode,string cCode,string cName)
        {
            InitializeComponent();
            Parameters["cBarCode"].Value = cBarCode;
            Parameters["cCode"].Value = cCode;
            Parameters["cName"].Value = cName;
            Parameters["dDate"].Value = DateTime.Now;
            
        }

    }
}
