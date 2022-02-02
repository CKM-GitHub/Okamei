using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class MoulderItiranBL
    {
        public bool ExistsDisplayResult(MoulderItiranModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@TantouSitenCD", SqlDbType.VarChar) { Value = model.SitenCD.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@NoukiStart", SqlDbType.Date) { Value = model.NoukiStart.ToDateTime() };
            sqlParams[2] = new SqlParameter("@NoukiEnd", SqlDbType.Date) { Value = model.NoukiEnd.ToDateTime() };
            sqlParams[3] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("MoulderItiran_ExistsDisplayResult", sqlParams);
            return dt.Rows.Count > 0;
        }

        public DataTable GetDisplayResult(MoulderItiranModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@TantouSitenCD", SqlDbType.VarChar) { Value = model.SitenCD.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@NoukiStart", SqlDbType.Date) { Value = model.NoukiStart.ToDateTime() };
            sqlParams[2] = new SqlParameter("@NoukiEnd", SqlDbType.Date) { Value = model.NoukiEnd.ToDateTime() };
            sqlParams[3] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("MoulderItiran_SelectDisplayResult", sqlParams);
            return dt;
        }
    }
}
