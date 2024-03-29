USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewProductSelection]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewProductSelection]
(
		@ProductId int,
		@SelectionId int,
		@BusinessId int,
		@CreationDate datetime2(7),
		@UpdateDate datetime2(7)
      )
AS  
BEGIN   

   Insert ProductSelection
		( 
		   	ProductId,
			SelectionId,
			BusinessId,
			CreationDate,
			UpdateDate

	 )
   Values
   (
		    @ProductId,
			@SelectionId,
			@BusinessId,
			@CreationDate,
			@UpdateDate

   );
   SELECT SCOPE_IDENTITY();
END  
 


GO
