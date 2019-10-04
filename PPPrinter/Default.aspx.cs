using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPPrinter
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["report2"] != null)
            {
                ASPxDocumentViewer1.Report = Session["report2"] as XtraReport;
            }
            var cBarCode = Request.QueryString["cBarCode"];
            var cCode = Request.QueryString["cCode"];
            var cName = Request.QueryString["cName"];

            var xreport1 = new XReport(cBarCode, cCode, cName);
            xreport1.CreateDocument();
            ASPxDocumentViewer1.ReportTypeName = "PPPrinter.XReport";
            ASPxDocumentViewer1.Report = xreport1;
            ASPxDocumentViewer1.DataBind();
            Session["report2"] = xreport1;

        }
    }
}