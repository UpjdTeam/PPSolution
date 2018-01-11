using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPBLOG.dll
{
    /// <summary>
    /// HandlerLogViewer 的摘要说明
    /// </summary>
    public class HandlerLogViewer : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var cIP = context.Request.Form["IP"];
                var cCity = context.Request.Form["City"];
                var cPage = context.Request.Form["Page"];
                var cBlogVersion = context.Request.Form["BlogVersion"];
                var dlv=new DalLogViewer();
                dlv.AddLogViewer(cIP,cCity,cPage,cBlogVersion);
            }
            catch (Exception ex)
            {
                
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