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

    IF @AddDays = 0
    BEGIN
        RETURN @TargetDate
    END

    DECLARE @ReturnValue date
    DECLARE @DateTo date

    IF @AddDays < 0
    BEGIN
        SET @DateTo = DATEADD(dd, @AddDays - 30, @TargetDate)

        SELECT @ReturnValue = CalendarDate FROM
        (
            SELECT  CalendarDate, ROW_NUMBER()OVER(ORDER BY CalendarDate DESC) AS OrderNO 
            FROM    M_Calendar    
            WHERE   NOT EXISTS(SELECT * FROM M_MultiPorpose WHERE ID = 16 AND [Key] = CalendarDate)
            AND     CalendarDate > @DateTo 
            AND     CalendarDate < @TargetDate
        ) AS tbl
        WHERE tbl.OrderNO = ABS(@AddDays)
    END
    ELSE
    BEGIN
        SET @DateTo = DATEADD(dd, @AddDays + 30, @TargetDate)

        SELECT @ReturnValue = CalendarDate FROM
        (
            SELECT  CalendarDate, ROW_NUMBER()OVER(ORDER BY CalendarDate) AS OrderNO 
            FROM    M_Calendar    
            WHERE   NOT EXISTS(SELECT * FROM M_MultiPorpose WHERE ID = 16 AND [Key] = CalendarDate)
            AND     CalendarDate > @TargetDate
            AND     CalendarDate < @DateTo 
        ) AS tbl
        WHERE tbl.OrderNO = ABS(@AddDays)
    END

    RETURN @ReturnValue
END
