USE [Flyeats]
GO
/****** Object:  Table [dbo].[InternalUsers]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternalUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](60) NULL,
	[Password] [nvarchar](500) NOT NULL,
	[MobileNumber] [nvarchar](20) NULL,
	[AccountType] [int] NOT NULL,
	[Role] [int] NOT NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY]

GO
