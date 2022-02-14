IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_CreateBukkenComment')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_CreateBukkenComment]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_CreateBukkenComment](
     @BukkenNO                  varchar(8)
    ,@BukkenComment             varchar(100)
    ,@Operator                  varchar(10)
    ,@UpdateDateTime            varchar(23)
    ,@OutExclusionError         tinyint OUTPUT
)AS
BEGIN
    SET @OutExclusionError = 0
    DECLARE @SysDatetime datetime = GETDATE()

    --IF EXISTS (SELECT BukkenNO FROM D_Bukken WHERE BukkenNO = @BukkenNO AND UpdateDatetime <> @UpdateDateTime)
    --BEGIN
    --    SET @OutExclusionError = 1
    --    RETURN
    --END

    INSERT INTO D_BukkenComment (
        BukkenNO
        ,BukkenCommentRows
        ,BukkenComment
        ,InsertOperator
        ,InsertDateTime
        ,UpdateOperator
        ,UpdateDateTime
    ) VALUES (
        @BukkenNO
        ,ISNULL((SELECT MAX(BukkenCommentRows) + 1 FROM D_BukkenComment WHERE BukkenNO = @BukkenNO), 1)
        ,@BukkenComment
        ,@Operator
        ,@SysDatetime
        ,@Operator
        ,@SysDatetime
    )

END
