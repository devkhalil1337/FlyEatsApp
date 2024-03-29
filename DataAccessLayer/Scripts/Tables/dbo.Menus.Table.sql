USE [Flyeats]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [bigint] NOT NULL,
	[MenuName] [nvarchar](255) NOT NULL CONSTRAINT [DF_Menus_MenuName]  DEFAULT (''),
	[MenuUrl] [nvarchar](255) NOT NULL CONSTRAINT [DF_Menus_MenuUrl]  DEFAULT (''),
	[OrderBy] [int] NOT NULL CONSTRAINT [DF_Menus_OrderBy]  DEFAULT ((0)),
	[isActive] [bit] NOT NULL CONSTRAINT [DF_Menus_isActive]  DEFAULT ((0)),
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
