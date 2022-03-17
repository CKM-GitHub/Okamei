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

    DECLARE @Char1              varchar(100)  --メアド1
    DECLARE @Char2              varchar(100)  --メアド2
    DECLARE @Char3              varchar(100)  --メアド3
	DECLARE @Char4              varchar(50)   --ファイル種別名
    DECLARE @Char5              varchar(50)   --物件番号＆物件名＆ファイル種別名
    DECLARE @Char6              varchar(100)  --ファイル種別名　が更新されました。
	DECLARE @Char7              varchar(200)  --メアド合体版
    DECLARE @SendFromAddress    varchar(50)
    DECLARE @SendFromServer     varchar(50)
    DECLARE @MailServerAccount  varchar(50)
    DECLARE @MailServerPass     varchar(50)

    IF @BukkenFileShurui = 1
        SELECT @Char1 = Char1, @Char2 = Char2, @Char3 = Char3, @Char4 = '指示書･加工データ' FROM M_MultiPorpose WHERE ID = 17 AND [Key] = 1

    ELSE IF @BukkenFileShurui = 3
        SELECT @Char1 = Char1, @Char2 = Char2, @Char3 = Char3, @Char4 = '現地案内指示書･地図' FROM M_MultiPorpose WHERE ID = 18 AND [Key] = 1

    ELSE IF @BukkenFileShurui = 4
        SELECT @Char1 = Char1, @Char2 = Char2, @Char3 = Char3, @Char4 = '木出し' FROM M_MultiPorpose WHERE ID = 19 AND [Key] = 1

    ELSE
        RETURN

	select @Char5 = @BukkenNO + @BukkenName + @Char4
	select @Char6 = @Char4 + 'が更新されました。'
    select @Char7 = case when @Char1 is not NULL and @Char2 is not NULL and @Char3 is not NULL then @Char1 + ';' + @Char2 + ';' + @Char3
						 when @Char1 is not NULL and @Char2 is not NULL and @Char3 is     NULL then @Char1 + ';' + @Char2
						 when @Char1 is not NULL and @Char2 is     NULL and @Char3 is not NULL then @Char1                + ';' + @Char3
						 when @Char1 is     NULL and @Char2 is not NULL and @Char3 is not NULL then                @Char2 + ';' + @Char3
						 when @Char1 is not NULL and @Char2 is     NULL and @Char3 is     NULL then @Char1
						 when @Char1 is     NULL and @Char2 is not NULL and @Char3 is     NULL then                @Char2
						 when @Char1 is     NULL and @Char2 is     NULL and @Char3 is not NULL then                               @Char3
					end




 EXEC msdb.dbo.sp_send_dbmail
  @profile_name = 'TenpuMail'
 ,@recipients = @Char7
 ,@subject = @Char5
 ,@body = @Char6






END
GO