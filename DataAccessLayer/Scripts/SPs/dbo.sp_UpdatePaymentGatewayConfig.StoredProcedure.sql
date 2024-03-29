USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePaymentGatewayConfig]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdatePaymentGatewayConfig]
(
    @Id INT,
    @GatewayName VARCHAR(50),
    @ApiKey VARCHAR(100),
    @ApiSecret VARCHAR(100),
    @IsActive BIT,
    @PaymentMode INT
)
AS
BEGIN
DECLARE @status INT
    SET NOCOUNT ON;

    UPDATE [dbo].[PaymentGatewayKeys]
    SET [GatewayName] = @GatewayName,
        [ApiKey] = @ApiKey,
        [ApiSecret] = @ApiSecret,
        [IsActive] = @IsActive,
        [PaymentMode] = @PaymentMode
    WHERE [Id] = @Id
END
BEGIN
    SET @status = 1;

    RETURN @status;
END 

GO
