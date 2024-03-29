USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateProductVariant]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateProductVariant]
(
		   @VariantId int,
		   @ProductId int,
		   @BusinessId int,
		   @VariationName nvarchar(80),
		   @VariationPrice decimal(18,2),
		   @CreationDate datetime2(7),
		   @UpdateDate datetime2(7),
		   @IsDeleted bit,
		   @Active bit
)
AS  
BEGIN  
Declare @status int  

   update  ProductVariants
		SET 
	       
			ProductId=@ProductId,
			BusinessId=@BusinessId,
			VariationName=@VariationName,
			VariationPrice=@VariationPrice,
			CreationDate=@CreationDate,
			UpdateDate=@UpdateDate,
			IsDeleted=@IsDeleted,
			Active=@Active

	  where VariantId=@VariantId
   
END
Begin
   SET @status = 1;
    return @status;
 End  


GO
