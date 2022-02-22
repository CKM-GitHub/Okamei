using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class InputBukkenShousaiTekakouBL
    {
        public DataTable GetBukkenTekakouData(InputBukkenShousaiTekakouModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() }
            };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputBukkenShousai_SelectBukkenTeKakou", sqlParams);
            return dt;
        }

        public bool CreateBukkenTekakouData(InputBukkenShousaiTekakouModel model, out string msgid)
        {
            msgid = "";

            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() },
                new SqlParameter("@TeKakou1Honsuu", SqlDbType.Int) { Value = model.TeKakou1Honsuu.ToInt32(0) },
                new SqlParameter("@TeKakou2Honsuu", SqlDbType.Int) { Value = model.TeKakou2Honsuu.ToInt32(0) },
                new SqlParameter("@TeKakou3Honsuu", SqlDbType.Int) { Value = model.TeKakou3Honsuu.ToInt32(0) },
                new SqlParameter("@TeKakou4Honsuu", SqlDbType.Int) { Value = model.TeKakou4Honsuu.ToInt32(0) },
                new SqlParameter("@TeKakou5Honsuu", SqlDbType.Int) { Value = model.TeKakou5Honsuu.ToInt32(0) },
                new SqlParameter("@TeKakou6Honsuu", SqlDbType.Int) { Value = model.TeKakou6Honsuu.ToInt32(0) },
                new SqlParameter("@TeKakou7Honsuu", SqlDbType.Int) { Value = model.TeKakou7Honsuu.ToInt32(0) },
                new SqlParameter("@TeKakou8Honsuu", SqlDbType.Int) { Value = model.TeKakou8Honsuu.ToInt32(0) },
                new SqlParameter("@Operator", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() },
                new SqlParameter("@UpdateDateTime", SqlDbType.VarChar) { Value = model.HiddenUpdateDateTime.ToStringOrNull() },
            };

            try
            {
                DBAccess db = new DBAccess();
                return db.InsertUpdateDeleteData("InputBukkenShousai_CreateBukkenTeKakou", true, sqlParams);
            }
            catch (ExclusionException)
            {
                msgid = "S004"; //他端末エラー
                return false;
            }

        }
    }
}
