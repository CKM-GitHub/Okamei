using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace OkameiProduction.BL
{
    public class HanyouMasterMaintenanceBL
    {
        public DataTable CheckDataResult(HanyouMasterMaintenanceModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@ID", SqlDbType.VarChar) { Value = model.ID.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@Key", SqlDbType.VarChar) { Value = model.Key.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("HanyouMasterMaintenance_CheckDataResult", sqlParams);
            return dt;
        }
        public DataTable ModifyData(HanyouMasterMaintenanceModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[20];
            sqlParams[0] = new SqlParameter("@ID", SqlDbType.VarChar) { Value = model.ID.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@Key", SqlDbType.VarChar) { Value = model.Key.ToStringOrNull() };
            sqlParams[2] = new SqlParameter("@ID_name", SqlDbType.VarChar) { Value = model.ID_name.ToStringOrNull() };
            sqlParams[3] = new SqlParameter("@Char1", SqlDbType.VarChar) { Value = model.Char1.ToStringOrNull() };
            sqlParams[4] = new SqlParameter("@Char2", SqlDbType.VarChar) { Value = model.Char2.ToStringOrNull() };
            sqlParams[5] = new SqlParameter("@Char3", SqlDbType.VarChar) { Value = model.Char3.ToStringOrNull() };
            sqlParams[6] = new SqlParameter("@Char4", SqlDbType.VarChar) { Value = model.Char4.ToStringOrNull() };
            sqlParams[7] = new SqlParameter("@Char5", SqlDbType.VarChar) { Value = model.Char5.ToStringOrNull() };
            sqlParams[8] = new SqlParameter("@Num1", SqlDbType.VarChar) { Value = model.Num1.ToStringOrNull() };
            sqlParams[9] = new SqlParameter("@Num2", SqlDbType.VarChar) { Value = model.Num2.ToStringOrNull() };
            sqlParams[10] = new SqlParameter("@Num3", SqlDbType.VarChar) { Value = model.Num3.ToStringOrNull() };
            sqlParams[11] = new SqlParameter("@Num4", SqlDbType.VarChar) { Value = model.Num4.ToStringOrNull() };
            sqlParams[12] = new SqlParameter("@Num5", SqlDbType.VarChar) { Value = model.Num5.ToStringOrNull() };
            sqlParams[13] = new SqlParameter("@Date1", SqlDbType.VarChar) { Value = model.Date1.ToStringOrNull() };
            sqlParams[14] = new SqlParameter("@Date2", SqlDbType.VarChar) { Value = model.Date2.ToStringOrNull() };
            sqlParams[15] = new SqlParameter("@Date3", SqlDbType.VarChar) { Value = model.Date3.ToStringOrNull() };
            sqlParams[16] = new SqlParameter("@Date4", SqlDbType.VarChar) { Value = model.Date4.ToStringOrNull() };
            sqlParams[17] = new SqlParameter("@Date5", SqlDbType.VarChar) { Value = model.Date5.ToStringOrNull() };
            sqlParams[18] = new SqlParameter("@Mode", SqlDbType.VarChar) { Value = model.Mode.ToStringOrNull() };
            sqlParams[19] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("HanyouMasterMaintenance_ModifyData", sqlParams);
            return dt;
        }
    }
}
