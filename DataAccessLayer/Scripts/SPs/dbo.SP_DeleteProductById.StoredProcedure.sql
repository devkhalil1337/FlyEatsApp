USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteProductById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteProductById]
(
	@ProductId int
	
)
AS  
BEGIN   
Declare @status int  
   update  Products
		SET IsDeleted=1  where ProductId=@ProductId;
   Begin
   SET @status = 1;
    return @status;
 End
END  
 

GO
