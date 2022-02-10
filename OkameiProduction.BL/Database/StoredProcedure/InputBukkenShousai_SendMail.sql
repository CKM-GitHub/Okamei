IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_SendMail')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_SendMail]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_SendMail](
     @BukkenNO                  varchar(8)
    ,@BukkenName                varchar(20)
    ,@BukkenFileShurui          tinyint
)AS
BEGIN
    SET NOCOUNT ON

    DECLARE @Char1              varchar(100)
    DECLARE @Char2              varchar(100)
    DECLARE @Char3              varchar(100)
    DECLARE @SendFromAddress    varchar(50)
    DECLARE @SendFromServer     varchar(50)
    DECLARE @MailServerAccount  varchar(50)
    DECLARE @MailServerPass     varchar(50)

    IF @BukkenFileShurui = 1
        SELECT @Char1 = Char1, @Char2 = Char2, @Char3 = Char3 FROM M_MultiPorpose WHERE ID = 17 AND [Key] = 1

    ELSE IF @BukkenFileShurui = 3
        SELECT @Char1 = Char1, @Char2 = Char2, @Char3 = Char3 FROM M_MultiPorpose WHERE ID = 18 AND [Key] = 1

    ELSE IF @BukkenFileShurui = 4
        SELECT @Char1 = Char1, @Char2 = Char2, @Char3 = Char3 FROM M_MultiPorpose WHERE ID = 19 AND [Key] = 1

    ELSE
        RETURN


    SELECT 
        @SendFromAddress    = SendFromAddress,
        @SendFromServer     = SendFromServer,
        @MailServerAccount  = MailServerAccount,
        @MailServerPass     = MailServerPass
    FROM M_Control
    WHERE MainKey = 1

END
