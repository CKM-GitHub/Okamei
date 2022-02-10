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
            WHEN 1 THEN '�w��������H'
            WHEN 2 THEN '����'
            WHEN 3 THEN '���n�ē॒n�}'
            WHEN 4 THEN '�؏o��'
            WHEN 5 THEN '����'
            WHEN 6 THEN '�ŏICSV'
            WHEN 7 THEN '���r���['
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
