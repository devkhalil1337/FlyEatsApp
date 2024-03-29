USE [Flyeats]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailsId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [nvarchar](255) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[VariantId] [int] NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[ProductQuantity] [int] NOT NULL,
	[ProductPrice] [decimal](8, 2) NOT NULL,
	[ProductComments] [nvarchar](255) NULL,
	[ProductHaveSelection] [bit] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
