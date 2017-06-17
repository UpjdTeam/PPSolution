using System;
using System.Collections.Generic;
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
                //var cType = context.Request.Form["cType"];
                //var cSerialNumber = context.Request.Form["cSerialNumber"];
                //var cArea = context.Request.Form["Area"];
                
                QueryMain(context);
                return;
            }
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}