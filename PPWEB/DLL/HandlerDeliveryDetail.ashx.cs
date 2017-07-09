using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPWEB.DLL
{
    /// <summary>
    /// HandlerStoreDetail 的摘要说明
    /// </summary>
    public class HandlerDeliveryDetail : IHttpHandler
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
            IList<DeliveryDetail> list = new List<DeliveryDetail>();

            var dt = DalDeliveryDetail.GetDeliveryDetail();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var deliveryItem = new DeliveryDetail();
                deliveryItem.RowNumber =(i+1).ToString();
                deliveryItem.BarCode = dt.Rows[i]["cBarCode"].ToString();
                deliveryItem.InvCode = dt.Rows[i]["cInvCode"].ToString();

                deliveryItem.InvName = dt.Rows[i]["cInvName"].ToString();
                deliveryItem.Qty = dt.Rows[i]["iQuantity"].ToString();
                deliveryItem.FromId = dt.Rows[i]["cUser"].ToString();
                deliveryItem.ScanTime = dt.Rows[i]["dScanTime"].ToString();


                list.Add(deliveryItem);
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