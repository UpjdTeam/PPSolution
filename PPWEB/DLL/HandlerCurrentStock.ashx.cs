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
    public class HandlerCurrentStock : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                
                QueryMain(context);
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

        private void QueryMain(HttpContext context)
        {
            IList<CurrentStock> list = new List<CurrentStock>();

            var dt = DalCurrentStock.GetCurrentStock();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var currentStockItem = new CurrentStock();
                currentStockItem.RowNumber = (i + 1).ToString();
                currentStockItem.InvCode = dt.Rows[i]["cInvCode"].ToString();
                currentStockItem.InvName = dt.Rows[i]["cInvName"].ToString();
                currentStockItem.Quantity = dt.Rows[i]["iQuantity"].ToString();
                list.Add(currentStockItem);
            }

            context.Response.Write(list.ToJson());
            context.Response.End();
        }

        private void ExportToExcel(HttpContext context)
        {
            HttpResponse resp = System.Web.HttpContext.Current.Response;
            resp.Charset = "utf-8";
            resp.Clear();
            string filename = "现存量明细表_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            resp.ContentEncoding = System.Text.Encoding.UTF8;

            resp.ContentType = "application/ms-excel";
            string style = "<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=utf-8\"/>" + "<style> .table{ font: 9pt Tahoma, Verdana; color: #000000; text-align:center;  background-color:#8ECBE8;  }.table td{text-align:center;height:45px;background-color:#EFF6FF;}.table th{ font: 9pt Tahoma, Verdana; color: #000000; font-weight: bold; background-color: #8ECBEA; height:35px;  text-align:center; padding-left:10px;}</style>";
            resp.Write(style);

            resp.Write("<table class='table'><tr><th>产品编码</th><th>产品名称</th><th>数量</th></tr>");

            var dt = DalCurrentStock.GetCurrentStock();
            foreach (DataRow tmpRow in dt.Rows)
            {
                resp.Write("<tr><td>" + tmpRow["cInvCode"] + "</td>");
                resp.Write("<td>" + tmpRow["cInvName"] + "</td>");
                resp.Write("<td>" + tmpRow["iQuantity"] + "</td>");
                resp.Write("</tr>");
            }
            resp.Write("<table>");

            resp.Flush();
            resp.End();
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