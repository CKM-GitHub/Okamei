using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class KadouKeikakuBL
    {
        public bool ExistsDisplayResult(KadouKeikakuModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[16];
            sqlParams[0] = new SqlParameter("@TantouSitenCD", SqlDbType.VarChar) { Value = model.TantouSitenCD.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@BukkenSiteiOption", SqlDbType.TinyInt) { Value = model.BukkenSiteiOption.ToInt16(0) };
            sqlParams[2] = new SqlParameter("@NoukiStart", SqlDbType.Date) { Value = model.NoukiStart.ToDateTime() };
            sqlParams[3] = new SqlParameter("@NoukiEnd", SqlDbType.Date) { Value = model.NoukiEnd.ToDateTime() };
            sqlParams[4] = new SqlParameter("@TantouEigyouCD", SqlDbType.VarChar) { Value = model.TantouEigyouCD.ToStringOrNull() };
            sqlParams[5] = new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = model.TantouCadCD.ToStringOrNull() };
            sqlParams[6] = new SqlParameter("@OukazaiSumi", SqlDbType.VarChar) { Value = model.OukazaiSumi.ToInt16(0) };
            sqlParams[7] = new SqlParameter("@HasirazaiSumi", SqlDbType.VarChar) { Value = model.HasirazaiSumi.ToInt16(0) };
            sqlParams[8] = new SqlParameter("@HagarazaiSumi", SqlDbType.VarChar) { Value = model.HagarazaiSumi.ToInt16(0) };
            sqlParams[9] = new SqlParameter("@YukaSumi", SqlDbType.VarChar) { Value = model.YukaSumi.ToInt16(0) };
            sqlParams[10] = new SqlParameter("@NoziSumi", SqlDbType.VarChar) { Value = model.NoziSumi.ToInt16(0) };
            sqlParams[11] = new SqlParameter("@KabeSumi", SqlDbType.VarChar) { Value = model.KabeSumi.ToInt16(0) };
            sqlParams[12] = new SqlParameter("@HiuchiSumi", SqlDbType.VarChar) { Value = model.HiuchiSumi.ToInt16(0) };
            sqlParams[13] = new SqlParameter("@TekakouSumi", SqlDbType.VarChar) { Value = model.TekakouSumi.ToInt16(0) };
            sqlParams[14] = new SqlParameter("@HundeggerSumi", SqlDbType.VarChar) { Value = model.HundeggerSumi.ToInt16(0) };
            sqlParams[15] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("KadouKeikaku_ExistsDisplayResult", sqlParams);
            return dt.Rows.Count > 0;
        }
        public DataTable GetDisplayResult(KadouKeikakuModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[16];
            sqlParams[0] = new SqlParameter("@TantouSitenCD", SqlDbType.VarChar) { Value = model.TantouSitenCD.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@BukkenSiteiOption", SqlDbType.TinyInt) { Value = model.BukkenSiteiOption.ToInt16(0) };
            sqlParams[2] = new SqlParameter("@NoukiStart", SqlDbType.Date) { Value = model.NoukiStart.ToDateTime() };
            sqlParams[3] = new SqlParameter("@NoukiEnd", SqlDbType.Date) { Value = model.NoukiEnd.ToDateTime() };
            sqlParams[4] = new SqlParameter("@TantouEigyouCD", SqlDbType.VarChar) { Value = model.TantouEigyouCD.ToStringOrNull() };
            sqlParams[5] = new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = model.TantouCadCD.ToStringOrNull() };
            sqlParams[6] = new SqlParameter("@OukazaiSumi", SqlDbType.VarChar) { Value = model.OukazaiSumi.ToInt16(0) };
            sqlParams[7] = new SqlParameter("@HasirazaiSumi", SqlDbType.VarChar) { Value = model.HasirazaiSumi.ToInt16(0) };
            sqlParams[8] = new SqlParameter("@HagarazaiSumi", SqlDbType.VarChar) { Value = model.HagarazaiSumi.ToInt16(0) };
            sqlParams[9] = new SqlParameter("@YukaSumi", SqlDbType.VarChar) { Value = model.YukaSumi.ToInt16(0) };
            sqlParams[10] = new SqlParameter("@NoziSumi", SqlDbType.VarChar) { Value = model.NoziSumi.ToInt16(0) };
            sqlParams[11] = new SqlParameter("@KabeSumi", SqlDbType.VarChar) { Value = model.KabeSumi.ToInt16(0) };
            sqlParams[12] = new SqlParameter("@HiuchiSumi", SqlDbType.VarChar) { Value = model.HiuchiSumi.ToInt16(0) };
            sqlParams[13] = new SqlParameter("@TekakouSumi", SqlDbType.VarChar) { Value = model.TekakouSumi.ToInt16(0) };
            sqlParams[14] = new SqlParameter("@HundeggerSumi", SqlDbType.VarChar) { Value = model.HundeggerSumi.ToInt16(0) };
            sqlParams[15] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };
            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("KadouKeikaku_SelectDisplayResult", sqlParams);
            return dt;
            
        }
    }
}
