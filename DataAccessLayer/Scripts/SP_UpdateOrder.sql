USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateBusinesInfo]    Script Date: 2/9/2023 4:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_UpdateOrder]
(
    @OrderInvoiceNumber nvarchar(255),
    @OrderStatus nvarchar(50),
    @OrderDeliveryTime int,
    @OrderCompletedBy nvarchar(50),
    @IsDeleted bit
)
AS  
BEGIN  
Declare @status int  

   update  [Order]
		SET 
      OrderInvoiceNumber = @OrderInvoiceNumber,
      OrderStatus = @OrderStatus,
      OrderDeliveryTime = @OrderDeliveryTime,
      OrderCompletedBy = @OrderCompletedBy,
      IsDeleted = @IsDeleted 
	  where OrderInvoiceNumber=@OrderInvoiceNumber;
   Begin
   SET @status = 1;
 End
    return @status;
END  
