USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllProductsVariantsByProductId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllProductsVariantsByProductId]
(
	@ProductId int
)
AS  
BEGIN  
   select * from ProductVariants where ProductId=@ProductId and IsDeleted=0;
END  

GO
