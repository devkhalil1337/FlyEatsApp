USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateOrderAttributes]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateOrderAttributes]
(
    @OrderInvoiceNumber nvarchar(255),
    @OrderStatus nvarchar(50),
    @ServiceChargeAmount decimal(10, 2),
    @DiscountAmount decimal(10, 2),
    @VoucherId int,
    @VoucherDiscountAmount decimal(10, 2),
    @TotalAmount decimal(10, 2),
    @VatAmount decimal(10, 2),
    @VatPercentage decimal(10, 2),
    @VatType nvarchar(30),
    @PaymentStatus nvarchar(30),
    @PaymentMethod nvarchar(30),
    @AveragePreparationTime int,
    @Comments nvarchar(255),
    @DeliveryTime int,
    @CustomerDeliveryId int,
    @CompletedBy nvarchar(50),
    @DeliveryCharges decimal(10, 2),
    @CardPaymentId nvarchar(50),
    @ModifiedDate nvarchar(50)
)
AS  
BEGIN  
Declare @status int  

   update  [Flyeats].[dbo].[Orders]
		SET 
        OrderStatus = @OrderStatus,
        ServiceChargeAmount = @ServiceChargeAmount,
        DiscountAmount = @DiscountAmount,
        VoucherId = @VoucherId,
        VoucherDiscountAmount = @VoucherDiscountAmount,
        TotalAmount = @TotalAmount,
        VatAmount = @VatAmount,
        VatPercentage = @VatPercentage,
        VatType = @VatType,
        PaymentStatus = @PaymentStatus,
        PaymentMethod = @PaymentMethod,
        AveragePreparationTime = @AveragePreparationTime,
        Comments = @Comments,
        DeliveryTime = @DeliveryTime,
        CustomerDeliveryId = @CustomerDeliveryId,
        CompletedBy = @CompletedBy,
        DeliveryCharges = @DeliveryCharges,
        CardPaymentId = @CardPaymentId,
        ModifiedDate = @ModifiedDate
	  where OrderInvoiceNumber=@OrderInvoiceNumber;
   Begin
   SET @status = 1;
 End
    return @status;
END  



GO
