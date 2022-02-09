IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_SelectBukkenComment')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_SelectBukkenComment]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_SelectBukkenComment](
     @BukkenNO  varchar(8)
)AS
BEGIN
    SELECT
         FORMAT(dbc.InsertDatetime, 'yyyy/MM/dd HH:mm:ss')  AS InsertDatetime
        ,dbc.BukkenComment                                  AS BukkenComment
        ,mmp.Char1                                          AS InsertOperator
        ,dbc.BukkenCommentRows                              AS BukkenCommentRows

    FROM D_BukkenComment dbc
    LEFT OUTER JOIN M_MultiPorpose mmp ON mmp.ID = 20 AND mmp.[Key] = dbc.InsertOperator

    WHERE BukkenNO = @BukkenNO

END
