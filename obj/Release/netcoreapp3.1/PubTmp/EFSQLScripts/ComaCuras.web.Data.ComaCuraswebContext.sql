IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [FullName] nvarchar(max) NULL,
        [DOB] datetime2 NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [Pictures] (
        [Id] int NOT NULL IDENTITY,
        [Data] varbinary(max) NULL,
        CONSTRAINT [PK_Pictures] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [Shop] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [E_mail] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [ImgURL] nvarchar(max) NULL,
        [HolidayEndDate] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        [SubscriptionEndDate] datetime2 NOT NULL,
        [IsSubscribed] bit NOT NULL,
        CONSTRAINT [PK_Shop] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [Agent] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [SevicesList] nvarchar(max) NULL,
        [HolidayEndDate] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        [PictureId] int NOT NULL,
        [ShopId] int NOT NULL,
        CONSTRAINT [PK_Agent] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Agent_Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [Pictures] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Agent_Shop_ShopId] FOREIGN KEY ([ShopId]) REFERENCES [Shop] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [Image] (
        [Id] int NOT NULL IDENTITY,
        [Data] varbinary(max) NULL,
        [ShopId] int NOT NULL,
        CONSTRAINT [PK_Image] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Image_Shop_ShopId] FOREIGN KEY ([ShopId]) REFERENCES [Shop] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [Rate] (
        [Id] int NOT NULL IDENTITY,
        [ShopId] int NOT NULL,
        CONSTRAINT [PK_Rate] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Rate_Shop_ShopId] FOREIGN KEY ([ShopId]) REFERENCES [Shop] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [Schedule] (
        [Id] int NOT NULL IDENTITY,
        [Day] int NOT NULL,
        [Start] nvarchar(max) NULL,
        [End] nvarchar(max) NULL,
        [Pstart] nvarchar(max) NULL,
        [Pend] nvarchar(max) NULL,
        [ShopId] int NOT NULL,
        CONSTRAINT [PK_Schedule] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Schedule_Shop_ShopId] FOREIGN KEY ([ShopId]) REFERENCES [Shop] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [Service] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Cost] int NOT NULL,
        [Duration] int NOT NULL,
        [DiscountRatio] int NOT NULL,
        [HasDiscount] bit NOT NULL,
        [ShopId] int NOT NULL,
        CONSTRAINT [PK_Service] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Service_Shop_ShopId] FOREIGN KEY ([ShopId]) REFERENCES [Shop] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE TABLE [Appointment] (
        [Id] int NOT NULL IDENTITY,
        [AgentNumber] int NOT NULL,
        [Start] nvarchar(max) NULL,
        [End] nvarchar(max) NULL,
        [Date] datetime2 NOT NULL,
        [ServiceId] int NOT NULL,
        CONSTRAINT [PK_Appointment] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Appointment_Service_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Service] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE UNIQUE INDEX [IX_Agent_PictureId] ON [Agent] ([PictureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE INDEX [IX_Agent_ShopId] ON [Agent] ([ShopId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE INDEX [IX_Appointment_ServiceId] ON [Appointment] ([ServiceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE INDEX [IX_Image_ShopId] ON [Image] ([ShopId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE INDEX [IX_Rate_ShopId] ON [Rate] ([ShopId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE INDEX [IX_Schedule_ShopId] ON [Schedule] ([ShopId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    CREATE INDEX [IX_Service_ShopId] ON [Service] ([ShopId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014151534_init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201014151534_init', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015125042_changed_images_handler')
BEGIN
    ALTER TABLE [Agent] DROP CONSTRAINT [FK_Agent_Pictures_PictureId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015125042_changed_images_handler')
BEGIN
    DROP TABLE [Image];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015125042_changed_images_handler')
BEGIN
    DROP TABLE [Pictures];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015125042_changed_images_handler')
BEGIN
    DROP INDEX [IX_Agent_PictureId] ON [Agent];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015125042_changed_images_handler')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'ImgURL');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Shop] DROP COLUMN [ImgURL];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015125042_changed_images_handler')
BEGIN
    ALTER TABLE [Shop] ADD [GalleryId] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015125042_changed_images_handler')
BEGIN
    ALTER TABLE [Shop] ADD [PictureId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015125042_changed_images_handler')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201015125042_changed_images_handler', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201017092110_created_picture_table')
BEGIN
    CREATE TABLE [Picture] (
        [Id] int NOT NULL IDENTITY,
        [Data] varbinary(max) NULL,
        CONSTRAINT [PK_Picture] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201017092110_created_picture_table')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201017092110_created_picture_table', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201021143830_added_city_to_shops')
BEGIN
    ALTER TABLE [Shop] ADD [City] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201021143830_added_city_to_shops')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201021143830_added_city_to_shops', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201021185451_shop_services_agents_verifications')
BEGIN
    ALTER TABLE [Shop] ADD [AgentsCheck] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201021185451_shop_services_agents_verifications')
BEGIN
    ALTER TABLE [Shop] ADD [ServicesCheck] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201021185451_shop_services_agents_verifications')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201021185451_shop_services_agents_verifications', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201022091437_added_adress_prop_to_shop')
BEGIN
    ALTER TABLE [Shop] ADD [Adress] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201022091437_added_adress_prop_to_shop')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201022091437_added_adress_prop_to_shop', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201022214207_changed_some_attr_in_shop')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'AgentsCheck');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Shop] DROP COLUMN [AgentsCheck];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201022214207_changed_some_attr_in_shop')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'ServicesCheck');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Shop] DROP COLUMN [ServicesCheck];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201022214207_changed_some_attr_in_shop')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201022214207_changed_some_attr_in_shop', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201023130942_included_user_mail_in_appointment')
BEGIN
    ALTER TABLE [Appointment] ADD [UserMail] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201023130942_included_user_mail_in_appointment')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201023130942_included_user_mail_in_appointment', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201023214757_add_shop_id_to_appointments')
BEGIN
    ALTER TABLE [Appointment] ADD [ShopId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201023214757_add_shop_id_to_appointments')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201023214757_add_shop_id_to_appointments', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201027110811_add_city_db')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'City');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Shop] DROP COLUMN [City];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201027110811_add_city_db')
BEGIN
    ALTER TABLE [Shop] ADD [CityId] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201027110811_add_city_db')
BEGIN
    ALTER TABLE [Shop] ADD [CityId1] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201027110811_add_city_db')
BEGIN
    CREATE TABLE [Cities] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Cities] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201027110811_add_city_db')
BEGIN
    CREATE INDEX [IX_Shop_CityId1] ON [Shop] ([CityId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201027110811_add_city_db')
BEGIN
    ALTER TABLE [Shop] ADD CONSTRAINT [FK_Shop_Cities_CityId1] FOREIGN KEY ([CityId1]) REFERENCES [Cities] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201027110811_add_city_db')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201027110811_add_city_db', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201028094215_fixed_cityid_prop_in_shop')
BEGIN
    ALTER TABLE [Shop] DROP CONSTRAINT [FK_Shop_Cities_CityId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201028094215_fixed_cityid_prop_in_shop')
BEGIN
    DROP INDEX [IX_Shop_CityId1] ON [Shop];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201028094215_fixed_cityid_prop_in_shop')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'CityId1');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Shop] DROP COLUMN [CityId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201028094215_fixed_cityid_prop_in_shop')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'CityId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Shop] ALTER COLUMN [CityId] int NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201028094215_fixed_cityid_prop_in_shop')
BEGIN
    CREATE INDEX [IX_Shop_CityId] ON [Shop] ([CityId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201028094215_fixed_cityid_prop_in_shop')
BEGIN
    ALTER TABLE [Shop] ADD CONSTRAINT [FK_Shop_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201028094215_fixed_cityid_prop_in_shop')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201028094215_fixed_cityid_prop_in_shop', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030155909_changed_has_discount_field_to_method')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Service]') AND [c].[name] = N'HasDiscount');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Service] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Service] DROP COLUMN [HasDiscount];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030155909_changed_has_discount_field_to_method')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'PhoneNumber');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Shop] ALTER COLUMN [PhoneNumber] nvarchar(10) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030155909_changed_has_discount_field_to_method')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'Name');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Shop] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030155909_changed_has_discount_field_to_method')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'Location');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Shop] ALTER COLUMN [Location] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030155909_changed_has_discount_field_to_method')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'E_mail');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Shop] ALTER COLUMN [E_mail] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030155909_changed_has_discount_field_to_method')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'Adress');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Shop] ALTER COLUMN [Adress] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030155909_changed_has_discount_field_to_method')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Service]') AND [c].[name] = N'Name');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Service] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Service] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030155909_changed_has_discount_field_to_method')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cities]') AND [c].[name] = N'Name');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Cities] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Cities] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030155909_changed_has_discount_field_to_method')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201030155909_changed_has_discount_field_to_method', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030170848_changed_agent_is_active_prop')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Agent]') AND [c].[name] = N'IsActive');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Agent] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [Agent] DROP COLUMN [IsActive];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030170848_changed_agent_is_active_prop')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Agent]') AND [c].[name] = N'Name');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Agent] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [Agent] ALTER COLUMN [Name] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030170848_changed_agent_is_active_prop')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201030170848_changed_agent_is_active_prop', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031084509_added_validation_prop')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'IsActive');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [Shop] DROP COLUMN [IsActive];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031084509_added_validation_prop')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'IsSubscribed');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [Shop] DROP COLUMN [IsSubscribed];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031084509_added_validation_prop')
