using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class InputBukkenShousaiBL
    {
        public string GetNewBukkenNO(string sitenCD)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@TantouSitenCD", SqlDbType.VarChar) { Value = sitenCD };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("M_MultiPorpose_GetNewBukkenNO", sqlParams);

            if (dt.Rows.Count == 0)
            {
                return "";
            }

            var dr = dt.Rows[0];
            var prefix = dr["Prefix"].ToStringOrEmpty();
            var number = dr["Number"].ToStringOrEmpty();
            var newBukkenNO = prefix + number.PadLeft(8 - prefix.Length, '0');

            return newBukkenNO;
        }

        public DataTable GetDisplayResult(InputBukkenShousaiModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() }
            };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputBukkenShousai_SelectDisplayResult", sqlParams);
            return dt;
        }

        public DataTable GetBukkenFile(InputBukkenShousaiBukkenFileModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() },
                new SqlParameter("@BukkenFileShurui", SqlDbType.TinyInt) { Value = model.BukkenFileShurui.ToByte(8) }
            };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputBukkenShousai_SelectBukkenFile", sqlParams);
            return dt;
        }

        public DataTable GetBukkenFileName(string bukkenNO, string bukkenFileRowsCsv)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = bukkenNO.ToStringOrNull() },
                new SqlParameter("@BukkenFileRowsCsv", SqlDbType.VarChar) { Value = bukkenFileRowsCsv.ToStringOrNull() }
            };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputBukkenShousai_SelectBukkenFileName", sqlParams);
            return dt;
        }

        public DataTable GetBukkenComment(InputBukkenShousaiBukkenCommentModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() }
            };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputBukkenShousai_SelectBukkenComment", sqlParams);
            return dt;
        }





        private List<SqlParameter> CreateSqlParams(InputBukkenShousaiModel model)
        {
            var sqlParams = new List<SqlParameter>()
            {
                new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() },
                new SqlParameter("@TantouSitenCD", SqlDbType.VarChar) { Value = model.TantouSitenCD.ToStringOrNull() },
                new SqlParameter("@BukkenName", SqlDbType.VarChar) { Value = model.BukkenName.ToStringOrNull() },
                new SqlParameter("@Juusho", SqlDbType.VarChar) { Value = model.Juusho.ToStringOrNull() },
                new SqlParameter("@KoumutenName", SqlDbType.VarChar) { Value = model.KoumutenName.ToStringOrNull() },
                new SqlParameter("@KakoutuboSuu", SqlDbType.Decimal) { Value = model.KakouTubosuu.ToDecimal(0) },
                new SqlParameter("@NoukiMiteiKBN", SqlDbType.TinyInt) { Value = model.NoukiMiteiKBN.ToByte(0) },
                new SqlParameter("@Nouki", SqlDbType.Date) { Value = model.Nouki.ToDateTime(new DateTime(2099,12,31)) },
                new SqlParameter("@UnsouKuraireDate", SqlDbType.Date) { Value = model.UnsouKuraireDate.ToDateTime() },
                new SqlParameter("@KubunCD", SqlDbType.VarChar) { Value = model.KubunCD.ToStringOrNull() },
                new SqlParameter("@TantouEigyouCD", SqlDbType.VarChar) { Value = model.TantouEigyouCD.ToStringOrNull() },
                new SqlParameter("@TantouPcCD", SqlDbType.VarChar) { Value = model.TantouPcCD.ToStringOrNull() },
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
                new SqlParameter("@OukazaiSumi", SqlDbType.TinyInt) { Value = model.OukazaiSumi.ToByte(0) },
                new SqlParameter("@KabeKakou", SqlDbType.TinyInt) { Value = model.KabeKakou.ToInt16(0) },
                new SqlParameter("@KabeSumi", SqlDbType.TinyInt) { Value = model.KabeSumi.ToByte(0) },
                new SqlParameter("@HasirazaiKakou", SqlDbType.TinyInt) { Value = model.HasirazaiKakou.ToInt16(0) },
                new SqlParameter("@HasirazaiSumi", SqlDbType.TinyInt) { Value = model.HasirazaiSumi.ToByte(0) },
                new SqlParameter("@HiuchiKakou", SqlDbType.TinyInt) { Value = model.HiuchiKakou.ToInt16(0) },
                new SqlParameter("@HiuchiSumi", SqlDbType.TinyInt) { Value = model.HiuchiSumi.ToByte(0) },
                new SqlParameter("@HagarazaiKakou", SqlDbType.TinyInt) { Value = model.HagarazaiKakou.ToInt16(0) },
                new SqlParameter("@HagarazaiSumi", SqlDbType.TinyInt) { Value = model.HagarazaiSumi.ToByte(0) },
                new SqlParameter("@HagarazaiSuu", SqlDbType.Int) { Value = model.HagarazaiSuu.ToInt32(0) },
                new SqlParameter("@YukaKakou", SqlDbType.TinyInt) { Value = model.YukaKakou.ToInt16(0) },
                new SqlParameter("@YukaSumi", SqlDbType.TinyInt) { Value = model.YukaSumi.ToByte(0) },
                new SqlParameter("@YukaGouhanShurui", SqlDbType.VarChar) { Value = model.YukaGouhanShurui.ToStringOrNull() },
                new SqlParameter("@YukaGouhanSuu", SqlDbType.Int) { Value = model.YukaGouhanSuu.ToInt32(0) },
                new SqlParameter("@NoziKakou", SqlDbType.TinyInt) { Value = model.NoziKakou.ToInt16(0) },
                new SqlParameter("@NoziSumi", SqlDbType.TinyInt) { Value = model.NoziSumi.ToByte(0) },
                new SqlParameter("@NoziGouhanShurui", SqlDbType.VarChar) { Value = model.NoziGouhanShurui.ToStringOrNull() },
                new SqlParameter("@NoziGouhanSuu", SqlDbType.Int) { Value = model.NoziGouhanSuu.ToInt32(0) },
                new SqlParameter("@TekakouKakou", SqlDbType.TinyInt) { Value = model.TekakouKakou.ToInt16(0) },
                new SqlParameter("@TekakouSumi", SqlDbType.TinyInt) { Value = model.TekakouSumi.ToByte(0) },
                new SqlParameter("@TekakouTime", SqlDbType.Decimal) { Value = model.TekakouTime.ToDecimal(0) },
                new SqlParameter("@HundeggerKakou", SqlDbType.TinyInt) { Value = model.HundeggerKakou.ToInt16(0) },
                new SqlParameter("@HundeggerSumi", SqlDbType.TinyInt) { Value = model.HundeggerSumi.ToByte(0) },
                new SqlParameter("@HundeggerTime", SqlDbType.Decimal) { Value = model.HundeggerTime.ToDecimal(0) },
                new SqlParameter("@BukkenComment", SqlDbType.VarChar) { Value = model.BukkenComment.ToStringOrNull() },
                new SqlParameter("@Operator", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() },
                //new SqlParameter("@BukkenMoulderTBL", SqlDbType.Structured) { Value = dt },
            };

            if (model.Mode == EMode.Edit)
            {
                sqlParams.Add(new SqlParameter("@UpdateDatetime", SqlDbType.VarChar) { Value = model.HiddenUpdateDateTime.ToStringOrNull() });
            }
            return sqlParams;
        }

        public bool CreateBukkenAll(InputBukkenShousaiModel model, out string msgid)
        {
            msgid = "";
            SqlParameter[] sqlParams = CreateSqlParams(model).ToArray();
            try
            {
                DBAccess db = new DBAccess();
                return db.InsertUpdateDeleteData("InputBukkenShousai_Create", true, sqlParams);
            }
            catch (ExclusionException)
            {
                msgid = "S004"; //他端末エラー
                return false;
            }
        }

        public bool UpdateBukkenAll(InputBukkenShousaiModel model, out string msgid)
        {
            msgid = "";
            SqlParameter[] sqlParams = CreateSqlParams(model).ToArray();
            try
            {
                DBAccess db = new DBAccess();
                return db.InsertUpdateDeleteData("InputBukkenShousai_Update", true, sqlParams);
            }
            catch (ExclusionException)
            {
                msgid = "S004"; //他端末エラー
                return false;
            }
        }

        public bool DeleteBukkenAll(InputBukkenShousaiModel model, out string msgid)
        {
            msgid = "";

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@UpdateDatetime", SqlDbType.VarChar) { Value = model.HiddenUpdateDateTime.ToStringOrNull() };

            try
            {
                DBAccess db = new DBAccess();
                return db.InsertUpdateDeleteData("InputBukkenShousai_Delete", true, sqlParams);
            }
            catch (ExclusionException)
            {
                msgid = "S004"; //他端末エラー
                return false;
            }
        }

        public bool CreateBukkenFile(InputBukkenShousaiBukkenFileModel model, out string msgid)
        {
            msgid = "";

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@BukkenFileShurui", SqlDbType.VarChar) { Value = model.BukkenFileShurui.ToByte(0) };
            sqlParams[2] = new SqlParameter("@BukkenFileName", SqlDbType.VarChar) { Value = model.BukkenFileName.ToStringOrNull() };
            sqlParams[3] = new SqlParameter("@Operator", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };
            sqlParams[4] = new SqlParameter("@UpdateDatetime", SqlDbType.VarChar) { Value = model.HiddenUpdateDatetime.ToStringOrNull() };
            try
            {
                DBAccess db = new DBAccess();
                return db.InsertUpdateDeleteData("InputBukkenShousai_CreateBukkenFile", true, sqlParams);
            }
            catch (ExclusionException)
            {
                msgid = "S004"; //他端末エラー
                return false;
            }
        }

        public bool DeleteBukkenFile(InputBukkenShousaiBukkenFileModel model, out string msgid)
        {
            msgid = "";

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@BukkenFileRows", SqlDbType.Int) { Value = model.BukkenFileRows.ToInt32(0) };
            sqlParams[2] = new SqlParameter("@UpdateDatetime", SqlDbType.VarChar) { Value = model.HiddenUpdateDatetime.ToStringOrNull() };
            try
            {
                DBAccess db = new DBAccess();
                return db.InsertUpdateDeleteData("InputBukkenShousai_DeleteBukkenFile", true, sqlParams);
            }
            catch (ExclusionException)
            {
                msgid = "S004"; //他端末エラー
                return false;
            }
        }

        public bool CreateBukkenComment(InputBukkenShousaiBukkenCommentModel model, out string msgid)
        {
            msgid = "";

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@BukkenComment", SqlDbType.VarChar) { Value = model.BukkenComment.ToStringOrNull() };
            sqlParams[2] = new SqlParameter("@Operator", SqlDbType.VarChar) { Value = model.UserID.ToStringOrNull() };
            sqlParams[3] = new SqlParameter("@UpdateDatetime", SqlDbType.VarChar) { Value = model.HiddenUpdateDatetime.ToStringOrNull() };
            try
            {
                DBAccess db = new DBAccess();
                return db.InsertUpdateDeleteData("InputBukkenShousai_CreateBukkenComment", true, sqlParams);
            }
            catch (ExclusionException)
            {
                msgid = "S004"; //他端末エラー
                return false;
            }
        }
        public bool DeleteBukkenComment(InputBukkenShousaiBukkenCommentModel model, out string msgid)
        {
            msgid = "";

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@BukkenCommentRows", SqlDbType.Int) { Value = model.BukkenCommentRows.ToInt32(0) };
            sqlParams[2] = new SqlParameter("@UpdateDatetime", SqlDbType.VarChar) { Value = model.HiddenUpdateDatetime.ToStringOrNull() };
            try
            {
                DBAccess db = new DBAccess();
                return db.InsertUpdateDeleteData("InputBukkenShousai_DeleteBukkenComment", true, sqlParams);
            }
            catch (ExclusionException)
            {
                msgid = "S004"; //他端末エラー
                return false;
            }
        }

        public bool SendMail(InputBukkenShousaiModel model)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BukkenNO", SqlDbType.VarChar) { Value = model.BukkenNO.ToStringOrNull() };
            sqlParams[1] = new SqlParameter("@BukkenName", SqlDbType.VarChar) { Value = model.BukkenName.ToStringOrNull() };
            sqlParams[2] = new SqlParameter("@BukkenFileShurui", SqlDbType.TinyInt) { Value = model.BukkenFileShurui.ToByte(8) };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("InputBukkenShousai_SendMail", sqlParams);

            return true;
        }
    }
}
