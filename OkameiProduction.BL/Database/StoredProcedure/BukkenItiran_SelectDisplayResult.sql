IF EXISTS (select * from sys.objects where name = 'BukkenItiran_SelectDisplayResult')
BEGIN
    DROP PROCEDURE [BukkenItiran_SelectDisplayResult]
END
GO

CREATE PROCEDURE [dbo].[BukkenItiran_SelectDisplayResult](
    @TantouSitenCD          varchar(10)
    ,@JuchuuOption          tinyint
    ,@BukkenSiteiOption     tinyint
    ,@NoukiStart            date
    ,@NoukiEnd              date
    ,@TantouEigyouCD        varchar(10)
    ,@TantouPcCD            varchar(10)
    ,@TantouCadCD           varchar(10)
    ,@KubunCD               varchar(10)
    ,@BukkenNO              varchar(8)
    ,@BukkenName            varchar(20)
    ,@KoumutenName          varchar(50)
    ,@TokuchuuzaiUmu        tinyint
    ,@UserID                varchar(10)
)AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @SystemDate date = GETDATE()

    SELECT 
        CASE buk.NoukiMiteiKBN WHEN 1 THEN 'ñ¢íË' 
                ELSE FORMAT(buk.Nouki, 'MM/dd') END                     AS Nouki
        ,FORMAT(buk.UnsouKuraireDate, 'MM/dd')                          AS UnsouKuraireDate
        ,buk.BukkenNO                                                   AS BukkenNO
        ,CONVERT(varchar(16), buk.BukkenName)                           AS BukkenName
        ,CONVERT(varchar(30), buk.KoumutenName)                         AS KoumutenName
        ,FORMAT(buk.KakouTubosuu, '##0.00')                             AS KakouTubosuu
        ,CONVERT(varchar(6), siten.Char2)                               AS TantouSitenName
        ,CONVERT(varchar(6), eigyo.Char2)                               AS TantouEigyouName
        ,CONVERT(varchar(6), cad.Char2)                                 AS TantouCadName
        ,FORMAT(buk.JuchuuDate, 'MM/dd')                                AS JuchuuDate
        ,FORMAT(buk.FusezuTeishutuDate, 'MM/dd')                        AS FusezuTeishutuDate 
        ,CASE buk.NoukiMiteiKBN 
				WHEN 1 THEN '' 
				ELSE FORMAT(dbo.fnGetBusinessDay(buk.Nouki, ISNULL(mp999.Num1,0) * -1), 'MM/dd') END  AS ShouninKijitu
        ,FORMAT(buk.KakoutuShouninDate, 'MM/dd')                        AS KakouShouninDate 
        ,CASE WHEN buk.KakousijishoHakkouDate IS NOT NULL THEN 'çœ'
                ELSE FORMAT(dbo.fnGetSijiKijitu(
                            buk.Nouki,
                            buk.UnsouKuraireDate,
                            buk.KanamonoCD,
                            buk.TekakouTime,
                            buk.HundeggerTime), 'MM/dd') END            AS SijiKikitu
        ,FORMAT(buk.KakouNissuu, '##0')                                 AS KakouNissuu
        ,FORMAT(buk.UpdateDateTime, 'MM/dd')                            AS UpdateDateTime
        ,CONVERT(varchar(10), kanam.Char1)                              AS KanamonoName
        ,CONVERT(varchar(6), nyusa.Char1)                               AS NyuuryokusakiName

        ,CASE buk.TokuchuuzaiUmu 
                WHEN 1 THEN 'óL' 
                WHEN 2 THEN 'ñ≥' 
                WHEN 3 THEN 'ñ¢íË' ELSE '' END                           AS TokuchuuzaiUmu

        ,CASE WHEN buk.KakousijishoHakkouDate IS NOT NULL AND buk.KakousijishoHakkouDate < @SystemDate THEN 'â¡çHéwé¶çœÇ›'
                WHEN buk.KakoutuShouninDate   IS NOT NULL AND buk.KakoutuShouninDate     < @SystemDate THEN 'â¡çHè≥îFçœÇ›'
                WHEN buk.FusezuTeishutuDate   IS NOT NULL AND buk.FusezuTeishutuDate     < @SystemDate THEN 'ïöê}íÒèoçœÇ›'
                ELSE '' END                                             AS SinchokuJoukyou

        ,buk.KoumutenName                                               AS SortKoumutenName
        ,buk.Nouki                                                      AS SortNouki

    FROM D_Bukken buk
    LEFT OUTER JOIN M_MultiPorpose siten ON siten.ID = 3 AND siten.[Key] = buk.TantouSitenCD
    LEFT OUTER JOIN M_MultiPorpose eigyo ON eigyo.ID = 4 AND eigyo.[Key] = buk.TantouEigyouCD
    LEFT OUTER JOIN M_MultiPorpose cad   ON cad.ID   = 6 AND cad.[Key]   = buk.TantouCadCD
    LEFT OUTER JOIN M_MultiPorpose kanam ON kanam.ID = 8 AND kanam.[Key] = buk.KanamonoCD
    LEFT OUTER JOIN M_MultiPorpose nyusa ON nyusa.ID = 7 AND nyusa.[Key] = buk.NyuuryokusakiCD
    LEFT OUTER JOIN M_MultiPorpose mp999 ON mp999.ID = 999 AND mp999.[Key] = 1

    WHERE (@TantouSitenCD IS NULL OR buk.TantouSitenCD = @TantouSitenCD)

      AND ((@JuchuuOption = 1 AND buk.CancelDate IS NULL) 
        OR (@JuchuuOption = 2 AND buk.CancelDate IS NOT NULL)
        )

      AND (@BukkenSiteiOption <> 2 
        OR (buk.TantouEigyouCD = @UserID 
        OR buk.TantouPcCD = @UserID
        OR buk.TantouCadCD = @UserID
        ))

      AND (@NoukiStart IS NULL OR buk.Nouki >= @NoukiStart)
      AND (@NoukiEnd IS NULL OR buk.Nouki <= @NoukiEnd)
      AND (@TantouEigyouCD IS NULL OR buk.TantouEigyouCD = @TantouEigyouCD)
      AND (@TantouPcCD IS NULL OR buk.TantouPcCD = @TantouPcCD)
      AND (@TantouCadCD IS NULL OR buk.TantouCadCD = @TantouCadCD)
      AND (@KubunCD IS NULL OR buk.KubunCD = @KubunCD)
      AND (@BukkenNO IS NULL OR buk.BukkenNO = @BukkenNO)
      AND (@BukkenName IS NULL OR buk.BukkenName like CONCAT('%', @BukkenName, '%'))
      AND (@KoumutenName IS NULL OR buk.KoumutenName like CONCAT('%', @KoumutenName, '%'))
      AND (@TokuchuuzaiUmu = 0 OR buk.TokuchuuzaiUmu = @TokuchuuzaiUmu)

      ORDER BY buk.Nouki, eigyo.Num1, buk.TantouEigyouCD, buk.KoumutenName --Ç‹Ç∏ÇÕî[ä˙èáÇ≈éÊìæ
END
