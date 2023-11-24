USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateBusinessTimes]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateBusinessTimes]
(
           
		   @BusinessTimesId int,
           @StartDay varchar(30),
		   @EndDay varchar(30)
)
AS  
BEGIN  
Declare @status int  

   update  BusinessTimes
		SET 
	       
			StartDay=@StartDay,
			EndDay=@EndDay

	  where BusinessTimesId=@BusinessTimesId
   
END
Begin
   SET @status = 1;
    return @status;
 End  


GO
