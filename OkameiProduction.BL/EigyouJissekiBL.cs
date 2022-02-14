using System;
using System.Data;
using System.Data.SqlClient;
using Models;
namespace OkameiProduction.BL
{
    public class EigyouJissekiBL
    {
        public DataTable GetDisplayResult(EigyouJissekiModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@DateMonth", SqlDbType.VarChar) { Value = model.KankeiMonth.Replace("/","-") + "-01" };
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("EigyouJisseki_SelectDisplayResult", sqlParams);
            return dt;
        }
    }
}
