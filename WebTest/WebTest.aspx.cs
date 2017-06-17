using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebTest
{
    public partial class WebTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {




        }



        /// <summary>  
        /// 添加菜单  
        /// </summary>  
        /// <param name="accessToken"></param>  
        /// <param name="data"></param>  
        /// <returns></returns>  
        public static string AddMenu(string accessToken, string data)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", accessToken);
            HttpWebRequest hwr = WebRequest.Create(url) as HttpWebRequest;
            hwr.Method = "POST";
            hwr.ContentType = "application/x-www-form-urlencoded";
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(data);
            hwr.ContentLength = payload.Length;
            Stream writer = hwr.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            var result = hwr.GetResponse() as HttpWebResponse;
            StreamReader sr=new StreamReader(result.GetResponseStream(),System.Text.Encoding.Default);
            return sr.ReadToEnd();
        }

        protected void btnCreateMenu_Click(object sender, EventArgs e)
        {

            try
            {
                var mFile = new FileInfo(Server.MapPath("Menu.json"));
                var stR = mFile.OpenText();
                var strMenu = stR.ReadToEnd();
                stR.Close();


                var strMsg = AddMenu("sVhsuIhs3MWDfsPYV6a3PBz3vDd1JWtlPTDAnhpml42lPK2ZbvnWXNnxhEpbIyiDM3msAC7W0SSaNrZLLnm7SnLeZXj2cMLrZugniRua6AFqdca3tt3wtIYaPJP67EMbGJYaAEANQF", strMenu);
                Response.Write(strMsg);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }



        

    }
}