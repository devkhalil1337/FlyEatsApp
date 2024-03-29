USE [Flyeats]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](255) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[BusinessId] [int] NULL,
	[PasswordHash] [varchar](255) NULL,
	[Salt] [varchar](255) NULL,
	[isGuest] [bit] NOT NULL DEFAULT ((0)),
	[CreatedAt] [datetime] NULL DEFAULT (getdate()),
	[UpdatedAt] [datetime] NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
