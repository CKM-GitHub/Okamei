IF EXISTS (select * from sys.objects where name = 'M_MultiPorpose_SelectForDropDownLit')
BEGIN
    DROP PROCEDURE [M_MultiPorpose_SelectForDropDownLit]
END
GO

CREATE PROCEDURE [dbo].[M_MultiPorpose_SelectForDropDownLit](
    @ID  int,
	@Char4 varchar(100) = NULL
)AS
BEGIN
    SET NOCOUNT ON;

	IF @ID = 1
	BEGIN
		SELECT
			[Key]  AS [Value]
			,CONVERT(varchar(50), Char1) AS DisplayText
			,ROW_NUMBER() OVER(ORDER BY Char3)  AS SortBy
		FROM M_MultiPorpose
		WHERE ID = @ID
		AND (@Char4 IS NULL OR Char4 = @Char4)
		ORDER BY Char3
	END
	
	--add by TZA for the dropdownlists of SmitumoriKanri Form
	ELSE IF @Char4 = N'SmitumoriKanri'
	BEGIN
		SELECT
			[Key]  AS [Value]
			,CONVERT(varchar(50), Char2) AS DisplayText
			,Num1  AS SortBy
		FROM M_MultiPorpose
		WHERE ID = @ID
		ORDER BY Num1
	END

	BEGIN
		SELECT
			[Key]  AS [Value]
			,CONVERT(varchar(50), Char1) AS DisplayText
			,Num1  AS SortBy
		FROM M_MultiPorpose
		WHERE ID = @ID
		AND (@Char4 IS NULL OR Char4 = @Char4)
		ORDER BY Num1
	END
END
