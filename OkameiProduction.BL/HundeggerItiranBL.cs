using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class HundeggerItiranBL
    {
       

        public DataTable GetDisplayResult(HundeggerItiranModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[0];
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("HundeggerItiran_SelectDisplayResult", sqlParams);
            return dt;
        }
    }
}
