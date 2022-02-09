using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class UnsouSoukoireBL
    {
        public bool ExistsDisplayResult(UnsouSoukoireModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[0];
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("UnsouSoukoire_SelectDisplayResult", sqlParams);
            return dt.Rows.Count > 0;
        }
        public DataTable GetDisplayResult(UnsouSoukoireModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[0];
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("UnsouSoukoire_SelectDisplayResult", sqlParams);
            return dt;
            
        }

    }
}
