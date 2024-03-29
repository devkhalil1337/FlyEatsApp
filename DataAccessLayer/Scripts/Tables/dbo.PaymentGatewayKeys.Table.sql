USE [Flyeats]
GO
/****** Object:  Table [dbo].[PaymentGatewayKeys]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentGatewayKeys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[GatewayName] [nvarchar](50) NOT NULL,
	[ApiKey] [nvarchar](500) NOT NULL,
	[ApiSecret] [nvarchar](500) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((0)),
	[PaymentMode] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_BusinessId_GatewayName] UNIQUE NONCLUSTERED 
(
	[BusinessId] ASC,
	[GatewayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
