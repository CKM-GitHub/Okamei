IF EXISTS (select * from sys.objects where name = 'M_MultiPorpose_SelectUpdateBukkenNO')
BEGIN
    DROP PROCEDURE [M_MultiPorpose_SelectUpdateBukkenNO]
END
GO

CREATE PROCEDURE [dbo].[M_MultiPorpose_SelectUpdateBukkenNO](
	@SitenCD varchar(10)
)AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix varchar(8)
    DECLARE @Number int

    UPDATE M_MultiPorpose SET 
        @Prefix = Char4,
        @Number = Num2 = ISNULL(Num2,0) + 1
    WHERE ID = 3
    AND [Key] = @SitenCD

    SELECT @Prefix  AS Prefix,
           @Number AS Number
END
