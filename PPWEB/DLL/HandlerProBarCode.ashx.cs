using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPWEB.DLL
{
    /// <summary>
    /// HandlerProBarCode 的摘要说明
    /// </summary>
    public class HandlerProBarCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {

                var cType = context.Request.Form["cType"];
                //更新产品档案
                if (cType.Equals("AddProBarCode"))
                {
                    var cBarCode = context.Request.Form["BarCode"];
                    var cInvCode = context.Request.Form["InvCode"];


                    var di = new DalProBarCode();
                    var bR = di.AddProBarCode(cBarCode,cInvCode);

                    context.Response.Write(bR.ToJson());
                }

                //查询物料
                if (cType.Equals("QueryProBarCode"))
                {
                    var cPageIndex = context.Request.Form["PageIndex"];
                    int iPageIndex;
                    if (!int.TryParse(cPageIndex, out iPageIndex))
                        return;

                    int rPageCount = 0, rRecordCount = 0;

                    var di = new DalProBarCode();
                    var dtProBarCode = di.QueryProBarCode(iPageIndex, 10, ref rPageCount, ref rRecordCount);

                    var invListItems = new List<ProBarCode>();

                    for (var i = 0; i < dtProBarCode.Rows.Count; i++)
                    {
                        var inv = new ProBarCode
                        {
                            RowNumber = (i + 1).ToString(),
                            BarCode = dtProBarCode.Rows[i]["cSerialNumber"].ToString(),
                            InvCode = dtProBarCode.Rows[i]["cInvCode"].ToString(),
                            InvName = dtProBarCode.Rows[i]["cInvName"].ToString(),
                            Memo = dtProBarCode.Rows[i]["cMemo"].ToString(),
                        };

                        invListItems.Add(inv);

                    }


                    var invListCollection = new ProBarCodeLists();
                    invListCollection.ProBarCodeItems = invListItems.ToArray();
                    invListCollection.PageCount = rPageCount;
                    invListCollection.RecordCount = rRecordCount;



                    context.Response.Write(invListCollection.ToJson());
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}