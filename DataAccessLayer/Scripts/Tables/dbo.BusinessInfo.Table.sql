USE [Flyeats]
GO
/****** Object:  Table [dbo].[BusinessInfo]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessInfo](
	[BusinessId] [int] IDENTITY(1,1) NOT NULL,
	[Localization] [nvarchar](50) NOT NULL,
	[BusinessLogo] [nvarchar](100) NOT NULL,
	[BusinessName] [nvarchar](100) NOT NULL,
	[BusinessContact] [nvarchar](50) NOT NULL,
	[BusinessEmail] [nvarchar](50) NOT NULL,
	[BusinessAddress] [nvarchar](255) NOT NULL,
	[BusinessPostcode] [nvarchar](10) NOT NULL,
	[BusinessCity] [nvarchar](30) NOT NULL,
	[BusinessCountry] [nvarchar](30) NOT NULL,
	[BusinessDetails] [nvarchar](255) NOT NULL,
	[BusinessLatitude] [nvarchar](25) NOT NULL,
	[BusinessLongitude] [nvarchar](25) NOT NULL,
	[BusinessCurrency] [nvarchar](25) NOT NULL,
	[BusinessWebsiteUrl] [nvarchar](50) NOT NULL,
	[BusinessTempClose] [bit] NOT NULL,
	[ClosetillDate] [datetime2](7) NOT NULL,
	[BusinessExpiryDate] [datetime2](7) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY]

GO
