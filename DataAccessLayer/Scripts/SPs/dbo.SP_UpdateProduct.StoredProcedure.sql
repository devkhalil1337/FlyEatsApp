USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateProduct]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateProduct]
  @ProductId int,
  @BusinessId int,
  @CategoryId int,
  @ProductName nvarchar(250),
  @ProductDescription nvarchar(500),
  @ProductImage nvarchar(250),
  @ProductSortOrder int,
  @ProductQuantity int,
  @IsTableProduct bit,
  @TablePrice decimal(18, 2),
  @TableVat decimal(18, 2),
  @IsPickupProduct bit,
  @PickupPrice decimal(18, 2),
  @PickupVat decimal(18, 2),
  @IsDeliveryProduct bit,
  @DeliveryPrice decimal(18, 2),
  @DeliveryVat decimal(18, 2),
  @HasVariations bit,
  @Featured bit,
  @UpdateDate datetime2,
  @IsDeleted bit,
  @Active bit
AS
BEGIN
  SET NOCOUNT ON;

  UPDATE Products
  SET 
    BusinessId = @BusinessId,
    CategoryId = @CategoryId,
    ProductName = @ProductName,
    ProductDescription = @ProductDescription,
    ProductImage = @ProductImage,
    ProductSortOrder = @ProductSortOrder,
    ProductQuantity = @ProductQuantity,
    IsTableProduct = @IsTableProduct,
    TablePrice = @TablePrice,
    TableVat = @TableVat,
    IsPickupProduct = @IsPickupProduct,
    PickupPrice = @PickupPrice,
    PickupVat = @PickupVat,
    IsDeliveryProduct = @IsDeliveryProduct,
    DeliveryPrice = @DeliveryPrice,
    DeliveryVat = @DeliveryVat,
    HasVariations = @HasVariations,
    Featured = @Featured,
    UpdateDate = @UpdateDate,
    IsDeleted = @IsDeleted,
    Active = @Active
  WHERE ProductId = @ProductId;
  IF @@ROWCOUNT = 1
	BEGIN
		RETURN 1;
	END
	ELSE
	BEGIN
		RETURN 0;
	END
END


GO
