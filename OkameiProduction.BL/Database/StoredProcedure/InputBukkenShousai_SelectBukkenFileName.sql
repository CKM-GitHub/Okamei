IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_SelectBukkenFileName')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_SelectBukkenFileName]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_SelectBukkenFileName](
     @BukkenNO  varchar(8),
     @BukkenFileRows int
)AS
BEGIN
    SELECT dbf.BukkenFileName                                 AS BukkenFileName

    FROM D_BukkenFile dbf

    WHERE BukkenNO = @BukkenNO
    AND BukkenFileRows = @BukkenFileRows

END
