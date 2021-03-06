IF EXISTS (select * from sys.objects where name = 'InputStep_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [InputStep_SelectDisplayResult]
END
GO
CREATE PROCEDURE [dbo].[InputStep_SelectDisplayResult]
	
	@TantouSitenCD          varchar(10),
    @JuchuuOption          tinyint,
    @BukkenSiteiOption     tinyint,
    @NoukiStart            date,
    @NoukiEnd              date,
    @TantouEigyouCD        varchar(10),
    @TantouCadCD           varchar(10),
	@TantouPcCD				varchar(10),
	@Dankai1                tinyint,
	@Dankai2                tinyint,
    @UserID                varchar(10)
	

AS
BEGIN
	
	SET NOCOUNT ON;

    
	declare @Data tinyint=1

 
 CREATE TABLE [dbo].[#Tmp_Denkai](
	JuchuuDate         varchar(10),
    Nouki          varchar(10),
    TempDankai     tinyint,
    BukkenNO         varchar(8),
    BukkenName       varchar(16),
    KoumutenName   varchar(30),
    KakoutuboSuu      decimal(5,2),
	TantouSitenName		varchar(6),
	TantouEigyouName  varchar(6),
	TantouCadName varchar(6),
	KanamonName varchar(10)
	
    ) 

	INSERT into #Tmp_Denkai
	SELECT
			cast (FORMAT(dbk.JuchuuDate, 'MM/dd')  as varchar(10) )   AS JuchuuDate,
	CASE	dbk.NoukiMiteiKBN WHEN 1 THEN '未定'
			 ELSE cast (FORMAT(dbk.Nouki, 'MM/dd')  as varchar(10) ) END AS Nouki,
	CASE		WHEN dbk.KakoutuShouninDate IS NOT NULL  THEN 3
             WHEN dbk.FusezuTeishutuDate   IS NOT NULL  THEN 2
             WHEN dbk.FusezuTeishutuDate   IS  NULL  THEN 1
             ELSE '' END                                            AS TempDankai,

			 dbk.BukkenNO,
			 CONVERT(varchar(16), dbk.BukkenName)					AS BukkenName,
			 CONVERT(varchar(30), dbk.KoumutenName)					AS KoumutenName,
			 FORMAT(dbk.KakoutuboSuu, '##0.00')						AS KakouTubosuu,
			 CONVERT(varchar(6), mp1.Char2)							AS TantouSitenName,
			 CONVERT(varchar(6), mp2.Char2)							AS TantouEigyouName,
			 CONVERT(varchar(6), mp3.Char2)							AS TantouCadName,
			 CONVERT(varchar(10), mp4.Char1)						AS KanamonName

		
    FROM	 D_Bukken dbk
    LEFT OUTER JOIN M_MultiPorpose mp1 ON mp1.ID = 3 AND mp1.[Key] = dbk.TantouSitenCD
    LEFT OUTER JOIN M_MultiPorpose mp2 ON mp2.ID = 4 AND mp2.[Key] = dbk.TantouEigyouCD
    LEFT OUTER JOIN M_MultiPorpose mp3 ON mp3.ID = 6 AND mp3.[Key] = dbk.TantouCadCD
    LEFT OUTER JOIN M_MultiPorpose mp4 ON mp4.ID = 8 AND mp4.[Key] = dbk.KanamonoCD

    WHERE	 (@TantouSitenCD IS NULL OR dbk.TantouSitenCD = @TantouSitenCD)

			 AND ((@JuchuuOption = 1 AND dbk.CancelDate IS NULL) 
			     OR (@JuchuuOption = 2 AND dbk.CancelDate IS NOT NULL)
			     )
			 
			 AND (@BukkenSiteiOption <> 2 
			     OR (dbk.TantouEigyouCD = @UserID 
			     OR dbk.TantouPcCD = @UserID
			     OR dbk.TantouCadCD = @UserID
			     ))
			 
			 AND (@NoukiStart IS NULL OR dbk.Nouki >= @NoukiStart)
			 AND (@NoukiEnd IS NULL OR dbk.Nouki <= @NoukiEnd)
			 AND (@TantouEigyouCD IS NULL OR dbk.TantouEigyouCD = @TantouEigyouCD)
			 AND (@TantouCadCD IS NULL OR dbk.TantouCadCD = @TantouCadCD)
			 AND (@TantouPcCD IS NULL OR dbk.TantouPcCD = @TantouPcCD)

	SELECT
			 JuchuuDate,
			 Nouki, 
			 CASE WHEN  #Tmp_Denkai.TempDankai =1  THEN '未'
			 WHEN #Tmp_Denkai.TempDankai   =2	THEN '待'
			 WHEN #Tmp_Denkai.TempDankai   =3	THEN '指'
			 ELSE '' END As Dankai 
			 
			 ,BukkenNO
			 ,BukkenName
			 ,KoumutenName
			 ,FORMAT(KakoutuboSuu, '##0.00')		AS KakouTubosuu
			 ,TantouSitenName
			 ,TantouEigyouName
			 ,TantouCadName
			 ,KanamonName 

	FROM     #Tmp_Denkai 
	WHERE    @Data =1 
			AND (
				((@Dankai1=0 AND @Dankai2=0) OR  #Tmp_Denkai.TempDankai=@Dankai1)
				OR 
				((@Dankai1=0 AND @Dankai2=0) OR    #Tmp_Denkai.TempDankai=@Dankai2)
				)
				
	ORDER BY Nouki Asc,TempDankai Asc
    DROP TABLE #Tmp_Denkai
    
END
