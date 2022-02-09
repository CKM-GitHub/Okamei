IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_DeleteBukkenFile')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_DeleteBukkenFile]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_DeleteBukkenFile](
     @BukkenNO                  varchar(8)
    ,@BukkenFileRows            int
    ,@UpdateDatetime            varchar(23)
    ,@OutExclusionError         tinyint OUTPUT
)AS
BEGIN

	DELETE D_BukkenFile
	WHERE BukkenNO = @BukkenNO
    AND   BukkenFileRows = @BukkenFileRows

END
