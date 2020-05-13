USE [Sloth]
GO

ALTER TABLE [dbo].[User] ADD [Email] VARCHAR (50) NULL;
GO

CREATE TABLE [dbo].[SessionRefreshToken](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[UserId] [uniqueidentifier] NOT NULL,	
	[Token] [nvarchar](500) NOT NULL,
	[ExpiredTime] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT 0,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
);
GO