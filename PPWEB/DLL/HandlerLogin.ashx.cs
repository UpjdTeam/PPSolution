using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace PPWEB.DLL
{
    /// <summary>
    /// HandlerLogin 的摘要说明
    /// </summary>
    public class HandlerLogin : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {

                var cType = context.Request.Form["cType"];

                if (cType.Equals("JudgeAlreadyLogin"))
                {
                    if (HttpContext.Current.Session["UserName"]!=null && 
                        !string.IsNullOrEmpty(HttpContext.Current.Session["UserName"].ToString()))
                    {
                        var bR = new HttpResult
                        {
                            IsSuccess = true,
                            ErrorCode = 1,
                            ErrorMessage = HttpContext.Current.Session["UserName"].ToString()
                        };

                        context.Response.Write(bR.ToJson());
                        context.Response.End();
                    }
                    else
                    {
                        var bR = new HttpResult
                        {
                            IsSuccess = false,
                            ErrorCode = -1,
                            ErrorMessage = "未登录"
                        };

                        context.Response.Write(bR.ToJson());
                        context.Response.End();
                    }
                }
                else
                {
                    HttpContext.Current.Session["UserName"] = "UPJD";
                    var bR = new HttpResult
                    {
                        IsSuccess = true,
                        ErrorCode = 1,
                        ErrorMessage = "Success"
                    };


                    context.Response.Write(bR.ToJson());
                    context.Response.End();
                    return;
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