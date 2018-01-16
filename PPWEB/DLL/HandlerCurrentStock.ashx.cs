using System;
using System.Collections.Generic;
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}