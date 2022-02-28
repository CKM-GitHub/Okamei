IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_SelectBukkenMoulder')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_SelectBukkenMoulder]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_SelectBukkenMoulder](
     @BukkenNO  varchar(8)
)AS
BEGIN
    SET NOCOUNT ON

    SELECT 
        BukkenNO
        ,CAST(BukkenMoulderRows AS varchar) AS BukkenMoulderRows
        ,MoulderHinmoku
        ,MoulderZairyou
        ,FORMAT(MoulderHaba, '###') AS MoulderHaba
        ,FORMAT(MoulderNari, '###') AS MoulderNari
        ,FORMAT(MoulderNagasa, '###') AS MoulderNagasa
        ,FORMAT(MoulderHonsuu, '###') AS MoulderHonsuu
        ,FORMAT(MoulderKakouDateTime, 'yyyy/MM/dd') AS MoulderKakouDateTime
        ,MoulderSumi
        ,FORMAT(UpdateDateTime, 'yyyy/MM/dd HH:mm:ss.fff') AS HiddenUpdateDateTime

	FROM D_BukkenMoulder
	WHERE BukkenNO = @BukkenNO

END
