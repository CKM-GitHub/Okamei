using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class InputStepBL
    {
       
        public DataTable GetDisplayResult(InputStepModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[11];
            sqlParams[0] = new SqlParameter("@TantouSitenCD", SqlDbType.VarChar) { Value = model.TantouSitenCD.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@JuchuuOption", SqlDbType.TinyInt) { Value = model.JuchuuOption.ToInt16(0) };
            sqlParams[2] = new SqlParameter("@BukkenSiteiOption", SqlDbType.TinyInt) { Value = model.BukkenSiteiOption.ToInt16(0) };
            sqlParams[3] = new SqlParameter("@NoukiStart", SqlDbType.Date) { Value = model.NoukiStart.ToDateTime() };
            sqlParams[4] = new SqlParameter("@NoukiEnd", SqlDbType.Date) { Value = model.NoukiEnd.ToDateTime() };
            sqlParams[5] = new SqlParameter("@TantouEigyouCD", SqlDbType.VarChar) { Value = model.TantouEigyouCD.ToStringOrNull() };
            sqlParams[6] = new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = model.TantouCadCD.ToStringOrNull() };
            sqlParams[7] = new SqlParameter("@TantouPcCD", SqlDbType.VarChar) { Value = model.TantouPcCD.ToStringOrNull() };
            sqlParams[8] = new SqlParameter("@Dankai1", SqlDbType.VarChar) { Value = model.Dankai1.ToStringOrNull() };
            sqlParams[9] = new SqlParameter("@Dankai2", SqlDbType.VarChar) { Value = model.Dankai2.ToStringOrNull() };
            sqlParams[10] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputStep_SelectDisplayResult", sqlParams);
            return dt;
            
        }
    }
}
