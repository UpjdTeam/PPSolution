using System;
using System.Collections.Generic;
using System.Data;
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
                    //更新产品档案
                    if (cType.Equals("Update"))
                    {
                        var cInvCode = context.Request.Form["InvCode"];
                        var cInvName = context.Request.Form["InvName"];
                        var cInvStd = context.Request.Form["InvStd"];
                        var cMemo = context.Request.Form["Memo"];
                        var iWeight = context.Request.Form["Weight"];
                        var iBoxQty = context.Request.Form["BoxQty"];


                        var di = new DalInventory();
                        var bR = di.UpdateInventory(cInvCode, cInvName, cInvStd, decimal.Parse(iWeight), int.Parse(iBoxQty), cMemo);

                        context.Response.Write(bR.ToJson());
                    }
                    //查询物料
                    if (cType.Equals("QueryInventory"))
                    {
                        var cPageIndex = context.Request.Form["PageIndex"];
                        int iPageIndex;
                        if (!int.TryParse(cPageIndex, out iPageIndex))
                            return;

                        int rPageCount=0, rRecordCount=0;

                        var di = new DalInventory();
                        var dtInventory = di.QueryInventory(iPageIndex, 10,ref rPageCount,ref rRecordCount);

                        var invListItems = new List<Inventory>();

                        for (var i = 0; i < dtInventory.Rows.Count; i++)
                        {
                            var inv = new Inventory
                            {
                                RowNumber = (i + 1).ToString(),
                                InvCode = dtInventory.Rows[i]["cInvCode"].ToString(),
                                InvName = dtInventory.Rows[i]["cInvName"].ToString(),
                                InvStd = dtInventory.Rows[i]["cInvStd"] == DBNull.Value ? "" : dtInventory.Rows[i]["cInvStd"].ToString(),
                                Weight = dtInventory.Rows[i]["iWeight"].ToString(),
                                BoxQty = dtInventory.Rows[i]["iBoxQty"].ToString(),
                                Memo = dtInventory.Rows[i]["cMemo"].ToString(),
                                AddTime = dtInventory.Rows[i]["dAddTime"].ToString(),
                                UpdateTime = dtInventory.Rows[i]["dUpdateTime"] == DBNull.Value ? "" : dtInventory.Rows[i]["dUpdateTime"].ToString()
                            };

                            invListItems.Add(inv);

                        }


                        var invListCollection=new InventoryLists();
                        invListCollection.InventoryItems = invListItems.ToArray();
                        invListCollection.PageCount = rPageCount;
                        invListCollection.RecordCount = rRecordCount;



                        context.Response.Write(invListCollection.ToJson());
                    }
                    //获取所有物料
                    if (cType.Equals("QueryInventoryAll"))
                    {
                       
                        var di = new DalInventory();
                        var dtInventory = di.QueryInventoryAll();

                        var invListItems = new List<Inventory>();

                        for (var i = 0; i < dtInventory.Rows.Count; i++)
                        {
                            var inv = new Inventory
                            {
                                RowNumber = (i + 1).ToString(),
                                InvCode = dtInventory.Rows[i]["cInvCode"].ToString(),
                                InvName = dtInventory.Rows[i]["cInvName"].ToString(),
                                InvStd = dtInventory.Rows[i]["cInvStd"] == DBNull.Value ? "" : dtInventory.Rows[i]["cInvStd"].ToString(),
                                Weight = dtInventory.Rows[i]["iWeight"].ToString(),
                                BoxQty = dtInventory.Rows[i]["iBoxQty"].ToString(),
                                Memo = dtInventory.Rows[i]["cMemo"].ToString(),
                                AddTime = dtInventory.Rows[i]["dAddTime"].ToString(),
                                UpdateTime = dtInventory.Rows[i]["dUpdateTime"] == DBNull.Value ? "" : dtInventory.Rows[i]["dUpdateTime"].ToString()
                            };

                            invListItems.Add(inv);

                        }


                        var invListCollection = new InventoryLists();
                        invListCollection.InventoryItems = invListItems.ToArray();
                        invListCollection.PageCount = 1;
                        invListCollection.RecordCount = dtInventory.Rows.Count;



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
            else
            {
                var cType = context.Request.QueryString["Action"];
                if (cType != null && cType == "Excel")
                {
                    ExportToExcel(context);
                }
            }
        }

        private void ExportToExcel(HttpContext context)
        {

            var cPageIndex = context.Request.QueryString["PageIndex"];
            int iPageIndex;
            if (!int.TryParse(cPageIndex, out iPageIndex))
                return;

            HttpResponse resp = System.Web.HttpContext.Current.Response;
            resp.Charset = "utf-8";
            resp.Clear();
            string filename = "产品档案_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            resp.ContentEncoding = System.Text.Encoding.UTF8;

            resp.ContentType = "application/ms-excel";
            string style = "<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=utf-8\"/>" + "<style> .table{ font: 9pt Tahoma, Verdana; color: #000000; text-align:center;  background-color:#8ECBE8;  }.table td{text-align:center;height:45px;background-color:#EFF6FF;}.table th{ font: 9pt Tahoma, Verdana; color: #000000; font-weight: bold; background-color: #8ECBEA; height:35px;  text-align:center; padding-left:10px;}</style>";
            resp.Write(style);

            resp.Write("<table class='table'><tr><th>产品编号</th><th>产品名称</th><th>产品规格</th><th>重量</th><th>箱规</th><th>备注</th><th>添加时间</th><th>更新时间</th></tr>");

            int rPageCount = 0, rRecordCount = 0;

            var di = new DalInventory();
            var dtInventory = di.QueryInventory(iPageIndex, 10, ref rPageCount, ref rRecordCount);
            foreach (DataRow tmpRow in dtInventory.Rows)
            {
                resp.Write("<tr><td>" + tmpRow["cInvCode"] + "</td>");
                resp.Write("<td>" + tmpRow["cInvName"] + "</td>");
                resp.Write("<td>" + tmpRow["cInvStd"] + "</td>");
                resp.Write("<td>" + tmpRow["iWeight"] + "</td>");
                resp.Write("<td>" + tmpRow["iBoxQty"] + "</td>");
                resp.Write("<td>" + tmpRow["cMemo"] + "</td>");
                resp.Write("<td>" + tmpRow["dAddTime"] + "</td>");
                resp.Write("<td>" + tmpRow["dUpdateTime"] + "</td>");
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