USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteSelectionChoicesById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteSelectionChoicesById]
(
	@ChoicesId int
	
)
AS  
BEGIN   
Declare @status int  
   update  SelectionChoices
		SET IsDeleted=1  where ChoicesId=@ChoicesId;
   Begin
   SET @status = 1;
    return @status;
 End
END  

GO
