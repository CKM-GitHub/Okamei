using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class InputBukkenShousaiHiuchiBL
    {
        public DataTable GetBukkenHiuchiData(InputBukkenShousaiHiuchiModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() }
            };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputBukkenShousai_SelectBukkenHiuchi", sqlParams);
            return dt;
        }

        public bool CreateBukkenHiuchiData(InputBukkenShousaiHiuchiModel model, out string msgid)
        {
            msgid = "";

            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SouCount", SqlDbType.Int) { Value = model.SouCount.ToInt32(0) },
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() },
                new SqlParameter("@Sou1", SqlDbType.VarChar) { Value = model.Sou1.ToStringOrNull() },
                new SqlParameter("@Zairyou11", SqlDbType.VarChar) { Value = model.Zairyou11.ToStringOrNull() },
                new SqlParameter("@Toukyuu11", SqlDbType.VarChar) { Value = model.Toukyuu11.ToStringOrNull() },
                new SqlParameter("@Honsuu11", SqlDbType.Int) { Value = model.Honsuu11.ToInt32(0) },
                new SqlParameter("@Zairyou12", SqlDbType.VarChar) { Value = model.Zairyou12.ToStringOrNull() },
                new SqlParameter("@Toukyuu12", SqlDbType.VarChar) { Value = model.Toukyuu12.ToStringOrNull() },
                new SqlParameter("@Honsuu12", SqlDbType.Int) { Value = model.Honsuu12.ToInt32(0) },
                new SqlParameter("@Zairyou13", SqlDbType.VarChar) { Value = model.Zairyou13.ToStringOrNull() },
                new SqlParameter("@Toukyuu13", SqlDbType.VarChar) { Value = model.Toukyuu13.ToStringOrNull() },
                new SqlParameter("@Honsuu13", SqlDbType.Int) { Value = model.Honsuu13.ToInt32(0) },
                new SqlParameter("@Sou1Sumi", SqlDbType.TinyInt) { Value = model.Sou1Sumi.ToInt16(0) },
                new SqlParameter("@Sou2", SqlDbType.VarChar) { Value = model.Sou2.ToStringOrNull() },
                new SqlParameter("@Zairyou21", SqlDbType.VarChar) { Value = model.Zairyou21.ToStringOrNull() },
                new SqlParameter("@Toukyuu21", SqlDbType.VarChar) { Value = model.Toukyuu21.ToStringOrNull() },
                new SqlParameter("@Honsuu21", SqlDbType.Int) { Value = model.Honsuu21.ToInt32(0) },
                new SqlParameter("@Zairyou22", SqlDbType.VarChar) { Value = model.Zairyou22.ToStringOrNull() },
                new SqlParameter("@Toukyuu22", SqlDbType.VarChar) { Value = model.Toukyuu22.ToStringOrNull() },
                new SqlParameter("@Honsuu22", SqlDbType.Int) { Value = model.Honsuu22.ToInt32(0) },
                new SqlParameter("@Zairyou23", SqlDbType.VarChar) { Value = model.Zairyou23.ToStringOrNull() },
                new SqlParameter("@Toukyuu23", SqlDbType.VarChar) { Value = model.Toukyuu23.ToStringOrNull() },
                new SqlParameter("@Honsuu23", SqlDbType.Int) { Value = model.Honsuu23.ToInt32(0) },
                new SqlParameter("@Sou2Sumi", SqlDbType.TinyInt) { Value = model.Sou2Sumi.ToInt16(0) },
                new SqlParameter("@Sou3", SqlDbType.VarChar) { Value = model.Sou3.ToStringOrNull() },
                new SqlParameter("@Zairyou31", SqlDbType.VarChar) { Value = model.Zairyou31.ToStringOrNull() },
                new SqlParameter("@Toukyuu31", SqlDbType.VarChar) { Value = model.Toukyuu31.ToStringOrNull() },
                new SqlParameter("@Honsuu31", SqlDbType.Int) { Value = model.Honsuu31.ToInt32(0) },
                new SqlParameter("@Zairyou32", SqlDbType.VarChar) { Value = model.Zairyou32.ToStringOrNull() },
                new SqlParameter("@Toukyuu32", SqlDbType.VarChar) { Value = model.Toukyuu32.ToStringOrNull() },
                new SqlParameter("@Honsuu32", SqlDbType.Int) { Value = model.Honsuu32.ToInt32(0) },
                new SqlParameter("@Zairyou33", SqlDbType.VarChar) { Value = model.Zairyou33.ToStringOrNull() },
                new SqlParameter("@Toukyuu33", SqlDbType.VarChar) { Value = model.Toukyuu33.ToStringOrNull() },
                new SqlParameter("@Honsuu33", SqlDbType.Int) { Value = model.Honsuu33.ToInt32(0) },
                new SqlParameter("@Sou3Sumi", SqlDbType.TinyInt) { Value = model.Sou3Sumi.ToInt16(0) },
                new SqlParameter("@Sou4", SqlDbType.VarChar) { Value = model.Sou4.ToStringOrNull() },
                new SqlParameter("@Zairyou41", SqlDbType.VarChar) { Value = model.Zairyou41.ToStringOrNull() },
                new SqlParameter("@Toukyuu41", SqlDbType.VarChar) { Value = model.Toukyuu41.ToStringOrNull() },
                new SqlParameter("@Honsuu41", SqlDbType.Int) { Value = model.Honsuu41.ToInt32(0) },
                new SqlParameter("@Zairyou42", SqlDbType.VarChar) { Value = model.Zairyou42.ToStringOrNull() },
                new SqlParameter("@Toukyuu42", SqlDbType.VarChar) { Value = model.Toukyuu42.ToStringOrNull() },
                new SqlParameter("@Honsuu42", SqlDbType.Int) { Value = model.Honsuu42.ToInt32(0) },
                new SqlParameter("@Zairyou43", SqlDbType.VarChar) { Value = model.Zairyou43.ToStringOrNull() },
                new SqlParameter("@Toukyuu43", SqlDbType.VarChar) { Value = model.Toukyuu43.ToStringOrNull() },
                new SqlParameter("@Honsuu43", SqlDbType.Int) { Value = model.Honsuu43.ToInt32(0) },
                new SqlParameter("@Sou4Sumi", SqlDbType.TinyInt) { Value = model.Sou4Sumi.ToInt16(0) },
                new SqlParameter("@Operator", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() },
                new SqlParameter("@UpdateDateTime", SqlDbType.VarChar) { Value = model.HiddenUpdateDateTime.ToStringOrNull() },
            };

            try
            {
                DBAccess db = new DBAccess();
                return db.InsertUpdateDeleteData("InputBukkenShousai_CreateBukkenHiuchi", true, sqlParams);
            }
            catch (ExclusionException)
            {
                msgid = "S004"; //他端末エラー
                return false;
            }

        }
    }
}
