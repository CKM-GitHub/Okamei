using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class InputBukkenShousaiMoulderBL
    {
        public DataTable GetBukkenMoulderData(InputBukkenShousaiMoulderModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() }
            };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputBukkenShousai_SelectBukkenMoulder", sqlParams);
            return dt;
        }

        public bool CreateBukkenMoulderData(InputBukkenShousaiMoulderModel model, out string msgid)
        {
            msgid = "";

            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() },
                new SqlParameter("@BukkenMoulderTable", SqlDbType.Structured) { TypeName = "dbo.T_BukkenMoulder", Value = model.Records.ToDataTable() },
                new SqlParameter("@Operator", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() },
                new SqlParameter("@UpdateDateTime", SqlDbType.VarChar) { Value = model.HiddenUpdateDateTime.ToStringOrNull() },
            };

            try
            {
                DBAccess db = new DBAccess();
                return db.InsertUpdateDeleteData("InputBukkenShousai_CreateBukkenMoulder", true, sqlParams);
            }
            catch (ExclusionException)
            {
                msgid = "S004"; //他端末エラー
                return false;
            }
        }
    }
}
