USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewOrderDetails]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewOrderDetails]
(
	@OrderId nvarchar(255),
    @BusinessId int,
    @CategoryId int,
    @ProductId int,
    @VariantId int,
    @ProductName nvarchar(255),
    @ProductQuantity int,
    @ProductPrice decimal(8,2),
    @ProductComments nvarchar(255),
    @ProductHaveSelection bit
)
AS  
BEGIN   
 INSERT INTO [OrderDetails]
           (
      [OrderId]
      ,[BusinessId]
      ,[CategoryId]
      ,[ProductId]
      ,[VariantId]
      ,[ProductName]
      ,[ProductQuantity]
      ,[ProductPrice]
      ,[ProductComments]
      ,[ProductHaveSelection])
     VALUES
           (    
		   @OrderId,
           @BusinessId,
           @CategoryId,
           @ProductId,
           @VariantId,
           @ProductName,
           @ProductQuantity,
           @ProductPrice,
           @ProductComments,
           @ProductHaveSelection)
		   SELECT SCOPE_IDENTITY();
END  


GO
