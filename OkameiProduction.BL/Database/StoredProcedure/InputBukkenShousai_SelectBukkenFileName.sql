IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_SelectBukkenFileName')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_SelectBukkenFileName]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_SelectBukkenFileName](
     @BukkenNO  varchar(8),
     @BukkenFileRowsCsv varchar(max)
)AS
BEGIN
    SET NOCOUNT ON

    DECLARE @BukkenFileRowsTable TABLE
    (
        BukkenFileRows int
    )
    INSERT INTO @BukkenFileRowsTable SELECT value FROM STRING_SPLIT(@BukkenFileRowsCsv, ',')


    SELECT DISTINCT(dbf.BukkenFileName) AS BukkenFileName

    FROM D_BukkenFile dbf

    WHERE BukkenNO = @BukkenNO
    AND BukkenFileRows IN (SELECT BukkenFileRows FROM @BukkenFileRowsTable)

END
