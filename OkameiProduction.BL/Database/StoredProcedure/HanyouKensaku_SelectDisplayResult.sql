
IF EXISTS (select * from sys.objects where name = 'HanyouKensaku_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [HanyouKensaku_SelectDisplayResult]
END
GO 
Create PROCEDURE [dbo].[HanyouKensaku_SelectDisplayResult]
	-- Add the parameters for the stored procedure here
	@ID 			VARCHAR(10),	
    @UserID         VARCHAR(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ID,
		[Key],
		Char1,
		Char2,
		Char3
	FROM M_MultiPorpose
	WHERE @ID IS NULL OR ID = @ID
	ORDER BY Num1 ASC, [Key] ASC
END
