USE [Flyeats]
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckVoucherRedemptionEligibility]    Script Date: 2/22/2023 10:26:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_CheckVoucherRedemptionEligibility]
    @VoucherCode NVARCHAR(50),
    @UserId INT,
    @Amount DECIMAL(18, 2)
AS
BEGIN
    DECLARE @VoucherId INT
    DECLARE @RedemptionCount INT
    DECLARE @MaxRedemptionCount INT
    DECLARE @IsEligible INT

    -- Check if the voucher code exists in the table
    IF NOT EXISTS (SELECT 1 FROM Voucher WHERE VoucherCode = @VoucherCode)
    BEGIN
        SET @IsEligible = -1 -- Voucher code does not exist
        SELECT @IsEligible
        RETURN
    END

    -- Get the voucher ID for the given voucher code
    SELECT @VoucherId = VoucherId
    FROM Voucher
    WHERE VoucherCode = @VoucherCode

    -- Get the redemption count for the given user and voucher
    SELECT @RedemptionCount = COUNT(*)
    FROM VoucherRedemption
    WHERE VoucherId = @VoucherId AND UserId = @UserId

    -- Get the maximum redemption count for the voucher
    SELECT @MaxRedemptionCount = RedeemCount
    FROM Voucher
    WHERE VoucherId = @VoucherId

    -- Check if the user is eligible to redeem the voucher
    IF @RedemptionCount >= @MaxRedemptionCount
    BEGIN
        SET @IsEligible = 0 -- Not eligible
    END
    ELSE IF @Amount < (SELECT MinValue FROM Voucher WHERE VoucherId = @VoucherId)
    BEGIN
        SET @IsEligible = 1 -- Not eligible
    END
    ELSE
    BEGIN
        SET @IsEligible = 2 -- Eligible
    END
    
    SELECT @IsEligible
END
