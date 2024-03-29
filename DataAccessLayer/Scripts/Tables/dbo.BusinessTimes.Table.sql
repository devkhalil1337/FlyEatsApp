USE [Flyeats]
GO
/****** Object:  Table [dbo].[BusinessTimes]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BusinessTimes](
	[BusinessTimesId] [bigint] IDENTITY(1,1) NOT NULL,
	[BusinessDaysId] [bigint] NULL,
	[StartDay] [varchar](30) NULL,
	[EndDay] [varchar](30) NULL,
 CONSTRAINT [PK_BusinessTimes] PRIMARY KEY CLUSTERED 
(
	[BusinessTimesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[BusinessTimes]  WITH CHECK ADD  CONSTRAINT [FK_BusinessTimes_BusinessDays] FOREIGN KEY([BusinessDaysId])
REFERENCES [dbo].[BusinessDays] ([BusinessDaysId])
GO
ALTER TABLE [dbo].[BusinessTimes] CHECK CONSTRAINT [FK_BusinessTimes_BusinessDays]
GO
