IF EXISTS (select * from sys.objects where name = 'SmitumoriKanri_InsertUpdateDataResult')
BEGIN
    DROP PROCEDURE [SmitumoriKanri_InsertUpdateDataResult]
END
GO

CREATE PROCEDURE [dbo].[SmitumoriKanri_InsertUpdateDataResult]
	-- Add the parameters for the stored procedure here
	@DetailDataJson		VARCHAR(MAX),
	@UserID				VARCHAR(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT *
	INTO #temp
	FROM OPENJSON(@DetailDataJson)
	WITH( 
	SagyouDate VARCHAR(10) '$.SagyouDate' ,
	TantouCadCD VARCHAR(10) '$.TantouCadCD',
	BukkenName VARCHAR(30) '$.BukkenName' ,
	KoumutenName VARCHAR(50) '$.KoumutenName',
	KakoutuboSuu DECIMAL(5,2) '$.KakoutuboSuu' ,
	TantouSitenCD VARCHAR(10) '$.TantouSitenCD',
	IraiEigyouCD VARCHAR(10) '$.IraiEigyouCD' ,
	KaesiYoteiDate VARCHAR(10) '$.KaesiYoteiDate',
	SMitumoriCounter INT '$.SMitumoriCounter'
	)

	UPDATE ds
	SET ds.SagyouDate = t.SagyouDate, 
		ds.TantouCadCD = t.TantouCadCD,
		ds.BukkenName = t.BukkenName,
		ds.KoumutenName = t.KoumutenName,
		ds.KakoutuboSuu = t.KakoutuboSuu,
		ds.TantouSitenCD = t.TantouSitenCD,
		ds.IraiEigyouCD = t.IraiEigyouCD,
		ds.KaesiYoteiDate = t.KaesiYoteiDate,
		ds.UpdateOperator = @UserID,
		ds.UpdateDateTime = GETDATE()
	FROM D_SMitumoriKanri ds
	INNER JOIN #temp t ON t.SMitumoriCounter = ds.SMitumoriCounter

	WHILE EXISTS (SELECT 1 FROM #temp WHERE SMitumoriCounter = 0)
	BEGIN
		DECLARE @SMitumoriCounter INT
		SELECT @SMitumoriCounter = SMitumoriCounter + 1 FROM M_Control WHERE MainKey = 1
		
		UPDATE M_Control SET SMitumoriCounter = SMitumoriCounter + 1 WHERE MainKey = 1

		UPDATE t
		SET SMitumoriCounter = @SMitumoriCounter
		FROM (SELECT TOP 1 * FROM #temp WHERE SMitumoriCounter = 0) t
	END

	INSERT INTO D_SMitumoriKanri(SMitumoriCounter, SagyouDate, TantouCadCD, BukkenName, KoumutenName, KakoutuboSuu, TantouSitenCD, IraiEigyouCD, KaesiYoteiDate, InsertOperator, InsertDateTime, UpdateOperator, UpdateDateTime)
	SELECT SMitumoriCounter, SagyouDate, TantouCadCD, BukkenName, KoumutenName, KakoutuboSuu, TantouSitenCD, IraiEigyouCD, KaesiYoteiDate, @UserID, GETDATE(), @UserID, GETDATE()
	FROM #temp t
	WHERE NOT EXISTS (SELECT ds.SMitumoriCounter FROM D_SMitumoriKanri ds WHERE t.SMitumoriCounter = ds.SMitumoriCounter)

	DROP TABLE #temp
END
