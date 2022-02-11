using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class InputStepBL
    {
        public bool ExistsDisplayResult(InputStepModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
           
            sqlParams[0] = new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = model.TantouCadCD.ToStringOrNull() };
           
          

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputStep_SelectDisplayResult", sqlParams);
            return dt.Rows.Count > 0;
        }
        public DataTable GetDisplayResult(InputStepModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputStep_SelectDisplayResult", sqlParams);
            return dt;
            
        }

    }
}
