using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace PPWEB.DLL
{

    /// <summary>
    /// 入库明细
    /// </summary>
    public class StoreDetail
    {
        [DataMember]
        public string RowNumber { get; set; }

        [DataMember]
        public string BarCode { get; set; }

        [DataMember]
        public string InvCode { get; set; }

        [DataMember]
        public string InvName { get; set; }
        [DataMember]
        public string Qty { get; set; }

        [DataMember]
        public string FromId { get; set; }

        [DataMember]
        public string ScanTime { get; set; }

        
    }
}