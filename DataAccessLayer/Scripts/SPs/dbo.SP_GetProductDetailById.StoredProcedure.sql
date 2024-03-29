USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductDetailById]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetProductDetailById]
(
	@ProductId int

 
)
AS  
BEGIN  

   select p.ProductId, c.CategoryName,p.CategoryId
      ,p.BusinessId
      ,p.ProductImage
      ,p.ProductName
      ,p.ProductDescription
	  ,p.IsTableProduct
      ,p.TablePrice
      ,p.TableVat
      ,p.IsPickupProduct
      ,p.PickupPrice
      ,p.PickupVat
	  ,p.IsDeliveryProduct
      ,p.DeliveryPrice
      ,p.DeliveryVat
      ,p.ProductSortOrder
      ,p.ProductQuantity
      ,p.HasVariations
      ,p.Featured
      ,p.CreationDate
      ,p.UpdateDate
      ,p.IsDeleted
      ,p.Active FROM Flyeats.dbo.Products p inner join Flyeats.dbo.Categories c on c.categoryId = p.CategoryId where ProductId = @ProductId
END  

GO
