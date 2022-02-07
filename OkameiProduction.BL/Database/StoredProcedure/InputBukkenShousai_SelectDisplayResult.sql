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
        ,FORMAT(Nouki, 'yyyy/MM/dd') AS Nouki
        ,FORMAT(UnsouKuraireDate, 'yyyy/MM/dd') AS UnsouKuraireDate
        ,KubunCD
        ,TantouEigyouCD
        ,TantouPcCD
        ,TantouCadCD
        ,NyuuryokusakiCD
        ,TokuchuuzaiUmu
        ,FORMAT(ZairyouNouki, 'yyyy/MM/dd') AS ZairyouNouki
        ,TokuchuuzaiComment
        ,FORMAT(JuchuuDate, 'yyyy/MM/dd') AS JuchuuDate
        ,FORMAT(FusezuTeishutuDate, 'yyyy/MM/dd') AS FusezuTeishutuDate
        ,FORMAT(KakoutuShouninDate, 'yyyy/MM/dd') AS KakouShouninDate
        ,FORMAT(KidasiDate, 'yyyy/MM/dd') AS KidasiDate
        ,FORMAT(KakousijishoHakkouDate, 'yyyy/MM/dd') AS KakousijishoHakkouDate
        ,FORMAT(KannouDate, 'yyyy/MM/dd') AS KannouDate
        ,FORMAT(CancelDate, 'yyyy/MM/dd') AS CancelDate
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
        ,FORMAT(UpdateDateTime, 'yyyy/MM/dd HH:mm:ss') AS UpdateDateTime
        ,FORMAT(UpdateDateTime, 'yyyy/MM/dd HH:mm:ss.fff') AS HiddenUpdateDatetime

	FROM D_Bukken
	WHERE BukkenNO = @BukkenNO

END
