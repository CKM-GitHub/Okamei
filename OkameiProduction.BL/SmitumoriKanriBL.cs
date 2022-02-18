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
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@sagyou_sDate", SqlDbType.VarChar) { Value = model.sagyou_sDate.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@sagyou_eDate", SqlDbType.VarChar) { Value = model.sagyou_eDate.ToStringOrNull() };
            sqlParams[2] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("SmitumoriKanri_ExistsDisplayResult", sqlParams);
            return dt.Rows.Count > 0;
        }

        public DataTable GetDisplayResult(SmitumoriKanriModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@sagyou_sDate", SqlDbType.VarChar) { Value = model.sagyou_sDate.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@sagyou_eDate", SqlDbType.VarChar) { Value = model.sagyou_eDate.ToStringOrNull() };
            sqlParams[2] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("SmitumoriKanri_SelectDisplayResult", sqlParams);
            return dt;
        }

        public bool UserIDCheck(SmitumoriKanriModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@ID", SqlDbType.VarChar) { Value = "015" };
            sqlParams[1] = new SqlParameter("@Key", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("M_MultiPorpose_SelectByIDKey", sqlParams);
            return dt.Rows.Count > 0;
        }

        public bool DeleteSMitumoriCounterData(SmitumoriKanriModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@SMitumoriCounter", SqlDbType.VarChar) { Value = model.SMitumoriCounter.ToStringOrNull() };

            DBAccess db = new DBAccess();
            return db.InsertUpdateDeleteData("SmitumoriKanri_DeleteDataResult", true, sqlParams);
        }
    }
}
