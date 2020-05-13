USE [Sloth]
GO

ALTER TABLE [dbo].[User] ADD [Email] VARCHAR (50) NULL;
GO

CREATE TABLE [dbo].[SessionRefreshToken](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[UserId] [uniqueidentifier] NOT NULL,	
	[Token] [nvarchar](500) NULL,
	[ExpiredTime] [datetime] NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
);
GO