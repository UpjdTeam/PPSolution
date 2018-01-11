using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace PPWEB
{
    public partial class XtraReportBarCode : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportBarCode(string cBarCode, string cInvName, string cInvStd)
        {
            
            InitializeComponent();
            xrBarCode1.Text = cBarCode;
            xlInvName.Text = cInvName;
            xlInvStd.Text = cInvStd;
        }

    }
}
