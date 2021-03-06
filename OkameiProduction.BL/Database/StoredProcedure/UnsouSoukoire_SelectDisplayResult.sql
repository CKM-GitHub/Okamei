IF EXISTS (select * from sys.objects where name = 'UnsouSoukoire_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [UnsouSoukoire_SelectDisplayResult]
END
GO
CREATE PROCEDURE [dbo].[UnsouSoukoire_SelectDisplayResult] 
	
AS
BEGIN
	SET NOCOUNT ON;

   
	Declare @SystemDate as date=GETDATE()
SELECT	 
		
		FORMAT(dbk.UnsouKuraireDate, 'MM/dd')   AS UnsouKuraireDate,
		dbk.BukkenNO,
		CONVERT(varchar(16), dbk.BukkenName)	AS BukkenName,
		CONVERT(varchar(30), dbk.KoumutenName)  AS KoumutenName,
		FORMAT(dbk.KakoutuboSuu, '##0.00')		AS KakouTubosuu,
		CONVERT(varchar(10), mp1.Char1)			AS TantouSitenName,
		CONVERT(varchar(6), mp2.Char2)			AS TantouEigyouName,
		CONVERT(varchar(6), mp3.Char2)			AS TantouPcName,
		CONVERT(varchar(6), mp4.Char2)			AS TantouCadName,
		CASE dbk.NoukiMiteiKBN WHEN 1 THEN '未定' 
           ELSE FORMAT(dbk.Nouki, 'MM/dd') END  AS Nouki,
		CONVERT(varchar(10), mp5.Char1)			AS KanamonName
		
	FROM D_Bukken AS dbk
	left join M_MultiPorpose as mp1 on mp1.ID=003 and mp1.[Key] =dbk.TantouSitenCD
	left join M_MultiPorpose as mp2 on mp2.ID=004 and mp2.[Key] =dbk.TantouEigyouCD
	left join M_MultiPorpose as mp3 on mp3.ID=005 and mp3.[Key] =dbk.TantouPcCD
	left join M_MultiPorpose as mp4 on mp4.ID=006 and mp4.[Key] =dbk.TantouCadCD
	left join M_MultiPorpose as mp5 on mp5.ID=008 and mp5.[Key] =dbk.KanamonoCD
  
	WHERE dbk.UnsouKuraireDate  is not Null
	and dbk.Nouki >=@SystemDate
	order by  dbk.UnsouKuraireDate ASC, dbk.BukkenNO ASC
END
