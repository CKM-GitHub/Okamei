IF EXISTS (select * from sys.objects where name = 'SmitumoriKanri_DeleteDataResult')
BEGIN
    DROP PROCEDURE [SmitumoriKanri_DeleteDataResult]
END
GO

CREATE PROCEDURE [dbo].[SmitumoriKanri_DeleteDataResult]
	-- Add the parameters for the stored procedure here
	@SMitumoriCounter		INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM D_SMitumoriKanri WHERE SMitumoriCounter = @SMitumoriCounter
END
