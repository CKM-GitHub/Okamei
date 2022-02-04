IF EXISTS (select * from sys.table_types where user_type_id = Type_id(N'T_BukkenMoulder'))
BEGIN
    IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_Create')
        DROP PROCEDURE [InputBukkenShousai_Create]

    IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_Update')
        DROP PROCEDURE [InputBukkenShousai_Update]

    IF EXISTS (select * from sys.objects where name = 'InputBukkenShousai_Delete')
        DROP PROCEDURE [InputBukkenShousai_Delete]

    DROP TYPE [T_BukkenMoulder]
END
GO

CREATE TYPE T_BukkenMoulder AS TABLE
(
     BukkenNO               varchar(8) NOT NULL
    ,BukkenMoulderRows	    int
    ,MoulderHinmoku	        varchar(10)
    ,MoulderZairyou	        varchar(10)
    ,MoulderHaba	        int
    ,MoulderNari	        int
    ,MoulderNagasa	        int
    ,MoulderHonsuu	        int
    ,MoulderKakouDateTime	datetime
    ,MoulderSumi	        tinyint
    ,UpdateDateTime	        datetime
)
GO
