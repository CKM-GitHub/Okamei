using System;
using System.Data;
using System.Data.SqlClient;
using Models;


namespace OkameiProduction.BL
{
   public class HiuchiItiranBL
    {

        public DataTable GetDisplayResult(HiuchiItiranModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@TantouSitenCD", SqlDbType.VarChar) { Value = model.TantouSitenCD.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@NoukiStart", SqlDbType.Date) { Value = model.NoukiStart.ToDateTime() };
            sqlParams[2] = new SqlParameter("@NoukiEnd", SqlDbType.Date) { Value = model.NoukiEnd.ToDateTime() }; 
            sqlParams[3] = new SqlParameter("@TantouEigyouCD", SqlDbType.VarChar) { Value = model.TantouEigyouCD.ToStringOrNull() }; 
            sqlParams[4] = new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = model.TantouCadCD.ToStringOrNull() };
            //sqlParams[5] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };


            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("HiuchiItiran_SelectDisplayResult", sqlParams);
            dt.Columns.Remove("Flg");
            dt.Columns.Add("Export");
            dt.AcceptChanges();
            return dt; 
        }
    }
}
