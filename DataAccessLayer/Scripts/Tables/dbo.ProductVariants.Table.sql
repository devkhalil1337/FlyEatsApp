USE [Flyeats]
GO
/****** Object:  Table [dbo].[ProductVariants]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductVariants](
	[VariantId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[BusinessId] [int] NOT NULL,
	[VariationName] [nvarchar](80) NOT NULL,
	[VariationPrice] [decimal](18, 2) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY]

GO
