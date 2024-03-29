USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewSettings]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddNewSettings]
(
           @BusinessId int,
           @RegisterNumber nvarchar(100),
           @Vat decimal(18,2),
           @VatType nvarchar(15),
           @ServiceCharges decimal(18,2),
		   @DeliveryCharges decimal(18,2),
           @MinimumOrder decimal(18,2),
           @DeliveryTime decimal(18,2),
		   @AveragePrepareTime decimal(18,2),
           @CreationDate datetime2(7),
           @UpdateDate datetime2(7),
		   @IsDeliveryOrderActive bit,
     	   @IsCollectionOrderActive bit,
		   @IsTableOrderActive bit,
           @IsGuestLoginActive bit
)
AS  
BEGIN   
   Insert Settings
		( 
           BusinessId,
           RegisterNumber,
           Vat,
           VatType,
           ServiceCharges,
		   DeliveryCharges,
           MinimumOrder,
           DeliveryTime,
		   AveragePrepareTime,
           CreationDate,
           UpdateDate,
		   IsDeliveryOrderActive,
		   IsCollectionOrderActive,
		   IsTableOrderActive,
           IsGuestLoginActive

		 )
   Values
   (
           @BusinessId,
		   @RegisterNumber,
		   @Vat,
		   @VatType,
		   @ServiceCharges,
		   @DeliveryCharges,
		   @MinimumOrder,
		   @DeliveryTime,
		   @AveragePrepareTime,
           @CreationDate,
           @UpdateDate,
		   @IsDeliveryOrderActive,
		   @IsTableOrderActive,
		   @IsCollectionOrderActive,
		   @IsGuestLoginActive

   );
   SELECT SCOPE_IDENTITY();
END  


GO
