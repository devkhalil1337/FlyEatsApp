USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateBusinessDays]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateBusinessDays]
(
		   @BusinessDaysId int,
		   @BusinessId int,
		   @WeekDayName nvarchar(100),
		   @CreationDate datetime,
		   @UpdateDate datetime,
		   @Active bit
)
AS  
BEGIN  
Declare @status int  

   update  BusinessDays
		SET 
	       
			BusinessId=@BusinessId,
			WeekDayName=@WeekDayName,
			CreationDate=@CreationDate,
			UpdateDate=@UpdateDate,
			Active=@Active

	  where BusinessDaysId=@BusinessDaysId
   
END
Begin
   SET @status = 1;
    return @status;
 End  


GO
