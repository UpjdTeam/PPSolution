using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WxBridge
{
    public class ProStoreAdapter
    {
        public string AddProStore(string cBarCode, string fromUserName)
        {
            try
            {

                var cmd = new SqlCommand("AddProStore") { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@cBarCode", cBarCode);
                cmd.Parameters.AddWithValue("@fromUserName", fromUserName);
                var mfun = new MFunction();
                var dResult = mfun.GetSqlTableWithUsing(cmd);
                return dResult.Rows[0]["Msg"].ToString();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }


        public string GetProStoreSummary()
        {
            try
            {

                var cmd = new SqlCommand("Usp_GetStoreSummary") { CommandType = CommandType.StoredProcedure };
                var mfun = new MFunction();
                var dResult = mfun.GetSqlTableWithUsing(cmd);
                if (dResult == null || dResult.Rows.Count < 1)
                {
                    return "今日无入库";
                }

                var cResult="";
                for (var i = 0; i < dResult.Rows.Count; i++)
                {
                    cResult = cResult + dResult.Rows[i]["cUser"] + "入库:" + dResult.Rows[i]["CNT"] + " ; ";
                }

                return cResult;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}