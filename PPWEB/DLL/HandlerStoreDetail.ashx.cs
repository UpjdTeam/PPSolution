using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PPWEB.DLL
{
    /// <summary>
    /// HandlerStoreDetail 的摘要说明
    /// </summary>
    public class HandlerStoreDetail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                var cType = context.Request.Form["cType"];
                //var cSerialNumber = context.Request.Form["cSerialNumber"];
                //var cArea = context.Request.Form["Area"];
                if (cType == "Query")
                {
                    QueryMain(context);
                }
                else if (cType == "QueryData")
                {
                    QueryByDate(context);
                }
                return;
            }
            else
            {
                var cAction = context.Request.QueryString["Action"];
                if (cAction != null && cAction == "Excel")
                {
                    ExportToExcel(context);
                }
            }
        }

        private void ExportToExcel(HttpContext context)
        {
            HttpResponse resp = System.Web.HttpContext.Current.Response;
            resp.Charset = "utf-8";
            resp.Clear();
            string filename = "入库明细表_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            resp.ContentEncoding = System.Text.Encoding.UTF8;

            resp.ContentType = "application/ms-excel";
            string style = "<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=utf-8\"/>" + "<style> .table{ font: 9pt Tahoma, Verdana; color: #000000; text-align:center;  background-color:#8ECBE8;  }.table td{text-align:center;height:45px;background-color:#EFF6FF;}.table th{ font: 9pt Tahoma, Verdana; color: #000000; font-weight: bold; background-color: #8ECBEA; height:35px;  text-align:center; padding-left:10px;}</style>";
            resp.Write(style);

            resp.Write("<table class='table'><tr><th>条码号</th><th>产品编码</th><th>产品名称</th><th>数量</th><th>操作员</th><th>操作时间</th></tr>");

            var dt = new DataTable("sqlTemp");

            DateTime dStart, dEnd;
            var cStart = context.Request.QueryString["dStart"];
            var cEnd = context.Request.QueryString["dEnd"];
            if (DateTime.TryParse(cStart, out dStart) && DateTime.TryParse(cEnd, out dEnd))
            {
                dt = DalStoreDetail.GetStoreDetailByDate(dStart, dEnd);
            }
            else
            {
                dt = DalStoreDetail.GetStoreDetail();
            }
            foreach (DataRow tmpRow in dt.Rows)
            {
                resp.Write("<tr><td style='mso-number-format:\"@\"'>" + tmpRow[0] + "</td>");
                resp.Write("<td>" + tmpRow[1] + "</td>");
                resp.Write("<td>" + tmpRow[2] + "</td>");
                resp.Write("<td>" + tmpRow[3] + "</td>");
                resp.Write("<td>" + tmpRow[4] + "</td>");
                resp.Write("<td>" + tmpRow[5] + "</td>");
                resp.Write("</tr>");
            }
            resp.Write("<table>");

            resp.Flush();
            resp.End();
        }


        private void QueryMain(HttpContext context)
        {
            IList<StoreDetail> list = new List<StoreDetail>();

            var dt = DalStoreDetail.GetStoreDetail();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var storeItem = new StoreDetail();
                storeItem.RowNumber =(i+1).ToString();
                storeItem.BarCode = dt.Rows[i]["cBarCode"].ToString();
                storeItem.InvCode = dt.Rows[i]["cInvCode"].ToString();

                storeItem.InvName = dt.Rows[i]["cInvName"].ToString();
                storeItem.Qty = dt.Rows[i]["iQuantity"].ToString();
                storeItem.FromId = dt.Rows[i]["cUser"].ToString();
                storeItem.ScanTime = dt.Rows[i]["dScanTime"].ToString();


                list.Add(storeItem);
            }

            context.Response.Write(list.ToJson());
            context.Response.End();
        }

        private void QueryByDate(HttpContext context)
        {
            IList<StoreDetail> list = new List<StoreDetail>();
            DateTime dStart, dEnd;
            var cStart = context.Request.Form["dStart"];
            var cEnd = context.Request.Form["dEnd"];
            if (!DateTime.TryParse(cStart, out dStart))
            {
                return;
            }
            if (!DateTime.TryParse(cEnd, out dEnd))
            {
                return;
            }

            var dt = DalStoreDetail.GetStoreDetailByDate(dStart,dEnd);
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var storeItem = new StoreDetail();
                storeItem.RowNumber = (i + 1).ToString();
                storeItem.BarCode = dt.Rows[i]["cBarCode"].ToString();
                storeItem.InvCode = dt.Rows[i]["cInvCode"].ToString();

                storeItem.InvName = dt.Rows[i]["cInvName"].ToString();
                storeItem.Qty = dt.Rows[i]["iQuantity"].ToString();
                storeItem.FromId = dt.Rows[i]["cUser"].ToString();
                storeItem.ScanTime = dt.Rows[i]["dScanTime"].ToString();


                list.Add(storeItem);
            }

            context.Response.Write(list.ToJson());
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}