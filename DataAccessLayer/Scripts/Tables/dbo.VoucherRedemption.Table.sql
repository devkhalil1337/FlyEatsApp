USE [Flyeats]
GO
/****** Object:  Table [dbo].[VoucherRedemption]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoucherRedemption](
	[RedeemId] [int] IDENTITY(1,1) NOT NULL,
	[VoucherId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[RedeemDateTime] [datetime] NOT NULL DEFAULT (getdate()),
	[RedeemAmount] [decimal](18, 2) NOT NULL,
	[RedeemCount] [int] NOT NULL DEFAULT ((1)),
PRIMARY KEY CLUSTERED 
(
	[RedeemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[VoucherRedemption]  WITH CHECK ADD FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher] ([VoucherId])
GO
