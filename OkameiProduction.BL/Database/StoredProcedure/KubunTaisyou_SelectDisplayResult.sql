IF EXISTS (select * from sys.objects where name = 'KubunTaisyou_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [KubunTaisyou_SelectDisplayResult]
END
GO 
Create PROCEDURE [dbo].[KubunTaisyou_SelectDisplayResult] 
			@TantouSitenCD as varchar(10),
			@KubunCD as varchar(10),
			@NoukiStart as date,
			@NoukiEnd as date,
			@UserID as varchar(10) 
AS
BEGIN 
	SET NOCOUNT ON; 
	select 
			mp1.Char1 as KubunName,
			(CASE when db.Nouki  is null THEN null
            ELSE FORMAT(db.Nouki, 'MM/dd') END) as Nouki, 
			db.BukkenNo as BukkenNo,
			db.BukkenName,
			db.KoumutenName,
			FORMAT(db.KakoutuboSuu, '##0.00') as KakoutuboSuu ,
			mp2.Char1 as ShitenName,
			mp3.Char2 as eigyouName,
			mp4.Char2 as cadName 
			
			from D_Bukken db 
			left join M_Multiporpose mp1 on mp1.ID='002' and mp1.[Key]=db.KubunCD
			left join M_Multiporpose mp2 on mp2.ID='003' and mp2.[Key]=db.TantouSitenCD
			left join M_Multiporpose mp3 on mp3.ID='004' and mp3.[Key]=db.TantouEigyouCD
			left join M_Multiporpose mp4 on mp4.ID='006' and mp4.[Key]=db.TantouCadCD
			where  
			(@KubunCD is null or db.KubunCD = @KubunCD) 
			AND (@NoukiStart IS NULL OR db.Nouki >= @NoukiStart)
			AND (@NoukiEnd IS NULL OR db.Nouki <= @NoukiEnd)
			AND (@TantouSitenCD IS NULL OR db.TantouSitenCD = @TantouSitenCD)
			--AND  (db.TantouEigyouCD = @UserID 
   --     OR db.TantouPcCD = @UserID
   --     OR db.TantouCadCD = @UserID )
			order by 
			mp1.Num1 asc,
			mp2.Num1 asc,
			db.Nouki asc,
			db.BukkenNo asc
END
