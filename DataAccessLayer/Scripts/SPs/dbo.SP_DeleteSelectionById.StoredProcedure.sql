USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteSelectionById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteSelectionById]
(
	@SelectionId int,
	@BusinessId int
	
)
AS  
BEGIN   
Declare @status int  
   update  Selections
		SET IsDeleted=1  where SelectionId=@SelectionId and BusinessId = @BusinessId;
   update  SelectionChoices
		SET IsDeleted=1  where SelectionId=@SelectionId and BusinessId = @BusinessId;;
   Begin
   SET @status = 1;
    return @status;
 End
END  
GO
