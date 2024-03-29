USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateSettings]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateSettings]
(
		   @SettingsId int,
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
Declare @status int  

   update Settings
		SET 
           BusinessId=@BusinessId,
           RegisterNumber=@RegisterNumber,
           Vat=@Vat,
           VatType=@VatType,
           ServiceCharges=@ServiceCharges,
		   DeliveryCharges = @DeliveryCharges,
           MinimumOrder=@MinimumOrder,
           DeliveryTime=@DeliveryTime,
		   AveragePrepareTime=@AveragePrepareTime,
           UpdateDate=@UpdateDate,
           IsGuestLoginActive=@IsGuestLoginActive,
		   IsDeliveryOrderActive=@IsDeliveryOrderActive,
		   IsCollectionOrderActive=@IsCollectionOrderActive,
		   IsTableOrderActive=@IsTableOrderActive
	  where SettingsId=@SettingsId and BusinessId = @BusinessId;
   
END
Begin
   SET @status = 1;
    return @status;
 End  


GO
