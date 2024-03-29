USE [Flyeats]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[SettingsId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[RegisterNumber] [nvarchar](100) NULL,
	[Vat] [decimal](10, 2) NULL,
	[VatType] [nchar](15) NULL,
	[ServiceCharges] [decimal](10, 2) NULL,
	[DeliveryCharges] [decimal](10, 2) NULL,
	[MinimumOrder] [decimal](10, 2) NULL,
	[AveragePrepareTime] [decimal](10, 2) NULL,
	[DeliveryTime] [decimal](10, 2) NULL,
	[IsGuestLoginActive] [bit] NULL,
	[IsDeliveryOrderActive] [bit] NULL,
	[IsCollectionOrderActive] [bit] NULL,
	[IsTableOrderActive] [bit] NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[SettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
