IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_Update')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_Update]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_Update](
     @BukkenNO                  varchar(8)
    ,@TantouSitenCD             varchar(10)
    ,@BukkenName                varchar(20)
    ,@Juusho                    varchar(50)
    ,@KoumutenName              varchar(50)
    ,@KakoutuboSuu              decimal(5,2)    
    ,@NoukiMiteiKBN             tinyint   
    ,@Nouki                     date
    ,@UnsouKuraireDate          date   
    ,@KubunCD                   varchar(10)
    ,@TantouEigyouCD            varchar(10)
    ,@TantouPcCD                varchar(10)
    ,@TantouCadCD               varchar(10)
    ,@NyuuryokusakiCD           varchar(10)
    ,@TokuchuuzaiUmu            tinyint 
    ,@ZairyouNouki              date  
    ,@TokuchuuzaiComment        varchar(300)
    ,@JuchuuDate                date
    ,@FusezuTeishutuDate        date
    ,@KakoutuShouninDate        date
    ,@KidasiDate                date
    ,@KakousijishoHakkouDate    date
    ,@KannouDate                date
    ,@CancelDate                date
    ,@KakouNissuu               int
    ,@KanamonoCD                varchar(10)
    ,@OukazaiKakou              tinyint
    ,@OukazaiSumi               tinyint
    ,@KabeKakou                 tinyint
    ,@KabeSumi                  tinyint
    ,@HasirazaiKakou            tinyint
    ,@HasirazaiSumi             tinyint
    ,@HiuchiKakou               tinyint
    ,@HiuchiSumi                tinyint
    ,@HagarazaiKakou            tinyint
    ,@HagarazaiSumi             tinyint
    ,@HagarazaiSuu              int
    ,@YukaKakou                 tinyint
    ,@YukaSumi                  tinyint
    ,@YukaGouhanShurui          varchar(10)
    ,@YukaGouhanSuu             int
    ,@NoziKakou                 tinyint
    ,@NoziSumi                  tinyint
    ,@NoziGouhanShurui          varchar(10)
    ,@NoziGouhanSuu             int
    ,@TekakouKakou              tinyint
    ,@TekakouSumi               tinyint
    ,@TekakouTime               decimal(5,1)    
    ,@HundeggerKakou            tinyint
    ,@HundeggerSumi             tinyint
    ,@HundeggerTime             decimal(5,1)    
    ,@BukkenComment             varchar(100)
    ,@Operator                  varchar(10)
    ,@UpdateDatetime            datetime
    ,@BukkenMoulderTBL          T_BukkenMoulder READONLY
)AS
BEGIN
    DECLARE @Error tinyint = 1
    DECLARE @SystemDate datetime = GETDATE()

    UPDATE D_Bukken SET
    TantouSitenCD           = @TantouSitenCD
    ,BukkenName             = @BukkenName
    ,Juusho                 = @Juusho
    ,KoumutenName           = @KoumutenName
    ,KakoutuboSuu           = @KakoutuboSuu
    ,NoukiMiteiKBN          = @NoukiMiteiKBN
    ,Nouki                  = @Nouki
    ,UnsouKuraireDate       = @UnsouKuraireDate
    ,KubunCD                = @KubunCD
    ,TantouEigyouCD         = @TantouEigyouCD
    ,TantouPcCD             = @TantouPcCD
    ,TantouCadCD            = @TantouCadCD
    ,NyuuryokusakiCD        = @NyuuryokusakiCD
    ,TokuchuuzaiUmu         = @TokuchuuzaiUmu
    ,ZairyouNouki           = @ZairyouNouki
    ,TokuchuuzaiComment     = @TokuchuuzaiComment
    ,JuchuuDate             = @JuchuuDate
    ,FusezuTeishutuDate     = @FusezuTeishutuDate
    ,KakoutuShouninDate     = @KakoutuShouninDate
    ,KidasiDate             = @KidasiDate
    ,KakousijishoHakkouDate = @KakousijishoHakkouDate
    ,KannouDate             = @KannouDate
    ,CancelDate             = @CancelDate
    ,KakouNissuu            = @KakouNissuu
    ,KanamonoCD             = @KanamonoCD
    ,OukazaiKakou           = @OukazaiKakou
    ,OukazaiSumi            = @OukazaiSumi
    ,KabeKakou              = @KabeKakou
    ,KabeSumi               = @KabeSumi
    ,HasirazaiKakou         = @HasirazaiKakou
    ,HasirazaiSumi          = @HasirazaiSumi
    ,HiuchiKakou            = @HiuchiKakou
    ,HiuchiSumi             = @HiuchiSumi
    ,HagarazaiKakou         = @HagarazaiKakou
    ,HagarazaiSumi          = @HagarazaiSumi
    ,HagarazaiSuu           = @HagarazaiSuu
    ,YukaKakou              = @YukaKakou
    ,YukaSumi               = @YukaSumi
    ,YukaGouhanShurui       = @YukaGouhanShurui
    ,YukaGouhanSuu          = @YukaGouhanSuu
    ,NoziKakou              = @NoziKakou
    ,NoziSumi               = @NoziSumi
    ,NoziGouhanShurui       = @NoziGouhanShurui
    ,NoziGouhanSuu          = @NoziGouhanSuu
    ,TekakouKakou           = @TekakouKakou
    ,TekakouSumi            = @TekakouSumi
    ,TekakouTime            = @TekakouTime
    ,HundeggerKakou         = @HundeggerKakou
    ,HundeggerSumi          = @HundeggerSumi
    ,HundeggerTime          = @HundeggerTime
    ,UpdateOperator         = @Operator
    ,UpdateDateTime         = @SystemDate

    WHERE BukkenNO = @BukkenNO
    AND   UpdateDateTime = @UpdateDatetime

    IF @@ROWCOUNT = 0 RETURN @Error

    INSERT INTO D_BukkenComment (
        BukkenNO
        ,BukkenCommentRows
        ,BukkenComment
        ,InsertOperator
        ,InsertDateTime
        ,UpdateOperator
        ,UpdateDateTime
    ) VALUES (
        @BukkenNO
        ,(SELECT MAX(BukkenCommentRows) + 1 FROM D_BukkenComment WHERE BukkenNO = @BukkenNO)
        ,@BukkenComment
        ,@Operator
        ,@SystemDate
        ,@Operator
        ,@SystemDate
    )

END
