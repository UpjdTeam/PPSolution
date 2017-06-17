using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WxBridge
{
    public class MFunction
    {
        private readonly string _constr;

        public MFunction()
        {
            _constr = Properties.Settings.Default.SqlCon;
        }



        /// <summary>
        /// 查询MSSQL数据库数据，并返回DataTable
        /// </summary>
        /// <param name="cmd">查询命令</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetSqlTableWithUsing(SqlCommand cmd)
        {
            using (var con = new SqlConnection(_constr))
            {
                cmd.Connection = con;
                using (cmd)
                {
                    var dt = new DataTable("SqlTable");
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }
        }




        /// <summary>
        /// 执行MSSQl数据库查询，返回首个结果
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <returns>首个结果</returns>
        public string ReturnFirstResult(SqlCommand cmd)
        {
            using (var con = new SqlConnection(_constr))
            {
                cmd.Connection = con;
                con.Open();
                using (cmd)
                {
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }



        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <returns>影响行数大于1，则成功，否则失败</returns>
        public bool ExecProcWithUsing(SqlCommand cmd)
        {
            using (var con = new SqlConnection(_constr))
            {
                cmd.Connection = con;
                using (cmd)
                {
                    try
                    {
                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
        }



    }
}