USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteBusinessInfo]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteBusinessInfo]
(
	@BusinessId int	
)
AS  
BEGIN   
Declare @status int  
   update  BusinessInfo
		SET Deleted=1 where BusinessId=@BusinessId 
   Begin
   SET @status = 1;
    return @status;
 End
END  


GO
