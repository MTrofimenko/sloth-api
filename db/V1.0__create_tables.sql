USE [Sloth]
GO

CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Login] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LogoId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NOT NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[Chat](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Name] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LogoId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NOT NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[ChatMember](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[UserId] [uniqueidentifier] NOT NULL,
	[ChatId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NOT NULL DEFAULT GetUtcDate()
)

GO