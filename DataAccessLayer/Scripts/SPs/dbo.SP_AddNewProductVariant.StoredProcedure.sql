USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewProductVariant]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewProductVariant]
(
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
   Insert ProductVariants
		( 
		   ProductId,
		   BusinessId,
		   VariationName,
		   VariationPrice,
		   CreationDate,
		   UpdateDate,
		   IsDeleted,
		   Active
	 )
   Values
   (
		   @ProductId,
		   @BusinessId,
		   @VariationName,
		   @VariationPrice,
		   @CreationDate,
		   @UpdateDate,
		   @IsDeleted,
		   @Active

   );
   SELECT SCOPE_IDENTITY();
END  


GO
