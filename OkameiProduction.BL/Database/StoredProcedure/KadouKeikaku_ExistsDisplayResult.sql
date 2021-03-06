IF EXISTS (select * from sys.objects where name = 'KadouKeikaku_ExistsDisplayResult')
BEGIN
    DROP PROCEDURE [KadouKeikaku_ExistsDisplayResult]
END
GO
CREATE PROCEDURE [dbo].[KadouKeikaku_ExistsDisplayResult](
	-- Add the parameters for the stored procedure here
	@TantouSitenCD          varchar(10),
    @BukkenSiteiOption     tinyint,
    @NoukiStart            date,
    @NoukiEnd              date,
    @TantouEigyouCD        varchar(10),
    @TantouCadCD           varchar(10),
    @UserID                varchar(10),
	@OukazaiSumi          tinyint,
	@HasirazaiSumi		  tinyint,
	@HagarazaiSumi		  tinyint,
	@YukaSumi			  tinyint,
	@NoziSumi			  tinyint,
	@KabeSumi			  tinyint,
	@HiuchiSumi			  tinyint,
	@TekakouSumi		  tinyint,
	@HundeggerSumi		  tinyint
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT TOP 1 BukkenNO

    FROM D_Bukken dbk
	  WHERE	(@TantouSitenCD IS NULL OR dbk.TantouSitenCD = @TantouSitenCD)
		
				AND (@BukkenSiteiOption <> 2 
		        OR (dbk.TantouEigyouCD = @UserID 
		        OR dbk.TantouPcCD = @UserID
		        OR dbk.TantouCadCD = @UserID
		        ))

				AND (@NoukiStart IS NULL OR dbk.Nouki >= @NoukiStart)
				AND (@NoukiEnd IS NULL OR dbk.Nouki <= @NoukiEnd)
				AND (@TantouEigyouCD IS NULL OR dbk.TantouEigyouCD = @TantouEigyouCD)
				AND (@TantouCadCD IS NULL OR dbk.TantouCadCD = @TantouCadCD)
				AND (@OukazaiSumi =0 OR (@OukazaiSumi = 1 AND dbk.OukazaiSumi <>1) )
				AND (@HasirazaiSumi=0 OR (@HasirazaiSumi =1 AND dbk.HasirazaiSumi <> 1) )
				AND (@HagarazaiSumi =0 OR (@HagarazaiSumi = 1 AND dbk.HagarazaiSumi <> 1))
				AND (@YukaSumi =0 OR (@YukaSumi = 1 AND dbk.YukaSumi <> 1))
				AND (@NoziSumi =0 OR (@NoziSumi =1 AND dbk.NoziSumi <> 1))
				AND (@KabeSumi=0 OR (@KabeSumi = 1 AND dbk.KabeSumi <> 1))
				AND (@HiuchiSumi =0 OR (@HiuchiSumi = 1 AND dbk.HiuchiSumi <> 1)) 
				AND (@TekakouSumi =0 OR (@TekakouSumi = 1 AND dbk.TekakouSumi <> 1)) 
				AND (@HundeggerSumi =0 OR (@HundeggerSumi = 1 AND dbk.HundeggerSumi <> 1))
				AND ((dbk.Oukazaikakou =1 AND dbk.OukazaiSumi <> 1)
				OR	(dbk.HasirazaiKakou = 1 AND dbk.HasirazaiSumi <> 1) 
				OR	(dbk.HagarazaiKakou =1 AND dbk.HagarazaiSumi <> 1)
				OR	(dbk.YukaKakou = 1 AND dbk.YukaSumi <> 1 ) 
				OR	(dbk.NoziKakou = 1 AND dbk.NoziSumi <> 1 )
				OR	(dbk.KabeKakou =1 AND dbk.KabeSumi <> 1 )
				OR	(dbk.HiuchiKakou = 1 AND dbk.HiuchiSumi <> 1 ) 
				OR	(dbk.TekakouKakou = 1 AND dbk.TekakouSumi <> 1)
				OR	(dbk.HundeggerKakou = 1 AND dbk.HundeggerSumi <>1)
				)
END
