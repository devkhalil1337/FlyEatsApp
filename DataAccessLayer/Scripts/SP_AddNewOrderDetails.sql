      USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewOrder]    Script Date: 2/9/2023 9:57:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_AddNewOrderDetails]
(
	@OrderId bigint,
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
