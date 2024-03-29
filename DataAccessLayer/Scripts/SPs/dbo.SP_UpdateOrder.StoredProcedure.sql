USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateOrder]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateOrder]
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


GO
