USE [Flyeats]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ProductName] [nvarchar](150) NOT NULL,
	[ProductDescription] [nvarchar](250) NOT NULL,
	[ProductImage] [nvarchar](150) NOT NULL,
	[ProductSortOrder] [int] NOT NULL,
	[ProductQuantity] [int] NOT NULL,
	[IsTableProduct] [bit] NOT NULL,
	[TablePrice] [decimal](18, 2) NOT NULL,
	[TableVat] [decimal](18, 2) NOT NULL,
	[IsPickupProduct] [bit] NOT NULL,
	[PickupPrice] [decimal](18, 2) NOT NULL,
	[PickupVat] [decimal](18, 2) NOT NULL,
	[IsDeliveryProduct] [bit] NOT NULL,
	[DeliveryPrice] [decimal](18, 2) NOT NULL,
	[DeliveryVat] [decimal](18, 2) NOT NULL,
	[HasVariations] [bit] NOT NULL,
	[Featured] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[IsPopular] [bit] NOT NULL DEFAULT ((0))
) ON [PRIMARY]

GO
