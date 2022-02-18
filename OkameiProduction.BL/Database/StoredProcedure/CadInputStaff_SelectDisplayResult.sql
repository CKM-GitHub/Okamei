IF EXISTS (select * from sys.objects where name = 'CadInputStaff_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [CadInputStaff_SelectDisplayResult] 
END
GO 
Create PROCEDURE [dbo].[CadInputStaff_SelectDisplayResult]
	-- Add the parameters for the stored procedure here
 	@TantouCadCD as varchar(300) 
AS
BEGIN 
	SET NOCOUNT ON; 
							
							if (@TantouCadCD is null)
							Begin
						select * from 
						(select 
						db.BukkenNo,
						mp.Num1,
						 (CASE WHEN db.KakousijishoHakkouDate IS NOT NULL THEN '済'
						  ELSE FORMAT(dbo.fnGetSijiKijitu(
                          db.Nouki,
                          db.UnsouKuraireDate,
                          db.KanamonoCD,
                          db.TekakouTime,
                          db.HundeggerTime), 'MM/dd') END  )
						   AS SijiKikitu,
						   mp.Char2 as TantouName,
						   ('['+(Case   when db.KakoutuShouninDate is not null then '指'  when db.FusezuTeiShutuDate is not null then '待'  else '未' end) + ']' +
						   '['+Cast (Floor(db.KakoutuboSuu) as varchar(6) )+']' + Cast (db.KoumutenName as varchar (14)) + '/' + Cast(db.BukkenName as varchar(16)) ) as BukkenName
						    
						   from D_Bukken db 
						   left join M_Multiporpose mp on mp.ID= '006' and mp.[Key] = db.TantouCAdCD 

							where 
							-- db.TantouCadCD   in  (select Item from SplitString(@TantouCadCD,','))    and
						  ---Follow Query is about tO Neglect Null in Sama Filed Char2 After Testing , Recomment
							--UnsouKuraireDate is not null and mp.Char2 is not null and
							KakousijishoHakkouDate is null and mp.Char2 is not null
							 
							 ) Bken 
							Order by SijiKikitu asc, Num1 asc, BukkenNo asc
							End
							Else 
							BEgin
							select * from 
						(select 
						db.BukkenNo,
						mp.Num1,
						 (CASE WHEN db.KakousijishoHakkouDate IS NOT NULL THEN '済'
						  ELSE FORMAT(dbo.fnGetSijiKijitu(
                          db.Nouki,
                          db.UnsouKuraireDate,
                          db.KanamonoCD,
                          db.TekakouTime,
                          db.HundeggerTime), 'MM/dd') END  )
						   AS SijiKikitu,
						   mp.Char2 as TantouName,
						   ('['+(Case   when db.KakoutuShouninDate is not null then '指'  when db.FusezuTeiShutuDate is not null then '待'  else '未' end) + ']' +
						   '['+Cast (Floor(db.KakoutuboSuu) as varchar(6) )+']' + Cast (db.KoumutenName as varchar (14)) + '/' +Cast(db.BukkenName as varchar(16))) as BukkenName
						    
						   from D_Bukken db 
						   left join M_Multiporpose mp on mp.ID= '006' and mp.[Key] = db.TantouCAdCD 

							where 
							 db.TantouCadCD   in  (select Item from SplitString(@TantouCadCD,','))    and
						  ---Follow Query is about tO Neglect Null in Sama Filed Char2 After Testing , Recomment
						--	UnsouKuraireDate is not null and mp.Char2 is not null and
							KakousijishoHakkouDate is null   and mp.Char2 is not null
							 
							 ) Bken 
							Order by SijiKikitu asc, Num1 asc, BukkenNo asc
							End
	 
END