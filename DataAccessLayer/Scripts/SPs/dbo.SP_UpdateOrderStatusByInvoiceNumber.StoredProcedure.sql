USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateOrderStatusByInvoiceNumber]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateOrderStatusByInvoiceNumber]
    @orderInvoiceNumber NVARCHAR(50),
    @newOrderStatus NVARCHAR(50)
AS
BEGIN
    DECLARE @rowsAffected INT;

    UPDATE [dbo].[Orders]
    SET [OrderStatus] = @newOrderStatus
    WHERE [OrderInvoiceNumber] = @orderInvoiceNumber;

    SELECT @rowsAffected = @@ROWCOUNT;

    IF @rowsAffected > 0
    BEGIN
        RETURN 1;
    END
    ELSE
    BEGIN
        RETURN 0;
    END
END


GO
