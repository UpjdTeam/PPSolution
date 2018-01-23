using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPWEB.DLL
{
    public class InventoryLists
    {
        [DataMember]
        public Inventory[] InventoryItems { get; set; }


        [DataMember]
        public int Count
        {
            get
            {
                if (InventoryItems != null && InventoryItems.Any()) return InventoryItems.Count();

                return 0;
            }

        }

        [DataMember]
        public int PageCount;

        [DataMember]
        public int RecordCount;

        public Inventory Item(int index)
        {
            if (InventoryItems != null && InventoryItems.Any()) return InventoryItems.ElementAt(index);

            return null;
        }

    }
}