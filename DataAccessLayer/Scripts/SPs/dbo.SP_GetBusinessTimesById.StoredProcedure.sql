USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBusinessTimesById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetBusinessTimesById]
(
	@BusinessDaysId int
)
AS  
BEGIN  
	if NOT EXISTS(Select * from BusinessTimes WHERE BusinessDaysId=@BusinessDaysId)
	Begin
/*		execute SP_AddBusinessTimes @BusinessDaysId,'2017-02-23 08:00','2017-02-23 19:00' */
        Select * from BusinessTimes WHERE BusinessDaysId=@BusinessDaysId
	END
	ELSE
	Begin
	 Select * from BusinessTimes WHERE BusinessDaysId=@BusinessDaysId
	END

END  

GO
