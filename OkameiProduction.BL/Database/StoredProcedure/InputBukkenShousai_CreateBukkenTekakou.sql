IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_CreateBukkenTeKakou')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_CreateBukkenTeKakou]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_CreateBukkenTeKakou](
    @BukkenNO                   varchar(8)
    ,@TeKakou1Honsuu            int
    ,@TeKakou2Honsuu            int
    ,@TeKakou3Honsuu            int
    ,@TeKakou4Honsuu            int
    ,@TeKakou5Honsuu            int
    ,@TeKakou6Honsuu            int
    ,@TeKakou7Honsuu            int
    ,@TeKakou8Honsuu            int
    ,@Operator                  varchar(10)
    ,@UpdateDateTime            varchar(23)
    ,@OutExclusionError         tinyint OUTPUT
)AS
BEGIN
    SET @OutExclusionError = 0
    DECLARE @SysDatetime datetime = GETDATE()

    DELETE FROM D_BukkenTeKakou
    WHERE BukkenNO = @BukkenNO
    AND UpdateDateTime = @UpdateDateTime

    IF @@ROWCOUNT = 0 AND @UpdateDateTime IS NOT NULL
    BEGIN
        SET @OutExclusionError = 1
        RETURN
    END

    IF @TeKakou1Honsuu <> 0 OR @TeKakou2Honsuu <> 0 OR @TeKakou3Honsuu <> 0 OR @TeKakou4Honsuu <> 0
    OR @TeKakou5Honsuu <> 0 OR @TeKakou6Honsuu <> 0 OR @TeKakou7Honsuu <> 0 OR @TeKakou8Honsuu <> 0
	BEGIN
        INSERT INTO D_BukkenTeKakou
        (
            BukkenNO
            ,TeKakou1Honsuu
            ,TeKakou2Honsuu
            ,TeKakou3Honsuu
            ,TeKakou4Honsuu
            ,TeKakou5Honsuu
            ,TeKakou6Honsuu
            ,TeKakou7Honsuu
            ,TeKakou8Honsuu
            ,InsertOperator
            ,InsertDateTime
            ,UpdateOperator
            ,UpdateDateTime
        )
        VALUES
        (
             @BukkenNO
            ,@TeKakou1Honsuu
            ,@TeKakou2Honsuu
            ,@TeKakou3Honsuu
            ,@TeKakou4Honsuu
            ,@TeKakou5Honsuu
            ,@TeKakou6Honsuu
            ,@TeKakou7Honsuu
            ,@TeKakou8Honsuu
            ,@Operator
            ,@SysDatetime
            ,@Operator
            ,@SysDatetime
        )
	END
END
