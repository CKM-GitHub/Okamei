using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class SmitumoriKanriBL
    {
        public bool ExistsDisplayResult(SmitumoriKanriModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@sagyou_sDate", SqlDbType.VarChar) { Value = model.sagyou_sDate.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@sagyou_eDate", SqlDbType.VarChar) { Value = model.sagyou_eDate.ToStringOrNull() };
            sqlParams[2] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("SmitumoriKanri_ExistsDisplayResult", sqlParams);
            return dt.Rows.Count > 0;
        }

        public DataTable GetDisplayResult(SmitumoriKanriModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[0];

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("SmitumoriKanri_SelectDisplayResult", sqlParams);
            return dt;
        }
    }
}
