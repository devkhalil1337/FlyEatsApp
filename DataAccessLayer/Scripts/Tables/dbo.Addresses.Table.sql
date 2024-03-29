USE [Flyeats]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AddressLine1] [varchar](255) NOT NULL,
	[AddressLine2] [varchar](255) NULL,
	[City] [varchar](255) NOT NULL,
	[StateProvince] [varchar](255) NOT NULL,
	[ZipPostalCode] [varchar](255) NOT NULL,
	[Country] [varchar](255) NOT NULL,
	[Latitude] [decimal](9, 6) NULL,
	[Longitude] [decimal](9, 6) NULL,
	[PhoneNumber] [varchar](255) NULL,
	[AddressType] [varchar](255) NULL,
	[Active] [bit] NOT NULL DEFAULT ((1)),
	[Timestamp] [datetime] NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
