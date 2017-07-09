using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPL
{
    class Program
    {
        static void Main(string[] args)
        {
            UpdateStoreDeliveryToCurrentDay();
        }


        private static void UpdateStoreDeliveryToCurrentDay()
        {
            using (var con = new SqlConnection(Properties.Settings.Default.Constr))
            {
                using (
                    var cmd = new SqlCommand("spLocal_Temp_UpdateStoreDeliveryToCurrentDay", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
