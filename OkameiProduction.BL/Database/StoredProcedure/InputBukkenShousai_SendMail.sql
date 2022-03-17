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

    DECLARE @Char1              varchar(100)  --���A�h1
    DECLARE @Char2              varchar(100)  --���A�h2
    DECLARE @Char3              varchar(100)  --���A�h3
	DECLARE @Char4              varchar(50)   --�t�@�C����ʖ�
    DECLARE @Char5              varchar(50)   --�����ԍ������������t�@�C����ʖ�
    DECLARE @Char6              varchar(100)  --�t�@�C����ʖ��@���X�V����܂����B
	DECLARE @Char7              varchar(200)  --���A�h���̔�
    DECLARE @SendFromAddress    varchar(50)
    DECLARE @SendFromServer     varchar(50)
    DECLARE @MailServerAccount  varchar(50)
    DECLARE @MailServerPass     varchar(50)

    IF @BukkenFileShurui = 1
        SELECT @Char1 = Char1, @Char2 = Char2, @Char3 = Char3, @Char4 = '�w��������H�f�[�^' FROM M_MultiPorpose WHERE ID = 17 AND [Key] = 1

    ELSE IF @BukkenFileShurui = 3
        SELECT @Char1 = Char1, @Char2 = Char2, @Char3 = Char3, @Char4 = '���n�ē��w������n�}' FROM M_MultiPorpose WHERE ID = 18 AND [Key] = 1

    ELSE IF @BukkenFileShurui = 4
        SELECT @Char1 = Char1, @Char2 = Char2, @Char3 = Char3, @Char4 = '�؏o��' FROM M_MultiPorpose WHERE ID = 19 AND [Key] = 1

    ELSE
        RETURN

	select @Char5 = @BukkenNO + @BukkenName + @Char4
	select @Char6 = @Char4 + '���X�V����܂����B'
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