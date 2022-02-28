IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_CreateBukkenMoulder')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_CreateBukkenMoulder]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_CreateBukkenMoulder](
    @BukkenNO                   varchar(8)
	,@BukkenMoulderTable		T_BukkenMoulder READONLY
    ,@Operator                  varchar(10)
    ,@UpdateDateTime            varchar(23)
    ,@OutExclusionError         tinyint OUTPUT
)AS
BEGIN
    SET @OutExclusionError = 0
    DECLARE @SysDatetime datetime = GETDATE()


    DECLARE @OldBukkenMoulderTable T_BukkenMoulder
    INSERT INTO @OldBukkenMoulderTable
    (
        BukkenMoulderRows
        ,MoulderKakouDateTime
    )
    SELECT
        BukkenMoulderRows
        ,MoulderKakouDateTime
    FROM D_BukkenMoulder
    WHERE BukkenNO = @BukkenNO


    DELETE FROM D_BukkenMoulder
    WHERE BukkenNO = @BukkenNO
    AND UpdateDateTime = @UpdateDateTime

    IF @@ROWCOUNT = 0 AND @UpdateDateTime IS NOT NULL
    BEGIN
        SET @OutExclusionError = 1
        RETURN
    END


    INSERT INTO D_BukkenMoulder
    (
        BukkenNO
        ,BukkenMoulderRows
        ,MoulderHinmoku
        ,MoulderZairyou
        ,MoulderHaba
        ,MoulderNari
        ,MoulderNagasa
        ,MoulderHonsuu
        ,MoulderKakouDateTime
        ,MoulderSumi
        ,InsertOperator
        ,InsertDateTime
        ,UpdateOperator
        ,UpdateDateTime
    )
	SELECT 
        @BukkenNO
        ,ROW_NUMBER() OVER (ORDER BY new.DisplayOrder)
        ,new.MoulderHinmoku
        ,new.MoulderZairyou
        ,new.MoulderHaba
        ,new.MoulderNari
        ,new.MoulderNagasa
        ,new.MoulderHonsuu
        ,CASE new.MoulderSumi WHEN 1 THEN ISNULL(old.MoulderKakouDateTime, @SysDatetime) ELSE NULL END
        ,new.MoulderSumi
        ,@Operator
        ,@SysDatetime
        ,@Operator
        ,@SysDatetime
	FROM @BukkenMoulderTable AS new
    LEFT OUTER JOIN @OldBukkenMoulderTable AS old ON old.BukkenMoulderRows = new.BukkenMoulderRows

END
