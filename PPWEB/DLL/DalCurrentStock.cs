using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPWEB.DLL
{
    public class DalCurrentStock
    {
        public static DataTable GetCurrentStock()
        {
            var cmd = new SqlCommand("spLocal_GetCurrentStock");
            cmd.CommandType = CommandType.StoredProcedure;
            var msf = new MSSQLFunction();
            return msf.GetSqlTableWithUsing(cmd);
        }
    }
}