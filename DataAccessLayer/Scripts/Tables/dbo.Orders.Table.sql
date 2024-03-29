USE [Flyeats]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[OrderInvoiceNumber] [nvarchar](255) NOT NULL,
	[OrderType] [nvarchar](20) NOT NULL,
	[OrderTableId] [int] NOT NULL,
	[OrderStatus] [nvarchar](50) NOT NULL,
	[ServiceChargeAmount] [decimal](10, 2) NOT NULL,
	[DiscountAmount] [decimal](10, 2) NOT NULL,
	[VoucherId] [int] NOT NULL,
	[VoucherDiscountAmount] [decimal](10, 2) NOT NULL,
	[TotalAmount] [decimal](10, 2) NOT NULL,
	[VatAmount] [decimal](10, 2) NOT NULL,
	[VatPercentage] [decimal](10, 2) NOT NULL,
	[VatType] [nvarchar](30) NOT NULL,
	[PaymentStatus] [nvarchar](30) NOT NULL,
	[PaymentMethod] [nvarchar](30) NOT NULL,
	[AveragePreparationTime] [int] NOT NULL,
	[Comments] [nvarchar](255) NOT NULL,
	[DeliveryTime] [int] NOT NULL,
	[CustomerDeliveryId] [int] NOT NULL,
	[CompletedBy] [nvarchar](50) NOT NULL,
	[DeliveryCharges] [decimal](10, 2) NOT NULL,
	[CardPaymentId] [nvarchar](50) NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[ModifiedDate] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]

GO
