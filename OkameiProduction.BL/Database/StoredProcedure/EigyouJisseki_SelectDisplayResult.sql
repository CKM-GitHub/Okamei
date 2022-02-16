IF EXISTS (select * from sys.objects where name = 'EigyouJisseki_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [EigyouJisseki_SelectDisplayResult] 
END
GO 
Create PROCEDURE[dbo].[EigyouJisseki_SelectDisplayResult]
@DateMonth as Date
AS
BEGIN

					declare @i as int=0, @date as date = @DateMonth,@startDate as date, @endDate as Date;
					set @startDate= convert(varchar(7), (DATEADD(MONTH,-1,@date)), 126) +'-21';
					set @endDate= convert(varchar(7), @date, 126) +'-20'; 
						
						select 
						--distinct
							(cast (mp1.Char1 as varchar(10))) Shop,
							(cast (mp2.Char1 as varchar(20)))  Dutier ,
							(mp1.Num1)  as mp1Num1,
							(mp2.Num1) as mp2Num1,
							Nouki,
							 cast (KakoutuboSuu as decimal(6,2)) as KakoutuboSuu,
							TantouSitenCD ,
							TantouEigyouCD
								
							from D_Bukken db
							left join M_Multiporpose mp1 on db.TantouSitenCD = mp1.[Key] and mp1.ID='003'
							left join M_Multiporpose mp2 on db.TantouEigyouCD= mp2.[Key] and mp2.ID='004' 
							where Nouki >= @startDate and Nouki <= @endDate
							order by mp1.Num1 asc, mp2.num1 asc--,TantouSitenCD asc, TantouEigyouCD asc
							--order by  Num1 asc, num2 asc
END
