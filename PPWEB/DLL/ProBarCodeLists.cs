using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPWEB.DLL
{
    public class ProBarCodeLists
    {
        [DataMember]
        public ProBarCode[] ProBarCodeItems { get; set; }


        [DataMember]
        public int Count
        {
            get
            {
                if (ProBarCodeItems != null && ProBarCodeItems.Any()) return ProBarCodeItems.Count();

                return 0;
            }

        }

        [DataMember]
        public int PageCount;

        [DataMember]
        public int RecordCount;

        public ProBarCode Item(int index)
        {
            if (ProBarCodeItems != null && ProBarCodeItems.Any()) return ProBarCodeItems.ElementAt(index);

            return null;
        }

    }
}