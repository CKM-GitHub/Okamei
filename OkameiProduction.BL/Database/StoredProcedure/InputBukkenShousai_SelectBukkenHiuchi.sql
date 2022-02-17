IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_SelectBukkenHiuchi')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_SelectBukkenHiuchi]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_SelectBukkenHiuchi](
     @BukkenNO  varchar(8)
)AS
BEGIN
    SET NOCOUNT ON

    SELECT 
        BukkenNO
        ,Sou1
        ,Zairyou11
        ,Toukyuu11
        ,FORMAT(Honsuu11, '###') AS Honsuu11
        ,Zairyou12
        ,Toukyuu12
        ,FORMAT(Honsuu12, '###') AS Honsuu12
        ,Zairyou13
        ,Toukyuu13
        ,FORMAT(Honsuu13, '###') AS Honsuu13
        ,Sou1Sumi
        ,FORMAT(Sou1KakouDateTime, 'yyyy/MM/dd HH:mm:ss') AS Sou1KakouDateTime
        ,Sou2
        ,Zairyou21
        ,Toukyuu21
        ,FORMAT(Honsuu21, '###') AS Honsuu21
        ,Zairyou22
        ,Toukyuu22
        ,FORMAT(Honsuu22, '###') AS Honsuu22
        ,Zairyou23
        ,Toukyuu23
        ,FORMAT(Honsuu23, '###') AS Honsuu23
        ,Sou2Sumi
        ,FORMAT(Sou2KakouDateTime, 'yyyy/MM/dd HH:mm:ss') AS Sou2KakouDateTime
        ,Sou3
        ,Zairyou31
        ,Toukyuu31
        ,FORMAT(Honsuu31, '###') AS Honsuu31
        ,Zairyou32
        ,Toukyuu32
        ,FORMAT(Honsuu32, '###') AS Honsuu32
        ,Zairyou33
        ,Toukyuu33
        ,FORMAT(Honsuu33, '###') AS Honsuu33
        ,Sou3Sumi
        ,FORMAT(Sou3KakouDateTime, 'yyyy/MM/dd HH:mm:ss') AS Sou3KakouDateTime
        ,Sou4
        ,Zairyou41
        ,Toukyuu41
        ,FORMAT(Honsuu41, '###') AS Honsuu41
        ,Zairyou42
        ,Toukyuu42
        ,FORMAT(Honsuu42, '###') AS Honsuu42
        ,Zairyou43
        ,Toukyuu43
        ,FORMAT(Honsuu43, '###') AS Honsuu43
        ,Sou4Sumi
        ,FORMAT(Sou4KakouDateTime, 'yyyy/MM/dd HH:mm:ss') AS Sou4KakouDateTime
        ,FORMAT(UpdateDateTime, 'yyyy/MM/dd HH:mm:ss.fff') AS HiddenUpdateDateTime

	FROM D_BukkenHiuchi
	WHERE BukkenNO = @BukkenNO

END
