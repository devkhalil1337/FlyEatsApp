USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewOrderDetails]    Script Date: 2/11/2023 2:57:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_AddNewOrderDetails]
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
END  
