ALter PROCEDURE [dbo].[SP_UpdateOrderStatusByInvoiceNumber]
    @orderInvoiceNumber NVARCHAR(50),
    @newOrderStatus NVARCHAR(50)
AS
BEGIN
    DECLARE @rowsAffected INT;

    UPDATE [dbo].[Order]
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
