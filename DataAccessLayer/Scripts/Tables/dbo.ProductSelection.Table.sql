USE [Flyeats]
GO
/****** Object:  Table [dbo].[ProductSelection]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSelection](
	[ProductSelectionId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[SelectionId] [int] NULL,
	[BusinessId] [int] NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL
) ON [PRIMARY]

GO
