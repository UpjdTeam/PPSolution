﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WxBridge
{
    public class Signature
    {
         /// <summary> 
         /// 在网站没有提供Token（或传入为null）的情况下的默认Token，建议在网站中进行配置。 
         /// </summary> 
        public const string Token = "Paper65"; 
         /// <summary> 
         /// 在网站没有提供EncodingAESKey（或传入为null）的情况下的默认Token，建议在网站中进行配置。 
         /// </summary> 
        public const string EncodingAESKey = "e0iDl1ikBn4A7zop1okekHEi8pWScszuu5QDaoJF8FT"; 
         /// <summary> 
         /// 在网站没有提供CorpId（或传入为null）的情况下的默认Token，建议在网站中进行配置。 
         /// </summary> 
         public const string CorpId = "wxff7081edfbdbe841"; 
 

         /// <summary> 
         /// 获取签名 
         /// </summary> 
         /// <param name="token"></param> 
         /// <param name="timeStamp"></param> 
         /// <param name="nonce"></param> 
         /// <param name="msgEncrypt"></param> 
         /// <returns></returns> 
         public static string GenarateSinature(string token, string timeStamp, string nonce, string msgEncrypt) 
         { 
             string msgSignature = null; 
             var result = WXBizMsgCrypt.GenarateSinature(token, timeStamp, nonce, msgEncrypt, ref msgSignature); 
             return result == 0 ? msgSignature : result.ToString(); 
         } 
 

         /// <summary> 
         /// 检查签名 
         /// </summary> 
         /// <param name="token"></param> 
         /// <param name="encodingAESKey"></param> 
         /// <param name="corpId"></param> 
         /// <param name="msgSignature">签名串，对应URL参数的msg_signature</param> 
         /// <param name="timeStamp">时间戳，对应URL参数的timestamp</param> 
         /// <param name="nonce">随机串，对应URL参数的nonce</param> 
         /// <param name="echoStr">随机串，对应URL参数的echostr</param> 
         /// <returns></returns> 
         public static string VerifyURL(string token, string encodingAESKey, string corpId, string msgSignature, string timeStamp, string nonce, string echoStr) 
         { 
             WXBizMsgCrypt crypt = new WXBizMsgCrypt(token, encodingAESKey, corpId); 
             string replyEchoStr = null; 
             var result = crypt.VerifyURL(msgSignature, timeStamp, nonce, echoStr, ref replyEchoStr); 
             if (result == 0) 
             { 
                 //验证成功，比较随机字符串 
                 return replyEchoStr; 
             } 
             else 
             { 
                 //验证错误，这里可以分析具体的错误信息 
                
                 return null; 
             } 


         }

 

         /// <summary> 
         /// 加密消息 
         /// </summary> 
         /// <param name="token"></param> 
         /// <param name="encodingAESKey"></param> 
         /// <param name="corpId"></param> 
         /// <param name="replyMsg"></param> 
         /// <param name="timeStamp"></param> 
         /// <param name="nonce"></param> 
         /// <returns></returns> 
         public static string EncryptMsg(string token, string encodingAESKey, string corpId, string replyMsg, string timeStamp, string nonce) 
         { 
             WXBizMsgCrypt crypt = new WXBizMsgCrypt(token, encodingAESKey, corpId); 
             string encryptMsg = null; 
             var result = crypt.EncryptMsg(replyMsg, timeStamp, nonce, ref encryptMsg); 
             return encryptMsg; 
         } 
     }
}