IF EXISTS (select * from sys.objects where name = 'fnGetSijiKijitu')
BEGIN
    DROP FUNCTION [fnGetSijiKijitu]
END
GO

CREATE FUNCTION [dbo].[fnGetSijiKijitu](
     @Nouki             AS date
    ,@UnsouKuraireDate  AS date
    ,@KanamonoCD        AS varchar(10)
    ,@TekakouTime       AS decimal(5, 2)
    ,@HundeggerTime     AS decimal(5, 2)
)
RETURNS date
BEGIN  

    DECLARE @MaxDate date = '2099/01/01'
    DECLARE @KijunDate date
    
    SET @KijunDate = dbo.fnGetBusinessDay(
        CASE WHEN ISNULL(@Nouki, @MaxDate) < ISNULL(@UnsouKuraireDate, @MaxDate) 
        THEN @Nouki 
        ELSE @UnsouKuraireDate END, -5)

    IF @KanamonoCD IS NOT NULL
    BEGIN
        SET @KijunDate = dbo.fnGetBusinessDay(@KijunDate, -2)
    END

    IF @TekakouTime <> 0
    BEGIN
        SET @KijunDate = dbo.fnGetBusinessDay(@KijunDate, FLOOR(@TekakouTime / 3) * -1)
    END

    IF @HundeggerTime <> 0
    BEGIN
        SET @KijunDate = dbo.fnGetBusinessDay(@KijunDate, FLOOR(@HundeggerTime / 3) * -1)
    END

    RETURN @KijunDate
END
