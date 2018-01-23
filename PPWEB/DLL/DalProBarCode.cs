using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPWEB.DLL
{
    public class DalProBarCode
    {
        public HttpResult AddProBarCode(string cBarCode,string cInvCode)
        {
            var mfun = new MSSQLFunction();
            var cmd = new SqlCommand("spLocal_AddProBarCode") { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@cBarCode", cBarCode);
            cmd.Parameters.AddWithValue("@cInvCode", cInvCode);

            if (mfun.ExecSqlCmdWithUsing(cmd) > 0)
            {
                var bR = new HttpResult
                {
                    IsSuccess = true,
                    ErrorCode = 1,
                    ErrorMessage = "成功"
                };
                return bR;
            }
            else
            {
                var bR = new HttpResult
                {
                    IsSuccess = false,
                    ErrorCode = -1,
                    ErrorMessage = "条码已经存在"
                };

                return bR;
            }
        }

        public DataTable QueryProBarCode(int iPageIndex, int iPageSize, ref int pageCount, ref int recordCount)
        {
            var mfun = new MSSQLFunction();
            var cmd = new SqlCommand("GetRecordPage") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@PageIndex", iPageIndex);
            cmd.Parameters.AddWithValue("@TableRecord", "ProBarCode");
            cmd.Parameters.AddWithValue("@fldName", "AutoId");
            cmd.Parameters.AddWithValue("@PageSize", iPageSize);
            cmd.Parameters.AddWithValue("@strWhere", "");



            var pageCountPara = new SqlParameter("@PageCount", SqlDbType.Int);
            pageCountPara.Direction = ParameterDirection.Output;


            var recordCountPara = new SqlParameter("@RecordCount", SqlDbType.Int);
            recordCountPara.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(pageCountPara);
            cmd.Parameters.Add(recordCountPara);

            var tempTable = mfun.GetSqlTableWithUsing(cmd);
            pageCount = (int)pageCountPara.Value;
            recordCount = (int)recordCountPara.Value;


            return tempTable;


        }
    }
}