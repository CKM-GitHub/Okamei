IF EXISTS (select * from sys.objects where name = 'SmitumoriKanri_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [SmitumoriKanri_SelectDisplayResult]
END
GO

CREATE PROCEDURE [dbo].[SmitumoriKanri_SelectDisplayResult]
	-- Add the parameters for the stored procedure here
	@sagyou_sDate		VARCHAR(10),
	@sagyou_eDate		VARCHAR(10),
	@UserID				VARCHAR(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    SELECT 
		CONVERT(VARCHAR(10), ds.SagyouDate, 111) AS SagyouDate,
		--mmp1.Char2 AS TantouCad,
		ds.TantouCadCD,
		CONVERT(VARCHAR(30), ds.BukkenName) AS BukkenName,
		ds.KoumutenName AS KoumutenName,
		ds.KakoutuboSuu,
		--mmp3.Char2 AS TantouSiten,
		ds.TantouSitenCD,
		--mmp2.Char2 AS TantouEigyou,
		ds.IraiEigyouCD,
		CONVERT(VARCHAR(10), ds.KaesiYoteiDate, 111) AS KaesiYoteiDate,
		ds.SMitumoriCounter
	FROM D_SMitumoriKanri ds
	--LEFT JOIN M_MultiPorpose mmp1 ON mmp1.ID = 006 AND mmp1.[Key] = ds.TantouCadCD
	--LEFT JOIN M_MultiPorpose mmp2 ON mmp2.ID = 004 AND mmp2.[Key] = ds.IraiEigyouCD
	--LEFT JOIN M_MultiPorpose mmp3 ON mmp3.ID = 003 AND mmp3.[Key] = ds.TantouSitenCD
	WHERE (@sagyou_sDate IS NULL OR ds.SagyouDate >= @sagyou_sDate) AND (@sagyou_eDate IS NULL OR ds.SagyouDate <= @sagyou_eDate)
	ORDER BY ds.SagyouDate ASC--, mmp1.Num1 ASC
END
