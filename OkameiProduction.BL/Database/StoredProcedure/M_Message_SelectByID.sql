IF EXISTS (select * from sys.objects where name = 'M_Message_SelectByID')
BEGIN
    DROP PROCEDURE [M_Message_SelectByID]
END
GO

CREATE PROCEDURE [dbo].[M_Message_SelectByID](
    @MessageID varchar(4)
)AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
     MessageID
    ,MessageText1
    ,MessageText2
    ,MessageText3
    ,MessageText4
    ,MessageButton
    ,MessageMark
    ,ButtonValues
    ,InsertOperator
    ,CONVERT(varchar, InsertDateTime, 121) AS InsertDateTime
    ,UpdateOperator
    ,CONVERT(varchar, UpdateDateTime, 121) AS UpdateDateTime
    FROM M_Message
    WHERE MessageID = @MessageID
END