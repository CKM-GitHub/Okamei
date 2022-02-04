IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_SelectDisplayResult]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_SelectDisplayResult](
     @BukkenNO  varchar(8)
)AS
BEGIN
	SELECT BukkenNO
		,TantouSitenCD
        ,BukkenName
        ,Juusho
        ,KoumutenName
        ,KakoutuboSuu
        ,NoukiMiteiKBN
        ,Nouki
        ,UnsouKuraireDate
        ,KubunCD
        ,TantouEigyouCD
        ,TantouPcCD
        ,TantouCadCD
        ,NyuuryokusakiCD
        ,TokuchuuzaiUmu
        ,ZairyouNouki
        ,TokuchuuzaiComment
        ,JuchuuDate
        ,FusezuTeishutuDate
        ,KakoutuShouninDate
        ,KidasiDate
        ,KakousijishoHakkouDate
        ,KannouDate
        ,CancelDate
        ,KakouNissuu
        ,KanamonoCD
        ,OukazaiKakou
        ,OukazaiSumi
        ,KabeKakou
        ,KabeSumi
        ,HasirazaiKakou
        ,HasirazaiSumi
        ,HiuchiKakou
        ,HiuchiSumi
        ,HagarazaiKakou
        ,HagarazaiSumi
        ,HagarazaiSuu
        ,YukaKakou
        ,YukaSumi
        ,YukaGouhanShurui
        ,YukaGouhanSuu
        ,NoziKakou
        ,NoziSumi
        ,NoziGouhanShurui
        ,NoziGouhanSuu
        ,TekakouKakou
        ,TekakouSumi
        ,TekakouTime
        ,HundeggerKakou
        ,HundeggerSumi
        ,HundeggerTime
        ,UpdateDateTime

	FROM D_Bukken
	WHERE BukkenNO = @BukkenNO

END
