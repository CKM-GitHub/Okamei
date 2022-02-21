IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_KakoukadoShoExport')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_KakoukadoShoExport]
END
GO
Create PROCEDURE InputBukkenShousai_KakoukadoShoExport
	 
	 	@TantousitenCD as varchar(10) , 
		@TantoueigyouCD as varchar(10), 
		@Nouki as date, 
		@KoumuTenName as varchar(200), 
		@BukkenName as varchar(20) 
AS
BEGIN 
	SET NOCOUNT ON;
	 	select 
		cast (@KoumuTenName as varchar(40) ) as KoumutenName,
		cast (@BukkenName as varchar(20)) as BukkenName,
		cast((select char2  from M_Multiporpose where ID= '003' and [Key] = @TantousitenCD) as varchar(6)) as ShopName,
		cast((select char2  from M_Multiporpose where ID= '004' and [Key] = @TantoueigyouCD) as varchar(6)) as SaleMan,
		cast((select char3  from M_Multiporpose where ID= '004' and [Key] = @TantoueigyouCD) as varchar(56)) as MailAddress,
		 @Nouki as FirstDate,
		(Case when @Nouki is null then null else (
		 FORMAT(dbo.fnGetBusinessDay(@Nouki, ISNULL((select Num1  from M_Multiporpose where ID= '999' and [Key] = '1'),0) * -1), 'MM/dd')) end) as SecondDate
END
GO