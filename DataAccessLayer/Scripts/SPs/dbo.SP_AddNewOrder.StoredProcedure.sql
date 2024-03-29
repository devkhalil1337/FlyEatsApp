USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewOrder]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddNewOrder]
(
    @BusinessId INT,
    @CustomerId INT,
    @OrderInvoiceNumber NVARCHAR(255),
    @OrderType NVARCHAR(20),
    @OrderTableId INT,
    @OrderStatus NVARCHAR(50),
    @ServiceChargeAmount DECIMAL(10,2),
    @DiscountAmount DECIMAL(10,2),
    @VoucherId INT,
    @VoucherDiscountAmount DECIMAL(10,2),
    @TotalAmount DECIMAL(10,2),
    @VatAmount DECIMAL(10,2),
    @VatPercentage DECIMAL(10,2),
    @VatType NVARCHAR(30),
    @PaymentStatus NVARCHAR(30),
    @PaymentMethod NVARCHAR(30),
    @AveragePreparationTime INT,
    @Comments NVARCHAR(255),
    @DeliveryTime INT,
    @CustomerDeliveryId INT,
    @CompletedBy NVARCHAR(50),
    @DeliveryCharges DECIMAL(10,2),
    @CardPaymentId NVARCHAR(50) = NULL,
    @CreatedDate NVARCHAR(50) = NULL,
    @ModifiedDate DATETIME = NULL,
    @IsDeleted BIT
)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Orders
    (
        BusinessId,
        CustomerId,
        OrderInvoiceNumber,
        OrderType,
        OrderTableId,
        OrderStatus,
        ServiceChargeAmount,
        DiscountAmount,
        VoucherId,
        VoucherDiscountAmount,
        TotalAmount,
        VatAmount,
        VatPercentage,
        VatType,
        PaymentStatus,
        PaymentMethod,
        AveragePreparationTime,
        Comments,
        DeliveryTime,
        CustomerDeliveryId,
        CompletedBy,
        DeliveryCharges,
        CardPaymentId,
        CreatedDate,
        ModifiedDate,
        IsDeleted
    )
    VALUES
    (
        @BusinessId,
        @CustomerId,
        @OrderInvoiceNumber,
        @OrderType,
        @OrderTableId,
        @OrderStatus,
        @ServiceChargeAmount,
        @DiscountAmount,
        @VoucherId,
        @VoucherDiscountAmount,
        @TotalAmount,
        @VatAmount,
        @VatPercentage,
        @VatType,
        @PaymentStatus,
        @PaymentMethod,
        @AveragePreparationTime,
        @Comments,
        @DeliveryTime,
        @CustomerDeliveryId,
        @CompletedBy,
        @DeliveryCharges,
        @CardPaymentId,
        @CreatedDate,
        @ModifiedDate,
        @IsDeleted
    )
    SELECT SCOPE_IDENTITY();
END


GO
