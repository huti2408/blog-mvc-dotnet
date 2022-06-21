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
    VALUES (N'20220621065923_initial', N'6.0.5');
END;
GO

COMMIT;
GO

