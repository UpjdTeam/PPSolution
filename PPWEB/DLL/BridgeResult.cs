using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPWEB.DLL
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class HttpResult
    {
        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }
}