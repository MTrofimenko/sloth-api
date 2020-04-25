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
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[Chat](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Name] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LogoId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[ChatMember](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[UserId] [uniqueidentifier] NOT NULL,
	[ChatId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[ChatMessage](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[ChatMemberId] [uniqueidentifier] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[ReplyToMessageId] [uniqueidentifier] NULL,
	[ForwardFromUserId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

-- Settings: User --

CREATE TABLE [dbo].[UserSetting](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Name] [varbinary](100) NOT NULL,
	[Description] [varbinary](500) NULL,
	[Type] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[UserDefSettingValue](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[SettingId] [uniqueidentifier] NOT NULL,
	[NumberValue] [float] NULL,
	[StringValue] [nvarchar](500) NULL,
	[DateTimeValue] [datetime] NULL,
	[BooleanValue] [bit] NULL,
	[LookupValueId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[UserSettingValue](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[SettingId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[NumberValue] [float] NULL,
	[StringValue] [nvarchar](500) NULL,
	[DateTimeValue] [datetime] NULL,
	[BooleanValue] [bit] NULL,
	[LookupValueId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[UserSettingLookupValue](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[SettingId] [uniqueidentifier] NOT NULL,	
	[Value] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)


-- Settings: Chat --

CREATE TABLE [dbo].[ChatSetting](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Name] [varbinary](100) NOT NULL,
	[Description] [varbinary](500) NULL,
	[Type] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[ChatDefSettingValue](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[SettingId] [uniqueidentifier] NOT NULL,
	[NumberValue] [float] NULL,
	[StringValue] [nvarchar](500) NULL,
	[DateTimeValue] [datetime] NULL,
	[BooleanValue] [bit] NULL,
	[LookupValueId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[ChatSettingValue](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[SettingId] [uniqueidentifier] NOT NULL,
	[ChatId] [uniqueidentifier] NOT NULL,
	[NumberValue] [float] NULL,
	[StringValue] [nvarchar](500) NULL,
	[DateTimeValue] [datetime] NULL,
	[BooleanValue] [bit] NULL,
	[LookupValueId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[ChatSettingLookupValue](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[SettingId] [uniqueidentifier] NOT NULL,	
	[Value] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

-- Settings: Chat Member --

CREATE TABLE [dbo].[ChatMemberSetting](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Name] [varbinary](100) NOT NULL,
	[Description] [varbinary](500) NULL,
	[Type] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[ChatMemberDefSettingValue](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[SettingId] [uniqueidentifier] NOT NULL,
	[NumberValue] [float] NULL,
	[StringValue] [nvarchar](500) NULL,
	[DateTimeValue] [datetime] NULL,
	[BooleanValue] [bit] NULL,
	[LookupValueId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[ChatMemberSettingValue](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[SettingId] [uniqueidentifier] NOT NULL,
	[ChatMemberId] [uniqueidentifier] NOT NULL,
	[NumberValue] [float] NULL,
	[StringValue] [nvarchar](500) NULL,
	[DateTimeValue] [datetime] NULL,
	[BooleanValue] [bit] NULL,
	[LookupValueId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

CREATE TABLE [dbo].[ChatMemberSettingLookupValue](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[SettingId] [uniqueidentifier] NOT NULL,	
	[Value] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL DEFAULT GetUtcDate(),
	[ModifiedOn] [datetime] NULL DEFAULT GetUtcDate()
)

GO