IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_PurecattoExport')    
BEGIN
    DROP PROCEDURE [InputBukkenShousai_PurecattoExport]
END
GO
Create PROCEDURE [dbo].[InputBukkenShousai_PurecattoExport] 
		@TantousitenCD as varchar(10) , 
		@TantoueigyouCD as varchar(10),
		@KoumuTenName as varchar(200), 
		@BukkenName as varchar(200),
		@KakouTubosuu as decimal(5,2),
		@BukkenNo as varchar(10) 
AS
BEGIN 
	SET NOCOUNT ON;
			select  
		Cast(@BukkenName as varchar(16)) as BukkenName,
		cast(@KoumuTenName as varchar(30)) as KoumuTenName,
		cast(( select Char2 from M_MultiPorpose where ID='003' and [Key]=@TantousitenCD) as varchar(6)) as ShopName,
		cast((	select Char2 from M_MultiPorpose where ID='004' and [Key]=@TantoueigyouCD) as varchar(6)) as TentouName,
		cast(@KakouTubosuu as decimal(5,2)) as KakouTubosuu,
		cast(@BukkenNo as varchar(8)) as BukkenNo

END
