using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace OkameiProduction.BL
{
    public class HanyouKensakuBL
    {
        public DataTable GetDisplayResult(HanyouKensakuModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@ID", SqlDbType.VarChar) { Value = model.ID.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("HanyouKensaku_SelectDisplayResult", sqlParams);
            return dt;
        }
    }
}
