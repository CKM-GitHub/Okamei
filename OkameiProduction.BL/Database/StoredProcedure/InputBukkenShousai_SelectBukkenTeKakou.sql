IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_SelectBukkenTeKakou')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_SelectBukkenTeKakou]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_SelectBukkenTeKakou](
     @BukkenNO  varchar(8)
)AS
BEGIN
    SET NOCOUNT ON

    SELECT 
        BukkenNO
        ,FORMAT(TeKakou1Honsuu, '###') AS TeKakou1Honsuu
        ,FORMAT(TeKakou2Honsuu, '###') AS TeKakou2Honsuu
        ,FORMAT(TeKakou3Honsuu, '###') AS TeKakou3Honsuu
        ,FORMAT(TeKakou4Honsuu, '###') AS TeKakou4Honsuu
        ,FORMAT(TeKakou5Honsuu, '###') AS TeKakou5Honsuu
        ,FORMAT(TeKakou6Honsuu, '###') AS TeKakou6Honsuu
        ,FORMAT(TeKakou7Honsuu, '###') AS TeKakou7Honsuu
        ,FORMAT(TeKakou8Honsuu, '###') AS TeKakou8Honsuu
        ,FORMAT(UpdateDateTime, 'yyyy/MM/dd HH:mm:ss.fff') AS HiddenUpdateDateTime

	FROM D_BukkenTeKakou
	WHERE BukkenNO = @BukkenNO

END
