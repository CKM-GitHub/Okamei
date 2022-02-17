IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_Delete')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_Delete]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_Delete](
     @BukkenNO                  varchar(8)
    ,@UpdateDateTime            varchar(23)
    ,@OutExclusionError         tinyint OUTPUT
)AS
BEGIN
    SET @OutExclusionError = 0

    DELETE D_Bukken
    WHERE BukkenNO = @BukkenNO
    AND   UpdateDateTime = @UpdateDateTime

    IF @@ROWCOUNT = 0
    BEGIN
        SET @OutExclusionError = 1
        RETURN
    END

	DELETE D_BukkenFile
	WHERE BukkenNO = @BukkenNO

	DELETE D_BukkenComment
	WHERE BukkenNO = @BukkenNO

    DELETE D_BukkenHiuchi
    WHERE BukkenNO = @BukkenNO

    DELETE D_BukkenTeKakou
    WHERE BukkenNO = @BukkenNO

    DELETE D_BukkenMoulder
    WHERE BukkenNO = @BukkenNO

END
