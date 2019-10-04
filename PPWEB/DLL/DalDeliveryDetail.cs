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
        public static DataTable GetDeliveryDetailByDate(DateTime dStart,DateTime dEnd)
        {
            var cmd = new SqlCommand("spLocal_GetDeliveryDetailByDate");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dStart", dStart);
            cmd.Parameters.AddWithValue("@dEnd", dEnd);
            var msf = new MSSQLFunction();
            return msf.GetSqlTableWithUsing(cmd);
        }
    }
}