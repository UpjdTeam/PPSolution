using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WxBridge
{
    public class ProDeliveryAdapter
    {
        public string AddProDelivery(string cBarCode, string fromUserName)
        {
            try
            {

                var cmd = new SqlCommand("AddProDelivery") { CommandType = CommandType.StoredProcedure };
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


        public string GetProDeliverySummary()
        {
            try
            {

                var cmd = new SqlCommand("Usp_GetDeliverySummary") { CommandType = CommandType.StoredProcedure };
                var mfun = new MFunction();
                var dResult = mfun.GetSqlTableWithUsing(cmd);
                if (dResult == null || dResult.Rows.Count < 1)
                {
                    return "今日无出库";
                }

                var cResult = "";
                for (var i = 0; i < dResult.Rows.Count; i++)
                {
                    cResult = cResult + dResult.Rows[i]["cUser"] + "出库:" + dResult.Rows[i]["CNT"] + " ; ";
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