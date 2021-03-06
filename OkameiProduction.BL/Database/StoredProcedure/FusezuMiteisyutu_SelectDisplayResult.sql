IF EXISTS (select * from sys.objects where name = 'FusezuMiteisyutu_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [FusezuMiteisyutu_SelectDisplayResult]
END
GO

CREATE PROCEDURE [dbo].[FusezuMiteisyutu_SelectDisplayResult]
	
	@TantouCadCD as varchar(10)

AS
BEGIN
	
	SET NOCOUNT ON;
	select	 
		FORMAT(dbk.JuchuuDate, 'MM/dd')			AS JuchuuDate,
		CASE dbk.NoukiMiteiKBN WHEN 1 THEN '未定' 
           ELSE FORMAT(dbk.Nouki, 'MM/dd') END  AS Nouki,
		dbk.BukkenNO,
		CONVERT(varchar(16), dbk.BukkenName)	AS BukkenName,
		CONVERT(varchar(30), dbk.KoumutenName)  AS KoumutenName,
		FORMAT(dbk.KakoutuboSuu, '##0.00')		AS KakouTubosuu,
		CONVERT(varchar(6), mp1.Char2)			AS TantouSitenName,
		CONVERT(varchar(6), mp2.Char2)			AS TantouEigyouName,
		CONVERT(varchar(6), mp3.Char2)			AS TantouCadName,
		CONVERT(varchar(10), mp4.Char1)			AS KanamonName
		
	from D_Bukken as dbk
	left join M_MultiPorpose as mp1 on mp1.ID=003 and mp1.[Key] =dbk.TantouSitenCD
	left join M_MultiPorpose as mp2 on mp2.ID=004 and mp2.[Key] =dbk.TantouEigyouCD
	left join M_MultiPorpose as mp3 on mp3.ID=006 and mp3.[Key] =dbk.TantouCadCD
	left join M_MultiPorpose as mp4 on mp4.ID=008 and mp4.[Key] =dbk.KanamonoCD
  
	where dbk.FusezuTeishutuDate is Null
	and dbk.KakoutuShouninDate is Null
	and dbk.KidasiDate is Null
	and dbk.KakousijishoHakkouDate is Null
	and dbk.KannouDate is Null 
	and dbk.CancelDate is Null
	and (@TantouCadCD is NULL OR dbk.TantouCadCD =@TantouCadCD)
	order by  dbk.JuchuuDate asc,dbk.Nouki asc, dbk.BukkenNO asc
END
