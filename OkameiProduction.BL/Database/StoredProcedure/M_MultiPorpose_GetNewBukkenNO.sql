IF EXISTS (select * from sys.objects where name = 'M_MultiPorpose_GetNewBukkenNO')
BEGIN
    DROP PROCEDURE [M_MultiPorpose_GetNewBukkenNO]
END
GO

CREATE PROCEDURE [dbo].[M_MultiPorpose_GetNewBukkenNO](
	@TantouSitenCD varchar(10)
)AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix varchar(8)
    DECLARE @Number int

    UPDATE M_MultiPorpose SET 
        @Prefix = Char4,
        @Number = Num2 = ISNULL(Num2,0) + 1
    WHERE ID = 3
    AND [Key] = @TantouSitenCD

    SELECT @Prefix  AS Prefix,
           @Number AS Number
END
