USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPaymentGatewaysByBusinessId]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetPaymentGatewaysByBusinessId]
    @businessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[PaymentGatewayKeys]
    WHERE [BusinessId] = @businessId
END


GO
