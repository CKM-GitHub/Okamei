IF EXISTS (select * from sys.objects where name = 'M_Control_Select')
BEGIN
    DROP PROCEDURE [M_Control_Select]
END
GO

CREATE PROCEDURE [dbo].[M_Control_Select]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
         CompanyName
        ,ShortName
        ,ZipCD1
        ,ZipCD2
        ,Address1
        ,Address2
        ,TelephoneNO
        ,FaxNO
        ,SMitumoriCounter
        ,TenpuFilePass
        ,SendFromAddress
        ,SendFromServer
        ,MailServerAccount
        ,MailServerPass
        ,Yobi1
        ,Yobi2
        ,Yobi3
        ,Yobi4
        ,Yobi5
        ,Yobi6
        ,Yobi7
        ,Yobi8
        ,Yobi9
    FROM M_Control
    WHERE MainKey = 1

END
