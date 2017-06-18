using System;
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
    }
}