using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class HanyouKensakuBL
    {
        public DataTable GetDisplayResult(HanyouKensakuModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@GeneralPurposeCD", SqlDbType.VarChar) { Value = model.GeneralPurposeCD.ToStringOrNull() };
            sqlParams[3] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("HanyouKensaku_SelectDisplayResult", sqlParams);
            return dt;
        }
    }
}
