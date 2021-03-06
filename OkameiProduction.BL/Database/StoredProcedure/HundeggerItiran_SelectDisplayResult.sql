IF EXISTS (select * from sys.objects where name = 'HundeggerItiran_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [HundeggerItiran_SelectDisplayResult]
END
GO

CREATE PROCEDURE [dbo].[HundeggerItiran_SelectDisplayResult]

	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @SystemDate date = GETDATE()

		SELECT	 
		
			CASE NoukiMiteiKBN WHEN 1 THEN '未定' 
			   ELSE FORMAT(Nouki, 'MM/dd') END				AS Nouki,
			BukkenNO,
			CONVERT(varchar(16), BukkenName)				AS BukkenName,
			CONVERT(varchar(30), KoumutenName)				AS KoumutenName,
			FORMAT(KakoutuboSuu, '##0.00')					AS KakouTubosuu,
			FORMAT(KakousijishoHakkouDate, 'MM/dd')			AS KakousijishoHakkouDate,
			CASE HundeggerTime WHEN 0 THEN '' 
			   ELSE FORMAT(HundeggerTime, '##0.0') END		AS HundeggerTime

			FROM D_Bukken  
			WHERE HundeggerKakou =1
			AND Nouki >= @SystemDate
			Order by Nouki ASC ,BukkenNO ASC
	
END
