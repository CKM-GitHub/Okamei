IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_Delete')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_Delete]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_Delete](
     @BukkenNO                  varchar(8)
    ,@Operator                  varchar(10)
    ,@UpdateDatetime            varchar(23)
    ,@OutExclusionError         tinyint OUTPUT
)AS
BEGIN
    SET @OutExclusionError = 0

    DELETE D_Bukken
    WHERE BukkenNO = @BukkenNO
    AND   UpdateDateTime = @UpdateDatetime

    IF @@ROWCOUNT = 0
    BEGIN
        SET @OutExclusionError = 1
        RETURN
    END

	DELETE D_BukkenComment
	WHERE BukkenNO = @BukkenNO

    DELETE D_BukkenMoulder
    WHERE BukkenNO = @BukkenNO

END
