IF EXISTS (select * from sys.objects where name = 'M_MultiPorpose_SelectForDropDownLit')
BEGIN
    DROP PROCEDURE [M_MultiPorpose_SelectForDropDownLit]
END
GO

CREATE PROCEDURE [dbo].[M_MultiPorpose_SelectForDropDownLit](
    @ID  int
)AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [Key]  AS [Value]
        ,Char1 AS DisplayText
        ,Num1  AS SortBy
    FROM M_MultiPorpose
    WHERE ID = @ID
    ORDER BY Num1
END
