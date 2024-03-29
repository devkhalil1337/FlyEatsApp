USE [Flyeats]
GO
/****** Object:  Table [dbo].[Selections]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Selections](
	[SelectionId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[SelectionName] [nvarchar](100) NULL,
	[MinimumSelection] [int] NULL,
	[MaximumSelection] [int] NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[IsDeleted] [bit] NULL,
	[Active] [bit] NULL
) ON [PRIMARY]

GO
