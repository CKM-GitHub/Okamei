IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_Delete')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_Delete]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_Delete](
     @BukkenNO                  varchar(8)
    ,@Operator                  varchar(10)
	,@UpdateDatetime			datetime
	,@BukkenCommentTable		T_BukkenComment READONLY
	,@BukkenMoulderTable		T_BukkenMoulder READONLY
)AS
BEGIN
	DECLARE @Error tinyint = 1

    DELETE D_Bukken
    WHERE BukkenNO = @BukkenNO
    AND   UpdateDateTime = @UpdateDatetime

	IF @@ROWCOUNT = 0 RETURN @Error


END
