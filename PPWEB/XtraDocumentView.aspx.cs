using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPWEB
{
    public partial class XtraDocumentView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBarCode.Text.Trim()))
                return;
            var xtraRpt = new XtraReportBarCode(txtBarCode.Text.Trim(), txtInvName.Text.Trim(), txtInvStd.Text.Trim());
            xtraRpt.RequestParameters = false;
            xtraRpt.CreateDocument();
            DevXtraDocument.ReportTypeName = "PPWEB.XtraReportBarCode";
            DevXtraDocument.Report = xtraRpt;
            DevXtraDocument.DataBind();
        }

    }
}