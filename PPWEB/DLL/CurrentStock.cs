using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPWEB.DLL
{
    /// <summary>
    /// 现存量表
    /// </summary>
    public class CurrentStock
    {
        [DataMember]
        public string RowNumber { get; set; }

        [DataMember]
        public string InvCode { get; set; }

        [DataMember]
        public string InvName { get; set; }

        [DataMember]
        public string Quantity { get; set; }


    }
}