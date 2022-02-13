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
            SqlParameter[] sqlParams = new SqlParameter[0];
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputStep_SelectDisplayResult", sqlParams);
            return dt;
            
        }

    }
}
