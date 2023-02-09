USE [Flyeats]
GO

/****** Object:  Table [dbo].[Order]    Script Date: 2/9/2023 4:44:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Order](
	[OrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[OrderInvoiceNumber] [nvarchar](255) NOT NULL,
	[OrderType] [nvarchar](20) NOT NULL,
	[OrderTableId] [int] NOT NULL,
	[OrderStatus] [nvarchar](50) NOT NULL,
	[OrderServiceCharges] [decimal](10, 2) NOT NULL,
	[OrderDiscount] [decimal](10, 2) NOT NULL,
	[OrderVoucherId] [int] NOT NULL,
	[OrderVoucherDiscountAmount] [decimal](10, 2) NOT NULL,
	[OrderTotalAmount] [decimal](10, 2) NOT NULL,
	[OrderVatAmount] [decimal](10, 2) NOT NULL,
	[OrderVatPercentage] [decimal](10, 2) NOT NULL,
	[VatType] [nvarchar](30) NOT NULL,
	[OrderPaymentStatus] [nvarchar](30) NOT NULL,
	[OrderPaymentMethod] [nvarchar](30) NOT NULL,
	[AverageOrderPreprationTime] [int] NOT NULL,
	[OrderComments] [nvarchar](255) NOT NULL,
	[OrderDeliveryTime] [int] NOT NULL,
	[CustomerDeliveryId] [int] NOT NULL,
	[OrderCompletedBy] [nvarchar](50) NOT NULL,
	[CreationDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


