using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class CadStaffMiteiBL
    {
        
        public DataTable GetDisplayResult(CadStaffMiteiModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[0];
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("CadStaffMitei_SelectDisplayResult", sqlParams);
            return dt;
            
        }

    }
}
