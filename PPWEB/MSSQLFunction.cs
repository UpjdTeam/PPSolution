using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPWEB
{
    public class MSSQLFunction
    {
        public string _constr;

        public MSSQLFunction()
        {
            _constr = Properties.Settings.Default.SqlCon;
        }

        /// <summary>
        /// Get Sql Table With Using,
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <returns>SQL Table</returns>
        public DataTable GetSqlTableWithUsing(SqlCommand cmd)
        {
            using (var con = new SqlConnection(_constr))
            {
                cmd.Connection = con;
                cmd.CommandTimeout = 600;
                using (cmd)
                {
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable("SqlTable");
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        /// <summary>
        /// Get Sql Table With Using,
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <returns>SQL Table</returns>
        public DataSet GetSqlDataSetWithUsing(SqlCommand cmd)
        {
            using (var con = new SqlConnection(_constr))
            {
                cmd.Connection = con;
                cmd.CommandTimeout = 600;
                using (cmd)
                {
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet("SqlDataSet");
                    da.Fill(ds);
                    return ds;
                }
            }
        }

        public int ExecSqlCmdWithUsing(SqlCommand cmd)
        {
            using (var con = new SqlConnection(_constr))
            {
                cmd.Connection = con;
                using (cmd)
                {
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


    }

}