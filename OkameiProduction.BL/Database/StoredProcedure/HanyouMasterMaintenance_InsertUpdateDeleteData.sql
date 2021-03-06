IF EXISTS (select * from sys.objects where name = 'HanyouMasterMaintenance_InsertUpdateDeleteData')
BEGIN
    DROP PROCEDURE [HanyouMasterMaintenance_InsertUpdateDeleteData]
END
GO 
CREATE PROCEDURE [dbo].[HanyouMasterMaintenance_InsertUpdateDeleteData]
	-- Add the parameters for the stored procedure here
	@ID			INT,
	@Key		VARCHAR(10),
	@ID_name	VARCHAR(50),
	@Char1		VARCHAR(100),
	@Char2		VARCHAR(100),
	@Char3		VARCHAR(100),
	@Char4		VARCHAR(100),
	@Char5		VARCHAR(100),
	@Num1		INT,
	@Num2		INT,
	@Num3		INT,
	@Num4		INT,
	@Num5		INT,
	@Date1		DATETIME,
	@Date2		DATETIME,
	@Date3		DATETIME,
	@Date4		DATETIME,
	@Date5		DATETIME,
	@Mode		VARCHAR(10),
	@UserID		VARCHAR(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @Mode = 'New'
	BEGIN
		INSERT INTO M_MultiPorpose(ID, [Key], IDName, Char1, Char2, Char3, Char4, Char5, Num1, Num2, Num3, Num4, Num5, Date1, Date2, Date3, Date4, Date5,
		InsertOperator, InsertDateTime, UpdateOperator, UpdateDateTime)
		VALUES (@ID, @Key, @ID_name, @Char1, @Char2, @Char3, @Char4, @Char5, @Num1, @Num2, @Num3, @Num4, @Num5, @Date1, @Date2, @Date3, @Date4, @Date5,
		@UserID, GETDATE(), @UserID, GETDATE())
	END

	ELSE IF @Mode = 'Edit'
	BEGIN
		UPDATE M_MultiPorpose
		SET Char1 = @Char1, Char2 = @Char2, Char3 = @Char3, Char4 = @Char4, Char5 = @Char5, Num1 = @Num1, Num2 = @Num2, Num3 = @Num3, Num4 = @Num4, Num5 = @Num5,
		Date1 = @Date1, Date2 = @Date2, Date3 = @Date3, Date4 = @Date4, Date5 = @Date5, UpdateOperator = @UserID, UpdateDateTime = GETDATE()
		WHERE ID = @ID AND [Key] = @Key
	END

	ELSE IF @Mode = 'Delete'
	BEGIN
		DELETE FROM M_MultiPorpose
		WHERE ID = @ID AND [Key] = @Key
	END
END
