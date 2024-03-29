USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBusinessDaysById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetBusinessDaysById]
(
	@BusinessId int
)
AS  
BEGIN  

   IF NOT EXISTS(Select 1 from BusinessDays WHERE BusinessId=@BusinessId)
    BEGIN
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Monday',0
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Tuesday',0
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Wednesday',0
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Thuresday',0
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Friday',0
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Saturday',0
	execute [dbo].[SP_AddBusinessDays] @BusinessId,'Sunday',0
	Select * from BusinessDays WHERE BusinessId=@BusinessId
   End
   ELSE
   BEGIN
	Select * from BusinessDays WHERE BusinessId=@BusinessId
   END
END  

GO
