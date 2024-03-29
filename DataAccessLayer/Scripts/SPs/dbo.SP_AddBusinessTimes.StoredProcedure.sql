USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddBusinessTimes]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddBusinessTimes]
(
		   @BusinessDaysId int,
		   @StartDay varchar(30),
		   @EndDay varchar(30)
    )
AS  
BEGIN   
   Insert BusinessTimes
		( 
		    BusinessDaysId,
			StartDay,
			EndDay
	 )
   Values
   (
		    @BusinessDaysId,
			@StartDay,
			@EndDay

   );
   SELECT SCOPE_IDENTITY();
END  


GO
