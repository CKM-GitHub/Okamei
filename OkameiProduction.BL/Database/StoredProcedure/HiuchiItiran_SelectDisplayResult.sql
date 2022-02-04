
IF EXISTS (select * from sys.objects where name = 'HiuchiItiran_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [HiuchiItiran_SelectDisplayResult]
END
GO 
Create PROCEDURE [dbo].[HiuchiItiran_SelectDisplayResult] 
		   @TantouSitenCD          varchar(15) 
			  ,@NoukiStart            varchar(15) 
			  ,@NoukiEnd              varchar(15) 
			  ,@TantouEigyouCD        varchar(15) 
			  ,@TantouCadCD           varchar(15) 
AS
BEGIN
			 create table #ptk(
			 Flg varchar(500),
			 Nouki varchar(500),
			 BukkenNo varchar(500),
			 BukkenName varchar(500),
			 SouName varchar(500),
			 SouDateTime varchar(500),
			 Zairyou varchar(500),
			 Toukyuu varchar(500),
			 Honsuu varchar(500)
			  ) 
			  declare @i as int=1,@j as int=1,
			  @sql as nvarchar(max), @sub as varchar(max)='', @root as varchar(max)='';
			  while (@i < 5)
			  Begin
			 

			  set @j =1;
			   while (@j < 4)
			   Begin	
			   	set @sub = cast((@i) as varchar(1))+cast((@j) as varchar(1));
				set @root =   cast((@i) as varchar(1));
			  set @sql =N'insert into #ptk  select * from  
					(select 
					 '+@sub+' as Flg, 
					(CASE db.NoukiMiteiKBN WHEN 1 THEN ''未定''  ELSE FORMAT(db.Nouki, ''MM/dd'') END ) as Nouki,
					 db.BukkenNo as BukkenNo,
					 db.BukkenName as BukkenName,
					 mp.Char1 as SouName, 
					 dbh.Sou'+@root+'KakouDateTime as SouDateTime,
					 dbh.zairyou'+@sub+' as zairyou,
					 dbh.toukyuu'+@sub+' as toukyuu,
					 dbh.honsuu'+@sub+' as honsuu
					from D_Bukken db 
					left join D_BukkenHiuchi dbh on db.BukkenNo = dbh.BukkenNO
					left join M_Multiporpose mp on mp.ID=''10'' and mp.[Key]= dbh.Sou'+@root+'
					where 
				(@TantouSitenCD IS NULL OR db.TantouSitenCD =   @TantouSitenCD )  
			AND ( cast(@NoukiStart as varchar(15))  IS NULL OR db.Nouki >=   cast(@NoukiStart as varchar(15)) )
			AND ( cast(@NoukiEnd as varchar(15))  IS NULL OR db.Nouki <=  cast(@NoukiEnd as varchar(15)))
			AND ( @TantouEigyouCD  IS NULL OR db.TantouEigyouCD =  @TantouEigyouCD) 
			AND ( @TantouCadCD  IS NULL OR db.TantouCadCD =  @TantouCadCD )
			AND db.HiuchiKakou =1 
			 ) a'; 
			EXEc sp_executesql @sql, N'@TantouSitenCD varchar(15),@NoukiStart varchar(15),@NoukiEnd varchar(15),@TantouEigyouCD varchar(15), @TantouCadCD varchar(15) ',
			 @TantouSitenCD = @TantouSitenCD,@NoukiStart=@NoukiStart, @NoukiEnd=@NoukiEnd, @TantouEigyouCD=@TantouEigyouCD,@TantouCadCD=@TantouCadCD

			set @j= @j +1;
			End
			 set @i = @i+1;

					End
					
			  select * from #ptk where Zairyou is not null order by Nouki asc, BukkenNo asc, flg asc 
					Drop table #ptk
				
END
