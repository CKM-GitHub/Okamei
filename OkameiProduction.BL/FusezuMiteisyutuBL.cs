using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class FusezuMiteisyutuBL
    {
        public bool ExistsDisplayResult(FusezuMiteisyutuModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
           
            sqlParams[0] = new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = model.TantouCadCD.ToStringOrNull() };
           
          

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("FusezuMiteisyutu_SelectDisplayResult", sqlParams);
            return dt.Rows.Count > 0;
        }
        public DataTable GetDisplayResult(FusezuMiteisyutuModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = model.TantouCadCD.ToStringOrNull() };
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("FusezuMiteisyutu_SelectDisplayResult", sqlParams);
            return dt;
            
        }

    }
}
