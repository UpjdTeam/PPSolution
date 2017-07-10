using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPBLOG.dll
{
    public class DalLogViewer
    {
        public void AddLogViewer(string cIp, string cCity, string cPage, string cBlogVersion)
        {
            var cmd = new SqlCommand("Usp_Add_LogViewer") {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@cIP", cIp);
            cmd.Parameters.AddWithValue("@cCity", cCity);
            cmd.Parameters.AddWithValue("@cPage", cPage);
            cmd.Parameters.AddWithValue("@cBlogVersion", cBlogVersion);
            var msf = new MSSQLFunction();
            msf.ExecSqlCmdWithUsing(cmd);
        }
    }
}