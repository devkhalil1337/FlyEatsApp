USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateProductSelection]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateProductSelection]
(
		@ProductSelectionId int,
		@ProductId int,
		@SelectionId int,
		@BusinessId int,
		@UpdateDate datetime2(7)

		)
AS  
BEGIN  
   update  ProductSelection
		SET 
	       
			ProductId=@ProductId ,
			SelectionId=@SelectionId ,
			BusinessId=@BusinessId ,
			UpdateDate=@UpdateDate 


	  where ProductSelectionId=@ProductSelectionId
   SELECT SCOPE_IDENTITY();
END


GO
