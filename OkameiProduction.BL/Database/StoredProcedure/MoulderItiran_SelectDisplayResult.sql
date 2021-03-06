IF EXISTS (select * from sys.objects where name = 'MoulderItiran_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [MoulderItiran_SelectDisplayResult]
END
GO

CREATE PROCEDURE [dbo].[MoulderItiran_SelectDisplayResult]
	-- Add the parameters for the stored procedure here
	@TantouSitenCD          varchar(10),
    @NoukiStart            date,
    @NoukiEnd              date,
    @UserID                varchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT (CASE WHEN buk.NoukiMiteiKBN = 1 THEN '未定'
			WHEN buk.Nouki  is null THEN null
            ELSE FORMAT(buk.Nouki, 'MM/dd') END) AS Nouki,
		buk.BukkenNO,
		CONVERT(varchar(16), buk.BukkenName) AS BukkenName,
		CONVERT(varchar(10), mmp1.Char1) AS Name,
		CONVERT(varchar(20), mmp2.Char1) AS Material,
		CONVERT(varchar(20), CAST(dbm.MoulderHaba AS VARCHAR(10)) + ' × ' + CAST(dbm.MoulderNari AS VARCHAR(10)) + ' × ' + CAST(dbm.MoulderNagasa AS VARCHAR(10))) AS Content,
		CONVERT(varchar(3), dbm.MoulderHonsuu) AS MoulderHonsuu		
		--FORMAT(dbm.MoulderKakouDateTime, 'yyyy-MM-dd HH:mm:ss') AS MoulderKakouDateTime
    FROM D_Bukken buk 
	INNER JOIN D_BukkenMoulder dbm ON dbm.BukkenNO = buk.BukkenNO
	LEFT OUTER JOIN M_MultiPorpose mmp1 ON mmp1.ID = '013' AND mmp1.[Key] = dbm.MoulderHinmoku
	LEFT OUTER JOIN M_MultiPorpose mmp2 ON mmp2.ID = '014' AND mmp2.[Key] = dbm.MoulderZairyou
    WHERE (@TantouSitenCD IS NULL OR buk.TantouSitenCD = @TantouSitenCD)
      AND (@NoukiStart IS NULL OR buk.Nouki >= @NoukiStart)
      AND (@NoukiEnd IS NULL OR buk.Nouki <= @NoukiEnd)
	  AND dbm.MoulderSumi = 0
	ORDER BY buk.Nouki ASC, buk.BukkenNO ASC
END
