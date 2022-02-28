using System;
using System.Collections.Generic;
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
                //new SqlParameter("@Moulder1Honsuu", SqlDbType.Int) { Value = model.Moulder1Honsuu.ToInt32(0) },
                //new SqlParameter("@Moulder2Honsuu", SqlDbType.Int) { Value = model.Moulder2Honsuu.ToInt32(0) },
                //new SqlParameter("@Moulder3Honsuu", SqlDbType.Int) { Value = model.Moulder3Honsuu.ToInt32(0) },
                //new SqlParameter("@Moulder4Honsuu", SqlDbType.Int) { Value = model.Moulder4Honsuu.ToInt32(0) },
                //new SqlParameter("@Moulder5Honsuu", SqlDbType.Int) { Value = model.Moulder5Honsuu.ToInt32(0) },
                //new SqlParameter("@Moulder6Honsuu", SqlDbType.Int) { Value = model.Moulder6Honsuu.ToInt32(0) },
                //new SqlParameter("@Moulder7Honsuu", SqlDbType.Int) { Value = model.Moulder7Honsuu.ToInt32(0) },
                //new SqlParameter("@Moulder8Honsuu", SqlDbType.Int) { Value = model.Moulder8Honsuu.ToInt32(0) },
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