BEGIN
    ALTER TABLE [Shop] ADD [IsValid] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031084509_added_validation_prop')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Schedule]') AND [c].[name] = N'Start');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Schedule] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [Schedule] ALTER COLUMN [Start] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031084509_added_validation_prop')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Schedule]') AND [c].[name] = N'Pstart');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Schedule] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [Schedule] ALTER COLUMN [Pstart] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031084509_added_validation_prop')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Schedule]') AND [c].[name] = N'Pend');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Schedule] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [Schedule] ALTER COLUMN [Pend] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031084509_added_validation_prop')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Schedule]') AND [c].[name] = N'End');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Schedule] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [Schedule] ALTER COLUMN [End] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031084509_added_validation_prop')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201031084509_added_validation_prop', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031090945_revoked_isactive_prop')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'IsValid');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [Shop] DROP COLUMN [IsValid];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031090945_revoked_isactive_prop')
BEGIN
    ALTER TABLE [Shop] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031090945_revoked_isactive_prop')
BEGIN
    ALTER TABLE [Shop] ADD [IsSubscribed] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031090945_revoked_isactive_prop')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201031090945_revoked_isactive_prop', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031100016_removed_some_props_from_shop')
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'IsActive');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [Shop] DROP COLUMN [IsActive];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031100016_removed_some_props_from_shop')
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Shop]') AND [c].[name] = N'IsSubscribed');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Shop] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [Shop] DROP COLUMN [IsSubscribed];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201031100016_removed_some_props_from_shop')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201031100016_removed_some_props_from_shop', N'3.1.9');
END;

GO

