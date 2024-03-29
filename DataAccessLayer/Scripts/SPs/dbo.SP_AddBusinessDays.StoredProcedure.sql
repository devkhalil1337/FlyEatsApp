USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddBusinessDays]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddBusinessDays]
(
		   @BusinessId int,
		   @WeekDayName nvarchar(100),
		   @Active bit
      )
AS  
BEGIN   
   Insert BusinessDays
		( 
		    BusinessId,
			WeekDayName,
			CreationDate,
			UpdateDate,
			Active
	 )
   Values
   (
		    @BusinessId,
			@WeekDayName,
			GETDATE(),
			GETDATE(),
			@Active

   );
   SELECT SCOPE_IDENTITY();
END  


GO
