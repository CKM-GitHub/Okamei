IF EXISTS (select * from sys.objects where name = 'M_MultiPorpose_SelectByID')
BEGIN
    DROP PROCEDURE [M_MultiPorpose_SelectByID]
END
GO

CREATE PROCEDURE [dbo].[M_MultiPorpose_SelectByID](
    @ID			INT
)AS
BEGIN
    SET NOCOUNT ON;
    
	SELECT 
		ID
		,[Key]
		,IDName
		,Char1
		,Char2
		,Char3
		,Char4
		,Char5
		,Num1
		,Num2
		,Num3
		,Num4
		,Num5
		,Date1
		,Date2
		,Date3
		,Date4
		,Date5
		,InsertOperator
		,CONVERT(varchar, InsertDateTime, 121) AS InsertDateTime
		,UpdateOperator
		,CONVERT(varchar, UpdateDateTime, 121) AS UpdateDateTime
	FROM M_MultiPorpose
	WHERE ID = @ID
	ORDER BY [Key]

END