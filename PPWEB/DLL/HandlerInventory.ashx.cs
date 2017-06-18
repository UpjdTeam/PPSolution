using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPWEB.DLL
{
    /// <summary>
    /// HandlerInventory 的摘要说明
    /// </summary>
    public class HandlerInventory : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                try
                {
                    var cType = context.Request.Form["cType"];
                    var cInvCode = context.Request.Form["InvCode"];
                    var cInvName = context.Request.Form["InvName"];
                    var cInvStd = context.Request.Form["InvStd"];
                    var cMemo = context.Request.Form["Memo"];
                    var iWeight = context.Request.Form["Weight"];
                    var iBoxQty = context.Request.Form["BoxQty"];

                    if (cType.Equals("Update"))
                    {
                        var di = new DalInventory();
                        var bR = di.UpdateInventory(cInvCode, cInvName, cInvStd, decimal.Parse(iWeight), int.Parse(iBoxQty), cMemo);

                        context.Response.Write(bR.ToJson());
                    }
                }
                catch (Exception ex)
                {
                    var bR = new HttpResult
                    {
                        IsSuccess = false,
                        ErrorCode = -1,
                        ErrorMessage = ex.Message
                    };

                    context.Response.Write(bR.ToJson());
                }
                

            }
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