USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteProductSelectionById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteProductSelectionById]
(
	@ProductId int
	
)
AS  
BEGIN   
Declare @status int  
DELETE FROM ProductSelection where ProductId=@ProductId;
  
   Begin
   SET @status = 1;
    return @status;
 End
END  

GO
