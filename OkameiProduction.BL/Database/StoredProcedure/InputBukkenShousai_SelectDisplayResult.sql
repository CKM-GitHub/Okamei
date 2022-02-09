IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_SelectDisplayResult]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_SelectDisplayResult](
     @BukkenNO  varchar(8)
)AS
BEGIN
    DECLARE @MaxDate date = '2099/12/31'

	SELECT BukkenNO
		,TantouSitenCD
        ,BukkenName
        ,Juusho
        ,KoumutenName
        ,FORMAT(KakouTubosuu, '##0.00') AS KakouTubosuu
        ,NoukiMiteiKBN
        ,FORMAT(CASE Nouki WHEN @MaxDate THEN NULL ELSE Nouki END, 'yyyy/MM/dd') AS Nouki
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
        ,FORMAT(KakouNissuu, '##0') AS KakouNissuu
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
        ,FORMAT(HagarazaiSuu, '##0') AS HagarazaiSuu
        ,YukaKakou
        ,YukaSumi
        ,YukaGouhanShurui
        ,FORMAT(YukaGouhanSuu, '##0') AS YukaGouhanSuu
        ,NoziKakou
        ,NoziSumi
        ,NoziGouhanShurui
        ,FORMAT(NoziGouhanSuu, '##0') AS NoziGouhanSuu
        ,TekakouKakou
        ,TekakouSumi
        ,FORMAT(TekakouTime, '##0.0') AS TekakouTime
        ,HundeggerKakou
        ,HundeggerSumi
        ,FORMAT(HundeggerTime, '##0.0') AS HundeggerTime
        ,FORMAT(UpdateDateTime, 'yyyy/MM/dd HH:mm:ss') AS UpdateDateTime
        ,FORMAT(UpdateDateTime, 'yyyy/MM/dd HH:mm:ss.fff') AS HiddenUpdateDatetime

	FROM D_Bukken
	WHERE BukkenNO = @BukkenNO

END
