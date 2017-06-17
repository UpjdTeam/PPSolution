using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace WxBridge
{
    /// <summary>
    /// WxEnterprise 的摘要说明
    /// </summary>
    public class WxEnterprise : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                string postString;
                using (var stream = HttpContext.Current.Request.InputStream)
                {
                    var postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }

                if (!string.IsNullOrEmpty(postString))
                {
                    Execute(postString);
                }

            }
            else
            {
                Auth();
            }

        }

       


        private void Execute(string postStr)
        {

            var echoString = HttpContext.Current.Request.QueryString["echoStr"];
            var msgSignature = HttpContext.Current.Request.QueryString["msg_signature"];//企业号的 msg_signature
            var timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            var nonce = HttpContext.Current.Request.QueryString["nonce"];


            //根据参数信息，初始化微信对应的消息加密解密类
            var wxcpt = new WXBizMsgCrypt(Signature.Token, Signature.EncodingAESKey, Signature.CorpId);


            
            var sMsg = "";  // 解析之后的明文
            var flag = wxcpt.DecryptMsg(msgSignature, timestamp, nonce, postStr, ref sMsg);
            if (flag == 0)
            {
                //LogTextHelper.Info("记录解密后的数据:");
                DataSubmit("0DDD001", "解密", "解密:" + sMsg);

                try
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(sMsg);
                    var rootElement = xmlDoc.DocumentElement;
                    if (rootElement == null)
                    {
                        return;
                    }
                    var selectSingleNode = rootElement.SelectSingleNode("ToUserName");
                    if (selectSingleNode != null)
                    {
                        var toUserName = selectSingleNode.InnerText;
                        var fromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                        var eventKey = rootElement.SelectSingleNode("EventKey").InnerText;

                        


                        if (eventKey.Equals("scan_store_1"))
                        {

                        

                            var xmlMsg = @"<xml><ToUserName><![CDATA[" + fromUserName + @"]]></ToUserName>
                                <FromUserName><![CDATA[" + toUserName + @"]]></FromUserName>
                                <CreateTime>1441848212</CreateTime>
                                <MsgType><![CDATA[text]]></MsgType>
                                <Content><![CDATA[" + "今日入库总数10" + @"]]></Content>
                                <MsgId>4385226390207725595</MsgId>
                                <AgentID>1</AgentID>
                                </xml>";
                            DataSubmit("0DDD001", "发送", "发送:" + xmlMsg);
                            //加密后并发送
                            //LogTextHelper.Info(responseContent);
                            string encryptResponse = "";
                            wxcpt.EncryptMsg(xmlMsg, timestamp, nonce, ref encryptResponse);



                            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                            HttpContext.Current.Response.Write(encryptResponse);
                            DataSubmit("0DDD002", "发送", "发送:" + encryptResponse);
                        }
                        else if (eventKey.Equals("scan_store_0"))
                        {
                            var sResult = rootElement.SelectSingleNode("ScanCodeInfo").ChildNodes[1].InnerText;

                            if (sResult.Contains(","))
                            {
                                sResult = sResult.Substring(sResult.IndexOf(',')+1, sResult.Length - sResult.IndexOf(',')-1);
                            }
                            var psa=new ProStoreAdapter();
                            var gR = psa.AddProStore(sResult, fromUserName);

                            var xmlMsg = @"<xml><ToUserName><![CDATA[" + fromUserName + @"]]></ToUserName>
                                <FromUserName><![CDATA[" + toUserName + @"]]></FromUserName>
                                <CreateTime>1441848212</CreateTime>
                                <MsgType><![CDATA[text]]></MsgType>
                                <Content><![CDATA[" + gR + @"]]></Content>
                                <MsgId>4385226390207725595</MsgId>
                                <AgentID>1</AgentID>
                                </xml>";
                            DataSubmit("0DDD001", "发送", "发送:" + xmlMsg);
                            //加密后并发送
                            //LogTextHelper.Info(responseContent);
                            string encryptResponse = "";
                            wxcpt.EncryptMsg(xmlMsg, timestamp, nonce, ref encryptResponse);



                            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                            HttpContext.Current.Response.Write(encryptResponse);
                            DataSubmit("0DDD002", "发送", "发送:" + encryptResponse);
                        }
                        
                    }
                }
                catch (Exception ex)
                {

                    DataSubmit("04", "发送", "发送消息失败！");
                }
                
            }
            else
            {
                DataSubmit("05", "解密", "解密消息失败！");
            }
        }

        /// <summary>
        /// 成为开发者的第一步，验证并相应服务器的数据
        /// </summary>
        private void Auth()
        {
            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string Strsignature = HttpContext.Current.Request.QueryString["msg_signature"];//企业号的 msg_signature
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];

            string decryptEchoString = Signature.VerifyURL(Signature.Token, Signature.EncodingAESKey, Signature.CorpId, Strsignature, timestamp, nonce, echoString);
            if (!string.IsNullOrEmpty(decryptEchoString))
            {

                HttpContext.Current.Response.Write(decryptEchoString);
                HttpContext.Current.Response.End();
            }
        }



        private void DataSubmit(string FromUser, string ToUser, string Content)
        {
            using (var con = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                using (
                    var cmd =
                        new SqlCommand(
                            "insert into T_Submit(FromUser,ToUser,dAddTime,cContent)  Values(@FromUser,@ToUser,getdate(),@cContent)")
                    )
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@FromUser", FromUser);
                    cmd.Parameters.AddWithValue("@ToUser", ToUser);
                    cmd.Parameters.AddWithValue("@cContent", Content);
                    con.Open();
                    cmd.ExecuteNonQuery();
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