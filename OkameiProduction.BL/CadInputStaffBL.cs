using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
   public class CadInputStaffBL
    {
        public DataTable GetDisplayResult(CadInputStaffModel model)
        {
            var value =
               ( model.TantouCadCD1 ?? "") + "," +
               ( model.TantouCadCD2 ?? "" )+ "," +
               ( model.TantouCadCD3 ?? "" )+ "," +
               ( model.TantouCadCD4 ?? "" )+ "," +
               ( model.TantouCadCD5 ?? "" )+ "," +
               ( model.TantouCadCD6 ?? "" )+ "," +
               ( model.TantouCadCD7 ?? "" )+ "," +
               ( model.TantouCadCD8 ?? "" )+ "," +
               ( model.TantouCadCD9 ?? "" )+ "," +
               ( model.TantouCadCD10 ?? "") + ",";
            if (value.Length == 10)
                value = "";
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = value.ToStringOrNull() }; 
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("CadInputStaff_SelectDisplayResult", sqlParams);
            return dt;
        }
        public DataTable ExistDisplayResult(CadInputStaffModel model)
        {
            var value =
               (model.TantouCadCD1 ?? "") + "," +
               (model.TantouCadCD2 ?? "") + "," +
               (model.TantouCadCD3 ?? "") + "," +
               (model.TantouCadCD4 ?? "") + "," +
               (model.TantouCadCD5 ?? "") + "," +
               (model.TantouCadCD6 ?? "") + "," +
               (model.TantouCadCD7 ?? "") + "," +
               (model.TantouCadCD8 ?? "") + "," +
               (model.TantouCadCD9 ?? "") + "," +
               (model.TantouCadCD10 ?? "") + ",";
            if (value.Length == 10)
                value = "";
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = value.ToStringOrNull() };
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("CadInputStaff_ExistDisplayResult", sqlParams);
            return dt;
        }
    }
}
