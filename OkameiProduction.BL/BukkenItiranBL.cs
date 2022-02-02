using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class BukkenItiranBL
    {
        public bool ExistsDisplayResult(BukkenItiranModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[14];
            sqlParams[0] = new SqlParameter("@TantouSitenCD", SqlDbType.VarChar) { Value = model.SitenCD.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@JuchuuOption", SqlDbType.TinyInt) { Value = model.JuchuuOption.ToInt16(0) };
            sqlParams[2] = new SqlParameter("@BukkenSiteiOption", SqlDbType.TinyInt) { Value = model.BukkenSiteiOption.ToInt16(0) };
            sqlParams[3] = new SqlParameter("@NoukiStart", SqlDbType.Date) { Value = model.NoukiStart.ToDateTime() };
            sqlParams[4] = new SqlParameter("@NoukiEnd", SqlDbType.Date) { Value = model.NoukiEnd.ToDateTime() };
            sqlParams[5] = new SqlParameter("@TantouEigyouCD", SqlDbType.VarChar) { Value = model.TantouEigyouCD.ToStringOrNull() };
            sqlParams[6] = new SqlParameter("@TantouPcCD", SqlDbType.VarChar) { Value = model.PCSupportCD.ToStringOrNull() };
            sqlParams[7] = new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = model.TantouCadCD.ToStringOrNull() };
            sqlParams[8] = new SqlParameter("@KubunCD", SqlDbType.VarChar) { Value = model.KubunCD.ToStringOrNull() };
            sqlParams[9] = new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() };
            sqlParams[10] = new SqlParameter("@BukkenName", SqlDbType.VarChar) { Value = model.BukkenName.ToStringOrNull() };
            sqlParams[11] = new SqlParameter("@KoumutenName", SqlDbType.VarChar) { Value = model.KoumutenName.ToStringOrNull() };
            sqlParams[12] = new SqlParameter("@TokuchuuzaiUmu", SqlDbType.TinyInt) { Value = model.TokuchuuzaiUmu.ToInt16(0) };
            sqlParams[13] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("BukkenItiran_ExistsDisplayResult", sqlParams);
            return dt.Rows.Count > 0;
        }

        public DataTable GetDisplayResult(BukkenItiranModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[14];
            sqlParams[0] = new SqlParameter("@TantouSitenCD", SqlDbType.VarChar) { Value = model.SitenCD.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@JuchuuOption", SqlDbType.TinyInt) { Value = model.JuchuuOption.ToInt16(0) };
            sqlParams[2] = new SqlParameter("@BukkenSiteiOption", SqlDbType.TinyInt) { Value = model.BukkenSiteiOption.ToInt16(0) };
            sqlParams[3] = new SqlParameter("@NoukiStart", SqlDbType.Date) { Value = model.NoukiStart.ToDateTime() };
            sqlParams[4] = new SqlParameter("@NoukiEnd", SqlDbType.Date) { Value = model.NoukiEnd.ToDateTime() };
            sqlParams[5] = new SqlParameter("@TantouEigyouCD", SqlDbType.VarChar) { Value = model.TantouEigyouCD.ToStringOrNull() };
            sqlParams[6] = new SqlParameter("@TantouPcCD", SqlDbType.VarChar) { Value = model.PCSupportCD.ToStringOrNull() };
            sqlParams[7] = new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = model.TantouCadCD.ToStringOrNull() };
            sqlParams[8] = new SqlParameter("@KubunCD", SqlDbType.VarChar) { Value = model.KubunCD.ToStringOrNull() };
            sqlParams[9] = new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() };
            sqlParams[10] = new SqlParameter("@BukkenName", SqlDbType.VarChar) { Value = model.BukkenName.ToStringOrNull() };
            sqlParams[11] = new SqlParameter("@KoumutenName", SqlDbType.VarChar) { Value = model.KoumutenName.ToStringOrNull() };
            sqlParams[12] = new SqlParameter("@TokuchuuzaiUmu", SqlDbType.TinyInt) { Value = model.TokuchuuzaiUmu.ToInt16(0) };
            sqlParams[13] = new SqlParameter("@UserID", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("BukkenItiran_SelectDisplayResult", sqlParams);

            if (model.SortOption == 2)
            {
                var query = from dr in dt.AsEnumerable()
                            orderby dr.Field<string>("SortKoumutenName"), dr.Field<DateTime?>("SortNouki")
                            select dr;
                dt = query.CopyToDataTable();
            }

            dt.Columns.Remove("SortKoumutenName");
            dt.Columns.Remove("SortNouki");
            return dt;
        }
    }
}
