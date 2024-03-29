USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddProduct]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddProduct]
  @BusinessId int,
  @CategoryId int,
  @ProductName nvarchar(150),
  @ProductDescription nvarchar(250),
  @ProductImage nvarchar(150),
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
  @CreationDate datetime2,
  @UpdateDate datetime2,
  @IsDeleted bit,
  @Active bit,
  @IsPopular bit
AS
BEGIN
  SET NOCOUNT ON;

  INSERT INTO Products (BusinessId, CategoryId, ProductName, ProductDescription, ProductImage, ProductSortOrder, ProductQuantity, IsTableProduct, TablePrice, TableVat, IsPickupProduct, PickupPrice, PickupVat, IsDeliveryProduct, DeliveryPrice, DeliveryVat, HasVariations, Featured, CreationDate,UpdateDate, IsDeleted, Active,IsPopular)
  VALUES (@BusinessId, @CategoryId, @ProductName, @ProductDescription, @ProductImage, @ProductSortOrder, @ProductQuantity, @IsTableProduct, @TablePrice, @TableVat, @IsPickupProduct, @PickupPrice, @PickupVat, @IsDeliveryProduct, @DeliveryPrice, @DeliveryVat, @HasVariations, @Featured, @CreationDate, @UpdateDate, @IsDeleted, @Active,@IsPopular)
  SELECT SCOPE_IDENTITY();
END


GO
