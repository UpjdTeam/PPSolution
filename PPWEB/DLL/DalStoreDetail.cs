using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPWEB.DLL
{
    public class DalStoreDetail
    {
        public static DataTable GetStoreDetail()
        {
            var cmd = new SqlCommand("spLocal_GetStoreDetail");
            cmd.CommandType = CommandType.StoredProcedure;
            var msf = new MSSQLFunction();
            return msf.GetSqlTableWithUsing(cmd);
        }
    }
}