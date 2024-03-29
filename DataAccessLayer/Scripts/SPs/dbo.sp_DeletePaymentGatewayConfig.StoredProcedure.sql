USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletePaymentGatewayConfig]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeletePaymentGatewayConfig]
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[PaymentGatewayConfiguration]
    SET [IsDeleted] = 1,
        [UpdateDate] = GETUTCDATE()
    WHERE [Id] = @Id
    AND [IsDeleted] = 0;
END


GO
