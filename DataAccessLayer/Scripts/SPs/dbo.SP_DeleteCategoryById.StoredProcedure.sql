USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteCategoryById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DeleteCategoryById]
(
	@CategoryId
 int
)
AS  
BEGIN   
Declare @status int  
   update  Categories
		SET IsDeleted=1 where CategoryId=@CategoryId 
   Begin
   SET @status = 1;
    return @status;
 End
END  
 

GO
