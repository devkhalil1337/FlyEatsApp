USE [Flyeats]
GO
/****** Object:  Table [dbo].[SelectionChoices]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SelectionChoices](
	[ChoicesId] [int] IDENTITY(1,1) NOT NULL,
	[SelectionId] [int] NULL,
	[BusinessId] [int] NULL,
	[ChoiceName] [nvarchar](100) NULL,
	[ChoicePrice] [decimal](18, 2) NULL,
	[ChoiceSortedBy] [int] NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]

GO
