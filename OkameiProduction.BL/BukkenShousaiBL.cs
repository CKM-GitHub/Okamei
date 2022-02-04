using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class BukkenShousaiBL
    {
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
            sqlParams[12] = new SqlParameter("@TokuchuuzaiUmu", SqlDbType.Int) { Value = model.TokuchuuzaiUmu.ToInt32(0) };
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

        private List<SqlParameter> CreateSqlParams(BukkenShousaiModel model)
        {
            var sqlParams = new List<SqlParameter>()
            {
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() },
                new SqlParameter("@TantouSitenCD", SqlDbType.VarChar) { Value = model.SitenCD.ToStringOrNull() },
                new SqlParameter("@BukkenName", SqlDbType.VarChar) { Value = model.BukkenName.ToStringOrNull() },
                new SqlParameter("@Juusho", SqlDbType.VarChar) { Value = model.Juusho.ToStringOrNull() },
                new SqlParameter("@KoumutenName", SqlDbType.VarChar) { Value = model.KoumutenName.ToStringOrNull() },
                new SqlParameter("@KakoutuboSuu", SqlDbType.Decimal) { Value = model.KakouTubosuu.ToDecimal(0) },
                new SqlParameter("@NoukiMiteiKBN", SqlDbType.TinyInt) { Value = model.NoukiMiteiKBN ? 1 : 0 },
                new SqlParameter("@Nouki", SqlDbType.Date) { Value = model.Nouki.ToDateTime() },
                new SqlParameter("@UnsouKuraireDate", SqlDbType.Date) { Value = model.UnsouKuraireDate.ToDateTime() },
                new SqlParameter("@KubunCD", SqlDbType.VarChar) { Value = model.KubunCD.ToStringOrNull() },
                new SqlParameter("@TantouEigyouCD", SqlDbType.VarChar) { Value = model.TantouEigyouCD.ToStringOrNull() },
                new SqlParameter("@TantouPcCD", SqlDbType.VarChar) { Value = model.PCSupportCD.ToStringOrNull() },
                new SqlParameter("@TantouCadCD", SqlDbType.VarChar) { Value = model.TantouCadCD.ToStringOrNull() },
                new SqlParameter("@NyuuryokusakiCD", SqlDbType.VarChar) { Value = model.NyuuryokusakiCD.ToStringOrNull() },
                new SqlParameter("@TokuchuuzaiUmu", SqlDbType.TinyInt) { Value = model.TokuchuuzaiUmu.ToInt16(0) },
                new SqlParameter("@ZairyouNouki", SqlDbType.Date) { Value = model.ZairyouNouki.ToDateTime() },
                new SqlParameter("@TokuchuuzaiComment", SqlDbType.VarChar) { Value = model.TokuchuuzaiComment.ToStringOrNull() },
                new SqlParameter("@JuchuuDate", SqlDbType.Date) { Value = model.JuchuuDate.ToDateTime() },
                new SqlParameter("@FusezuTeishutuDate", SqlDbType.Date) { Value = model.FusezuTeishutuDate.ToDateTime() },
                new SqlParameter("@KakoutuShouninDate", SqlDbType.Date) { Value = model.KakouShouninDate.ToDateTime() },
                new SqlParameter("@KidasiDate", SqlDbType.Date) { Value = model.KidasiDate.ToDateTime() },
                new SqlParameter("@KakousijishoHakkouDate", SqlDbType.Date) { Value = model.KakousijishoHakkouDate.ToDateTime() },
                new SqlParameter("@KannouDate", SqlDbType.Date) { Value = model.KannouDate.ToDateTime() },
                new SqlParameter("@CancelDate", SqlDbType.Date) { Value = model.CancelDate.ToDateTime() },
                new SqlParameter("@KakouNissuu", SqlDbType.Int) { Value = model.KakouNissuu.ToInt32(0) },
                new SqlParameter("@KanamonoCD", SqlDbType.VarChar) { Value = model.KanamonoCD.ToStringOrNull() },
                new SqlParameter("@OukazaiKakou", SqlDbType.TinyInt) { Value = model.OukazaiKakou.ToInt16(0) },
                new SqlParameter("@OukazaiSumi", SqlDbType.TinyInt) { Value = model.OukazaiSumi ? 1 : 0 },
                new SqlParameter("@KabeKakou", SqlDbType.TinyInt) { Value = model.KabeKakou.ToInt16(0) },
                new SqlParameter("@KabeSumi", SqlDbType.TinyInt) { Value = model.KabeSumi ? 1 : 0 },
                new SqlParameter("@HasirazaiKakou", SqlDbType.TinyInt) { Value = model.HasirazaiKakou.ToInt16(0) },
                new SqlParameter("@HasirazaiSumi", SqlDbType.TinyInt) { Value = model.HasirazaiSumi ? 1 : 0 },
                new SqlParameter("@HiuchiKakou", SqlDbType.TinyInt) { Value = model.HiuchiKakou.ToInt16(0) },
                new SqlParameter("@HiuchiSumi", SqlDbType.TinyInt) { Value = model.HiuchiSumi ? 1 : 0 },
                new SqlParameter("@HagarazaiKakou", SqlDbType.TinyInt) { Value = model.HagarazaiKakou.ToInt16(0) },
                new SqlParameter("@HagarazaiSumi", SqlDbType.TinyInt) { Value = model.HagarazaiSumi ? 1 : 0 },
                new SqlParameter("@HagarazaiSuu", SqlDbType.Int) { Value = model.HagarazaiSuu.ToInt32(0) },
                new SqlParameter("@YukaKakou", SqlDbType.TinyInt) { Value = model.YukaKakou.ToInt16(0) },
                new SqlParameter("@YukaSumi", SqlDbType.TinyInt) { Value = model.YukaSumi ? 1 : 0 },
                new SqlParameter("@YukaGouhanShurui", SqlDbType.VarChar) { Value = model.YukaGouhanShurui.ToStringOrNull() },
                new SqlParameter("@YukaGouhanSuu", SqlDbType.Int) { Value = model.YukaGouhanSuu.ToInt32(0) },
                new SqlParameter("@NoziKakou", SqlDbType.TinyInt) { Value = model.NoziKakou.ToInt16(0) },
                new SqlParameter("@NoziSumi", SqlDbType.TinyInt) { Value = model.NoziSumi ? 1 : 0 },
                new SqlParameter("@NoziGouhanShurui", SqlDbType.VarChar) { Value = model.NoziGouhanShurui.ToStringOrNull() },
                new SqlParameter("@NoziGouhanSuu", SqlDbType.Int) { Value = model.NoziGouhanSuu.ToInt32(0) },
                new SqlParameter("@TekakouKakou", SqlDbType.TinyInt) { Value = model.TekakouKakou.ToInt16(0) },
                new SqlParameter("@TekakouSumi", SqlDbType.TinyInt) { Value = model.TekakouSumi ? 1 : 0 },
                new SqlParameter("@TekakouTime", SqlDbType.Decimal) { Value = model.TekakouTime.ToDecimal(0) },
                new SqlParameter("@HundeggerKakou", SqlDbType.TinyInt) { Value = model.HundeggerKakou.ToInt16(0) },
                new SqlParameter("@HundeggerSumi", SqlDbType.TinyInt) { Value = model.HundeggerSumi ? 1 : 0 },
                new SqlParameter("@HundeggerTime", SqlDbType.Decimal) { Value = model.HundeggerTime.ToDecimal(0) },
                new SqlParameter("@BukkenComment", SqlDbType.VarChar) { Value = model.BukkenComment.ToStringOrNull() },
                new SqlParameter("@Operator", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() },
                //new SqlParameter("@BukkenMoulderTBL", SqlDbType.Structured) { Value = dt },
            };

            if (model.Mode == EMode.Edit)
            {
                sqlParams.Add(new SqlParameter("@UpdateDatetime", SqlDbType.DateTime) { Value = model.UpdateDatetime.ToDateTime() });
            }

            return sqlParams;
        }

        public bool CreateBukkenAll(BukkenShousaiModel model, out string msgid)
        {
            msgid = "";
            try
            {
                DBAccess db = new DBAccess();
                return db.InsertUpdateDeleteData("InputBukkenShousai_Create", true, CreateSqlParams(model).ToArray());
            }
            catch (ExclusionException)
            {
                msgid = "S004"; //他の画面で処理中のデータです。しばらくしてから再度処理してください。
                return false;
            }
        }

        public bool UpdateBukkenAll(BukkenShousaiModel model, out string msgid)
        {
            msgid = "";
            return true;
        }

        public bool DeleteBukkenAll(BukkenShousaiModel model, out string msgid)
        {
            msgid = "";

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@Operator", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };
            sqlParams[2] = new SqlParameter("@UpdateDatetime", SqlDbType.DateTime) { Value = model.UpdateDatetime.ToDateTime() };

            return true;
        }

        public bool DeleteBukkenComment(BukkenShousaiModel model, out string msgid)
        {
            msgid = "";
            return true;
        }

        public bool CreateBukkenFile(BukkenShousaiModel model, out string msgid)
        {
            msgid = "";
            return true;
        }

        public bool DeleteBukkenFile(BukkenShousaiModel model, out string msgid)
        {
            msgid = "";
            return true;
        }
    }
}
