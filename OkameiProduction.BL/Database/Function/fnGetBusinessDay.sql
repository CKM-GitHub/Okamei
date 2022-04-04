IF EXISTS (select * from sys.objects where name = 'fnGetBusinessDay')
BEGIN
    DROP FUNCTION [fnGetBusinessDay]
END
GO

CREATE FUNCTION [dbo].[fnGetBusinessDay](
     @TargetDate    AS date
    ,@AddDays       AS int
)
RETURNS date
BEGIN  

    DECLARE @ReturnValue date
    DECLARE @Table  table (BusinessDay date, OrderNO int)
    DECLARE @DateTo date

    IF @AddDays < 0
    BEGIN
        SET @DateTo = DATEADD(dd, @AddDays - 30, @TargetDate)

        ;WITH DateTable (MyDate)
        AS
        (
            SELECT FORMAT(DATEADD(dd, -1, @TargetDate), 'yyyyMMdd') AS MyDate --指定日の前日が1日目
            UNION ALL
            SELECT FORMAT(DATEADD(dd, -1, MyDate), 'yyyyMMdd') AS MyDate
            FROM   DateTable
            WHERE  MyDate > @DateTo
        )
        INSERT  INTO @Table
        SELECT  myDate, ROW_NUMBER()OVER(ORDER BY MyDate DESC) AS OrderNO 
        FROM    DateTable    
        WHERE   NOT EXISTS(SELECT * FROM M_MultiPorpose WHERE ID = 16 AND [Key] = MyDate)
        OPTION　(MAXRECURSION 500)
    END
    ELSE
    BEGIN
        SET @DateTo = DATEADD(dd, @AddDays + 30, @TargetDate)

        ;WITH DateTable (MyDate)
        AS
        (
            SELECT FORMAT(DATEADD(dd, 1, @TargetDate), 'yyyyMMdd') AS MyDate --指定日の翌日が1日目
            UNION ALL
            SELECT FORMAT(DATEADD(dd, 1, MyDate), 'yyyyMMdd')
            FROM   DateTable
            WHERE  MyDate < @DateTo
        )
        INSERT  INTO @Table
        SELECT  MyDate, ROW_NUMBER()OVER(ORDER BY MyDate) AS OrderNO 
        FROM    DateTable    
        WHERE   NOT EXISTS(SELECT * FROM M_MultiPorpose WHERE ID = 16 AND [Key] = MyDate)
        OPTION (MAXRECURSION 500);
    END

    SELECT @ReturnValue = BusinessDay FROM @Table WHERE OrderNO = ABS(@AddDays)
    RETURN @ReturnValue
END
