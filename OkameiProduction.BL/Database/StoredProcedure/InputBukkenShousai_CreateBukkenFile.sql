IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_CreateBukkenFile')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_CreateBukkenFile]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_CreateBukkenFile](
     @BukkenNO                  varchar(8)
    ,@BukkenFileShurui          tinyint
    ,@BukkenFileName            varchar(100)
    ,@Operator                  varchar(10)
    ,@UpdateDatetime            varchar(23)
    ,@OutExclusionError         tinyint OUTPUT
)AS
BEGIN
    SET @OutExclusionError = 0
    DECLARE @SysDatetime datetime = GETDATE()

    --IF EXISTS (SELECT BukkenNO FROM D_Bukken WHERE BukkenNO = @BukkenNO AND UpdateDatetime <> @UpdateDatetime)
    --BEGIN
    --    SET @OutExclusionError = 1
    --    RETURN
    --END

    INSERT INTO D_BukkenFile
    (
         BukkenNO
        ,BukkenFileRows
        ,BukkenFileShurui
        ,BukkenFileName
        ,InsertOperator
        ,InsertDateTime
        ,UpdateOperator
        ,UpdateDateTime
    )
    VALUES
    (
         @BukkenNO
        ,ISNULL((SELECT MAX(BukkenFileRows) + 1 FROM D_BukkenFile WHERE BukkenNO = @BukkenNO), 1)
        ,@BukkenFileShurui
        ,@BukkenFileName
        ,@Operator
        ,@SysDatetime
        ,@Operator
        ,@SysDatetime
    )
END
