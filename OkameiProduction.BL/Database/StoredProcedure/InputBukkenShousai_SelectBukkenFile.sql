IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_SelectBukkenFile')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_SelectBukkenFile]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_SelectBukkenFile](
     @BukkenNO  varchar(8),
     @BukkenFileShurui tinyint
)AS
BEGIN
    SET NOCOUNT ON

    SELECT
        CASE dbf.BukkenFileShurui
            WHEN 1 THEN '指示書･加工'
            WHEN 2 THEN '物件'
            WHEN 3 THEN '現地案内･地図'
            WHEN 4 THEN '木出し'
            WHEN 5 THEN '金物'
            WHEN 6 THEN '最終CSV'
            WHEN 7 THEN 'レビュー'
            ELSE '' END                                     AS BukkenFileShurui
        ,FORMAT(dbf.InsertDatetime, 'yyyy/MM/dd HH:mm:ss')  AS InsertDatetime
        ,dbf.BukkenFileName                                 AS BukkenFileName
        ,mmp.Char1                                          AS InsertOperator
        ,BukkenFileRows                                     AS BukkenFileRows

    FROM D_BukkenFile dbf
    LEFT OUTER JOIN M_MultiPorpose mmp ON mmp.ID = 20 AND mmp.[Key] = dbf.InsertOperator

    WHERE BukkenNO = @BukkenNO
    AND (@BukkenFileShurui = 8 OR BukkenFileShurui = @BukkenFileShurui)

END
