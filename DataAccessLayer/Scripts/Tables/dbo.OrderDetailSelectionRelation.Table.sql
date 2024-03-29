USE [Flyeats]
GO
/****** Object:  Table [dbo].[OrderDetailSelectionRelation]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetailSelectionRelation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderDetailsId] [int] NOT NULL,
	[BusinessId] [int] NOT NULL,
	[SelectionId] [int] NOT NULL,
	[ChoicesId] [int] NOT NULL,
	[ChoiceName] [nvarchar](255) NULL,
	[ChoicePrice] [decimal](10, 2) NULL,
 CONSTRAINT [PK_OrderDetailSelectionRelation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
