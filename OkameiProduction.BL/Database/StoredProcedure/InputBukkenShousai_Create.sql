IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_Create')
BEGIN
    DROP PROCEDURE [InputBukkenShousai_Create]
END
GO

CREATE PROCEDURE [dbo].[InputBukkenShousai_Create](
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
    ,@Operator                  varchar(10)
	,@BukkenCommentTable		T_BukkenComment READONLY
	,@BukkenMoulderTable		T_BukkenMoulder READONLY
)AS
BEGIN
    DECLARE @SystemDate datetime = GETDATE()

    INSERT INTO D_Bukken
    (
        BukkenNO
        ,TantouSitenCD
        ,BukkenName
        ,Juusho
        ,KoumutenName
        ,KakoutuboSuu
        ,NoukiMiteiKBN
        ,Nouki
        ,UnsouKuraireDate
        ,KubunCD
        ,TantouEigyouCD
        ,TantouPcCD
        ,TantouCadCD
        ,NyuuryokusakiCD
        ,TokuchuuzaiUmu
        ,ZairyouNouki
        ,TokuchuuzaiComment
        ,JuchuuDate
        ,FusezuTeishutuDate
        ,KakoutuShouninDate
        ,KidasiDate
        ,KakousijishoHakkouDate
        ,KannouDate
        ,CancelDate
        ,KakouNissuu
        ,KanamonoCD
        ,OukazaiKakou
        ,OukazaiSumi
        ,KabeKakou
        ,KabeSumi
        ,HasirazaiKakou
        ,HasirazaiSumi
        ,HiuchiKakou
        ,HiuchiSumi
        ,HagarazaiKakou
        ,HagarazaiSumi
        ,HagarazaiSuu
        ,YukaKakou
        ,YukaSumi
        ,YukaGouhanShurui
        ,YukaGouhanSuu
        ,NoziKakou
        ,NoziSumi
        ,NoziGouhanShurui
        ,NoziGouhanSuu
        ,TekakouKakou
        ,TekakouSumi
        ,TekakouTime
        ,HundeggerKakou
        ,HundeggerSumi
        ,HundeggerTime
        ,InsertOperator
        ,InsertDateTime
        ,UpdateOperator
        ,UpdateDateTime
    ) VALUES (
        @BukkenNO
        ,@TantouSitenCD
        ,@BukkenName
        ,@Juusho
        ,@KoumutenName
        ,@KakoutuboSuu
        ,@NoukiMiteiKBN
        ,@Nouki
        ,@UnsouKuraireDate
        ,@KubunCD
        ,@TantouEigyouCD
        ,@TantouPcCD
        ,@TantouCadCD
        ,@NyuuryokusakiCD
        ,@TokuchuuzaiUmu
        ,@ZairyouNouki
        ,@TokuchuuzaiComment
        ,@JuchuuDate
        ,@FusezuTeishutuDate
        ,@KakoutuShouninDate
        ,@KidasiDate
        ,@KakousijishoHakkouDate
        ,@KannouDate
        ,@CancelDate
        ,@KakouNissuu
        ,@KanamonoCD
        ,@OukazaiKakou
        ,@OukazaiSumi
        ,@KabeKakou
        ,@KabeSumi
        ,@HasirazaiKakou
        ,@HasirazaiSumi
        ,@HiuchiKakou
        ,@HiuchiSumi
        ,@HagarazaiKakou
        ,@HagarazaiSumi
        ,@HagarazaiSuu
        ,@YukaKakou
        ,@YukaSumi
        ,@YukaGouhanShurui
        ,@YukaGouhanSuu
        ,@NoziKakou
        ,@NoziSumi
        ,@NoziGouhanShurui
        ,@NoziGouhanSuu
        ,@TekakouKakou
        ,@TekakouSumi
        ,@TekakouTime
        ,@HundeggerKakou
        ,@HundeggerSumi
        ,@HundeggerTime
        ,@Operator
        ,@SystemDate
        ,@Operator
        ,@SystemDate
    )

END
