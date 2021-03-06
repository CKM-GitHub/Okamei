IF EXISTS (select * from sys.objects where name = 'KadouKeikaku_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [KadouKeikaku_SelectDisplayResult]
END
GO
CREATE PROCEDURE [dbo].[KadouKeikaku_SelectDisplayResult]
    -- Add the parameters for the stored procedure here
    @TantouSitenCD      varchar(10),
    @BukkenSiteiOption  tinyint,
    @NoukiStart         date,
    @NoukiEnd           date,
    @TantouEigyouCD     varchar(10),
    @TantouCadCD        varchar(10),
    @UserID             varchar(10),
    @OukazaiSumi        tinyint,
    @HasirazaiSumi      tinyint,
    @HagarazaiSumi      tinyint,
    @YukaSumi           tinyint,
    @NoziSumi           tinyint,
    @KabeSumi           tinyint,
    @HiuchiSumi         tinyint,
    @TekakouSumi        tinyint,
    @HundeggerSumi      tinyint
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    DECLARE @MaxDate date = '2099/01/01'

    SELECT
        Nouki,
        UnsouKuraireDate,
        BukkenNO,
        BukkenName,
        KoumutenName,
        TantouCadName,
        KakouTubosuu,
        KanamonName,
        KidasiDate,
        CASE WHEN KakousijishoHakkouDate IS NOT NULL THEN '済'
             WHEN SortSijiKikitu = @MaxDate THEN NULL
             ELSE FORMAT(SortSijiKikitu, 'MM/dd') END        AS SijiKikitu,
        OukazaiSumi,
        HasirazaiSumi,
        HagarazaiSumi,
        YukaSumi,
        NoziSumi ,
        KabeSumi,
        HiuchiSumi,
        TekakouSumi ,
        HundeggerSumi,
        ZairyouNouki,
        HagarazaiSuu,
        YukaGouhanSuu,
        NoziGouhanSuu,
        TekakouTime,
        HundeggerTime
    FROM
    (    
        SELECT
        CASE    dbk.NoukiMiteiKBN 
                WHEN 1 THEN '未定'
                ELSE FORMAT(dbk.Nouki, 'MM/dd')        END      AS Nouki,
                FORMAT(dbk.UnsouKuraireDate, 'MM/dd')           AS UnsouKuraireDate,
                dbk.BukkenNO,
                CONVERT(varchar(16), dbk.BukkenName)            AS BukkenName,
                CONVERT(varchar(30), dbk.KoumutenName)          AS KoumutenName,
                CONVERT(varchar(6), mp1.Char2)                  AS TantouCadName,
                FORMAT(dbk.KakoutuboSuu, '##0.00')              AS KakouTubosuu,
                CONVERT(varchar(10), mp2.Char1)                 AS KanamonName,
                FORMAT(dbk.KidasiDate, 'MM/dd')                 AS KidasiDate,
         --CASE   WHEN dbk.KakousijishoHakkouDate IS NOT NULL THEN '済'
         --       ELSE FORMAT(dbo.fnGetSijiKijitu(
         --                   dbk.Nouki,
         --                   dbk.UnsouKuraireDate,
         --                   dbk.KanamonoCD,
         --                   dbk.TekakouTime,
         --                   dbk.HundeggerTime), 'MM/dd') END    AS SijiKikitu,
         dbk.KakousijishoHakkouDate,
         ISNULL(dbo.fnGetSijiKijitu(
                                dbk.Nouki,
                                dbk.UnsouKuraireDate,
                                dbk.KanamonoCD,
                                dbk.TekakouTime,
                                dbk.HundeggerTime),@MaxDate)    AS SortSijiKikitu,
         CASE   WHEN dbk.OukazaiSumi = 1 Then '済'
                WHEN dbk.OukazaiKakou = 1 Then '〇'
                WHEN dbk.OukazaiKakou = 2 Then '×'
                ELSE ''    END                                  AS OukazaiSumi,
         CASE    WHEN dbk.HasirazaiSumi =1 Then '済'            
                WHEN dbk.HasirazaiKakou =1 Then '〇'
                WHEN dbk.HasirazaiKakou =2 Then '×'
                ELSE '' END                                     AS HasirazaiSumi,
         CASE   WHEN dbk.HagarazaiSumi =1 Then '済'
                WHEN dbk.HagarazaiKakou =1 Then '〇'
                WHEN dbk.HagarazaiKakou =2 Then '×'
                ELSE '' END                                     AS HagarazaiSumi,
         CASE   WHEN dbk.YukaSumi =1 Then '済'
                WHEN dbk.YukaKakou = 1 Then '〇'
                WHEN dbk.YukaKakou =2 Then '×'
                ELSE '' END                                     AS YukaSumi,
         CASE   WHEN dbk.NoziSumi =1 Then '済'
                WHEN dbk.NoziKakou=1 Then '〇'
                WHEN dbk.NoziKakou =2 Then '×'
                ELSE '' END                                      AS NoziSumi ,
         CASE   WHEN dbk.KabeSumi =1 Then '済'
                WHEN dbk.KabeKakou =1 Then '〇'
                WHEN dbk.KabeKakou =2 Then '×'
                ELSE '' END                                      AS KabeSumi,
         CASE   WHEN dbk.HiuchiSumi=1 Then '済'
                WHEN dbk.HiuchiKakou = 1 Then '〇'
                WHEN dbk.HiuchiKakou =2 Then '×'
                ELSE '' END                                     AS HiuchiSumi,
         CASE   WHEN dbk.TekakouSumi =1 Then '済'
                WHEN dbk.TekakouKakou =1 Then '〇'
                WHEN dbk.TekakouKakou =2 Then '×'
                ELSE '' END                                     AS TekakouSumi ,
         CASE   WHEN dbk.HundeggerSumi = 1 Then '済'
                WHEN dbk.HundeggerKakou = 1 Then '〇'
                WHEN dbk.HundeggerKakou = 2 Then '×'
                ELSE '' END                                     AS HundeggerSumi,
                FORMAT(dbk.ZairyouNouki, 'MM/dd')               AS ZairyouNouki,
                dbk.HagarazaiSuu                                AS HagarazaiSuu,
                dbk.YukaGouhanSuu                               AS YukaGouhanSuu,
                dbk.NoziGouhanSuu                               AS NoziGouhanSuu,
         CASE   dbk.TekakouTime 
                WHEN 0 Then ''
                Else   FORMAT(dbk.TekakouTime, '##0.0')    END  AS TekakouTime,
         CASE   dbk.HundeggerTime 
                WHEN 0 Then ''
                Else FORMAT(dbk.HundeggerTime, '##0.0')    END  AS HundeggerTime

        FROM    D_Bukken dbk
                Left Outer Join M_MultiPorpose mp1 On mp1.ID=6 and mp1.[Key]=dbk.TantouCadCD
                Left Outer Join M_MultiPorpose mp2 On mp2.ID=8 and mp2.[Key]=dbk.KanamonoCD
        WHERE    (@TantouSitenCD IS NULL OR dbk.TantouSitenCD = @TantouSitenCD)
        
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
                OR   (dbk.HasirazaiKakou = 1 AND dbk.HasirazaiSumi <> 1) 
                OR   (dbk.HagarazaiKakou =1 AND dbk.HagarazaiSumi <> 1)
                OR   (dbk.YukaKakou = 1 AND dbk.YukaSumi <> 1 ) 
                OR   (dbk.NoziKakou = 1 AND dbk.NoziSumi <> 1 )
                OR   (dbk.KabeKakou =1 AND dbk.KabeSumi <> 1 )
                OR   (dbk.HiuchiKakou = 1 AND dbk.HiuchiSumi <> 1 ) 
                OR   (dbk.TekakouKakou = 1 AND dbk.TekakouSumi <> 1)
                OR   (dbk.HundeggerKakou = 1 AND dbk.HundeggerSumi <>1)
                )
        --Order By  SijiKikitu ASC
    ) W
    ORDER BY SortSijiKikitu, BukkenNO
END
