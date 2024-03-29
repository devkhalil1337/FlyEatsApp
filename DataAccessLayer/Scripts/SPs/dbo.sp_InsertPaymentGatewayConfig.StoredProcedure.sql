USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertPaymentGatewayConfig]    Script Date: 11/25/2023 12:48:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertPaymentGatewayConfig]
(
    @BusinessId int,
    @GatewayName VARCHAR(50),
    @ApiKey VARCHAR(100),
    @ApiSecret VARCHAR(100),
    @IsActive BIT,
    @PaymentMode INT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[PaymentGatewayKeys]
    (
     	[BusinessId],
        [GatewayName],
        [ApiKey],
        [ApiSecret],
        [IsActive],
        [PaymentMode]
    )
    VALUES
    (
		@BusinessId,
        @GatewayName,
        @ApiKey,
        @ApiSecret,
        @IsActive,
        @PaymentMode
    );
END


GO
