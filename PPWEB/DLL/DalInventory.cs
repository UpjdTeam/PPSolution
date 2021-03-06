﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPWEB.DLL
{
    public class DalInventory
    {
        public HttpResult UpdateInventory(string cInvCode,string cInvName,string cInvStd,
            decimal iWeight,int iBoxQty,string cMemo)
        {
            var mfun=new MSSQLFunction();
            var cmd = new SqlCommand("spLocal_UpdateInventory"){CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@cInvCode", cInvCode);
            cmd.Parameters.AddWithValue("@cInvName", cInvName);
            cmd.Parameters.AddWithValue("@cInvStd", cInvStd);
            cmd.Parameters.AddWithValue("@iWeight", iWeight);
            cmd.Parameters.AddWithValue("@iBoxQty", iBoxQty);
            cmd.Parameters.AddWithValue("@cMemo", cMemo);

            if (mfun.ExecSqlCmdWithUsing(cmd)>0)
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
                    ErrorMessage = "更新失败"
                };

                return bR;
            }
        }


        public DataTable QueryInventory(int iPageIndex, int iPageSize,ref int pageCount,ref int recordCount )
        {
            var mfun = new MSSQLFunction();
            var cmd = new SqlCommand("GetRecordPage") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@PageIndex", iPageIndex);
            cmd.Parameters.AddWithValue("@TableRecord", "Inventory");
            cmd.Parameters.AddWithValue("@fldName", "I_id");
            cmd.Parameters.AddWithValue("@PageSize", iPageSize);
            cmd.Parameters.AddWithValue("@strWhere", "");



            var pageCountPara = new SqlParameter("@PageCount",SqlDbType.Int);
            pageCountPara.Direction = ParameterDirection.Output;


            var recordCountPara = new SqlParameter("@RecordCount", SqlDbType.Int);
            recordCountPara.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(pageCountPara);
            cmd.Parameters.Add(recordCountPara);

            var tempTable=mfun.GetSqlTableWithUsing(cmd);
            pageCount = (int)pageCountPara.Value;
            recordCount = (int)recordCountPara.Value;


            return tempTable;


        }


        /// <summary>
        /// 获取所有物料档案
        /// </summary>
        /// <returns></returns>
        public DataTable QueryInventoryAll()
        {
            var mfun = new MSSQLFunction();
            var cmd = new SqlCommand("select * from Inventory");
            var tempTable = mfun.GetSqlTableWithUsing(cmd);
            return tempTable;


        }
    }
}