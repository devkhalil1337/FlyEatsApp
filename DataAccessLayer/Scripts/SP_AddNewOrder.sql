USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewOrder]    Script Date: 2/9/2023 4:36:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_AddNewOrder]
(
	@BusinessId int,
    @CustomerId int,
    @OrderInvoiceNumber nvarchar(255),
    @OrderType nvarchar(20),
    @OrderTableId int,
    @OrderStatus nvarchar(50),
    @OrderServiceCharges decimal(10,2),
    @OrderDiscount decimal(10,2),
    @OrderVoucherId int,
    @OrderVoucherDiscountAmount decimal(10,2),
    @OrderTotalAmount decimal(10,2),
    @OrderVatAmount decimal(10,2),
    @OrderVatPercentage decimal(10,2),
    @VatType nvarchar(30),
    @OrderPaymentStatus nvarchar(30),
    @OrderPaymentMethod nvarchar(30),
    @AverageOrderPreprationTime int,
    @OrderComments nvarchar(255),
    @OrderDeliveryTime int,
    @CustomerDeliveryId int,
    @OrderCompletedBy nvarchar(50),
    @CreationDate datetime,
    @UpdateDate datetime,
    @IsDeleted bit
)
AS  
BEGIN   
 INSERT INTO [OrderId]
           ([BusinessId]
           ,[CustomerId]
           ,[OrderInvoiceNumber]
           ,[OrderType]
           ,[OrderTableId]
           ,[OrderStatus]
           ,[OrderServiceCharges]
           ,[OrderDiscount]
           ,[OrderVoucherId]
           ,[OrderVoucherDiscountAmount]
           ,[OrderTotalAmount]
           ,[OrderVatAmount]
           ,[OrderVatPercentage]
           ,[VatType]
           ,[OrderPaymentStatus]
           ,[OrderPaymentMethod]
           ,[AverageOrderPreprationTime]
           ,[OrderComments]
           ,[OrderDeliveryTime]
           ,[CustomerDeliveryId]
           ,[OrderCompletedBy]
           ,[CreationDate]
           ,[UpdateDate]
           ,[IsDeleted])
     VALUES
           (@BusinessId,
           @CustomerId,
           @OrderInvoiceNumber,
           @OrderType,
           @OrderTableId,
           @OrderStatus,
           @OrderServiceCharges,
           @OrderDiscount,
           @OrderVoucherId,
           @OrderVoucherDiscountAmount,
           @OrderTotalAmount,
           @OrderVatAmount,
           @OrderVatPercentage,
           @VatType,
           @OrderPaymentStatus,
           @OrderPaymentMethod,
           @AverageOrderPreprationTime,
           @OrderComments,
           @OrderDeliveryTime,
           @CustomerDeliveryId,
           @OrderCompletedBy,
           @CreationDate,
           @UpdateDate,
           @IsDeleted)
END  
