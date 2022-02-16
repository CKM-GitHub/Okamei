IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_DeleteBukkenComment')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_DeleteBukkenComment]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_DeleteBukkenComment](
     @BukkenNO                  varchar(8)
    ,@BukkenCommentRows         int
    ,@UpdateDateTime            varchar(23)
    ,@OutExclusionError         tinyint OUTPUT
)AS
BEGIN

	DELETE D_BukkenComment
	WHERE BukkenNO = @BukkenNO
    AND   BukkenCommentRows = @BukkenCommentRows

END
