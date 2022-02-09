USE [Okamei]
GO
IF EXISTS (select * from sys.objects where name = 'CadStaffMitei_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [CadStaffMitei_SelectDisplayResult]
END
GO
CREATE PROCEDURE [dbo].[CadStaffMitei_SelectDisplayResult]
	
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT	 
		FORMAT(dbk.JuchuuDate, 'MM/dd')			AS JuchuuDate,
		CASE dbk.NoukiMiteiKBN WHEN 1 THEN '–¢’è' 
           ELSE FORMAT(dbk.Nouki, 'MM/dd') END  AS Nouki,
		dbk.BukkenNO,
		CONVERT(varchar(16), dbk.BukkenName)	AS BukkenName,
		CONVERT(varchar(30), dbk.KoumutenName)  AS KoumutenName,
		FORMAT(dbk.KakoutuboSuu, '##0.00')		AS KakouTubosuu,
		CONVERT(varchar(6), mp1.Char2)			AS TantouSitenName,
		CONVERT(varchar(6), mp2.Char2)			AS TantouEigyouName,
		CONVERT(varchar(10), mp4.Char1)			AS KanamonName
		
	FROM D_Bukken AS dbk
	left join M_MultiPorpose as mp1 on mp1.ID=003 and mp1.[Key] =dbk.TantouSitenCD
	left join M_MultiPorpose as mp2 on mp2.ID=004 and mp2.[Key] =dbk.TantouEigyouCD
	left join M_MultiPorpose as mp4 on mp4.ID=008 and mp4.[Key] =dbk.KanamonoCD
  
	WHERE dbk.TantouCadCD is Null
	order by  dbk.Nouki ASC, dbk.BukkenNO ASC
END
