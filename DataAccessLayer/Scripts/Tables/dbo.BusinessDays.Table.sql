USE [Flyeats]
GO
/****** Object:  Table [dbo].[BusinessDays]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessDays](
	[BusinessDaysId] [bigint] IDENTITY(1,1) NOT NULL,
	[BusinessId] [bigint] NULL,
	[WeekDayName] [nchar](20) NULL,
	[CreationDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_dbo.BusinessDays] PRIMARY KEY CLUSTERED 
(
	[BusinessDaysId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
