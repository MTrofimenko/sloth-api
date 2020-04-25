USE [Sloth]
GO

ALTER TABLE [dbo].[ChatMember] ADD [PublicKey] VARCHAR (250) NULL;
ALTER TABLE [dbo].[ChatMember] ADD [Status] INT NOT NULL;
ALTER TABLE [dbo].[ChatMember] DROP COLUMN [IsActive];

ALTER TABLE [dbo].[Chat] ADD [Status] INT NOT NULL;
ALTER TABLE [dbo].[Chat] DROP COLUMN [IsActive];

ALTER TABLE [dbo].[Chat] ALTER COLUMN [Name] [nvarchar](100) NULL

GO