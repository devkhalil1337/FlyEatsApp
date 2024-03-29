USE [Flyeats]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NOT NULL,
	[CategoryImage] [nvarchar](150) NOT NULL,
	[CategoryName] [nvarchar](150) NOT NULL,
	[CategoryDetails] [nvarchar](500) NOT NULL,
	[CategorySortBy] [int] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
