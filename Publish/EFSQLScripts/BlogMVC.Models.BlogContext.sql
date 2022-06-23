IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621065923_initial')
BEGIN
    CREATE TABLE [Categories] (
        [Id] bigint NOT NULL IDENTITY,
        [name] nvarchar(80) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621065923_initial')
BEGIN
    CREATE TABLE [Blogs] (
        [BlogID] bigint NOT NULL IDENTITY,
        [Title] nvarchar(60) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [content] nvarchar(max) NOT NULL,
        [Image] nvarchar(max) NULL,
        [CategoryId] bigint NOT NULL,
        CONSTRAINT [PK_Blogs] PRIMARY KEY ([BlogID]),
        CONSTRAINT [FK_Blogs_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621065923_initial')
BEGIN
    CREATE INDEX [IX_Blogs_CategoryId] ON [Blogs] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621065923_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220621065923_initial', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621102823_add-user1')
BEGIN
    ALTER TABLE [Blogs] ADD [UserId] bigint NULL DEFAULT CAST(1 AS bigint);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621102823_add-user1')
BEGIN
    CREATE TABLE [Users] (
        [Id] bigint NOT NULL IDENTITY,
        [Username] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [Role] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621102823_add-user1')
BEGIN
    CREATE INDEX [IX_Blogs_UserId] ON [Blogs] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621102823_add-user1')
BEGIN
    ALTER TABLE [Blogs] ADD CONSTRAINT [FK_Blogs_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621102823_add-user1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220621102823_add-user1', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621103215_add-user2')
BEGIN
    ALTER TABLE [Blogs] DROP CONSTRAINT [FK_Blogs_Users_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621103215_add-user2')
BEGIN
    DROP INDEX [IX_Blogs_UserId] ON [Blogs];
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Blogs]') AND [c].[name] = N'UserId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Blogs] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Blogs] ALTER COLUMN [UserId] bigint NOT NULL;
    ALTER TABLE [Blogs] ADD DEFAULT CAST(0 AS bigint) FOR [UserId];
    CREATE INDEX [IX_Blogs_UserId] ON [Blogs] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621103215_add-user2')
BEGIN
    ALTER TABLE [Blogs] ADD CONSTRAINT [FK_Blogs_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621103215_add-user2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220621103215_add-user2', N'6.0.6');
END;
GO

COMMIT;
GO

