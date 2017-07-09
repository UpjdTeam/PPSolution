using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPWEB.DLL
{
    public class DalDeliveryDetail
    {
        public static DataTable GetDeliveryDetail()
        {
            var cmd = new SqlCommand("spLocal_GetDeliveryDetail");
            cmd.CommandType = CommandType.StoredProcedure;
            var msf = new MSSQLFunction();
            return msf.GetSqlTableWithUsing(cmd);
        }
    }
}